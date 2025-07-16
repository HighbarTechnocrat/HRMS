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
using System.Linq;

public partial class procs_App_Latter_Approval : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public string userid;
	public int did = 0;
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	#endregion
	SP_Methods spm = new SP_Methods();
	DataSet dspaymentVoucher_Apprs = new DataSet();
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			//mobile_btnPrintPV.Visible = false;

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
			}
			else
			{

				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					editform.Visible = true;
					divmsg.Visible = false;
					hdnempcode.Value = Convert.ToString(Session["Empcode"]);
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["APPLatterAccPath"]).Trim());
					hdnAdminFile.Value = @"D:\HRMS\hrmsadmin\CandidateDocument\";
					//txt_FD_DOB.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					if (Request.QueryString.Count > 0)
					{
						hdnAppointment_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnType.Value = Convert.ToString(Request.QueryString[1]).Trim();
						Get_EmployeeDetails();
						if (hdnType.Value == "Pending")//
						{
							GetApp_CurrentApprID();
							txtApprovalDate.Text = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");
							btnRecBack.HRef = "App_Latter_M_Index.aspx?Type=Pending";
							//	App_Latter_M_Index.aspx?itype=Pending
						}
						else
						{//APP
							btnRecBack.HRef = "App_Latter_M_Index.aspx?Type=APP";
							txtRemark.Enabled = false;
							lnk_Correction.Visible = false;
							lnk_Update_Profile.Visible = false;
						}
						
					}
					else
					{
						
					}

					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
				else
				{

				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	public void Get_EmployeeDetails()
	{
		DataTable dtEmployee = new DataTable();
		//hdnempcode.Value
		dtEmployee = spm.Get_APP_Approval_Details("APP_Approval_Pending", "SP_APP_Employee_Details", hdnempcode.Value, Convert.ToInt32(hdnAppointment_ID.Value));
		if (dtEmployee.Rows.Count > 0)
		{
			txt_EmailAddress.Text = Convert.ToString(dtEmployee.Rows[0]["Emp_Emailaddress"]).Trim();
			txtEmpName.Text = Convert.ToString(dtEmployee.Rows[0]["Emp_Name"]).Trim();
			txt_DOJ.Text = Convert.ToString(dtEmployee.Rows[0]["emp_doj"]).Trim();
			Txt_Band.Text = Convert.ToString(dtEmployee.Rows[0]["grade"]).Trim();
			Txt_Department.Text = Convert.ToString(dtEmployee.Rows[0]["Department_Name"]).Trim();
			txt_Designation.Text = Convert.ToString(dtEmployee.Rows[0]["DesginationName"]).Trim();
			//txt_App_Issued_Date.Text = Convert.ToString(dtEmployee.Rows[0]["APP_Issued_Date"]).Trim();
			txt_App_Issued_Date.Text = Convert.ToString(dtEmployee.Rows[0]["CreatedOn"]).Trim();
			lnkuplodedfile.Text = Convert.ToString(dtEmployee.Rows[0]["Appointment_File"]).Trim();
			txtEmpCode.Text = Convert.ToString(dtEmployee.Rows[0]["Emp_Code"]).Trim();
			Txt_ProjectCode.Text = Convert.ToString(dtEmployee.Rows[0]["PROJ_NAME_LONG"]).Trim();
			ltTable.Text = Convert.ToString(dtEmployee.Rows[0]["DraftName"]).Trim();
			hdnAppointment_ID.Value = Convert.ToString(dtEmployee.Rows[0]["Appointment_ID"]).Trim();
			txtAcceptanceDate.Text = Convert.ToString(dtEmployee.Rows[0]["Acceptance_Date"]).Trim();
			hdnCreateByEmail.Value = Convert.ToString(dtEmployee.Rows[0]["CreatedByEmail"]).Trim();
			if (Convert.ToInt32(dtEmployee.Rows[0]["Status_ID"]) == 3 || Convert.ToInt32(dtEmployee.Rows[0]["Status_ID"]) == 4)
			{
				txtApprovalDate.Text = Convert.ToString(dtEmployee.Rows[0]["Approval_Date"]).Trim();
				txtRemark.Text = Convert.ToString(dtEmployee.Rows[0]["Remark"]).Trim();
			}
				lnkSinedFile.Text = Convert.ToString(dtEmployee.Rows[0]["Signed_File"]).Trim();
				chk_Read_Acceptance.Checked = Convert.ToBoolean(dtEmployee.Rows[0]["Read_Acceptance"]);
				//txtModerationName.Text = Convert.ToString(dtEmployee.Rows[0]["ModeName"]).Trim();
				txtStatus.Text = Convert.ToString(dtEmployee.Rows[0]["Status_Title"]).Trim();
			
		}
	}
	public void Get_Moderation_Employee()
	{
		DataTable dtEmployee = new DataTable();
		dtEmployee = spm.Get_APP_Employee_Details("Moderation_Employee", "SP_APP_Employee_Details", hdnempcode.Value);
		if (dtEmployee.Rows.Count > 0)
		{
			hdnModerationEmail.Value = Convert.ToString(dtEmployee.Rows[0]["Emp_Emailaddress"]).Trim();
			hdnModerationName.Value = Convert.ToString(dtEmployee.Rows[0]["Emp_Name"]).Trim();
			hdnModerationCode.Value = Convert.ToString(dtEmployee.Rows[0]["Emp_Code"]).Trim();
		}
	}
	protected void GetApp_CurrentApprID()
	{

		DataTable dtCApprID = new DataTable();
		dtCApprID = spm.Get_APP_Approval_Details("Check_Current_M_Status", "SP_APP_Employee_Details", hdnempcode.Value, Convert.ToInt32(hdnAppointment_ID.Value));
		//capprid = (int)dtCApprID.Rows[0]["Status_ID"];
		if (dtCApprID.Rows.Count > 0)
		{
			if ((int)dtCApprID.Rows[0]["Appointment_ID"] != Convert.ToInt32(hdnAppointment_ID.Value))
			{
				Response.Redirect("~/procs/App_Latter_M_Index.aspx?itype=Pending");
			}
		}
		else
		{
		    Response.Redirect("~/procs/App_Latter_M_Index.aspx?itype=Pending");		
			lnk_Update_Profile.Visible = false;
			lblmessage.Text = "You already actioned for this request";
			return;
		}
	}

	public void SendMail_Employee(string AcceptanceDate)
	{
		StringBuilder strbuild = new StringBuilder();
		try
		{
			strbuild = new StringBuilder();
			strbuild.Capacity = 0;
			strbuild.Length = 0;
			strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td> Dear " + txtEmpName.Text + ",</ td></tr>");
			strbuild.Append("<tr><td style='height:15px'></td></tr>");
			strbuild.Append("<tr><td>This is to inform you that <b> " + hdnModerationName.Value + " </b> has approved your appointment letter acceptance. </td></tr>");
			strbuild.Append("<tr><td><hr></td></tr>");
			strbuild.Append("<tr><td>");
			strbuild.Append("<table style='width:100%';'color:#000000;font-size:11pt;font-family:Arial;font-style:Regular>");
			//strbuild.Append("<tr><td>This is in reference to your job application for the profile of   </td></tr>");
			strbuild.Append("<tr><td style='height:10px'></td></tr>");
			strbuild.Append("<tr><td >Employee Code  :</td><td >" + txtEmpCode.Text + "</td></tr>");
			strbuild.Append("<tr><td >Employee  Name :</td><td >" + txtEmpName.Text + "</td></tr>");
			strbuild.Append("<tr><td >Employee  Email ID :</td><td >" + txt_EmailAddress.Text + "</td></tr>");
			strbuild.Append("<tr><td >Department :</td><td >" + Txt_Department.Text + "</td></tr>");
			strbuild.Append("<tr><td >Date of Joining  :</td><td >" + txt_DOJ.Text + "</td></tr>");
			strbuild.Append("<tr><td >Appointment Issued Date :</td><td >" + txt_App_Issued_Date.Text + "</td></tr>");
			strbuild.Append("<tr><td >Appointment Acceptance Date :</td><td >" + txtAcceptanceDate.Text + "</td></tr>");
			strbuild.Append("<tr><td >Approval Date :</td><td >" + AcceptanceDate + "</td></tr>");
			strbuild.Append("<tr><td >Status  :</td><td> Approved </td></tr>");
			strbuild.Append("<tr><td >Remark :</td><td >" + txtRemark.Text + "</td></tr>");
			//strbuild.Append("<tr><td style='height:20px;'></td></tr>");
			strbuild.Append("</table>");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
			strbuild.Append("</table>");
			var strsubject = "OneHR:- Appointment Letter Approved - " + txtEmpName.Text;
			spm.sendMail(txt_EmailAddress.Text, strsubject, Convert.ToString(strbuild).Trim(), "", hdnCreateByEmail.Value);
		}
		catch (Exception)
		{
			throw;
		}
	}
	public void SendMail_Employee_SendforCorrection(string AcceptanceDate)
	{
		StringBuilder strbuild = new StringBuilder();
		try
		{
			string redirectURL = "http://localhost/hrms/login.aspx?ReturnUrl=procs/App_Latter_Acceptance.aspx";
			strbuild = new StringBuilder();
			strbuild.Capacity = 0;
			strbuild.Length = 0;
			strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td> Dear " + txtEmpName.Text + ",</ td></tr>");
			strbuild.Append("<tr><td style='height:15px'></td></tr>");
			strbuild.Append("<tr><td> This is to inform you that <b> " + hdnModerationName.Value + "</b> has sent back your appointment letter acceptance request for correction. Please physically sign the Appointment letter then Scan, Upload & submit for Approval again. </td></tr>");//of " + Emp_Name + " as per the details below.
			//strbuild.Append("<tr><td> " + hdnModerationName.Value + " has appointment letter & with signed has approved as per the details below following.</td></tr>");
			strbuild.Append("<tr><td><hr></td></tr>");
			strbuild.Append("<tr><td>");
			strbuild.Append("<table style='width:100%';'color:#000000;font-size:11pt;font-family:Arial;font-style:Regular>");
			strbuild.Append("<tr><td style='height:10px'></td></tr>");
			strbuild.Append("<tr><td >Employee Code  :</td><td >" + txtEmpCode.Text + "</td></tr>");
			strbuild.Append("<tr><td >Employee  Name :</td><td >" + txtEmpName.Text + "</td></tr>");
			strbuild.Append("<tr><td >Employee  Email ID :</td><td >" + txt_EmailAddress.Text + "</td></tr>");
			strbuild.Append("<tr><td >Department :</td><td >" + Txt_Department.Text + "</td></tr>");
			strbuild.Append("<tr><td >Date of Joining  :</td><td >" + txt_DOJ.Text + "</td></tr>");
			strbuild.Append("<tr><td >Appointment Issued Date :</td><td >" + txt_App_Issued_Date.Text + "</td></tr>");
			strbuild.Append("<tr><td >Appointment Acceptance Date :</td><td >" + txtAcceptanceDate.Text + "</td></tr>");
			strbuild.Append("<tr><td >Sent back for Correction on :</td><td >" + AcceptanceDate + "</td></tr>");
			strbuild.Append("<tr><td >Status :</td><td >Send For Correction</td></tr>");
			strbuild.Append("<tr><td >Remark :</td><td >" + txtRemark.Text + "</td></tr>");
			strbuild.Append("</table>");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td><a href='" + redirectURL + "' target='_blank'> Please click here to take action on appointment letter </td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
			strbuild.Append("</table>");
			var strsubject = "OneHR:- Appointment Letter Sent Back For Correction - " + txtEmpName.Text ;
			spm.sendMail(txt_EmailAddress.Text, strsubject, Convert.ToString(strbuild).Trim(), "", "");
		}
		catch (Exception)
		{
			throw;
		}
	}
	protected void lnk_Update_Profile_Click(object sender, EventArgs e)
	{
		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			int Read_Acceptance = 0, Appointment_ID = 0;
			#region Check For Blank Fields
			lblmessage.Text = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			//if (Convert.ToString(uploadfilesigned.FileName).Trim() == "")
			//{
			//	lblmessage.Text = "Please upload signed appointment Letter";
			//	return;
			//}
			Get_Moderation_Employee();
			#endregion
			Appointment_ID = Convert.ToInt32(hdnAppointment_ID.Value);
			spm.Update_APP_Employee_Details("Moderation_Update", Appointment_ID, filename, hdnempcode.Value, 3, hdnModerationCode.Value, Read_Acceptance,txtRemark.Text);
			DateTime loaded = DateTime.Now;
			strfromDate = loaded.ToString("dd-MM-yyyy hh:mm tt");
			//Send mail Moderation
			SendMail_Employee(strfromDate);
			Response.Redirect("~/procs/App_Latter_M_Index.aspx?itype=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	protected void lnk_Correction_Click(object sender, EventArgs e)
	{
		try
		{
			var filename = "";
			string strfromDate = "";
			var strfileName = "";
			int Read_Acceptance = 0, Appointment_ID = 0;
			#region Check For Blank Fields
			lblmessage.Text = "";
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString(txtRemark.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter remark !.";
				return;
			}
			Get_Moderation_Employee();
			#endregion
			Appointment_ID = Convert.ToInt32(hdnAppointment_ID.Value);
			spm.Update_APP_Employee_Details("Moderation_Update", Appointment_ID, filename, hdnempcode.Value, 4, hdnModerationCode.Value, Read_Acceptance,txtRemark.Text);
			DateTime loaded = DateTime.Now;
			strfromDate = loaded.ToString("dd-MM-yyyy hh:mm tt");
			//Send mail Moderation
			SendMail_Employee_SendforCorrection(strfromDate);
			Response.Redirect("~/procs/App_Latter_M_Index.aspx?itype=Pending");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
}