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

public partial class procs_Req_Offer_View : System.Web.UI.Page
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
						//checkOffer_ApprovalStatus_Submit();
						//GetOffer_CuurentApprID();
						GetRecruitmentDetail();
						GetOffferAPPDetail();
						OfferCreatelist();
						GetOffer_Approverlist();
						Get_HR_Employee_Details();// HR Approval 

					}
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

				lstPositionLoca.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
				txtOtherDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["OtherDepartment"]).Trim();
				txtPositionDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionDesignationOther"]).Trim();
				hdncomp_code.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
				hdndept_Id.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
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
				txtProbableJoiningDate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ProbableJoiningDate"]).Trim();
				txtpositionOffer.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle"].ToString());
				txt_EmploymentType.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Particulars"]).Trim();
				if (Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Location_name"].ToString()) != "")
				{
					txtOfferedLocation.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Location_name"].ToString());
				}
				else
				{
					txtOfferedLocation.Text = Convert.ToString(lstPositionLoca.SelectedItem.Text);
				}

				txtOfferAppcmt.Enabled = false;

				if (IsException == 1)
				{
					chk_exception.Checked = true;

				}
				else
				{
					chk_exception.Checked = false;
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
				//GRDOfferHistory.DataSource = dsRecruitmentDetails.Tables[2];
				//GRDOfferHistory.DataBind();
				//OfferhistoryS.Visible = true;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	private void GetOffer_Approverlist()
	{
		int RecrutID = 0, Offer_App_ID = 0;
		DataTable dtapprover = new DataTable();
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
		var Dept_id = 0;
		var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
		if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
		{
			Dept_id = Convert.ToInt32(hdndept_Id.Value);
			qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
		}
		RecrutID = Convert.ToString(hdnReqID.Value).Trim() != "" ? Convert.ToInt32(hdnReqID.Value) : 0;
		Offer_App_ID = Convert.ToString(hdnOffer_App_ID.Value).Trim() != "" ? Convert.ToInt32(hdnOffer_App_ID.Value) : 0;
		dtapprover = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID, qtype, getcompSelectedval, Dept_id);
		DgvOfferApprover.DataSource = null;
		DgvOfferApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvOfferApprover.DataSource = dtapprover;
			DgvOfferApprover.DataBind();
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
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
		var Dept_id = 0;
		var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
		if (getcompSelectedText.Contains("Head Office") || getcompSelectedText.Contains("PROSP_"))
		{
			Dept_id = Convert.ToInt32(hdndept_Id.Value);
			qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
		}
		dtAppRej = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID, qtype, getcompSelectedval, Dept_id);
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

	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		
		

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
					SpanEducationDetails1.Visible = true;
					SpanEducationDetails2.Visible = true;
					GVEducationDetails.DataSource = dsCandidateData.Tables[3];
					GVEducationDetails.DataBind();
				}
				else
				{
					SpanEducationDetails.Visible = false;
					SpanEducationDetails1.Visible = false;
					SpanEducationDetails2.Visible = false;
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
		dtapprover = spm.CTC_Exception_Approval("GET_CTC_Exception_APP", Recruitment_ReqID, Candidate_ID, "", 0, CTCID,0);
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
		GrdIRIntSummary.DataSource = null;
		GrdIRIntSummary.DataBind();

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
			OfferHistList( Offer_App_ID);
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

				txt_EmploymentType.Text = Convert.ToString(dsOffercreate.Tables[0].Rows[0]["Particulars"]).Trim();
				if (Convert.ToString(dsOffercreate.Tables[0].Rows[0]["Location_name"].ToString()) != "")
				{
					txtOfferedLocation.Text = Convert.ToString(dsOffercreate.Tables[0].Rows[0]["Location_name"].ToString());
				}
				else
				{
					txtOfferedLocation.Text = Convert.ToString(lstPositionLoca.SelectedItem.Text);
				}
				if (dsOffercreate.Tables[0].Rows[0]["IsException"].ToString() == "No")
				{
					chk_exception.Checked = false;
				}
				else
				{
					chk_exception.Checked = true;
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
				chk_exception.Enabled = false;
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
					txtOffer_Can_Name.Text = txtName.Text;
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
				txt_Offer_Location.Text = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				SPOffer_Can_Address.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				SP_Offer_Address2.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				SPOffer_CandidateName.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();
				SP_candidate_Name1.InnerHtml = SPOffer_CandidateName.InnerHtml;
				SP_Candidate_Name4.InnerHtml = SPOffer_CandidateName.InnerHtml;
				SPOffer_latterNo.InnerHtml = dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString();
				SP_OfferNo2.InnerHtml = dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString();
				SP_Greetings.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();
				SP_ApprovalDate.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
				SP_ApprovalDate1.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
				SP_ApprovalDate3.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
				SP_Candidate_Name2.InnerHtml = SPOffer_CandidateName.InnerHtml;
				txt_Offer_Basic.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["BASIC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Offer_HRA.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["HRA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));

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
					//txt_Retention_Remark.Text = Convert.ToString(dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString());
					TDJoiningRemark.InnerHtml = "d - " + dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString();
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
					//txt_ALP_Remark.Text = Convert.ToString(dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString());
					TDALPRemark.InnerHtml = "e - " + dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString();
					TRALP.Visible = true;
					Tr10.Visible = true;
				}
				else
				{
					Tr10.Visible = false;
				}

				SP_HRA.InnerText = dsOffer.Tables[0].Rows[0]["RHR"].ToString();
				SP_Superannucation_All.InnerText = dsOffer.Tables[0].Rows[0]["RSUPERAN"].ToString();
				SP_LTA.InnerText = dsOffer.Tables[0].Rows[0]["RLTA"].ToString();
				SP_PF.InnerText = dsOffer.Tables[0].Rows[0]["RPF"].ToString();
				SP_Gratuity.InnerText = dsOffer.Tables[0].Rows[0]["RGRA"].ToString();

                if (dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "0.00")
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

                if (dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0.00")
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

				SP_Candidate_Jo.InnerHtml = SP_JoiningDate.InnerHtml + " " + SPOffer_CandidateName.InnerHtml;
				SP_Greetings2.InnerHtml = SPOffer_CandidateName.InnerHtml;

				//txt_Offer_HRA.Text = dsOffer.Tables[1].Rows[0]["JoiningDate"].ToString();


			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
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
			spars[3].Value = Convert.ToInt32(0);
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
}