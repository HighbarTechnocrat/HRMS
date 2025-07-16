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
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

public partial class procs_RecruiterCTCView : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;

	SP_Methods spm = new SP_Methods();
	DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource;
	public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet;
	public string filename = "";
	string strFilter = "", IsApprover = "", nxtapprname = "", nxtapprcode = "", approveremailaddress = "";
	int apprid, statusid;

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
					hdnEmpCode.Value = Convert.ToString(Session["Empcode"]).Trim();
					hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
					hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();
					hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());
					HFQuestionnaire.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
					getMainSkillsetView();
					getCVSource();
					#region  RecrutmentDetailsInfoViewLoading
					GetSkillsetName();
					GetPositionName();
					GetPositionCriticality();
					GetDepartmentMaster();
					GetCompany_Location();
					GetReasonRequisition();
					GetPositionDesign();
					GetPreferredEmpType();
					GetlstPositionBand();
					GetCandidateInfoRecruitmentwisedataBind();
					GetInterviewer();
					if (Request.QueryString.Count > 0)
					{
						hdRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
						hdnCTCID.Value = Convert.ToString(Request.QueryString[1]).Trim();
						GetecruitmentDetail();

					}
					#endregion
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	private void checkApprovalStatus_Submit()
	{
		try
		{
			DataTable dtTrDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[3];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "Check_CTCAppr_Status";
			spars[1] = new SqlParameter("@CTCID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnCTCID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(hdnEmpCode.Value).Trim();
			dtTrDetails = spm.getMobileRemDataList(spars, "SP_Shortlisting_CTC_Exception");
			if (dtTrDetails.Rows.Count == 0)
			{
				Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
			}
			if (dtTrDetails.Rows.Count > 0)
			{
				if (Convert.ToString(dtTrDetails.Rows[0]["pvappstatus"]) != "Pending")
				{
					Response.Redirect("~/procs/Req_RequisitionIndex.aspx?itype=Pending");
				}
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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
	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		DivViewrowWiseCandidateInformation.Visible = false;

	}
	protected void btnTra_Details_Click(object sender, EventArgs e)
	{
		if (DivRecruitment.Visible)
		{

			//DivViewrowWiseCandidateInformation.Visible = false;
			btnTra_Details.Text = "+";
			DivRecruitment.Visible = false;
		}
		else
		{
			btnTra_Details.Text = "-";
			DivRecruitment.Visible = true;

		}
	}


	#region  All_Methods

	public void InterviewerMappingRoundWiseDisplaySalarySection()
	{
		SqlParameter[] spars = new SqlParameter[40];
		DataSet DS = new DataSet();
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "InterviewerRoundWiseDisplaySalarySection";
		spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
		spars[2] = new SqlParameter("@InterviewerEmpCode", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		spars[3] = new SqlParameter("@SkillSet", SqlDbType.VarChar);
		spars[3].Value = lstSkillset.SelectedValue;
		DS = spm.getDatasetList(spars, "sp_TempData_SendForShortlisting");
		if (DS.Tables[0].Rows.Count > 0)
		{
			LiSalarySection1.Visible = true;
			LiSalarySection2.Visible = true;
			LiSalarySection3.Visible = true;
			LiSalarySection4.Visible = true;
			LiSalarySection5.Visible = true;
			LiSalarySection6.Visible = true;
			LiSalarySection7.Visible = true;
			LiSalarySection8.Visible = true;
			LiSalarySection9.Visible = true;
			Session["gridColumnHide"] = null;
		}
		else
		{
			LiSalarySection1.Visible = false;
			LiSalarySection2.Visible = false;
			LiSalarySection3.Visible = false;
			LiSalarySection4.Visible = false;
			LiSalarySection5.Visible = false;
			LiSalarySection6.Visible = false;
			LiSalarySection7.Visible = false;
			LiSalarySection8.Visible = false;
			LiSalarySection9.Visible = false;
			Session["gridColumnHide"] = "gridColumnHide";
		}

	}

	public void PopulateCadidateRecruitmentWiseData()
	{
		SqlParameter[] spars = new SqlParameter[40];
		DataSet DS = new DataSet();
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "GetData";
		spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
		spars[2] = new SqlParameter("@Recruiter_EmpCode", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.VarChar);
		spars[3].Value = hdCandidate_ID.Value;
		DS = spm.getDatasetList(spars, "sp_TempData_SendForShortlisting");
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
				// Txt_CandidateExperence.Text = dsCandidateData.Tables[0].Rows[0]["CandidateExperience_Years"].ToString();
				//  Txt_CandidateCurrentCTC.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentCTC"].ToString();
				// Txt_CandidateExpectedCTC.Text = dsCandidateData.Tables[0].Rows[0]["CandidateExpectedCTC"].ToString();
				lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();

				Txt_lstCVSource.Text = lstCVSource.SelectedItem.Text;

				lnkuplodedfileResume.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				hdfilename.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();

				//////lnkbtnIRSheet.Text = dsRecruitmentDetails.Tables[1].Rows[0]["IRSheet"].ToString();
				//////hdfilenameIRSheet.Value = dsRecruitmentDetails.Tables[1].Rows[0]["IRSheet"].ToString();

				filename = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				lnkuplodedfileResume.Visible = true;
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
				//  Txt_EducationQualifacation.Text = dsCandidateData.Tables[0].Rows[0]["EducationQualification"].ToString();
				//  Txt_Certifications.Text = dsCandidateData.Tables[0].Rows[0]["Certifications"].ToString();
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
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();

			throw;
		}

	}
	private void getMainSkillsetView()
	{
		dtmainSkillSet = spm.GetMainSkillset();
		DDLmainSkillSet.DataSource = dtmainSkillSet;
		DDLmainSkillSet.DataTextField = "ModuleDesc";
		DDLmainSkillSet.DataValueField = "ModuleId";
		DDLmainSkillSet.DataBind();
		DDLmainSkillSet.Items.Insert(0, new ListItem("Select SkillSet", ""));
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

			//lstSkillsetQue.DataSource = dtSkillset;
			//lstSkillsetQue.DataTextField = "ModuleDesc";
			//lstSkillsetQue.DataValueField = "ModuleId";
			//lstSkillsetQue.DataBind();
			//lstSkillsetQue.Items.Insert(0, new ListItem("Select Skillset", "0"));

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

			//lstPositionQue.DataSource = dtPositionName;
			//lstPositionQue.DataTextField = "PositionTitle";
			//lstPositionQue.DataValueField = "PositionTitle_ID";
			//lstPositionQue.DataBind();
			//lstPositionQue.Items.Insert(0, new ListItem("Select Position", "0"));
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
		}
	}
	public void GetInterviewer()
	{
		DataTable dtInterviewer = new DataTable();
		dtInterviewer = spm.GetRecruitment_Req_Employee_Mst();
		if (dtInterviewer.Rows.Count > 0)
		{
			//lstInterviewerOneView.DataSource = dtInterviewer;
			//lstInterviewerOneView.DataTextField = "EmployeeName";
			//lstInterviewerOneView.DataValueField = "EmployeeCode";
			//lstInterviewerOneView.DataBind();
			//lstInterviewerOneView.Items.Insert(0, new ListItem("Select Screening By", "0"));

			LstRecommPerson.DataSource = dtInterviewer;
			LstRecommPerson.DataTextField = "EmployeeName";
			LstRecommPerson.DataValueField = "EmployeeCode";
			LstRecommPerson.DataBind();
			LstRecommPerson.Items.Insert(0, new ListItem("Select Recommended Person", "0"));
		}
	}

	public void GetInterviewerScreeningBy(int ModuleId)
	{
		DataTable dtIntervie = new DataTable();
		dtIntervie = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);

		lstInterviewerOneView.DataSource = dtIntervie;
		lstInterviewerOneView.DataTextField = "EmployeeName";
		lstInterviewerOneView.DataValueField = "EmployeeCode";
		lstInterviewerOneView.DataBind();
		lstInterviewerOneView.Items.Insert(0, new ListItem("Select Screening By", "0"));
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
				txtFromdate.Text = DateTime.Now.ToString("MM/dd/yyyy");// "MM/dd/yyyy""dd-MM-yyyy HH:mm:ss"
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
			SqlParameter[] spars = new SqlParameter[4];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "GET_CTC_Candidated_Approval";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Session["Empcode"].ToString();
			spars[3] = new SqlParameter("@CTCID", SqlDbType.Int);
			spars[3].Value = Convert.ToInt32(hdnCTCID.Value);
			dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Shortlisting_CTC_Exception");
			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
				txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();
				txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
				//  txtFromdate.Text = Convert.ToDateTime(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"], CultureInfo.CurrentCulture).ToString("MM/dd/yyyy");
				txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
				//txtFromdate.Text = Convert.ToDateTime(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"], CultureInfo.CurrentCulture).ToString("dd/MM/yyyy hh:MM:ss");
				txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
				txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

				lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
				GetInterviewerScreeningBy(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));

				lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
				lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
				lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
				// lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();

				lstPositionDesign.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation_iD"]).Trim();
				//  lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
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
				// txtRequiredExperienceto.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
				LstRecommPerson.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
				txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
				lstInterviewerOneView.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
				//   lnkuplodedfileResume.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
				Lnk_Questionnaire.Text = dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"].ToString();
				HFQuestionnairename.Value = dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"].ToString();
				txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();

				//InterviewerMappingRoundWiseDisplaySalarySection();			
				if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
				{
					hdCandidate_ID.Value = Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["Candidate_ID"]).Trim();
					txtRecruterRemark.Text = Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["RecruiterRemark"]).Trim();
					HDNIntMain_ID.Value = Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["InterviewShortListMain_ID"]).Trim();
					hdnRecruterName.Value = Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["Emp_Name"]).Trim();
					hdnRecruterEmail.Value = Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["Emp_Emailaddress"]).Trim();
					chk_exception.Checked = Convert.ToBoolean(dsRecruitmentDetails.Tables[1].Rows[0]["IsException"]);
					PopulateCandidateData();
				}
				
				getApproverlist();
				
				
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	private void getApproverlist()
	{
		DataTable dtapprover = new DataTable();
		int Recruitment_ReqID = 0, CTCID = 0;
		Recruitment_ReqID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
		CTCID = Convert.ToString(hdnCTCID.Value).Trim() != "" ? Convert.ToInt32(hdnCTCID.Value) : 0;
		dtapprover = spm.CTC_Exception_Approval("GET_CTC_Exception_Approval_List", Recruitment_ReqID, 0, "", 0, CTCID,0);
		DgvApprover.DataSource = null;
		DgvApprover.DataBind();
		if (dtapprover.Rows.Count > 0)
		{
			DgvApprover.DataSource = dtapprover;
			DgvApprover.DataBind();
		}
	}

	#endregion

	protected void trvl_localbtn_Click(object sender, EventArgs e)
	{
		if (DivViewrowWiseCandidateInformation.Visible)
		{
			//trvl_localbtn.Text = "+";
			DivViewrowWiseCandidateInformation.Visible = false;
		}
		else
		{
			//trvl_localbtn.Text = "-";
			DivViewrowWiseCandidateInformation.Visible = true;

		}
	}



	protected void trvldeatils_cancel_btn_Click(object sender, EventArgs e)
	{
		lblmsg2.Text = "";
		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
		if (confirmValue != "Yes")
		{
			return;
		}
		if (Convert.ToString(txtAPPRemark.Text).Trim() == "")
		{
			lblmsg2.Text = "Please enter  approval comment";
			return;
		}
		if (Convert.ToString((hdnstaus.Value).Trim()) == "")
		{
			spm.Insert_CTC_Exception_ApproverRequest("INSERTCTC_APPROVAL", hdnnextappcode.Value, "Pending", Convert.ToInt32(hdnapprid.Value), Convert.ToInt32(hdnCTCID.Value));
			//spm.Update_CTC_Exception_ApproverRequest("UPDATE_CTC_APPROVAL", Convert.ToInt32(hdCandidate_ID.Value), hdnEmpCode.Value, "Approved", Convert.ToInt32(0), Convert.ToInt32(hdnCTCID.Value), "", txtAPPRemark.Text, 0);
		}
		else
		{
			//spm.Update_CTC_Exception_ApproverRequest("UPDATE_CTC_APPROVAL", Convert.ToInt32(hdCandidate_ID.Value), hdnEmpCode.Value, "Approved", Convert.ToInt32(0), Convert.ToInt32(hdnCTCID.Value), Convert.ToString(hdnstaus.Value).Trim(), txtAPPRemark.Text, Convert.ToInt32(HDNIntMain_ID.Value));
		}
		string ApprovalList = "";
		string RequiredByDate = "";
		ApprovalList = Get_CTC_Approve_RejectList();
		RequiredByDate = GetRequiredByDate();
		if (Convert.ToString((hdnstaus.Value).Trim()) == "")
		{
			SendMail_CTC_Next_Approval(nxtapprname, hflApproverEmail.Value, RequiredByDate, ApprovalList, Convert.ToInt32(hdnCTCID.Value));
		}
		else
		{
			var strsubject = "OneHR:- Recruitment - CTC Approved Candidates against request for - " + txtReqName.Text;
			SendMail_CTC_Recruiter(strsubject, hdnRecruterName.Value, hdnRecruterEmail.Value, RequiredByDate, ApprovalList, Convert.ToInt32(hdnCTCID.Value));
		}
	}
	public void SendMail_CTC_Next_Approval(string Approvers_Name, string ApprovalEmailID, string RequiredByDate, string applist, int CTCID)
	{
		StringBuilder strbuild = new StringBuilder();
		try
		{
			//string redirectURL = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/App_Latter_Approval.aspx?Appointment_ID=1123" + "&Type=Pending";
			string redirectURL = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/RecruiterCTCApproval.aspx.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value + "&CTCID=" + CTCID + "";
			strbuild = new StringBuilder();
			strbuild.Capacity = 0;
			strbuild.Length = 0;
			strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td> Dear " + Approvers_Name + ",</ td></tr>");
			strbuild.Append("<tr><td style='height:15px'></td></tr>");
			strbuild.Append("<tr><td><b>" + txtReqName.Text + "</b> has applied for new requisition as per the details below. Please CTC candidates approval</td></tr>");
			strbuild.Append("<tr><td><hr></td></tr>");
			strbuild.Append("<tr><td>");
			strbuild.Append("<table style='width:100%';'color:#000000;font-size:11pt;font-family:Arial;font-style:Regular>");
			//strbuild.Append("<tr><td>This is in reference to your job application for the profile of   </td></tr>");
			strbuild.Append("<tr><td style='height:5px'></td></tr>");
			strbuild.Append("<tr><td>Requisition No :</td><td> " + txtReqNumber.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Requisitioner Name :</td><td> " + txtReqName.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Requisition Date :</td><td> " + txtFromdate.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Required By Date :</td><td> " + RequiredByDate + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Department :</td><td colspan=2> " + lstPositionDept.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td>Location :</td><td colspan=2> " + lstPositionLoca.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td>Skill set :</td><td > " + lstSkillset.SelectedItem.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Position Title :</td><td colspan=2> " + lstPositionName.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td>Band :</td><td> " + lstPositionBand.SelectedItem.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>NoOfPosition :</td><td> " + txtNoofPosition.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td colspan='2'><hr></td></tr>");
			strbuild.Append("<tr><td>Candidate Name :</td><td> " + txtName.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Current CTC :</td><td> " + TxtCurrentCTC_Total.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Expected CTC :</td><td> " + TxtExpCTC_Total.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Remark :</td><td> " + txtRecruterRemark.Text + "</td></tr>");
			strbuild.Append("<tr><td>Status :</td><td colspan=2>" + applist + "</td></tr>");
			strbuild.Append("</table>");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td><a href='" + redirectURL + "' target='_blank'> Please click here to take action on CTC Approval for candidate </td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
			strbuild.Append("</table>");
			var strsubject = "OneHR:- Recruitment - CTC Approval Candidates against request for - " + txtReqName.Text;
			spm.sendMail(ApprovalEmailID, strsubject, Convert.ToString(strbuild).Trim(), "", "");
		}
		catch (Exception)
		{
			throw;
		}
	}
	public void SendMail_CTC_Recruiter(string strsubject, string Approvers_Name, string ApprovalEmailID, string RequiredByDate, string applist, int CTCID)
	{
		StringBuilder strbuild = new StringBuilder();
		try
		{
			string redirectURL = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/App_Latter_Approval.aspx?Appointment_ID=1123" + "&Type=Pending";
			strbuild = new StringBuilder();
			strbuild.Capacity = 0;
			strbuild.Length = 0;
			strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td> Dear " + Approvers_Name + ",</ td></tr>");
			strbuild.Append("<tr><td style='height:15px'></td></tr>");
			strbuild.Append("<tr><td> This is to inform you that " + Approvers_Name + " has <b>Rejected</b> request of " + hdnRecruterName.Value + " as per the details below. </td></tr>");
			strbuild.Append("<tr><td><hr></td></tr>");
			strbuild.Append("<tr><td>");
			strbuild.Append("<table style='width:100%';'color:#000000;font-size:11pt;font-family:Arial;font-style:Regular>");
			//strbuild.Append("<tr><td>This is in reference to your job application for the profile of   </td></tr>");
			strbuild.Append("<tr><td style='height:5px'></td></tr>");
			strbuild.Append("<tr><td>Requisition No :</td><td> " + txtReqNumber.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Requisitioner Name :</td><td> " + txtReqName.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Requisition Date :</td><td> " + txtFromdate.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Required By Date :</td><td> " + RequiredByDate + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Department :</td><td colspan=2> " + lstPositionDept.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td>Location :</td><td colspan=2> " + lstPositionLoca.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td>Skill set :</td><td > " + lstSkillset.SelectedItem.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Position Title :</td><td colspan=2> " + lstPositionName.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td>Band :</td><td> " + lstPositionBand.SelectedItem.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>NoOfPosition :</td><td> " + txtNoofPosition.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td colspan='2'><hr></td></tr>");
			strbuild.Append("<tr><td>Candidate Name :</td><td> " + txtName.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Current CTC :</td><td> " + TxtCurrentCTC_Total.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Expected CTC :</td><td> " + TxtExpCTC_Total.Text + "</td><td></td></tr>");
			strbuild.Append("<tr><td>Remark :</td><td> " + txtRecruterRemark.Text + "</td></tr>");
			strbuild.Append("<tr><td>Status :</td><td colspan=2>" + applist + "</td></tr>");
			strbuild.Append("</table>");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			//strbuild.Append("<tr><td><a href='" + redirectURL + "' target='_blank'> Please click here to take action on appointment letter approval </td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
			strbuild.Append("</table>");
			spm.sendMail(ApprovalEmailID, strsubject, Convert.ToString(strbuild).Trim(), "", "");
		}
		catch (Exception)
		{
			throw;
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
	protected void trvldeatils_btnSave_Click1(object sender, EventArgs e)
	{
		lblmsg2.Text = "";
		string strapprovermails = "";
		string confirmValue = hdnYesNo.Value.ToString(); //Request.Form("confirm_value"); 
		if (confirmValue != "Yes")
		{
			return;
		}
		if (Convert.ToString(txtAPPRemark.Text).Trim() == "")
		{
			lblmsg2.Text = "Please Enter Approval Comment";
			return;
		}
		strapprovermails = Get_Previous_ApproversList();
		//spm.Update_CTC_Exception_ApproverRequest("REJECT_CTC_APPROVAL", Convert.ToInt32(hdCandidate_ID.Value), hdnEmpCode.Value, "Reject", Convert.ToInt32(0), Convert.ToInt32(hdnCTCID.Value), Convert.ToString(hdnstaus.Value).Trim(), txtAPPRemark.Text, Convert.ToInt32(HDNIntMain_ID.Value));
		string ApprovalList = "", RequiredByDate = "";
		ApprovalList = Get_CTC_Approve_RejectList();
		RequiredByDate = GetRequiredByDate();
		var strsubject = "OneHR:- Recruitment - CTC Reject Candidates against request for - " + txtReqName.Text;
		SendMail_CTC_Recruiter(strsubject, hdnRecruterName.Value, hdnRecruterEmail.Value, RequiredByDate, ApprovalList, Convert.ToInt32(hdnCTCID.Value));

	}
	protected String Get_Previous_ApproversList()
	{
		String email_ids = "";
		try
		{
			DataSet dsTrDetails = new DataSet();
			SqlParameter[] spars = new SqlParameter[4];
			spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
			spars[0].Value = "Previous_CTC_APPROVAL";
			spars[1] = new SqlParameter("@CTCID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdnCTCID.Value);
			spars[2] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();
			dsTrDetails = spm.getDatasetList(spars, "SP_Shortlisting_CTC_Exception");
			if (dsTrDetails.Tables[0].Rows.Count > 0)
			{
				for (int irow = 0; irow < dsTrDetails.Tables[0].Rows.Count; irow++)
				{
					if (Convert.ToString(email_ids).Trim() == "")
						email_ids = Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
					else
						email_ids = email_ids + ";" + Convert.ToString(dsTrDetails.Tables[0].Rows[irow]["Emp_Emailaddress"]).Trim();
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
		return email_ids;

	}

	protected string Get_CTC_Approve_RejectList()
	{
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		int Recruitment_ReqID = 0, CTCID = 0;
		Recruitment_ReqID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
		CTCID = Convert.ToString(hdnCTCID.Value).Trim() != "" ? Convert.ToInt32(hdnCTCID.Value) : 0;
		dtAppRej = spm.CTC_Exception_Approval("GET_CTC_Exception_Approval", Recruitment_ReqID, 0, "", 0, CTCID,0);
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

}