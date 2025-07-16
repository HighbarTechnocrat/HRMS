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


public partial class ITAssetTermsAndConditions : System.Web.UI.Page
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
                    editform.Visible = true;
                    if (Request.QueryString.Count > 0)
                    {
                        hdnId.Value = Convert.ToString(Request.QueryString[0]);
                        Id = Convert.ToInt32(hdnId.Value);
                        GetDetails(Id);
                        //string type = Convert.ToString(Request.QueryString[1]);
                        //if (type == "ad")
                        //{
                        //    hmlink.Visible = false;
                        //}
                        hmlink.Visible = false;
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


    protected void mobile_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var qType = "";
            var isSubmitted = 0;
            // collect all variables
            ///collect data for Email
            var EmpName = txtEmpName.Text.ToString().Trim();
            var AssetAlloReqNo = "";// txtAssetAlloReqNo.Text.ToString().Trim();
            var EmpCode = txtEmpCode.Text.ToString().Trim();
            var Doj = hdnDoj.Value;
            var Location = hdnOfficeLocation.Value.ToString().Trim();
            var Dept = txtEmpDept.Text.ToString().Trim();
            var Designation = txtEmpDesig.Text.ToString().Trim();
            var RMgr = hdnRMgr.Value;
            var HOD = hdnHod.Value;
            var ReqDate = "";// txtReqDate.Text.ToString().Trim();
            var emailTo = hdnEmpEmail.Value;

            if (chkSelect.Checked != true)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Please Accept Terms & Conditions";
                chkSelect.Focus();
                return;
            }
            else
            {
                //update status submitted in table
                isSubmitted = '1'; qType = "UpdateIsSubmitted";
                spm.UpdateNewJoineeFormStatus(qType, isSubmitted, Convert.ToInt32(hdnId.Value));
                //Get ITAssetServiceReqData
                //DataSet ds= spm.ITAssetServiceReqData(Convert.ToInt32(hdnId.Value));
                lblmessage.Visible = true;
                lblmessage.Text = "Terms & Conditions Submitted sucessfully";
                Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            lblmessage.Visible = true;
            lblmessage.Text = ex.Message.ToString();
            return;
        }
    }

    public void GetDetails(int Id)
    {
        try
        {
            string EmpCode = "";
            DataTable dtDetails = new DataTable();
            dtDetails = spm.GetDetails(Id);
            if (dtDetails.Rows.Count > 0)
            {
                EmpCode = Convert.ToString(dtDetails.Rows[0]["EmpCode"]);
            }

            GetEmployeeDetails(EmpCode);
            DataTable dt = spm.GetSelectedAsset(Convert.ToString(dtDetails.Rows[0]["AARNo"]));
            if (dt.Rows.Count > 0)
            {
                txtAssetNo.Text = Convert.ToString(dt.Rows[0]["AssetNumber"]);
                txtSrNo.Text = Convert.ToString(dt.Rows[0]["SrNo"]);
                txtAssetType.Text = Convert.ToString(dt.Rows[0]["AssetType"]);
                txtAssetProperty.Text = Convert.ToString(dt.Rows[0]["AssetProperty"]);
                txtBrand.Text = Convert.ToString(dt.Rows[0]["BrandName"]);
                txtModel.Text = Convert.ToString(dt.Rows[0]["Model"]);
                //txtAssignedDate.Text = Convert.ToDateTime(dt.Rows[0]["UpdatedOn"]).ToString("dd/MM/yyyy");
                string Assigndt= Convert.ToDateTime(dt.Rows[0]["UpdatedOn"]).ToString("dd/MM/yyyy");
                txtAssignedDate.Text = Assigndt.Replace('-', '/');
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
                txtEmpDesig.Text = Convert.ToString(dtEmpDetails.Rows[0]["Designation"]);
                txtEmpDept.Text = Convert.ToString(dtEmpDetails.Rows[0]["Department"]);
                hdnOfficeLocation.Value = Convert.ToString(dtEmpDetails.Rows[0]["emp_location"]);
                hdnDoj.Value = Convert.ToString(dtEmpDetails.Rows[0]["doj"]);
                hdnRMgr.Value = Convert.ToString(dtEmpDetails.Rows[0]["RM"]);
                hdnHod.Value = Convert.ToString(dtEmpDetails.Rows[0]["HOD"]);
                hdnEmpEmail.Value = Convert.ToString(dtEmpDetails.Rows[0]["Emp_EmailAddress"]);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());

            throw;
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

            strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Trebuchet MS !important;font-style:Regular;width:100%;table-layout:fixed;'><tr><td>   ");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td>Dear Sir/Madam,</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td width = '25%'> For following Employee Asset Allocation Request has been approved by HOD.</td ></ tr>");
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
            strbuild.Append("</td></tr>");
            strbuild.Append("</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'><a href='" + redirectURL + "' target='_blank'> Click here to take action</td></tr>");
            strbuild.Append("<tr><td style='height:20px'></td></tr>");
            strbuild.Append("<tr><td style='height:20px'>This is a system generated mail, Please do not reply.</td></tr>");
            strbuild.Append("</table>");

            sendMail(emailTo, "", "", strsubject, Convert.ToString(strbuild).Trim(), "", "");
            //sendMail("", "", "", strsubject, Convert.ToString(strbuild).Trim(), strattchfilepath, "");
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

            if (emailTo.Contains(";"))
            {
                string[] strtoEmail = Convert.ToString(emailTo).Trim().Split(';');
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

            //mail.To.Add("Raj.patel@highbartech.com");
            //mail.To.Add("manisha.tambade@highbartech.com");
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
}