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

public partial class procs_EmployeeRetentionDetails : System.Web.UI.Page
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

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;

				if (!Page.IsPostBack)
				{
					hdnFilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RetentionPath"]).Trim());
					txt_WithEffectFrom.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtRetentionFromDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtRetentionTillDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");

					Check_Employee_HOD_Dept();
					Get_RetentionType();
					if (Request.QueryString.Count > 0)
					{
						btBack.Visible = true;
						hdnRetentionID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						Get_Employee_Retention();
						Get_Employee_Retention_Details();
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
	#region Method
	public void Check_Employee_HOD_Dept()
	{
		try
		{
			DataTable dtResult = spm.Get_Dept_Retention_Detail("Get_Resig_Dept_Employee", Convert.ToString(Session["Empcode"]).Trim());
			if (dtResult.Rows.Count > 0)
			{
				DDLEmpName.DataSource = dtResult;
				DDLEmpName.DataTextField = "Emp_Name";
				DDLEmpName.DataValueField = "Emp_Code";
				DDLEmpName.DataBind();
				ListItem item = new ListItem("Select Employee Name", "0");
				DDLEmpName.Items.Insert(0, item);
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Get_Employee_Retention()
	{
		try
		{
			DataTable dtResult = spm.Get_Dept_Retention_Detail("MyRetention_EMP", Convert.ToString(Session["Empcode"]).Trim());
			if (dtResult.Rows.Count > 0)
			{
				DDLEmpName.DataSource = dtResult;
				DDLEmpName.DataTextField = "Emp_Name";
				DDLEmpName.DataValueField = "Emp_Code";
				DDLEmpName.DataBind();
				ListItem item = new ListItem("Select Employee Name", "0");
				DDLEmpName.Items.Insert(0, item);
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Get_RetentionType()
	{
		try
		{
			DataTable dtResult = spm.Get_Dept_Retention_Detail("Get_RetentionType", Convert.ToString(Session["Empcode"]).Trim());
			if (dtResult.Rows.Count > 0)
			{
				ddlRetentionType.DataSource = dtResult;
				ddlRetentionType.DataTextField = "RetentionTypeName";
				ddlRetentionType.DataValueField = "RetentionTypeID";
				ddlRetentionType.DataBind();
				ListItem item = new ListItem("Select Retention Type", "0");
				ddlRetentionType.Items.Insert(0, item);
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	public void Get_Employee_Retention_Details()
	{
		try
		{
			int RetentionID = Convert.ToInt32(hdnRetentionID.Value);
			DataTable dtResult = spm.Get_Retention_ID("MyRetention_EMP_ID", Convert.ToString(Session["Empcode"]).Trim(), RetentionID);
			if (dtResult.Rows.Count > 0)
			{
				Txt_ProjectCode.Text = dtResult.Rows[0]["Location_name"].ToString();
				txtEmpCode.Text = dtResult.Rows[0]["Emp_Code"].ToString();
				Txt_Department.Text = dtResult.Rows[0]["Department_Name"].ToString();
				txt_Designation.Text = dtResult.Rows[0]["DesginationName"].ToString();
				DDLEmpName.SelectedValue = dtResult.Rows[0]["Emp_Code"].ToString();
				txt_DOJ.Text = dtResult.Rows[0]["emp_doj"].ToString();
				ddlRetentionType.SelectedValue = dtResult.Rows[0]["RetentionTypeID"].ToString();
				txt_WithEffectFrom.Text = dtResult.Rows[0]["WithEffectFrom"].ToString();
				txtRetentionFromDate.Text = dtResult.Rows[0]["FromDate"].ToString();
				txtRetentionTillDate.Text = dtResult.Rows[0]["ToDate"].ToString();
				txtRemark.Text = dtResult.Rows[0]["Remarks"].ToString();
				hdnStatus_ID.Value = dtResult.Rows[0]["StatusID"].ToString();

				txt_WithEffectFrom.Enabled = false;
				txtRetentionFromDate.Enabled = false;
				txtRetentionTillDate.Enabled = false;
				txtRemark.Enabled = false;
				lstPeriod.Enabled = false;
				ddlRetentionType.Enabled = false;
				DDLEmpName.Enabled = false;
				h1.Visible = false;
				Get_Retention_MultipleFile();
				lnk_Update_Profile.Visible = false;
				SPUpload.Visible = false;
				if (dtResult.Rows[0]["RetentionTypeID"].ToString() == "1" && dtResult.Rows[0]["StatusID"].ToString() == "4")
				{
					ddlRetentionType.Enabled = true;
					lnk_Update.Visible = true;
				}
				if (dtResult.Rows[0]["StatusID"].ToString() != "4")
				{
					UlModeration.Visible = true;
					txtStatus.Text = dtResult.Rows[0]["StatusName"].ToString();
					txtMRemark.Text = dtResult.Rows[0]["MRemarks"].ToString();
					txtApprovalDate.Text = dtResult.Rows[0]["RevisedDate"].ToString();
					lnkRevisedFile.Text = dtResult.Rows[0]["RevisedLetterFile"].ToString();
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
	public void Get_Employee_Details(string Empcode)
	{
		try
		{
			DataTable dtResult = spm.Get_Dept_Retention_Detail("Get_Employee_Details", Empcode);
			if (dtResult.Rows.Count > 0)
			{
				Txt_ProjectCode.Text = dtResult.Rows[0]["Location_name"].ToString();
				txtEmpCode.Text = dtResult.Rows[0]["Emp_Code"].ToString();
				Txt_Department.Text = dtResult.Rows[0]["Department_Name"].ToString();
				txt_Designation.Text = dtResult.Rows[0]["DesginationName"].ToString();
				txt_DOJ.Text = dtResult.Rows[0]["emp_doj"].ToString();
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
	private void ClearValues()
	{
		txtEmpCode.Text = "";
		Txt_Department.Text = "";
		txt_Designation.Text = "";
		txt_DOJ.Text = "";
		Txt_ProjectCode.Text = "";
	}
	private void GetRetentionType()
	{
		if (hdnStatus_ID.Value != "" && ddlRetentionType.SelectedValue != "2")
		{
			txtRetentionFromDate.Enabled = false;
			txtRetentionTillDate.Enabled = false;
			txtRemark.Enabled = false;
			lstPeriod.Enabled = false;
			h1.Visible = false;
			SPEffect.Visible = false;
			txt_WithEffectFrom.Enabled = false;
			txt_WithEffectFrom.Text = "";
			SPUpload.Visible = false;

		}
		else if (hdnStatus_ID.Value == "" && ddlRetentionType.SelectedValue == "1")
		{
			h1.Visible = true;
			SPEffect.Visible = false;
			txt_WithEffectFrom.Enabled = false;
			txt_WithEffectFrom.Text = "";
			SPUpload.Visible = true;
		}
		else
		{
			
			SPEffect.Visible = true;
			txt_WithEffectFrom.Enabled = true;
			txtRetentionFromDate.Enabled = true;
			txtRetentionTillDate.Enabled = true;
			txtRemark.Enabled = true;
			lstPeriod.Enabled = true;
			h1.Visible = true;

		}
	}
	#endregion
	#region Page Event Button
	protected void lnk_Update_Profile_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			string[] strdate, strProbable, strTill;
			string strtoDate = "", Probable = "", Todate, multiplefilename = "", multiplefilenameadd = "", MailAttachment = "";
			DateTime Offerdate, ProbableDate, Tilldate;
			DataTable dtResult;
			lblmessage.Text = "";
			int IsException = 0, PID = 0, RetentionTypeID = 0, StatusID=0;
			if (DDLEmpName.SelectedValue == "" || DDLEmpName.SelectedValue == "0")
			{
				lblmessage.Text = "Please Select Employee Name";
				return;
			}

			if (ddlRetentionType.SelectedValue == "0" || ddlRetentionType.SelectedValue == "")
			{
				lblmessage.Text = "Please Select Retention Type";
				return;
			}
			if (ddlRetentionType.SelectedValue == "2")
			{
				if (txt_WithEffectFrom.Text == "")
				{
					lblmessage.Text = "Please enter With Effect From";
					return;
				}
			}
			if (txtRetentionFromDate.Text == "")
			{
				lblmessage.Text = "Please Enter Retention From Date";
				return;
			}
			if (txtRetentionTillDate.Text == "")
			{
				lblmessage.Text = "Please Enter Retention Till Date";
				return;
			}
			if (Convert.ToString(FileUpload.FileName).Trim() == "")
			{
				lblmessage.Text = "Please Enter Retention Files";
				return;
			}
			if (txtRemark.Text == "")
			{
				lblmessage.Text = "Please Enter Retention Remark";
				return;
			}

			if (Convert.ToString(FileUpload.FileName).Trim() != "")
			{
				HttpFileCollection fileCollection = Request.Files;
				for (int i = 0; i < fileCollection.Count; i++)
				{
					HttpPostedFile uploadfileName = fileCollection[i];
					string fileName = Path.GetFileName(uploadfileName.FileName);
					if (uploadfileName.ContentLength > 0)
					{
						multiplefilename = fileName;
						string strfileName = "";
						string Dates = DateTime.Now.ToString("ddMMyyyy_HHmmss");
						strfileName = txtEmpCode.Text.Trim() + "_" + Dates + "_" + i + Path.GetExtension(uploadfileName.FileName);
						multiplefilename = strfileName;
						uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RetentionPath"]).Trim()), strfileName));
						multiplefilenameadd += strfileName + ",";


					}
				}
				multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			}

			if (ddlRetentionType.SelectedValue == "2")
			{
				IsException = 1;
				StatusID = 1;
			}
			else
			{
				StatusID = 4;
				IsException = 0;
			}
			if (ddlRetentionType.SelectedValue == "2")
			{
				strdate = Convert.ToString(txt_WithEffectFrom.Text).Trim().Split('/');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				//Offerdate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			}
			strProbable = Convert.ToString(txtRetentionFromDate.Text).Trim().Split('/');
			Probable = Convert.ToString(strProbable[2]) + "-" + Convert.ToString(strProbable[1]) + "-" + Convert.ToString(strProbable[0]);
			ProbableDate = DateTime.ParseExact(Probable, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			strTill = Convert.ToString(txtRetentionTillDate.Text).Trim().Split('/');
			Todate = Convert.ToString(strTill[2]) + "-" + Convert.ToString(strTill[1]) + "-" + Convert.ToString(strTill[0]);
			Tilldate = DateTime.ParseExact(Todate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			Get_Moderation_Employee();//Moderation Info

			RetentionTypeID = string.IsNullOrEmpty(ddlRetentionType.SelectedValue) ? 0 : Convert.ToInt32(ddlRetentionType.SelectedValue);
			dtResult = spm.Insert_Retention_Employee("InsertEmployeeRetention", 0, Convert.ToString(Session["Empcode"]).Trim(), txtEmpCode.Text, RetentionTypeID, strtoDate, ProbableDate, Tilldate, txtRemark.Text.Trim(), hdnModerationCode.Value, multiplefilenameadd, IsException, StatusID);
			if (dtResult.Rows.Count > 0)
			{
				if (ddlRetentionType.SelectedValue == "2")
				{
					int RetentionID = 0;
					RetentionID = Convert.ToInt32(dtResult.Rows[0]["RetentionID"].ToString());
					StringBuilder strbuild = new StringBuilder();
					strbuild.Length = 0;
					strbuild.Clear();
					string cc_email = "";
					SqlParameter[] spars1 = new SqlParameter[2];
					spars1[0] = new SqlParameter("@Qtype", SqlDbType.VarChar);
					spars1[0].Value = "Retention_Mail_CC";
					spars1[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
					spars1[1].Value = txtEmpCode.Text;
					DataTable tt2 = spm.getData_FromCode(spars1, "SP_Employee_Retention_Details");
					if (tt2.Rows.Count > 0)
					{
						cc_email = "";
						foreach (DataRow row in tt2.Rows)
						{
							cc_email = cc_email + ";" + Convert.ToString(row["Emp_Emailaddress"].ToString());
						}
					}
					string strOfferApprovalURL = "";
					strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Retention_Approval"]).Trim() + "?RetentionID=" + RetentionID + "&Type=APP";
					strbuild.Append("<table border='1' style='width:95%;'>");
					strbuild.Append("<tr><td style='width:30%;'>Employee Code: </td><td style='width:70%;'>" + Convert.ToString(txtEmpCode.Text)
						 + "</td></tr><tr><td style='width:30%;'>Employee Name: </td><td style='width:70%;'>" + Convert.ToString(DDLEmpName.SelectedItem.Text)
						 + "</td></tr><tr><td style='width:30%;'>Location: </td><td style='width:70%;'>" + Convert.ToString(Txt_ProjectCode.Text)
						 + "</td></tr><tr><td style='width:30%;'>Department: </td><td style='width:70%;'>" + Convert.ToString(Txt_Department.Text)
						 + "</td></tr><tr><td style='width:30%;'>Designation: </td><td style='width:70%;'>" + Convert.ToString(txt_Designation.Text)
						 + "</td></tr><tr><td style='width:30%;'>Date of Joining: </td><td style='width:70%;'>" + Convert.ToString(txt_DOJ.Text)
						 + "</td></tr><tr><td style='width:30%;'>Retention Type: </td><td style='width:70%;'>" + Convert.ToString(ddlRetentionType.SelectedItem.Text)
						 + "</td></tr><tr><td style='width:30%;'>With Effect From: </td><td style='width:70%;'>" + Convert.ToString(txt_WithEffectFrom.Text)
						 + "</td></tr><tr><td style='width:30%;'>Retention From Date: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionFromDate.Text)
						  + "</td></tr><tr><td style='width:30%;'>Retention Till Date: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionTillDate.Text)
						 + "</td></tr><tr><td style='width:30%;'>Remark: </td><td style='width:70%;'>" + Convert.ToString(txtRemark.Text)
						 + "</td></tr>");
					strbuild.Append("</table>");
					MailAttachment = Server.MapPath(ConfigurationManager.AppSettings["RetentionPath"]);
					string LoginName =Convert.ToString(Session["emp_loginName"]);
					string Subject = "OneHR - " + DDLEmpName.SelectedItem.Text + " is retained by "+ LoginName;
					spm.SendMail_Retaination_Moderation(hdnModerationEmail.Value, hdnModerationName.Value, Subject, DDLEmpName.SelectedItem.Text, LoginName, Convert.ToString(strbuild), cc_email, strOfferApprovalURL, MailAttachment, multiplefilenameadd);

				}
				Response.Redirect("~/procs/MyRetentionInbox.aspx");
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
	protected void ddlRetentionType_SelectedIndexChanged(object sender, EventArgs e)
	{
			GetRetentionType();
	}
	protected void lstPeriod_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			DateTime LWDay = new DateTime();
			string Rdate = "", FromDate = "";
			if (lstPeriod.SelectedIndex > 0 && txtRetentionFromDate.Text != "")
			{
				FromDate = txtRetentionFromDate.Text;
				DateTime dtFromDate = DateTime.ParseExact(FromDate, "dd/MM/yyyy", null);
				if (lstPeriod.SelectedValue == "1")
				{
					LWDay = dtFromDate.AddMonths(6);

				}
				else if (lstPeriod.SelectedValue == "2")
				{
					LWDay = dtFromDate.AddYears(1);
				}
				else if (lstPeriod.SelectedValue == "3")
				{
					LWDay = dtFromDate.AddMonths(18);
				}
				else if (lstPeriod.SelectedValue == "4")
				{
					LWDay = dtFromDate.AddYears(2);
				}
				else
				{
					LWDay = DateTime.Now;
				}
				LWDay = LWDay.AddDays(-1);
				Rdate = LWDay.ToString("dd/MM/yyyy");
				DateTime ddt = DateTime.ParseExact(Rdate, "dd/MM/yyyy", null);
				// DateTime ddt = DateTime.ParseExact(Rdate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
				txtRetentionTillDate.Text = ddt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
			}
			else
			{
				txtRetentionTillDate.Text = "";
			}
			//--DateTime ddt = DateTime.ParseExact(LWDay, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			//--RequiredDate = ddt.AddDays(Days);

		}
		catch (Exception ex)
		{
			Response.Write(ex.ToString());
		}
	}
	protected void lnk_Update_Click(object sender, EventArgs e)
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
			string[] strdate, strProbable, strTill;
			string strtoDate = "", Probable = "", Todate="", MailAttachment = "", multiplefilenameadd = "", multiplefilename="";
			DateTime  ProbableDate, Tilldate;
			int RetentionID = 0, RetentionTypeID = 0, IsException=0, StatusID=0;
			if (ddlRetentionType.SelectedValue == "0" || ddlRetentionType.SelectedValue == "")
			{
				lblmessage.Text = "Please Select Retention Type";
				return;
			}
			if (ddlRetentionType.SelectedValue == "2")
			{
				if (txt_WithEffectFrom.Text == "")
				{
					lblmessage.Text = "Please enter With Effect From";
					return;
				}
			}
			if (txtRetentionFromDate.Text == "")
			{
				lblmessage.Text = "Please Enter Retention From Date";
				return;
			}
			if (txtRetentionTillDate.Text == "")
			{
				lblmessage.Text = "Please Enter Retention Till Date";
				return;
			}
			//if (Convert.ToString(FileUpload.FileName).Trim() == "")
			//{
			//	lblmessage.Text = "Please Enter Retention Files";
			//	return;
			//}
			if (txtRemark.Text == "")
			{
				lblmessage.Text = "Please Enter Retention Remark";
				return;
			}

			if (Convert.ToString(FileUpload.FileName).Trim() != "")
			{
				HttpFileCollection fileCollection = Request.Files;
				for (int i = 0; i < fileCollection.Count; i++)
				{
					HttpPostedFile uploadfileName = fileCollection[i];
					string fileName = Path.GetFileName(uploadfileName.FileName);
					if (uploadfileName.ContentLength > 0)
					{
						multiplefilename = fileName;
						string strfileName = "";
						string Dates = DateTime.Now.ToString("ddMMyyyy_HHmmss");
						strfileName = txtEmpCode.Text.Trim() + "_" + Dates + "_" + i + Path.GetExtension(uploadfileName.FileName);
						multiplefilename = strfileName;
						uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["RetentionPath"]).Trim()), strfileName));
						multiplefilenameadd += strfileName + ",";


					}
				}
				multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			}

			if (ddlRetentionType.SelectedValue == "2")
			{
				IsException = 1;
				StatusID = 1;
			}
			else
			{
				StatusID = 4;
				IsException = 0;
			}
			if (ddlRetentionType.SelectedValue == "2")
			{
				strdate = Convert.ToString(txt_WithEffectFrom.Text).Trim().Split('/');
				strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				//Offerdate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			}
			strProbable = Convert.ToString(txtRetentionFromDate.Text).Trim().Split('/');
			Probable = Convert.ToString(strProbable[2]) + "-" + Convert.ToString(strProbable[1]) + "-" + Convert.ToString(strProbable[0]);
			ProbableDate = DateTime.ParseExact(Probable, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			strTill = Convert.ToString(txtRetentionTillDate.Text).Trim().Split('/');
			Todate = Convert.ToString(strTill[2]) + "-" + Convert.ToString(strTill[1]) + "-" + Convert.ToString(strTill[0]);
			Tilldate = DateTime.ParseExact(Todate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			Get_Moderation_Employee();//Moderation Info

			RetentionTypeID = string.IsNullOrEmpty(ddlRetentionType.SelectedValue) ? 0 : Convert.ToInt32(ddlRetentionType.SelectedValue);
			RetentionID = string.IsNullOrEmpty(hdnRetentionID.Value) ? 0 : Convert.ToInt32(hdnRetentionID.Value);
			dtResult = spm.Insert_Retention_Employee("Update_Retention_Type", RetentionID, Convert.ToString(Session["Empcode"]).Trim(), txtEmpCode.Text, RetentionTypeID, strtoDate, ProbableDate, Tilldate, txtRemark.Text.Trim(), hdnModerationCode.Value, multiplefilenameadd, IsException, StatusID);
			if (dtResult.Rows.Count > 0) 
			{
				StringBuilder strbuild = new StringBuilder();
				strbuild.Length = 0;
				strbuild.Clear();
				string cc_email = "";
				SqlParameter[] spars1 = new SqlParameter[2];
				spars1[0] = new SqlParameter("@Qtype", SqlDbType.NVarChar);
				spars1[0].Value = "Retention_Mail_CC";
				spars1[1] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
				spars1[1].Value = txtEmpCode.Text;
				DataTable tt2 = spm.getData_FromCode(spars1, "SP_Employee_Retention_Details");
				if (tt2.Rows.Count > 0)
				{
					cc_email = "";
					foreach (DataRow row in tt2.Rows)
					{
						cc_email = cc_email + ";" + Convert.ToString(row["Emp_Emailaddress"].ToString());
					}
				}
				DataTable dtFile = spm.Get_Retention_ID("MyRetention_EMP_File", Convert.ToString(Session["Empcode"]).Trim(), RetentionID);
				if (dtFile.Rows.Count > 0)
				{
					multiplefilenameadd = "";
					foreach (DataRow row in dtFile.Rows)
					{
						multiplefilenameadd += Convert.ToString(row["FileNames"].ToString()) + ",";
					}
				}
				string strOfferApprovalURL = "";
				strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Retention_Approval"]).Trim() + "?RetentionID=" + RetentionID + "&Type=APP";
				strbuild.Append("<table border='1' style='width:95%;'>");
				strbuild.Append("<tr><td style='width:30%;'>Employee Code: </td><td style='width:70%;'>" + Convert.ToString(txtEmpCode.Text)
					 + "</td></tr><tr><td style='width:30%;'>Employee Name: </td><td style='width:70%;'>" + Convert.ToString(DDLEmpName.SelectedItem.Text)
					 + "</td></tr><tr><td style='width:30%;'>Location: </td><td style='width:70%;'>" + Convert.ToString(Txt_ProjectCode.Text)
					 + "</td></tr><tr><td style='width:30%;'>Department: </td><td style='width:70%;'>" + Convert.ToString(Txt_Department.Text)
					 + "</td></tr><tr><td style='width:30%;'>Designation: </td><td style='width:70%;'>" + Convert.ToString(txt_Designation.Text)
					 + "</td></tr><tr><td style='width:30%;'>Date of Joining: </td><td style='width:70%;'>" + Convert.ToString(txt_DOJ.Text)
					 + "</td></tr><tr><td style='width:30%;'>Retention Type: </td><td style='width:70%;'>" + Convert.ToString(ddlRetentionType.SelectedItem.Text)
					 + "</td></tr><tr><td style='width:30%;'>With Effect From: </td><td style='width:70%;'>" + Convert.ToString(txt_WithEffectFrom.Text)
					 + "</td></tr><tr><td style='width:30%;'>Retention From Date: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionFromDate.Text)
					  + "</td></tr><tr><td style='width:30%;'>Retention Till Date: </td><td style='width:70%;'>" + Convert.ToString(txtRetentionTillDate.Text)
					 + "</td></tr><tr><td style='width:30%;'>Remark: </td><td style='width:70%;'>" + Convert.ToString(txtRemark.Text)
					 + "</td></tr>");
				strbuild.Append("</table>");
				MailAttachment = Server.MapPath(ConfigurationManager.AppSettings["RetentionPath"]);
				multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
				string LoginName = Convert.ToString(Session["emp_loginName"]);
				string Subject = "OneHR - " + DDLEmpName.SelectedItem.Text + " is retained by " + LoginName;
				spm.SendMail_Retaination_Moderation(hdnModerationEmail.Value, hdnModerationName.Value, Subject, DDLEmpName.SelectedItem.Text, LoginName, Convert.ToString(strbuild), cc_email, strOfferApprovalURL, MailAttachment, multiplefilenameadd);
				Response.Redirect("~/procs/MyRetentionInbox.aspx");
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
	protected void DDLEmpName_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (DDLEmpName.SelectedIndex > 0)
		{
			Get_Employee_Details(DDLEmpName.SelectedValue);
		}
		else
		{
			ClearValues();
		}
	} 
	#endregion
}