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
public partial class procs_ExitProcess_Mo_Approval : System.Web.UI.Page
{
	public string userid;
	SP_Methods spm = new SP_Methods();
	public DataTable dtEmp, dtRectruter;
	public string filename = "", approveremailaddress;

	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	#region Page Event
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			lblmessage.Text = "";
			if (Convert.ToString(Session["Empcode"]).Trim() == "")
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");
			lblmessage.Visible = true;
			//   
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					txt_WithEffectFrom.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtRetentionFromDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtRetentionTillDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					hdnFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RetentionPath"]).Trim());
					if (Request.QueryString.Count > 0)
					{
						btBack.Visible = true;
						hdnRetentionID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnType.Value = Convert.ToString(Request.QueryString[1]).Trim();
						if (hdnType.Value == "APP")
						{
							Get_Retention_Mode_Pending_Check();
							Get_Employee_Retention_Details();
							btBack.Visible = true;
							btBack1.Visible = false;
						}
						else
						{
							Get_Employee_Retention_Details();
							btBack.Visible = false;
							btBack1.Visible = true;
						}

					}
					this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	protected void lnk_Update_Profile_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			DataTable dtResult;
			lblmessage.Text = "";
			string strfileName = "";
			int RetentionID = 0;
			//if (txtRemark.Text == "")
			//{
			//	lblmessage.Text = "Please Enter Remark";
			//	return;
			//}
			if (Convert.ToString(UploadFile.FileName).Trim() == "")
			{
				lblmessage.Text = "Please Upload Revised Letter Files";
				return;
			}
			if (Convert.ToString(UploadFile.FileName).Trim() != "")
			{
				string fileName = Path.GetFileName(UploadFile.FileName);
				string Dates = DateTime.Now.ToString("ddMMyyyy_HHmmss");
				strfileName = "M_" + txtEmpCode.Text.Trim() + "_" + Dates + Path.GetExtension(UploadFile.FileName);
				UploadFile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RetentionPath"]).Trim()), strfileName));
			}
			RetentionID = string.IsNullOrEmpty(hdnRetentionID.Value) ? 0 : Convert.ToInt32(hdnRetentionID.Value);
			dtResult = spm.Update_Retention_Employee("Update_M_Retention", RetentionID, Convert.ToString(Session["Empcode"]).Trim(), txtEmpCode.Text, txtMRemark.Text.Trim(), strfileName, 2);
			if (dtResult.Rows.Count > 0)
			{
				StringBuilder strbuild = new StringBuilder();
				strbuild.Length = 0;
				strbuild.Clear();
				string To_email = "";
				SqlParameter[] spars1 = new SqlParameter[2];
				spars1[0] = new SqlParameter("@Qtype", SqlDbType.NVarChar);
				spars1[0].Value = "Retention_Mail_CC";
				spars1[1] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
				spars1[1].Value = txtEmpCode.Text;
				DataTable tt2 = spm.getData_FromCode(spars1, "SP_Employee_Retention_Details");
				if (tt2.Rows.Count > 0)
				{
					To_email = "";
					foreach (DataRow row in tt2.Rows)
					{
						To_email = To_email + ";" + Convert.ToString(row["Emp_Emailaddress"].ToString());
					}
				}
				string sattchedfileName = "";

				//strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Retention_Approval"]).Trim() + "?RetentionID=" + RetentionID + "&Type=APP";
				strbuild.Append("<table border='1' style='width:95%;'>");
				strbuild.Append("<tr><td style='width:30%;'>Employee Code: </td><td style='width:70%;'>" + Convert.ToString(txtEmpCode.Text)
					 + "</td></tr><tr><td style='width:30%;'>Employee Name: </td><td style='width:70%;'>" + Convert.ToString(txtEmployeeName.Text)
					 + "</td></tr><tr><td style='width:30%;'>Location: </td><td style='width:70%;'>" + Convert.ToString(Txt_ProjectCode.Text)
					 + "</td></tr><tr><td style='width:30%;'>Department: </td><td style='width:70%;'>" + Convert.ToString(Txt_Department.Text)
					 + "</td></tr><tr><td style='width:30%;'>Designation: </td><td style='width:70%;'>" + Convert.ToString(txt_Designation.Text)
					 + "</td></tr><tr><td style='width:30%;'>Date of Joining: </td><td style='width:70%;'>" + Convert.ToString(txt_DOJ.Text)
					 + "</td></tr><tr><td style='width:30%;'>Retention Type: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionType.Text)
					 + "</td></tr><tr><td style='width:30%;'>With Effect From: </td><td style='width:70%;'>" + Convert.ToString(txt_WithEffectFrom.Text)
					 + "</td></tr><tr><td style='width:30%;'>Retention From Date: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionFromDate.Text)
					 + "</td></tr><tr><td style='width:30%;'>Retention Till Date: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionTillDate.Text)
					 + "</td></tr><tr><td style='width:30%;'>Remark: </td><td style='width:70%;'>" + Convert.ToString(txtRemark.Text)
					+ "</td></tr><tr><td style='width:30%;'>Retention Status: </td><td style='width:70%;'>Completed"+"</td></tr>");

				if (txtMRemark.Text != "")
				{
					strbuild.Append("<tr><td style='width:30%;'>Moderation Remark: </td><td style='width:70%;'>" + Convert.ToString(txtMRemark.Text)
					+ "</td></tr>");
				}
				strbuild.Append("</table>");
				sattchedfileName = Server.MapPath(ConfigurationManager.AppSettings["RetentionPath"]) + strfileName;
				string Subject = "OneHR - " + txtEmployeeName.Text + "’s revised CTC letter";
				string LoginName = Convert.ToString(Session["emp_loginName"]);
				spm.SendMail_Retaination_CreateBy(To_email, LoginName, Subject, txtEmployeeName.Text, Convert.ToString(strbuild), "", "", sattchedfileName);
				Response.Redirect("~/procs/ExitProcess_Mo_Index.aspx?type=Pending");
			}
			else
			{
				lblmessage.Text = "Please check record already exists";
				return;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	} 
	#endregion
	#region Page Method
	public void Get_Employee_Retention_Details()
	{
		try
		{
			int RetentionID = Convert.ToInt32(hdnRetentionID.Value);
			DataTable dtResult = spm.Get_Retention_ID("Retention_EMP_ID_Mode", Convert.ToString(Session["Empcode"]).Trim(), RetentionID);
			if (dtResult.Rows.Count > 0)
			{
				Txt_ProjectCode.Text = dtResult.Rows[0]["Location_name"].ToString();
				txtEmpCode.Text = dtResult.Rows[0]["Emp_Code"].ToString();
				Txt_Department.Text = dtResult.Rows[0]["Department_Name"].ToString();
				txt_Designation.Text = dtResult.Rows[0]["DesginationName"].ToString();
				txtEmployeeName.Text = dtResult.Rows[0]["Emp_Name"].ToString();
				txt_DOJ.Text = dtResult.Rows[0]["emp_doj"].ToString();
				txtRetentionType.Text = dtResult.Rows[0]["RetentionTypeName"].ToString();
				txt_WithEffectFrom.Text = dtResult.Rows[0]["WithEffectFrom"].ToString();
				txtRetentionFromDate.Text = dtResult.Rows[0]["FromDate"].ToString();
				txtRetentionTillDate.Text = dtResult.Rows[0]["ToDate"].ToString();
				txtRemark.Text = dtResult.Rows[0]["Remarks"].ToString();
				hdnStatus_ID.Value = dtResult.Rows[0]["StatusID"].ToString();
				txtStatus.Text = dtResult.Rows[0]["StatusName"].ToString();

				Get_Retention_MultipleFile();
				if (Convert.ToString(txt_WithEffectFrom.Text) != "")
				{
					Effect.Visible = true;
				}

				if (dtResult.Rows[0]["StatusID"].ToString() == "1")
				{
					txtMRemark.Enabled = true;
					ApprovalDate.Visible = false;
				}
				else
				{
					txtMRemark.Text = dtResult.Rows[0]["MRemarks"].ToString();
					txtApprovalDate.Text = dtResult.Rows[0]["RevisedDate"].ToString();
					lnkRevisedFile.Text = dtResult.Rows[0]["RevisedLetterFile"].ToString();
					txtMRemark.Enabled = false;
					ApprovalDate.Visible = true;
					UploadFile.Visible = false;
					lnk_Update_Profile.Visible = false;
					lnk_Cancle.Visible = false;
				}
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Get_Retention_MultipleFile()
	{
		try
		{
			int RetentionID = Convert.ToInt32(hdnRetentionID.Value);
			DataTable dtResult = spm.Get_Retention_ID("MyRetention_EMP_File", Convert.ToString(Session["Empcode"]).Trim(), RetentionID);
			GRDRetentionfile.DataSource = null;
			GRDRetentionfile.DataBind();
			if (dtResult.Rows.Count > 0)
			{
				GRDRetentionfile.DataSource = dtResult;
				GRDRetentionfile.DataBind();
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Get_Retention_Mode_Pending_Check()
	{
		try
		{
			int RetentionID = Convert.ToInt32(hdnRetentionID.Value);
			DataTable dtResult = spm.Get_Retention_ID("Retention_M_Pending", Convert.ToString(Session["Empcode"]).Trim(), RetentionID);
			if (dtResult.Rows.Count == 0)
			{
				Response.Redirect("~/procs/ExitProcess_Mo_Index.aspx?Type=Pending");
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Get_Moderation_Employee()
	{
		DataTable dtEmployee = new DataTable();
		dtEmployee = spm.Get_Dept_Retention_Detail("Moderation_Employee", hdnEmpCode.Value);
		if (dtEmployee.Rows.Count > 0)
		{
			hdnModerationEmail.Value = Convert.ToString(dtEmployee.Rows[0]["Emp_Emailaddress"]).Trim();
			hdnModerationName.Value = Convert.ToString(dtEmployee.Rows[0]["Emp_Name"]).Trim();
			hdnModerationCode.Value = Convert.ToString(dtEmployee.Rows[0]["Emp_Code"]).Trim();
		}
	} 
	#endregion

}