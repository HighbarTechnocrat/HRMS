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
public partial class procs_App_Latter_Acceptance : System.Web.UI.Page
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
					//FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["APPLatterAccPath"]).Trim());
					FilePath.Value = System.Configuration.ConfigurationManager.AppSettings["APPLatterAccPath"].ToString();
					hdnAdminFile.Value = @"D:\HRMS\hrmsadmin\CandidateDocument\";
					//txt_FD_DOB.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					
					if (Request.QueryString.Count > 0)
					{
						hdnType.Value = Convert.ToString(Request.QueryString[0]).Trim();
						lnk_Update_Profile.Visible = false;
						chk_Read_Acceptance.Enabled = false;
						uploadfilesigned.Visible = false;

					}
					else
					{
						GetApp_CurrentApprID();
					}
					Get_EmployeeDetails();

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
		dtEmployee = spm.Get_APP_Employee_Details("APP_Employee_Pending", "SP_APP_Employee_Details", hdnempcode.Value);
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
			//txtJobDescription.Text = Convert.ToString(dtEmployee.Rows[0]["DraftName"]).Trim();
			hdnAppointment_ID.Value = Convert.ToString(dtEmployee.Rows[0]["Appointment_ID"]).Trim();
			hdnAppointmentNo.Value = Convert.ToString(dtEmployee.Rows[0]["AppointmentNo"]).Trim();
			ltTable.Text= Convert.ToString(dtEmployee.Rows[0]["DraftName"]).Trim();
			if (Convert.ToInt32(dtEmployee.Rows[0]["Status_ID"])!= 1 )//&& Convert.ToInt32(dtEmployee.Rows[0]["Status_ID"]) != 4)
			{
				if (hdnType.Value == "View" || Convert.ToInt32(dtEmployee.Rows[0]["Status_ID"]) == 4)
				{
					//UlModeration.Visible = true;
					lnkSinedFile.Text = Convert.ToString(dtEmployee.Rows[0]["Signed_File"]).Trim();
					chk_Read_Acceptance.Checked = Convert.ToBoolean(dtEmployee.Rows[0]["Read_Acceptance"]);
					txtRemark.Text = Convert.ToString(dtEmployee.Rows[0]["Remark"]).Trim();
					txtModerationName.Text = Convert.ToString(dtEmployee.Rows[0]["ModeName"]).Trim();
					txtApprovalDate.Text = Convert.ToString(dtEmployee.Rows[0]["Approval_Date"]).Trim();
					txtStatus.Text = Convert.ToString(dtEmployee.Rows[0]["Status_Title"]).Trim();
				}
			}
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
		dtCApprID = spm.Get_APP_Employee_Details("Check_Current_Status", "SP_APP_Employee_Details", hdnempcode.Value);
		//capprid = (int)dtCApprID.Rows[0]["Status_ID"];
		if (dtCApprID.Rows.Count > 0)
		{
		}
		else
		{
			lnk_Update_Profile.Visible = false;
			lblmessage.Text = "You have already submitted Acceptance for this request";
			return;
		}
	}


	public void SendMail_Moderation(string AcceptanceDate)
	{
		StringBuilder strbuild = new StringBuilder();
		try
		{
            DataSet ds = new DataSet();
            SqlParameter[] spars = new SqlParameter[1];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "Moderation_Employee_CC";

            string strCC = "";

            ds = spm.getDatasetList(spars, "SP_APP_Employee_Details");

            if (ds.Tables[0].Rows.Count > 0)
            {
                strCC = ds.Tables[0].Rows[0]["Emp_Emailaddress"].ToString();
            }


            string redirectURL = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/App_Latter_Approval.aspx?Appointment_ID=" + hdnAppointment_ID.Value + "&Type=Pending";
			strbuild = new StringBuilder();
			strbuild.Capacity = 0;
			strbuild.Length = 0;
			strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td> Dear " + hdnModerationName.Value + ",</ td></tr>");
			strbuild.Append("<tr><td style='height:15px'></td></tr>");
			strbuild.Append("<tr><td>This is to inform you that <b>" + txtEmpName.Text + "</b> has read & accepted appointment letter.</td></tr>");
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
			strbuild.Append("<tr><td >Appointment Acceptance Date :</td><td >" + AcceptanceDate + "</td></tr>");
			strbuild.Append("<tr><td >Status :</td><td >Accepted</td></tr>");
			//strbuild.Append("<tr><td style='height:20px;'></td></tr>");
			strbuild.Append("</table>");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			//strbuild.Append("<tr><td><a href='" + redirectURL + "' target='_blank'> Please click here to take action on appointment letter approval </td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
			strbuild.Append("</table>");
			var strsubject = "OneHR:- Appointment Letter Accepted - " + txtEmpName.Text;
			spm.sendMail(hdnModerationEmail.Value, strsubject, Convert.ToString(strbuild).Trim(), "", strCC);
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
			if (!chk_Read_Acceptance.Checked)
			{
				lblmessage.Text = "Please Accept Terms & Conditions";
				return;
			}
			//if (Convert.ToString(uploadfilesigned.FileName).Trim() == "")
			//{
			//	lblmessage.Text = "Please upload signed appointment Letter";
			//	return;
			//}
			Get_Moderation_Employee();
			#endregion
			if (Convert.ToString(hdnAppointmentNo.Value).Trim() != "")
			{
				filename = "ALS_" + txtEmpCode.Text + ".pdf";
			}
				//if (Convert.ToString(uploadfilesigned.FileName).Trim() != "")
				//{
				//	DateTime loadedDate = DateTime.Now;
				//	strfromDate = loadedDate.ToString("ddMMyyyyHHmmss");
				//	filename = uploadfilesigned.FileName;
				//	strfileName = "";
				//	strfileName = "ALS_" + txtEmpCode.Text + "_" + strfromDate + Path.GetExtension(uploadfilesigned.FileName);
				//	filename = strfileName;
				//	//uploadfilesigned.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["APPLatterAccPath"]).Trim()), strfileName));
				//}
				if (chk_Read_Acceptance.Checked)
			{
				Read_Acceptance = 1;
			}
			Appointment_ID = Convert.ToInt32(hdnAppointment_ID.Value);
			spm.Update_APP_Employee_Details("Employee_Update", Appointment_ID, filename, txtEmpCode.Text, 2, hdnModerationCode.Value, Read_Acceptance,"");
			if (Convert.ToString(hdnAppointmentNo.Value).Trim() != "")
			{
				SignCopy_Appointment_Letter(filename);
			}
			DateTime loaded= DateTime.Now;
			strfromDate = loaded.ToString("dd-MM-yyyy hh:mm tt");
			//Send mail Moderation
			SendMail_Moderation(strfromDate);
			Response.Redirect("~/procs/App_Latter_Index.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void SignCopy_Appointment_Letter(string filename)
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Get_Appointment_Candidate_Details";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = txtEmpCode.Text.Trim();
			DataSet DTResult = spm.getDatasetList(spars, "SP_Rec_Generate_Offer_Candidate");
			if (DTResult.Tables[0].Rows.Count > 0)
			{
				LocalReport ReportViewer1 = new LocalReport();
				//string filename = "";
				string dsysdate = DateTime.Now.ToString("ddMMyyyy_HHmmss");
				//filename = dsOffer.Tables[0].Rows[0]["Candidate_ID"].ToString();
				//filename = Txt_Code.Text.Trim() + ".PDF";
				ReportViewer1.ReportPath = Server.MapPath("~/Procs/Appointment_Letter_Employee.rdlc");
				ReportDataSource rds = new ReportDataSource("AppointmentD", DTResult.Tables[0]);
				ReportDataSource rd1 = new ReportDataSource("Compensation", DTResult.Tables[1]);
				ReportViewer1.DataSources.Clear();
				ReportViewer1.DataSources.Add(rds);
				ReportViewer1.DataSources.Add(rd1);
				byte[] Bytes = ReportViewer1.Render(format: "PDF", deviceInfo: "");
				using (FileStream stream = new FileStream(FilePath.Value + filename, FileMode.Create))
				//using (FileStream stream = new FileStream(Server.MapPath(ConfigurationManager.AppSettings["APPLatterAccPath"]) + filename, FileMode.Create))
				{
					stream.Write(Bytes, 0, Bytes.Length);
				}
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.ToString());
		}
	}
}