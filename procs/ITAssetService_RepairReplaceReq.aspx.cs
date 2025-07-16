using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;


public partial class ITAssetService_RepairReplaceReq : System.Web.UI.Page
{

    #region Creative_Default_methods
    SqlConnection source;
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public static string pimg = "";
    public static string cimg = "";
    public string loc = "", dept = "", subdept = "", desg = "";
    public int did = 0;
    public static DataTable dtSearchAsset = new DataTable();
    public static DataTable dtNewAssetOptions = new DataTable();
    SP_Methods spm = new SP_Methods();
    string strempcode = ""; int Id;
    protected void lnkhome_Click(object sender, EventArgs e)
    {
        Response.Redirect(ReturnUrl("sitepathmain") + "default");
    }

    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }


    #endregion

    #region Page_Events

    private void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
            }

            if (Convert.ToString(Session["Empcode"]).Trim() == "")
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");

            if (Page.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
            }
            else
            {
                Page.SmartNavigation = true;
                hdnEmpCode.Value = Convert.ToString(Session["Empcode"]);
                if (!Page.IsPostBack)
                {
                    string strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());
                    FilePath.Value = strfilepath;
                    editform.Visible = true;
                    SetReqDate();
                    Fill_Search_Employee();
                    Fill_AssetNumber();
                    dtNewAssetOptions = new DataTable();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Id = Convert.ToInt32(Request.QueryString[0]);
                        GetDetails(Id);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }

    #endregion

    #region PageMethods

    protected void SendNotificationMailToHOD(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName, string redirectURL, string Remarks, string emailTo, string RequestType)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();
        var strsubject = "";
        if (RequestType == "Repair/Upgrade")
        {
            strsubject = "Asset Repair/Upgrade Request - " + EmpName;
        }
        if (RequestType == "Replacement")
        {
            strsubject = "Asset Replacement Request - " + EmpName;
        }

        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            if (RequestType == "Repair/Upgrade")
            {
                strbuild.Append("<tr><td width = '25%'> New IT asset repair/upgrade request is generated, which requires your approval.</td ></ tr>");
            }
            if (RequestType == "Replacement")
            {
                strbuild.Append("<tr><td width = '25%'> New IT asset replacement request is generated,which requires your approval.</td ></ tr>");
            }

            strbuild.Append("<tr><td width = '30%'> Please register you action.</td ></ tr>");
            strbuild.Append("<tr><td style='height:10px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='30%'>Asset Allocation Request : </td><td >" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Employee Code : </td><td >" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Employee Name : </td><td >" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Request Type : </td><td >" + RequestType + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Location : </td><td width='70%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Department :</td><td width='70%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Designation :</td><td width='70%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Remarks :</td><td width='70%'>" + Remarks + "</td></tr>"); ;
            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");
            sendMail(emailTo, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "");
        }
        catch (Exception)
        {

            throw;
        }
    }


    public void sendMail(string emailTo, string emailCc1, string emailCc2, string strsubject, string strbody, string strattchfilepath, string ccMailIDs)
    {
        try
        {
            MailMessage mail = new MailMessage();
            //Prod
            mail.To.Add(emailTo);

            //Dev
            //emailTo = "Manisha.tambade@highbartech.com";
            //mail.CC.Add("Raj.patel@highbartech.com");
            //mail.To.Add(emailTo);

            StringBuilder strsignature = new StringBuilder();
            mail.From = new MailAddress("noreply@highbartech.com");
            mail.Subject = strsubject;
            mail.Body = Convert.ToString(strbody) + Convert.ToString(strsignature);
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Low;


            if (Convert.ToString(strattchfilepath).Trim() != "")
            {
                Attachment attch = new Attachment(strattchfilepath);
                mail.Attachments.Add(attch);
            }

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.office365.com"; //Highbar SMTP

                smtp.Port = 587;
                smtp.TargetName = "STARTTLS/smtp.office365.com";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("noreply@highbartech.com", "HBT@2019");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = SMTPUserInfo;
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;
                smtp.Send(mail);
            }
            mail.Dispose();
        }
        catch (Exception)
        {

            throw;
        }
    }


    #endregion

    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string Remarks = "";
        string RequestType = "";
        int isAssigned = 1;
        
        string EmpCode = txtEmpCode.Text.ToString().Trim();
        try
        {
            if(ddl_Employee.SelectedValue=="0")
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please select employee";
                return;
            }
            RequestType = ddlReqType.SelectedItem.Text.ToString().Trim();

            if (Convert.ToString(txtRemarks.Text).Trim() == "")
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please add remarks";
                return;
            }
            else
            {
                Remarks = txtRemarks.Text.ToString().Trim();
            }

            //Assgine To IT HOD
            var AssigneTo = "";
            var HODEmail = "";
            DataTable dtHOD = new DataTable();
            //GetITHOD
            dtHOD = spm.GetITHod();
            if (dtHOD.Rows.Count > 0)
            {
                AssigneTo = Convert.ToString(dtHOD.Rows[0]["HOD"]);
                HODEmail = Convert.ToString(dtHOD.Rows[0]["Emp_Emailaddress"]);
            }
            string AppRemarks = "";
            if (hdnReqStatus.Value != "3")
            {
                //Update ITAssetServiceRequestDetails Table and insert in ITAssetServiceRequestLog
                var AARNoId = spm.InsertReqRepairReplacement(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "InsertAARNoForRepairReplacement", AssigneTo, isAssigned, 2, 1, Convert.ToString(Session["Empcode"]).Trim(), RequestType, EmpCode);
                //GetITAssetRequestDetailsByAARNo
                DataTable dtDetails = spm.GetITAssetRequestDetailsByAARNo(Convert.ToInt32(AARNoId));
                if (dtDetails.Rows.Count > 0)
                {
                    txtAssetAlloReqNo.Text = Convert.ToString(dtDetails.Rows[0]["AARNo"]);
                }

                //Save File Upload

                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                }

                #region insert or upload multiple files
                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                    if (Convert.ToString(filename).Trim() != "")
                    {
                        #region date formatting
                        if (Convert.ToString(txtReqDate.Text).Trim() != "")
                        {
                            strdate = Convert.ToString(txtReqDate.Text).Trim().Split('/');
                            strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                        }
                        #endregion

                        string AssetInventoryPath = "";
                        AssetInventoryPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());
                        bool folderExists = Directory.Exists(AssetInventoryPath);
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(AssetInventoryPath);
                        }

                        Boolean blnfile = false;
                        HttpFileCollection fileCollection = Request.Files;
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            strfileName = "";
                            HttpPostedFile uploadfileName = fileCollection[i];
                            string fileName = Path.GetFileName(uploadfileName.FileName);
                            if (uploadfileName.ContentLength > 0)
                            {
                                strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + uploadfileName.FileName.ToString();
                                filename = strfileName;
                                uploadfileName.SaveAs(Path.Combine(AssetInventoryPath, strfileName));
                                spm.InserITAssetRequestFiles(txtAssetAlloReqNo.Text.ToString().Trim(), txtEmpCode.Text.ToString().Trim(), hdnEmpCode.Value, Convert.ToString(strfileName).Trim());
                                blnfile = true;
                            }
                        }
                    }
                }
                #endregion

                //Save Asset Option List data to ITAssetServiceRequestOptionList
                string AssetNumber = "";
                string AARNo = txtAssetAlloReqNo.Text.ToString().Trim();
                //Save New Asset Options-In Repair/Upgrade case only one Asset
                if (RequestType == "Replacement")
                {
                    //Save selected Old Asset for replacement option 
                    foreach (GridViewRow row in gvAssetOption.Rows)
                    {
                        AssetNumber = row.Cells[1].Text;
                        //spm.InsertOldAssetOptionListForReplacement(AARNo, AssetNumber);
                        spm.InsertOldAssetOptionListForReplacement(AARNo, AssetNumber);
                    }
                    //Save selected New Asset for replacement option 
                    if (gvNewAssetOptions.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvNewAssetOptions.Rows)
                        {
                            AssetNumber = row.Cells[1].Text;
                            spm.InsertAssetOptionListSentBack(AARNo, AssetNumber);
                        }
                    }
                    else
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Please select asset for replacement";
                        return;
                    }
                }


                if (RequestType == "Repair/Upgrade")
                {
                    foreach (GridViewRow row in gvAssetOption.Rows)
                    {
                        AssetNumber = row.Cells[1].Text;
                        spm.InsertAssetOptionListSentBack(AARNo, AssetNumber);
                    }
                }

                var EmpName = txtEmpName.Text.ToString().Trim();
                var AssetAlloReqNo = txtAssetAlloReqNo.Text.ToString().Trim();
                //var EmpCode = txtEmpCode.Text.ToString().Trim();
                var Doj = txtDoj.Text.ToString().Trim();
                var Location = txtEmpLocation.Text.ToString().Trim();
                var Dept = txtEmpDept.Text.ToString().Trim();
                var Designation = txtEmpDesig.Text.ToString().Trim();
                var RMgr = txtRMgr.Text.ToString().Trim();
                var HOD = txtHOD.Text.ToString().Trim();
                var ReqDate = txtReqDate.Text.ToString().Trim();

                string ITAssetService = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/ITAssetService_RepairReplaceReqApp.aspx";
                string redirectURL = Convert.ToString(ITAssetService).Trim() + "?id=" + Convert.ToInt32(AARNoId) + "&type=app";
                SendNotificationMailToHOD(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", redirectURL, Remarks, HODEmail, RequestType);
                lblmessage.Visible = true;
                lblmessage.Text = "IT Asset Service Request Submitted Successfully";
                Response.Redirect("~/procs/ITAssetService.aspx");

            }
            else
            {
                var AARNO = txtAssetAlloReqNo.Text.ToString().Trim();
                //Update ITAssetServiceRequestDetails Table and insert in ITAssetServiceRequestLog
                spm.UpdateSentBkReqRepairReplacement(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateSentBkAARNoForRepairReplacement", AssigneTo, isAssigned, 2, 1, Convert.ToString(Session["Empcode"]).Trim(), RequestType, EmpCode,Convert.ToInt32(hdnId.Value), AARNO);

                //Save File Upload
                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                }

                #region insert or upload multiple files
                if (uploadfile.HasFile)
                {
                    filename = uploadfile.FileName;
                    if (Convert.ToString(filename).Trim() != "")
                    {
                        #region date formatting
                        if (Convert.ToString(txtReqDate.Text).Trim() != "")
                        {
                            strdate = Convert.ToString(txtReqDate.Text).Trim().Split('/');
                            strfromDate = Convert.ToString(strdate[0]) + Convert.ToString(strdate[1]) + Convert.ToString(strdate[2]);
                        }
                        #endregion

                        string AssetInventoryPath = "";
                        AssetInventoryPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["AssetInventoryPath"]).Trim());
                        bool folderExists = Directory.Exists(AssetInventoryPath);
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(AssetInventoryPath);
                        }

                        Boolean blnfile = false;
                        HttpFileCollection fileCollection = Request.Files;
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            strfileName = "";
                            HttpPostedFile uploadfileName = fileCollection[i];
                            string fileName = Path.GetFileName(uploadfileName.FileName);
                            if (uploadfileName.ContentLength > 0)
                            {
                                strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + uploadfileName.FileName.ToString();
                                filename = strfileName;
                                uploadfileName.SaveAs(Path.Combine(AssetInventoryPath, strfileName));
                                spm.InserITAssetRequestFiles(txtAssetAlloReqNo.Text.ToString().Trim(), txtEmpCode.Text.ToString().Trim(), hdnEmpCode.Value, Convert.ToString(strfileName).Trim());
                                blnfile = true;
                            }
                        }
                    }
                }
                #endregion

                //Save Asset Option List data to ITAssetServiceRequestOptionList
                string AssetNumber = "";
                string AARNo = txtAssetAlloReqNo.Text.ToString().Trim();
                //Save New Asset Options-In Repair/Upgrade case only one Asset
                if (RequestType == "Replacement")
                {
                    //Save selected Old Asset for replacement option 
                    foreach (GridViewRow row in gvAssetOption.Rows)
                    {
                        AssetNumber = row.Cells[1].Text;
                        spm.InsertOldAssetOptionListForReplacement(AARNo, AssetNumber);
                    }
                    //Save selected New Asset for replacement option 
                    if (gvNewAssetOptions.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvNewAssetOptions.Rows)
                        {
                            AssetNumber = row.Cells[1].Text;
                            spm.InsertAssetOptionListSentBack(AARNo, AssetNumber);
                        }
                    }
                    else
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Please select asset for replacement";
                        return;
                    }
                }


                if (RequestType == "Repair/Upgrade")
                {
                    foreach (GridViewRow row in gvAssetOption.Rows)
                    {
                        AssetNumber = row.Cells[1].Text;
                        spm.InsertAssetOptionListSentBack(AARNo, AssetNumber);
                    }
                }

                var EmpName = txtEmpName.Text.ToString().Trim();
                var AssetAlloReqNo = txtAssetAlloReqNo.Text.ToString().Trim();
                //var EmpCode = txtEmpCode.Text.ToString().Trim();
                var Doj = txtDoj.Text.ToString().Trim();
                var Location = txtEmpLocation.Text.ToString().Trim();
                var Dept = txtEmpDept.Text.ToString().Trim();
                var Designation = txtEmpDesig.Text.ToString().Trim();
                var RMgr = txtRMgr.Text.ToString().Trim();
                var HOD = txtHOD.Text.ToString().Trim();
                var ReqDate = txtReqDate.Text.ToString().Trim();

                string ITAssetService = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/ITAssetService_RepairReplaceReqApp.aspx";
                string redirectURL = Convert.ToString(ITAssetService).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=app";
                SendNotificationMailToHOD(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", redirectURL, Remarks, HODEmail, RequestType);
                lblmessage.Visible = true;
                lblmessage.Text = "IT Asset Service Request Submitted Successfully";
                Response.Redirect("~/procs/ITAssetService.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    public void Fill_Search_Employee()
    {

        DataTable dtEmp = new DataTable();
        dtEmp = spm.SearchEmp();
        ddl_Employee.DataSource = dtEmp;
        ddl_Employee.DataTextField = "empname";
        ddl_Employee.DataValueField = "Emp_Code";
        ddl_Employee.DataBind();
        ListItem item = new ListItem("Select Employee", "0");
        ddl_Employee.Items.Insert(0, item);
    }
    public void SearchAsset()
    {
        string AssetNo = null;
        string SrNo = null;
        string Description = null;

        if (ddl_AssetNumber.SelectedValue != "0")
            AssetNo = ddl_AssetNumber.SelectedItem.Text.ToString().Trim();
        if (txtSrNo.Text.ToString().Trim() != "")
            SrNo = txtSrNo.Text.ToString().Trim();
        if (txtDesc.Text.ToString().Trim() != "")
            Description = txtDesc.Text.ToString().Trim();

        dtSearchAsset = spm.SearchAssetNo(AssetNo, SrNo, Description);
        if (dtSearchAsset.Rows.Count > 0)
        {
            gvSearchedAsset.DataSource = dtSearchAsset;
            gvSearchedAsset.DataBind();
            lblAssetOptions.Visible = false;
        }
    }
    public void Fill_AssetNumber()
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.FillDdlAssetNo();
        if (dtDetails.Rows.Count > 0)
        {
            ddl_AssetNumber.DataSource = dtDetails;
            ddl_AssetNumber.DataTextField = "AssetNumber";
            ddl_AssetNumber.DataValueField = "id";
            ddl_AssetNumber.DataBind();
            ListItem item = new ListItem("Select Asset Number", "0");
            ddl_AssetNumber.Items.Insert(0, item);
        }
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
    }

    protected void ddlReqType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReqType.SelectedItem.Text == "Repair/Upgrade")
        {
            lblAssetOps.Visible = false;
            liOldAssetOptions.Visible = false;
            lblNewAssetOptions.Visible = false;
            lblAssetOptions.Visible = true;
            liddlAsetNo.Visible = false;
            litxtSrNo.Visible = false;
            litxtDesc.Visible = false;
            claimmob_btnSubmit.Visible = false;
        }
        else
        {
            ddlReqType.SelectedItem.Text = "Replacement";
            lblAssetOps.Visible = true;
            liOldAssetOptions.Visible = true;
            lblNewAssetOptions.Visible = true;
            lblAssetOptions.Visible = false;
            liddlAsetNo.Visible = true;
            litxtSrNo.Visible = true;
            litxtDesc.Visible = true;
            claimmob_btnSubmit.Visible = true;

        }
    }
    protected void ddl_AssetNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_AssetNumber.SelectedIndex != 0)
        {
            SearchAsset();
        }
    }

    protected void ddl_Employee_SelectedIndexChanged(object sender, EventArgs e)
    {
        string EmpCode = ddl_Employee.SelectedValue;
        GetEmployeeDetails(EmpCode);
        loadAllocationGrid(EmpCode);
    }
    public void loadAllocationGrid(string EmpCode)
    {
        DataTable dt = new DataTable();
        dt = spm.GetAllocatedAssetList(EmpCode);
        if (dt.Rows.Count > 0)
        {
            gvproject.DataSource = dt;
            gvproject.DataBind();
            gvproject.Visible = true;
        }
    }

    protected void ChckedChanged(object sender, EventArgs e)
    {
        lblmessage.Visible = false;
        string AssetNumber = "";
        DataTable dtSelectedOptions = new DataTable();
        if (dtSelectedOptions.Rows.Count > 0)
        {
            dtSelectedOptions.Rows.Clear();
        }

        if (dtSelectedOptions.Columns.Count == 0)
        {
            dtSelectedOptions.Columns.Add("Id");
            dtSelectedOptions.Columns.Add("AssetNumber");
            dtSelectedOptions.Columns.Add("AssetType");
            dtSelectedOptions.Columns.Add("AssetDesc");
            dtSelectedOptions.Columns.Add("SrNo");
            dtSelectedOptions.Columns.Add("BrandName");
            dtSelectedOptions.Columns.Add("Model");
            dtSelectedOptions.Columns.Add("AgeOfAsset");
            dtSelectedOptions.Columns.Add("CPUMakeAndModel");
            dtSelectedOptions.Columns.Add("RAM");
            dtSelectedOptions.Columns.Add("HDD");
        }
        foreach (GridViewRow row in gvproject.Rows)
            if (((RadioButton)row.Cells[0].FindControl("rbSelect")).Checked)
            {

                AssetNumber = row.Cells[2].Text;

                if (ddlReqType.SelectedItem.Text == "Repair/Upgrade")
                {
                    if (AssetNumber.Contains("RNT"))
                    {
                        lblmessage.Visible = true;
                        lblmessage.Text = "Please select own asset to repair";
                        return;
                    }
                }
                DataRow[] dr = dtSelectedOptions.Select("AssetNumber = '" + AssetNumber + "'");
                if (dr.Length == 0)
                {
                    dtSelectedOptions.Rows.Add(row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text,
                    row.Cells[4].Text, row.Cells[5].Text, row.Cells[6].Text, row.Cells[7].Text,
                    row.Cells[8].Text, row.Cells[9].Text, row.Cells[10].Text, row.Cells[11].Text);
                }

            }

        if (dtSelectedOptions.Rows.Count > 0)
        {
            gvAssetOption.DataSource = dtSelectedOptions;
            gvAssetOption.DataBind();

        }
        if (ddlReqType.SelectedItem.Text == "Repair/Upgrade")
        {
            lblAssetOptions.Visible = true;
            lblAssetOps.Visible = false;
            liOldAssetOptions.Visible = false;
            lblNewAssetOptions.Visible = false;
            mobile_btnBack.Visible = false;
            liddlAsetNo.Visible = false;
            litxtSrNo.Visible = false;
            litxtDesc.Visible = false;
        }
        else
        {
            lblAssetOptions.Visible = false;
            lblAssetOps.Visible = true;
            liOldAssetOptions.Visible = true;
            lblNewAssetOptions.Visible = true;
            mobile_btnBack.Visible = true;
            liddlAsetNo.Visible = true;
            litxtSrNo.Visible = true;
            litxtDesc.Visible = true;
        }
    }

    protected void SearchbtnSubmit_Click(object sender, EventArgs e)
    {
        SearchAsset();
    }

    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
        string AssetNumber = "";
        if (dtNewAssetOptions.Columns.Count == 0)
        {
            dtNewAssetOptions.Columns.Add("Id");
            dtNewAssetOptions.Columns.Add("AssetNumber");
            dtNewAssetOptions.Columns.Add("AssetType");
            dtNewAssetOptions.Columns.Add("AssetDesc");
            dtNewAssetOptions.Columns.Add("SrNo");
            dtNewAssetOptions.Columns.Add("BrandName");
            dtNewAssetOptions.Columns.Add("Model");
            dtNewAssetOptions.Columns.Add("AgeOfAsset");
            dtNewAssetOptions.Columns.Add("CPUMakeAndModel");
            dtNewAssetOptions.Columns.Add("RAM");
            dtNewAssetOptions.Columns.Add("HDD");
        }

        foreach (GridViewRow row in gvSearchedAsset.Rows)
            if (((CheckBox)row.Cells[0].FindControl("chkSelect")).Checked)
            {
                AssetNumber = row.Cells[2].Text;
                DataRow[] dr = dtNewAssetOptions.Select("AssetNumber = '" + AssetNumber + "'");
                if (dr.Length == 0)
                {
                    dtNewAssetOptions.Rows.Add(row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text,
                    row.Cells[4].Text, row.Cells[5].Text, row.Cells[6].Text, row.Cells[7].Text,
                    row.Cells[8].Text, row.Cells[9].Text, row.Cells[10].Text, row.Cells[11].Text);
                }
            }
        if (dtNewAssetOptions.Rows.Count > 0)
        {
            gvNewAssetOptions.DataSource = dtNewAssetOptions;
            gvNewAssetOptions.DataBind();
            gvNewAssetOptions.Visible = true;
        }
        lblAssetOptions.Visible = false;
    }

    protected void gvNewAssetOptions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edititem")
        {
            //id = Convert.ToString(e.CommandArgument);
            //Response.Redirect(string.Format(ConfigurationManager.AppSettings["sitepath"] + "CustodianAdd.aspx?id={0}", id.ToString()));
        }

        else if (e.CommandName.ToLower() == "deleteitem")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DataRow[] rows = dtNewAssetOptions.Select("Id = '" + id + "'");
            foreach (DataRow row in rows)
            {
                dtNewAssetOptions.Rows.Remove(row);
            }
            dtNewAssetOptions.AcceptChanges();
            if (dtNewAssetOptions.Rows.Count > 0)
            {
                gvNewAssetOptions.DataSource = dtNewAssetOptions;
                gvNewAssetOptions.DataBind();
                lblAssetOptions.Visible = true;
            }
            else
            {
                gvNewAssetOptions.DataSource = dtNewAssetOptions;
                gvNewAssetOptions.DataBind();
            }
        }
    }

    public void SetReqDate()
    {
        var getDate = DateTime.Now.ToString("dd/MM/yyyy");
        getDate = getDate.Replace('-', '/');
        txtReqDate.Text = getDate;
    }

    public void GetDetails(int Id)
    {
        try
        {
            string EmpCode = "";
            int ReqStatus;
            string RequestType = "";

            DataTable dtDetails = new DataTable();
            dtDetails = spm.GetDetails(Id);
            if (dtDetails.Rows.Count > 0)
            {
                ddlReqType.SelectedItem.Text = Convert.ToString(dtDetails.Rows[0]["RequestType"]);
                EmpCode = Convert.ToString(dtDetails.Rows[0]["EmpCode"]);
                txtAssetAlloReqNo.Text = Convert.ToString(dtDetails.Rows[0]["AARNo"]);
                txtAppRemarks.Text = Convert.ToString(dtDetails.Rows[0]["AppRemarks"]);
                txtRemarks.Text = Convert.ToString(dtDetails.Rows[0]["Remarks"]);
                txtReqDate.Text = Convert.ToString(dtDetails.Rows[0]["nCreatedDate"]);
                ReqStatus = Convert.ToInt32(dtDetails.Rows[0]["Status"]);
                hdnReqStatus.Value = Convert.ToString(ReqStatus);
                RequestType = Convert.ToString(dtDetails.Rows[0]["RequestType"]);
                GetEmployeeDetails(EmpCode);
                if (ReqStatus == 3)
                {
                    AppRemark.Visible = true;
                    gvAppFiles.Visible = true;
                    ddlReqType.Enabled = false;
                    ddl_Employee.Enabled = false;
                    GetFilesDetails(txtAssetAlloReqNo.Text.ToString().Trim(), EmpCode, hdnEmpCode.Value);

                    if (RequestType == "Repair/Upgrade")
                    {
                        lblAssetOps.Visible = false;
                        liOldAssetOptions.Visible = false;
                        lblNewAssetOptions.Visible = false;
                        lblAssetOptions.Visible = true;
                        liddlAsetNo.Visible = false;
                        litxtSrNo.Visible = false;
                        litxtDesc.Visible = false;
                        gvproject.Visible = true;
                        loadAllocationGrid(EmpCode);
                        GetAppFilesDetails(txtAssetAlloReqNo.Text.ToString().Trim(), EmpCode, "00003851");
                        GetOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
                    }
                    if (RequestType == "Replacement")
                    {
                        lblAssetOps.Visible = true;
                        liOldAssetOptions.Visible = true;
                        lblNewAssetOptions.Visible = true;
                        lblAssetOptions.Visible = false;
                        liddlAsetNo.Visible = true;
                        litxtSrNo.Visible = true;
                        litxtDesc.Visible = true;
                        mobile_btnBack.Visible = true;

                        GetOldOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
                        GetNewOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
                        loadAllocationGrid(EmpCode);
                        GetAppFilesDetails(txtAssetAlloReqNo.Text.ToString().Trim(), EmpCode, "00003851");
                        claimmob_btnSubmit.Visible = true;

                    }
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }
    public void GetEmployeeDetails(string EmpCode)
    {
        try
        {
            DataTable dtEmpDetails = new DataTable();
            dtEmpDetails = spm.GetEmpData(EmpCode);
            if (dtEmpDetails.Rows.Count > 0)
            {
                txtEmpCode.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Code"]);
                txtEmpName.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Name"]);
                ddl_Employee.SelectedValue = Convert.ToString(dtEmpDetails.Rows[0]["Emp_Code"]);
                txtEmpLocation.Text = Convert.ToString(dtEmpDetails.Rows[0]["emp_location"]);
                txtEmpDesig.Text = Convert.ToString(dtEmpDetails.Rows[0]["Designation"]);
                txtEmpDept.Text = Convert.ToString(dtEmpDetails.Rows[0]["Department"]);
                txtEmpType.Text = Convert.ToString(dtEmpDetails.Rows[0]["Particulars"]);
                txtDoj.Text = Convert.ToString(dtEmpDetails.Rows[0]["doj"]);
                txtMainModule.Text = Convert.ToString(dtEmpDetails.Rows[0]["emp_module"]);
                txtRMgr.Text = Convert.ToString(dtEmpDetails.Rows[0]["RM"]);
                txtHOD.Text = Convert.ToString(dtEmpDetails.Rows[0]["HOD"]);
                txtEmail.Text = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);
                txtMobileNo.Text = Convert.ToString(dtEmpDetails.Rows[0]["mobile"]);
                txtCurAddress.Text = Convert.ToString(dtEmpDetails.Rows[0]["C_ADD"]);
                txtPerAddress.Text = Convert.ToString(dtEmpDetails.Rows[0]["P_ADD"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
        }
    }

    public void GetAppFilesDetails(string AARNo, string EmpCode, string CustodianCode)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetFileDetails(AARNo, EmpCode, CustodianCode);
        gvAppViewFiles.DataSource = null;
        gvAppViewFiles.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvAppViewFiles.DataSource = dtDetails;
            gvAppViewFiles.DataBind();
        }
        gvAppFiles.Visible = true;
        gvAppViewFiles.Visible = true;
    }

    public void GetFilesDetails(string AARNo, string EmpCode, string CustodianCode)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetFileDetails(AARNo, EmpCode, CustodianCode);
        gvViewFiles.DataSource = null;
        gvViewFiles.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvViewFiles.DataSource = dtDetails;
            gvViewFiles.DataBind();
            gvViewFiles.Visible = true;
            CustViewFiles.Visible = true;
        }
    }
    public void GetOptionList(string AARNo)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetOptionList(AARNo);
        gvAssetOption.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvAssetOption.DataSource = dtDetails;
            gvAssetOption.DataBind();
        }
        lblAssetOptions.Visible = true;
    }

    public void GetOldOptionList(string AARNo)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetOldOptionList(AARNo);
        gvAssetOption.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvAssetOption.DataSource = dtDetails;
            gvAssetOption.DataBind();
        }
    }

    public void GetNewOptionList(string AARNo)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetOptionList(AARNo);
        gvNewAssetOptions.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvNewAssetOptions.DataSource = dtDetails;
            gvNewAssetOptions.DataBind();
        }
    }

    protected void gvproject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string EmpCode = txtEmpCode.Text.ToString().Trim();
        gvproject.PageIndex = e.NewPageIndex;
        loadAllocationGrid(EmpCode);
    }
    protected void gvSearchedAsset_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvproject.PageIndex = e.NewPageIndex;
        SearchAsset();
    }
}