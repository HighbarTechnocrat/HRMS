using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;


public partial class ResignationForm : System.Web.UI.Page
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
	public DataTable DtToMail, DtCCMail;
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
					string strfilepath = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ResigAttachmentPath"]).Trim());
					FilePath.Value = strfilepath;
					editform.Visible = true;
					//var getDate = DateTime.Now.ToString("dd/MM/yyyy");
					// txtFromdate_N.Text = getDate;
					Retention_Employee_Check();// Retention Employee Check 
					ResignationCheck();// HR or Admin Employee Add entry of resignation check
					#region Comment by Sanjay on 07.06.2022 due get LWD from server
					/*  DataTable Dt = spm.GetEmploymentType(hdnEmpCode.Value);
					 string EmpType = "";
					 DateTime LWDay = new DateTime();
					 if (Dt.Rows.Count > 0)
					 {
						 EmpType = Dt.Rows[0]["EMPLOYMENT_TYPE"].ToString();
						 hdnEmpType.Value = EmpType;
						 if (EmpType == "2")
						 {
							 LWDay = DateTime.Now.AddDays(29);
							 txtNoticePeriod.Text = "30(day)s";
						 }
						 else
						 {
							 LWDay = DateTime.Now.AddDays(89);
							 txtNoticePeriod.Text = "90(day)s";
						 }
					 }
					 txtTodate_N.Text = LWDay.ToString("dd/MM/yyyy");
					 */
					#endregion
					txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtEmpComment.Attributes.Add("maxlength", "200");
					Fill_ResignationReason();
					if (Request.QueryString.Count > 0)
					{
						hdnId.Value = Convert.ToString(Request.QueryString[0]);
						Id = Convert.ToInt32(hdnId.Value);
						hdnResigId.Value = Convert.ToString(Request.QueryString[0]);
						GetResignationDetails(Id);

						if (ResigStatus.Value == "Pending")
						{
							var getDate1 = DateTime.Now.ToString("dd/MM/yyyy");
							//txtFromdate_N.Text = getDate1; Commented by Sada @ 2-May-2022
							DateTime LWDay1 = new DateTime();
							if (hdnEmpType.Value == "2")
							{
								LWDay1 = DateTime.Now.AddDays(29);
							}
							else
							{
								LWDay1 = DateTime.Now.AddDays(89);
							}
							//txtTodate_N.Text = LWDay1.ToString("dd/MM/yyyy"); Commented by Sada @ 2-May-2022
							txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
							txtEmpComment.Attributes.Add("maxlength", "200");
							txtEmpComment.Enabled = true;
							ddlResignationReason.Enabled = true;
							claimmob_btnSubmit.Visible = true;
						}
						if (ResigStatus.Value == "Retain")
						{
							mobile_btnSave.Visible = false;
							claimmob_btnSubmit.Visible = false;
						}
						if (ResigStatus.Value == "Rejected")
						{
							mobile_btnSave.Visible = false;
							claimmob_btnSubmit.Visible = false;
						}
						if (ResigStatus.Value == "Approved" || ResigStatus.Value == "Resigned")
						{
							var getDate1 = DateTime.Now.ToString("dd/MM/yyyy");
							//txtFromdate_N.Text = getDate1; Commented by Sada @ 2-May-2022
							DateTime LWDay1 = new DateTime();
							if (hdnEmpType.Value == "2")
							{
								LWDay1 = DateTime.Now.AddDays(29);
							}
							else
							{
								LWDay1 = DateTime.Now.AddDays(89);
							}
							//txtTodate_N.Text = LWDay1.ToString("dd/MM/yyyy"); Commented by Sada @ 2-May-2022
							txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
							txtEmpComment.Attributes.Add("maxlength", "200");
							txtEmpComment.Enabled = false;
							ddlResignationReason.Enabled = false;
							claimmob_btnSubmit.Visible = false;
							mobile_btnSave.Visible = false;
						}
					}
					else
					{
						Fill_ResignationReason();
						MyLatestResignation();
						claimmob_btnSubmit.Visible = false;
						/*DateTime LWDay1 = new DateTime();
                        if (hdnEmpType.Value == "2")
                        {
                            LWDay1 = DateTime.Now.AddDays(29);
                        }
                        else
                        {
                            LWDay1 = DateTime.Now.AddDays(89);
                        }
                        var getDate1 = DateTime.Now.ToString("dd/MM/yyyy");
                        txtFromdate_N.Text = getDate1;
                        txtTodate_N.Text = LWDay1.ToString("dd/MM/yyyy");
                        */
						if (ResigStatus.Value == "Pending")
						{
							txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
							txtEmpComment.Attributes.Add("maxlength", "200");
							txtEmpComment.Enabled = true;
							ddlResignationReason.Enabled = true;
							claimmob_btnSubmit.Visible = true;
						}

						if (ResigStatus.Value == "Retain")
						{
							txtFromdate_N.Enabled = false;
							txtTodate_N.Enabled = false;
							txtEmpComment.Enabled = true;
							txtEmpComment.Text = "";
							ddlResignationReason.Enabled = true;
							lblmessage.Text = "";
							mobile_btnSave.Visible = true;
							mobile_btnSave.Enabled = true;
							claimmob_btnSubmit.Visible = true;
							claimmob_btnSubmit.Visible = false;
						}

						if (ResigStatus.Value == "Approved" || ResigStatus.Value == "Resigned")
						{
							txtFromdate_N.Attributes.Add("onkeypress", "return noanyCharecters(event);");
							txtEmpComment.Attributes.Add("maxlength", "200");
							txtEmpComment.Enabled = false;
							ddlResignationReason.Enabled = false;
							claimmob_btnSubmit.Visible = false;
							mobile_btnSave.Visible = false;

						}
						if (ResigStatus.Value == "Rejected")
						{
							txtFromdate_N.Enabled = false;
							txtTodate_N.Enabled = false;
							txtEmpComment.Enabled = true;
							txtEmpComment.Text = "";
							ddlResignationReason.Enabled = true;
							lblmessage.Text = "";
							mobile_btnSave.Visible = true;
							claimmob_btnSubmit.Visible = true;
							mobile_btnSave.Enabled = true;
							claimmob_btnSubmit.Visible = false;
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
	private void MyResignation()
	{
		try
		{
			DataTable dtleaveInbox = new DataTable();
			dtleaveInbox = spm.GetMyResignations(hdnEmpCode.Value);

			if (dtleaveInbox.Rows.Count > 0)
			{
				ResigStatus.Value = Convert.ToString(dtleaveInbox.Rows[0]["Action"].ToString());
				hdnIsResigSubmitted.Value = "Submitted";
				lblmessage.Text = "You have already submitted resignation. Status- " + ResigStatus.Value;
				mobile_btnSave.Enabled = false;
				claimmob_btnSubmit.Enabled = false;
				hdnResigId.Value = Convert.ToString(dtleaveInbox.Rows[0]["ResignationID"].ToString());
				txtFromdate_N.Text = Convert.ToString(dtleaveInbox.Rows[0]["ResignationDate"].ToString());
				txtTodate_N.Text = Convert.ToString(dtleaveInbox.Rows[0]["LastWorkingDate"].ToString());
				txtEmpComment.Text = Convert.ToString(dtleaveInbox.Rows[0]["Remarks"].ToString());
				ddlResignationReason.SelectedValue = dtleaveInbox.Rows[0]["ReasonID"].ToString();
				lnkuplodedfile.Text = Convert.ToString(dtleaveInbox.Rows[0]["FileName"]).Trim();
				hdn_Attchment.Value = Convert.ToString(dtleaveInbox.Rows[0]["FileName"]).Trim();
				txtFromdate_N.Enabled = false;
				txtTodate_N.Enabled = false;
				txtEmpComment.Enabled = false;
				ddlResignationReason.Enabled = false;
				if (ResigStatus.Value == "Pending")
				{
					mobile_btnSave.Visible = false;
					claimmob_btnSubmit.Enabled = true;
				}
			}
			else
			{
				hdnIsResigSubmitted.Value = "";
				lblmessage.Text = "";
				mobile_btnSave.Enabled = true;
				claimmob_btnSubmit.Enabled = true;
			}
		}
		catch (Exception ex)
		{

		}
	}
	private void MyLatestResignation()
	{
		try
		{

			DataTable dtleaveInbox = new DataTable();
			dtleaveInbox = spm.GetMyLatestResignation(hdnEmpCode.Value);

			if (dtleaveInbox.Rows.Count > 0)
			{
				ResigStatus.Value = Convert.ToString(dtleaveInbox.Rows[0]["Action"].ToString());
				hdnIsResigSubmitted.Value = "Submitted";
				lblmessage.Text = "You have already submitted resignation. Status- " + ResigStatus.Value;
				mobile_btnSave.Enabled = false;
				claimmob_btnSubmit.Enabled = false;
				hdnResigId.Value = Convert.ToString(dtleaveInbox.Rows[0]["ResignationID"].ToString());
				txtFromdate_N.Text = Convert.ToString(dtleaveInbox.Rows[0]["ResignationDate"].ToString());
				txtTodate_N.Text = Convert.ToString(dtleaveInbox.Rows[0]["LastWorkingDate"].ToString());
				txtEmpComment.Text = Convert.ToString(dtleaveInbox.Rows[0]["Remarks"].ToString());
				ddlResignationReason.SelectedValue = dtleaveInbox.Rows[0]["ReasonID"].ToString();
				lnkuplodedfile.Text = Convert.ToString(dtleaveInbox.Rows[0]["FileName"]).Trim();
				hdn_Attchment.Value = Convert.ToString(dtleaveInbox.Rows[0]["FileName"]).Trim();
				txtFromdate_N.Enabled = false;
				txtTodate_N.Enabled = false;
				txtEmpComment.Enabled = false;
				ddlResignationReason.Enabled = false;
				if (ResigStatus.Value == "Pending")
				{
					mobile_btnSave.Visible = false;
					claimmob_btnSubmit.Enabled = true;
				}
			}
			else
			{
				//hdnIsResigSubmitted.Value = "";
				//lblmessage.Text = "";
				//mobile_btnSave.Enabled = true;
				//claimmob_btnSubmit.Enabled = true;
			}
		}
		catch (Exception ex)
		{

		}
	}
	public void Fill_ResignationReason()
	{
		DataTable Dt = spm.GetResignationReason();
		ddlResignationReason.DataSource = Dt;

		ddlResignationReason.DataTextField = "Particulars";
		ddlResignationReason.DataValueField = "pid";
		ddlResignationReason.DataBind();
		ListItem item = new ListItem("Select Resignation Reason", "0");
		ddlResignationReason.Items.Insert(0, item);
	}

	public void GetResignationDetails(int Id)
	{
		try
		{
			DataTable dtleaveInbox = new DataTable();
			dtleaveInbox = spm.GetResignationDetailsByResigId(Id);
			if (dtleaveInbox.Rows.Count > 0)
			{
				ResigStatus.Value = Convert.ToString(dtleaveInbox.Rows[0]["Action"].ToString());
				hdnIsResigSubmitted.Value = "Submitted";
				lblmessage.Text = "You have already submitted resignation. Status- " + ResigStatus.Value;
				mobile_btnSave.Enabled = false;
				claimmob_btnSubmit.Enabled = false;

				txtFromdate_N.Text = Convert.ToString(dtleaveInbox.Rows[0]["ResignationDate"].ToString());
				txtTodate_N.Text = Convert.ToString(dtleaveInbox.Rows[0]["LastWorkingDate"].ToString());
				txtEmpComment.Text = Convert.ToString(dtleaveInbox.Rows[0]["Remarks"].ToString());
				ddlResignationReason.SelectedValue = dtleaveInbox.Rows[0]["ReasonID"].ToString();
				lnkuplodedfile.Text = Convert.ToString(dtleaveInbox.Rows[0]["FileName"]).Trim();
				hdn_Attchment.Value = Convert.ToString(dtleaveInbox.Rows[0]["FileName"]).Trim();
				ResigStatus.Value = Convert.ToString(dtleaveInbox.Rows[0]["Action"].ToString());

				txtFromdate_N.Enabled = false;
				txtTodate_N.Enabled = false;
				txtEmpComment.Enabled = false;
				ddlResignationReason.Enabled = false;
				if (ResigStatus.Value == "Pending")
				{
					mobile_btnSave.Visible = false;
					claimmob_btnSubmit.Enabled = true;
				}

				if (ResigStatus.Value == "Approved")
				{
					mobile_btnSave.Visible = false;
					claimmob_btnSubmit.Visible = false;
				}
			}
		}
		catch (Exception ex)
		{

		}
	}


	#endregion
	protected void claimmob_btnSubmit_Click(object sender, EventArgs e)
	{   //Retain
		int StatusID = 6;
		lblmessage.Text = "";
		int ResignationID = Convert.ToInt32(hdnResigId.Value);
		//int ResignationID = Convert.ToInt32(hdnId.Value);
		string filename = "";
		string strfileName = "";

		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}

		//update Attachment
		if (uploadfile.HasFile)
		{
			filename = uploadfile.FileName;
		}

		if (Convert.ToString(filename).Trim() != "")
		{
			filename = uploadfile.FileName;
			strfileName = "";
			strfileName = hdnEmpCode.Value + "_" + uploadfile.FileName;
			filename = strfileName;
			uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ResigAttachmentPath"]).Trim()), strfileName));
		}

		int apprid = 0;
		string Approvers_code = "";
		string approveremailaddress = "";
		DataTable dtApproverEmailIds = spm.GetExitProcApproverDetails(hdnEmpCode.Value);
		if (dtApproverEmailIds.Rows.Count > 0)
		{
			approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
			apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
			Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
			hflapprcode.Value = Approvers_code;
		}
		foreach (DataRow row in dtApproverEmailIds.Rows)
		{
			hdnApprEmailaddress.Value = hdnApprEmailaddress.Value + Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
		}

		spm.ExitProcRetainResignation("RetainResignation", ResignationID, Convert.ToInt32(ddlResignationReason.SelectedValue.ToString()), txtEmpComment.Text.ToString(), StatusID, filename, Approvers_code, apprid);
		DtCCMail = new DataTable();
		DtToMail = new DataTable();

		//Send Mail-To extra 3 employee
		string ExtraMail = "";
		ExtraMail = approveremailaddress;
		SqlParameter[] spars13 = new SqlParameter[2];
		spars13[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars13[0].Value = "ResignMail_Approver_Extra";
		spars13[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		spars13[1].Value = hdnEmpCode.Value.ToString();
		DtToMail = spm.getData_FromCode(spars13, "SP_Admin_Employee_Exit");
		if (DtToMail.Rows.Count > 0)
		{
			foreach (DataRow row in DtToMail.Rows)
			{
				if (ExtraMail != Convert.ToString(row["email"].ToString()))
				{
					approveremailaddress = approveremailaddress + ";" + Convert.ToString(row["email"].ToString());
				}
			}
		}
		//Send Mail-To all approver
		//Get CC mail-ids
		string cc_email = "";
		SqlParameter[] spars1 = new SqlParameter[2];
		spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars1[0].Value = "ResignMail_ApproverDetails_cc";

		spars1[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		spars1[1].Value = hdnEmpCode.Value.ToString();

		DtCCMail = spm.getData_FromCode(spars1, "SP_Admin_Employee_Exit");
		if (DtCCMail.Rows.Count > 0)
		{
			cc_email = "";
			foreach (DataRow rows in DtCCMail.Rows)
			{   //Dublicate values Remove CC mail 
				var Result = from row in DtToMail.AsEnumerable()
							 where row.Field<string>("email") == Convert.ToString(rows["email"].ToString())
							 select row;
				int countCCMail = Result.Count<DataRow>();
				if (countCCMail == 0)
				{
					if (ExtraMail != Convert.ToString(rows["email"].ToString()))
					{
						cc_email = cc_email + Convert.ToString(rows["email"].ToString()) + ";";
					}
				}
			}
		}
		//Get To mail-ids
		string emp_name = "";
		StringBuilder strbuild = new StringBuilder();
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars[0].Value = "ResignationMail_EmployeeDetails";

		spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
		spars[1].Value = hdnEmpCode.Value.ToString();

		DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");
		string separationStatus = "";
		string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ITAssetService_RepairReplaceReqApp.aspx";
		string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?id=" + Convert.ToInt32(ResignationID) + "&type=app";
		if (tt.Rows.Count > 0)
		{
			strbuild.Length = 0;
			strbuild.Clear();

			strbuild.Append("<table border='1'>");

			foreach (DataRow row in tt.Rows)
			{
				if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
				{
					strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
					 + "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
					 + "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
					 + "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
					 + "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
					 + "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
					 + "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
					 + "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
					 + "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
					 + "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
					 + "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
					 + "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
					 + "</td></tr><tr><td>Separation Status: </td><td>" + separationStatus
					 + "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
					 //+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
					 + "</td></tr>"
					 );
				}
				else
				{
					strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
					+ "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
					+ "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
					+ "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
					+ "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
					+ "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
					+ "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
					+ "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
					+ "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
					+ "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
					+ "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
					+ "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
					+ "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
					+ "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
					+ "</td></tr><tr><td>Separation Status: </td><td>" + separationStatus
					+ "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
							// + "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
							+ "</td></tr>");

				}
				emp_name = Convert.ToString(row["Emp_Name"].ToString());
			}
			strbuild.Append("</table>");
		}
		string strsubject = emp_name + " retained";
		spm.SendMailOnResignationRetain(approveremailaddress, strsubject, emp_name, Convert.ToString(strbuild), cc_email);

		lblmessage.Visible = true;
		lblmessage.Text = "Resignation Retain Reuqest Submitted Successfully";
		Response.Redirect("~/procs/ExitProcess_Index.aspx");

	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}


		//Submit
		int StatusID = 1;
		string[] strdate;
		string strfromDateN = "";
		string strtoDateN = "";
		string filename = "";
		string strfileName = "";
		lblmessage.Text = "";

		ResignationCheck();//admin  or HR submit regs.. employee  entry check
		if (ResigStatus.Value == "Resigned")
		{
			return;
		}
		#region Validation
		if (Convert.ToInt32(ddlResignationReason.SelectedValue) == 0)
		{
			lblmessage.Text = "Please select resignation reason";
			return;
		}

		if (Convert.ToString(txtEmpComment.Text).Trim() == "")
		{
			lblmessage.Text = "Please enter employee comment";
			return;
		}
		#endregion




		if (uploadfile.HasFile)
		{
			filename = uploadfile.FileName;
		}

		//if (Convert.ToString(lnkuplodedfile.Text).Trim() == "")
		//{
		//    if (Convert.ToString(filename).Trim() == "")
		//    {
		//        lblmessage.Text = "Please upload File!";
		//        return;
		//    }
		//}

		if (Convert.ToString(filename).Trim() != "")
		{
			filename = uploadfile.FileName;
			strfileName = "";
			strfileName = hdnEmpCode.Value + "_" + uploadfile.FileName;
			filename = strfileName;
			uploadfile.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ResigAttachmentPath"]).Trim()), strfileName));
		}
		if (Convert.ToString(txtFromdate_N.Text).Trim() != "")
		{
			strdate = Convert.ToString(txtFromdate_N.Text).Trim().Split('/');
			strfromDateN = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);


		}
		if (Convert.ToString(txtTodate_N.Text).Trim() != "")
		{
			strdate = Convert.ToString(txtTodate_N.Text).Trim().Split('/');
			strtoDateN = Convert.ToString(strdate[2]) + "/" + Convert.ToString(strdate[1]) + "/" + Convert.ToString(strdate[0]);
		}

		//Get All Approver Details-
		int apprid = 0;
		string Approvers_code = "";
		string approveremailaddress = "";
		DataTable dtApproverEmailIds = spm.GetExitProcApproverDetails(hdnEmpCode.Value);
		if (dtApproverEmailIds.Rows.Count > 0)
		{
			approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
			apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
			Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
			hflapprcode.Value = Approvers_code;
		}
		foreach (DataRow row in dtApproverEmailIds.Rows)
		{
			hdnApprEmailaddress.Value = hdnApprEmailaddress.Value + Convert.ToString(row["Emp_Emailaddress"].ToString()) + ";";
		}

		var ResignationId = spm.ExitProcSubmitResignation("SubmitResignation", hdnEmpCode.Value, strfromDateN.ToString(), strtoDateN.ToString(), Convert.ToInt32(ddlResignationReason.SelectedValue.ToString()), txtEmpComment.Text.ToString(), StatusID, filename, Approvers_code, apprid);

		//Get To mail-ids
		string emp_name = "";
		StringBuilder strbuild = new StringBuilder();
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars[0].Value = "ResignationMail_EmployeeDetails";

		spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
		spars[1].Value = hdnEmpCode.Value.ToString();

		DataTable tt = spm.getData_FromCode(spars, "SP_ExitProcess");

		string ResigForm_AppLink = "http://localhost/hrms/login.aspx?ReturnUrl=procs/ResignationForm_App.aspx";
		string redirectURL = Convert.ToString(ResigForm_AppLink).Trim() + "?id=" + Convert.ToInt32(ResignationId);

		if (tt.Rows.Count > 0)
		{
			strbuild.Length = 0;
			strbuild.Clear();

			strbuild.Append("<table border='1'>");

			foreach (DataRow row in tt.Rows)
			{
				if (Convert.ToString(row["emp_location"].ToString()) == "HO-NaviMum")
				{

					strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
					+ "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
					+ "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
					+ "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
					+ "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
					+ "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
					+ "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
					+ "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
					+ "</td></tr><tr><td>Reporting Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
					+ "</td></tr><tr><td>HOD: </td><td>" + Convert.ToString(row["DH"].ToString())
					+ "</td></tr><tr><td>Date Of Joining: </td><td>" + Convert.ToString(row["emp_doj"].ToString())
					+ "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
					+ "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
					+ "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
					+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
					+ "</td></tr>");
				}
				else
				{
					strbuild.Append("<tr><td>Employee Code: </td><td>" + Convert.ToString(row["Emp_Code"].ToString())
					+ "</td></tr><tr><td>Employee Name: </td><td>" + Convert.ToString(row["Emp_Name"].ToString())
					+ "</td></tr><tr><td>Employment Type: </td><td>" + Convert.ToString(row["Particulars"].ToString())
					+ "</td></tr><tr><td>Email Id: </td><td>" + Convert.ToString(row["Emp_Emailaddress"].ToString())
					+ "</td></tr><tr><td>Mobile no.: </td><td>" + Convert.ToString(row["mobile"].ToString())
					+ "</td></tr><tr><td>Location: </td><td>" + Convert.ToString(row["emp_location"].ToString())
					+ "</td></tr><tr><td>Department: </td><td>" + Convert.ToString(row["Department_Name"].ToString())
					+ "</td></tr><tr><td>Designation: </td><td>" + Convert.ToString(row["DesginationName"].ToString())
					+ "</td></tr><tr><td>Project Manager: </td><td>" + Convert.ToString(row["RM"].ToString())
					+ "</td></tr><tr><td>Delivery Manager: </td><td>" + Convert.ToString(row["DM"].ToString())
					+ "</td></tr><tr><td>Program Manager: </td><td>" + Convert.ToString(row["PRM"].ToString())
					+ "</td></tr><tr><td>Delivery Head: </td><td>" + Convert.ToString(row["DH"].ToString())
					+ "</td></tr><tr><td>Date Of Joining: </td><td>" + Convert.ToString(row["emp_doj"].ToString())
					+ "</td></tr><tr><td>Resignation Date: </td><td>" + Convert.ToString(row["ResignationDate"].ToString())
					+ "</td></tr><tr><td>Resignation Reason: </td><td>" + Convert.ToString(row["ResignationReason"].ToString())
					+ "</td></tr><tr><td>Last Working Date as policy: </td><td>" + Convert.ToString(row["LastWorkingDate"].ToString())
					+ "</td></tr><tr><td>Release Date: </td><td>" + Convert.ToString(row["HrReleaseDate"].ToString())
					+ "</td></tr>");
				}
				emp_name = Convert.ToString(row["Emp_Name"].ToString());
			}
			strbuild.Append("</table>");
		}
		string strsubject = emp_name + " resigned";
		spm.SendMailOnResignation(approveremailaddress, strsubject, emp_name, Convert.ToString(strbuild), "", redirectURL);


		//Send Mail to others as per ExitAdd page
		DtToMail = new DataTable();
		DtCCMail = new DataTable();

		spars = new SqlParameter[2];
		spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars[0].Value = "ResignMail_ApproverDetails";
		spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		spars[1].Value = hdnEmpCode.Value.ToString();

		DtToMail = spm.getData_FromCode(spars, "SP_Admin_Employee_Exit");
		string to_email = "";
		string cc_email = "";
		if (DtToMail.Rows.Count > 0)
		{
			foreach (DataRow row in DtToMail.Rows)
			{
				to_email = to_email + Convert.ToString(row["email"].ToString()) + ";";
			}
		}

		spars = new SqlParameter[2];
		spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
		spars[0].Value = "ResignMail_ApproverDetails_cc";
		spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		spars[1].Value = hdnEmpCode.Value.ToString();

		DtCCMail = spm.getData_FromCode(spars, "SP_Admin_Employee_Exit");
		if (DtCCMail.Rows.Count > 0)
		{
			foreach (DataRow rows in DtCCMail.Rows)
			{   //Dublicate values Remove CC mail 
				var Result = from row in DtToMail.AsEnumerable()
							 where row.Field<string>("email") == Convert.ToString(rows["email"].ToString())
							 select row;
				int countCCMail = Result.Count<DataRow>();
				if (countCCMail == 0)
				{
					cc_email = cc_email + Convert.ToString(rows["email"].ToString()) + ";";
				}
			}
		}

		spm.send_mail_Employee_resigned(emp_name, to_email, emp_name + " resigned", Convert.ToString(strbuild), "", cc_email);

		lblmessage.Visible = true;
		lblmessage.Text = "Resignation Reuqest Submitted Successfully";
		Response.Redirect("~/procs/ExitProcess_Index.aspx");

	}


	protected void mobile_cancel_Click(object sender, EventArgs e)
	{   //Cancel
		Response.Redirect("~/procs/ExitProcess_Index.aspx");
	}

	protected void txtFromdate_N_TextChanged(object sender, EventArgs e)
	{
		try
		{
			Int64 addedDays = 0;
			DateTime endDate = DateTime.ParseExact(txtFromdate_N.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
			if (hdnEmpType.Value == "2")
			{
				addedDays = 29;
			}
			else
			{
				addedDays = 89;
			}
			endDate = endDate.AddDays(addedDays);
			DateTime end = endDate;
			txtTodate_N.Text = end.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());

		}
	}

	protected void lnkuplodedfile_Click(object sender, EventArgs e)
	{
		try
		{
			String strfilepath = Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["ResigAttachmentPath"]).Trim()), lnkuplodedfile.Text.Trim());

			Response.ContentType = ContentType;
			Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(strfilepath));
			Response.WriteFile(strfilepath);
			Response.End();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	private void ResignationCheck()
	{
		try
		{
			SqlParameter[] spars1 = new SqlParameter[2];
			spars1[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars1[0].Value = "ResignationCheck";
			spars1[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars1[1].Value = hdnEmpCode.Value.ToString();

			//DataTable Result = spm.getData_FromCode(spars1, "SP_ExitProcess");
			DataSet dsResult = spm.getDatasetList(spars1, "SP_ExitProcess");

			if (dsResult != null)
			{
				if (dsResult.Tables[0].Rows.Count > 0)
				{
					ResigStatus.Value = "Resigned";//Convert.ToString(Result.Rows[0]["Action"].ToString());
					hdnIsResigSubmitted.Value = "Submitted";
					lblmessage.Text = "You have already submitted resignation. Status -  Resigned";// + ResigStatus.Value;
					mobile_btnSave.Visible = false;
					claimmob_btnSubmit.Visible = false;
					txtFromdate_N.Enabled = false;
					txtTodate_N.Enabled = false;
					txtEmpComment.Enabled = false;
					ddlResignationReason.Enabled = false;
					ddlResignationReason.SelectedIndex = -1;
					txtEmpComment.Text = "";
				}
				if (dsResult.Tables[1].Rows.Count > 0)
				{
					hdnEmpType.Value = Convert.ToString(dsResult.Tables[1].Rows[0]["EMPLOYMENT_TYPE"]).Trim();
					txtNoticePeriod.Text = Convert.ToString(dsResult.Tables[1].Rows[0]["LWDay_days"]).Trim();
					txtTodate_N.Text = Convert.ToString(dsResult.Tables[1].Rows[0]["LWDay"]).Trim();
					txtFromdate_N.Text = Convert.ToString(dsResult.Tables[1].Rows[0]["Resignation_dt"]).Trim();
				}
			}

		}
		catch (Exception)
		{

			throw;
		}
	}
	private void Retention_Employee_Check()
	{
		try
		{
			SqlParameter[] spars1 = new SqlParameter[2];
			spars1[0] = new SqlParameter("@Qtype", SqlDbType.VarChar);
			spars1[0].Value = "Resigned_Employee_Check";
			spars1[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars1[1].Value = hdnEmpCode.Value.ToString();
			DataSet dsResult = spm.getDatasetList(spars1, "SP_Employee_Retention_Details");
			if (dsResult != null)
			{
				string Result="";
				if (dsResult.Tables[0].Rows.Count > 0)
				{
					Result = dsResult.Tables[0].Rows[0]["ToDate"].ToString();
					lblRetention.Text = "This is to bring it to your notice, that you have agreed Mandatory Bond/Retention Upto Date - <b>" + Result + ".</b> </br>Please review your retention agreements during your previous resignation and then continue.";
					mobile_btnSave.Visible = false;
					//claimmob_btnSubmit.Visible = false;
				}
				
			}

		}
		catch (Exception)
		{

			throw;
		}
	}
}