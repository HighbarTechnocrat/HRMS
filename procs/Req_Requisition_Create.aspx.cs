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

public partial class Req_Requisition_Create : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public string loc = "", dept = "", subdept = "", desg = "";
	public int did = 0;
	public DataTable dtEmp, dtRecruitment, dtApproverEmailIds, ReqdtStatus, dtCancelRec, dtEmpBand, dtInterviewer, dtfilter;
	public string filename = "", approveremailaddress, message, Approvers_code;
	string strFilter = "";
	public int apprid;
	SP_Methods spm = new SP_Methods();
	Recruitment_Requisition_Parameter Req = new Recruitment_Requisition_Parameter();
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}

	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	#endregion
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			hdnEmpCpde.Value = Session["Empcode"].ToString();

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
			}
			else
			{

				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{

					txtNoofPosition.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
					//txtSalaryRangeFrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					// txtSalaryRangeTo.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					txttofilledIn.Attributes.Add("onkeypress", "return onOnlyNumber(event);");
					//txtRequiredExperiencefrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//txtRequiredExperienceto.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");                    

					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
					PopulateEmployeeData();
					GetRequisitionNo();
					GetSkillsetName();
					GetPositionName();
					GetPositionCriticality();
					GetDepartmentMaster();
					GetCompany_Location_Prosepect();
					GetReasonRequisition();
					GetPositionDesign();
					GetPreferredEmpType();
					GetlstPositionBand();
					GetInterviewer(0);


					// trvldeatils_delete_btn.Visible = false;
					if (Request.QueryString.Count > 0)
					{
						hdnRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						localtrvl_btnSave.Visible = false;
						trvldeatils_btnSave.Visible = false;
						accmo_cancel_btn.Visible = false;
						localtrvl_delete_btn.Visible = false;
						GetecruitmentDetail();
						setenablefalseConttols();
						//accmo_delete_btn.Visible = true;
						lblheading.Text = "View Recruitment Requisition ";
						//getApproverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value);
						btnRecBack.Visible = true;
					}


				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}

	}
	private void setenablefalseConttols()
	{
		if (HDNISDraft.Value != "1" && hflStatusID.Value != "5")
		//if (Convert.ToString(hdnLeaveStatus.Value).Trim() != "1" && Convert.ToString(hdnLeaveStatus.Value).Trim() != "5")
		{
			lstPositionName.Enabled = false;
			//  lstPositionCriti.Enabled = false;
			lstSkillset.Enabled = false;
			lstPositionDept.Enabled = false;
			lstPositionDesign.Enabled = false;
			lstPositionLoca.Enabled = false;
			txtOtherDept.Enabled = false;
			txtPositionDesig.Enabled = false;

			txtNoofPosition.Enabled = false;
			txtAdditionSkill.Enabled = false;
			txttofilledIn.Enabled = false;
			//txtSalaryRangeFrom.Enabled = false;
			//txtSalaryRangeTo.Enabled = false;
			lstReasonForRequi.Enabled = false;

			lstPreferredEmpType.Enabled = false;
			lstPositionBand.Enabled = false;
			//trvl_localbtn.Enabled = false;
			txtEssentialQualifi.Enabled = false;
			txtDesiredQualifi.Enabled = false;

			txtRequiredExperiencefrom.Enabled = false;
			txtRequiredExperienceto.Enabled = false;
			lstRecommPerson.Enabled = false;
			txtJobDescription.Enabled = false;
			localtrvl_delete_btn.Enabled = false;
			accmo_cancel_btn.Enabled = false;
			localtrvl_delete_btn.Visible = false;
			accmo_cancel_btn.Visible = false;

			//txtComments.Enabled = false;
			lstInterviewerTwo.Enabled = false;
			lstInterviewerOne.Enabled = false;
			txtInterviewerOptOne.Enabled = false;
			txtInterviewerOptTwo.Enabled = false;
			//Recruiter.Visible = true;
			trvldeatils_btnSave.Visible = false;
			//trvldeatils_delete_btn.Visible = false;
			lnkuplodedfile.Style.Add("padding-bottom", "15px");
		}

	}
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{

			int Quest_ID = 0;
			DataTable QuestionnaireList = new DataTable();
			string Stype = "FilterQuestionnaire";
			#region Check For Blank Fields
			if (Convert.ToString(lstSkillsetQue.SelectedValue).Trim() == "" || Convert.ToString(lstSkillsetQue.SelectedValue).Trim() == "0")
			{
				Label2.Text = "Please select Skill Set";
				ModalPopupExtenderLogin.Show();
				return;

			}
			if (Convert.ToString(lstPositionQue.SelectedValue).Trim() == "" || Convert.ToString(lstPositionQue.SelectedValue).Trim() == "0")
			{
				Label2.Text = "Please select Position Title";
				ModalPopupExtenderLogin.Show();
				return;
			}
			//Label2.Visible = false;
			//string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 

			#endregion
			QuestionnaireList = spm.GetAssign_QuestionnaireFilter(Stype, Convert.ToInt32(lstSkillsetQue.SelectedValue), Convert.ToInt32(lstPositionQue.SelectedValue));
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (QuestionnaireList.Rows.Count > 0)
			{
				Label2.Text = "";
				gvMngTravelRqstList.DataSource = QuestionnaireList;
				gvMngTravelRqstList.DataBind();
				mobile_cancel.Visible = true;
			}
			else
			{
				mobile_cancel.Visible = false;
				Label2.Text = "No Questionnaires for the selected combination. Please contact Administrator.";
			}
			ModalPopupExtenderLogin.Show();
		}
		catch (Exception ex)
		{

		}

	}

	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		lnkuplodedfile.Text = "";
		#region Check For Blank Fields
		int Count = gvMngTravelRqstList.Rows.Count;
		if (Count == 0)
		{
			Label2.Text = "Please select data ";
			ModalPopupExtenderLogin.Show();
			return;
		}
		HiddenField txtPostId = new HiddenField();
		HiddenField txtQutId = new HiddenField();

		foreach (GridViewRow gvrow in gvMngTravelRqstList.Rows)
		{
			CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
			if (chk != null & !chk.Checked)
			{
				Label2.Text = "Please select check box";
				ModalPopupExtenderLogin.Show();
				return;
			}
			else
			{
				txtPostId = (HiddenField)gvrow.FindControl("hfId");
				txtQutId = (HiddenField)gvrow.FindControl("AssignQuestiID");
			}
		}
		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
														 //if (confirmValue != "Yes")
														 //{
														 //    return;
														 //}
		#endregion
		//lnkuplodedfile.Attributes.Add("OnClientClick", "DownloadFile("+ txtPostId.Value +");");
		hdnAssignQuestiID.Value = txtQutId.Value;
		FileName.Value = txtPostId.Value;
		lnkuplodedfile.Text = txtPostId.Value;
	}

	[System.Web.Services.WebMethod]
	public static List<string> SearchInterviewerOne(string prefixText, int count)
	{

		using (SqlConnection conn = new SqlConnection())
		{
			conn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

			using (SqlCommand cmd = new SqlCommand())
			{
				string strsql = "";

				strsql = "  Select t.empname from  ( " +
							   "  Select Emp_Code + '-'  +Emp_Name as empname " +
							   "  from tbl_Employee_Mst  " +
							   "   where emp_status='Onboard' " +
							   "    " +
							   " ) t " +
							   " where t.empname like '%' + @SearchText + '%'   Order by t.empname ";

				cmd.CommandText = strsql;
				cmd.Parameters.AddWithValue("@SearchText", Convert.ToString(prefixText).Trim());
				cmd.Parameters.AddWithValue("@empcode", Convert.ToString(HttpContext.Current.Session["Empcode"]));

				////                }


				cmd.Connection = conn;
				conn.Open();
				List<string> employees = new List<string>();
				using (SqlDataReader sdr = cmd.ExecuteReader())
				{
					while (sdr.Read())
					{
						employees.Add(sdr["empname"].ToString());
					}
				}
				conn.Close();
				return employees;
			}
		}
	}



	#region PageMethods
	public void PopulateEmployeeData()
	{
		try
		{
			dtEmp = spm.Get_Requisition_EmployeeHOD(Convert.ToString(Session["Empcode"]).Trim());
			dtEmpBand = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_EmployeeBand");
			hflStatusID.Value = "0";
			if (dtEmp.Rows.Count > 0)
			{
				hflEmpDepartment.Value = (string)dtEmp.Rows[0]["Department"].ToString().Trim();
				hflEmpDesignation.Value = (string)dtEmp.Rows[0]["Designation"].ToString().Trim();
				hflEmailAddress.Value = (string)dtEmp.Rows[0]["Emp_EmailAddress"];
				hdnHOD.Value = "HOD";

				if (hflEmpDepartment.Value.Trim() == "Human Resources")
				{
				}
			}
			else
			{
				hdnHOD.Value = "Employee";
			}

			if (dtEmpBand.Rows.Count == 0)
			{
				Response.Redirect("~/procs/Requisition_Index.aspx");
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();

			throw;
		}

	}
	public void GetSkillsetName()
	{
		DataTable dtSkillset = new DataTable();
		dtSkillset = spm.GetRecruitment_SkillsetName();
		if (dtSkillset.Rows.Count > 0)
		{
			lstSkillset.DataSource = dtSkillset;
			lstSkillset.DataTextField = "ModuleDesc";
			lstSkillset.DataValueField = "ModuleId";
			lstSkillset.DataBind();
			lstSkillset.Items.Insert(0, new ListItem("Select Skillset", "0"));

			lstSkillsetQue.DataSource = dtSkillset;
			lstSkillsetQue.DataTextField = "ModuleDesc";
			lstSkillsetQue.DataValueField = "ModuleId";
			lstSkillsetQue.DataBind();
			lstSkillsetQue.Items.Insert(0, new ListItem("Select Skillset", "0"));

			lstDGSkillset.DataSource = dtSkillset;
			lstDGSkillset.DataTextField = "ModuleDesc";
			lstDGSkillset.DataValueField = "ModuleId";
			lstDGSkillset.DataBind();
			lstDGSkillset.Items.Insert(0, new ListItem("Select Skillset", "0"));


		}
	}
	public void GetPositionName()
	{
		DataTable dtPositionName = new DataTable();
		dtPositionName = spm.GetRecruitment_PositionTitle();
		if (dtPositionName.Rows.Count > 0)
		{
			lstPositionName.DataSource = dtPositionName;
			lstPositionName.DataTextField = "PositionTitle";
			lstPositionName.DataValueField = "PositionTitle_ID";
			lstPositionName.DataBind();
			lstPositionName.Items.Insert(0, new ListItem("Select Position", "0"));

			lstPositionQue.DataSource = dtPositionName;
			lstPositionQue.DataTextField = "PositionTitle";
			lstPositionQue.DataValueField = "PositionTitle_ID";
			lstPositionQue.DataBind();
			lstPositionQue.Items.Insert(0, new ListItem("Select Position", "0"));

			lstDGPosition.DataSource = dtPositionName;
			lstDGPosition.DataTextField = "PositionTitle";
			lstDGPosition.DataValueField = "PositionTitle_ID";
			lstDGPosition.DataBind();
			lstDGPosition.Items.Insert(0, new ListItem("Select Position", "0"));
		}
	}
	public void GetPositionCriticality()
	{
		DataTable dtPositionCriti = new DataTable();
		dtPositionCriti = spm.GetRecruitment_Req_PositionCriticality();
		if (dtPositionCriti.Rows.Count > 0)
		{
			lstPositionCriti.DataSource = dtPositionCriti;
			lstPositionCriti.DataTextField = "PositionCriticality";
			lstPositionCriti.DataValueField = "PositionCriticality_ID";
			lstPositionCriti.DataBind();
			lstPositionCriti.Items.Insert(0, new ListItem("Select Criticality", "0"));
		}
	}
	public void GetDepartmentMaster()
	{
		DataTable dtPositionDept = new DataTable();
		dtPositionDept = spm.GetRecruitment_Req_DepartmentMaster();
		if (dtPositionDept.Rows.Count > 0)
		{
			lstPositionDept.DataSource = dtPositionDept;
			lstPositionDept.DataTextField = "Department_Name";
			lstPositionDept.DataValueField = "Department_id";
			lstPositionDept.DataBind();
			lstPositionDept.Items.Insert(0, new ListItem("Select Department", "0"));
			////lstPositionDept.Enabled = false;
			//////updated code
			////DataRow[] dr = dtPositionDept.Select("Department_Name = '" + txtReqDept.Text.ToString().Trim() + "'");
			////if (dr.Length > 0)
			////{
			////	string avalue = dr[0]["Department_id"].ToString();			
			////	lstPositionDept.SelectedValue = avalue;
			////}



		}
	}
	public void GetCompany_Location_Prosepect()
	{
		DataTable lstPosition = new DataTable();
		lstPosition = spm.GetRec_Req_Location_Prosepect();
		if (lstPosition.Rows.Count > 0)
		{
			lstPositionLoca.DataSource = lstPosition;
			lstPositionLoca.DataTextField = "Location_name";
			lstPositionLoca.DataValueField = "comp_code";
			lstPositionLoca.DataBind();
			lstPositionLoca.Items.Insert(0, new ListItem("Select Position Location", "0"));

		}
	}
	public void GetReasonRequisition()
	{
		DataTable lstReasonFor = new DataTable();
		lstReasonFor = spm.GetRecruitment_Req_ReasonRequisition();
		if (lstReasonFor.Rows.Count > 0)
		{
			lstReasonForRequi.DataSource = lstReasonFor;
			lstReasonForRequi.DataTextField = "ReasonRequisition";
			lstReasonForRequi.DataValueField = "ReasonRequisition_ID";
			lstReasonForRequi.DataBind();
			lstReasonForRequi.Items.Insert(0, new ListItem("Select Reason Requisition", "0"));
		}
	}
	public void GetPositionDesign()
	{
		DataTable dtPositionDesign = new DataTable();
		dtPositionDesign = spm.GetRecruitment_Req_DesignationMaster();
		if (dtPositionDesign.Rows.Count > 0)
		{
			lstPositionDesign.DataSource = dtPositionDesign;
			lstPositionDesign.DataTextField = "DesginationName";
			lstPositionDesign.DataValueField = "Designation_iD";
			lstPositionDesign.DataBind();
			lstPositionDesign.Items.Insert(0, new ListItem("Select Position Designation", "0"));
		}
	}
	public void GetPreferredEmpType()
	{
		DataTable dtPositionName = new DataTable();
		dtPositionName = spm.GetRecruitment_Req_HRMS_Employment_Type();
		if (dtPositionName.Rows.Count > 0)
		{
			lstPreferredEmpType.DataSource = dtPositionName;
			lstPreferredEmpType.DataTextField = "Particulars";
			lstPreferredEmpType.DataValueField = "PID";
			lstPreferredEmpType.DataBind();
			lstPreferredEmpType.Items.Insert(0, new ListItem("Select Preferred Emp Type", "0"));
		}
	}
	public void GetlstPositionBand()
	{
		DataTable dtPositionBand = new DataTable();
		dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
		if (dtPositionBand.Rows.Count > 0)
		{
			lstPositionBand.DataSource = dtPositionBand;
			lstPositionBand.DataTextField = "BAND";
			lstPositionBand.DataValueField = "BAND";
			lstPositionBand.DataBind();
			lstPositionBand.Items.Insert(0, new ListItem("Select BAND", "0"));
		}
	}

	public void GetInterviewer(int ModuleId)
	{
		DataTable dtIntervie = new DataTable();
		//dtIntervie = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);
		SqlParameter[] spars1 = new SqlParameter[5];
		spars1[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
		spars1[0].Value = "sp_Req_Screeners_Mst_New";
		spars1[1] = new SqlParameter("@moduleID", SqlDbType.Int);
		spars1[1].Value = ModuleId;
		//spars1[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		//spars1[2].Value = Convert.ToString(hdnEmpCpde.Value).Trim();
		dtIntervie = spm.getMobileRemDataList(spars1, "SP_GETREQUISTIONLIST_DETAILS");
		if(dtIntervie.Rows.Count>0)
		{
			lstInterviewerOne.DataSource = dtIntervie;
			lstInterviewerOne.DataTextField = "EmployeeName";
			lstInterviewerOne.DataValueField = "EmployeeCode";
			lstInterviewerOne.DataBind();
			lstInterviewerOne.Items.Insert(0, new ListItem("Select Screening By", "0"));
			         
		}

	}
	public void GetRequisitionNo()
	{
		DataSet dsReqNo = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_Req_REQUISTIONNO";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			dsReqNo = spm.getDatasetList(spars, "SP_GET_REQ_REQUISTIONNO");
			if (dsReqNo.Tables[0].Rows.Count > 0)
			{
				txtReqNumber.Text = "";//Convert.ToString(dsReqNo.Tables[0].Rows[0]["MaxReq_ID"]).Trim();
				txtFromdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");// "MM/dd/yyyy""dd-MM-yyyy HH:mm:ss"
				txtReqName.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["EmpName"]).Trim();
				txtReqDept.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["Department"]).Trim();
				txtReqDesig.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["Designation"]).Trim();
				txtReqEmail.Text = Convert.ToString(dsReqNo.Tables[1].Rows[0]["Emp_Emailaddress"]).Trim();
				// lstPositionName.SelectedValue = Convert.ToString(dsQuestDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void GetecruitmentDetail()
	{

		DataSet dsRecruitmentDetails = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[3];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "RecruitmentReq_Edit";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[2].Value = Session["Empcode"].ToString();
			dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
				txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();
				//DateTime Fromdate = DateTime.ParseExact(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"].ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				// var Timesheet_date = Fromdate.ToString("dd-MM-yyyy HH:mm:ss");
				// txtFromdate.Text = Fromdate.ToString("dd-MM-yyyy HH:mm:ss");
				txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).ToString();
				txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
				txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
				txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

				lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
				lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
				lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
				lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();

				lstPositionDesign.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation_iD"]).Trim();
				//lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
				lstPositionLoca.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
				txtOtherDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["OtherDepartment"]).Trim();
				txtPositionDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionDesignationOther"]).Trim();
				txtAdditionSkill.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AdditionalSkillset"]).Trim();
				txttofilledIn.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ToBeFilledIn_Days"]).Trim();
				txtSalaryRangeFrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangefrom_Lakh_Year"]).Trim();
				txtSalaryRangeTo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangeto_Lakh_Year"]).Trim();
				lstReasonForRequi.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
				lstPreferredEmpType.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PID"]).Trim();
				lstPositionBand.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["BAND"]).Trim();

				txtEssentialQualifi.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["EssentialQualification"]).Trim();
				txtDesiredQualifi.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["DesiredQualification"]).Trim();
				txtRequiredExperiencefrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Required_ExperienceFrom_Years"]).Trim();
				txtRequiredExperienceto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Required_ExperienceTo_Years"]).Trim();
				// txtRequiredExperienceto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
				lstRecommPerson.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();

				if ((dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).ToString() != "")
				{
					GetInterviewer(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));
				}
				else
				{
					GetInterviewer(0);
				}
				txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
				txtInterviewerOptOne.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1_OPT"]).Trim();
				txtInterviewerOptTwo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2_OPT"]).Trim();
				lstInterviewerOne.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
				lstInterviewerTwo.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2"]).Trim();
				lnkuplodedfile.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
				hdnAssignQuestiID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AssignQuestionnaire_ID"]).Trim();
				FileName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
				hflStatusID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Status_ID"]).Trim();
				hflStatusName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"]).Trim();
				HDNISDraft.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ISDraft"]).Trim();
				hdnRecrtStatus.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"]).Trim();
				hdnBankDetailID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JD_BankDetail_ID"]).Trim();
				txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
				//GetFilterGD();
				lstPositionCriti.Enabled = false;
				txtSalaryRangeFrom.Enabled = false;
				txtSalaryRangeTo.Enabled = false;
				if (HDNISDraft.Value == "1" || hflStatusID.Value == "5")
				{
					localtrvl_btnSave.Visible = true;
					trvldeatils_btnSave.Visible = true;
					lstPositionCriti.Enabled = true;
					//txtSalaryRangeFrom.Enabled = true;
					//txtSalaryRangeTo.Enabled = true;
					//accmo_cancel_btn.Visible = true;
					//localtrvl_delete_btn.Visible = true;
				}
				else
				{
					lnkuplodedfile.Style.Add("padding-bottom", "15px");
				}


				ReqdtStatus = spm.Get_Requisition_Status(Convert.ToInt32(hdnRecruitment_ReqID.Value));
				if (ReqdtStatus.Rows.Count > 0)
				{

					// StatusName= Convert.ToString(ReqdtStatus.Rows[0]["Action"]).Trim();
					if (Convert.ToString(ReqdtStatus.Rows[0]["Action"]).Trim() == "Pending" || Convert.ToString(ReqdtStatus.Rows[0]["Action"]).Trim() == "Approved")
					{
						accmo_delete_btn.Visible = true;
						trvldeatils_delete_btn.Visible = true;
						Lnk_CTCWith_BAND.Visible = true;
						lstPositionCriti.Enabled = true;
						//txtSalaryRangeFrom.Enabled = true;
						//txtSalaryRangeTo.Enabled = true;
					}
				}
				if ((hflStatusName.Value != "Approved" && hflStatusID.Value != "1") || hdnRecrtStatus.Value != "Open")
				{
					accmo_delete_btn.Visible = false;
					trvldeatils_delete_btn.Visible = false;
					Lnk_CTCWith_BAND.Visible = false;
					lstPositionCriti.Enabled = false;
					txtSalaryRangeFrom.Enabled = false;
					txtSalaryRangeTo.Enabled = false;
					if (hflStatusID.Value == "5")
					{
						lstPositionCriti.Enabled = true;
						//txtSalaryRangeFrom.Enabled = true;
						//txtSalaryRangeTo.Enabled = true;
					}
				}
				if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
				{
					accmo_delete_btn.Visible = false;
					trvldeatils_delete_btn.Visible = false;
					Lnk_CTCWith_BAND.Visible = false;
					lstPositionCriti.Enabled = false;
					txtSalaryRangeFrom.Enabled = false;
					txtSalaryRangeTo.Enabled = false;
				}
				var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
				var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
				var qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";// GETREQUISITIONHODAPPROVERSTATUS
				var qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
				if (getcompSelectedText.Contains("Head Office"))
				{
					qtype = "GETREQUISITIONAPPROVERSTATUS";
					qtypeHOD = "GETREQUISITIONHODAPPROVERSTATUS";
				}
				int DeptID = 0;
				if ((dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).ToString() != "")
				{
					DeptID = (Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]));
				}
				if (hdnHOD.Value == "HOD")
				{
					if (Convert.ToInt32(hflStatusID.Value) == 3)
						getHOD_Cancel_Approvallist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID, getcompSelectedval, qtype);
					else
						getHODApproverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID, getcompSelectedval, qtypeHOD);

				}
				else
				{
					if (Convert.ToInt32(hflStatusID.Value) == 3)
					{
						get_Cancel_Approverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID);
					}
					else
					{
						getApproverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID, getcompSelectedval, qtype);
					}
				}
				//DivTrvl.Visible = true;
				//btnTra_Details.Text = "-";
				DivAccm.Visible = true;
				trvl_accmo_btn.Text = "-";
				Div_Locl.Visible = true;
				trvl_localbtn.Text = "-";

			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void GetFilterGD()
	{
		int Quest_ID = 0;
		txtJobDescription.Text = "";
		hdnBankDetailID.Value = "";
		DataTable JDBankList = new DataTable();
		string Stype = "getAssignJDBank_Filter";
		JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
		if (JDBankList.Rows.Count > 0)
		{
			txtJobDescription.Text = Convert.ToString(JDBankList.Rows[0]["JobDescription"]).Trim();
			hdnBankDetailID.Value = Convert.ToString(JDBankList.Rows[0]["JD_BankDetail_ID"]).Trim();
		}
	}


	private void GetQuestionnaire()
	{
		int Quest_ID = 0;
		DataTable QuestionnaireList = new DataTable();
		string Stype = "FilterQuestionnaire";
		lnkuplodedfile.Text = "";
		hdnAssignQuestiID.Value = "";
		QuestionnaireList = spm.GetAssign_QuestionnaireFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
		if (QuestionnaireList.Rows.Count > 0)
		{
			lnkuplodedfile.Text = Convert.ToString(QuestionnaireList.Rows[0]["UploadData"]).Trim();
			hdnAssignQuestiID.Value = Convert.ToString(QuestionnaireList.Rows[0]["AssignQuestionnaire_ID"]).Trim();
			FileName.Value = lnkuplodedfile.Text;
		}


	}
	#endregion

	protected void trvl_accmo_btn_Click(object sender, EventArgs e)
	{
		int Recruitment_ReqID = 0, AssignQuestiID = 0;
		string Stype = "SaveforDraft";
		int ISDraft = 1;
		try
		{

			SaveForDraftRecruitment(Stype, ISDraft);
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		int Recruitment_ReqID = 0;
		string Stype = "RequestSubmitted";
		int ISDraft = 0;
		try
		{

			if (hdnHOD.Value == "HOD")
			{
				Stype = "RequestSubmittedHOD";
				SaveReRecruitmentHOD(Stype, ISDraft);
			}
			else
			{

				SaveReRecruitment(Stype, ISDraft);
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	private void SaveForDraftRecruitment(string Stype, int ISDraft)
	{
		int Recruitment_ReqID = 0, AssignQuestiID = 0;

		//string Stype = "RequestSubmitted";
		string EmpCode = "";
		try
		{
			#region Check For Blank Fields
			#endregion
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			Req.Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			Req.Stype = Stype;
			Req.RequisitionNumber = txtReqNumber.Text;
			Req.RequisitionDate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
			Req.Emp_Code = EmpCode;
			Req.PositionCriticality_ID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;
			Req.PositionTitle_ID = Convert.ToString(lstPositionName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionName.SelectedValue) : 0;
			Req.NoOfPosition = txtNoofPosition.Text;
			Req.Department_id = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			Req.PositionDesignationOther = txtPositionDesig.Text;

			Req.loc_code = Convert.ToString(lstPositionLoca.SelectedValue).Trim() != "" ? lstPositionLoca.SelectedValue : "0";
			Req.OtherDepartment = txtOtherDept.Text;

			Req.ModuleId = Convert.ToString(lstSkillset.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillset.SelectedValue) : 0;
			Req.ReasonRequisition_ID = Convert.ToString(lstReasonForRequi.SelectedValue).Trim() != "" ? Convert.ToInt32(lstReasonForRequi.SelectedValue) : 0;
			Req.AdditionalSkillset = txtAdditionSkill.Text;
			Req.Designation_ID = Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDesign.SelectedValue) : 0;

			Req.PID = Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPreferredEmpType.SelectedValue) : 0;
			Req.BAND = lstPositionBand.SelectedValue;
			Req.SalaryRangefrom_Lakh_Year = Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeFrom.Text) : 0;
			Req.SalaryRangeto_Lakh_Year = Convert.ToString(txtSalaryRangeTo.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeTo.Text) : 0;
			Req.ToBeFilledIn_Days = txttofilledIn.Text;
			Req.EssentialQualification = txtEssentialQualifi.Text;
			Req.DesiredQualification = txtDesiredQualifi.Text;

			Req.Required_ExperienceFrom_Years = Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperiencefrom.Text) : 0;
			Req.Required_ExperienceTo_Years = Convert.ToString(txtRequiredExperienceto.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperienceto.Text) : 0;
			Req.RecommendedPerson = Convert.ToString(lstRecommPerson.SelectedValue).Trim();
			Req.JD_BankDetail_ID = Convert.ToString(hdnBankDetailID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetailID.Value) : 0;
			Req.Assign_Questionnaire_ID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;
			Req.Comments = txtComments.Text;
			Req.Emp_Code_Inter1 = Convert.ToString(lstInterviewerOne.SelectedValue).Trim();
			Req.Emp_Code_Inter2 = Convert.ToString(lstInterviewerTwo.SelectedValue).Trim();

			Req.Emp_Code_Inter1_OPT = "";// Convert.ToString(txtInterviewerOptOne.Text).Trim() != "" ? txtInterviewerOptOne.Text.Trim().Split('-')[0] : "";
			Req.Emp_Code_Inter2_OPT = "";// Convert.ToString(txtInterviewerOptTwo.Text).Trim() != "" ? txtInterviewerOptTwo.Text.Trim().Split('-')[0] : "";
										 //Req.RecruiterID = "0";
			Req.Status_ID = 1;
			Req.ISDraft = 1;
			dtRecruitment = spm.InsertRecruitmentRequisition(Req);
			//DateTime Fromdate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
			////var Timesheet_date = temp.ToString("yyyy-MM-dd");
			//dtRecruitment = spm.InsertRecruitmentRequisition(Stype,"" , Fromdate, EmpCode, Convert.ToInt32(lstPositionCriti.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue), txtNoofPosition.Text, Convert.ToInt32(lstPositionDept.SelectedValue), lstPositionLoca.SelectedValue,
			//txtOtherDept.Text, Convert.ToInt32(lstSkillset.SelectedValue),
			//Convert.ToInt32(lstReasonForRequi.SelectedValue), txtAdditionSkill.Text, Convert.ToInt32(lstPositionDesign.SelectedValue), txtPositionDesig.Text, Convert.ToInt32(lstPreferredEmpType.SelectedValue), lstPositionBand.SelectedValue, Convert.ToDecimal(txtSalaryRangeFrom.Text), Convert.ToDecimal(txtSalaryRangeTo.Text),
			//txttofilledIn.Text, txtEssentialQualifi.Text, txtDesiredQualifi.Text, Convert.ToDecimal(txtRequiredExperiencefrom.Text), Convert.ToDecimal(txtRequiredExperienceto.Text), txtRecommPerson.Text, Convert.ToInt32(hdnBankDetailID.Value), AssignQuestiID, txtComments.Text, lstInterviewerOne.SelectedValue, txtInterviewerOptOne.Text, lstInterviewerTwo.SelectedValue, txtInterviewerOptTwo.Text, "0", "1", ISDraft, Recruitment_ReqID);
			int ireqid = 0;

			Response.Redirect("~/procs/Req_Requisition_Details.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void SaveReRecruitment(string Stype, int ISDraft)
	{
		int Recruitment_ReqID = 0, AssignQuestiID = 0;

		//string Stype = "RequestSubmitted";
		string EmpCode = "";
		try
		{

			#region Check For Blank Fields


			if (Convert.ToString(txtReqName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Requisition Name";
				return;
			}
			if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Title";
				return;
			}

			if (Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "" || Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Criticality";
				return;
			}
			if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Skill Set";
				return;
			}
			if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDept.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Department Name";
				return;
			}
			//if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
			//{
			//    lblmessage.Text = "Please select Position Designation";
			//    return;
			//}
			if (Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "" || Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Location";
				return;
			}
			if (Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0")
			{
				if (Convert.ToString(lstPositionDept.Text).Trim() == "Other")
				{
					if (Convert.ToString(txtOtherDept.Text).Trim() == "")
					{
						lblmessage.Text = "Please enter Other Department";
						return;
					}
				}
			}

			if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "0")
			{
				if (Convert.ToString(lstPositionDesign.Text).Trim() == "Other")
				{
					if (Convert.ToString(txtPositionDesig.Text).Trim() == "")
					{
						lblmessage.Text = "Please enter Position Designation Other";
						return;
					}
				}

			}
			if (Convert.ToString(txtNoofPosition.Text).Trim() == "" || Convert.ToString(txtNoofPosition.Text).Trim() == "0")
			{
				lblmessage.Text = "Please enter No of Position";
				return;
			}

			if (Convert.ToString(txttofilledIn.Text).Trim() == "" || Convert.ToString(txttofilledIn.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter To Be Filled In(Days)";
				return;
			}


			if (Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "" || Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Reason For Requisition";
				return;
			}

			if (Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "" || Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Preferred Employment Type";
				return;
			}
			if (Convert.ToString(lstPositionBand.SelectedValue).Trim() == "" || Convert.ToString(lstPositionBand.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Band";
				return;
			}
			if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter Salary Range From (Lakh/Year)";
				return;
			}
			if (Convert.ToString(txtSalaryRangeTo.Text).Trim() == "" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter Salary Range To (Lakh/Year)";
				return;
			}
			if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtSalaryRangeFrom.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
				{
					lblmessage.Text = "Please  enter Salary Range To  greater than from(Lakh/Year)";
					return;
				}
			}

			if (Convert.ToString(txtEssentialQualifi.Text).Trim() == "")
			{
				lblmessage.Text = "Please  enter Essential Qualification";
				return;
			}
			if (Convert.ToString(txtDesiredQualifi.Text).Trim() == "")
			{
				lblmessage.Text = "Please  enter Desired Qualification";
				return;
			}
			if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "" || Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter  Required Experience(Year) From";
				return;
			}
			if (Convert.ToString(txtRequiredExperienceto.Text).Trim() == "" || Convert.ToString(txtRequiredExperienceto.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter  Required Experience (Year) To";
				return;
			}

			if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtRequiredExperiencefrom.Text) > Convert.ToDecimal(txtRequiredExperienceto.Text))
				{
					lblmessage.Text = "Please  enter  Required Experience (Year) To  gheter than from Experience (Year)";
					return;
				}
			}


			//if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			//{
			//    lblmessage.Text = "Please  enter Job Description";
			//    return;
			//}
			if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			{
				if (Convert.ToString(hdnBankDetailID.Value).Trim() == "" || Convert.ToString(hdnBankDetailID.Value).Trim() == "0")
				{
					lblmessage.Text = "No JD from bank for the selected combination. Please contact Administrator.";
					return;
				}
			}
			else
			{
				if (Convert.ToString(hdnBankDetailID.Value).Trim() == "" || Convert.ToString(hdnBankDetailID.Value).Trim() == "0")
				{
					lblmessage.Text = "No JD from bank for the selected combination. Please contact Administrator.";
					return;
				}
			}

			//if (Convert.ToString(hdnAssignQuestiID.Value).Trim() == "")
			//{
			//	lblmessage.Text = "No Questionnaires for the selected combination. Please contact Administrator.";
			//	return;
			//}


			if (Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Screening By";
				return;
			}
			//if (Convert.ToString(lstInterviewerTwo.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerTwo.SelectedValue).Trim() == "0")
			//{
			//    lblmessage.Text = "Please select Interviewer (2st Level)";
			//    return;
			//}


			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			#endregion
			// Get New Approver List
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
			var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
			var qtype = "SP_GETREQUIS_REQUESTER_APPMETRIX_COMP";
			if (getcompSelectedText.Contains("Head Office"))
			{
				qtype = "SP_GETREQUIS_REQUESTER_APPMETRIX";
			}
			//End
			Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			int DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(EmpCode, DeptID, getcompSelectedval, qtype);
			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
				hflapprcode.Value = Approvers_code;
			}
			Req.Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			Req.Stype = Stype;
			Req.RequisitionNumber = txtReqNumber.Text;
			Req.RequisitionDate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
			Req.Emp_Code = EmpCode;
			Req.PositionCriticality_ID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;
			Req.PositionTitle_ID = Convert.ToString(lstPositionName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionName.SelectedValue) : 0;
			Req.NoOfPosition = txtNoofPosition.Text;
			Req.Department_id = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			Req.PositionDesignationOther = txtPositionDesig.Text;

			Req.loc_code = Convert.ToString(lstPositionLoca.SelectedValue).Trim() != "" ? lstPositionLoca.SelectedValue : "0";
			Req.OtherDepartment = txtOtherDept.Text;

			Req.ModuleId = Convert.ToString(lstSkillset.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillset.SelectedValue) : 0;
			Req.ReasonRequisition_ID = Convert.ToString(lstReasonForRequi.SelectedValue).Trim() != "" ? Convert.ToInt32(lstReasonForRequi.SelectedValue) : 0;
			Req.AdditionalSkillset = txtAdditionSkill.Text;
			Req.Designation_ID = Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDesign.SelectedValue) : 0;

			Req.PID = Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPreferredEmpType.SelectedValue) : 0;
			Req.BAND = lstPositionBand.SelectedValue;
			Req.SalaryRangefrom_Lakh_Year = Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeFrom.Text) : 0;
			Req.SalaryRangeto_Lakh_Year = Convert.ToString(txtSalaryRangeTo.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeTo.Text) : 0;
			Req.ToBeFilledIn_Days = txttofilledIn.Text;
			Req.EssentialQualification = txtEssentialQualifi.Text;
			Req.DesiredQualification = txtDesiredQualifi.Text;

			Req.Required_ExperienceFrom_Years = Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperiencefrom.Text) : 0;
			Req.Required_ExperienceTo_Years = Convert.ToString(txtRequiredExperienceto.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperienceto.Text) : 0;
			Req.RecommendedPerson = Convert.ToString(lstRecommPerson.SelectedValue).Trim();
			Req.JD_BankDetail_ID = Convert.ToString(hdnBankDetailID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetailID.Value) : 0;
			Req.Assign_Questionnaire_ID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;
			Req.Comments = txtComments.Text;
			Req.Emp_Code_Inter1 = Convert.ToString(lstInterviewerOne.SelectedValue).Trim();
			Req.Emp_Code_Inter2 = Convert.ToString(lstInterviewerTwo.SelectedValue).Trim();

			Req.Emp_Code_Inter1_OPT = "";// Convert.ToString(txtInterviewerOptOne.Text).Trim() != "" ? txtInterviewerOptOne.Text.Trim().Split('-')[0] : "";
			Req.Emp_Code_Inter2_OPT = "";// Convert.ToString(txtInterviewerOptTwo.Text).Trim() != "" ? txtInterviewerOptTwo.Text.Trim().Split('-')[0] : "";
										 //Req.RecruiterID = "0";
			Req.Status_ID = 1;
			Req.ISDraft = ISDraft;
			dtRecruitment = spm.InsertRecruitmentRequisition(Req);

			int ireqid = 0;
			if (Stype != "SaveforDraft")
			{
				if (dtRecruitment.Rows.Count > 0)
				{
					ireqid = Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);
					if (Convert.ToString(txtReqNumber.Text.Trim()) == "")
					{
						txtReqNumber.Text = Convert.ToString(dtRecruitment.Rows[0]["REQUISTIONNO"]).Trim();
					}

					if (Convert.ToString(ireqid).Trim() == "0")
						return;

					spm.Insert_Requisition_ApproverRequest(Approvers_code, apprid, ireqid);

					String strLeaveRstURL = "";
					//if (Wsch.ToString().Trim() == "6")
					if ("ddd" == "6")
						strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR_S"]).Trim() + "?reqid=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]) + "&itype=0";
					else
						strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_RequiCR"]).Trim() + "?Req_Requi_ID=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);

					string strInsertmediaterlist = "";
					//strInsertmediaterlist = GetIntermediatesList();
					string strApproverlist = "";
					strApproverlist = GetRequisitionApprove_RejectList(hdnEmpCpde.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString(), Convert.ToInt32(lstPositionDept.SelectedValue));
					string RequiredByDate = "";
					RequiredByDate = GetRequiredByDate();
					string strHREmailForCC = "";
					// strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(hdnEmpCpde.Value));
					//if (is_self == "Y")
					//{
					//    //spm.send_mailto_Requester(hflEmailAddress.Value, "", "Request for " + Convert.ToString(txtLeaveType.Text), txtLeaveType.Text, lpm.LeaveDays.ToString(), lpm.Reason, strfromdate_M, lpm.Leave_From_for, strtodate_M, lpm.Leave_To_For, lpm.Emp_Name, "", strApproverlist, hflEmpName.Value, "");
					//}
					//else
					{
						spm.send_mailto_Req_Requisition_Approver(txtReqName.Text, txtReqEmail.Text, approveremailaddress, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);
					}
				}
			}
			Response.Redirect("~/procs/Req_Requisition_Details.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void SaveReRecruitmentHOD(string Stype, int ISDraft)
	{
		int Recruitment_ReqID = 0, AssignQuestiID = 0;

		string EmpCode = "";
		try
		{
			#region Check For Blank Fields


			if (Convert.ToString(txtReqName.Text).Trim() == "")
			{
				lblmessage.Text = "Please enter Requisition Name";
				return;
			}
			if (Convert.ToString(lstPositionName.SelectedValue).Trim() == "" || Convert.ToString(lstPositionName.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Title";
				return;
			}

			if (Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "" || Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Criticality";
				return;
			}
			if (Convert.ToString(lstSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstSkillset.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Skill Set";
				return;
			}
			if (Convert.ToString(lstPositionDept.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDept.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Department Name";
				return;
			}
			//if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() == "0")
			//{
			//    lblmessage.Text = "Please select Position Designation";
			//    return;
			//}
			if (Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "" || Convert.ToString(lstPositionLoca.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Location";
				return;
			}
			if (Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0")
			{
				if (Convert.ToString(lstPositionDept.Text).Trim() == "Other")
				{
					if (Convert.ToString(txtOtherDept.Text).Trim() == "")
					{
						lblmessage.Text = "Please enter Other Department";
						return;
					}
				}
			}

			if (Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" || Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "0")
			{
				if (Convert.ToString(lstPositionDesign.Text).Trim() == "Other")
				{
					if (Convert.ToString(txtPositionDesig.Text).Trim() == "")
					{
						lblmessage.Text = "Please enter Position Designation Other";
						return;
					}
				}

			}
			if (Convert.ToString(txtNoofPosition.Text).Trim() == "" || Convert.ToString(txtNoofPosition.Text).Trim() == "0")
			{
				lblmessage.Text = "Please enter No of Position";
				return;
			}

			if (Convert.ToString(txttofilledIn.Text).Trim() == "" || Convert.ToString(txttofilledIn.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter To Be Filled In(Days)";
				return;
			}
			if (Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "" || Convert.ToString(lstReasonForRequi.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Reason For Requisition";
				return;
			}

			if (Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "" || Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Preferred Employment Type";
				return;
			}
			if (Convert.ToString(lstPositionBand.SelectedValue).Trim() == "" || Convert.ToString(lstPositionBand.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Band";
				return;
			}
			if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter Salary Range From (Lakh/Year)";
				return;
			}
			if (Convert.ToString(txtSalaryRangeTo.Text).Trim() == "" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter Salary Range To (Lakh/Year)";
				return;
			}
			if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtSalaryRangeFrom.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
				{
					lblmessage.Text = "Please  enter Salary Range To  gheter than from(Lakh/Year)";
					return;
				}
			}

			if (Convert.ToString(txtEssentialQualifi.Text).Trim() == "")
			{
				lblmessage.Text = "Please  enter Essential Qualification";
				return;
			}
			if (Convert.ToString(txtDesiredQualifi.Text).Trim() == "")
			{
				lblmessage.Text = "Please  enter Desired Qualification";
				return;
			}
			if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "" || Convert.ToString(txtRequiredExperiencefrom.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter  Required Experience(Year) From";
				return;
			}
			if (Convert.ToString(txtRequiredExperienceto.Text).Trim() == "" || Convert.ToString(txtRequiredExperienceto.Text).Trim() == "0")
			{
				lblmessage.Text = "Please  enter  Required Experience (Year) To";
				return;
			}
			if (Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "")
			{
				if (Convert.ToDecimal(txtRequiredExperiencefrom.Text) > Convert.ToDecimal(txtRequiredExperienceto.Text))
				{
					lblmessage.Text = "Please  enter  Required Experience (Year) To  gheter than from Experience (Year)";
					return;
				}
			}

			//if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			//         {
			//             lblmessage.Text = "Please  enter Job Description";
			//             return;
			//         }			
			if (Convert.ToString(txtJobDescription.Text).Trim() == "")
			{
				if (Convert.ToString(hdnBankDetailID.Value).Trim() == "" || Convert.ToString(hdnBankDetailID.Value).Trim() == "0")
				{
					lblmessage.Text = "No JD from bank for the selected combination. Please contact Administrator.";
					return;
				}
			}
			else
			{
				if (Convert.ToString(hdnBankDetailID.Value).Trim() == "" || Convert.ToString(hdnBankDetailID.Value).Trim() == "0")
				{
					lblmessage.Text = "No JD from bank for the selected combination. Please contact Administrator.";
					return;
				}
			}
			//if (Convert.ToString(hdnAssignQuestiID.Value).Trim() == "")
			//{
			//	lblmessage.Text = "No Questionnaires for the selected combination. Please contact Administrator.";
			//	return;
			//}



			if (Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerOne.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Screening By";
				return;
			}
			//if (Convert.ToString(lstInterviewerTwo.SelectedValue).Trim() == "" || Convert.ToString(lstInterviewerTwo.SelectedValue).Trim() == "0")
			//{
			//    lblmessage.Text = "Please select Interviewer (2st Level)";
			//    return;
			//}



			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			#endregion

			// Get New Approver List
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
			var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
			var qtype = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
			if (getcompSelectedText.Contains("Head Office"))
			{
				qtype = "GETREQUISITIONHODAPPROVERSTATUS";
			}
			//End
			Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}

			int DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;

			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "NEXTLEVELHODMAIL";
			spars[1] = new SqlParameter("@REQID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(Recruitment_ReqID);
			spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCpde.Value).Trim();
			spars[3] = new SqlParameter("@Dept_ID", SqlDbType.Int);
			spars[3].Value = Convert.ToInt32(lstPositionDept.SelectedValue);
			spars[4] = new SqlParameter("@comp_code", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(lstPositionLoca.SelectedValue);
			dtApproverEmailIds = spm.getMobileRemDataList(spars, qtype);
			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
				hflapprcode.Value = Approvers_code;
			}
			else
			{
				getLWPML_HR_ApproverCode("");
			}
			DateTime Fromdate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

			Req.Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			Req.Stype = Stype;
			Req.RequisitionNumber = txtReqNumber.Text;
			Req.RequisitionDate = DateTime.ParseExact(txtFromdate.Text.ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
			Req.Emp_Code = EmpCode;
			Req.PositionCriticality_ID = Convert.ToString(lstPositionCriti.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionCriti.SelectedValue) : 0;
			Req.PositionTitle_ID = Convert.ToString(lstPositionName.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionName.SelectedValue) : 0;
			Req.NoOfPosition = txtNoofPosition.Text;
			Req.Department_id = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			Req.PositionDesignationOther = txtPositionDesig.Text;

			Req.loc_code = Convert.ToString(lstPositionLoca.SelectedValue).Trim() != "" ? lstPositionLoca.SelectedValue : "0";
			Req.OtherDepartment = txtOtherDept.Text;

			Req.ModuleId = Convert.ToString(lstSkillset.SelectedValue).Trim() != "" ? Convert.ToInt32(lstSkillset.SelectedValue) : 0;
			Req.ReasonRequisition_ID = Convert.ToString(lstReasonForRequi.SelectedValue).Trim() != "" ? Convert.ToInt32(lstReasonForRequi.SelectedValue) : 0;
			Req.AdditionalSkillset = txtAdditionSkill.Text;
			Req.Designation_ID = Convert.ToString(lstPositionDesign.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDesign.SelectedValue) : 0;

			Req.PID = Convert.ToString(lstPreferredEmpType.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPreferredEmpType.SelectedValue) : 0;
			Req.BAND = lstPositionBand.SelectedValue;
			Req.SalaryRangefrom_Lakh_Year = Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeFrom.Text) : 0;
			Req.SalaryRangeto_Lakh_Year = Convert.ToString(txtSalaryRangeTo.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeTo.Text) : 0;
			Req.ToBeFilledIn_Days = txttofilledIn.Text;
			Req.EssentialQualification = txtEssentialQualifi.Text;
			Req.DesiredQualification = txtDesiredQualifi.Text;

			Req.Required_ExperienceFrom_Years = Convert.ToString(txtRequiredExperiencefrom.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperiencefrom.Text) : 0;
			Req.Required_ExperienceTo_Years = Convert.ToString(txtRequiredExperienceto.Text).Trim() != "" ? Convert.ToDecimal(txtRequiredExperienceto.Text) : 0;
			Req.RecommendedPerson = Convert.ToString(lstRecommPerson.SelectedValue).Trim();
			Req.JD_BankDetail_ID = Convert.ToString(hdnBankDetailID.Value).Trim() != "" ? Convert.ToInt32(hdnBankDetailID.Value) : 0;
			Req.Assign_Questionnaire_ID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;
			Req.Comments = txtComments.Text;
			Req.Emp_Code_Inter1 = Convert.ToString(lstInterviewerOne.SelectedValue).Trim();
			Req.Emp_Code_Inter2 = Convert.ToString(lstInterviewerTwo.SelectedValue).Trim();

			Req.Emp_Code_Inter1_OPT = "";// Convert.ToString(txtInterviewerOptOne.Text).Trim() != "" ? txtInterviewerOptOne.Text.Trim().Split('-')[0] : "";
			Req.Emp_Code_Inter2_OPT = "";// Convert.ToString(txtInterviewerOptTwo.Text).Trim() != "" ? txtInterviewerOptTwo.Text.Trim().Split('-')[0] : "";

			//Req.RecruiterID = "0";
			Req.Status_ID = 1;
			Req.ISDraft = ISDraft;
			dtRecruitment = spm.InsertRecruitmentRequisition(Req);

			int ireqid = 0;

			if (dtRecruitment.Rows.Count > 0)
			{
				ireqid = Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);
				if (Convert.ToString(txtReqNumber.Text.Trim()) == "")
				{
					txtReqNumber.Text = Convert.ToString(dtRecruitment.Rows[0]["REQUISTIONNO"]).Trim();
				}

				if (Convert.ToString(ireqid).Trim() == "0")
					return;

				//spm.Insert_Requisition_ApproverRequest(hdnnextappcode.Value, Convert.ToInt32(hdnapprid.Value), ireqid);
				spm.Insert_Requisition_ApproverRequest(Approvers_code, apprid, ireqid);
				String strLeaveRstURL = "";
				//if (Wsch.ToString().Trim() == "6")
				if ("ddd" == "6")
					strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_LR_S"]).Trim() + "?reqid=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]) + "&itype=0";
				else
					strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["approverLink_RequiCR"]).Trim() + "?Req_Requi_ID=" + Convert.ToInt32(dtRecruitment.Rows[0]["Recruit_ReqID"]);

				string strInsertmediaterlist = "";
				string strApproverlist = "";
				strApproverlist = GetHODRequisitionApprove_RejectList(hdnEmpCpde.Value, (dtRecruitment.Rows[0]["Recruit_ReqID"]).ToString(), Convert.ToInt32(lstPositionDept.SelectedValue), getcompSelectedval, qtype);
				string strHREmailForCC = "";
				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();
				//strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(hdnEmpCpde.Value));                
				//spm.send_mailto_Req_Requisition_Approver(txtReqName.Text, txtReqEmail.Text, hdnApproverid_LWPPLEmail.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);
				spm.send_mailto_Req_Requisition_Approver(txtReqName.Text, txtReqEmail.Text, approveremailaddress, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);

			}

			Response.Redirect("~/procs/Req_Requisition_Details.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	private string GetRequiredByDate()
	{
		string[] strdate;
		string strtoDate = "", RequiredByDate = "";
		DateTime RequiredDate;
		int Days;
		try
		{
			if (txtFromdate.Text != "")
			{
				Days = Convert.ToInt32(txttofilledIn.Text);
				strdate = Convert.ToString(txtFromdate.Text).Trim().Split('-');

				strtoDate = Convert.ToString(strdate[2].Substring(0, 4)) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
				DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				RequiredDate = ddt.AddDays(Days);
				RequiredByDate = RequiredDate.ToString("dd-MM-yyyy");
			}
		}
		catch (Exception ex)
		{
			return RequiredByDate;
			//Response.Write(ex.Message.ToString());
		}
		return RequiredByDate;
	}
	public void getLWPML_HR_ApproverCode(string strtype)
	{

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "LWPML_HREmpCode";
		spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[2].Value = hdnEmpCpde.Value;
		dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
		//Travel Desk Approver Code
		//hdnisApprover_TDCOS.Value = "Approver";       
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnnextappcode.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
			Approvers_code = hdnnextappcode.Value.ToString();
			hdnapprid.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["app_id"]).Trim();
			apprid = Convert.ToInt32(hdnapprid.Value);
			hdnApproverid_LWPPLEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
			approveremailaddress = hdnApproverid_LWPPLEmail.Value.ToString();
			hdnisApprover_TDCOS.Value = "NA";
		}

	}
	protected string Is_Self_Approver(string strEmpCode)
	{
		string strSelfApprover = "";
		try
		{
			DataSet dsTrDetails = new DataSet();
			SqlParameter[] spars = new SqlParameter[2];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "Is_Self_Approver";

			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = strEmpCode;

			dsTrDetails = spm.getDatasetList(spars, "SP_GET_REQ_REQUISTIONNO");

			if (dsTrDetails.Tables[0].Rows.Count > 0)
			{
				strSelfApprover = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["self_approver"]).Trim();
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

		return strSelfApprover;

	}
	protected string GetRequisitionApprove_RejectList_Cancel(string EmpCode, string RecrutID, int DeptID)
	{
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.GetRequisitionApproverStatus_Cancellation(EmpCode, RecrutID, DeptID);
		if (dtAppRej.Rows.Count > 0)
		{
			sbapp.Append("<table>");
			for (int i = 0; i < dtAppRej.Rows.Count; i++)
			{
				sbapp.Append("<tr>");
				sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
				sbapp.Append("</tr>");
			}
			sbapp.Append("</table>");
		}
		return Convert.ToString(sbapp);
	}
	protected string GetRequisitionApprove_RejectList(string EmpCode, string RecrutID, int DeptID)
	{
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
		var qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
		if (getcompSelectedText.Contains("Head Office"))
		{
			qtype = "GETREQUISITIONAPPROVERSTATUS";
		}
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.GetRequisitionApproverStatus(EmpCode, RecrutID, DeptID, getcompSelectedval, qtype);
		if (dtAppRej.Rows.Count > 0)
		{
			sbapp.Append("<table>");
			for (int i = 0; i < dtAppRej.Rows.Count; i++)
			{
				sbapp.Append("<tr>");
				sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
				sbapp.Append("</tr>");
			}
			sbapp.Append("</table>");
		}
		return Convert.ToString(sbapp);
	}
	protected string GetHODRequisitionApprove_RejectList(string EmpCode, string RecrutID, int DeptID, string comp_code, string qtype)
	{
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.GetRequisitionHODApproverStatus(EmpCode, RecrutID, DeptID, comp_code, qtype);
		if (dtAppRej.Rows.Count > 0)
		{
			sbapp.Append("<table>");
			for (int i = 0; i < dtAppRej.Rows.Count; i++)
			{
				sbapp.Append("<tr>");
				sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
				sbapp.Append("</tr>");
			}
			sbapp.Append("</table>");
		}
		return Convert.ToString(sbapp);
	}
	protected string GetHODRequisitionApprove_RejectList_Cancel(string EmpCode, string RecrutID, int DeptID, string comp_code, string qtype)
	{
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.GetRequisitionHODApproverStatus_Cancel(EmpCode, RecrutID, DeptID, comp_code, qtype);
		if (dtAppRej.Rows.Count > 0)
		{
			sbapp.Append("<table>");
			for (int i = 0; i < dtAppRej.Rows.Count; i++)
			{
				sbapp.Append("<tr>");
				sbapp.Append("<td>" + dtAppRej.Rows[i]["names"] + "</td>");
				sbapp.Append("</tr>");
			}
			sbapp.Append("</table>");
		}
		return Convert.ToString(sbapp);
	}
	protected string Get_HREmail_ForCC(string strEmpCode)
	{
		String sbapp = "";

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[2];

		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "get_HREmail_ID";

		spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
		spars[1].Value = strEmpCode;

		dsTrDetails = spm.getDatasetList(spars, "SP_GET_REQ_REQUISTIONNO");

		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			for (int i = 0; i < dsTrDetails.Tables[0].Rows.Count; i++)
			{
				if (Convert.ToString(sbapp).Trim() == "")
					sbapp = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
				else
					sbapp = ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();

			}
		}

		return Convert.ToString(sbapp);
	}

	protected void lstPositionName_SelectedIndexChanged(object sender, EventArgs e)
	{
		if ((Convert.ToString(lstPositionName.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0") && (Convert.ToString(lstSkillset.SelectedValue).Trim() != "" || Convert.ToString(lstSkillset.SelectedValue).Trim() != "0"))
		{
			GetFilterGD();
			GetQuestionnaire();

		}
	}
	protected void lstSkillset_SelectedIndexChanged(object sender, EventArgs e)
	{
		if ((Convert.ToString(lstPositionName.SelectedValue).Trim() != "" || Convert.ToString(lstPositionName.SelectedValue).Trim() != "0") && (Convert.ToString(lstSkillset.SelectedValue).Trim() != "" || Convert.ToString(lstSkillset.SelectedValue).Trim() != "0"))
		{
			GetFilterGD();
			GetQuestionnaire();
			lstInterviewerOne.SelectedIndex = -1;
			GetInterviewer(Convert.ToInt32(lstSkillset.SelectedValue));
		}

	}


	//protected void btnTra_Details_Click(object sender, EventArgs e)
	//{

	//    if (DivTrvl.Visible)
	//    {
	//        DivTrvl.Visible = false;
	//        btnTra_Details.Text = "+";
	//    }
	//    else
	//    {

	//        DivTrvl.Visible = true;
	//        btnTra_Details.Text = "-";
	//    }
	//}

	protected void LinkButton1_Click(object sender, EventArgs e)
	{
		if (Convert.ToString(txtReqName.Text).Trim() == "")
		{
			lblmessage.Text = "Please enter Requisition Name";
			return;
		}
		if (DivAccm.Visible)
		{
			DivAccm.Visible = false;
			trvl_accmo_btn.Text = "+";
		}
		else
		{
			DivAccm.Visible = true;
			trvl_accmo_btn.Text = "-";

		}

	}

	protected void trvl_localbtn_Click(object sender, EventArgs e)
	{

		if (Div_Locl.Visible)
		{
			Div_Locl.Visible = false;
			trvl_localbtn.Text = "+";
		}
		else
		{
			Div_Locl.Visible = true;
			trvl_localbtn.Text = "-";
		}

	}

	protected void localtrvl_cancel_btn_Click(object sender, EventArgs e)
	{

		try
		{

			int Quest_ID = 0; lblmsg3.Text = "";
			DataTable JDBankList = new DataTable();
			string Stype = "getAssignJDBank_Filter";

			#region Check For Blank Fields
			if (Convert.ToString(lstDGSkillset.SelectedValue).Trim() == "" || Convert.ToString(lstDGSkillset.SelectedValue).Trim() == "0")
			{
				lblmsg3.Text = "Please select Skill Set";
				ModalPopupExtenderDG.Show();
				return;
			}
			if (Convert.ToString(lstDGPosition.SelectedValue).Trim() == "" || Convert.ToString(lstDGPosition.SelectedValue).Trim() == "0")
			{
				lblmsg3.Text = "Please select Position Title";
				ModalPopupExtenderDG.Show();
				return;
			}
			//string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			//if (confirmValue != "Yes")
			//{
			//    return;
			//}
			#endregion
			JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstDGSkillset.SelectedValue), Convert.ToInt32(lstDGPosition.SelectedValue));
			grdGetGD.DataSource = null;
			grdGetGD.DataBind();
			if (JDBankList.Rows.Count > 0)
			{
				lblmsg3.Text = "";
				grdGetGD.DataSource = JDBankList;
				grdGetGD.DataBind();
				ModalPopupExtenderDG.Show();
				Oth_btnDelete.Visible = true;
			}
			else
			{
				lblmsg3.Text = "No JD from bank for the selected combination. Please contact Administrator.";
				Oth_btnDelete.Visible = false;
			}
			ModalPopupExtenderDG.Show();
		}
		catch (Exception ex)
		{

		}

	}

	protected void Oth_btnDelete_Click(object sender, EventArgs e)
	{
		int Quest_ID = 0;
		txtJobDescription.Text = "";
		DataTable JDBankList = new DataTable();
		string Stype = "getAssignJDBank_Filter";
		JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstDGSkillset.SelectedValue), Convert.ToInt32(lstDGPosition.SelectedValue));
		if (JDBankList.Rows.Count > 0)
		{
			txtJobDescription.Text = Convert.ToString(JDBankList.Rows[0]["JobDescription"]).Trim();
			hdnBankDetailID.Value = Convert.ToString(JDBankList.Rows[0]["JD_BankDetail_ID"]).Trim();
			//mobile_cancel.Visible = true;
		}

	}

	protected void accmo_delete_btn_Click(object sender, EventArgs e)
	{
		if (hdnHOD.Value == "HOD")
		{
			CancelReuisitionHOD();
		}
		else
		{
			CancelReuisition();
		}

	}
	private void CancelReuisition()
	{
		//Cancel requisition
		string EmpCode = "";
		int Recruitment_ReqID = 0;
		#region Check For Blank Fields


		if (Convert.ToString(txtComments.Text).Trim() == "")
		{
			lblmessage.Text = "Please enter comment";
			return;
		}


		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
		if (confirmValue != "Yes")
		{
			return;
		}

		#endregion

		Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
		//AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

		if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
		{
			EmpCode = Convert.ToString(Session["Empcode"]).ToString();
		}
		string strapprovermails = "";
		strapprovermails = getCancellationmailList();
		string strApproverlist = "";
		int DeptID = 0;
		DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
		strApproverlist = GetRequisitionApprove_RejectList_Cancel(Convert.ToString(hdnEmpCpde.Value), hdnRecruitment_ReqID.Value, DeptID);

		string strHREmailForCC = "";
		strHREmailForCC = Get_HREmail_ForCC(Convert.ToString(hdnEmpCpde.Value));
		string StatusName = "";
		dtCancelRec = spm.Get_Requisition_Status(Convert.ToInt32(hdnRecruitment_ReqID.Value));

		if (dtCancelRec.Rows.Count > 0)
		{
			StatusName = (string)dtCancelRec.Rows[0]["Action"];
		}

		if (Convert.ToString(StatusName).Trim() == "Approved")
		{
			// Get New Approver List
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
			var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
			var qtype = "SP_GETREQUIS_REQUESTER_APPMETRIX_COMP";
			if (getcompSelectedText.Contains("Head Office"))
			{
				qtype = "SP_GETREQUIS_REQUESTER_APPMETRIX";
			}
			//End
			#region get First Approver id
			string approverEMPCode = "";
			DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
			dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(hdnEmpCpde.Value, DeptID, getcompSelectedval, qtype);

			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				approverEMPCode = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];

			}
			#endregion
			String strRecRstURL = "";
			strRecRstURL = Convert.ToString(ConfigurationManager.AppSettings["CancelLink_RequiCR"]).Trim() + "?Req_Requi_ID=" + Convert.ToInt32(hdnRecruitment_ReqID.Value);
			string RequiredByDate = "", CancedlList = "";
			RequiredByDate = GetRequiredByDate();
			spm.Cancel_Requisition_Approved_Request(hdnRecruitment_ReqID.Value, Convert.ToString(txtComments.Text).Trim(), approverEMPCode, apprid);
			CancedlList = GetRequisitionApprove_RejectList_Cancel(hdnEmpCpde.Value, (hdnRecruitment_ReqID.Value).ToString(), DeptID);
			spm.send_mailto_Req_RequisitionNext_Approver_Cancellation(txtReqName.Text, hflEmailAddress.Value, approveremailaddress, "Recruitment - Cancellation Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, CancedlList, "", strRecRstURL);
		}
		else
		{
			// Get New Approver List
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
			var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
			var qtype = "SP_GETREQUIS_REQUESTER_APPMETRIX_COMP";
			if (getcompSelectedText.Contains("Head Office"))
			{
				qtype = "SP_GETREQUIS_REQUESTER_APPMETRIX";
			}
			//End
			spm.Cancel_Requisition_Request(hdnRecruitment_ReqID.Value, Convert.ToString(txtComments.Text).Trim(), null, apprid);
			DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
			dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(EmpCode, DeptID, getcompSelectedval, qtype);
			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
				hflapprcode.Value = Approvers_code;
			}
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			if (Convert.ToString(strapprovermails).Trim() != "")
				strapprovermails = ";" + strHREmailForCC;
			else
				strapprovermails = strHREmailForCC;

			spm.send_mailto_Cancel_Intermediate_Req_Requisition(approveremailaddress, strapprovermails, "Recruitment - Cancellation of " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text, txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtComments.Text, strApproverlist, txtReqName.Text, "");

		}
		Response.Redirect("~/procs/Req_Requisition_Details.aspx");
	}
	private void CancelReuisitionHOD()
	{
		//Cancel requisition
		string EmpCode = "";
		int Recruitment_ReqID = 0;
		#region Check For Blank Fields


		if (Convert.ToString(txtComments.Text).Trim() == "")
		{
			lblmessage.Text = "Please enter comment";
			return;
		}


		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
		if (confirmValue != "Yes")
		{
			return;
		}

		#endregion
		// Get New Approver List
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
		var qtype = "GETREQUISITIONHODAPPROVERSTATUS_COMP";
		if (getcompSelectedText.Contains("Head Office"))
		{
			qtype = "GETREQUISITIONHODAPPROVERSTATUS";
		}
		//End


		Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
		//AssignQuestiID = Convert.ToString(hdnAssignQuestiID.Value).Trim() != "" ? Convert.ToInt32(hdnAssignQuestiID.Value) : 0;

		if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
		{
			EmpCode = Convert.ToString(Session["Empcode"]).ToString();
		}

		string StatusName = "";
		dtCancelRec = spm.Get_Requisition_Status(Convert.ToInt32(hdnRecruitment_ReqID.Value));

		if (dtCancelRec.Rows.Count > 0)
		{
			StatusName = (string)dtCancelRec.Rows[0]["Action"];
		}
		String strRecRstURL = "";
		strRecRstURL = Convert.ToString(ConfigurationManager.AppSettings["CancelLink_RequiCR"]).Trim() + "?Req_Requi_ID=" + Convert.ToInt32(hdnRecruitment_ReqID.Value);
		//if (Convert.ToString((hdnstaus.Value).Trim()) != "")

		DataTable dsapproverNxt = new DataTable();
		SqlParameter[] spars1 = new SqlParameter[5];
		spars1[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
		spars1[0].Value = "NEXTLEVELHODMAIL";
		spars1[1] = new SqlParameter("@REQID", SqlDbType.Int);
		spars1[1].Value = Convert.ToInt32(hdnRecruitment_ReqID.Value);
		spars1[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars1[2].Value = Convert.ToString(hdnEmpCpde.Value).Trim();
		spars1[3] = new SqlParameter("@Dept_ID", SqlDbType.Int);
		spars1[3].Value = Convert.ToInt32(lstPositionDept.SelectedValue);
		spars1[4] = new SqlParameter("@comp_code", SqlDbType.VarChar);
		spars1[4].Value = Convert.ToString(lstPositionLoca.SelectedValue);
		//dsapproverNxt = spm.getMobileRemDataList(spars1, "GETREQUISITIONHODAPPROVERSTATUS");
		dsapproverNxt = spm.getMobileRemDataList(spars1, qtype);
		if (dsapproverNxt.Rows.Count > 0)
		{
			approveremailaddress = (string)dsapproverNxt.Rows[0]["Emp_Emailaddress"];
			apprid = (int)dsapproverNxt.Rows[0]["APPR_ID"];
			Approvers_code = (string)dsapproverNxt.Rows[0]["A_EMP_CODE"];
			hflapprcode.Value = Approvers_code;
		}
		//getLWPML_HR_ApproverCode("");
		string RequiredByDate = "";
		RequiredByDate = GetRequiredByDate();
		if (Convert.ToString(StatusName).Trim() == "Approved")
		{
			String strapproverlist = "";
			string strInsertmediaterlist = "";
			spm.Cancel_Requisition_Approved_Request(hdnRecruitment_ReqID.Value, Convert.ToString(txtComments.Text).Trim(), Approvers_code, Convert.ToInt32(apprid));
			strapproverlist = GetHODRequisitionApprove_RejectList_Cancel(hdnEmpCpde.Value, (hdnRecruitment_ReqID.Value).ToString(), Convert.ToInt32(lstPositionDept.SelectedValue), getcompSelectedval, qtype);
			spm.send_mailto_Req_RequisitionNext_Approver_Cancellation(txtReqName.Text, hflEmailAddress.Value, approveremailaddress, "Recruitment - Cancellation Request for " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, strapproverlist, "", strRecRstURL);
		}
		else
		{
			spm.Cancel_Requisition_Request(hdnRecruitment_ReqID.Value, Convert.ToString(txtComments.Text).Trim(), null, Convert.ToInt32(apprid));
			spm.send_mailto_Cancel_Intermediate_Req_Requisition(approveremailaddress, "", "Recruitment - Cancellation of " + Convert.ToString(txtReqNumber.Text), txtReqNumber.Text, txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtComments.Text, "", txtReqName.Text, "");

		}
		Response.Redirect("~/procs/Req_Requisition_Details.aspx");
	}
	private void get_Cancel_Approverlist(string EmpCode, string RecrutID, int DeptID)
	{
		DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
		dtapprover = spm.GetRequisitionApproverStatus_Cancellation(EmpCode, RecrutID, DeptID);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}

	private void getApproverlist(string EmpCode, string RecrutID, int DeptID, string comp_code, string qtype)
	{
		DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
		dtapprover = spm.GetRequisitionApproverStatus(EmpCode, RecrutID, DeptID, comp_code, qtype);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}
	private void getHODApproverlist(string EmpCode, string RecrutID, int DeptID, string comp_code, string qtype)
	{
		DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
		dtapprover = spm.GetRequisitionHODApproverStatus(EmpCode, RecrutID, DeptID, comp_code, qtype);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}
	private void getHOD_Cancel_Approvallist(string EmpCode, string RecrutID, int DeptID, string comp_code, string qtype)
	{//
		DataTable dtapprover = new DataTable();
		//string strleavetype = Convert.ToString(txtLeaveType.Text).Trim();       
		dtapprover = spm.GetRequisitionHODApproverStatus_Cancel(EmpCode, RecrutID, DeptID, comp_code, qtype);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}
	protected string getCancellationmailList()
	{
		string email_ids = "";
		int DeptID = 0;
		DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
		dtApproverEmailIds = spm.Get_Requisition_ApproverDetails_Rejection_cancellation(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID, "get_PreviousApproverDetails_mail");
		//lstApprover.Items.Clear();
		if (dtApproverEmailIds.Rows.Count > 0)
		{
			for (int irow = 0; irow < dtApproverEmailIds.Rows.Count; irow++)
			{
				if (Convert.ToString(email_ids).Trim() == "")
					email_ids = Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
				else
					email_ids = email_ids + ";" + Convert.ToString(dtApproverEmailIds.Rows[irow]["Emp_Emailaddress"]).Trim();
			}
		}

		return email_ids;

	}


	protected void lnkJDEdit_Click(object sender, ImageClickEventArgs e)
	{
		JDShow.Visible = true;
		int Quest_ID = 0;
		txtJobDescription.Text = "";
		DataTable JDBankList = new DataTable();
		string Stype = "getAssignJDBank_Filter";
		JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstDGSkillset.SelectedValue), Convert.ToInt32(lstDGPosition.SelectedValue));
		if (JDBankList.Rows.Count > 0)
		{
			txtJDShow.Text = Convert.ToString(JDBankList.Rows[0]["JobDescription"]).Trim();
			hdnBankDetailID.Value = Convert.ToString(JDBankList.Rows[0]["JD_BankDetail_ID"]).Trim();
			//mobile_cancel.Visible = true;
		}
	}
	protected void trvldeatils_delete_btn_Click(object sender, EventArgs e)
	{

		try
		{
			string EmpCode = "";
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			if (Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "" || Convert.ToString(lstPositionCriti.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Criticality";
				return;
			}
			//if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0" || Convert.ToString(txtSalaryRangeFrom.Text).Trim() == "0.00")
			//{
			//	lblmessage.Text = "Please  enter Salary Range From (Lakh/Year)";
			//	return; (
			//}
			//if (Convert.ToString(txtSalaryRangeTo.Text).Trim() == "" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0" || Convert.ToString(txtSalaryRangeTo.Text).Trim() == "0.00")
			//{
			//	lblmessage.Text = "Please  enter Salary Range To (Lakh/Year)";
			//	return;
			//}
			//if (Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "")
			//{
			//	if (Convert.ToDecimal(txtSalaryRangeFrom.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
			//	{
			//		lblmessage.Text = "Please  enter Salary Range To  gheter than from(Lakh/Year)";
			//		return;
			//	}
			//}
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}

			DataTable dtCriticalityChange = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "PositionCriticalityChange";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(EmpCode);
			spars[3] = new SqlParameter("@PositionCriticality_ID", SqlDbType.Int);
			spars[3].Value = Convert.ToInt32(lstPositionCriti.SelectedValue);
			spars[4] = new SqlParameter("@SalaryRangefrom_Lakh_Year", SqlDbType.Decimal);
			spars[4].Value = Convert.ToString(txtSalaryRangeFrom.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeFrom.Text) : 0;
			spars[5] = new SqlParameter("@SalaryRangeto_Lakh_Year", SqlDbType.Decimal);
			spars[5].Value = Convert.ToString(txtSalaryRangeTo.Text).Trim() != "" ? Convert.ToDecimal(txtSalaryRangeTo.Text) : 0;
			dtCriticalityChange = spm.getMobileRemDataList(spars, "SP_Recruitment_Requisition_INSERT");
			if (dtCriticalityChange.Rows.Count > 0)
			{
			}
			Response.Redirect("~/procs/Req_Requisition_Details.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected void lstPositionDept_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lstPositionDept.SelectedIndex > 0)
		{
			if (lstPositionLoca.SelectedIndex > 0)
			{
				var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
				var getcompSelectedval = lstPositionLoca.SelectedItem.Value;
				var qtype = "GETREQUISITIONAPPROVERSTATUS_COMP";
				if (getcompSelectedText.Contains("Head Office"))
				{
					qtype = "GETREQUISITIONAPPROVERSTATUS";
				}

				int DeptID = 0;
				DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
				if (hdnHOD.Value == "HOD")
				{
					//getHODApproverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value,DeptID);	
					getApproverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID, getcompSelectedval, qtype);
				}
				else
				{
					getApproverlist(hdnEmpCpde.Value, hdnRecruitment_ReqID.Value, DeptID, getcompSelectedval, qtype);
				}
			}
			else
			{
				lblmessage.Text = "Please select company.";
				return;
			}
		}
	}

	protected void lstPositionBand_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			if (lstPositionBand.SelectedIndex > 0)
			{
				string Band = lstPositionBand.SelectedValue.Trim();
				CTC_Rage_With_Band(Band);
			}
			else
			{
				txtSalaryRangeFrom.Text = "";
				txtSalaryRangeTo.Text = "";
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	private void CTC_Rage_With_Band(string Band)
	{
		try
		{
			int Recruitment_ReqID = 0;
			Recruitment_ReqID = Convert.ToString(hdnRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdnRecruitment_ReqID.Value) : 0;
			string EmpCode = "";
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			DataTable dtCTCRange = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Recruitment_BAND";
			spars[1] = new SqlParameter("@ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(Recruitment_ReqID);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
			spars[2].Value = Convert.ToString(EmpCode);
			spars[3] = new SqlParameter("@Band", SqlDbType.NVarChar);
			spars[3].Value = Convert.ToString(Band);
			dtCTCRange = spm.getMobileRemDataList(spars, "SP_Rec_CTC_With_Band");
			if (dtCTCRange.Rows.Count > 0)
			{
				txtSalaryRangeFrom.Text = Convert.ToString(dtCTCRange.Rows[0]["SalAverage"]).Trim();
				txtSalaryRangeTo.Text = Convert.ToString(dtCTCRange.Rows[0]["SalMax"]).Trim();
			}
			else
			{
				string msg = "Salary range not set for the selected Band. Please contact HR!.";
				txtSalaryRangeFrom.Text = "";
				txtSalaryRangeTo.Text = "";
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);

			}
		}
		catch (Exception)
		{

			throw;
		}
	}

	protected void Lnk_CTCWith_BAND_Click(object sender, EventArgs e)
	{
		try
		{
			string EmpCode = "";
			if (Convert.ToString(Session["Empcode"]).Trim() != "" || Session["Empcode"] != null)
			{
				EmpCode = Convert.ToString(Session["Empcode"]).ToString();
			}
			if (Convert.ToString(lstPositionBand.SelectedValue).Trim() == "" || Convert.ToString(lstPositionBand.SelectedValue).Trim() == "0")
			{
				lblmessage.Text = "Please select Position Band";
				return;
			}
			string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
			if (confirmValue != "Yes")
			{
				return;
			}
			DataTable dtCriticalityChange = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "PositionCTCChange";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(EmpCode);
			spars[3] = new SqlParameter("@BAND", SqlDbType.NVarChar);
			spars[3].Value = Convert.ToString(lstPositionBand.SelectedValue);
			dtCriticalityChange = spm.getMobileRemDataList(spars, "SP_Recruitment_Requisition_INSERT");
			if (dtCriticalityChange.Rows.Count > 0)
			{
			}
			Response.Redirect("~/procs/Req_Requisition_Details.aspx");
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	protected void lstPositionLoca_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
			DgvApprover.DataSource = null;
			DgvApprover.DataBind();
			lstPositionDept.SelectedValue = Convert.ToString("0").Trim();
		}
		catch (Exception ex)
		{

		}
	}
}