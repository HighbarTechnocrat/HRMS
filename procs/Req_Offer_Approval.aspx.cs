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
using System.Linq;
using ClosedXML.Excel;

public partial class procs_Rec_Offer_Approval : System.Web.UI.Page
{
	#region CreativeMethods
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public static string pimg = "";
	public static string cimg = "";
	public DataSet dsCandidateData, dsCVSource, dtIrSheetReport;
	public DataTable dtofferApproval, dtOfferStatus, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
	public string loc = "", dept = "", subdept = "", desg = "";
	public string filename = "", multiplefilename = "", multiplefilenameadd = "";
	public int did = 0;
	private static Random random = new Random();
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}

	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
	#endregion

	SP_Methods spm = new SP_Methods();
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}

			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Reembursementindex");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					hdnEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
					hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
					hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
					OfferApprovalOther.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["OfferApprovalDocumentpath"]).Trim());
					FilePath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
					hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());
					hdfilefathIRSheet.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheet"]).Trim());
					hdnInterviewphtoPath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_InterviewerPhoto"]).Trim());

					GetSkillsetName();
					GetPositionName();
					GetPositionCriticality();
					GetDepartmentMaster();
					GetCompany_Location();
					GetReasonRequisition();
					GetPositionDesign();
					GetPreferredEmpType();
					GetlstPositionBand();
					CheckHideLabel();
					//GetInterviewer();
					GetRecruiter();
					getCVSource();

					GetCandidateInfoRecruitmentwisedataBind();
					if (Request.QueryString.Count > 0)
					{
						hdnOffer_App_ID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnReqID.Value = Convert.ToString(Request.QueryString[1]).Trim();
						checkOffer_ApprovalStatus_Submit();
						GetOffer_CuurentApprID();
						Get_HR_Employee_Details();// HR Approval 
						GetRecruitmentDetail();
						GetOffferAPPDetail();
						OfferCreatelist();
						//GetOffer_Approverlist();

					}
					GetlstRequisitionNo();
					SearchGetSkillsetName();
					SearchGetCompany_Location();
					SearchGetlstPositionBand();
					SearchGetPositionName();
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	private void getCVSource()
	{
		dsCVSource = spm.GetCVSource();
		lstCVSource.DataSource = dsCVSource;
		lstCVSource.DataTextField = "CVSource";
		lstCVSource.DataValueField = "CVSource_ID";
		lstCVSource.DataBind();
		lstCVSource.Items.Insert(0, new ListItem("Select CVSource", ""));
	}
	public void CheckHideLabel()
	{
		try
		{
			lstPositionName.Enabled = false;
			lstPositionCriti.Enabled = false;
			lstSkillset.Enabled = false;
			lstPositionDept.Enabled = false;
			lstPositionDesign.Enabled = false;
			lstPositionLoca.Enabled = false;
			txtOtherDept.Enabled = false;
			txtPositionDesig.Enabled = false;
			//listRecruiter.Enabled = false;
			txtNoofPosition.Enabled = false;
			txtAdditionSkill.Enabled = false;
			txttofilledIn.Enabled = false;
			txtSalaryRangeFrom.Enabled = false;
			txtSalaryRangeTo.Enabled = false;
			lstReasonForRequi.Enabled = false;

			lstPreferredEmpType.Enabled = false;
			lstPositionBand.Enabled = false;

			txtEssentialQualifi.Enabled = false;
			txtDesiredQualifi.Enabled = false;

			txtRequiredExperiencefrom.Enabled = false;
			txtRequiredExperienceto.Enabled = false;
			lstRecommPerson.Enabled = false;
			txtJobDescription.Enabled = false;
			txtComments.Enabled = false;
			lstInterviewerOne.Enabled = false;
			lstInterviewerTwo.Enabled = false;
			txtInterviewerOptOne.Enabled = false;
			txtInterviewerOptTwo.Enabled = false;
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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

			DDLmainSkillSet.DataSource = dtSkillset;
			DDLmainSkillSet.DataTextField = "ModuleDesc";
			DDLmainSkillSet.DataValueField = "ModuleId";
			DDLmainSkillSet.DataBind();
			DDLmainSkillSet.Items.Insert(0, new ListItem("Select SkillSet", ""));

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
			lstPositionDept.Enabled = false;
			//updated code
			DataRow[] dr = dtPositionDept.Select("Department_Name = '" + txtReqDept.Text.ToString().Trim() + "'");
			if (dr.Length > 0)
			{
				string avalue = dr[0]["Department_id"].ToString();
				lstPositionDept.SelectedValue = avalue;
			}
			//lstPositionDept.Items.FindByText(txtReqDept.Text).Selected = true;
			//lstPositionDept.Enabled = false;
		}
	}
	public void GetCompany_Location()
	{
		DataTable lstPosition = new DataTable();
		lstPosition = spm.GetRecruitment_Req_company_Location();
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

			lstOfferBand.DataSource = dtPositionBand;
			lstOfferBand.DataTextField = "BAND";
			lstOfferBand.DataValueField = "BAND";
			lstOfferBand.DataBind();
			lstOfferBand.Items.Insert(0, new ListItem("Select Offer Band", "0"));
		}
	}
	public void GetInterviewer(int ModuleId)
	{
		DataTable dtInterviewer = new DataTable();
		dtInterviewer = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);
		if (dtInterviewer.Rows.Count > 0)
		{
			lstInterviewerOne.DataSource = dtInterviewer;
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
				txtReqNumber.Text = Convert.ToString(dsReqNo.Tables[0].Rows[0]["MaxReq_ID"]).Trim();
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
	public void GetRecruiter()
	{
		DataTable dtInterviewer = new DataTable();
		dtInterviewer = spm.GetRecruitment_Recruiter();
		if (dtInterviewer.Rows.Count > 0)
		{
			//listRecruiter.DataSource = dtInterviewer;
			//listRecruiter.DataTextField = "EmpName";
			//listRecruiter.DataValueField = "Emp_Code";
			//listRecruiter.DataBind();
			//listRecruiter.Items.Insert(0, new ListItem("Select Recruiter", "0"));
		}
	}
	private void GetRecruitmentDetail()
	{

		DataSet dsRecruitmentDetails = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[4];

			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "RecruitmentReq_ViewEnterviewOffer";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnReqID.Value);
			spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[2].Value = Session["Empcode"].ToString();
			spars[3] = new SqlParameter("@Designation_ID", SqlDbType.VarChar);
			spars[3].Value = hdnOffer_App_ID.Value;
			dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
				txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();

				txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).ToString();
				txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
				txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
				txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
				//hdnEmpCodePrve.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code"]).Trim();
				lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
				lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
				lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
				lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
				hdncomp_code.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
				hdndept_Id.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				lstPositionLoca.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
				txtOtherDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["OtherDepartment"]).Trim();
				txtPositionDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionDesignationOther"]).Trim();
				txtAdditionSkill.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["AdditionalSkillset"]).Trim();
				txttofilledIn.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ToBeFilledIn_Days"]).Trim();
				txtSalaryRangeFrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangefrom_Lakh_Year"]).Trim();
				txtSalaryRangeTo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangeto_Lakh_Year"]).Trim();
				txtsalaryfrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangefrom_Lakh_Year"]).Trim();
				txtsalaryto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["SalaryRangeto_Lakh_Year"]).Trim();
				lstReasonForRequi.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
				lstPreferredEmpType.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PID"]).Trim();
				lstPositionBand.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["BAND"]).Trim();

				txtEssentialQualifi.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["EssentialQualification"]).Trim();
				txtDesiredQualifi.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["DesiredQualification"]).Trim();
				txtRequiredExperiencefrom.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Required_ExperienceFrom_Years"]).Trim();
				txtRequiredExperienceto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Required_ExperienceTo_Years"]).Trim();

				lstRecommPerson.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
				GetInterviewer(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));
				txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
				lstInterviewerOne.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
				//listRecruiter.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruiterID"]).Trim();
				lnkuplodedfile.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();

				FileName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();


				txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();

				if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
				{
					if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
					{
						hdCandidate_ID.Value = Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["Candidate_ID"]).Trim();
						GVShortListInterviewer.DataSource = dsRecruitmentDetails.Tables[1];
						GVShortListInterviewer.DataBind();
						PopulateCandidateData();
						DivViewrowWiseCandidateInformation.Visible = true;
						DivCandidateRoundHistory.Visible = true;
					}
					else
					{
						GVShortListInterviewer.DataSource = null;
						GVShortListInterviewer.DataBind();

						GVCandidateRoundHistory.DataSource = null;
						GVCandidateRoundHistory.DataBind();
					}
				}


				DivTrvl.Visible = true;
				trvl_localbtn.Text = "-";
				Div_Locl.Visible = true;
				Div_Locl.Visible = true;
				trvl_localbtn.Text = "-";
				Div_CanDetails.Visible = true;
				btnTra_Details.Text = "-";

			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void GetOffferAPPDetail()
	{
		DataSet dsRecruitmentDetails = new DataSet();
		int IsException = 0;
		try
		{
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = "OfferApprovalEdit";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@empCode", SqlDbType.VarChar);
			spars[2].Value = Session["Empcode"].ToString();
			spars[3] = new SqlParameter("@Recruitment_ReqID", SqlDbType.VarChar);
			spars[3].Value = Convert.ToInt32(hdnReqID.Value); ;
			spars[4] = new SqlParameter("@Candidate_ID", SqlDbType.VarChar);
			spars[4].Value = Convert.ToInt32(hdCandidate_ID.Value);
			dsRecruitmentDetails = spm.getDatasetList(spars, "sp_Offer_Approval_Details");
			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				txtofferDate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Offer_Date"]).Trim();
				txtOfferAppcmt.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comment"]).Trim();
				hdnStatusName.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"]).Trim();
				hdnReqID.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Recruitment_ReqID"]).Trim();
				hdnRecruiteCode.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruiterCode"]).Trim();
				txtOfferno1.Text = Convert.ToDecimal(dsRecruitmentDetails.Tables[0].Rows[0]["OldCTC"]).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txtOfferno2.Text = Convert.ToDecimal(dsRecruitmentDetails.Tables[0].Rows[0]["NewCTC"]).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				IsException = Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["IsException"]);
				lstOfferBand.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["OfferBAND"]).Trim();
				txtExceptionamt.Text = Convert.ToDecimal(Convert.ToDecimal(dsRecruitmentDetails.Tables[0].Rows[0]["OldCTC"]) - Convert.ToDecimal(dsRecruitmentDetails.Tables[0].Rows[0]["NewCTC"])).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				lstRecruitmentCharges.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentCharges"]).Trim();
				txtpositionOffer.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle"].ToString());
				txtProbableJoiningDate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ProbableJoiningDate"]).Trim();
				txt_EmploymentType.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Particulars"]).Trim();
				hdnEmployeeType.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PID"]).Trim();
				if (Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Location_name"].ToString()) != "")
				{
					txtOfferedLocation.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Location_name"].ToString());
				}
				else
				{
					txtOfferedLocation.Text = Convert.ToString(lstPositionLoca.SelectedItem.Text);
				}
				//if (IsException == 1 && "00002726"!= Session["Empcode"].ToString())

				if (IsException == 1)
				{
					chk_exception.Checked = true;
				}
				if (IsException == 1 && "00002726" != Session["Empcode"].ToString() && Convert.ToString(HdnFinalStatus.Value) == "")
				{

					hdnOfferConditionid.Value = "1";
					GetOffer_Approverlist1();
				}
				else
				{
					hdnOfferConditionid.Value = "0";
					GetOffer_Approverlist1();
					//GetOffer_Approverlist();
				}
				if ("00002726" == Session["Empcode"].ToString() || Convert.ToString(HdnFinalStatus.Value) != "")
				{
					//chk_exception.Checked = false;
					chk_exception.Enabled = false;
				}
				txtOfferAppcmt.Enabled = false;
				if (IsException == 0)
				{
					DIV_TransferCanInfo1.Visible = true;
					DIV_TransferCanInfo2.Visible = true;
					DIV_TransferCanInfo3.Visible = true;
					DIV_TransferCanInfo16.Visible = true;
					Link_BtnTransferCandidate.Visible = true;
				}
				if ("00002726" == Session["Empcode"].ToString() || Convert.ToString(HdnFinalStatus.Value) != "")
				{
					DIV_TransferCanInfo1.Visible = false;
					DIV_TransferCanInfo2.Visible = false;
					DIV_TransferCanInfo3.Visible = false;
					DIV_TransferCanInfo16.Visible = false;
					Link_BtnTransferCandidate.Visible = false;
				}
				//txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Status_ID"]).Trim();
				//DateTime Fromdate = DateTime.ParseExact(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"].ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);               
			}

			Div_Oth.Visible = true;
			lnkbtn_expdtls.Text = "-";

			if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
			{
				gvOfferOtherfile.DataSource = dsRecruitmentDetails.Tables[1];
				gvOfferOtherfile.DataBind();
			}
			if (dsRecruitmentDetails.Tables[2].Rows.Count > 0)
			{
				GRDOfferHistory.DataSource = dsRecruitmentDetails.Tables[2];
				GRDOfferHistory.DataBind();
				OfferhistoryS.Visible = true;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	private void GetOffer_Approverlist()
	{
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
		var Dept_id = 0;
		var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
		if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
		{
			Dept_id = Convert.ToInt32(hdndept_Id.Value);
			qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
		}

		int RecrutID = 0, Offer_App_ID = 0;
		DataTable dtapprover = new DataTable();
		RecrutID = Convert.ToString(hdnReqID.Value).Trim() != "" ? Convert.ToInt32(hdnReqID.Value) : 0;
		Offer_App_ID = Convert.ToString(hdnOffer_App_ID.Value).Trim() != "" ? Convert.ToInt32(hdnOffer_App_ID.Value) : 0;
		dtapprover = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID, qtype, getcompSelectedval, Dept_id);
		DgvOfferApprover.DataSource = null;
		DgvOfferApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvOfferApprover.DataSource = dtapprover;
			DgvOfferApprover.DataBind();

			//if (dtapprover.Rows.Count == 1)
			//{
			//    DIV_TransferCanInfo1.Visible = true;
			//    DIV_TransferCanInfo2.Visible = true;
			//    DIV_TransferCanInfo3.Visible = true;
			//    DIV_TransferCanInfo16.Visible = true;
			//    Link_BtnTransferCandidate.Visible = true;
			//}
			//else
			//{
			//    DIV_TransferCanInfo1.Visible = false;
			//    DIV_TransferCanInfo2.Visible = false;
			//    DIV_TransferCanInfo3.Visible = false;
			//    DIV_TransferCanInfo16.Visible = false;
			//    Link_BtnTransferCandidate.Visible = false;
			//}
		}
	}

	private void checkOffer_ApprovalStatus_Submit()
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = "check_OfferAppr_Status";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@empCode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dtOfferStatus = spm.getMobileRemDataList(spars, "sp_Offer_Approval_Details");
			if (dtOfferStatus.Rows.Count == 0)
			{
				Response.Redirect("~/procs/Req_Offer_Index.aspx?itype=Pending");
			}
			if (dtOfferStatus.Rows.Count > 0)
			{
				if (Convert.ToString(dtOfferStatus.Rows[0]["pvappstatus"]) != "Pending")
				{
					Response.Redirect("~/procs/Req_Offer_Index.aspx?itype=Pending");
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	protected void GetOffer_CuurentApprID()
	{
		int capprid;
		string Actions = "";
		DataTable dtCApprID = new DataTable();
		dtCApprID = spm.GetCurrentRec_Offer_ApprID(Convert.ToInt32(hdnOffer_App_ID.Value), hdnEmpCode.Value);
		capprid = (int)dtCApprID.Rows[0]["APPR_ID"];
		Actions = (string)dtCApprID.Rows[0]["Action"];
		hdnCurrentID.Value = capprid.ToString();
		//foreach (DataRow row in dtCApprID.Rows)
		//{
		//	if (row[1].ToString() == "00002726")
		//	{				
		//		chk_exception.Visible = false;
		//	}
		//}
		if (Convert.ToString(hdnCurrentID.Value).Trim() == "")
		{
			lblmessage.Text = "Acton on this Request not yet taken by other approvals";
			return;
		}
		else if (Convert.ToString(Actions).Trim() != "Pending")
		{
			lblmessage.Text = "You already actioned for this request";
			return;
		}
	}

	private void GetCandidateInfoRecruitmentwisedataBind()
	{
		DataSet DSRecwisedatacandidate = new DataSet();
		DSRecwisedatacandidate = spm.GetCanInfoRecruitmentwisedataBind();

		DDLBaseLocationPreference.DataSource = DSRecwisedatacandidate.Tables[0];
		DDLBaseLocationPreference.DataTextField = "BaseLocationPreference";
		DDLBaseLocationPreference.DataValueField = "BaseLocationPreferenceID";
		DDLBaseLocationPreference.DataBind();
		DDLBaseLocationPreference.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLprojecthandled.DataSource = DSRecwisedatacandidate.Tables[1];
		DDLprojecthandled.DataTextField = "TypeofProject";
		DDLprojecthandled.DataValueField = "TypeProjecthandledID";
		DDLprojecthandled.DataBind();
		DDLprojecthandled.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLDomainExperence.DataSource = DSRecwisedatacandidate.Tables[2];
		DDLDomainExperence.DataTextField = "TotalDomainExperience";
		DDLDomainExperence.DataValueField = "DomainExperienceID";
		DDLDomainExperence.DataBind();
		DDLDomainExperence.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLSAPExperence.DataSource = DSRecwisedatacandidate.Tables[3];
		DDLSAPExperence.DataTextField = "SAPExperience";
		DDLSAPExperence.DataValueField = "SAPExperienceID";
		DDLSAPExperence.DataBind();
		DDLSAPExperence.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLImplementationprojectWorkedOn.DataSource = DSRecwisedatacandidate.Tables[4];
		DDLImplementationprojectWorkedOn.DataTextField = "ImplementationProjectsWorkOn";
		DDLImplementationprojectWorkedOn.DataValueField = "ImplementationProjectsWorkOnID";
		DDLImplementationprojectWorkedOn.DataBind();
		DDLImplementationprojectWorkedOn.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLSupportproject.DataSource = DSRecwisedatacandidate.Tables[5];
		DDLSupportproject.DataTextField = "SupportProject";
		DDLSupportproject.DataValueField = "TotalSupportProjectID";
		DDLSupportproject.DataBind();
		DDLSupportproject.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLPhaseWorkimplementation.DataSource = DSRecwisedatacandidate.Tables[6];
		DDLPhaseWorkimplementation.DataTextField = "PhasesImplementationWork";
		DDLPhaseWorkimplementation.DataValueField = "PhasesImplementationWorkId";
		DDLPhaseWorkimplementation.DataBind();
		DDLPhaseWorkimplementation.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLRolesPlaydImplementation.DataSource = DSRecwisedatacandidate.Tables[7];
		DDLRolesPlaydImplementation.DataTextField = "RoleImplementationProject";
		DDLRolesPlaydImplementation.DataValueField = "RoleImplementationProjectID";
		DDLRolesPlaydImplementation.DataBind();
		DDLRolesPlaydImplementation.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLnatureOfIndustryClient.DataSource = DSRecwisedatacandidate.Tables[8];
		DDLnatureOfIndustryClient.DataTextField = "NatureIndustryClient";
		DDLnatureOfIndustryClient.DataValueField = "NatureIndustryClientID";
		DDLnatureOfIndustryClient.DataBind();
		DDLnatureOfIndustryClient.Items.Insert(0, new ListItem("-- Select --", ""));

		DDLCommunicationSkill.DataSource = DSRecwisedatacandidate.Tables[9];
		DDLCommunicationSkill.DataTextField = "CheckCommunicationSkill";
		DDLCommunicationSkill.DataValueField = "CheckCommunicationSkillID";
		DDLCommunicationSkill.DataBind();
		DDLCommunicationSkill.Items.Insert(0, new ListItem("-- Select --", ""));



	}
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		int Offer_App_ID = 0; string Exception = "";
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}
		if (Convert.ToString((txtApprovalcmt.Text).Trim()) == "")
		{
			lblmessage.Text = "Please mention the comment before offer Apporval request";
			return;
		}
		if (chk_exception.Checked)
		{
			Exception = "Yes";
		}
		else
		{
			Exception = "No";
		}
		Offer_App_ID = Convert.ToString(hdnOffer_App_ID.Value).Trim() != "" ? Convert.ToInt32(hdnOffer_App_ID.Value) : 0;
		String strOfferApprovalURL = "", strApproverlist = "";
		strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Approval"]).Trim() + "?Offer_App_ID=" + Offer_App_ID + "&Rec_ID=" + hdnReqID.Value;
		//spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value));
		//strApproverlist = GetReq_offer_Approve_RejectList(Offer_App_ID, Convert.ToInt32(hdnReqID.Value));
		//Rec team mail send
		getRecruiteAssignCode(hdnRecruiteCode.Value);
		getLWPML_HR_ApproverCode("");
		string RequiredByDate = "";
		RequiredByDate = GetRequiredByDate();
		string ProbableJoiningDate = Convert.ToString(txtProbableJoiningDate.Text).Trim() != "" ? Convert.ToString(txtProbableJoiningDate.Text) : "";
		string OfferBand = Convert.ToString(lstOfferBand.SelectedValue).Trim() != "0" ? lstOfferBand.SelectedValue.ToString() : "";
		string RecruitmentCharges = Convert.ToString(lstRecruitmentCharges.SelectedValue).Trim() != "0" ? lstRecruitmentCharges.SelectedValue.ToString() : "";

		string StrCandidateNameandSkillSet = txtName.Text.Trim() + " - " + DDLmainSkillSet.SelectedItem.ToString();

		if (hdnOfferConditionid.Value == "1")
		{
			get_HOD_ForNextApprover();
			get_HOD_Next_Offer_Update();
			//spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value), hdnOfferConditionid.Value);
			spm.Insert_Req_Offer_Approver_Request(hdnNextofferApprovalCode.Value, Convert.ToInt32(hdnNextofferApprovalID.Value), Offer_App_ID);
			get_ISExpException_Approval();
			strApproverlist = GetReq_Next_offer_Approve_RejectList(Offer_App_ID, Convert.ToInt32(hdnReqID.Value), "0");
			//hdnApproverid_LWPPLEmail.Value
			spm.send_mailto_Req_Requisition_Offer_Approver(StrCandidateNameandSkillSet, hdnRecruiteName.Value, hdnNextofferApprovalName.Value, hdnLoginEmpEmail.Value, hdnNextofferApprovalEmail.Value, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text), txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtOfferAppcmt.Text.Trim(), hdnFinalizedDate.Value, hdnApproverid_LWPPLEmail.Value, strOfferApprovalURL, strApproverlist, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate, txtpositionOffer.Text, txtOfferedLocation.Text.Trim());

		}
		else
		{
			Get_HR_Employee_Details();
			Get_HR_Approver();
			if (Convert.ToString(HdnFinalStatus.Value) == "FinalApproval")
			{
				spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value), "0");
				if (hdnEmployeeType.Value == "1")
				{
					InsertCandidateLoginDetail_ELC();
				}
			}
			else
			{
				spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value), "0");
				spm.Insert_Req_Offer_Approver_Request(hdnNextofferApprovalCode.Value, Convert.ToInt32(hdnNextofferApprovalID.Value), Offer_App_ID);
			}
			get_ISExpException_Approval();
			strApproverlist = GetReq_offer_Approve_RejectList(Offer_App_ID, Convert.ToInt32(hdnReqID.Value));
			if (Convert.ToString(HdnFinalStatus.Value) == "FinalApproval")
			{
				//Rec team mail send //hdnApproverid_LWPPLEmail.Value remove recruitment Head 
				spm.send_mailto_Req_Requisition_Offer_Approval_Recruite(StrCandidateNameandSkillSet, hdnRecruiteName.Value, hdnLoginUserName.Value, hflEmailAddress.Value, hdnRecruiteEmailID.Value, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text.Trim()), txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtApprovalcmt.Text, hdnFinalizedDate.Value, strApproverlist, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate, txtpositionOffer.Text,txtOfferedLocation.Text.Trim());
			}
			else
			{
				//hdnApproverid_LWPPLEmail.Value remove recruitment Head 
				spm.send_mailto_Req_Requisition_Offer_Approver(StrCandidateNameandSkillSet, hdnRecruiteName.Value, hdnNextofferApprovalName.Value, hdnLoginEmpEmail.Value, hdnNextofferApprovalEmail.Value, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text), txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtOfferAppcmt.Text.Trim(), hdnFinalizedDate.Value, hdnApproverid_LWPPLEmail.Value, strOfferApprovalURL, strApproverlist, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate, txtpositionOffer.Text, txtOfferedLocation.Text.Trim());

			}
		}
		Response.Redirect("~/procs/Req_Offer_Index.aspx?itype=Pending");
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
	public void getRecruiteAssignCode(string strtype)
	{

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "RecruiterEmpCode";
		spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[2].Value = strtype;
		dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnRecruiteName.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Name"]).Trim();
			hdnRecruiteEmailID.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
			//hdnisApprover_TDCOS.Value = "NA";
		}
	}
	protected string GetReq_offer_Approve_RejectList(int Offer_App_ID, int RecrutID)
	{
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
		var Dept_id = 0;
		var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
		if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
		{
			Dept_id = Convert.ToInt32(hdndept_Id.Value);
			qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
		}
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		dtAppRej = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID, qtype, getcompSelectedval, Dept_id);
		if (dtAppRej.Rows.Count > 0)
		{
			List<DataRow> removeRows = dtAppRej.AsEnumerable().Where(r => r.Field<string>("Status") == "" && r.Field<int>("APPR_ID") == 1).ToList();
			removeRows.ForEach(dtAppRej.Rows.Remove);
			dtAppRej.AcceptChanges();

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

	public void getLWPML_HR_ApproverCode(string strtype)
	{

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "LWPML_HREmpCode";
		spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
		//Travel Desk Approver Code
		//hdnisApprover_TDCOS.Value = "Approver";       
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnApproverid_LWPPLEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();

		}

	}
	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		//Offer Reject Request  
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			string strApproverlist = "", Exception = "", strfalgisexception = "";
			if (confirmValue != "Yes")
			{
				return;
			}
			if (Convert.ToString((txtApprovalcmt.Text).Trim()) == "")
			{
				lblmessage.Text = "Please mention the comment before offer reject request";
				return;
			}
			if (chk_exception.Checked)
			{
				Exception = "Yes";
				if (Session["Empcode"].ToString() == "00002726" || Convert.ToString(HdnFinalStatus.Value) != "")
				{
					strfalgisexception = "0";
				}
				else
				{
					strfalgisexception = "1";
				}
			}
			else
			{
				Exception = "No";
				strfalgisexception = "0";
			}
			getLWPML_HR_ApproverCode("");
			Get_Pre_Employee_EmailID();
			//spm.Rreject_Requisition_Request(Convert.ToInt32(hdnRecruitment_ReqID.Value), hdnEmpCpde.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovercmt.Text);
			spm.Reject_Req_Offer_Request(Convert.ToInt32(hdnOffer_App_ID.Value), hdnEmpCode.Value, Convert.ToInt32(hdnCurrentID.Value), txtApprovalcmt.Text, strfalgisexception);
			strApproverlist = GetReq_offer_Approve_RejectList(Convert.ToInt32(hdnOffer_App_ID.Value), Convert.ToInt32(hdnReqID.Value));
			getRecruiteAssignCode(hdnRecruiteCode.Value);
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();

			string StrCandidateNameandSkillSet = txtName.Text.Trim() + " - " + DDLmainSkillSet.SelectedItem.ToString() + " Rejected against requisition " + Convert.ToString(txtReqNumber.Text.Trim()) + ".";
			string OfferBand = Convert.ToString(lstOfferBand.SelectedValue).Trim() != "0" ? lstOfferBand.SelectedValue.ToString() : "";
			string RecruitmentCharges = Convert.ToString(lstRecruitmentCharges.SelectedValue).Trim() != "0" ? lstRecruitmentCharges.SelectedValue.ToString() : "";
			string ProbableJoiningDate = Convert.ToString(txtProbableJoiningDate.Text).Trim() != "" ? Convert.ToString(txtProbableJoiningDate.Text) : "";
			//spm.send_mail_Req_Offer_Rejection(hdnRecruiteEmailID.Value, hdnLoginUserName.Value, "Recruitment - Reject of offer approval Request " + txtReqNumber.Text + " Request", txtReqNumber.Text, lstPositionName.SelectedItem.Text + " " + lstSkillset.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, txtApprovercmt.Text, "", strApproverlist, txtReqName.Text,"");
			spm.send_mail_Req_Offer_Rejection(hdnRecruiteEmailID.Value, hdnLoginUserName.Value, "Recruitment - Offer", Convert.ToString(txtReqNumber.Text.Trim()), txtReqName.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, hdnFinalizedDate.Value, txtApprovalcmt.Text, hdnApproverid_LWPPLEmail.Value, strApproverlist, StrCandidateNameandSkillSet, hdnRecruiteName.Value, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate, txtpositionOffer.Text);
			//Response.Redirect("~/procs/Req_Offer_Index.aspx?itype=APP");
			Response.Redirect("~/procs/Req_Offer_Index.aspx?itype=Pending");

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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



	protected void lnkbtn_expdtls_Click(object sender, EventArgs e)
	{
		if (Div_Oth.Visible)
		{
			Div_Oth.Visible = false;
			lnkbtn_expdtls.Text = "+";
		}
		else
		{
			Div_Oth.Visible = true;
			lnkbtn_expdtls.Text = "-";
		}
	}

	protected void btnTra_Details_Click(object sender, EventArgs e)
	{
		if (Div_CanDetails.Visible)
		{
			Div_CanDetails.Visible = false;
			btnTra_Details.Text = "+";
		}
		else
		{
			Div_CanDetails.Visible = true;
			btnTra_Details.Text = "-";
		}

	}

	protected void lnkEditView_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdCandidate_ID.Value = Convert.ToString(GVShortListInterviewer.DataKeys[row.RowIndex].Values[0]).Trim();
		PopulateCandidateData();
		DivViewrowWiseCandidateInformation.Visible = true;
		DivCandidateRoundHistory.Visible = true;

	}
	public void PopulateCandidateData()
	{
		try
		{
			string strreqCandidate_ID = hdCandidate_ID.Value;
			dsCandidateData = spm.getSearchCandidateList(strreqCandidate_ID);
			if (dsCandidateData.Tables[0].Rows.Count > 0)
			{
				hdCandidate_ID.Value = dsCandidateData.Tables[0].Rows[0]["Candidate_ID"].ToString();
				txtName.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();
				txtEmail.Text = dsCandidateData.Tables[0].Rows[0]["CandidateEmail"].ToString();
				Txt_CandidateMobile.Text = dsCandidateData.Tables[0].Rows[0]["CandidateMobile"].ToString();
				lstCandidategender.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CandidateGender"].ToString();
				lstMaritalStatus.SelectedValue = dsCandidateData.Tables[0].Rows[0]["Maritalstatus"].ToString();
				// Txt_CandidateCurrentLocation.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentLocation"].ToString();
				Txt_CandidateBirthday.Text = dsCandidateData.Tables[0].Rows[0]["CandidateBirthday"].ToString();
				Txt_CandidatePAN.Text = dsCandidateData.Tables[0].Rows[0]["CandidatePAN"].ToString();
				TxtAadharNo.Text = dsCandidateData.Tables[0].Rows[0]["AdharNo"].ToString();
				lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();

				Txt_lstCVSource.Text = lstCVSource.SelectedItem.Text;

				lnkResume.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				hdCanResume.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				filename = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				lnkuplodedfile.Visible = true;
				PopulateCadidateRecruitmentWiseData();
				if (lstCVSource.SelectedValue == "3")
				{
					Txt_ReferredbyEmpcode.Visible = true;
					Txt_ReferredBy.Visible = false;
					lbltext.Text = "Referred By";
					Txt_ReferredbyEmpcode.Text = dsCandidateData.Tables[0].Rows[0]["EmpName"].ToString();
				}
				else if (lstCVSource.SelectedValue == "4")
				{
					Txt_ReferredbyEmpcode.Visible = false;
					Txt_ReferredBy.Visible = true;
					lbltext.Text = "Others";
					spanIDreferredby.Visible = false;
					Txt_ReferredBy.Text = dsCandidateData.Tables[0].Rows[0]["Others"].ToString();
				}
				else if (lstCVSource.SelectedValue == "1")
				{
					lbltext.Text = "Vendors";
					Txt_ReferredbyEmpcode.Visible = false;
					Txt_ReferredBy.Visible = true;
					spanIDreferredby.Visible = false;
					Txt_ReferredBy.Text = dsCandidateData.Tables[0].Rows[0]["VenderName"].ToString();
				}
				else if (lstCVSource.SelectedValue == "2")
				{
					lbltext.Text = "Job Sites";
					Txt_ReferredbyEmpcode.Visible = false;
					Txt_ReferredBy.Visible = true;
					spanIDreferredby.Visible = false;
					Txt_ReferredBy.Text = dsCandidateData.Tables[0].Rows[0]["JobSitesName"].ToString();
				}

				DDLmainSkillSet.SelectedValue = dsCandidateData.Tables[0].Rows[0]["ModuleId"].ToString();
				Txt_AdditionalSkillset.Text = dsCandidateData.Tables[0].Rows[0]["AdditionalSkillset"].ToString();
				Txt_Comments.Text = dsCandidateData.Tables[0].Rows[0]["Comments"].ToString();
				if (Txt_CandidateBirthday.Text != "")
				{
					string[] strdate;
					string strtoDate = "";
					strdate = Convert.ToString(Txt_CandidateBirthday.Text).Trim().Split('/');
					strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
					DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
					string text = CalculateYourAge(ddt);
					Txt_CandidateAge.Text = text;
				}
				else
				{
					Txt_CandidateAge.Text = "";
				}
				if (dsCandidateData.Tables[3].Rows.Count > 0)
				{
					SpanEducationDetails.Visible = true;
					//SpanEducationDetails1.Visible = true;
					//SpanEducationDetails2.Visible = true;
					GVEducationDetails.DataSource = dsCandidateData.Tables[3];
					GVEducationDetails.DataBind();
				}
				else
				{
					SpanEducationDetails.Visible = false;
					//SpanEducationDetails1.Visible = false;
					//SpanEducationDetails2.Visible = false;
					GVEducationDetails.DataSource = null;
					GVEducationDetails.DataBind();
				}
				if (dsCandidateData.Tables[4].Rows.Count > 0)
				{
					SpanWorkExperiencedetail.Visible = true;
					SpanWorkExperiencedetail1.Visible = true;
					SpanWorkExperiencedetail2.Visible = true;
					GVWorkExperiencedetail.DataSource = dsCandidateData.Tables[4];
					GVWorkExperiencedetail.DataBind();
				}
				else
				{
					SpanWorkExperiencedetail.Visible = false;
					SpanWorkExperiencedetail1.Visible = false;
					SpanWorkExperiencedetail2.Visible = false;
					GVWorkExperiencedetail.DataSource = null;
					GVWorkExperiencedetail.DataBind();
				}

				if (dsCandidateData.Tables[1].Rows.Count > 0)
				{
					gvotherfile.DataSource = dsCandidateData.Tables[1];
					gvotherfile.DataBind();
				}

				DataSet dsCandidateRoundInfo = new DataSet();

				SqlParameter[] spars = new SqlParameter[4];
				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "RecruitmentReq_InterviewerFeedBackEdit";
				spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdnReqID.Value);
				//spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
				//spars[2].Value = Session["Empcode"].ToString();
				spars[2] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
				spars[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
				dsCandidateRoundInfo = spm.getDatasetList(spars, "SP_GetRecruitment_Interviewerfeedback");

				if (dsCandidateRoundInfo.Tables[3].Rows.Count > 0)
				{
					string Result = "Finalized";
					DataRow[] dr3 = dsCandidateRoundInfo.Tables[3].Select("InterviewFeedback='" + Result + "'");
					if (dr3.Length > 0)
					{
						hdnFinalizedDate.Value = dr3[0]["EnterviewDate"].ToString();
					}
					GVCandidateRoundHistory.DataSource = dsCandidateRoundInfo.Tables[3];
					GVCandidateRoundHistory.DataBind();
				}
				else
				{
					GVCandidateRoundHistory.DataSource = null;
					GVCandidateRoundHistory.DataBind();
				}
				if (dsCandidateRoundInfo.Tables[4].Rows.Count > 0)
				{
					GVUploadOtherFilesIRSheet.DataSource = dsCandidateRoundInfo.Tables[4];
					GVUploadOtherFilesIRSheet.DataBind();
				}
				else
				{
					GVUploadOtherFilesIRSheet.DataSource = null;
					GVUploadOtherFilesIRSheet.DataBind();
				}

			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();

			throw;
		}

	}
	static string CalculateYourAge(DateTime Dob)
	{
		DateTime Now = DateTime.Now;
		int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
		DateTime PastYearDate = Dob.AddYears(Years);
		int Months = 0;
		for (int i = 1; i <= 12; i++)
		{
			if (PastYearDate.AddMonths(i) == Now)
			{
				Months = i;
				break;
			}
			else if (PastYearDate.AddMonths(i) >= Now)
			{
				Months = i - 1;
				break;
			}
		}
		int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
		return String.Format(" {0} Year(s) {1} Month(s)", Years, Months, Days);
	}
	public void PopulateCadidateRecruitmentWiseData()
	{
		SqlParameter[] spars = new SqlParameter[40];
		DataSet DS = new DataSet();
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "GetData";
		spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(hdnReqID.Value);
		spars[2] = new SqlParameter("@Recruiter_EmpCode", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.VarChar);
		spars[3].Value = hdCandidate_ID.Value;
		DS = spm.getDatasetList(spars, "sp_TempData_SendForShortlisting");

		// txtName.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();

		if (DS.Tables[1].Rows.Count > 0)
		{
			Txt_NoticePeriodInday.Text = DS.Tables[1].Rows[0]["NoticePeriod"].ToString();
			TxtCurrentCTC_Total.Text = DS.Tables[1].Rows[0]["CurrentCTC"].ToString();
			TxtExpCTC_Total.Text = DS.Tables[1].Rows[0]["ExpectedCTC"].ToString();
			TxtTotalExperienceYrs.Text = DS.Tables[1].Rows[0]["ExperienceYear"].ToString();
			TxtRelevantExpYrs.Text = DS.Tables[1].Rows[0]["RelevantExpYrs"].ToString();
			Txt_ScreenerComments.Text = DS.Tables[1].Rows[0]["Remarks"].ToString();
			Txt_OtherNatureOfIndustryClient.Text = DS.Tables[1].Rows[0]["OtherNatureOfIndustryClient"].ToString();
			if (DS.Tables[1].Rows[0]["CurrentCTC_Fixed"].ToString() == "0.00")
			{
				Txt_CurrentCTC_Fixed.Text = "";
			}
			else
			{
				Txt_CurrentCTC_Fixed.Text = DS.Tables[1].Rows[0]["CurrentCTC_Fixed"].ToString();
			}

			if (DS.Tables[1].Rows[0]["CurrentCTC_Variable"].ToString() == "0.00")
			{
				TxtCurrentCTC_Variable.Text = "";
			}
			else
			{
				TxtCurrentCTC_Variable.Text = DS.Tables[1].Rows[0]["CurrentCTC_Variable"].ToString();
			}

			if (DS.Tables[1].Rows[0]["ExpCTC_Fixed"].ToString() == "0.00")
			{
				TxtExpCTC_Fixed.Text = "";
			}
			else
			{
				TxtExpCTC_Fixed.Text = DS.Tables[1].Rows[0]["ExpCTC_Fixed"].ToString();
			}
			if (DS.Tables[1].Rows[0]["ExpCTC_Variable"].ToString() == "0.00")
			{
				TxtExpCTC_Variable.Text = "";
			}
			else
			{
				TxtExpCTC_Variable.Text = DS.Tables[1].Rows[0]["ExpCTC_Variable"].ToString();
			}
			if (DS.Tables[1].Rows[0]["CTCException"].ToString() != "")
			{
				Chk_Exception_CTC.Checked = Convert.ToBoolean(DS.Tables[1].Rows[0]["CTCException"].ToString());
				if (Chk_Exception_CTC.Checked)
				{
					txtRecruiterRemark.Text = DS.Tables[1].Rows[0]["RecruiterRemark"].ToString();
					ExceptionR.Visible = true;
					CTC1.Visible = true;
					getCTCApproverlist();
				}
				else
				{
					Chk_Exception_CTC.Checked = false;
					txtRecruiterRemark.Text = "";
					ExceptionR.Visible = false;
					Chk_Exception_CTC.Visible = false;
					CTC1.Visible = false;
					DgvApprover.DataSource = null;
					DgvApprover.DataBind();
				}
			}
			else
			{
				Chk_Exception_CTC.Checked = false;
				txtRecruiterRemark.Text = "";
				ExceptionR.Visible = false;
				Chk_Exception_CTC.Visible = false;
				CTC1.Visible = false;
				DgvApprover.DataSource = null;
				DgvApprover.DataBind();
			}
			Txt_BaseLocationcurrentcompany.Text = DS.Tables[1].Rows[0]["LocationCurrentCompany"].ToString();
			Txt_CurrentLocation.Text = DS.Tables[1].Rows[0]["CurrentLocation"].ToString();
			Txt_NativeHomeLocation.Text = DS.Tables[1].Rows[0]["NativeLocation"].ToString();
			Txt_CurrentRoleorganization.Text = DS.Tables[1].Rows[0]["CurrentRoleorganization"].ToString();
			Txt_TravelContraintPandemicSituation.Text = DS.Tables[1].Rows[0]["TravelPandemicSituation"].ToString();
			//  Txt_CurrentRoleorganization.Text = DS.Tables[0].Rows[0]["CandidateCurrentRole"].ToString();
			TxtReasonforBreak.Text = DS.Tables[1].Rows[0]["Reasonforbreak"].ToString();
			Txt_RoleDomaincompany.Text = DS.Tables[1].Rows[0]["RoleDomainCompany"].ToString();
			Txt_lookingforChange.Text = DS.Tables[1].Rows[0]["lookingChangeReason"].ToString();
			if (DS.Tables[1].Rows[0]["CurrentlyOnNotice"].ToString() != "0")
			{
				DDlCurrentlyonnotice.SelectedValue = DS.Tables[1].Rows[0]["CurrentlyOnNotice"].ToString();
			}
			if (DS.Tables[1].Rows[0]["BaseLocationPreferenceID"].ToString() != "0")
			{

				DDLBaseLocationPreference.SelectedValue = DS.Tables[1].Rows[0]["BaseLocationPreferenceID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["Travelanylocations"].ToString() != "0")
			{

				DDLRelocateTravelAnyLocation.SelectedValue = DS.Tables[1].Rows[0]["Travelanylocations"].ToString();
			}
			if (DS.Tables[1].Rows[0]["OpentoTravel"].ToString() != "0")
			{

				DDLOpenToTravel.SelectedValue = DS.Tables[1].Rows[0]["OpentoTravel"].ToString();
			}
			if (DS.Tables[1].Rows[0]["Candidatepayroll"].ToString() != "0")
			{

				DDlpayrollsCompany.SelectedValue = DS.Tables[1].Rows[0]["Candidatepayroll"].ToString();
			}
			if (DS.Tables[1].Rows[0]["Candidateanybreakservice"].ToString() != "0")
			{

				DDLBreakInService.SelectedValue = DS.Tables[1].Rows[0]["Candidateanybreakservice"].ToString();
			}
			if (DS.Tables[1].Rows[0]["TypeProjecthandledID"].ToString() != "0")
			{

				DDLprojecthandled.SelectedValue = DS.Tables[1].Rows[0]["TypeProjecthandledID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["DomainExperienceID"].ToString() != "0")
			{

				DDLDomainExperence.SelectedValue = DS.Tables[1].Rows[0]["DomainExperienceID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["SAPExperienceID"].ToString() != "0")
			{

				DDLSAPExperence.SelectedValue = DS.Tables[1].Rows[0]["SAPExperienceID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["ImplementationProjectsWorkOnID"].ToString() != "0")
			{

				DDLImplementationprojectWorkedOn.SelectedValue = DS.Tables[1].Rows[0]["ImplementationProjectsWorkOnID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["TotalSupportProjectID"].ToString() != "0")
			{

				DDLSupportproject.SelectedValue = DS.Tables[1].Rows[0]["TotalSupportProjectID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["PhasesImplementationWorkId"].ToString() != "0")
			{

				DDLPhaseWorkimplementation.SelectedValue = DS.Tables[1].Rows[0]["PhasesImplementationWorkId"].ToString();
			}
			if (DS.Tables[1].Rows[0]["RoleImplementationProjectID"].ToString() != "0")
			{

				DDLRolesPlaydImplementation.SelectedValue = DS.Tables[1].Rows[0]["RoleImplementationProjectID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["NatureIndustryClientID"].ToString() != "0")
			{
				DDLnatureOfIndustryClient.SelectedValue = DS.Tables[1].Rows[0]["NatureIndustryClientID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["CheckCommunicationSkillID"].ToString() != "0")
			{
				DDLCommunicationSkill.SelectedValue = DS.Tables[1].Rows[0]["CheckCommunicationSkillID"].ToString();
			}
			if (DS.Tables[1].Rows[0]["AgreedBG"].ToString() != "")
			{
				txtAgreedBD.Text = DS.Tables[1].Rows[0]["AgreedBG"].ToString();
			}
			else
			{
				txtAgreedBD.Text = "NA";
			}
			if (DS.Tables[1].Rows[0]["AgreedPDC"].ToString() != "")
			{
				txtAgreedPDC.Text = DS.Tables[1].Rows[0]["AgreedPDC"].ToString();
			}
			else
			{
				txtAgreedPDC.Text = "NA";
			}

			if (DDLnatureOfIndustryClient.SelectedValue == "5")
			{
				Txt_OtherNatureOfIndustryClient.Visible = true;
				SpanTxtOtherNatureOfIndustryClient.Visible = true;
				SpanTxtOtherNatureOfIndustryClient1.Visible = true;
			}
			else
			{
				Txt_OtherNatureOfIndustryClient.Visible = false;
				SpanTxtOtherNatureOfIndustryClient.Visible = false;
				SpanTxtOtherNatureOfIndustryClient1.Visible = false;
			}
			if (DDLBreakInService.SelectedValue == "1")
			{
				TxtReasonforBreak.Visible = true;
				SpanTxtReasonforBreak.Visible = true;
				SpanTxtReasonforBreak1.Visible = true;
			}
			else
			{
				TxtReasonforBreak.Visible = false;
				SpanTxtReasonforBreak.Visible = false;
				SpanTxtReasonforBreak1.Visible = false;
			}
		}
		else
		{

		}

	}
	private void getCTCApproverlist()
	{
		DataTable dtapprover = new DataTable();
		int Recruitment_ReqID = 0, CTCID = 0, Candidate_ID = 0;
		Recruitment_ReqID = Convert.ToString(hdnReqID.Value).Trim() != "" ? Convert.ToInt32(hdnReqID.Value) : 0;
		//CTCID = Convert.ToString(hdnCTCID.Value).Trim() != "" ? Convert.ToInt32(hdnCTCID.Value) : 0;
		Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
		dtapprover = spm.CTC_Exception_Approval("GET_CTC_Exception_APP", Recruitment_ReqID, Candidate_ID, "", 0, CTCID, 0);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}
	protected void lstCVSource_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lstCVSource.SelectedValue == "3")
		{
			Txt_ReferredbyEmpcode.Visible = true;
			Txt_ReferredBy.Visible = false;
		}
		else
		{
			Txt_ReferredbyEmpcode.Visible = false;
			Txt_ReferredBy.Visible = true;
		}
	}
	protected void lnkIRsheetView_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdCandidate_ID.Value = Convert.ToString(GVCandidateRoundHistory.DataKeys[row.RowIndex].Values[0]).Trim();
		getIrSheetSummary();
		this.ModalPopupExtenderIRSheet.Show();
	}
	private void getIrSheetSummary()
	{

		txtRec_No.Text = txtReqNumber.Text;
		txtCandidateName.Text = txtName.Text;
		txtPositionInterviwed.Text = lstSkillset.SelectedItem.Text;
		txttotalExperince.Text = TxtTotalExperienceYrs.Text;
		txtRelevantExp.Text = TxtRelevantExpYrs.Text;
		txtpostionTitle.Text = lstPositionName.SelectedItem.Text;
		//this.DgvIrSheetSummary.Columns.RemoveAt(this.DgvIrSheetSummary.Columns.Count - 1);
		DgvIrSheetSummary.DataSource = null;
		DgvIrSheetSummary.DataBind();

		DataTable dtMerged = new DataTable();
		dtIrSheetReport = new DataSet();
		dtIrSheetReport = spm.Get_Rec_Recruit_IrSheetDetails("GetIrSheetSummary", Convert.ToInt32(hdnReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
		if (dtIrSheetReport.Tables[0].Rows.Count > 0)
		{
			dtMerged = new DataTable();
			DTInterviews = new DataTable();
			DTMaintable = new DataTable();

			if (dtIrSheetReport.Tables[3].Rows.Count > 0)
			{
				DTInterviews = GenerateColumn(dtIrSheetReport.Tables[3]);
			}
			if (dtIrSheetReport.Tables[1].Rows.Count > 0)
			{
				DTMaintable = getData(dtIrSheetReport.Tables[0], dtIrSheetReport.Tables[1]);
			}
			if (dtIrSheetReport.Tables[3].Rows.Count > 0)
			{
				dtMerged = MergeTables(DTMaintable, DTInterviews);
			}
			if (dtIrSheetReport.Tables[2].Rows.Count > 0)
			{
				GrdIRIntSummary.DataSource = dtIrSheetReport.Tables[2];
				GrdIRIntSummary.DataBind();
			}
			int cou = DgvIrSheetSummary.Columns.Count - 1;
			for (int i = cou; 1 < i; i--)
			{
				DgvIrSheetSummary.Columns.RemoveAt(i);
			}

			string RoundName = "";
			int B = 0, C = 0;
			if (dtIrSheetReport.Tables[3].Rows.Count > 0)

			{
				foreach (DataColumn col in DTInterviews.Columns)
				{
					if (C == 2)
					{
						B++;
						C = 0;
					}
					RoundName = NewDT.Rows[B]["InterviewRound"].ToString();
					var NewHeaderName = Regex.Replace(col.ColumnName, @"[\d-]", string.Empty);
					BoundField bfield = new BoundField();
					bfield.DataField = col.ColumnName;
					bfield.HeaderText = NewHeaderName + " - " + RoundName;
					bfield.ItemStyle.BorderColor = Color.Navy;
					DgvIrSheetSummary.Columns.Add(bfield);
					C++;
				}
			}

			DgvIrSheetSummary.DataSource = dtMerged;
			this.DgvIrSheetSummary.DataBind();
			this.updatePanel.Update();

		}
	}
	private DataTable GenerateColumn(DataTable dt)
	{
		NewDT = new DataTable();
		NewDTValue = new DataTable();
		DataView view = new DataView(dt);
		NewDT = view.ToTable(true, "InterviewRound");

		DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
		for (int i = 0; i < NewDT.Rows.Count; i++)
		{
			dcol = new DataColumn("Observation" + i, typeof(System.String));
			NewDTValue.Columns.Add(dcol);
			dcol = new DataColumn("Rating" + i, typeof(System.String));
			NewDTValue.Columns.Add(dcol);
		}
		DataRow dr;
		int m = 0;
		int A = 1;
		int Pre = 0, Next = 0;
		dr = NewDTValue.NewRow();
		dr["Observation" + m] = "";
		dr["Rating" + m] = "";
		NewDTValue.Rows.Add(dr);
		//m++;
		for (int j = 0; j < dt.Rows.Count; j++)
		{

			if (Convert.ToInt32(dt.Rows[j]["IsRound"]) == 1)
			{
				dr = NewDTValue.NewRow();
				dr["Observation" + m] = Convert.ToString(dt.Rows[j]["Observation_Remarks"]).Trim();
				dr["Rating" + m] = Convert.ToString(dt.Rows[j]["RatingName"]).Trim();
				NewDTValue.Rows.Add(dr);
			}
			else
			{
				Pre = Convert.ToInt32(dt.Rows[j - 1]["IsRound"]);
				Next = Convert.ToInt32(dt.Rows[j]["IsRound"]);
				if (Pre != Next)
				{
					m++;
					A = 1;
				}
				NewDTValue.Rows[A]["Observation" + m] = Convert.ToString(dt.Rows[j]["Observation_Remarks"]).Trim();
				NewDTValue.Rows[A]["Rating" + m] = Convert.ToString(dt.Rows[j]["RatingName"]).Trim();
				A++;
			}
		}

		return NewDTValue;
	}
	public DataTable getData(DataTable dt1, DataTable dt2)
	{
		try
		{
			int MainID = 0;
			dtMerge = new DataTable();
			dtMerge.Columns.Add("Main_Type_ID", typeof(string));
			dtMerge.Columns.Add("Heading", typeof(string));
			dtMerge.Columns.Add("Ishedeing", typeof(string));
			dtMerge.Columns.Add("SubType_ID", typeof(string));
			dtMerge.Columns.Add("SubType_Rating", typeof(string));
			DataRow dr;
			for (int i = 0; i < dt1.Rows.Count; i++)
			{
				dr = dtMerge.NewRow();
				dr["Main_Type_ID"] = Convert.ToString(dt1.Rows[i]["Main_Type_ID"]).Trim();
				dr["Heading"] = Convert.ToString(dt1.Rows[i]["Heading"]).Trim();
				dr["Ishedeing"] = 0;
				dr["SubType_ID"] = 0;
				dr["SubType_Rating"] = 'N';
				dtMerge.Rows.Add(dr);
				MainID = Convert.ToInt32(dt1.Rows[i]["Main_Type_ID"]);
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					//var valueFormain2
					if (MainID == Convert.ToInt32(dt2.Rows[j]["Main_Type_ID"]))
					{
						dr = dtMerge.NewRow();
						dr["Main_Type_ID"] = Convert.ToString(dt2.Rows[j]["Main_Type_ID"]).Trim();
						dr["Heading"] = Convert.ToString(dt2.Rows[j]["Heading"]).Trim();
						dr["Ishedeing"] = Convert.ToString(dt2.Rows[j]["Ishedeing"]).Trim();
						dr["SubType_ID"] = Convert.ToString(dt2.Rows[j]["SubType_ID"]).Trim();
						dr["SubType_Rating"] = Convert.ToString(dt2.Rows[j]["SubType_Rating"]).Trim();
						dtMerge.Rows.Add(dr);
					}
				}
			}
			//DgvIrSheet.DataSource = dtMerge;
			//DgvIrSheet.DataBind();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		return dtMerge;
	}
	public static DataTable MergeTables(DataTable baseTable, params DataTable[] additionalTables)
	{
		DataTable merged = baseTable;
		foreach (DataTable dt in additionalTables)
		{
			merged = AddTable(merged, dt);
		}
		return merged;
	}
	public static DataTable AddTable(DataTable baseTable, DataTable additionalTable)
	{
		// Build combined table columns
		DataTable merged = baseTable.Clone();                  // Include all columns from t1 in result.
		foreach (DataColumn col in additionalTable.Columns)
		{
			string newColumnName = col.ColumnName;
			merged.Columns.Add(newColumnName, col.DataType);
		}
		// Add all rows from both tables
		var bt = baseTable.AsEnumerable();
		var at = additionalTable.AsEnumerable();
		var mergedRows = bt.Zip(at, (r1, r2) => r1.ItemArray.Concat(r2.ItemArray).ToArray());
		foreach (object[] rowFields in mergedRows)
		{
			merged.Rows.Add(rowFields);
		}
		return merged;
	}
	protected void DgvIrSheetSummary_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string lsDataKeyValue = DgvIrSheetSummary.DataKeys[e.Row.RowIndex].Values[1].ToString();
			string lsDataKeyValue2 = DgvIrSheetSummary.DataKeys[e.Row.RowIndex].Values[2].ToString();

			if (e.Row.Cells[1].Text.Trim() == "&nbsp;")
			{
				e.Row.Visible = false;
			}

			if (Convert.ToString(lsDataKeyValue).Trim() == "0")
			{
				e.Row.Cells[1].Style["font-size"] = "16px";
				e.Row.Cells[1].Style["color"] = "#000066";
			}

			if (Convert.ToString(lsDataKeyValue).Trim() == "1")
			{
				e.Row.Cells[1].Font.Bold = true;
				e.Row.Cells[1].Style["font-size"] = "13px";
				//e.Row.BackColor = Color.Teal;//HotPink  Teal
				e.Row.Cells[1].ForeColor = Color.DodgerBlue;
			}


		}
	}

	protected void chk_exception_CheckedChanged(object sender, EventArgs e)
	{
		if (chk_exception.Checked)
		{
			hdnOfferConditionid.Value = "1";
			DIV_TransferCanInfo1.Visible = false;
			DIV_TransferCanInfo2.Visible = false;
			DIV_TransferCanInfo3.Visible = false;
			DIV_TransferCanInfo16.Visible = false;
			Link_BtnTransferCandidate.Visible = false;
			GetOffer_Approverlist1();
		}
		else
		{
			hdnOfferConditionid.Value = "0";
			GetOffer_Approverlist1();
			DIV_TransferCanInfo1.Visible = true;
			DIV_TransferCanInfo2.Visible = true;
			DIV_TransferCanInfo3.Visible = true;
			DIV_TransferCanInfo16.Visible = true;
			Link_BtnTransferCandidate.Visible = true;
		}

	}
	private void GetOffer_Approverlist1()
	{
		try
		{
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
			var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
			var Dept_id = 0;
			var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
			if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
			{
				Dept_id = Convert.ToInt32(hdndept_Id.Value);
				qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
			}
			DataTable dsAppDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "REQUISITIONOFFER";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@REQID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(hdnReqID.Value);
			spars[3] = new SqlParameter("@OfferCondition", SqlDbType.VarChar);
			spars[3].Value = hdnOfferConditionid.Value;
			spars[4] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			spars[5] = new SqlParameter("@comp_code", SqlDbType.VarChar);
			spars[5].Value = Convert.ToString(hdncomp_code.Value).Trim();
			spars[6] = new SqlParameter("@Dept_Id", SqlDbType.Int);
			spars[6].Value = Dept_id;
			dsAppDetails = spm.getMobileRemDataList(spars, qtype);
			DgvOfferApprover.DataSource = null;
			DgvOfferApprover.DataBind();
			if (dsAppDetails.Rows.Count > 0)
			{
				//var table = dsAppDetails.AsEnumerable().Where(r => r.Field<string>("Status") != "").CopyToDataTable();
				List<DataRow> removeRows = dsAppDetails.AsEnumerable().Where(r => r.Field<string>("Status") == "" && r.Field<int>("APPR_ID") == 1).ToList();
				removeRows.ForEach(dsAppDetails.Rows.Remove);
				dsAppDetails.AcceptChanges();
				DgvOfferApprover.DataSource = dsAppDetails;
				DgvOfferApprover.DataBind();
				for (int i = 0; i < dsAppDetails.Rows.Count; i++)
				{
					if (i == 0)
					{
						//if (dsAppDetails.Rows[0]["Status"].ToString() == "Pending")
						//{
						//	DIV_TransferCanInfo1.Visible = true;
						//	DIV_TransferCanInfo2.Visible = true;
						//	DIV_TransferCanInfo3.Visible = true;
						//	DIV_TransferCanInfo16.Visible = true;
						//	Link_BtnTransferCandidate.Visible = true;
						//	break;
						//}
					}
					else
					{
						DIV_TransferCanInfo1.Visible = false;
						DIV_TransferCanInfo2.Visible = false;
						DIV_TransferCanInfo3.Visible = false;
						DIV_TransferCanInfo16.Visible = false;
						Link_BtnTransferCandidate.Visible = false;
					}
				}
			}
		}

		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}



	private void get_HOD_ForNextApprover()
	{
		try
		{
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
			var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
			var Dept_id = 0;
			var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
			if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
			{
				Dept_id = Convert.ToInt32(hdndept_Id.Value);
				qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
			}
			DataTable dsNextAppDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "get_next_Offer_Approver";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@REQID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(hdnReqID.Value);
			spars[3] = new SqlParameter("@OfferCondition", SqlDbType.VarChar);
			spars[3].Value = hdnOfferConditionid.Value;
			spars[4] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(hdnEmpCode.Value).Trim();

			spars[5] = new SqlParameter("@comp_code", SqlDbType.VarChar);
			spars[5].Value = Convert.ToString(hdncomp_code.Value).Trim();
			spars[6] = new SqlParameter("@Dept_Id", SqlDbType.Int);
			spars[6].Value = Convert.ToInt32(Dept_id);

			dsNextAppDetails = spm.getMobileRemDataList(spars, qtype);
			if (dsNextAppDetails.Rows.Count > 0)
			{
				hdnNextofferApprovalName.Value = Convert.ToString(dsNextAppDetails.Rows[0]["Emp_Name"]).Trim();
				hdnNextofferApprovalEmail.Value = Convert.ToString(dsNextAppDetails.Rows[0]["Emp_Emailaddress"]).Trim();
				hdnNextofferApprovalCode.Value = Convert.ToString(dsNextAppDetails.Rows[0]["A_EMP_CODE"]).Trim();
				hdnNextofferApprovalID.Value = Convert.ToString(dsNextAppDetails.Rows[0]["APPR_ID"]).Trim();
			}
		}

		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void get_HOD_Next_Offer_Update()
	{
		try
		{
			DataTable dsNextAppDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
			spars[0].Value = "UPDATE";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@OfferCondition", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnOfferConditionid.Value);
			spars[3] = new SqlParameter("@appr_comments", SqlDbType.VarChar);
			spars[3].Value = txtApprovalcmt.Text;
			spars[4] = new SqlParameter("@APPR_EMP_CODE", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			spars[5] = new SqlParameter("@Action", SqlDbType.VarChar);
			spars[5].Value = "Approved";
			spars[6] = new SqlParameter("@APPR_ID", SqlDbType.Int);
			spars[6].Value = Convert.ToInt32(hdnCurrentID.Value);
			dsNextAppDetails = spm.getMobileRemDataList(spars, "SP_Save_Req_Offer_App_Status");
			if (dsNextAppDetails.Rows.Count > 0)
			{
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	protected string GetReq_Next_offer_Approve_RejectList(int Offer_App_ID, int RecrutID, string OfferCondition)
	{
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
		var Dept_id = 0;
		var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
		if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
		{
			Dept_id = Convert.ToInt32(hdndept_Id.Value);
			qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
		}
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		SqlParameter[] spars = new SqlParameter[7];
		spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
		spars[0].Value = "REQUISITIONOFFER";
		spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
		spars[1].Value = Offer_App_ID;
		spars[2] = new SqlParameter("@OfferCondition", SqlDbType.VarChar);
		spars[2].Value = Convert.ToString(OfferCondition);
		spars[3] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		spars[3].Value = Convert.ToString(hdnEmpCode.Value).Trim();
		spars[4] = new SqlParameter("@REQID", SqlDbType.Int);
		spars[4].Value = RecrutID;
		spars[5] = new SqlParameter("@comp_code", SqlDbType.VarChar);
		spars[5].Value = getcompSelectedval;
		spars[6] = new SqlParameter("@Dept_Id", SqlDbType.Int);
		spars[6].Value = Dept_id;
		dtAppRej = spm.getMobileRemDataList(spars, qtype);
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

	private void get_ISExpException_Approval()
	{
		try
		{
			int exception = 0;
			if (chk_exception.Checked)
			{
				exception = 1;
			}
			else
			{
				exception = 0;
			}


			DataTable dsNextAppDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = "UPDATE_IsException";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@IsException", SqlDbType.Int);
			spars[2].Value = exception;
			spars[3] = new SqlParameter("@empCode", SqlDbType.VarChar);
			spars[3].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dsNextAppDetails = spm.getMobileRemDataList(spars, "sp_Offer_Approval_Details");
			if (dsNextAppDetails.Rows.Count > 0)
			{
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	#region Transfercode -- harshad 

	public void GetlstRequisitionNo()
	{
		SqlParameter[] spars = new SqlParameter[3];
		DataSet DS = new DataSet();
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "sp_Req_ReQuisitionNo";
		DS = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");

		if (DS.Tables[0].Rows.Count > 0)
		{
			DDLsearchRequisitionnumber.DataSource = DS.Tables[0];
			DDLsearchRequisitionnumber.DataTextField = "RequisitionNumber";
			DDLsearchRequisitionnumber.DataValueField = "Recruitment_ReqID";
			DDLsearchRequisitionnumber.DataBind();
			DDLsearchRequisitionnumber.Items.Insert(0, new ListItem("Select Requisition Number", "0")); //

			DataRow[] dr3 = DS.Tables[0].Select("Recruitment_ReqID='" + hdnReqID.Value + "'");
			if (dr3.Length > 0)
			{ //  string itemValue = "4";
				if (DDLsearchRequisitionnumber.Items.FindByValue(hdnReqID.Value) != null)
				{
					string itemText = DDLsearchRequisitionnumber.Items.FindByValue(hdnReqID.Value).Text;
					ListItem li = new ListItem();
					li.Text = itemText;
					li.Value = hdnReqID.Value;
					DDLsearchRequisitionnumber.Items.Remove(li);
				}
			}
		}
	}
	public void SearchGetPositionName()
	{
		DataTable dtPositionName = new DataTable();
		dtPositionName = spm.GetRecruitment_PositionTitle();
		if (dtPositionName.Rows.Count > 0)
		{
			lstPositionTitleSearch.DataSource = dtPositionName;
			lstPositionTitleSearch.DataTextField = "PositionTitle";
			lstPositionTitleSearch.DataValueField = "PositionTitle_ID";
			lstPositionTitleSearch.DataBind();
			lstPositionTitleSearch.Items.Insert(0, new ListItem("Select Position", "0"));
		}
	}
	public void SearchGetSkillsetName()
	{
		DataTable dtSkillset = new DataTable();
		dtSkillset = spm.GetRecruitment_SkillsetName();
		if (dtSkillset.Rows.Count > 0)
		{
			LstSkillSetSearch.DataSource = dtSkillset;
			LstSkillSetSearch.DataTextField = "ModuleDesc";
			LstSkillSetSearch.DataValueField = "ModuleId";
			LstSkillSetSearch.DataBind();
			LstSkillSetSearch.Items.Insert(0, new ListItem("Select Skillset", "0"));

		}
	}
	public void SearchGetCompany_Location()
	{
		DataTable lstPosition = new DataTable();
		lstPosition = spm.GetRecruitment_Req_company_Location();
		if (lstPosition.Rows.Count > 0)
		{
			LstLocationSearch.DataSource = lstPosition;
			LstLocationSearch.DataTextField = "Location_name";
			LstLocationSearch.DataValueField = "comp_code";
			LstLocationSearch.DataBind();
			LstLocationSearch.Items.Insert(0, new ListItem("Select Location", "0"));

		}
	}
	public void SearchGetlstPositionBand()
	{
		DataTable dtPositionBand = new DataTable();
		dtPositionBand = spm.GetRecruitment_Req_HRMS_BAND_MASTER();
		if (dtPositionBand.Rows.Count > 0)
		{
			LstbandSearch.DataSource = dtPositionBand;
			LstbandSearch.DataTextField = "BAND";
			LstbandSearch.DataValueField = "BAND";
			LstbandSearch.DataBind();
			LstbandSearch.Items.Insert(0, new ListItem("Select BAND", "0"));
		}
	}

	protected void Link_TransferHideUnhide_Click(object sender, EventArgs e)
	{
		if (Link_TransferHideUnhide.Text == "+")
		{
			Link_TransferHideUnhide.Text = "-";
			DIV_TransferCanInfo1.Visible = true;
			DIV_TransferCanInfo2.Visible = true;
			DIV_TransferCanInfo3.Visible = true;
			DIV_TransferCanInfo4.Visible = true;
			DIV_TransferCanInfo5.Visible = true;
			DIV_TransferCanInfo6.Visible = true;
			DIV_TransferCanInfo7.Visible = true;
			DIV_TransferCanInfo8.Visible = true;
			DIV_TransferCanInfo9.Visible = true;
			DIV_TransferCanInfo10.Visible = true;
			DIV_TransferCanInfo11.Visible = true;
			DIV_TransferCanInfo12.Visible = true;
			DIV_TransferCanInfo13.Visible = true;
			DIV_TransferCanInfo14.Visible = true;
			DIV_TransferCanInfo15.Visible = true;
			DIV_TransferCanInfo16.Visible = true;
		}
		else if (Link_TransferHideUnhide.Text == "-")
		{
			Link_TransferHideUnhide.Text = "+";
			DIV_TransferCanInfo1.Visible = true;
			DIV_TransferCanInfo2.Visible = true;
			DIV_TransferCanInfo3.Visible = true;
			DIV_TransferCanInfo4.Visible = false;
			DIV_TransferCanInfo5.Visible = false;
			DIV_TransferCanInfo6.Visible = false;
			DIV_TransferCanInfo7.Visible = false;
			DIV_TransferCanInfo8.Visible = false;
			DIV_TransferCanInfo9.Visible = false;
			DIV_TransferCanInfo10.Visible = false;
			DIV_TransferCanInfo11.Visible = false;
			DIV_TransferCanInfo12.Visible = false;
			DIV_TransferCanInfo13.Visible = false;
			DIV_TransferCanInfo14.Visible = false;
			DIV_TransferCanInfo15.Visible = false;
			DIV_TransferCanInfo16.Visible = false;
		}


	}


	protected void LinkBtnSearchTransferfilter_Click(object sender, EventArgs e)
	{
		SqlParameter[] spars = new SqlParameter[6];
		DataSet DS = new DataSet();
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "Rec_TransferCandidateSearch";
		spars[1] = new SqlParameter("@PositionTitleID", SqlDbType.VarChar);
		spars[1].Value = lstPositionTitleSearch.SelectedValue;
		spars[2] = new SqlParameter("@SkillSetID", SqlDbType.VarChar);
		spars[2].Value = LstSkillSetSearch.SelectedValue;
		spars[3] = new SqlParameter("@LocationID", SqlDbType.VarChar);
		spars[3].Value = LstLocationSearch.SelectedValue;
		spars[4] = new SqlParameter("@BandID", SqlDbType.VarChar);
		spars[4].Value = LstbandSearch.SelectedValue;
		DS = spm.getDatasetList(spars, "SP_Get_Rec_TransferCandidate_OtherRequisition");

		if (DS.Tables[0].Rows.Count > 0)
		{
			DDLsearchRequisitionnumber.DataSource = DS.Tables[0];
			DDLsearchRequisitionnumber.DataTextField = "RequisitionNumber";
			DDLsearchRequisitionnumber.DataValueField = "Recruitment_ReqID";
			DDLsearchRequisitionnumber.DataBind();
			DDLsearchRequisitionnumber.Items.Insert(0, new ListItem("Select Requisition Number", "0"));

			DataRow[] dr3 = DS.Tables[0].Select("Recruitment_ReqID='" + hdnReqID.Value + "'");
			if (dr3.Length > 0)
			{ //  string itemValue = "4";
				if (DDLsearchRequisitionnumber.Items.FindByValue(hdnReqID.Value) != null)
				{
					string itemText = DDLsearchRequisitionnumber.Items.FindByValue(hdnReqID.Value).Text;
					ListItem li = new ListItem();
					li.Text = itemText;
					li.Value = hdnReqID.Value;
					DDLsearchRequisitionnumber.Items.Remove(li);
				}
			}
		}
		else
		{
			Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "alert('No records found')", true);
		}
	}

	protected void LinkBtnSearchTransferfilterClear_Click(object sender, EventArgs e)
	{
		lstPositionTitleSearch.SelectedValue = "0";
		LstSkillSetSearch.SelectedValue = "0";
		LstLocationSearch.SelectedValue = "0";
		LstbandSearch.SelectedValue = "0";
		GetlstRequisitionNo();
	}

	#endregion



	protected void Link_BtnTransferCandidate_Click(object sender, EventArgs e)
	{

		lblmessage.Text = "";
		int Offer_App_ID = 0; string Exception = "";
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}
		if (Convert.ToString((txtApprovalcmt.Text).Trim()) == "")
		{
			lblmessage.Text = "Please mention the comment before offer Apporval request";
			return;
		}
		if (DDLsearchRequisitionnumber.SelectedValue == "0" || DDLsearchRequisitionnumber.SelectedValue == "")
		{
			lblmessage.Text = "Please select requisition number";
			return;
		}

		SqlParameter[] sparsduplicate = new SqlParameter[6];
		DataSet DSdup = new DataSet();
		sparsduplicate[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		sparsduplicate[0].Value = "Rec_TransferCandidateDuplicateCheck";
		sparsduplicate[1] = new SqlParameter("@CurrentRecruitment_ReqID", SqlDbType.Int);
		sparsduplicate[1].Value = Convert.ToInt32(hdnReqID.Value);
		sparsduplicate[2] = new SqlParameter("@CandidateID", SqlDbType.Int);
		sparsduplicate[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
		sparsduplicate[3] = new SqlParameter("@NewRecruitment_ReqID", SqlDbType.Int);
		sparsduplicate[3].Value = Convert.ToInt32(DDLsearchRequisitionnumber.SelectedValue);
		sparsduplicate[4] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
		sparsduplicate[4].Value = Session["Empcode"].ToString();
		string strempcod = Session["Empcode"].ToString();
		DSdup = spm.getDatasetList(sparsduplicate, "SP_Get_Rec_TransferCandidate_OtherRequisition");
		if (DSdup.Tables.Count != 0)
		{
			if (DSdup.Tables[0].Rows.Count > 0)
			{
				lblmessage.Text = "";
			}
		}
		else
		{
			lblmessage.Text = "Can not transfer Requisition.Already transfered same candidate";
			return;
		}


		if (lblmessage.Text == "")
		{

			if (chk_exception.Checked)
			{
				Exception = "Yes";
			}
			else
			{
				Exception = "No";
			}
			Offer_App_ID = Convert.ToString(hdnOffer_App_ID.Value).Trim() != "" ? Convert.ToInt32(hdnOffer_App_ID.Value) : 0;
			String strOfferApprovalURL = "", strApproverlist = "";
			//strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Approval"]).Trim() + "?Offer_App_ID=" + Offer_App_ID + "&Rec_ID=" + hdnReqID.Value;
			//spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value));
			//strApproverlist = GetReq_offer_Approve_RejectList(Offer_App_ID, Convert.ToInt32(hdnReqID.Value));
			//Rec team mail send
			getRecruiteAssignCode(hdnRecruiteCode.Value);
			getLWPML_HR_ApproverCode("");
			string RequiredByDate = "";
			RequiredByDate = GetRequiredByDate();
			string ProbableJoiningDate = Convert.ToString(txtProbableJoiningDate.Text).Trim() != "" ? Convert.ToString(txtProbableJoiningDate.Text) : "";
			string OfferBand = Convert.ToString(lstOfferBand.SelectedValue).Trim() != "0" ? lstOfferBand.SelectedValue.ToString() : "";
			string RecruitmentCharges = Convert.ToString(lstRecruitmentCharges.SelectedValue).Trim() != "0" ? lstRecruitmentCharges.SelectedValue.ToString() : "";
			string StrCandidateNameandSkillSet = txtName.Text.Trim() + " - " + DDLmainSkillSet.SelectedItem.ToString();

			if (hdnOfferConditionid.Value == "1")
			{
				//get_HOD_ForNextApprover();
				//get_HOD_Next_Offer_Update();
				////spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value), hdnOfferConditionid.Value);
				//spm.Insert_Req_Offer_Approver_Request(hdnNextofferApprovalCode.Value, Convert.ToInt32(hdnNextofferApprovalID.Value), Offer_App_ID);
				//get_ISExpException_Approval();
				//strApproverlist = GetReq_Next_offer_Approve_RejectList(Offer_App_ID, Convert.ToInt32(hdnReqID.Value), "0");
				//spm.send_mailto_Req_Requisition_Offer_Approver(StrCandidateNameandSkillSet, hdnRecruiteName.Value, hdnNextofferApprovalName.Value, hdnLoginEmpEmail.Value, hdnNextofferApprovalEmail.Value, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text), txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtOfferAppcmt.Text.Trim(), hdnFinalizedDate.Value, hdnApproverid_LWPPLEmail.Value, strOfferApprovalURL, strApproverlist, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate);

			}
			else
			{
				Get_HR_Approver();
				spm.Update_Req_Offer_AppRequest(Convert.ToInt32(hdnOffer_App_ID.Value), "Approved", txtApprovalcmt.Text, Convert.ToString(hdnEmpCode.Value).Trim(), Convert.ToInt32(hdnCurrentID.Value), "0");
				spm.Insert_Req_Offer_Approver_Request(hdnNextofferApprovalCode.Value, Convert.ToInt32(hdnNextofferApprovalID.Value), Offer_App_ID);
				get_ISExpException_Approval();
				//Rec team mail send
				//spm.send_mailto_Req_Requisition_Offer_Approval_Recruite(StrCandidateNameandSkillSet, hdnRecruiteName.Value, hdnLoginUserName.Value, hflEmailAddress.Value, hdnRecruiteEmailID.Value, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text.Trim()), txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtApprovalcmt.Text, hdnFinalizedDate.Value, strApproverlist, hdnApproverid_LWPPLEmail.Value, txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate);

			}
			SqlParameter[] spars = new SqlParameter[6];
			DataSet DS = new DataSet();
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "Rec_TransferCandidate";
			spars[1] = new SqlParameter("@CurrentRecruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnReqID.Value);
			spars[2] = new SqlParameter("@CandidateID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
			spars[3] = new SqlParameter("@NewRecruitment_ReqID", SqlDbType.Int);
			spars[3].Value = Convert.ToInt32(DDLsearchRequisitionnumber.SelectedValue);
			spars[4] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[4].Value = Session["Empcode"].ToString();
			//string strempcodeee = Session["Empcode"].ToString();
			DS = spm.getDatasetList(spars, "SP_Get_Rec_TransferCandidate_OtherRequisition");

			if (DS.Tables.Count != 0)
			{
				if (DS.Tables[0].Rows.Count > 0)
				{
					string mailsubject = "";
					string mailcontain = "";
					// mailsubject = "Recruitment - Candidate Transferred";
					mailsubject = "Recruitment - Candidate Transferred against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate is transferred. Current requisition to other requisition";

					string StrFrommail = DS.Tables[0].Rows[0]["Emp_EmailaddressRecruiter"].ToString();
					string StrTomail = DS.Tables[0].Rows[0]["Emp_EmailaddressRecruiterhead"].ToString();
					string StrName = DS.Tables[0].Rows[0]["Emp_NameRecruiterHead"].ToString();
					string StrCCmail = DS.Tables[0].Rows[0]["Emp_EmailaddressInterviewSheduler"].ToString();

					string StrNewRecDepartment_Name = DS.Tables[1].Rows[0]["Department_Name"].ToString();
					string StrNewReccompany_name = DS.Tables[1].Rows[0]["company_name"].ToString();
					string StrNewRecModuleDesc = DS.Tables[1].Rows[0]["ModuleDesc"].ToString();
					string StrNewRecBAND = DS.Tables[1].Rows[0]["PositionTitle"].ToString();
					string StrNewRecPositionTitle = DS.Tables[1].Rows[0]["BAND"].ToString();

					if (DS.Tables[0].Rows[0]["RecruiterID"].ToString() != DS.Tables[1].Rows[0]["RecruiterID"].ToString())
					{
						StrCCmail = StrCCmail + ";" + DS.Tables[1].Rows[0]["Emp_EmailaddressRecruiter"].ToString();
					}
					if (DS.Tables[0].Rows[0]["Emp_Code"].ToString() != DS.Tables[1].Rows[0]["Emp_Code"].ToString())
					{
						if (StrFrommail.Trim() == DS.Tables[1].Rows[0]["Emp_EmailaddressInterviewSheduler"].ToString().Trim())
						{
							StrCCmail = StrCCmail.Trim();
						}
						else
						{
							StrCCmail = StrCCmail + ";";
						}
					}
					string RequiredByDate1 = "";
					RequiredByDate1 = GetRequiredByDate();
					spm.send_mailto_TransferredRequisitionNo(txtReqName.Text, hdnLoginEmpEmail.Value, StrTomail, StrCCmail, mailsubject, StrName, txtName.Text, txtReqNumber.Text, DDLsearchRequisitionnumber.SelectedItem.Text.Trim(), mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate1, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrNewRecDepartment_Name, StrNewReccompany_name, StrNewRecModuleDesc, StrNewRecBAND, StrNewRecPositionTitle);
					SqlParameter[] spars1 = new SqlParameter[5];
					DataSet DSResult = new DataSet();
					spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
					spars1[0].Value = "Rec_TransferCandidateNewOfferMail";
					spars1[1] = new SqlParameter("@CandidateID", SqlDbType.Int);
					spars1[1].Value = Convert.ToInt32(hdCandidate_ID.Value);
					spars1[2] = new SqlParameter("@NewRecruitment_ReqID", SqlDbType.Int);
					spars1[2].Value = Convert.ToInt32(DDLsearchRequisitionnumber.SelectedValue);
					spars1[3] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
					spars1[3].Value = Session["Empcode"].ToString();
					DSResult = spm.getDatasetList(spars1, "SP_Get_Rec_TransferCandidate_OtherRequisition");
					if (DSResult.Tables[0].Rows.Count > 0)
					{
						var RequisitionDate = DSResult.Tables[0].Rows[0]["RequisitionDate"].ToString();
						var RequiredByDatesd = DSResult.Tables[0].Rows[0]["RequiredByDate"].ToString();
						var Emp_Name = DSResult.Tables[0].Rows[0]["Emp_Name"].ToString();
						var NoOfPosition = DSResult.Tables[0].Rows[0]["NoOfPosition"].ToString();
						var Offer_App_IDsd = Convert.ToInt32(DSResult.Tables[0].Rows[0]["Offer_App_ID"].ToString());
						string StrCandNameandSkillSet = txtName.Text.Trim() + " - " + StrNewRecModuleDesc;
						strApproverlist = "";
						strApproverlist = GetReq_offer_Approve_RejectList(Offer_App_IDsd, Convert.ToInt32(DDLsearchRequisitionnumber.SelectedValue));
						strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Approval"]).Trim() + "?Offer_App_ID=" + Offer_App_IDsd + "&Rec_ID=" + DDLsearchRequisitionnumber.SelectedValue;
						spm.send_mailto_Req_Requisition_Offer_Approver(StrCandNameandSkillSet, hdnRecruiteName.Value, hdnNextofferApprovalName.Value, hdnLoginEmpEmail.Value, hdnNextofferApprovalEmail.Value, "Recruitment - Request for Offer Approval " + Convert.ToString(DDLsearchRequisitionnumber.SelectedItem.Text.Trim()), Emp_Name, Convert.ToString(DDLsearchRequisitionnumber.SelectedItem.Text.Trim()), RequisitionDate, RequiredByDatesd, StrNewReccompany_name, StrNewRecModuleDesc, StrNewRecBAND, StrNewRecDepartment_Name, txtName.Text.Trim(), StrNewRecPositionTitle, txtOfferAppcmt.Text.Trim(), hdnFinalizedDate.Value, hdnApproverid_LWPPLEmail.Value, strOfferApprovalURL, strApproverlist, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, OfferBand, txtExceptionamt.Text.Trim(), RecruitmentCharges, ProbableJoiningDate, txtpositionOffer.Text, txtOfferedLocation.Text.Trim());
					}
					Response.Redirect("~/procs/Req_Offer_Index.aspx?itype=Pending");
				}
			}
			else
			{
				lblmessage.Text = "Can not transfer Requisition.Already transfered same candidate";
			}
		}
	}
	private void OfferCreatelist()
	{
		int RecrutID = 0, Candidate_ID = 0;
		RecrutID = Convert.ToString(hdnReqID.Value).Trim() != "" ? Convert.ToInt32(hdnReqID.Value) : 0;
		Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
		SqlParameter[] spars = new SqlParameter[6];
		DataSet dsOffercreate = new DataSet();
		spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
		spars[0].Value = "OfferCandidateList";
		spars[1] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
		spars[1].Value = Candidate_ID;
		spars[2] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
		spars[2].Value = RecrutID;
		dsOffercreate = spm.getDatasetList(spars, "sp_Offer_Approval_Details");

		if (dsOffercreate.Tables[0].Rows.Count > 0)
		{
			//Span2.Visible = true;
			GRDOfferCreatelist.DataSource = dsOffercreate.Tables[0];
			GRDOfferCreatelist.DataBind();

		}
		OfferHistList(Convert.ToInt32(hdnOffer_App_ID.Value));
	}

	protected void lnnOfferedit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			int Offer_App_ID = Convert.ToInt32(GRDOfferCreatelist.DataKeys[row.RowIndex].Values[0]);
			OfferHistList(Offer_App_ID);
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	private void OfferHistList(int Offer_App_ID)
	{
		try
		{
			SqlParameter[] spars = new SqlParameter[4];
			DataSet dsOffercreate = new DataSet();
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = "OfferCreateEdit";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Offer_App_ID;
			dsOffercreate = spm.getDatasetList(spars, "sp_Offer_Approval_Details");
			if (dsOffercreate.Tables[0].Rows.Count > 0)
			{
				//OfferCreate.Visible = true;
				//mobile_btnSave.Style.Add("display", "none");
				txtofferDate.Text = dsOffercreate.Tables[0].Rows[0]["Offer_Date"].ToString();
				lstOfferBand.SelectedValue = dsOffercreate.Tables[0].Rows[0]["OfferBAND"].ToString();
				txtpositionOffer.Text = dsOffercreate.Tables[0].Rows[0]["PositionTitle"].ToString();
				txtOfferno1.Text =Convert.ToDecimal(dsOffercreate.Tables[0].Rows[0]["OldCTC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")); 
				txtOfferno2.Text = Convert.ToDecimal(dsOffercreate.Tables[0].Rows[0]["NewCTC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txtExceptionamt.Text = Convert.ToDecimal(dsOffercreate.Tables[0].Rows[0]["ExceptionAmount"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txtOfferAppcmt.Text = dsOffercreate.Tables[0].Rows[0]["Comment"].ToString();
				txtProbableJoiningDate.Text = dsOffercreate.Tables[0].Rows[0]["ProbableJoiningDate"].ToString();
				lstRecruitmentCharges.Text = dsOffercreate.Tables[0].Rows[0]["RecruitmentCharges"].ToString();
				txt_EmploymentType.Text = dsOffercreate.Tables[0].Rows[0]["Particulars"].ToString();
				if (dsOffercreate.Tables[0].Rows[0]["IsException"].ToString() == "No")
				{
					chk_exception.Checked = false;
				}
				else
				{
					chk_exception.Checked = true;
				}
				if (Convert.ToString(dsOffercreate.Tables[0].Rows[0]["Location_name"].ToString()) != "")
				{
					txtOfferedLocation.Text = dsOffercreate.Tables[0].Rows[0]["Location_name"].ToString();
				}
				else
				{
					txtOfferedLocation.Text = Convert.ToString(lstPositionLoca.SelectedItem.Text);
				}
				//lblOfferCreate.Text = "Offer Edit";
				//	txtOfferDate.Enabled = false;
				txtpositionOffer.Enabled = false;
				lstOfferBand.Enabled = false;
				txtpositionOffer.Enabled = false;
				txtOfferno1.Enabled = false;
				txtExceptionamt.Enabled = false;
				txtOfferno2.Enabled = false;
				txtOfferAppcmt.Enabled = false;
				txtProbableJoiningDate.Enabled = false;
				lstRecruitmentCharges.Enabled = false;
				txt_EmploymentType.Enabled = false;
				txtOfferedLocation.Enabled = false;
				//chk_exception.Enabled = false;
				OfferhistoryS.Visible = true;
				GRDOfferHistory.DataSource = null;
				GRDOfferHistory.DataBind();
				if (dsOffercreate.Tables[1].Rows.Count > 0)
				{
					GRDOfferHistory.DataSource = dsOffercreate.Tables[1];
					GRDOfferHistory.DataBind();
				}
				GRDGenerate_Offer.DataSource = null;
				GRDGenerate_Offer.DataBind();
				if (dsOffercreate.Tables[2].Rows.Count > 0)
				{
					//txtOffer_Can_Name.Text = txtName.Text;
					if (!string.IsNullOrEmpty(dsOffercreate.Tables[2].Rows[0]["PLP_VARIABLE_PAY"].ToString()))
					{
						GRDGenerate_Offer.Columns[0].Visible = true;
						GRDGenerate_Offer.Columns[1].Visible = true;
					}
					else
					{
						GRDGenerate_Offer.Columns[0].Visible = false;
						GRDGenerate_Offer.Columns[1].Visible = false;
					}
					GRDGenerate_Offer.DataSource = dsOffercreate.Tables[2];
					GRDGenerate_Offer.DataBind();
				}
			}
		}
		catch (Exception)
		{

			throw;
		}
	}

	#region IR Sheet Export Excel 25-02-22
	protected void lnkIRsheetExport_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdCandidate_ID.Value = Convert.ToString(GVCandidateRoundHistory.DataKeys[row.RowIndex].Values[0]).Trim();
		getIrSheetExport();

	}
	private void getIrSheetExport()
	{
		try
		{
			DataTable dtMerged = new DataTable();
			dtIrSheetReport = new DataSet();
			dtIrSheetReport = spm.Get_Rec_Recruit_IrSheetDetails("GetIrSheetSummary", Convert.ToInt32(hdnReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
			if (dtIrSheetReport.Tables[0].Rows.Count > 0)
			{
				dtMerged = new DataTable();
				DTInterviews = new DataTable();
				DTMaintable = new DataTable();
				DataTable newIRSummary = new DataTable();
				if (dtIrSheetReport.Tables[3].Rows.Count > 0)
				{
					DTInterviews = GenerateColumn(dtIrSheetReport.Tables[3]);
				}
				if (dtIrSheetReport.Tables[1].Rows.Count > 0)
				{
					DTMaintable = getData(dtIrSheetReport.Tables[0], dtIrSheetReport.Tables[1]);
				}
				if (dtIrSheetReport.Tables[3].Rows.Count > 0)
				{
					dtMerged = MergeTables(DTMaintable, DTInterviews);
				}
				if (dtIrSheetReport.Tables[2].Rows.Count > 0)
				{
					newIRSummary = new DataTable();
					newIRSummary.Columns.Add("Interviewer level");
					newIRSummary.Columns.Add("Overall Rating");
					newIRSummary.Columns.Add("Selection Recommendation");
					newIRSummary.Columns.Add("Notes if any");
					newIRSummary.Columns.Add("Interviewer Remarks");
					newIRSummary.Columns.Add("Name of the Interviewer");
					foreach (DataRow item in dtIrSheetReport.Tables[2].Rows)
					{
						DataRow _dr = newIRSummary.NewRow();
						_dr["Interviewer level"] = item["InterviewRound"].ToString();
						_dr["Overall Rating"] = item["RatingName"].ToString();
						_dr["Selection Recommendation"] = item["Selection_Recommendation"].ToString();
						_dr["Notes if any"] = item["Notes"].ToString();
						_dr["Interviewer Remarks"] = item["InterviewrRemarks"].ToString();
						_dr["Name of the Interviewer"] = item["Emp_Name"].ToString();
						newIRSummary.Rows.Add(_dr);
					}
				}
				string RoundName = "";
				int B = 0, C = 0;
				if (dtIrSheetReport.Tables[3].Rows.Count > 0)

				{
					var newTable = new DataTable();
					DataTable dt = new DataTable();
					dtMerged.Columns.Remove("Main_Type_ID");
					dtMerged.Columns.Remove("SubType_Rating");
					dtMerged.Columns.Remove("SubType_ID");
					dtMerged.Columns.Remove("Ishedeing");

					//delete rows blank & null column
					dt = dtMerged.Rows
					.Cast<DataRow>()
					.Where(row => !row.ItemArray.All(f => f is DBNull ||
									 string.IsNullOrEmpty(f as string ?? f.ToString())))
					.CopyToDataTable();

					newTable.Columns.Add("Competency");
					foreach (DataColumn col in DTInterviews.Columns)
					{
						if (C == 2)
						{
							B++;
							C = 0;
						}
						RoundName = NewDT.Rows[B]["InterviewRound"].ToString();
						var NewHeaderName = Regex.Replace(col.ColumnName, @"[\d-]", string.Empty);
						newTable.Columns.Add(NewHeaderName + " - " + RoundName, typeof(string));
						C++;
					}
					var aCode = 65;
					var excelName = "IR_Sheet_" + txtName.Text;
					using (XLWorkbook wb = new XLWorkbook())
					{

						var ws = wb.Worksheets.Add("IR-Sheet Details");

						ws.Cell(1, 1).Value = "Candidate's Name";
						ws.Cell(1, 1).Style.Font.Bold = true;
						ws.Cell(2, 1).Value = "Requisition Number";
						ws.Cell(2, 1).Style.Font.Bold = true;
						ws.Cell(3, 1).Value = "Position Title";
						ws.Cell(3, 1).Style.Font.Bold = true;
						ws.Cell(1, 2).Value = txtName.Text;
						ws.Cell(2, 2).Value = txtReqNumber.Text;
						ws.Cell(3, 2).Value = lstPositionName.SelectedItem.Text;

						ws.Cell(1, 3).Value = "Position Interviewed for";
						ws.Cell(1, 3).Style.Font.Bold = true;
						ws.Cell(2, 3).Value = "Total Experience (In Year)";
						ws.Cell(2, 3).Style.Font.Bold = true;
						ws.Cell(3, 3).Value = "Relevant Experience (In Year)";
						ws.Cell(3, 3).Style.Font.Bold = true;

						ws.Cell(1, 4).Value = lstSkillset.SelectedItem.Text;
						ws.Cell(2, 4).Value = TxtTotalExperienceYrs.Text;
						ws.Cell(3, 4).Value = TxtRelevantExpYrs.Text;


						//var wsReportNameHeaderRange = ws.Range(string.Format("A{0}:{1}{0}", 3, Char.ConvertFromUtf32(aCode + 4)));
						//wsReportNameHeaderRange.Style.Font.Bold = true;
						//wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

						int rowIndex = 5; int i = 1;
						int columnIndex = 0;
						foreach (DataColumn column in newTable.Columns)
						{

							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.Gainsboro;
							ws.Column(i).Width = 25;
							ws.Column(1).Width = 45;
							columnIndex++; i++;
						}

						rowIndex++;
						foreach (DataRow row in dt.Rows)
						{
							int valueCount = 0;
							foreach (object rowValue in row.ItemArray)
							{
								ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
								ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
								ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Alignment.WrapText = true;
								valueCount++;
							}

							rowIndex++;
						}
						rowIndex = rowIndex + 3;
						columnIndex = 0;
						int j = 1;
						foreach (DataColumn column in newIRSummary.Columns)
						{
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Value = column.ColumnName;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Font.Bold = true;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
							ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + columnIndex), rowIndex)).Style.Fill.BackgroundColor = XLColor.Gainsboro;
							ws.Column(j).Width = 25;
							ws.Column(1).Width = 45;
							columnIndex++; j++;
						}
						rowIndex++;
						foreach (DataRow row in newIRSummary.Rows)
						{
							int valueCount = 0;
							foreach (object rowValue in row.ItemArray)
							{
								ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Value = rowValue;
								ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
								ws.Cell(string.Format("{0}{1}", Char.ConvertFromUtf32(aCode + valueCount), rowIndex)).Style.Alignment.WrapText = true;
								valueCount++;
							}

							rowIndex++;
						}
						Response.Clear();
						Response.Buffer = true;
						Response.Charset = "";
						Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
						Response.AddHeader("content-disposition", "attachment;filename=" + excelName + ".xlsx");
						using (MemoryStream MyMemoryStream = new MemoryStream())
						{
							wb.SaveAs(MyMemoryStream);
							MyMemoryStream.WriteTo(Response.OutputStream);
							Response.Flush();
							Response.End();
						}
					}
				}
			}
		}
		catch (Exception)
		{

			throw;
		}
	}
	#endregion

	#region MyRegion 06-10-22
	protected void lnkGenerateFiles_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			int TempLatterID = Convert.ToInt32(GRDGenerate_Offer.DataKeys[row.RowIndex].Values[0]);
			int Offer_App_ID = Convert.ToInt32(GRDGenerate_Offer.DataKeys[row.RowIndex].Values[1]);
			SqlParameter[] spars = new SqlParameter[4];
			DataSet dsOffer = new DataSet();
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Get_Offer_Apporval_Candidate";
			spars[1] = new SqlParameter("@StructureID", SqlDbType.Int);
			spars[1].Value = TempLatterID;
			spars[2] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[2].Value = Offer_App_ID;
			dsOffer = spm.getDatasetList(spars, "SP_Rec_Generate_Offer_Candidate");
			//if (HdnFinalStatus.Value == "FinalApproval")
			{
				Offer_Show.Visible = true;
				Offer_Show1.Visible = true;
			}
			if (dsOffer.Tables[0].Rows.Count > 0)
			{
				int t1 = 12, t2 = 4;
				this.ModalPopupExtenderGenerateOffer.Show();
				txt_Offer_band.Text = lstOfferBand.SelectedItem.Text;
				txt_Offer_Designation.Text = txtpositionOffer.Text;
				txtOffer_Can_Name.Text = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();
				SPOffer_CandidateName.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();

				txt_Offer_Location.Text = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				SPOffer_Can_Address.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				SP_Offer_Address2.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				SP_candidate_Name1.InnerHtml = SPOffer_CandidateName.InnerHtml;
				SP_Candidate_Name4.InnerHtml = SPOffer_CandidateName.InnerHtml;
				SPOffer_latterNo.InnerHtml = dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString();
				SP_OfferNo2.InnerHtml = dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString();
				SP_Greetings.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();
				SP_Candidate_Name2.InnerHtml = SPOffer_CandidateName.InnerHtml;
				txt_Offer_Basic.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["BASIC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Offer_HRA.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["HRA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				//txt_Offer_Location .Text= dsOffer.Tables[0].Rows[0]["Particulars"].ToString();
				//txt_Offer_Basic.Text = dsOffer.Tables[0].Rows[0]["BASIC"].ToString();
				//txt_Offer_HRA.Text = dsOffer.Tables[0].Rows[0]["HRA"].ToString();
				SP_ApprovalDate.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
				SP_ApprovalDate1.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
				SP_ApprovalDate3.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();

				SP_Candidate_Accpted_Date.InnerHtml = dsOffer.Tables[0].Rows[0]["AcceptedDate"].ToString();
				SP_Candidate_Accpted_Date2.InnerHtml = dsOffer.Tables[0].Rows[0]["AcceptedDate"].ToString();
				SP_Candidate_Accpted_Date3.InnerHtml = dsOffer.Tables[0].Rows[0]["AcceptedDate"].ToString();
				if (dsOffer.Tables[0].Rows[0]["AcceptedDate"].ToString() != "")
				{
					SP_Candidate_Name5.InnerHtml = SPOffer_CandidateName.InnerHtml;
				}

				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SPECIAL_ALLOWANCE"].ToString()))
				{
					txt_Off_Special_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SPECIAL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TSpecial.Visible = true;
					adhoc.Visible = false;
				}
				else
				{
					TSpecial.Visible = false;
					adhoc.Visible = true;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CONVEYANCE"].ToString()))
				{
					txt_Offer_Conveyance.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CONVEYANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TConveyance.Visible = true;
				}
				else
				{
					TConveyance.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ADHOC_ALLOWANCE"].ToString()))
				{
					txt_Offer_ADHOC_Allowance.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ADHOC_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TADHOC.Visible = true;
				}
				else
				{
					TADHOC.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SKILL_ALLOWANCE"].ToString()))
				{
					txt_offer_Skill_Allowance.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TSkill.Visible = true;
				}
				else
				{
					TSkill.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SUPERAN_ALLOWANCE"].ToString()))
				{
					txt_Offer_Superannucation.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SUPERAN_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TSuperannucation.Visible = true;
				}
				else
				{
					TSuperannucation.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()))
				{
					txt_Offer_Certificate_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TCertificate.Visible = true;
				}
				else
				{
					TCertificate.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()))
				{
					txt_Offer_Multi_Skill_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr1.Visible = true;
				}
				else
				{
					Tr1.Visible = false;
					t1 = --t1;
				}

				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()))
				{
					txt_Offer_Additional_Skill.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr2.Visible = true;
				}
				else
				{
					Tr2.Visible = false;
					t1 = --t1;
				}

				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CAR_ALLOWANCE"].ToString()))
				{
					txtCar_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CAR_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr3.Visible = true;
				}
				else
				{
					Tr3.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["FOOD_ALLOWANCE"].ToString()))
				{
					txt__Offer_Food_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["FOOD_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr4.Visible = true;
				}
				else
				{
					Tr4.Visible = false;
					t1 = --t1;
				}


				txt_Offer_Total1.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL1"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				tb1.RowSpan = t1;
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["LTA"].ToString()))
				{
					txt_Offer_LTA.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["LTA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MEDICAL"].ToString()))
				{
					txt_offer_Medical.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MEDICAL"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TMedical.Visible = true;
				}
				else
				{
					TMedical.Visible = false;
					t2 = --t2;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["DRIVER_SALARY"].ToString()))
				{
					txt_Offer_Driver_Salary.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["DRIVER_SALARY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TDriver.Visible = true;
				}
				else
				{
					TDriver.Visible = false;
					t2 = --t2;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CAR_LEASE"].ToString()))
				{
					txt_Offer_Car_lease.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CAR_LEASE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TCar.Visible = true;
				}
				else
				{
					TCar.Visible = false;
					t2 = --t2;
				}
				txt_Offer_Total2.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL2"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				tb2.RowSpan = t2;
				txt_Offer_PF.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["PF"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["GRATUITY_B"].ToString()))
				{
					txt_Offer_Gratuity.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["GRATUITY_B"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				txt_Offer_Total3.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL3"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Offer_Mediclaim.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MEDICLAIM"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Offer_Group_Policy.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["GROUP_ACC_POLICY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));

				txtTotal4.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL4"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Offer_CTC_Month.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_MONTH"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Offer_CTC_Annum.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()))
				{
					txt_Offer_PLP_variable.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					txt_Offer_PLP_Per.Text = dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_Percentage"].ToString();
					TPLP.Visible = true;
					Tr9.Visible = true;
				}
				else
				{
					TPLP.Visible = false;
					Tr9.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()))
				{
					txt_Offer_CTC_Annum_Incl_PLP.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr5.Visible = true;
				}
				else
				{
					Tr5.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ANNUAL_BONUS"].ToString()))
				{
					txt_Annual_Bonus.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ANNUAL_BONUS"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr7.Visible = true;
				}
				else
				{
					Tr7.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["OTHER"].ToString()))
				{
					txt_Offer_Other.Text = dsOffer.Tables[0].Rows[0]["OTHER"].ToString();
					Tr8.Visible = true;
				}
				else
				{
					Tr8.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["JOINING_BONUS"].ToString()))
				{
					txt_Retention_Bonus.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["JOINING_BONUS"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					//txt_Retention_Remark.Text = dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString();
					TDJoiningRemark.InnerHtml ="d - "+ dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString();
					TRJoining.Visible = true;
					Tr6.Visible = true;
				}
				else
				{
					Tr6.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ALP_ALLOWANCE"].ToString()))
				{
					txt_ALP_Amount.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ALP_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					//txt_ALP_Remark.Text = dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString();
					TDALPRemark.InnerHtml = "e - " + dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString();
					TRALP.Visible = true;
					Tr10.Visible = true;
				}
				else
				{
					Tr10.Visible = false;
				}


                if (dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() =="" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "0.00")
                {
                    TD_HealthCheckup1.Visible = false;
                    TD_HealthCheckup2.Visible = false;
                    TD_HealthCheckup3.Visible = false;
                }
                else
                {
                    txt_Offer_Health_Checkup.Text = dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString();
                    TD_HealthCheckup1.Visible = true;
                    TD_HealthCheckup2.Visible = true;
                    TD_HealthCheckup3.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() =="" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0.00")
                {
                    TR_CarHireCost.Visible = false;
                }
                else
                {
                    txt_offer_Car_Hire_Cost.Text = dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString();
                    TR_CarHireCost.Visible = true;
                    
                }

                if (dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "0.00")
                {
                    TR_CarExpensesReimbursment.Visible = false;
                }
                else
                {
                    txt_Offer_Car_Expenses_Reimbursment.Text = dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString();
                    TR_CarExpensesReimbursment.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "0.00")
                {
                    
                    TR_CarFuelExpensesReimbursment.Visible = false;
                }
                else
                {
                    txt_Car_Fuel_Expenses_Reimbursment.Text = dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString();
                    TR_CarFuelExpensesReimbursment.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "0.00")
                {
                    Tr_Total5.Visible = false;
                }
                else
                {
                    txtTotal5.Text = dsOffer.Tables[0].Rows[0]["Total5"].ToString();
                    Tr_Total5.Visible = true;
                }



                SP_HRA.InnerText = dsOffer.Tables[0].Rows[0]["RHR"].ToString();
				SP_Superannucation_All.InnerText = dsOffer.Tables[0].Rows[0]["RSUPERAN"].ToString();
				SP_LTA.InnerText = dsOffer.Tables[0].Rows[0]["RLTA"].ToString();
				SP_PF.InnerText = dsOffer.Tables[0].Rows[0]["RPF"].ToString();
				SP_Gratuity.InnerText = dsOffer.Tables[0].Rows[0]["RGRA"].ToString();
			}
			if (dsOffer.Tables[1].Rows.Count > 0)
			{

				SP_Recruitment_Head.InnerHtml = dsOffer.Tables[1].Rows[0]["RecruitmentHead"].ToString();
				SP_Recruitment2.InnerHtml = SP_Recruitment_Head.InnerHtml;
				SP_Recruitment3.InnerHtml = SP_Recruitment_Head.InnerHtml;
				SP_Recruiter_Name.InnerHtml = dsOffer.Tables[1].Rows[0]["RecruiterName"].ToString();
				SP_Design.InnerHtml = dsOffer.Tables[1].Rows[0]["PositionTitle"].ToString();
				SP_Band.InnerHtml = dsOffer.Tables[1].Rows[0]["OfferBAND"].ToString();
				txtOffer_Position_Location.Text = dsOffer.Tables[1].Rows[0]["Location_name"].ToString();
				SP_Location.InnerHtml = dsOffer.Tables[1].Rows[0]["locOffice_name"].ToString();

				SPOffer_Date.InnerHtml = dsOffer.Tables[1].Rows[0]["Offer_Date"].ToString();
				SP_Offer_Date2.InnerHtml = dsOffer.Tables[1].Rows[0]["Offer_Date"].ToString();
				SP_Offer_Date3.InnerHtml = SPOffer_Date.InnerHtml;
				SP_JoiningDate.InnerHtml = dsOffer.Tables[1].Rows[0]["ProbableJoiningDate"].ToString();

                if (dsOffer.Tables[1].Rows[0]["OfferAcceptanceByDate"].ToString() == "")
                {
                    SP_SP_JoiningDate1.InnerHtml = dsOffer.Tables[1].Rows[0]["ProbableJoiningDate"].ToString();
                }
                else
                {
                    SP_SP_JoiningDate1.InnerHtml = dsOffer.Tables[1].Rows[0]["OfferAcceptanceByDateformat"].ToString();
                }


                SP_JoiningDate2.InnerHtml = SP_JoiningDate.InnerHtml;

				SP_Candidate_Jo.InnerHtml = SPOffer_Date.InnerHtml + " " + SPOffer_CandidateName.InnerHtml;
				SP_Greetings2.InnerHtml = SPOffer_CandidateName.InnerHtml;

				//txt_Offer_HRA.Text = dsOffer.Tables[1].Rows[0]["JoiningDate"].ToString();


			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Get_HR_Approver()
	{
		try
		{
			DataTable dsNextAppDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "get_HR_Offer_Approver";
			spars[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			spars[2] = new SqlParameter("@REQID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(hdnReqID.Value);
			spars[3] = new SqlParameter("@OfferCondition", SqlDbType.VarChar);
			spars[3].Value = hdnOfferConditionid.Value;
			spars[4] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[4].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dsNextAppDetails = spm.getMobileRemDataList(spars, "GETREQUISITION_OFFER_APPROVER_STATUS_COMP");
			if (dsNextAppDetails.Rows.Count > 0)
			{
				//HdnFinalStatus.Value = "";
				hdnNextofferApprovalName.Value = Convert.ToString(dsNextAppDetails.Rows[0]["Emp_Name"]).Trim();
				hdnNextofferApprovalEmail.Value = Convert.ToString(dsNextAppDetails.Rows[0]["Emp_Emailaddress"]).Trim();
				hdnNextofferApprovalCode.Value = Convert.ToString(dsNextAppDetails.Rows[0]["A_EMP_CODE"]).Trim();
				hdnNextofferApprovalID.Value = Convert.ToString(dsNextAppDetails.Rows[0]["APPR_ID"]).Trim();

				if (Convert.ToString(hdnApproverid_LWPPLEmail.Value) == Convert.ToString(hdnNextofferApprovalEmail.Value))
				{
					hdnApproverid_LWPPLEmail.Value = "";
				}
			}
		}

		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void Get_HR_Employee_Details()
	{
		try
		{
			DataTable dsNextAppDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@Stype", SqlDbType.VarChar);
			spars[0].Value = "get_HR_Employee_Details";
			spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			spars[2] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(0);
			spars[3] = new SqlParameter("@REQID", SqlDbType.Int);
			//spars[3].Value = Convert.ToInt32(0);
			spars[3].Value = Convert.ToInt32(hdnCurrentID.Value);//Recruitment head check same dept
			dsNextAppDetails = spm.getMobileRemDataList(spars, "GETREQUISITION_OFFER_APPROVER_STATUS_COMP");
			if (dsNextAppDetails.Rows.Count > 0)
			{
				HdnFinalStatus.Value = "FinalApproval";
			}
		}

		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void InsertCandidateLoginDetail_ELC()
	{
		try
		{
			DataSet DSCandidateLoginDetail = new DataSet();
			string FileName = "";
			string StrCanName = txtName.Text.Trim();
			string[] SplitCanName = StrCanName.Split(' ');
			string Candidatemailpwd = RandomString(8);
			string hashedPassword = HashSHA1(Candidatemailpwd + txtEmail.Text.Trim());
			if (SplitCanName.Length == 3)
			{
				StrCanName = SplitCanName[0].Trim() + " " + SplitCanName[2].Trim();
			}
			SqlParameter[] spars = new SqlParameter[6];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "INSERT_OFFER";
			spars[1] = new SqlParameter("@CandidateId", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdCandidate_ID.Value);
			spars[2] = new SqlParameter("@UserName", SqlDbType.VarChar);
			spars[2].Value = txtEmail.Text.Trim();
			spars[3] = new SqlParameter("@Name", SqlDbType.VarChar);
			spars[3].Value = StrCanName.Trim();
			spars[4] = new SqlParameter("@Password", SqlDbType.VarChar);
			spars[4].Value = hashedPassword.ToString();
			spars[5] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[5].Value = Session["Empcode"].ToString();
			DSCandidateLoginDetail = spm.getDatasetList(spars, "SP_ELC_CandidateLoginDetails");

			if (DSCandidateLoginDetail.Tables[0].Rows[0]["Records"].ToString() == "Insert")
			{
				var emailCC = "";
				if (DSCandidateLoginDetail.Tables[1].Rows.Count > 0)
				{
					var RecruiterEmails = DSCandidateLoginDetail.Tables[1].Rows[0]["RecruiterEmail"].ToString();
					var RecruitmentHeadEmail = DSCandidateLoginDetail.Tables[1].Rows[0]["RecruitmentHeadEmail"].ToString();
					{
						emailCC = RecruiterEmails + ";" + RecruitmentHeadEmail;
					}

				}
				DateTime dsysdate = DateTime.Now.Date;
				var year = dsysdate.Year;
				var month = dsysdate.Month;
				var day = dsysdate.Day;
				string RecruiterName = "", RecruiterEmail = "", Recruitermobile = "", Designation = "", ReleaseDate = "";
				FileName = Convert.ToString(hdCandidate_ID.Value).Trim() + "_" + day + "." + month + "." + year + ".pdf"; //"testing.pdf";
				spm.Set_Generate_Offer_FileName("Candidate_OfferFile", Convert.ToInt32(hdnOffer_App_ID.Value), 0, 0, Session["Empcode"].ToString(), FileName,"");
				DataSet Offerlatter = new DataSet();
				SqlParameter[] spars1 = new SqlParameter[5];
				spars1[0] = new SqlParameter("@qtype", SqlDbType.NVarChar);
				spars1[0].Value = "Get_Offer_Candidate_Details";
				spars1[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
				spars1[1].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
				spars1[2] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
				spars1[2].Value = Session["Empcode"].ToString();
				Offerlatter = spm.getDatasetList(spars1, "SP_Rec_Generate_Offer_Candidate");
				if (Offerlatter.Tables[0].Rows.Count > 0)
				{

					RecruiterName = Offerlatter.Tables[0].Rows[0]["RecruiterName"].ToString();
					RecruiterEmail = Offerlatter.Tables[0].Rows[0]["RecruiterEmail"].ToString();
					Recruitermobile = Offerlatter.Tables[0].Rows[0]["Recruitermobile"].ToString();
					Designation = Offerlatter.Tables[0].Rows[0]["PositionTitle"].ToString();
					ReleaseDate = Offerlatter.Tables[0].Rows[0]["ReleaseDate"].ToString();
					LocalReport ReportViewer1 = new LocalReport();
					ReportViewer1.ReportPath = Server.MapPath("~/procs/Candidate_Offer_Latter.rdlc");
					ReportDataSource rds = new ReportDataSource("EmployeeD", Offerlatter.Tables[0]);
					ReportDataSource rd1 = new ReportDataSource("Compensation", Offerlatter.Tables[1]);
					ReportViewer1.DataSources.Clear();
					ReportViewer1.DataSources.Add(rds);
					ReportViewer1.DataSources.Add(rd1);
					byte[] Bytes = ReportViewer1.Render(format: "PDF", deviceInfo: "");
					using (FileStream stream = new FileStream(Server.MapPath(ConfigurationManager.AppSettings["CandidateOfferLatter"]) + FileName, FileMode.Create))
					{
						stream.Write(Bytes, 0, Bytes.Length);
					}
				}
				string mailsubject = "";
				string mailcontain = "";
				string strCandidateLoginURL = "";
				mailsubject = "Offer Letter - Highbar Technocrat Limited.";
				mailcontain = "Please follow these instructions for logging in to the system";
				string sattchedfileName = Server.MapPath(ConfigurationManager.AppSettings["CandidateOfferLatter"]) + FileName;
				//string strCandidateLoginURL = Convert.ToString(ConfigurationManager.AppSettings["Link_CandidateLogin"]).Trim();
				strCandidateLoginURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Accept"]).Trim() + "?Offer_App_ID=" + hdnOffer_App_ID.Value;
				spm.send_mailto_CandidateLoginDetail_ELC_Offer(StrCanName.Trim(), txtEmail.Text.Trim(), txtEmail.Text.Trim(), mailsubject, Candidatemailpwd.ToString(), mailcontain, strCandidateLoginURL, emailCC, sattchedfileName, Designation, ReleaseDate, RecruiterName, RecruiterEmail, Recruitermobile);

			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}
	}
	public static string RandomString(int length)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789@";
		return new string(Enumerable.Repeat(chars, length)
		  .Select(s => s[random.Next(s.Length)]).ToArray());
	}
	public static string HashSHA1(string value)
	{
		var sha1 = System.Security.Cryptography.SHA1.Create();
		var inputBytes = Encoding.ASCII.GetBytes(value);
		var hash = sha1.ComputeHash(inputBytes);

		var sb = new StringBuilder();
		for (var i = 0; i < hash.Length; i++)
		{
			sb.Append(hash[i].ToString("X2"));
		}
		return sb.ToString();
	}
	public void Get_Pre_Employee_EmailID()
	{
		try
		{
			DataSet dsResult = new DataSet();
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@mode", SqlDbType.NVarChar);
			spars[0].Value = "Approval_Emailaddress";
			spars[1] = new SqlParameter("@empCode", SqlDbType.NVarChar);
			spars[1].Value = Session["Empcode"].ToString();
			spars[2] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(hdnOffer_App_ID.Value);
			dsResult = spm.getDatasetList(spars, "sp_Offer_Approval_Details");
			if (dsResult.Tables[0].Rows.Count > 0)
			{
				string emailCC = "";
				foreach (DataRow item in dsResult.Tables[0].Rows)
				{
					if (emailCC == "")
					{
						emailCC = item["Emp_Emailaddress"].ToString();
					}
					else
					{
						emailCC = emailCC + ";" + item["Emp_Emailaddress"].ToString();
					}
				}
				if (Convert.ToString(hdnApproverid_LWPPLEmail.Value) != Convert.ToString(hdnLoginEmpEmail.Value))
				{
					if (emailCC == "")
					{
						emailCC = hdnApproverid_LWPPLEmail.Value;
					}
					else
					{
						emailCC = emailCC + ";" + hdnApproverid_LWPPLEmail.Value;
					}
				}
				hdnApproverid_LWPPLEmail.Value = emailCC;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.ToString());
		}
	}
	#endregion

}