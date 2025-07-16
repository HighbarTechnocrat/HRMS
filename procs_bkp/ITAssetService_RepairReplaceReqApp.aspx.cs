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



public partial class ITAssetService_RepairReplaceReqApp : System.Web.UI.Page
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
                    dtNewAssetOptions = new DataTable();
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        hdnRemid_Type.Value = Convert.ToString(Request.QueryString[1]).Trim();
                        var getType = Convert.ToString(Request.QueryString[1]).Trim();
                        Id = Convert.ToInt32(Request.QueryString[0]);
                        GetDetails(Id);
                        if (getType == "arr")
                        {
                            mobile_btnSave.Visible = false;
                            mobile_cancel.Visible = false;
                            mobile_btnBack.Visible = false;
                            txtRemarks.Enabled = false;
                            uploadfile.Enabled = false;
                            backToArr.Visible = true;
                            foreach (GridViewRow i in gvNewAssetOptions.Rows)
                            {
                                RadioButton Rb = (RadioButton)i.FindControl("rbSelect");
                                Rb.Enabled = false;
                            }

                            gvAppFiles.Visible = true;
                            gvAppViewFiles.Visible = true;
                            Li1.Visible = true;
                            li2.Visible = false;
                        }
                        if (getType == "emp")
                        {
                            mobile_btnSave.Visible = false;
                            mobile_cancel.Visible = false;
                            mobile_btnBack.Visible = false;
                            txtRemarks.Enabled = false;
                            uploadfile.Enabled = false;
                            backToEmployee.Visible = true;

                        }
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

    protected void SendNotificationMail(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName,string redirectURL)
    {

        MailMessage mail = new MailMessage();
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
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> Following Employee has been onboarded in OneHR, and IT team has provided options to allocate Asset.</td ></ tr>");
            strbuild.Append("<tr><td width = '25%'> Please register you action..</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='25%'>Asset Allocation Request : </td><td >" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Employee Code : </td><td >" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Employee Name : </td><td >" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Onboarding Date : </td><td width='75%'>" + Doj + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Location : </td><td width='75%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Department :</td><td width='75%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Designation :</td><td width='75%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>Reporting Manager :</td><td width='75%'>" + RMgr + "</td></tr>");
            strbuild.Append("<tr><td width='25%'>HOD :</td><td width='75%'>" + HOD + "</td></tr>");
            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            sendMail("", "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "","");

        }
        catch (Exception)
        {

            throw;
        }
    }


    public void sendMail(string emailTo, string emailCc1, string emailCc2, string strsubject, string strbody, string strattchfilepath, string ccMailIDs,string EmailToEmp)
    {
        try
        {
            MailMessage mail = new MailMessage();

            //Prod
            string[] strtoEmail = Convert.ToString(emailTo).Trim().Split(';');

            if (strtoEmail.Length > 0)
            {
                for (int i = 0; i < strtoEmail.Length; i++)
                {
                    if (Convert.ToString(strtoEmail[i]).Trim() != "")
                        mail.To.Add(strtoEmail[i]);
                }
            }
            else
            {
                mail.To.Add(emailTo);
            }

            if (emailCc1 != "")
            { mail.CC.Add(emailCc1); }

            if (emailCc2 != "")
            { mail.CC.Add(emailCc2); }

            if (EmailToEmp != "")
            {
                mail.CC.Add(EmailToEmp);
            }


            //Dev
            //emailTo = "manisha.tambade@highbartech.com";
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
            string AppRemarks = "";
            int isAssigned = 1;
            ///collect data for Email
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
            var emailTo = txtEmail.Text.ToString().Trim();
            var Remarks = txtCustRemarks.Text.ToString().Trim();
            var RequestType = ddlReqType.SelectedItem.Text.ToString().Trim();
            string EmailToEmp = txtEmail.Text.ToString().Trim();

        try
                {
                    if (Convert.ToString(txtRemarks.Text).Trim() != "")
                    {
                        AppRemarks = txtRemarks.Text.ToString().Trim();
                    }
                if (RequestType == "Replacement")
                {
                    foreach (GridViewRow row in gvNewAssetOptions.Rows)
                    {
                        RadioButton rb = (RadioButton)row.FindControl("rbSelect");
                        if (rb.Checked != true)
                        {
                            lblmessage.Visible = true;
                            lblmessage.Text = "Please select new asset option to assign";
                            return;
                        }
                    }
                }
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

            string AssetNumberID = ""; string AssetNumber = "";
            string AssigneTo = txtEmpCode.Text.ToString().Trim();
            string ITHODName = "";
            
            //GetITHOD
            DataTable dtHOD = spm.GetITHod();
            if (dtHOD.Rows.Count > 0)
            {
                //AssigneTo = Convert.ToString(dtHOD.Rows[0]["HOD"]);
                ITHODName = Convert.ToString(dtHOD.Rows[0]["ITHODName"]);
            }

            if (RequestType == "Replacement")
            {
                foreach (GridViewRow row in gvNewAssetOptions.Rows)
                {
                    //Find the Radio button control
                    RadioButton rb = (RadioButton)row.FindControl("rbSelect");
                    if (rb.Checked == true)
                    {
                        AssetNumberID = row.Cells[0].Text;
                        AssetNumber = row.Cells[1].Text;
                    }
                }
                spm.UpdateAutoCheckInData(AssigneTo, AssetNumberID);
                spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 4, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
                //update AssetoptionList to Selected AssetNumbar
                spm.UpdateAssetOptionList(AssetAlloReqNo, AssetNumber, "1");

                //Get All Custodians-
                string to_email = ""; 
                DataTable t2 = spm.GetAllCustodianDetails();
                if (t2.Rows.Count > 0)
                {
                    to_email = "";

                    foreach (DataRow row in t2.Rows)
                    {
                        to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                    }
                }

                //Get HODEmailid GetHODemailId
                DataTable dtHod = spm.GetHODemailId();
                string HodEmailcc = "";
                if (dtHod.Rows.Count > 0)
                {
                    HodEmailcc = Convert.ToString(dtHod.Rows[0]["HodEmailId"]);
                }

                //Get RM and PRM of employee
                string RM = "";
                string PRM = "";
                DataTable t = spm.GetEmpDataByEmpCode(txtEmpCode.Text.ToString().Trim());
                if (t.Rows.Count > 0)
                {
                    RM = Convert.ToString(t.Rows[0]["RMEmail"]);
                    PRM = Convert.ToString(t.Rows[0]["PRMEmail"]);
                }
                //Get HR HOD Emailid
                string HR = "";
                DataTable t1 = spm.GetHrHOD();
                if (t1.Rows.Count > 0)
                {
                    HR = Convert.ToString(t1.Rows[0]["HREmail"]);
                }

                //send asset allocation mail to emp/custodian,PN,RM,HOD
                DataTable dtData = spm.GetCheckInDataById(Convert.ToInt32(AssetNumberID));
                if (dtData != null)
                {
                    string AssetType = Convert.ToString(dtData.Rows[0]["AssetType"]);
                    string AssetDesc = Convert.ToString(dtData.Rows[0]["AssetDesc"]);
                    string SrNo = Convert.ToString(dtData.Rows[0]["SrNo"]);
                    string Manufacturer = Convert.ToString(dtData.Rows[0]["BrandName"]);
                    string Model = Convert.ToString(dtData.Rows[0]["Model"]);
                    string EmailTo = Convert.ToString(dtData.Rows[0]["Emp_Emailaddress"]);
                    //SendAssetAssignmentMail(AssetType, AssetDesc, SrNo, Manufacturer, Model, EmailTo, HodEmailcc, cc_email, RM, PRM, HR);
                    //send Asset Assignment mail to Newjoinee
                    string ITAssetService_Req_App = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetTermsAndConditions.aspx";
                    string redirectURL = Convert.ToString(ITAssetService_Req_App).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=app";
                    SendAssetAlocationMailToNewJoinee(EmpName, AssetType, AssetDesc, SrNo, Manufacturer, Model, emailTo, redirectURL);
                }
                SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "ReplaceApprove", "", EmailToEmp, ITHODName, AppRemarks, Remarks);
                lblmessage.Text = "IT Asset Service Request Approved Successfully";
                Response.Redirect("~/procs/ITAssetService.aspx");
            }

            if(RequestType=="Repair/Upgrade")
            {
                spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 5, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));

                //Get All Custodians-
                string to_email = ""; 
                DataTable t2 = spm.GetAllCustodianDetails();
                if (t2.Rows.Count > 0)
                {
                    to_email = "";
                    foreach (DataRow row in t2.Rows)
                    {
                        to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                    }
                }
                SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "RepairApprove", "", EmailToEmp, ITHODName,AppRemarks, Remarks);
                lblmessage.Text = "IT Asset Service Request Approved Successfully";
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

    protected void mobile_cancel_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string AppRemarks = "";
        int isAssigned = 1;
        ///collect data for Email
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
        var emailTo = txtEmail.Text.ToString().Trim();
        var Remarks = txtCustRemarks.Text.ToString().Trim();
        var RequestType = ddlReqType.SelectedItem.Text.ToString().Trim();

        try
        {
            if (Convert.ToString(txtRemarks.Text).Trim() != "")
            {
                AppRemarks = txtRemarks.Text.ToString().Trim();
            }
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

            string AssigneTo = txtEmpCode.Text.ToString().Trim();
            spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 5, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
            //Get All Custodians-
            string to_email = "";
            DataTable t2 = spm.GetAllCustodianDetails();
            if (t2.Rows.Count > 0)
            {
                to_email = "";
                foreach (DataRow row in t2.Rows)
                {
                    to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                }
            }
            if(RequestType=="Repair/Upgrade")
            {
                SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "RepairReject", "","","",AppRemarks, Remarks);
            }
            if (RequestType == "Replacement")
            {
                SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "ReplaceReject", "","","",AppRemarks, Remarks);
            }
                
            lblmessage.Text = "IT Asset Service Request Rejected Successfully";
            Response.Redirect("~/procs/ITAssetService.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string[] strdate;
        string strfromDate = "";
        string filename = "";
        String strfileName = "";
        string AppRemarks = "";
        int isAssigned = 1;
        ///collect data for Email
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
        var emailTo = txtEmail.Text.ToString().Trim();
        var Remarks = txtCustRemarks.Text.ToString().Trim();
        var RequestType = ddlReqType.SelectedItem.Text.ToString().Trim();

        try
        {
            if (Convert.ToString(txtRemarks.Text).Trim() != "")
            {
                AppRemarks = txtRemarks.Text.ToString().Trim();
            }
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
            string AssigneTo = hdnCustodianCode.Value;
            spm.InsertITAssetServiceRequestDetails(Convert.ToString(Session["Empcode"]).Trim(), Remarks, AppRemarks, "UpdateWF", AssigneTo, isAssigned, 3, 1, Convert.ToString(Session["Empcode"]).Trim(), Convert.ToInt32(hdnId.Value));
            //Get All Custodians-
            string to_email = "";
            DataTable t2 = spm.GetAllCustodianDetails();
            if (t2.Rows.Count > 0)
            {
                to_email = "";

                foreach (DataRow row in t2.Rows)
                {
                    to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
                }
            }

            string ITAssetService_Req = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetService_RepairReplaceReq.aspx";
            //string ITAssetService_Req = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetService_RepairReplaceReq.aspx";
            string redirectURL = Convert.ToString(ITAssetService_Req).Trim() + "?id=" + Convert.ToInt32(hdnId.Value) + "&type=emp";

            if (RequestType == "Repair/Upgrade")
            {
                SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "RepairSentBack", redirectURL,"","",AppRemarks, Remarks);
            }
            if (RequestType == "Replacement")
            {
                SendNotificationMailToCustodians(EmpName, AssetAlloReqNo, EmpCode, Doj, Location, Dept, Designation, RMgr, HOD, "", to_email, "ReplaceSentBack", redirectURL,"","",AppRemarks, Remarks);
            }
            lblmessage.Visible = true;
            lblmessage.Text = "IT Asset Service Request Sent Back Successfully";
            Response.Redirect("~/procs/ITAssetService.aspx");
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            //lblmessage.Text = ex.Message.ToString();
            Response.Redirect("~/procs/ITAssetService.aspx");
            return;
        }
    }


    protected void SendNotificationMailToNewJoinee(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName, string redirectURL, string emailTo)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();

        var strsubject = "New Joinee Asset Allocation Request Approval- " + EmpName;

        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> For following Employee Asset Allocation Request has been approved by HOD.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='30%'>Asset Allocation Request : </td><td width='70%'>" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Employee Code : </td><td width='70%'>" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Employee Name : </td><td width='70%'>" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Onboarding Date : </td><td width='70%'>" + Doj + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Location : </td><td width='70%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Department :</td><td width='70%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Designation :</td><td width='70%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>Reporting Manager :</td><td width='70%'>" + RMgr + "</td></tr>");
            strbuild.Append("<tr><td width='30%'>HOD :</td><td width='70%'>" + HOD + "</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");
            
            sendMail(emailTo, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "","");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void SendNotificationMailToCustodians(string EmpName, string AssetAlloReqNo, string EmpCode, string Doj, string Location, string Dept, string Designation, string RMgr, string HOD, string AttachmentFileName, string to_email, string ReqType, string redirectURL,string EmailToEmp,string ITHODName,string AppRemarks,string CustRemarks)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();
        var strsubject = "";
        ITHODName = ITHODName + ".";

        if (ReqType == "RepairApprove")
        {
            strsubject = "Asset Repair/Upgrade Request Approval- " + EmpName;
        }
        if (ReqType == "RepairReject")
        {
            strsubject = "Asset Repair/Upgrade Request Rejection- " + EmpName;
        }
        if (ReqType == "RepairSentBack")
        {
            strsubject = "Asset Repair/Upgrade Request Sent Back- " + EmpName;
        }
        if (ReqType == "ReplaceApprove")
        {
            strsubject = "Asset Replacement Request Approval- " + EmpName;
        }
        if (ReqType == "ReplaceReject")
        {
            strsubject = "Asset Replacement Request Rejection- " + EmpName;
        }
        if (ReqType == "ReplaceSentBack")
        {
            strsubject = "Asset Replacement Request Sent Back- " + EmpName;
        }

        try
        {
            strbuild = new StringBuilder();

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("<tr><td style='height:10px'></td></tr>");
            strbuild.Append("</td></tr>");

            if (ReqType == "RepairApprove")
            {
                strbuild.Append("<tr><td width = '30%'>For Following Employee Asset Repair/Upgrade Request has been approved by Technology & Innovation Department HOD- " + ITHODName + "</td ></ tr>");
            }
            if (ReqType == "RepairReject")
            {
                strbuild.Append("<tr><td width = '30%'>For Following Employee Asset Repair/Upgrade Request has been rejected by HOD.</ td ></ tr>");
            }
            if (ReqType == "RepairSentBack")
            {
                strbuild.Append("<tr><td width = '30%'>For Following Employee Asset Repair/Upgrade Request has been Sent Back For Correction by HOD.</td ></ tr>");
            }
            if (ReqType == "ReplaceApprove")
            {
                strbuild.Append("<tr><td width = '30%'>For Following Employee Asset Replacement Request has been approved by Technology & Innovation Department HOD- " + ITHODName + "</td ></ tr>");
            }
            if (ReqType == "ReplaceReject")
            {
                strbuild.Append("<tr><td width = '30%'>For Following Employee Asset Replacement Request has been rejected by HOD.</ td ></ tr>");
            }
            if (ReqType == "ReplaceSentBack")
            {
                strbuild.Append("<tr><td width = '30%'>For Following Employee Asset Replacement Request has been Sent Back For Correction by HOD.</td ></ tr>");
            }
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='50%'>Asset Allocation Request : </td><td width='50%'>" + AssetAlloReqNo + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Employee Code : </td><td width='50%'>" + EmpCode + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Employee Name : </td><td width='50%'>" + EmpName + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Location : </td><td width='50%'> " + Location + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Department :</td><td width='50%'>" + Dept + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Designation :</td><td width='50%'>" + Designation + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Reporting Manager :</td><td width='50%'>" + RMgr + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>HOD :</td><td width='50%'>" + HOD + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Remarks (IT Team) :</td><td width='50%'>" + CustRemarks + "</td></tr>");
            strbuild.Append("<tr><td width='50%'>Remarks (Head – Technology & Innovation) :</td><td width='50%'>" + AppRemarks + "</td></tr>");
            
            

            strbuild.Append("</table>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");

            if ((ReqType == "RepairSentBack")|| (ReqType == "ReplaceSentBack"))
            {
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
            }
            strbuild.Append("<tr><td style='height:40px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            sendMail(to_email, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "", EmailToEmp);
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void SendAssetAlocationMailToNewJoinee(string EmpName, string AssetType, string AssetDesc, string SrNo, string Manufacturer, string Model, string emailTo, string redirectURL)
    {

        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient();
        StringBuilder strbuild = new StringBuilder();

        var strsubject = "New Asset Allocated – Accept Terms And Conditions.";

        try
        {
            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td width='20%'>Following IT Asset has been allocated to you, please accept terms and condition for the same.</td ></ tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td>");
            strbuild.Append("<table style='width:100%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
            strbuild.Append("<tr><td width='20%'>Asset Type: </td><td width='70%'>" + AssetType + "</td></tr>");
            strbuild.Append("<tr><td width='20%'>Asset Description: </td><td width='70%'>" + AssetDesc + "</td></tr>");
            strbuild.Append("<tr><td width='20%'>Sr.No: </td><td width='70%'>" + SrNo + "</td></tr>");
            strbuild.Append("<tr><td width='20%'>Manufacturer/Brand: </td><td width='70%'>" + Manufacturer + "</td></tr>");
            strbuild.Append("<tr><td width='20%'>Model: </td><td width='70%'> " + Model + "</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td width='60%'><a href='" + redirectURL + "' target='_blank'>Click here to view and accept Terms & Conditions</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td></td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width='60%'>Note: -After 5 working days, terms and condition will be deemed accepted.</td></tr>");
            strbuild.Append("<tr><td style='height:10px'></td></tr>");
            strbuild.Append("<tr><td width='60%'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            sendMail(emailTo, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "","");
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected void ddlReqType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlReqType.SelectedItem.Text == "Repair/Upgrade")
        {
            lblAssetOps.Visible = false;
            liOldAssetOptions.Visible = false;
            lblNewAssetOptions.Visible = false;
            lblAssetOptions.Visible = true;
        }
        else
        {
            ddlReqType.SelectedItem.Text = "Replacement";
            lblAssetOps.Visible = true;
            liOldAssetOptions.Visible = true;
            lblNewAssetOptions.Visible = true;
            lblAssetOptions.Visible = false;
        }
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

    public void GetDetails(int Id)
    {
        try
        {
            string EmpCode = "";
            string CustodianCode = "";
            int ReqStatus;

            DataTable dtDetails = new DataTable();
            dtDetails = spm.GetDetails(Id);
            if (dtDetails.Rows.Count > 0)
            {
                EmpCode = Convert.ToString(dtDetails.Rows[0]["EmpCode"]);
                txtAssetAlloReqNo.Text = Convert.ToString(dtDetails.Rows[0]["AARNo"]);
                txtRemarks.Text= Convert.ToString(dtDetails.Rows[0]["AppRemarks"]);
                txtCustRemarks.Text= Convert.ToString(dtDetails.Rows[0]["Remarks"]);
                txtReqDate.Text= Convert.ToString(dtDetails.Rows[0]["nCreatedDate"]);
                CustodianCode = Convert.ToString(dtDetails.Rows[0]["CustodianCode"]);
                hdnCustodianCode.Value = CustodianCode;
                ReqStatus = Convert.ToInt32(dtDetails.Rows[0]["Status"]);
               // ddl_Employee.SelectedValue = EmpCode;
                ddlReqType.SelectedItem.Text= Convert.ToString(dtDetails.Rows[0]["RequestType"]);
                GetEmployeeDetails(EmpCode);
                GetFilesDetails(txtAssetAlloReqNo.Text.ToString().Trim(), EmpCode, CustodianCode);
                if (ddlReqType.SelectedItem.Text == "Repair/Upgrade")
                {
                    lblAssetOptions.Visible = true;
                    gvAssetOption.Visible = true;
                    GetOptionList(txtAssetAlloReqNo.Text.ToString().Trim());

                    //hide radio button in repair case
                    gvAssetOption.HeaderRow.Cells[10].CssClass = "hiddencol";
                    foreach (GridViewRow row in gvAssetOption.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                            row.Cells[10].CssClass = "hiddencol";
                    }
                }
                if (ddlReqType.SelectedItem.Text == "Replacement")
                {
                    lblAssetOptions.Visible = false;
                    gvAssetOption.Visible = true;
                    lblAssetOps.Visible = true;
                    liOldAssetOptions.Visible = true;
                    lblNewAssetOptions.Visible = true;
                    gvNewAssetOptions.Visible = true;
                    GetOldOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
                    GetNewOptionList(txtAssetAlloReqNo.Text.ToString().Trim());
                    gvNewAssetOptions.Visible = true;
                    gvAssetOption.Visible = true;

                    //hide radio button in repair case
                    gvAssetOption.HeaderRow.Cells[10].CssClass = "hiddencol";
                    foreach (GridViewRow row in gvAssetOption.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                            row.Cells[10].CssClass = "hiddencol";
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
        gvAssetOption.Visible = true;
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
}