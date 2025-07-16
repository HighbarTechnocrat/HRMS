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

public partial class procs_InterviewerShortList : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource, dsInterviewerShortList, dtIrSheetReport;
    public DataTable dtcandidateDetails, dtmainSkillSet, dtInterviewer1, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
    public string filename = "";

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
                   
                    hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());
                    HFQuestionnaire.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());

                    hdRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    checkInterviewerShortlistStatus_Submit();
                    GetCandidateInfoRecruitmentwisedataBind();
                    GetSkillsetName();
                    getCVSource();
                    GetSkillsetName();
                    GetPositionName();
                    GetPositionCriticality();
                    GetDepartmentMaster();
                    GetCompany_Location();
                    GetReasonRequisition();
                    GetPositionDesign();
                    GetPreferredEmpType();
                    GetlstPositionBand();
                    // GetInterviewer();
                    GetInterviewer();
                    GetecruitmentDetail();
                   
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }
    }
    protected void trvl_localbtn_Click(object sender, EventArgs e)
    {
        if (DivInterviewerShortlist.Visible)
        {
            trvl_localbtn.Text = "+";
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
            DivViewrowWiseCandidateInformation.Visible = false;
            DivInterviewerShortlist.Visible = false;
            DivInterviewerShortListButton.Visible = false;
            mobile_btnBack.Visible = false;
        }
        else
        {
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
            trvl_localbtn.Text = "-";
            DivViewrowWiseCandidateInformation.Visible = false;
            DivInterviewerShortlist.Visible = true;
            DivInterviewerShortListButton.Visible = true;
            mobile_btnBack.Visible = false;
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
        try
        {
            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }

            lblmessage.Text = "";
            foreach (GridViewRow gvrow in gvInterviewerShortlist.Rows)
            {
                DropDownList ddlSLS = (DropDownList)gvrow.FindControl("ddlShortlistingStatus");
                TextBox txtShortlistRemark = (TextBox)gvrow.FindControl("txtShortlistRemarks");
                if (ddlSLS.SelectedValue == "")
                {
                    lblmessage.Text = "Please select the Shortlisting Status";
                    return;
                }
                if (ddlSLS.SelectedValue == "2")
                {
                    if (txtShortlistRemark.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter the remarks";
                        return;
                    }
                }
            }
           
            if (lblmessage.Text == "")
            {
                string MultipleCandidateName = "";
				DataTable dtEmailtable = new DataTable();
				dtEmailtable.Columns.Add("CandidateName");
				dtEmailtable.Columns.Add("CandidateEmail");
				dtEmailtable.Columns.Add("ShortlistingStatus");
				dtEmailtable.Columns.Add("Remarks");

				foreach (GridViewRow gvrow in gvInterviewerShortlist.Rows)
                {
                    
                    DropDownList ddlSLS = (DropDownList)gvrow.FindControl("ddlShortlistingStatus");
                    TextBox      txtShortlistRemark = (TextBox)gvrow.FindControl("txtShortlistRemarks");
                    hdCandidate_ID.Value = Convert.ToString(gvInterviewerShortlist.DataKeys[gvrow.RowIndex].Values[0]).Trim();
                    HFISLMID.Value = Convert.ToString(gvInterviewerShortlist.DataKeys[gvrow.RowIndex].Values[1]).Trim();
					HiddenField HFCandidateemail = (HiddenField)gvrow.FindControl("HFEmail");
					// dtInterviewerShortList = spm.InsertSubmitShortlistingStatus(hdRecruitment_ReqID.Value, hdCandidate_ID.Value, HFISLMID.Value, ddlSLS.SelectedValue, Convert.ToString(Session["Empcode"]).Trim());
					DataRow dr = dtEmailtable.NewRow();
					dr["CandidateName"] = gvrow.Cells[1].Text;
					dr["CandidateEmail"] = HFCandidateemail.Value;
					dr["ShortlistingStatus"] = ddlSLS.SelectedItem.Text;
					dr["Remarks"] = txtShortlistRemark.Text.Trim();
					dtEmailtable.Rows.Add(dr);
					SqlParameter[] spars = new SqlParameter[8];
                    spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
                    spars[0].Value = "Update";
                    spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                    spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
                    spars[2] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
                    spars[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
                    spars[3] = new SqlParameter("@ISLMID", SqlDbType.Int);
                    spars[3].Value = Convert.ToInt32(HFISLMID.Value);
                    spars[4] = new SqlParameter("@DDlD", SqlDbType.Int);
                    spars[4].Value = Convert.ToInt32(ddlSLS.SelectedValue);
                    spars[5] = new SqlParameter("@InterviewerEmpCode", SqlDbType.VarChar);
                    spars[5].Value = Session["Empcode"].ToString();
                    spars[6] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    spars[6].Value = txtShortlistRemark.Text.Trim();

                    if (ddlSLS.SelectedValue == "1")
                    {
                        MultipleCandidateName += gvrow.Cells[1].Text + ",";
                    }
                    dsInterviewerShortList = spm.getDatasetList(spars, "sp_InsertSubmitShortlistingStatus");

					CheckReferral_Candidated(hdCandidate_ID.Value, ddlSLS.SelectedValue);
				}

                MultipleCandidateName = MultipleCandidateName.TrimEnd(',');

                SqlParameter[] sparss = new SqlParameter[4];
                sparss[0] = new SqlParameter("@mode", SqlDbType.VarChar);
                sparss[0].Value = "getvalueEmail";
                sparss[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                sparss[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
                sparss[2] = new SqlParameter("@InterviewerEmpCode", SqlDbType.VarChar);
                sparss[2].Value = Session["Empcode"].ToString();

                dsInterviewerShortList = spm.getDatasetList(sparss, "sp_InsertSubmitShortlistingStatus");

                string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_SheduleInterviewer"]).Trim() + "?Recruitment_ReqID=" + hdRecruitment_ReqID.Value;
                string StrInterviewerSchedularName = dsInterviewerShortList.Tables[0].Rows[0]["InterviewerSchedular_Name"].ToString();
                string StrInterviewerSchedularEmail = dsInterviewerShortList.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString();

                string mailsubject ="Recruitment - Schedule Interviews for shortlisted Candidates against request for  " + txtReqNumber.Text + " Of " + txtReqName.Text;
                string mailcontain = "has applied for new requisition as per the details below. Please schedule interviews.";

                string str = lstSkillset.SelectedItem.Text;
				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();
				spm.send_mailto_RecruiterSendShortListingfor_EnterviewerRecruiter1(txtReqName.Text, dsInterviewerShortList.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), StrInterviewerSchedularEmail, dsInterviewerShortList.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailsubject, MultipleCandidateName, StrInterviewerSchedularName, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, "", strLeaveRstURL, mailcontain , dtEmailtable);
				
				Response.Redirect("~/procs/Requisition_Index.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw;
        }
    }
	private void CheckReferral_Candidated(string CandidatedID,string Selected)
	{
		DataTable dtReferral = new DataTable();
		try
		{
			int StatusID = 0; string Result = "";
			if (Selected == "1")
			{
				StatusID = 5;
				Result = "Shortlisted";
			}
			else
			{
				StatusID = 8;
				//Result = "Rejected";
				Result = "Not Suitable for Current Requirements";
			}
			if (StatusID == 8)
			{
				dtReferral = spm.SearchCandidatedForReferral(CandidatedID, Convert.ToString(Session["Empcode"]).Trim(), StatusID);
				if (dtReferral.Rows.Count > 0)
				{
					var EmployeeName = dtReferral.Rows[0]["Emp_Name"].ToString();
					var EmployeeEmail = dtReferral.Rows[0]["Emp_Emailaddress"].ToString();
					var Ref_CandidateName = dtReferral.Rows[0]["Ref_CandidateName"].ToString();
					var Ref_CandidateEmail = dtReferral.Rows[0]["Ref_CandidateEmail"].ToString();
					var Gender = dtReferral.Rows[0]["Gender"].ToString();
					var Ref_CandidateMobile = dtReferral.Rows[0]["Ref_CandidateMobile"].ToString();
					var Maritalstatus = dtReferral.Rows[0]["Maritalstatus"].ToString();
					var Ref_CandidateTotalExperience = dtReferral.Rows[0]["Ref_CandidateTotalExperience"].ToString();
					var Ref_CandidateRelevantExperience = dtReferral.Rows[0]["Ref_CandidateRelevantExperience"].ToString();
					var AdditionalSkillset = dtReferral.Rows[0]["AdditionalSkillset"].ToString();
					var CREATEDON = dtReferral.Rows[0]["CREATEDON"].ToString();
					var Comments = dtReferral.Rows[0]["Comments"].ToString();
					var ModuleDesc = dtReferral.Rows[0]["ModuleDesc"].ToString();
					var Subject = "Referred Candidate “" + Ref_CandidateName + "” is “" + Result + "”";
					var Body = "This is to inform you that the candidate referred by you is “" + Result + "”.Refer following details.";
					spm.Ref_Candidated_send_mailto_recruitmentModule(Ref_CandidateName, Subject, Body, EmployeeEmail, EmployeeName, CREATEDON,
					Ref_CandidateEmail, "", Ref_CandidateMobile, Gender, Maritalstatus, ModuleDesc, Ref_CandidateTotalExperience, Ref_CandidateRelevantExperience, AdditionalSkillset, Comments);

				}
			}
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

	protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        if (DivRecruitment.Visible)
        {
            trvl_localbtn.Text = "+";
            DivViewrowWiseCandidateInformation.Visible = false;

            // DivSendshortlisting2.Visible = false;
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
            DivInterviewerShortlist.Visible = false;
            DivInterviewerShortListButton.Visible = false;

        }
        else
        {
            trvl_localbtn.Text = "+";
            btnTra_Details.Text = "-";
            DivRecruitment.Visible = true;
            DivViewrowWiseCandidateInformation.Visible = false;
            DivInterviewerShortlist.Visible = false;
            DivInterviewerShortListButton.Visible = false;

        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        //HFRecruitment_ReqID.Value = Convert.ToString(gvInterviewerInoxList.DataKeys[row.RowIndex].Values[0]).Trim();
        //Response.Redirect("InterviewerShortList.aspx?Recruitment_ReqID=" + HFRecruitment_ReqID.Value);
        hdCandidate_ID.Value = Convert.ToString(gvInterviewerShortlist.DataKeys[row.RowIndex].Values[0]).Trim();
        DivViewrowWiseCandidateInformation.Visible = true;
        PopulateCandidateData();
		GetRequisitiodetailsCandidate();
		mobile_btnBack.Visible = true;
    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        DivViewrowWiseCandidateInformation.Visible = false;
        mobile_btnBack.Visible = false;
    }

    protected void gvInterviewerShortlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ddlShortlistStatus = (DropDownList)e.Row.FindControl("ddlShortlistingStatus");
            ddlShortlistStatus.DataSource = dsCVSource.Tables[1];
            ddlShortlistStatus.DataTextField = "ShortlistingStatus";
            ddlShortlistStatus.DataValueField = "ShortlistingStatus_ID";
            ddlShortlistStatus.DataBind();
            ddlShortlistStatus.Items.Insert(0, new ListItem("--Select--", ""));

        }
    }

    #region  All_methods

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

        // txtName.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();

        if (DS.Tables[1].Rows.Count > 0)
        {
            Txt_NoticePeriodInday.Text = DS.Tables[1].Rows[0]["NoticePeriod"].ToString();
            TxtCurrentCTC_Total.Text = DS.Tables[1].Rows[0]["CurrentCTC"].ToString();
            TxtExpCTC_Total.Text = DS.Tables[1].Rows[0]["ExpectedCTC"].ToString();
            TxtTotalExperienceYrs.Text = DS.Tables[1].Rows[0]["ExperienceYear"].ToString();
            TxtRelevantExpYrs.Text = DS.Tables[1].Rows[0]["RelevantExpYrs"].ToString();

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
            Txt_OtherNatureOfIndustryClient.Text = DS.Tables[1].Rows[0]["OtherNatureOfIndustryClient"].ToString();
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
                Txt_OtherNatureOfIndustryClient.Text = "";
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
                TxtReasonforBreak.Text = "";
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

    private void checkInterviewerShortlistStatus_Submit()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_InterviewerShortlist_Status";
            spars[1] = new SqlParameter("@req_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();
            dtTrDetails = spm.getMobileRemDataList(spars, "Usp_InterviewerRecruiter_Detail_All");
            if (dtTrDetails.Rows.Count > 0)
            {

            }
            else
            {
                Response.Redirect("~/procs/Requisition_Index.aspx");
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
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
              //  Txt_CandidateCurrentLocation.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentLocation"].ToString();
                Txt_CandidateBirthday.Text = dsCandidateData.Tables[0].Rows[0]["CandidateBirthday"].ToString();
                Txt_CandidatePAN.Text = dsCandidateData.Tables[0].Rows[0]["CandidatePAN"].ToString();
                lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();
                Txt_lstCVSource.Text = lstCVSource.SelectedItem.Text;
                lnkuplodedfileResume.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                hdfilename.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                filename = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                TxtAadharNo.Text = dsCandidateData.Tables[0].Rows[0]["AdharNo"].ToString();
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
             //   Txt_EducationQualifacation.Text = dsCandidateData.Tables[0].Rows[0]["EducationQualification"].ToString();
             //   Txt_Certifications.Text = dsCandidateData.Tables[0].Rows[0]["Certifications"].ToString();
                GetSkillsetName();
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
                else
                {
                    gvotherfile.DataSource = null;
                    gvotherfile.DataBind();
                }
                if(Txt_CandidateBirthday.Text !="")
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
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();
            throw;
        }

    }

    public void PopulateCadidateShortListStatus()
    {
        try
        {
            string strreqCandidate_ID = hdCandidate_ID.Value;
            dsCandidateData = spm.getSearchCandidateList(strreqCandidate_ID);
            if (dsCandidateData.Tables[0].Rows.Count > 0)
            {
                hdCandidate_ID.Value = dsCandidateData.Tables[0].Rows[0]["Candidate_ID"].ToString();
                txtName.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            Response.End();

            throw;
        }

    }
    private void getCVSource()
    {
        dsCVSource = spm.GetCVSource();
        lstCVSource.DataSource = dsCVSource.Tables[0];
        lstCVSource.DataTextField = "CVSource";
        lstCVSource.DataValueField = "CVSource_ID";
        lstCVSource.DataBind();
        lstCVSource.Items.Insert(0, new ListItem("Select CVSource", ""));

        // dsCVSource = spm.GetCVSource();

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

            DDLmainSkillSet.DataSource = dtSkillset;
            DDLmainSkillSet.DataTextField = "ModuleDesc";
            DDLmainSkillSet.DataValueField = "ModuleId";
            DDLmainSkillSet.DataBind();
            DDLmainSkillSet.Items.Insert(0, new ListItem("Select SkillSet", ""));

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
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "RecruitmentReq_InterviewerShortlistingEdit";
            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[2].Value = Session["Empcode"].ToString();
            dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

            if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
            {
                txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
                txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();

                //  txtFromdate.Text = Convert.ToDateTime(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"], CultureInfo.CurrentCulture).ToString("MM/dd/yyyy");
                txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
                //txtFromdate.Text = Convert.ToDateTime(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"], CultureInfo.CurrentCulture).ToString("dd/MM/yyyy hh:MM:ss");
                txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
                txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
                txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

                lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
                GetInterviewerScreeningBy(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));

                lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
                lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
                lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
                txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
              //  lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();

                lstPositionDesign.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation_iD"]).Trim();
             //   lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
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
                LstRecommPerson.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
                txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
              //txtInterviewerOptOne.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1_OPT"]).Trim();
              //txtInterviewerOptTwo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2_OPT"]).Trim();
                lstInterviewerOneView.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
              //lstInterviewerTwo.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter2"]).Trim();
                Lnk_Questionnaire.Text = dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"].ToString();
                HFQuestionnairename.Value = dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"].ToString();
                txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
                if (dsRecruitmentDetails.Tables[2].Rows.Count > 0)
                {
                    trvldeatils_btnSave.Visible = false;
                }
                if (dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"].ToString().Trim() == "Cancelled")
                {
                    trvldeatils_btnSave.Visible = false;
                }
              //  GetFilterGD();
                if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
                {
                    gvInterviewerShortlist.DataSource = dsRecruitmentDetails.Tables[1];
                    gvInterviewerShortlist.DataBind();
                }
                else
                {
                    gvInterviewerShortlist.DataSource = null;
                    gvInterviewerShortlist.DataBind();
                }

                    // PopulateCandidateData();




                }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
	//private void GetFilterGD()
	//{
	//    int Quest_ID = 0;
	//    txtJobDescription.Text = "";
	//    DataTable JDBankList = new DataTable();
	//    string Stype = "getAssignJDBank_Filter";
	//    JDBankList = spm.GetAssign_JDBankFilter(Stype, Convert.ToInt32(lstSkillset.SelectedValue), Convert.ToInt32(lstPositionName.SelectedValue));
	//    if (JDBankList.Rows.Count > 0)
	//    {
	//        txtJobDescription.Text = Convert.ToString(JDBankList.Rows[0]["JobDescription"]).Trim();
	//        hdnBankDetailID.Value = Convert.ToString(JDBankList.Rows[0]["JD_BankDetail_ID"]).Trim();
	//        //mobile_cancel.Visible = true;
	//    }
	//}

	#endregion
	private void GetRequisitiodetailsCandidate()
	{
		try
		{
			//dsCandidateData = new DataSet();
			SqlParameter[] spars = new SqlParameter[9];
			spars[0] = new SqlParameter("@Mode", SqlDbType.NVarChar);
			spars[0].Value = "GetRequisitiodetailsCandidate";
			spars[1] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdCandidate_ID.Value);
			var dsCandidateRecq = spm.getServiceRequestReportCount(spars, "sp_RecCreateCandidate");
			gvMngTravelRqstList.DataSource = null;
			gvMngTravelRqstList.DataBind();
			if (dsCandidateRecq != null)
			{
				if (dsCandidateRecq.Tables.Count > 0)
				{
					var getTable1 = dsCandidateRecq.Tables[0];
					if (getTable1.Rows.Count > 0)
					{
						RecordCount.Visible = true;
						hr1.Visible = true;
						gvMngTravelRqstList.DataSource = getTable1;
						gvMngTravelRqstList.DataBind();

					}
				}
			}
		}
		catch (Exception ex)
		{

			throw;
		}
	}
	protected void lnkView_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdRecruitment_ReqID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[0]).Trim();
		hdCandidate_ID.Value = Convert.ToString(gvMngTravelRqstList.DataKeys[row.RowIndex].Values[1]).Trim();
		txtRec_No.Text = row.Cells[1].Text;
		txtPositionInterviwed.Text = row.Cells[4].Text;
		txtpostionTitle.Text = row.Cells[3].Text;
		getIrSheetSummary();
		this.ModalPopupExtenderIRSheet.Show();
	}
	private void getIrSheetSummary()
	{

		//txtRec_No.Text = txtReqNumber.Text;
		txtCandidateName.Text = txtName.Text;
		//txtPositionInterviwed.Text = lstSkillset.SelectedItem.Text;
		txttotalExperince.Text = TxtTotalExperienceYrs.Text;
		txtRelevantExp.Text = TxtRelevantExpYrs.Text;
		//txtpostionTitle.Text = lstPositionName.SelectedItem.Text;
		//this.DgvIrSheetSummary.Columns.RemoveAt(this.DgvIrSheetSummary.Columns.Count - 1);
		DgvIrSheetSummary.DataSource = null;
		DgvIrSheetSummary.DataBind();
		GrdIRIntSummary.DataSource = null;
		GrdIRIntSummary.DataBind();
		DataTable dtMerged = new DataTable();
		dtIrSheetReport = new DataSet();
		dtIrSheetReport = spm.Get_Rec_Recruit_IrSheetDetails("GetIrSheetSummary", Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
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
}