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
public partial class procs_RecruiterReschedule : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
	DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource, dtIrSheetReport;
	public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet, dtInterviewer1, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
	public string filename = "", multiplefilename = "", multiplefilenameadd = "";
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
                    hdfilefathIRSheet.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheet"]).Trim());
					hdnInterviewphtoPath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_InterviewerPhoto"]).Trim());

					hdRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
                    hdCandidate_ID.Value = Convert.ToString(Request.QueryString[1]).Trim();
                    HFDCandidateScheduleRound_ID.Value = Convert.ToString(Request.QueryString[2]).Trim();
                    checkInterviewerShortlistStatus_Submit();
                    getMainSkillsetView();
                    getInterviewAlldropDown();
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
                    RecruiterSendShortListingInterviewer();
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
        if (DivViewrowWiseCandidateInformation.Visible)
        {
            trvl_localbtn.Text = "+";
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
            DivViewrowWiseCandidateInformation.Visible = false;
            DivInterviewerShortListButton.Visible = false;
        }
        else
        {
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
            trvl_localbtn.Text = "-";
            DivViewrowWiseCandidateInformation.Visible = true;
            DivInterviewerShortListButton.Visible = true;


        }


    }

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {

    }
    protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string[] strdate;
            string confirmValue = hdnYesNo.Value.ToString();
            if (confirmValue != "Yes")
            {
                return;
            }
            DataSet dsInterviewSchedule = new DataSet();
            lblmessage.Text = "";
            if (DDLInterviewStatus.SelectedValue == "0" || DDLInterviewStatus.SelectedValue == "")
            {
                if (lstInterviewerOne.SelectedValue == "0")
                {
                    lblmessage.Text = "Please enter the Interviewer";
                    return;
                }
                if (TxtInterviewDate.Text == "")
                {
                    lblmessage.Text = "Please Select the Interview date";
                    return;
                }
                if (TxtInterviewTime.Text == "")
                {
                    lblmessage.Text = "Please Enter the Interview time";
                    return;
                }
                if (Convert.ToString(TxtInterviewTime.Text).Trim() != "")
                {
                    strdate = Convert.ToString(TxtInterviewTime.Text).Trim().Split(':');
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
            }
            
            if (lblmessage.Text == "")
            {
                DataSet dsRecruitmentDetailsValidation = new DataSet();
                SqlParameter[] sparsValidation = new SqlParameter[5];
                sparsValidation[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                sparsValidation[0].Value = "RecruitmentReq_RecruiterRescheduleEdit";
                sparsValidation[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                sparsValidation[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
                sparsValidation[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                sparsValidation[2].Value = Session["Empcode"].ToString();
                sparsValidation[3] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
                sparsValidation[3].Value = Convert.ToInt32(hdCandidate_ID.Value);
                sparsValidation[4] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.VarChar);
                sparsValidation[4].Value = Convert.ToInt32(HFDCandidateScheduleRound_ID.Value);
                dsRecruitmentDetailsValidation = spm.getDatasetList(sparsValidation, "SP_GetRecruitment_Interviewerfeedback");

                if (DDLInterviewStatus.SelectedValue == "0" || DDLInterviewStatus.SelectedValue == "")
                {
                    if (dsRecruitmentDetailsValidation.Tables[5].Rows.Count > 0)
                    {
                        string[] strdate1;
                        string strtoDate = "";
                        strdate1 = Convert.ToString(TxtInterviewDate.Text).Trim().Split('/');
                        strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
                        DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                        for (int i = 0; i < dsRecruitmentDetailsValidation.Tables[5].Rows.Count; i++)
                        {
                            if (Convert.ToDateTime(dsRecruitmentDetailsValidation.Tables[5].Rows[i]["InterviewDatecheck"]) <= Convert.ToDateTime(ddt))
                            {
                                //lblmessage.Text = "";
                            }
                            else
                            {
                                lblmessage.Text = "Please enter a date greater than the past Interview date.";
                                return;
                            }
                        }
                    }
                }
             }
            if (lblmessage.Text == "")
            {
                if (DDLInterviewStatus.SelectedValue == "0" || DDLInterviewStatus.SelectedValue == "")
                {
                    string[] strdate1;
                    string strtoDate = "";
                    strdate1 = Convert.ToString(TxtInterviewDate.Text).Trim().Split('/');
                    strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
                    DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }
                SqlParameter[] spars = new SqlParameter[12];
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "InterviewReScheduleInsert";
                spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
                spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
                spars[2].Value = Session["Empcode"].ToString();
                spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
                spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);

                if (DDLInterviewStatus.SelectedValue == "0" || DDLInterviewStatus.SelectedValue == "")
                {
                    string[] strdate1;
                    string strtoDate = "";
                    strdate1 = Convert.ToString(TxtInterviewDate.Text).Trim().Split('/');
                    strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
                    DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    spars[4] = new SqlParameter("@InterviewDate", SqlDbType.DateTime);
                    spars[4].Value = Convert.ToDateTime(ddt);
                }
                else
                {
                    spars[4] = new SqlParameter("@InterviewDate", SqlDbType.DateTime);
                    spars[4].Value = DateTime.Now.ToString();
                }
                spars[5] = new SqlParameter("@InterviewTime", SqlDbType.VarChar);
                spars[5].Value = TxtInterviewTime.Text;
                spars[6] = new SqlParameter("@InterviewRound_ID", SqlDbType.Int);
                spars[6].Value = DDLInterviewRound.SelectedValue;
                spars[7] = new SqlParameter("@Emp_Code_Inter1", SqlDbType.VarChar);
                spars[7].Value = lstInterviewerOne.SelectedValue;
                spars[8] = new SqlParameter("@InterviewShortListMain_ID", SqlDbType.Int);
                spars[8].Value = Convert.ToInt32(HFISLMID.Value);

                if (DDLInterviewStatus.SelectedValue == "0" || DDLInterviewStatus.SelectedValue == "")
                {
                    spars[9] = new SqlParameter("@InterviewStatus_ID", SqlDbType.Int);
                    spars[9].Value = 0;
                }
                else
                {
                    spars[9] = new SqlParameter("@InterviewStatus_ID", SqlDbType.Int);
                    spars[9].Value = DDLInterviewStatus.SelectedValue;
                }
                spars[10] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.Int);
                spars[10].Value = Convert.ToInt32(HFDCandidateScheduleRound_ID.Value);

                dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");
                string StrISCRD = dsInterviewSchedule.Tables[0].Rows[0]["ISCSRID"].ToString();

                string strRecruitername1 = dsInterviewSchedule.Tables[0].Rows[0]["Recruitername1"].ToString();

                string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_InterviewerFeedback"]).Trim() + "?ReqID=" + hdRecruitment_ReqID.Value + "&CanID=" + hdCandidate_ID.Value + "&CanSRID=" + StrISCRD;
				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();
				int DeptID = Convert.ToInt32(lstPositionDept.SelectedValue);
				string mailsubject = "", mailcontain = "";
                if (DDLInterviewStatus.SelectedValue == "0" || DDLInterviewStatus.SelectedValue == "")
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
                    mailsubject = "Recruitment - Interview scheduled for a Candidate against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
                    mailcontain = "Interview for the below candidate is scheduled. Please find the details below;";
                    DataTable dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(HFempcoderec.Value, DeptID,getcompSelectedval,qtype);
                    string mailrec = "";
                    for (int i = 0; i < dtApproverEmailIds.Rows.Count; i++)
                    {
                        if (dtApproverEmailIds.Rows[i]["APPR_ID"].ToString() == "103")
                        {
                            mailrec = dtApproverEmailIds.Rows[i]["Emp_Emailaddress"].ToString() + "," + dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress1"].ToString();
                        }
                        
                    }
                    if (DDLInterviewRound.SelectedValue == "1")
                    {
                        spm.send_mailto_ScheduleInterviewAndNxtroundShedule(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress1"].ToString(), mailsubject, lstInterviewerOne.SelectedItem.Text, txtName.Text, DDLInterviewRound.SelectedItem.Text, TxtInterviewDate.Text, TxtInterviewTime.Text, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, strLeaveRstURL, mailcontain);
                     }
                    else
                    {
                        spm.send_mailto_ScheduleInterviewAndNxtroundShedule(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, lstInterviewerOne.SelectedItem.Text, txtName.Text, DDLInterviewRound.SelectedItem.Text, TxtInterviewDate.Text, TxtInterviewTime.Text, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, strLeaveRstURL, mailcontain);
                    }
                }
                else
                {                  
                    mailsubject = "Recruitment - Candidate Backout against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
                    mailcontain = "This is to Inform you that, the Candidate i.e. " + txtName.Text + " has Backed Out during the Interview Scheduling. We'll start sourcing for the new candidates and schedule interviews after screening.";
                   // DataTable dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(HFempcoderec.Value);
                    string mailrec = "";
                    if (dsInterviewSchedule.Tables[3].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsInterviewSchedule.Tables[3].Rows.Count; i++)
                        {
                            if (dsInterviewSchedule.Tables[3].Rows[i]["Appr_id"].ToString() == "103")
                            {
                                mailrec += dsInterviewSchedule.Tables[3].Rows[i]["Emp_Emailaddress"].ToString() + ",";
                            }
                        }
                    }
                    if (mailrec.ToString().Trim() != "")
                    {
                        mailrec = mailrec.TrimEnd(',');
                        if (txtReqEmail.Text.Trim() != mailrec.ToString().Trim())
                        {
                            mailrec = txtReqEmail.Text + "," + mailrec.ToString().Trim();
                        }
                        else
                        {
                            mailrec = txtReqEmail.Text;
                        }
                    }
                   // spm.send_mailto_ScheduleInterviewAndNxtroundShedulebackout(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress1"].ToString(), mailsubject, strInterviewname, txtName.Text, "", "", "", strLeaveRstURL, mailcontain);
                    spm.send_mailto_ScheduleInterviewAndNxtroundShedulebackout(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress1"].ToString(), mailrec, mailsubject, strRecruitername1, txtName.Text, "", "", "", strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text);
					CheckReferral_Candidated(Convert.ToString(hdCandidate_ID.Value), "3");
				}
                DivViewrowWiseCandidateInformation.Visible = false;
                DivInterviewerShortListButton.Visible = false;
                Response.Redirect("~/procs/Requisition_Index.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw;
        }
    }

	private void CheckReferral_Candidated(string CandidatedID, string Selected)
	{
		DataTable dtReferral = new DataTable();
		try
		{
			int StatusID = 0; string Result = "",A="";
			if (Selected == "3")
			{
				StatusID = 9;
				Result = "Backed Out";
				A = "has";
			}
			
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
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
            DivInterviewerShortListButton.Visible = false;
        }
        else
        {
            trvl_localbtn.Text = "+";
            btnTra_Details.Text = "-";
            DivRecruitment.Visible = true;
            DivViewrowWiseCandidateInformation.Visible = false;
            DivInterviewerShortListButton.Visible = false;

        }
    }

    #region  ALL_Methods

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
            //TxtCurrentCTC_Total.Text = DS.Tables[1].Rows[0]["CurrentCTC"].ToString();
            //TxtExpCTC_Total.Text = DS.Tables[1].Rows[0]["ExpectedCTC"].ToString();
            TxtTotalExperienceYrs.Text = DS.Tables[1].Rows[0]["ExperienceYear"].ToString();
            TxtRelevantExpYrs.Text = DS.Tables[1].Rows[0]["RelevantExpYrs"].ToString();
            Txt_ScreenerComments.Text = DS.Tables[1].Rows[0]["Remarks"].ToString();

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

    public void RecruiterSendShortListingInterviewer()
    {
        dsRecEmpCodeInterviewer1 = spm.getRecruiterInterviewerCode1(hdRecruitment_ReqID.Value);
        Session["SendEmailInterviername"] = dsRecEmpCodeInterviewer1.Tables[0].Rows[0]["SendEmail"].ToString();

    }
    private void checkInterviewerShortlistStatus_Submit()
    {
        try
        {
            DataTable dtTrDetails = new DataTable();
            SqlParameter[] spars = new SqlParameter[7];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "check_RecruiterReshedule_Status";
            spars[1] = new SqlParameter("@req_id", SqlDbType.Decimal);
            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();
            spars[3] = new SqlParameter("@CandiadteID", SqlDbType.VarChar);
            spars[3].Value = hdCandidate_ID.Value;
            spars[4] = new SqlParameter("@CandidateRoundID", SqlDbType.VarChar);
            spars[4].Value = HFDCandidateScheduleRound_ID.Value;
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
    private void getMainSkillsetView()
    {
        dtmainSkillSet = spm.GetMainSkillset();
        DDLmainSkillSet.DataSource = dtmainSkillSet;
        DDLmainSkillSet.DataTextField = "ModuleDesc";
        DDLmainSkillSet.DataValueField = "ModuleId";
        DDLmainSkillSet.DataBind();
        DDLmainSkillSet.Items.Insert(0, new ListItem("Select SkillSet", ""));
    }
    private void getInterviewAlldropDown()
    {
        dsCVSource = spm.GetCVSource();


        DDLInterviewRound.DataSource = dsCVSource.Tables[2];
        DDLInterviewRound.DataTextField = "InterviewRound";
        DDLInterviewRound.DataValueField = "InterviewRound_ID";
        DDLInterviewRound.DataBind();
        DDLInterviewRound.Items.Insert(0, new ListItem("Select Interview Round", ""));

        DDLInterviewStatus.DataSource = dsCVSource.Tables[3];
        DDLInterviewStatus.DataTextField = "InterviewStatus";
        DDLInterviewStatus.DataValueField = "InterviewStatus_ID";
        DDLInterviewStatus.DataBind();
        DDLInterviewStatus.Items.Insert(0, new ListItem("Select Status", ""));

        string Result = "Conducted";
        DataRow[] dr2 = dsCVSource.Tables[3].Select("InterviewStatus='" + Result + "'");
        if (dr2.Length > 0)
        {
            string itemValue = "1";
            if (DDLInterviewStatus.Items.FindByValue(itemValue) != null)
            {
                string itemText = DDLInterviewStatus.Items.FindByValue(itemValue).Text;
                ListItem li = new ListItem();
                li.Text = itemText;
                li.Value = itemValue;
                DDLInterviewStatus.Items.Remove(li);
            }
        }
        string Result1 = "Not-Conducted";
        DataRow[] dr3 = dsCVSource.Tables[3].Select("InterviewStatus='" + Result1 + "'");
        if (dr3.Length > 0)
        {
            string itemValue = "2";
            if (DDLInterviewStatus.Items.FindByValue(itemValue) != null)
            {
                string itemText = DDLInterviewStatus.Items.FindByValue(itemValue).Text;
                ListItem li = new ListItem();
                li.Text = itemText;
                li.Value = itemValue;
                DDLInterviewStatus.Items.Remove(li);
            }
        }
        string Result2 = "Canceled";
        DataRow[] dr4 = dsCVSource.Tables[3].Select("InterviewStatus='" + Result2 + "'");
        if (dr4.Length > 0)
        {
            string itemValue = "4";
            if (DDLInterviewStatus.Items.FindByValue(itemValue) != null)
            {
                string itemText = DDLInterviewStatus.Items.FindByValue(itemValue).Text;
                ListItem li = new ListItem();
                li.Text = itemText;
                li.Value = itemValue;
                DDLInterviewStatus.Items.Remove(li);
            }
        }

        DDLInterviewFeedback.DataSource = dsCVSource.Tables[4];
        DDLInterviewFeedback.DataTextField = "InterviewFeedback";
        DDLInterviewFeedback.DataValueField = "InterviewFeedback_ID";
        DDLInterviewFeedback.DataBind();
        DDLInterviewFeedback.Items.Insert(0, new ListItem("Select Feedback", ""));

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
            //lstInterviewerOne.DataSource = dtInterviewer;
            //lstInterviewerOne.DataTextField = "EmployeeName";
            //lstInterviewerOne.DataValueField = "EmployeeCode";
            //lstInterviewerOne.DataBind();
            //lstInterviewerOne.Items.Insert(0, new ListItem("Select Interviewer", "0"));

            LstRecommPerson.DataSource = dtInterviewer;
            LstRecommPerson.DataTextField = "EmployeeName";
            LstRecommPerson.DataValueField = "EmployeeCode";
            LstRecommPerson.DataBind();
            LstRecommPerson.Items.Insert(0, new ListItem("Select Recommended Person", "0"));

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
            SqlParameter[] spars = new SqlParameter[5];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "RecruitmentReq_RecruiterRescheduleEdit";
            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[2].Value = Session["Empcode"].ToString();
            spars[3] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
            spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);
            spars[4] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.VarChar);
            spars[4].Value = Convert.ToInt32(HFDCandidateScheduleRound_ID.Value);
            dsRecruitmentDetails = spm.getDatasetList(spars, "SP_GetRecruitment_Interviewerfeedback");

            if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
            {
                HFempcoderec.Value = dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code"].ToString();
                txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
                txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();
                txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();

                txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
                txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
                txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
                lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
                GetInterviewerScreeningBy(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));

                lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
                lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
                lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
                txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
             //   lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();

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
                LstRecommPerson.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
                txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();
                lstInterviewerOneView.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
                txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();

                //  GetFilterGD();

                hdCandidate_ID.Value = dsRecruitmentDetails.Tables[1].Rows[0]["Candidate_ID"].ToString();
                HFISLMID.Value = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewShortListMain_ID"].ToString();
                txtName.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateName"].ToString();
                txtEmail.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateEmail"].ToString();
                Txt_CandidateMobile.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateMobile"].ToString();
                lstCandidategender.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateGender"].ToString();
                lstMaritalStatus.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["Maritalstatus"].ToString();
              //  Txt_CandidateCurrentLocation.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateCurrentLocation"].ToString();
                Txt_CandidateBirthday.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateBirthday"].ToString();
                Txt_CandidatePAN.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidatePAN"].ToString();
                TxtAadharNo.Text = dsRecruitmentDetails.Tables[1].Rows[0]["AdharNo"].ToString();
                //Txt_CandidateExperence.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateExperience_Years"].ToString();
                //Txt_CandidateCurrentCTC.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateCurrentCTC"].ToString();
                //Txt_CandidateExpectedCTC.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateExpectedCTC"].ToString();
                lnkuplodedfileResume.Text = dsRecruitmentDetails.Tables[1].Rows[0]["UploadResume"].ToString();
                hdfilename.Value = dsRecruitmentDetails.Tables[1].Rows[0]["UploadResume"].ToString();
                filename = dsRecruitmentDetails.Tables[1].Rows[0]["UploadResume"].ToString();

               // lnkbtnIRSheet.Text = dsRecruitmentDetails.Tables[1].Rows[0]["IRSheet"].ToString();
               // hdfilenameIRSheet.Value = dsRecruitmentDetails.Tables[1].Rows[0]["IRSheet"].ToString();

                lnkuplodedfileResume.Visible = true;
                DDLmainSkillSet.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["ModuleId"].ToString();
                Txt_AdditionalSkillset.Text = dsRecruitmentDetails.Tables[1].Rows[0]["AdditionalSkillset"].ToString();
              //  Txt_EducationQualifacation.Text = dsRecruitmentDetails.Tables[1].Rows[0]["EducationQualification"].ToString();
               // Txt_Certifications.Text = dsRecruitmentDetails.Tables[1].Rows[0]["Certifications"].ToString();
                TxtInterviewDate.Text = "";
                TxtInterviewTime.Text = "";
                DDLInterviewStatus.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewStatus_ID"].ToString();
                if (DDLInterviewStatus.SelectedValue == "2" || DDLInterviewStatus.SelectedValue == "4")
                {
                    DDLInterviewRound.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewRound_IDNOtC"].ToString();
                }
                else
                {
                    DDLInterviewRound.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewRound_ID"].ToString();
                }

               
                PopulateCadidateRecruitmentWiseData();

                DDLInterviewFeedback.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewFeedback_ID"].ToString();
                TxtInterviewerComments.Text = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewerComments"].ToString();
                HFDCandidateScheduleRound_ID.Value = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateScheduleRound_ID"].ToString();
                if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
                {
                    gvotherfile.DataSource = dsRecruitmentDetails.Tables[2];
                    gvotherfile.DataBind();
                }
                else
                {
                    gvotherfile.DataSource = null;
                    gvotherfile.DataBind();
                }
                if (dsRecruitmentDetails.Tables[4].Rows.Count > 0)
                {
                    GVUploadOtherFilesIRSheet.DataSource = dsRecruitmentDetails.Tables[4];
                    GVUploadOtherFilesIRSheet.DataBind();
                }
                else
                {
                    GVUploadOtherFilesIRSheet.DataSource = null;
                    GVUploadOtherFilesIRSheet.DataBind();
                }
                if (dsRecruitmentDetails.Tables[6].Rows.Count > 0)
                {
                    SpanEducationDetails.Visible = true;
                    SpanEducationDetails1.Visible = true;
                    SpanEducationDetails2.Visible = true;
                    GVEducationDetails.DataSource = dsRecruitmentDetails.Tables[6];
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
                if (dsRecruitmentDetails.Tables[7].Rows.Count > 0)
                {
                    SpanWorkExperiencedetail.Visible = true;
                    SpanWorkExperiencedetail1.Visible = true;
                    SpanWorkExperiencedetail2.Visible = true;
                    GVWorkExperiencedetail.DataSource = dsRecruitmentDetails.Tables[7];
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

                if (dsRecruitmentDetails.Tables[5].Rows.Count > 0)
                {
                    gvCandidateinterviewroundInformation.DataSource = dsRecruitmentDetails.Tables[5];
                    gvCandidateinterviewroundInformation.DataBind();
                    for (int i = 0; i < dsRecruitmentDetails.Tables[5].Rows.Count; i++)
                    {
                        if(dsRecruitmentDetails.Tables[5].Rows.Count-1 == i)
                        {
                            string Str = dsRecruitmentDetails.Tables[5].Rows[i]["InterviewStatus_ID"].ToString();
                            if (Str == "2" || Str == "4")
                            {
                                DDLInterviewRound.SelectedValue = dsRecruitmentDetails.Tables[5].Rows[i]["InterviewRound_ID"].ToString();
                            }
                            else
                            {
                                int i1 = Convert.ToInt32(dsRecruitmentDetails.Tables[5].Rows[i]["InterviewRound_ID"]) + 1;
                                DDLInterviewRound.SelectedValue = Convert.ToString(i1);
                            }
                        }
                    }
                    }
                else
                {
                    gvCandidateinterviewroundInformation.DataSource = null;
                    gvCandidateinterviewroundInformation.DataBind();
                }
                GetInterviewerDependingOnSkillSetANDInterviewRound();

                if (Txt_CandidateBirthday.Text !="")
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

                if (dsRecruitmentDetails.Tables[8].Rows.Count > 0)
                {
                    trvldeatils_btnSave.Visible = false;
                }
                if (dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"].ToString().Trim() == "Cancelled")
                {
                    trvldeatils_btnSave.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    #endregion

    protected void DDLInterviewRound_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInterviewerDependingOnSkillSetANDInterviewRound();
    }

    protected void DDLInterviewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtInterviewDate.Text = "";
        TxtInterviewTime.Text = "";
        lstInterviewerOne.SelectedValue = "0";
    }
	#region IR Sheet Export Excel 25-02-22
	protected void lnkIRsheetExport_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		//hdCandidate_ID.Value = Convert.ToString(gvCandidateinterviewroundInformation.DataKeys[row.RowIndex].Values[0]).Trim();
		getIrSheetExport();

	}
	private void getIrSheetExport()
	{
		try
		{
			DataTable dtMerged = new DataTable();
			dtIrSheetReport = new DataSet();
			dtIrSheetReport = spm.Get_Rec_Recruit_IrSheetDetails("GetIrSheetSummary", Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
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
	#endregion
}