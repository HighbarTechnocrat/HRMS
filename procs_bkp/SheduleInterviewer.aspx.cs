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

public partial class procs_SheduleInterviewer : System.Web.UI.Page
{

    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource;
    public DataTable dtInterviewerSchedule, dtcandidateDetails, dtmainSkillSet, dtInterviewer1;
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
                    hdRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    checkInterviewerShortlistStatus_Submit();
                 txtInterviewDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    txtInterviewTime.Attributes.Add("onkeypress", "return onCharOnlyNumber_Time(event);");
                    getMainSkillsetView();
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
                    GetInterviewer();
                    GetCandidateInfoRecruitmentwisedataBind();
                    lstInterviewerOne.Enabled = false;
                    GetecruitmentDetail();
                    RecruiterSendShortListingInterviewer();
                  
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
            DivSheduleInterview.Visible = false;
           
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
            DivSheduleInterview.Visible = false;
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
            string[] strdate;
            DataSet dsInterviewSchedule = new DataSet();
            lblmessage.Text = "";

            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }

            if (DDlSheduleStatus.SelectedValue == "5")
            {

                if (DDLInterviewRound.SelectedValue == "")
                {
                    lblmessage.Text = "Please Select the interview round";
                    return;
                }
                if (lstInterviewerOne.SelectedValue == "0" || lstInterviewerOne.SelectedItem.Text == "")
                {
                    lblmessage.Text = "Please enter the Interviewer";
                    return;
                }

                if (txtInterviewDate.Text == "")
                {
                    lblmessage.Text = "Please Select the interview date";
                    return;
                }
                if (txtInterviewTime.Text == "")
                {
                    lblmessage.Text = "Please Select the interview time";
                    return;
                }

                if (Convert.ToString(txtInterviewTime.Text).Trim() != "")
                {
                    strdate = Convert.ToString(txtInterviewTime.Text).Trim().Split(':');
                    if (strdate.Length == 2)
                    {
                        if (strdate[0].Length != 2 || strdate[1].Length != 2)
                        {
                            lblmessage.Text = "Please enter correct Time.";
                            return;
                        }
                        else
                        {
                            if (Convert.ToInt32(strdate[0].ToString()) > 24)
                            {
                                lblmessage.Text = "Please enter correct Time.";
                                return;
                            }
                            if (Convert.ToInt32(strdate[1].ToString()) > 59)
                            {
                                lblmessage.Text = "Please enter correct interview Time.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Please enter correct interview Time.";
                        return;
                    }
                }


                if (lblmessage.Text == "")
                {

                    string[] strdate1;
                    string strtoDate = "";
                    strdate1 = Convert.ToString(txtInterviewDate.Text).Trim().Split('/');
                    strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
                    DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                    SqlParameter[] spars = new SqlParameter[9];
                    spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                    spars[0].Value = "InterviewScheduleInsert";

                    spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                    spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);

                    spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                    spars[2].Value = Session["Empcode"].ToString();

                    spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
                    spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);

                    spars[4] = new SqlParameter("@InterviewShortListMain_ID", SqlDbType.Int);
                    spars[4].Value = Convert.ToInt32(HFISLMID.Value);

                    spars[5] = new SqlParameter("@InterviewRound_ID", SqlDbType.VarChar);
                    spars[5].Value = DDLInterviewRound.SelectedValue;

                    spars[6] = new SqlParameter("@InterviewDate", SqlDbType.DateTime);
                    spars[6].Value = Convert.ToDateTime(ddt);

                    spars[7] = new SqlParameter("@InterviewTime", SqlDbType.VarChar);
                    spars[7].Value = txtInterviewTime.Text;

                    spars[8] = new SqlParameter("@Emp_Code_Inter1", SqlDbType.VarChar);
                    spars[8].Value = lstInterviewerOne.SelectedValue;

                    dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");
                    DivSheduleInterview.Visible = false;
                    DivInterviewerShortListButton.Visible = false;
                    string stricsrID = dsInterviewSchedule.Tables[0].Rows[0]["ISCSRID"].ToString();
                    string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_InterviewerFeedback"]).Trim() + "?ReqID=" + hdRecruitment_ReqID.Value + "&CanID=" + hdCandidate_ID.Value + "&CanSRID=" + stricsrID;

                    string mailsubject = "Recruitment - Interview scheduled for a Candidate against request for  " + txtReqNumber.Text + " Of " + txtReqName.Text;
                    string mailcontain = "Interview for the below candidate is scheduled. Please find the details below;";
                    string RequiredByDate = "";
                    RequiredByDate = GetRequiredByDate();
                    //  spm.send_mailto_ScheduleInterview(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), "Recruitment - Interview scheduled for a Candidate against request for  " + txtReqNumber.Text + " Of " + txtReqName.Text, Session["SendEmailInterviername"].ToString(), txtName.Text, DDLInterviewRound.SelectedItem.Text,txtInterviewDate.Text, txtInterviewTime.Text, strLeaveRstURL);
                    spm.send_mailto_ScheduleInterviewAndNxtroundShedule(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress1"].ToString(), mailsubject, lstInterviewerOne.SelectedItem.Text, txtName.Text, DDLInterviewRound.SelectedItem.Text, txtInterviewDate.Text, txtInterviewTime.Text, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, strLeaveRstURL, mailcontain);
					CheckReferral_Candidated(hdCandidate_ID.Value.ToString(), "5");

				}
            }
            else
            {
                SqlParameter[] spars = new SqlParameter[6];
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "InterviewScheduleInsertbackout";

                spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);

                spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                spars[2].Value = Session["Empcode"].ToString();

                spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
                spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);

                spars[4] = new SqlParameter("@InterviewShortListMain_ID", SqlDbType.Int);
                spars[4].Value = Convert.ToInt32(HFISLMID.Value);

                spars[5] = new SqlParameter("@Emp_Code_Inter1", SqlDbType.VarChar);
                spars[5].Value = Session["Empcode"].ToString();

                dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");
                DivSheduleInterview.Visible = false;
                DivInterviewerShortListButton.Visible = false;
                string stricsrID = dsInterviewSchedule.Tables[0].Rows[0]["ISCSRID"].ToString();
                string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_InterviewerFeedback"]).Trim() + "?ReqID=" + hdRecruitment_ReqID.Value + "&CanID=" + hdCandidate_ID.Value + "&CanSRID=" + stricsrID;

                string mailsubject = "Recruitment - Candidate Backout against request for  " + txtReqNumber.Text + " Of " + txtReqName.Text;
                string mailcontain = "This is to inform you that the following candidate has Backed out during the interview scheduling process.";
                string RequiredByDate = "";
                RequiredByDate = GetRequiredByDate(); // 
                spm.send_mailto_ScheduleInterviewBackout(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress1"].ToString(), "", mailsubject, dsInterviewSchedule.Tables[0].Rows[0]["Recruitername1"].ToString(), txtName.Text, "1st Round",txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, mailcontain);
				CheckReferral_Candidated(hdCandidate_ID.Value.ToString(), "3");
			}

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw;
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
	private void CheckReferral_Candidated(string CandidatedID, string Selected)
	{
		DataTable dtReferral = new DataTable();
		try
		{
			int StatusID = 0; string Result = "",A="";
			if (Selected == "5")
			{
				StatusID = 6;
				Result = "Being Interviewed";
				A = "is";
			}
			else
			{
				StatusID = 9;
				A = "has";
				Result = "Backed Out";
			}
			if (StatusID == 9)
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
					var Subject = "Referred Candidate “" + Ref_CandidateName + "” " + A + " “" + Result + "”";
					var Body = "This is to inform you that the candidate referred by you " + A + " “" + Result + "”.Refer following details.";
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
            DivSheduleInterview.Visible = false;

        }
        else
        {
            trvl_localbtn.Text = "+";
            btnTra_Details.Text = "-";
            DivRecruitment.Visible = true;
            DivViewrowWiseCandidateInformation.Visible = false;
            DivInterviewerShortlist.Visible = false;
            DivInterviewerShortListButton.Visible = false;
            DivSheduleInterview.Visible = false;

        }
    }

    protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdCandidate_ID.Value = Convert.ToString(gvInterviewerShortlist.DataKeys[row.RowIndex].Values[0]).Trim();
        DivViewrowWiseCandidateInformation.Visible = true;
        PopulateCandidateData();
        mobile_btnBack.Visible = true;
        DivSheduleInterview.Visible = false;
        DivInterviewerShortListButton.Visible = true;
        trvldeatils_btnSave.Visible = false;

    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        DivViewrowWiseCandidateInformation.Visible = false;
        mobile_btnBack.Visible = false;
        DivSheduleInterview.Visible = false;
    }

    protected void lnkEditInterShedule_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdCandidate_ID.Value = Convert.ToString(gvInterviewerShortlist.DataKeys[row.RowIndex].Values[0]).Trim();
        HFISLMID.Value = Convert.ToString(gvInterviewerShortlist.DataKeys[row.RowIndex].Values[1]).Trim();
        DivViewrowWiseCandidateInformation.Visible = false;
        DivSheduleInterview.Visible = true;
        PopulateCandidateData();
        DivInterviewerShortListButton.Visible = true;

        mobile_btnBack.Visible = true;
        mobile_btnBack.Focus();
        GetSheduleStatusBind();
    }

    protected void gvInterviewerShortlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField StrCandidate = (HiddenField)e.Row.FindControl("hdGRIDCandidate_ID");
            string ss = StrCandidate.Value;
            dsCandidateData = spm.getSearchCandidateListSheduleInterview(StrCandidate.Value, hdRecruitment_ReqID.Value);
            if (dsCandidateData.Tables[2].Rows.Count > 0)
            {
                e.Row.BackColor = System.Drawing.Color.Red;
            }
        }
    }

    #region  All_Method

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
         //   TxtCurrentCTC_Total.Text = DS.Tables[1].Rows[0]["CurrentCTC"].ToString();
          //  TxtExpCTC_Total.Text = DS.Tables[1].Rows[0]["ExpectedCTC"].ToString();
            TxtTotalExperienceYrs.Text = DS.Tables[1].Rows[0]["ExperienceYear"].ToString();
            TxtRelevantExpYrs.Text = DS.Tables[1].Rows[0]["RelevantExpYrs"].ToString();

            //if (DS.Tables[1].Rows[0]["CurrentCTC_Fixed"].ToString() == "0.00")
            //{
            //    Txt_CurrentCTC_Fixed.Text = "";
            //}
            //else
            //{
            //    Txt_CurrentCTC_Fixed.Text = DS.Tables[1].Rows[0]["CurrentCTC_Fixed"].ToString();
            //}

            //if (DS.Tables[1].Rows[0]["CurrentCTC_Variable"].ToString() == "0.00")
            //{
            //    TxtCurrentCTC_Variable.Text = "";
            //}
            //else
            //{
            //    TxtCurrentCTC_Variable.Text = DS.Tables[1].Rows[0]["CurrentCTC_Variable"].ToString();
            //}

            //if (DS.Tables[1].Rows[0]["ExpCTC_Fixed"].ToString() == "0.00")
            //{
            //    TxtExpCTC_Fixed.Text = "";
            //}
            //else
            //{
            //    TxtExpCTC_Fixed.Text = DS.Tables[1].Rows[0]["ExpCTC_Fixed"].ToString();
            //}
            //if (DS.Tables[1].Rows[0]["ExpCTC_Variable"].ToString() == "0.00")
            //{
            //    TxtExpCTC_Variable.Text = "";
            //}
            //else
            //{
            //    TxtExpCTC_Variable.Text = DS.Tables[1].Rows[0]["ExpCTC_Variable"].ToString();
            //}
            Txt_BaseLocationcurrentcompany.Text = DS.Tables[1].Rows[0]["LocationCurrentCompany"].ToString();
            Txt_CurrentLocation.Text = DS.Tables[1].Rows[0]["CurrentLocation"].ToString();
            txtcurrentlocation1.Text = DS.Tables[1].Rows[0]["CurrentLocation"].ToString();
            Txt_NativeHomeLocation.Text = DS.Tables[1].Rows[0]["NativeLocation"].ToString();
            Txt_CurrentRoleorganization.Text = DS.Tables[1].Rows[0]["CurrentRoleorganization"].ToString();
            Txt_TravelContraintPandemicSituation.Text = DS.Tables[1].Rows[0]["TravelPandemicSituation"].ToString();
            //  Txt_CurrentRoleorganization.Text = DS.Tables[0].Rows[0]["CandidateCurrentRole"].ToString();
            TxtReasonforBreak.Text = DS.Tables[1].Rows[0]["Reasonforbreak"].ToString();
            Txt_RoleDomaincompany.Text = DS.Tables[1].Rows[0]["RoleDomainCompany"].ToString();
            Txt_lookingforChange.Text = DS.Tables[1].Rows[0]["lookingChangeReason"].ToString();
            Txt_OtherNatureOfIndustryClient.Text = DS.Tables[1].Rows[0]["OtherNatureOfIndustryClient"].ToString();


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
            dsCandidateData = spm.getSearchCandidateListSheduleInterview(strreqCandidate_ID, hdRecruitment_ReqID.Value);
            if (dsCandidateData.Tables[0].Rows.Count > 0)
            {
                hdCandidate_ID.Value = dsCandidateData.Tables[0].Rows[0]["Candidate_ID"].ToString();
                // HFISLMID.Value = dsCandidateData.Tables[0].Rows[0]["InterviewShortListMain_ID"].ToString();
                txtName.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();
                txtname1.Text = dsCandidateData.Tables[0].Rows[0]["CandidateName"].ToString();
                txtEmail.Text = dsCandidateData.Tables[0].Rows[0]["CandidateEmail"].ToString();
                txtemail1.Text = dsCandidateData.Tables[0].Rows[0]["CandidateEmail"].ToString();
                Txt_CandidateMobile.Text = dsCandidateData.Tables[0].Rows[0]["CandidateMobile"].ToString();
                txtmobile1.Text = dsCandidateData.Tables[0].Rows[0]["CandidateMobile"].ToString();

                lstCandidategender.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CandidateGender"].ToString();
                lstMaritalStatus.SelectedValue = dsCandidateData.Tables[0].Rows[0]["Maritalstatus"].ToString();
              //  Txt_CandidateCurrentLocation.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentLocation"].ToString();
                txtcurrentlocation1.Text = dsCandidateData.Tables[0].Rows[0]["CandidateCurrentLocation"].ToString();
                Txt_CandidateBirthday.Text = dsCandidateData.Tables[0].Rows[0]["CandidateBirthday"].ToString();
                Txt_CandidatePAN.Text = dsCandidateData.Tables[0].Rows[0]["CandidatePAN"].ToString();
                lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();
                Txt_Comments.Text = dsCandidateData.Tables[0].Rows[0]["Comments"].ToString(); 

                Txt_lstCVSource.Text = lstCVSource.SelectedItem.Text;
                TxtAadharNo.Text = dsCandidateData.Tables[0].Rows[0]["AdharNo"].ToString();
                lnkuplodedfileResume.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                lnkuplodedfile1.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                hdfilename.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
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
             
                getMainSkillsetView();
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

                if (dsCandidateData.Tables[2].Rows.Count > 0)
                {
                    int recordCount = dsCandidateData.Tables[2].Rows.Count;
                    for (int i = 0; i < dsCandidateData.Tables[2].Rows.Count; i++)
                    {
                        int ii = i;
                        if (recordCount - 1 == ii)
                        {
                            DDLInterviewRound.SelectedValue = dsCandidateData.Tables[2].Rows[i]["InterviewRound_ID"].ToString();
                            string strInterviewStatusID = dsCandidateData.Tables[2].Rows[i]["InterviewStatus_ID"].ToString();
                            if (strInterviewStatusID == "" || strInterviewStatusID == "1" || strInterviewStatusID == "2" ||  strInterviewStatusID == "4")
                            {
                                DDlSheduleStatus.SelectedValue = "5";
                            }
                            else
                            {
                                DDlSheduleStatus.SelectedValue = "3";
                            }

                            txtInterviewDate.Text = dsCandidateData.Tables[2].Rows[i]["InterviewDate"].ToString();
                            txtInterviewTime.Text = dsCandidateData.Tables[2].Rows[i]["InterviewTime"].ToString();
                           // GetInterviewerDependingOnSkillSetANDInterviewRound();
                            GetInterviewerName();
                            lstInterviewerOne.SelectedValue = dsCandidateData.Tables[2].Rows[i]["Emp_Code_Inter1"].ToString();

                            if(strInterviewStatusID =="3")
                            {
                                DDLInterviewRound.SelectedValue = "";
                                lstInterviewerOne.SelectedValue = "0";
                                txtInterviewDate.Text = "";
                                txtInterviewTime.Text = "";
                            }

                            DDLInterviewRound.Enabled = false;
                            txtInterviewDate.Enabled = false;
                            txtInterviewTime.Enabled = false;
                            trvldeatils_btnSave.Visible = false;
                            DDlSheduleStatus.Enabled = false;

                        }
                        else
                        {
                            DDlSheduleStatus.Enabled = true;
                            trvldeatils_btnSave.Visible = true;
                            DDLInterviewRound.SelectedValue = "";
                            txtInterviewDate.Text = "";
                            txtInterviewTime.Text = "";
                            txtInterviewDate.Enabled = false;
                            txtInterviewTime.Enabled = false;
                            DDLInterviewRound.Enabled = false;

                            // lstInterviewerOne.SelectedValue = "0";
                        }
                    }
                }
                else
                {
                    trvldeatils_btnSave.Visible = true;
                    DDLInterviewRound.SelectedValue = "";
                    txtInterviewDate.Text = "";
                    txtInterviewTime.Text = "";
                    lstInterviewerOne.Enabled = false;
                    lstInterviewerOne.Items.Clear();
                    DDLInterviewRound.Enabled = false;
                    txtInterviewDate.Enabled = false;
                    txtInterviewTime.Enabled = false;
                    DDlSheduleStatus.Enabled = true;

                }

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
            }

                DataSet dsRecruitmentDetailss = new DataSet();
                SqlParameter[] spars1 = new SqlParameter[3];
                spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars1[0].Value = "RecruitmentReq_InterviewerSheduleEdit";
                spars1[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                spars1[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
                spars1[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                spars1[2].Value = Session["Empcode"].ToString();
                dsRecruitmentDetailss = spm.getDatasetList(spars1, "SP_Recruitment_Requisition_INSERT");
            if (dsRecruitmentDetailss.Tables[2].Rows.Count > 0)
            {
                trvldeatils_btnSave.Visible = false;
            }
            if (dsRecruitmentDetailss.Tables[0].Rows[0]["Request_status"].ToString().Trim() == "Cancelled")
            {
                trvldeatils_btnSave.Visible = false;
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
            dsCandidateData = spm.getSearchCandidateListSheduleInterview(strreqCandidate_ID, hdRecruitment_ReqID.Value);
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
        lstCVSource.DataSource = dsCVSource.Tables[0];
        lstCVSource.DataTextField = "CVSource";
        lstCVSource.DataValueField = "CVSource_ID";
        lstCVSource.DataBind();
        lstCVSource.Items.Insert(0, new ListItem("Select CVSource", ""));


        DDLInterviewRound.DataSource = dsCVSource.Tables[2];
        DDLInterviewRound.DataTextField = "InterviewRound";
        DDLInterviewRound.DataValueField = "InterviewRound_ID";
        DDLInterviewRound.DataBind();
        DDLInterviewRound.Items.Insert(0, new ListItem("Select Interview Round", ""));
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
    private void checkInterviewerShortlistStatus_Submit()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_Sheduleinterview_Status";
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
    public void GetInterviewer()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Req_Employee_Mst();
        if (dtInterviewer.Rows.Count > 0)
        {
            LstRecommPerson.DataSource = dtInterviewer;
            LstRecommPerson.DataTextField = "EmployeeName";
            LstRecommPerson.DataValueField = "EmployeeCode";
            LstRecommPerson.DataBind();
            LstRecommPerson.Items.Insert(0, new ListItem("Select Recommended Person", "0"));
        }
    }

    public void GetInterviewerName()
    {
        DataTable dtInterviewer = new DataTable();
        dtInterviewer = spm.GetRecruitment_Req_Employee_Mst();
        if (dtInterviewer.Rows.Count > 0)
        {
            lstInterviewerOne.Enabled = false;
            lstInterviewerOne.DataSource = dtInterviewer;
            lstInterviewerOne.DataTextField = "EmployeeName";
            lstInterviewerOne.DataValueField = "EmployeeCode";
            lstInterviewerOne.DataBind();
            lstInterviewerOne.Items.Insert(0, new ListItem("Select Interviewer", "0"));

        }
    }

    public void GetInterviewerDependingOnSkillSetANDInterviewRound()
    {
        DataSet DSInterviewSkillSetInterRound = new DataSet();

        SqlParameter[] spars = new SqlParameter[5];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetInterviewerOnSkillSetANDInterRound";
        spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
        spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
        spars[2] = new SqlParameter("@ModuleId", SqlDbType.Int);
        spars[2].Value = Convert.ToInt32(lstSkillset.SelectedValue);
        spars[3] = new SqlParameter("@InterviewRoundID", SqlDbType.Int);
        spars[3].Value = Convert.ToInt32(DDLInterviewRound.SelectedValue);
        DSInterviewSkillSetInterRound = spm.getDatasetList(spars, "SP_GetInterviewerDependingOnSkillSetANDInterviewRound");
        if (DSInterviewSkillSetInterRound.Tables[0].Rows.Count > 0)
        {
            lstInterviewerOne.Enabled = true;
            lstInterviewerOne.DataSource = DSInterviewSkillSetInterRound.Tables[0];
            lstInterviewerOne.DataTextField = "Emp_Name";
            lstInterviewerOne.DataValueField = "Emp_Code";
            lstInterviewerOne.DataBind();
            lstInterviewerOne.Items.Insert(0, new ListItem("Select Interviewer", "0"));
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
            spars[0].Value = "RecruitmentReq_InterviewerSheduleEdit";
            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[2].Value = Session["Empcode"].ToString();
            dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

            if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
            {
                txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
                txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();

                txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
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
              //  lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ReasonRequisition_ID"]).Trim();
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
                lstInterviewerOneView.SelectedValue= Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
                txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();

                // GetFilterGD();
                if (dsRecruitmentDetails.Tables[2].Rows.Count > 0)
                {
                    trvldeatils_btnSave.Visible = false;
                }
                if (dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"].ToString().Trim() == "Cancelled")
                {
                    trvldeatils_btnSave.Visible = false;
                }

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
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void RecruiterSendShortListingInterviewer()
    {
        string strRecruitment_ReqID = Request.QueryString["Recruitment_ReqID"];
        hdRecruitment_ReqID.Value = Request.QueryString["Recruitment_ReqID"];
        dsRecEmpCodeInterviewer1 = spm.getRecruiterInterviewerCode1(strRecruitment_ReqID);
       // lst = dsRecEmpCodeInterviewer1.Tables[0].Rows[0]["EmpName"].ToString();
        Session["SendEmailInterviername"] = dsRecEmpCodeInterviewer1.Tables[0].Rows[0]["SendEmail"].ToString();

    }

    public void GetSheduleStatusBind()
    {
        DataSet DSSheduleStatus = new DataSet();

        SqlParameter[] spars = new SqlParameter[5];
        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
        spars[0].Value = "GetSheduleStatus";
        
        DSSheduleStatus = spm.getDatasetList(spars, "SP_GetInterviewerDependingOnSkillSetANDInterviewRound");
        if (DSSheduleStatus.Tables[0].Rows.Count > 0)
        {
            DDlSheduleStatus.DataSource = DSSheduleStatus.Tables[0];
            DDlSheduleStatus.DataTextField = "InterviewStatus";
            DDlSheduleStatus.DataValueField = "InterviewStatus_ID";
            DDlSheduleStatus.DataBind();
            DDlSheduleStatus.Items.Insert(0, new ListItem("Select Interview Scheduled Status", "0"));
        }
    }

    #endregion

    protected void DDLInterviewRound_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInterviewerDependingOnSkillSetANDInterviewRound();
    }

    protected void DDlSheduleStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDlSheduleStatus.SelectedValue == "5")
        {
            DDLInterviewRound.Enabled = true;
            lstInterviewerOne.Enabled = false;
            txtInterviewDate.Enabled = true;
            txtInterviewTime.Enabled = true;
        }
        else
        {
            DDLInterviewRound.Enabled = false;
            lstInterviewerOne.Enabled = false;
            txtInterviewDate.Enabled = false;
            txtInterviewTime.Enabled = false;
            DDLInterviewRound.SelectedValue = "";
            lstInterviewerOne.SelectedValue = "0";
            txtInterviewDate.Text = "";
            txtInterviewTime.Text = "";


        }
    }
}