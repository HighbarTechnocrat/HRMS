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



public partial class TaskOwnerAction : System.Web.UI.Page
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

    SP_Methods spm = new SP_Methods();
    string strempcode = "";
    //protected void lnkhome_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect(ReturnUrl("sitepathmain") + "default");
    //}

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
                    string strfilepath = ConfigurationManager.AppSettings["StaticAlertpath"].Trim();
                    //strfilepath = @"D:\HRMS\hrmsadmin\files\StaticAlert\";
                    FilePath.Value = strfilepath;
                    editform.Visible = true;
                    if (Request.QueryString.Count > 0)
                      hdnSubTaskId.Value = Convert.ToString(Request.QueryString[0]);
                      int SubtaskId = Convert.ToInt32(Request.QueryString[0]);
                      var TaskOwnerId = CheckIsTaskOwnerLoggedUser(SubtaskId);
                    if (Convert.ToString(Session["Empcode"]) == TaskOwnerId)
                    {
                        GetTaskDetails(SubtaskId);
                        this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
                        txtActionDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    }
                    else
                    {
                        claimmob_btnSubmit.Enabled = false;
                        lnkviewfile.Visible = false;
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
    protected void CloseTaskNotification()
    {
        try
        {
            DataTable dsTrDetails = new DataTable();
            var actionDate = "";
            var ownerRemarks = "";
            var empcode = "";
            var taskId = hdntaskId.Value;
            var taskStatus = "Closed";
            var actionBy = Convert.ToString(hdnTaskOwnerId.Value);
            string filename = "";
            string strfileName = "";
            string fileName = "";
            string nextReminderDate =null;

            if (Convert.ToString(txtActionDate.Text).Trim() != "")
            {
                if (Convert.ToString(txtActionDate.Text).Trim() != "")
                {
                    var start = DateTime.ParseExact(txtActionDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    actionDate = start.ToString("yyyy-MM-dd");
                }
                else
                {
                    lblmessage.Text = "Please select Action date";
                }
            }
            if (txtOwnerRemark.Text.ToString().Trim() != "")
            {
                ownerRemarks = txtOwnerRemark.Text.ToString().Trim();
            }

            filename = uploadfile.FileName;
            if (Convert.ToString(filename).Trim() != "")
            {
                //string AdminPath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["StaticAlertpath"]).Trim());
                //string AdminPath = @"D:\HRMS\hrmsadmin\files\StaticAlert\";
                string AdminPath = ConfigurationManager.AppSettings["StaticAlertpath"].Trim();
                Boolean blnfile = false;
                strfileName = "";
                fileName = Path.GetFileName(uploadfile.FileName);
                String InputFile = System.IO.Path.GetExtension(uploadfile.FileName);
                //strfileName = Convert.ToString(Txt_Policyno.Text.Trim()) + "_Policy" + InputFile;
                //filename = strfileName;
                strfileName = InputFile;
                filename = strfileName;
                uploadfile.SaveAs(Path.Combine(AdminPath, fileName));
                blnfile = true;

            }
            spm.CloseTaskNotification(Convert.ToInt32(hdnSubTaskId.Value), taskId, Convert.ToString(hdnEmpCode.Value), nextReminderDate, actionDate, ownerRemarks, fileName, taskStatus, actionBy);

            var taskDescription = txtDescription.Text.ToString().Trim();
            var taskOwner = txtTaskOwner.Text.ToString().Trim();
            var taskDueDate = txtDueDate.Text.ToString().Trim();
            var ownerRemark = txtOwnerRemark.Text.ToString().Trim();
            var emailTo = "";
            var emailCc1 = "";
            var emailCc2 = "";

            DataSet dsEmailIds = spm.GetEmailIds(hdnTaskOwnerId.Value, hdnTaskSupervisor1Id.Value, hdnTaskSupervisor2Id.Value);

            if (dsEmailIds != null && dsEmailIds.Tables.Count > 0)
            {
                if (dsEmailIds.Tables[0] != null && dsEmailIds.Tables[0].Rows.Count > 0)
                {
                    emailTo = Convert.ToString(dsEmailIds.Tables[0].Rows[0]["Emp_Emailaddress"]);
                }
                if (dsEmailIds.Tables[1] != null && dsEmailIds.Tables[1].Rows.Count > 0)
                {
                    emailCc1 = Convert.ToString(dsEmailIds.Tables[1].Rows[0]["Emp_Emailaddress"]);
                }
                if (dsEmailIds.Tables[2] != null && dsEmailIds.Tables[2].Rows.Count > 0)
                {
                    emailCc2 = Convert.ToString(dsEmailIds.Tables[2].Rows[0]["Emp_Emailaddress"]);
                }
            }

            SendNotificationCloseMail(taskDescription, taskOwner, taskDueDate, ownerRemark, actionDate, ownerRemarks,emailTo,emailCc1,emailCc2, fileName);
            Response.Redirect(ReturnUrl("sitepathmain") + "default");

        }
        catch (Exception)
        {
            throw;
        }

    }

    

    protected void SendNotificationCloseMail(string TaskDescription, string TaskOwner, string TaskDueDate, string TaskRemarks, string ActionDate, string ActionRemarks, string emailTo,string emailCc1, string emailCc2,string AttachmentFileName)
    {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            StringBuilder strbuild = new StringBuilder();
            DataTable dt_emp = new DataTable();
            string[] TaskDt = TaskDueDate.Split(' ');


            var strsubject = "OneHR - Closer Of Task "+ TaskDescription;
            try
            {
                strbuild = new StringBuilder();

                strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
                strbuild.Append("</td></tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>Dear Sir,</td></tr>");
                strbuild.Append("</td></tr>");
                strbuild.Append("</td></tr>");
                strbuild.Append("<tr><td width = '25%'> Following task is closed by " + TaskOwner + "</td ></ tr>");
                strbuild.Append("<tr><td style='height:20px'></td></tr>");
                strbuild.Append("<tr><td>");
                strbuild.Append("<table style='width:80%;color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;table-layout:fixed;'>");
                strbuild.Append("<tr><td width='25%'>Task Description : </td><td >" + TaskDescription + "</td></tr>");
                strbuild.Append("<tr><td width='25%'>Task Date : </td><td >" + TaskDt[0].ToString() + "</td></tr>");
                strbuild.Append("<tr><td width='25%'>Task Owner : </td><td >" + TaskOwner + "</td></tr>");
                strbuild.Append("<tr><td width='25%'>Task Remarks : </td><td width='75%'>" + TaskRemarks + "</td></tr>");
                strbuild.Append("<tr><td width='25%'>Action Date : </td><td width='75%'> " + ActionDate + "</td></tr>");
                strbuild.Append("<tr><td width='25%'>Action Remark :</td><td width='75%'>" + ActionRemarks + "</td></tr>");
                strbuild.Append("</table>");
                strbuild.Append("</td></tr>");
                strbuild.Append("</td></tr>");
                
                strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
                strbuild.Append("</table>");

            sendMail(emailTo, emailCc1,emailCc1, strsubject, Convert.ToString(strbuild).Trim(), "", "");

            }
        catch (Exception)
        {

            throw;
        }
    }


    public void sendMail(string emailTo, string emailCc1,string emailCc2,string strsubject, string strbody, string strattchfilepath, string ccMailIDs)
    {
        try
        {
            MailMessage mail = new MailMessage();
            //string[] strtoEmail = Convert.ToString(toMailIDs).Trim().Split(';');
            //string[] strCCEmail = Convert.ToString(ccMailIDs).Trim().Split(';');
            //for (int i = 0; i < strtoEmail.Length; i++)
            //{
            //    //if (Convert.ToString(strtoEmail[i]).Trim() != "")
            //    //    mail.To.Add(strtoEmail[i]);
            //}
            mail.To.Add(emailTo);
            if(emailCc1!="")
            { mail.CC.Add(emailCc1); }

            if (emailCc2 != "")
            { mail.CC.Add(emailCc2); }


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
            //mail.Dispose();

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.Host = "smtp.office365.com"; //Highbar SMTP

                smtp.Port = 587;
                smtp.TargetName = "STARTTLS/smtp.office365.com";
                //smtp.Host = "smtp-mail.outlook.com"; //Highbar SMTP
                //smtp.Port = 25;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential("noreply@highbartech.com", "HBT@2019");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = SMTPUserInfo;
                smtp.EnableSsl = true;
                //var securityProtocol = (int)System.Net.ServicePointManager.SecurityProtocol;

                //// 0 = SystemDefault in .NET 4.7+
                //if (securityProtocol != 0)
                //{
                //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                //}
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

    private void GetTaskDetails(int SubTaskId)
    {
        var dttaskdetails = new DataTable();

        dttaskdetails = spm.GetTaskHistorydtlsBySubTakId(SubTaskId);

        if (dttaskdetails != null && dttaskdetails.Rows.Count > 0)
        {
            string TaskId = Convert.ToString(dttaskdetails.Rows[0]["TaskId"]);
            string TaskStatus = Convert.ToString(dttaskdetails.Rows[0]["TaskStatus"]);
            string ActionDate = Convert.ToString(dttaskdetails.Rows[0]["ClosingDate"]);
            string OwnerRemarks = Convert.ToString(dttaskdetails.Rows[0]["OwnerRemarks"]);
            if (TaskStatus.ToString() == "Closed")
            {
                lblmessage.Text = "This Task is already closed!";
                claimmob_btnSubmit.Visible = false;
                txtActionDate.Enabled = false;
                txtOwnerRemark.Enabled = false;
            }
            else
            {
                lblmessage.Text = "";
                claimmob_btnSubmit.Visible = true;
            }
            dttaskdetails = spm.GetTaskDetails(TaskId);
            if (dttaskdetails.Rows.Count > 0)
            {
                txtDescription.Text = Convert.ToString(dttaskdetails.Rows[0]["TaskDescription"]);
                txtDueDate.Text = Convert.ToString(dttaskdetails.Rows[0]["DueDate"]);
                txtTaskOwner.Text = Convert.ToString(dttaskdetails.Rows[0]["Owner"]);
                txtSupervisor1.Text = Convert.ToString(dttaskdetails.Rows[0]["Supervisor1"]);
                txtSupervisor2.Text = Convert.ToString(dttaskdetails.Rows[0]["Supervisor2"]);
                txtRemarks.Text = Convert.ToString(dttaskdetails.Rows[0]["Remarks"]);
                txtOwnerRemark.Text = OwnerRemarks.ToString();
                txtActionDate.Text = ActionDate.ToString().Replace("00:00:00","");
                hdntaskId.Value = Convert.ToString(dttaskdetails.Rows[0]["TaskId"]);
                hdnTaskOwnerId.Value = Convert.ToString(dttaskdetails.Rows[0]["TaskOwner"]);
                hdnTaskSupervisor1Id.Value = Convert.ToString(dttaskdetails.Rows[0]["TaskSupervisor1"]);
                hdnTaskSupervisor2Id.Value = Convert.ToString(dttaskdetails.Rows[0]["TaskSupervisor2"]);
                if (Convert.ToString(dttaskdetails.Rows[0]["FileName"]) != "")
                {
                    lnkviewfile.Visible = true;
                    hdn_Attchment.Value = Convert.ToString(dttaskdetails.Rows[0]["FileName"]);
                    lnkviewfile.Text = hdn_Attchment.Value;
                }
            }
        }
    }

    #endregion


    protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(txtActionDate.Text).Trim() != "")
        {
            CloseTaskNotification();
        }
        else
        {
            lblmessage.Text = "Please Enter Action Date";
            //lblmessage.Visible = true;
        }
    }
}