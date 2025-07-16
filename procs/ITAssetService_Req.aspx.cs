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
using System.Activities.Expressions;
using Microsoft.Ajax.Utilities;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection.Emit;
using System.Reflection;


public partial class ITAssetService_Req : System.Web.UI.Page
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
    public static DataTable dtSelectedOptions = new DataTable();
    //public static DataTable dtSelectedelet = new DataTable();
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
                    dtSelectedOptions = new DataTable();
                    // dtSelectedelet = new DataTable();
                    Fill_AssetNumber();

                    Fill_Soft_name();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Id = Convert.ToInt32(hdnId.Value);
                        GetDetails(Id); 
                        GetDetailssoftlist();
                       // getSoftware_list();


                    }

                    // getSoftware_list();

                    //if (Convert.ToString(Session["Empcode"]) == TaskOwnerId)
                    //{
                    //    GetTaskDetails(SubtaskId);
                    //    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                    //    txtActionDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    //}
                    //else
                    //{
                    //    claimmob_btnSubmit.Enabled = false;
                    //    lnkviewfile.Visible = false;
                    //}
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
    public void GetAssetAllocationReqNo()
    {
        try
        {
            var getData = spm.GetAssetAllocationReqNo();
            if (getData.Rows.Count > 0)
            {
                var getId = getData.Rows[0]["AssetAllocationReq"].ToString();
                var getDate = DateTime.Now.ToString("dd-MM-yyyy HH:MM:ss");
                txtAssetAlloReqNo.Text = getId;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void SendNotificationMail(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName, string redirectURL, string emailTo)
    {

        // MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();

        var strsubject = "New Joinee Asset Allocation Approval - " + EmpName;

        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> Following Employee has been onboarded in OneHR, and IT team has provided options to allocate Asset.</td ></ tr>");
            strbuild.Append("<tr><td width = '25%'> Please register you action.</td ></ tr>");
            strbuild.Append("<tr><td style='height:10px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='30%'>Asset Allocation Request : </td><td >" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Employee Code : </td><td >" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Employee Name : </td><td >" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Onboarding Date : </td><td width='75%'>" + Doj + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Location : </td><td width='75%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Department :</td><td width='75%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Designation :</td><td width='75%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Reporting Manager :</td><td width='75%'>" + RMgr + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>HOD :</td><td width='75%'>" + HOD + "</td></tr>");
            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            //sendMail(emailTo , "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "");
            spm.sendMail(emailTo, strsubject, Convert.ToString(strbuild).Trim(), "", "");

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
            if (emailCc1 != "")
            { mail.CC.Add(emailCc1); }

            if (emailCc2 != "")
            { mail.CC.Add(emailCc2); }

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

            //SmtpClient smtp = new SmtpClient();

            //smtp.Host = "smtp.office365.com"; //Highbar SMTP
            //smtp.Port = 587;

            //System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("noreply@highbartech.com", "HBT@2019");
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = SMTPUserInfo;
            //smtp.EnableSsl = true;
            //smtp.Send(mail);

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


    public string CheckIsTaskOwnerLoggedUser(int SubTaskId)
    {
        var TaskOwner = "";
        DataTable dttaskdetails = spm.GetTaskHistorydtlsBySubTakId(SubTaskId);
        if (dttaskdetails != null && dttaskdetails.Rows.Count > 0)
        {
            string TaskId = Convert.ToString(dttaskdetails.Rows[0]["TaskId"]);
            dttaskdetails = spm.GetTaskDetails(TaskId);
            if (dttaskdetails.Rows.Count > 0)
            {
                TaskOwner = Convert.ToString(dttaskdetails.Rows[0]["TaskOwner"]);
            }
        }
        return TaskOwner;
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
        int isAssigned = Convert.ToInt32(ddlAssignment.SelectedValue);

        try
        {
            if (ddlAssignment.SelectedValue == "0")
            {
                if (Convert.ToString(txtRemarks.Text).Trim() == "")
                {
                    lblmessage.Text = "Please enter remarks.";
                    return;
                }
                else
                {
                    Remarks = txtRemarks.Text.ToString().Trim();
                }
            }
            if (Convert.ToString(txtRemarks.Text).Trim() != "")
            {
                Remarks = txtRemarks.Text.ToString().Trim();
            }
            if (uploadfile.HasFile)
            {
                filename = uploadfile.FileName;
            }

            foreach (GridViewRow row in DgvApprover.Rows)
            {
                DataSet dsList = new DataSet();
                SqlParameter[] spars = new SqlParameter[4];
                string sSoftId = Convert.ToString(DgvApprover.DataKeys[row.RowIndex].Values[0]).Trim();
                // string asset = Convert.ToString(gvAssetOption.DataKeys[row.RowIndex].Values[0]).Trim();

                spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                spars[0].Value = "insert_AssetSoft_mainTable";

                spars[1] = new SqlParameter("@soft_id", SqlDbType.Int);
                spars[1].Value = Convert.ToInt32(sSoftId);

                //spars[2] = new SqlParameter("@AssetNumber", SqlDbType.NVarChar);
                //spars[2].Value = Convert.ToString(ddl_AssetNumber.SelectedValue).Trim();
                // spars[2].Value = Convert.ToString(ddl_AssetNumber.SelectedValue).Trim();
                //  ddl_AssetNumber.SelectedItem.Text.ToString().Trim();

                spars[3] = new SqlParameter("@AssignedTo", SqlDbType.VarChar);
                spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

                foreach (GridViewRow assetRow in gvAssetOption.Rows)
                {
                    spars[2] = new SqlParameter("@AssetNumber", SqlDbType.NVarChar);
                    spars[2].Value = Convert.ToString(assetRow.Cells[0].Text).Trim();

                    dsList = spm.getDatasetList(spars, "SP_Admin_AssetInventory");

                }

                //  dsList = spm.getDatasetList(spars, "SP_Admin_AssetInventory");
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
                    //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                    //    FuelclaimPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["PaymentVoucherDocumentpath"]).Trim());
                    //else
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
                            //if (Convert.ToString(hdnRemid.Value).Trim() != "0")
                            //    strfileName = hdnRemid.Value + "_" + txtEmpCode.Text + "_" + Convert.ToString(hdnclaimidO.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_payment_Voucher" + Path.GetExtension(uploadfileName.FileName);
                            //else
                            //strfileName = txtEmpCode.Text + "_" + Convert.ToString(hdnclaimqry.Value) + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + "_payment_Voucher" + Path.GetExtension(uploadfileName.FileName);
                            strfileName = txtEmpCode.Text + "_" + strfromDate + "_" + Convert.ToString(i + 1).Trim() + uploadfileName.FileName.ToString();
                            filename = strfileName;
                            uploadfileName.SaveAs(Path.Combine(AssetInventoryPath, strfileName));

                            //spm.InsertpaymentUploaded_Files(Convert.ToDecimal(hdnRemid.Value), blnfile, Convert.ToString(strfileName).Trim(), "paymentVoucher_insertTmp", i + 1, Convert.ToString(txtEmpCode.Text).Trim(), Convert.ToInt32(hdnclaimqry.Value));
                            spm.InserITAssetRequestFiles(txtAssetAlloReqNo.Text.ToString().Trim(), txtEmpCode.Text.ToString().Trim(), hdnEmpCode.Value, Convert.ToString(strfileName).Trim());
                            blnfile = true;
                        }
                    }

                }
            }
            #endregion

            //Assgine To IT HOD
            var AssigneTo = ""; string emailTo = "";
            DataTable dtHOD = new DataTable();
            //GetITHOD
            dtHOD = spm.GetITHod();
            if (dtHOD.Rows.Count > 0)
            {
                AssigneTo = Convert.ToString(dtHOD.Rows[0]["HOD"]);
                emailTo = Convert.ToString(dtHOD.Rows[0]["Emp_Emailaddress"]);
            }
            //Update ITAssetServiceRequestDetails Table and insert in ITAssetServiceRequestLog
            string AppRemarks = "";
            spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 2, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));

            //Save sset Option List data to ITAssetServiceRequestOptionList
            foreach (GridViewRow row in gvAssetOption.Rows)
            {
                string AssetNumber = row.Cells[0].Text;
                string AARNo = txtAssetAlloReqNo.Text.ToString().Trim();
                spm.InsertAssetOptionListSentBack(AARNo, AssetNumber);
            }

            var EmpName = txtEmpName.Text.ToString().Trim();
            var AssetAlloReqNo = txtAssetAlloReqNo.Text.ToString().Trim();
            var EmpCode = txtEmpCode.Text.ToString().Trim();
            var Doj = txtDoj.Text.ToString().Trim();
            var Location = txtEmpLocation.Text.ToString().Trim();
            var Dept = txtEmpDept.Text.ToString().Trim();
            var Designation = txtEmpDesig.Text.ToString().Trim();
            var RMgr = txtRMgr.Text.ToString().Trim();
            var HOD = txtHOD.Text.ToString().Trim();
            var ReqDate = txtReqDate.Text.ToString().Trim();

            string ITAssetService_Req_App = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/ITAssetService_Req_App.aspx";
            string redirectURL = Convert.ToString(ITAssetService_Req_App).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=app";
            SendNotificationMail(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", redirectURL, emailTo);
            dtSelectedOptions.Rows.Clear();
            dtSelectedOptions.AcceptChanges();
           // GetDetailssoftlist();

            lblmessage.Visible = true;
            lblmessage.Text = "IT Asset Service Request Submitted Successfully";
            Response.Redirect("~/procs/ITAssetService.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
    }
    protected void ddlAssignment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAssignment.SelectedValue == "1")
        {
            ddl_AssetNumber.Enabled = true;
            txtSrNo.Enabled = true;
            txtDesc.Enabled = true;
            mobile_cancel.Enabled = true;
            mobile_btnBack.Enabled = true;
            gvproject.Visible = true;
        }
        else
        {
            ddl_AssetNumber.Enabled = false;
            txtSrNo.Enabled = false;
            txtDesc.Enabled = false;
            mobile_cancel.Enabled = false;
            mobile_btnBack.Enabled = false;
            gvproject.Visible = false;

        }
    }

    protected void gvAssetOption_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "edititem")
        {
            //id = Convert.ToString(e.CommandArgument);
            //Response.Redirect(string.Format(ConfigurationManager.AppSettings["sitepath"] + "CustodianAdd.aspx?id={0}", id.ToString()));
        }

        else if (e.CommandName.ToLower() == "deleteitem")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            DataRow[] rows = dtSelectedOptions.Select("Id = '" + id + "'");
            foreach (DataRow row in rows)
            {
                dtSelectedOptions.Rows.Remove(row);
            }
            dtSelectedOptions.AcceptChanges();
            if (dtSelectedOptions.Rows.Count > 0)
            {
                gvAssetOption.DataSource = dtSelectedOptions;
                gvAssetOption.DataBind();
                lblAssetOptions.Visible = true;
            }
        }


    }

    protected void DgvApprover_softlist(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName.ToLower() == "edititems")
        //{
        //    //id = Convert.ToString(e.CommandArgument);
        //    //Response.Redirect(string.Format(ConfigurationManager.AppSettings["sitepath"] + "CustodianAdd.aspx?id={0}", id.ToString()));
        //}

        //else if (e.CommandName.ToLower() == "DeleteItems")
        //{
        //    int soft_Id = Convert.ToInt32(e.CommandArgument);
        //    DataRow[] rows = dtSelectedelet.Select("soft_Id = '" + soft_Id + "'");
        //    foreach (DataRow row in rows)
        //    {
        //        dtSelectedelet.Rows.Remove(row);
        //    }
        //    dtSelectedelet.AcceptChanges();
        //    if (dtSelectedelet.Rows.Count > 0)
        //    {
        //        DgvApprover.DataSource = dtSelectedelet;
        //        DgvApprover.DataBind();

        //    }
        //}


    }



    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
        string AssetNumber = "";
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
            if (((CheckBox)row.Cells[0].FindControl("chkSelect")).Checked)
            {
                // DataRow[] foundAuthors = dtSelectedOptions.Select("Author = '" + searchAuthor + "'");
                AssetNumber = row.Cells[2].Text;
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
            lblAssetOptions.Visible = true;
        }
    }
    protected void lbSearchAsset_Click(object sender, EventArgs e)
    {
        SearchAsset();
       //GetDetailssoftlist();
    }
    public void GetDetailssoftlist()
    {
        DataSet dsList1 = new DataSet();
        SqlParameter[] spars1 = new SqlParameter[2];


        spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars1[0].Value = "deleted_record";
   

        dsList1 = spm.getDatasetList(spars1, "SP_Admin_AssetInventory");
    }

    public void GetDetails(int Id)
    {
        try
        {
            string EmpCode = "";
            int ReqStatus;
            var getDate = DateTime.Now.ToString("dd/MM/yyyy");
            getDate = getDate.Replace('-', '/');
            txtReqDate.Text = getDate;

            DataTable dtDetails = new DataTable();
            dtDetails = spm.GetDetails(Id);
            if (dtDetails.Rows.Count > 0)
            {
                EmpCode = Convert.ToString(dtDetails.Rows[0]["EmpCode"]);
                txtAssetAlloReqNo.Text = Convert.ToString(dtDetails.Rows[0]["AARNo"]);
                txtAppRemarks.Text = Convert.ToString(dtDetails.Rows[0]["AppRemarks"]);
                txtRemarks.Text = Convert.ToString(dtDetails.Rows[0]["Remarks"]);
                ReqStatus = Convert.ToInt32(dtDetails.Rows[0]["Status"]);
                if ((ReqStatus == 4) || (ReqStatus == 5))
                {
                    ddlAssignment.Enabled = false;
                    txtSrNo.Enabled = false;
                    ddl_AssetNumber.Enabled = false;
                    txtDesc.Enabled = false;
                    mobile_cancel.Enabled = false;
                    gvproject.Enabled = false;
                    mobile_btnBack.Enabled = false;
                    gvAssetOption.Enabled = false;
                    txtRemarks.Enabled = false;
                    mobile_btnSave.Enabled = false;
                }
                if (ReqStatus == 3)
                {
                    //gvAppViewFiles.Visible = true;
                    txtAppRemarks.Visible = true;
                    gvAppFiles.Visible = true;
                    HrgvFiles.Visible = true;
                    AppRemark.Visible = true;
                    lblAppDtls.Visible = true;
                    softname.Visible = false;
                    softlist.Visible = false;
                   // btn_del.Visible = false;
                    GetAppFilesDetails(txtAssetAlloReqNo.Text.ToString().Trim(), EmpCode, "00003851");
                    GetOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
                  //  getSoftware_list();
                }
                GetEmployeeDetails(EmpCode);
                 


            }

        }

        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            throw;
        }
    }

    //public void GetsoftwareDetails( )
    //{
         

    //     SqlParameter[] spars2 = new SqlParameter[2];

    //        spars2[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
    //        spars2[0].Value = "getSoftware_list_maintable";
    //        foreach (GridViewRow assetRow in gvAssetOption.Rows)
    //        {

    //            spars2[1] = new SqlParameter("@AssetNumber", SqlDbType.NVarChar);
    //            spars2[1].Value = Convert.ToString(assetRow.Cells[0].Text).Trim();

    //            DataTable dssoftwareList = spm.getDataList(spars2, "SP_Admin_AssetInventory");

    //        DgvApprover.DataSource = null;
    //        DgvApprover.DataBind();
    //        if (dssoftwareList.Rows.Count > 0)
    //        {
    //            DgvApprover.DataSource = dssoftwareList;
    //            DgvApprover.DataBind();
    //        }
             
    //    }
            
    //  getSoftware_list();

    //}

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
    public void GetOptionList(string AARNo)
    {
        DataTable dtDetails = new DataTable();
        dtDetails = spm.GetOptionList(AARNo);
        //gvAssetOption.DataSource = null;
        gvAssetOption.DataBind();
        if (dtDetails.Rows.Count > 0)
        {
            gvAssetOption.DataSource = dtDetails;
            gvAssetOption.DataBind();
        }
        lblAssetOptions.Visible = true;
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
            gvproject.DataSource = dtSearchAsset;
            gvproject.DataBind();
            mobile_btnBack.Visible = true;
            lblAssetOptions.Visible = true;
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

    protected void btnOut_Details_Click(object sender, EventArgs e)
    {
        //string Med_sex = "";
        //if (Convert.ToString(ddlMaritalStatus.SelectedValue).Trim() == "0")
        //{
        //    lblmessage.Text = "Please select marital status";
        //    return;
        //}
        //if (ddlRelationMed.SelectedValue == "Select")
        //{
        //    lblmessage.Text = "Please select Relation with Employee!";
        //    return;
        //}
        //if (Convert.ToString(txtMember_Name.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please enter Member Name!";
        //    return;
        //}
        //if (Convert.ToString(txtFromdateMain.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Please enter submission date";
        //    return;
        //}
        //if (Convert.ToString(txtBirthdatedate_Med.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Birthdate cannot be blank";
        //    return;
        //}

        //if (Convert.ToString(txtAge_Med.Text).Trim() == "")
        //{
        //    lblmessage.Text = "Birthdate cannot be blank";
        //    return;
        //}
        //AssigningSessions();
        ////Response.Redirect("~/procs/fuelClaim_Out.aspx?clmid=0&rem_id=" + hdnRemid.Value + "&inb=0");

        //string[] strdate;
        //#region Validations
        //if (((ddlRelationMed.SelectedValue == "Children1") && (Convert.ToInt32(txtAge_Med.Text.Trim()) > 25)) || ((ddlRelationMed.SelectedValue == "Children2") && (Convert.ToInt32(txtAge_Med.Text.Trim()) > 25)))
        //{
        //    lblmessage.Text = "Children above age 25 yrs not allowed.";
        //    return;
        //}
        ////if (Convert.ToString(txtRelationMed.Text).Trim() == "")
        ////{
        ////    lblmessage.Text = "Please enter Relation with Employee!";
        ////    return;
        ////}

        //if (Med_male.Checked)
        //{
        //    Med_sex = "Male";
        //}
        //else
        //{
        //    Med_sex = "Female";
        //}
        //if (Med_sex == "")
        //{
        //    lblmessage.Text = "Please Select Gender!";
        //    return;
        //}
        //#endregion

        //string strfromDate = "";

        //if (Convert.ToString(txtBirthdatedate_Med.Text).Trim() != "")
        //{
        //    strdate = Convert.ToString(txtBirthdatedate_Med.Text).Trim().Split('/');
        //    strfromDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
        //}

        //hdnsptype.Value = "InsertTempTable";

        //if (Convert.ToString(hdnClaimsID.Value).Trim() == "")
        //{
        //    hdnClaimsID.Value = "0";
        //}
        //if (Convert.ToString(hdnClaimsID.Value).Trim() != "" && Convert.ToString(hdnClaimsID.Value).Trim() != "0")
        //    hdnsptype.Value = "updateTempTable";

        ////CheckalreadyExistsRelation
        //DataTable Dt = spm.ChkRelationExist(txtEmpCode.Text, ddlRelationMed.SelectedValue.ToString().Trim());
        //if (Dt.Rows.Count > 0)
        //{
        //    lblmessage.Text = "Relation already exists.";
        //}
        //else
        //{

        //    //spm.Insert_Nominations_Mediclaim(Convert.ToInt32(hdnRemid.Value), strfromDate, txtEmpCode.Text, txtMember_Name.Text.ToString().Trim(), txtRelationMed.Text.ToString().Trim(), Med_sex, Convert.ToDecimal(txtAge_Med.Text), Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value));
        //    spm.Insert_Nominations_Mediclaim(Convert.ToInt32(hdnRemid.Value), strfromDate, txtEmpCode.Text, txtMember_Name.Text.ToString().Trim(), ddlRelationMed.SelectedValue.ToString().Trim(), Med_sex, Convert.ToDecimal(txtAge_Med.Text), Convert.ToString(hdnsptype.Value), Convert.ToInt32(hdnClaimsID.Value), Convert.ToString(ddlMaritalStatus.SelectedValue).Trim());
        //    //Response.Redirect("~/procs/Fuel_Req.aspx?clmid=" + hdnClaimsID.Value + "&rem_id=" + hdnremid.Value);
        //   get_NominationsDetails();
        //    txtBirthdatedate_Med.Text = "";
        //    txtAge_Med.Text = "";
        //    txtMember_Name.Text = "";
        //    //txtRelationMed.Text = "";
        //    ddlRelationMed.SelectedValue = "Select";
        //    // get_emp_Nominations_File();
        //    hdnClaimsID.Value = "0";
        //}

    }

    public void getSoftware_list()
    {


        DataTable dssoftwareList = new DataTable();       

        SqlParameter[] spars = new SqlParameter[2];

        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "getSoftware_list";

        spars[1] = new SqlParameter("@Soft_to_EmpCode", SqlDbType.VarChar);
        spars[1].Value = Convert.ToString(txtEmpCode.Text).Trim();

        dssoftwareList = spm.getDataList(spars, "SP_Admin_AssetInventory");


        DgvApprover.DataSource = null;
        DgvApprover.DataBind();
        if (dssoftwareList.Rows.Count > 0)
        {
            DgvApprover.DataSource = dssoftwareList;
            DgvApprover.DataBind();
        }

        //if (Convert.ToString(dssoftwareList.Tables[0].Rows[0]["Bal_Licence"]).Trim() == "0")
        //{
        //    lblmessage.Text = "The Software Name BalLicence is Zero.";
        //    return;
        //}
    }
    public void Fill_Soft_name()
    {
        DataTable dtPositionCriti = new DataTable();
        DataTable dtDetails = new DataTable();
        SqlParameter[] spars = new SqlParameter[1];
        spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
        spars[0].Value = "AssetSoft_names";
        

        dtPositionCriti = spm.getMobileRemDataList(spars, "SP_Admin_AssetInventory");
        if (dtPositionCriti.Rows.Count > 0)
        {


            lstsoft_name.DataSource = dtPositionCriti;
            lstsoft_name.DataTextField = "Soft_name";
            lstsoft_name.DataValueField = "Soft_Id";
            lstsoft_name.DataBind();
            lstsoft_name.ClearSelection();

            ListItem item = new ListItem("Select Software Name", "0");
            lstsoft_name.Items.Insert(0, item);
             
        }


        //DataTable dtDetails = new DataTable();
        //dtDetails = spm.FillDdlSoft_name();
        //if (dtDetails.Rows.Count > 0)
        //{

        //    lstsoft_name.DataSource = dtDetails;
        //    lstsoft_name.DataTextField = "Soft_name";
        //    lstsoft_name.DataValueField = "Soft_Id";
        //    lstsoft_name.DataBind();
        //    ListItem item = new ListItem("Select Software Name", "0");
        //    lstsoft_name.Items.Insert(0, item);
        //}
    }
    //public void Fill_Version_Name()
    //{ 
    //    DataTable dtDetails = new DataTable();
    //    dtDetails = spm.FillDdlVersion_Name();
    //    if (dtDetails.Rows.Count > 0)
    // {

    //    lstversion_name.DataSource = dtDetails;
    //    lstversion_name.DataTextField = "Version_Name";
    //    lstversion_name.DataValueField = "Version_Id";
    //    lstversion_name.DataBind();
    //    ListItem item = new ListItem("Select Version Name", "0");
    //    lstversion_name.Items.Insert(0, item);
    // }
    //}
    protected void gvproject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvproject.PageIndex = e.NewPageIndex;
        SearchAsset();
    }

    protected void gvAssetOption_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAssetOption.PageIndex = e.NewPageIndex;
        //if (dtSelectedOptions.Rows.Count > 0)
        //{
        //    gvAssetOption.DataSource = dtSelectedOptions;
        //    gvAssetOption.DataBind();
        //    lblAssetOptions.Visible = true;
        //}
    }



    ////////////////
    ///
    protected void btnBack_Click1(object sender, EventArgs e)
    {

        Label2.Text = "";
        string bal_licence = "";
        string Module = "";
        var ddlModule = "";
        var isSelected = false;

        
        foreach (ListItem item in lstsoft_name.Items)
        {
            if (item.Selected)
            {
                if (!string.IsNullOrEmpty(item.Value) && item.Value != "0")
                {

                    DataSet dsList1 = new DataSet();
                    SqlParameter[] spars1 = new SqlParameter[2];
                   // var soft_name = Convert.ToString(Session["Soft_name"]);
                    spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars1[0].Value = "Software_Master_ballicence_check";

                    spars1[1] = new SqlParameter("@soft_id", SqlDbType.Int);
                    spars1[1].Value = Convert.ToInt32(item.Value); // Convert item.Value to integer
 
                    //spars1[2] = new SqlParameter("@Bal_Licence", SqlDbType.VarChar);
                    //spars1[2].Value =bal_licence;

                    dsList1 = spm.getDatasetList(spars1, "SP_Admin_AssetInventory");

                    // Check if any rows are returned
                    if (dsList1.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToString(dsList1.Tables[0].Rows[0]["Bal_Licence"]).Trim() == "0")
                        {
                            Label2.Text = "The Software is not available.";
                            return;
                        }
                    }
                }
            }
        }



        foreach (ListItem item in lstsoft_name.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    DataSet dsList1 = new DataSet();
                    SqlParameter[] spars1 = new SqlParameter[4];


                    spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
                    spars1[0].Value = "insert_AssetSoft_list_tempTable";

                    spars1[1] = new SqlParameter("@soft_id", SqlDbType.Int);
                    spars1[1].Value =item.Value ;

                    
                    spars1[2] = new SqlParameter("@AssignedTo", SqlDbType.VarChar);
                    spars1[2].Value = Convert.ToString(txtEmpCode.Text).Trim();

                    foreach (GridViewRow assetRow in gvAssetOption.Rows)
                    {
                        spars1[2] = new SqlParameter("@Asset_number", SqlDbType.NVarChar);
                        spars1[2].Value = Convert.ToString(assetRow.Cells[0].Text).Trim();

                        dsList1 = spm.getDatasetList(spars1, "SP_Admin_AssetInventory");
                    }
                     
                    if (dsList1.Tables[0].Rows.Count > 0)
                        {
                        Label2.Text = "software Name is Already Exist.";
                            return;
                        }
                 //   lstsoft_name.ClearSelection();
                    // }

                }
            }
        }

        foreach (ListItem item in lstsoft_name.Items)
        {
            if (item.Selected)
            {
                if (item.Value != "" && item.Value != "0")
                {
                    isSelected = true;
                    if (ddlModule == "")
                    {
                        ddlModule = item.Value;
                        Module = item.Text;
                    }
                    else
                    {
                        ddlModule = ddlModule + "|" + item.Value;
                        Module = Module + ", " + item.Text;
                    }
                }
            }
        }



        DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];


            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "insert_AssetSoft_tempTableadd";

            spars[1] = new SqlParameter("@Module", SqlDbType.VarChar);
        spars[1].Value = ddlModule;
            
            //spars[2] = new SqlParameter("@AssetNumber", SqlDbType.VarChar);
            //spars[2].Value = Convert.ToString(ddl_AssetNumber.SelectedValue).Trim();

        spars[3] = new SqlParameter("@AssignedTo", SqlDbType.VarChar);
            spars[3].Value = Convert.ToString(txtEmpCode.Text).Trim();

        foreach (GridViewRow assetRow in gvAssetOption.Rows)
        {
            spars[2] = new SqlParameter("@Asset_number", SqlDbType.NVarChar);
            spars[2].Value = Convert.ToString(assetRow.Cells[0].Text).Trim();

            dsList = spm.getDatasetList(spars, "SP_Admin_AssetInventory");
        }
        //if (dsList.Tables[0].Rows.Count > 1)
        //{
        //    if (Convert.ToInt32(dsList.Tables[0].Rows[0]["soft_id"]) > 1)
        //    {
        //        lblmessage.Text = "The Software Name is Already Exists.";
        //        return;
        //    }
        //}

        lstsoft_name.ClearSelection();

        getSoftware_list();
         


        //var Description = lstsoft_name.Text.ToString();
        //var qtype = "InsertMainTable";
        //spm.InsertVersionName_SoftwareName(qtype, Convert.ToInt32(hdnId.Value), lstsoft_name.Text.ToString().Trim() );
        // Label2.Text = "You have add Software successfully ";

        //  Response.Redirect("~/procs/ITAssetService_Req.aspx?id=Empcode&type=app");
    }

    protected void lnkEdit_Click2(object sender, EventArgs e)
    {
        
    }
    protected void Del_Outstation(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string sSoftId= Convert.ToString(DgvApprover.DataKeys[row.RowIndex].Values[0]).Trim();
      
        if (Convert.ToString(sSoftId).Trim()!="")
        {
            DataTable dssoftwareList = new DataTable();

            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "delete_Soft_Assigned_employee_TempTable";

            spars[1] = new SqlParameter("@soft_id", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(sSoftId);

            foreach (GridViewRow assetRow in gvAssetOption.Rows)
            {
                spars[2] = new SqlParameter("@AssetNumber", SqlDbType.NVarChar);
                spars[2].Value = Convert.ToString(assetRow.Cells[0].Text).Trim();

                dssoftwareList = spm.getDataList(spars, "SP_Admin_AssetInventory");

            }

            

            getSoftware_list();
        }
        else
        {
            lblmessage.Text = "sSoftId";
        }
         

        
    }

    

}