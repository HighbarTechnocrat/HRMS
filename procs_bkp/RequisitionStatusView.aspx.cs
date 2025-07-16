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
public partial class procs_RequisitionStatusView : System.Web.UI.Page
{
    public SqlDataAdapter sqladp;
    public static string dob1;
    public string userid;
    public int did = 0;
    SP_Methods spm = new SP_Methods();
    DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource, dtIrSheetReport;
    public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet, dtInterviewer1, dtofferApproval, dtOfferApproverEmailIds, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
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
                    hdnLoginUserName.Value = Convert.ToString(Session["emp_loginName"]).Trim();
                    hdnLoginEmpEmail.Value = Convert.ToString(Session["LoginEmpmail"]).Trim();

                    hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());
                    hdfilefathIRSheet.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheet"]).Trim());
                    HFQuestionnaire.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
                    HDCandidateResignationSubAcceptanceDoc.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateJoiningtimefile"]).Trim());
                    OfferApprovalOther.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["OfferApprovalDocumentpath"]).Trim());
					hdnInterviewphtoPath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_InterviewerPhoto"]).Trim());

					//   Txt_JoiningDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					//  txtOfferDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
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
                    GetCandidateInfoRecruitmentwisedataBind();
                    hdRecruitment_ReqID.Value = Request.QueryString["Recruitment_ReqID"];
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

    protected void btnTra_Details_Click(object sender, EventArgs e)
    {
        if (DivRecruitment.Visible)
        {

            DivViewrowWiseCandidateInformation.Visible = false;
            btnTra_Details.Text = "+";
            DivRecruitment.Visible = false;
        }
        else
        {
            btnTra_Details.Text = "-";
            DivRecruitment.Visible = true;
        }
        // trvldeatils_btnSave.Visible = false;
    }

    protected void lnkEditView_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        hdCandidate_ID.Value = Convert.ToString(GVShortListInterviewer.DataKeys[row.RowIndex].Values[0]).Trim();

        HiddenField hf = (HiddenField)row.FindControl("HFInterFeedBackvalue");
        HiddenField hfOnNotice = (HiddenField)row.FindControl("HFCurrentlyOnNotice");
        HiddenField hfNPeriod = (HiddenField)row.FindControl("HFNoticePeriod");

        HiddenField hfCurCtc = (HiddenField)row.FindControl("HFCurrentCTC");
        HiddenField hfExptCTC = (HiddenField)row.FindControl("HFExpectedCTC");
        HiddenField hfExpYear = (HiddenField)row.FindControl("HFExperienceYear");
        HiddenField HFScreenerRemark = (HiddenField)row.FindControl("HFScreenerRemarks");

        string str = hf.Value.ToString().Trim();
        Txt_ScreenerComments.Text = HFScreenerRemark.Value.ToString().Trim();
        PopulateCandidateData();
        DivViewrowWiseCandidateInformation.Visible = true;
        //  trvldeatils_btnSave.Focus();
        DivCandidateRoundHistory.Visible = true;
        DivButton.Visible = true;
        if (str == "Finalized")
        {
            DivJoiningDetailInformation.Visible = true;
            DivJoiningDetails1.Visible = true;
            if (HDInterviewerSchedularEmpCode.Value == Convert.ToString(Session["Empcode"]).Trim())
            {
                //DivJoiningDetails1.Visible = true;
                //DivJoiningDetails2.Visible = true;

                //LIRecruiterComment.Visible = true;
                //txtRecruitercomment.Visible = true;
                //JobDetail_btnSave.Visible = false;
                //  trvldeatils_btnSave.Visible = false;
            }
            else
            {
                //JobDetail_btnSave.Visible = true;
                //DivJoiningDetails1.Visible = true;
                //DivJoiningDetails2.Visible = true;

                //LIRecruiterComment.Visible = true;
                //txtRecruitercomment.Visible = true;
                //Rec_AddjoinDetailbtn.Text = "-";
                //txtRecruitercomment.Text = "";
                //Txt_JoiningDate.Text = "";
                //DDLStatusUpdate.SelectedValue = "";
                //trvldeatils_btnSave.Visible = true;
            }

        }
        else
        {
            //DivJoiningDetails1.Visible = false;
            //DivJoiningDetails2.Visible = false;
            //LIRecruiterComment.Visible = false;
            //txtRecruitercomment.Visible = false;
        }

		//DgvOfferApprover.DataSource = null;
		//DgvOfferApprover.DataBind();
		//GetOffer_Approver_History_list();
		GRDOfferHistory.DataSource = null;
		GRDOfferHistory.DataBind();
		OfferhistoryS.Visible = false;
		OfferCreate.Visible = false;
		spoffer.Visible = false;
		OfferCreatelist();

	}

    protected void mobile_btnBack_Click(object sender, EventArgs e)
    {
        DivViewrowWiseCandidateInformation.Visible = false;
        DivCandidateRoundHistory.Visible = false;
        DivButton.Visible = false;
        //DivJoiningDetails1.Visible = false;
        //DivJoiningDetails2.Visible = false;
        DivJoiningDetailInformation.Visible = false;
        DivJoiningDetails1.Visible = false;
        OfferhistoryS.Visible = false;
		OfferCreate.Visible = false;
		spoffer.Visible = false;
		GRDOfferHistory.DataSource = null;
        GRDOfferHistory.DataBind();
		GRDOfferCreatelist.DataSource = null;
		GRDOfferCreatelist.DataBind();
	}

    private void GetOffer_Approver_History_list()
    {
        int RecrutID = 0, Candidate_ID = 0;
        DataTable dtapproverHistory = new DataTable();
        RecrutID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
        Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
        dtapproverHistory = spm.GetRequisition_Offer_Approver_History_Status(Candidate_ID, RecrutID);
        if (dtapproverHistory.Rows.Count > 0)
        {
            GRDOfferHistory.DataSource = dtapproverHistory;
            GRDOfferHistory.DataBind();
            OfferhistoryS.Visible = true;
        }
        else
        {
            GRDOfferHistory.DataSource = null;
            GRDOfferHistory.DataBind();
            OfferhistoryS.Visible = false;
        }
    }

    //protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string confirmValue = hdnYesNo.Value.ToString();
    //        if (confirmValue != "Yes")
    //        {
    //            return;
    //        }
    //        if (DDLJoinedemployee.SelectedValue == "0")
    //        {
    //            lblmessage.Text = "Please enter joined employee";
    //            return;
    //        }

    //        DataSet dsInterviewSchedule = new DataSet();
    //        lblmessage.Text = "";
    //        if (lblmessage.Text == "")
    //        {
    //            SqlParameter[] spars = new SqlParameter[9];
    //            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
    //            spars[0].Value = "InterviewFinalupdateClosedfromRecruiter";
    //            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
    //            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
    //            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
    //            spars[2].Value = Session["Empcode"].ToString();
    //            spars[3] = new SqlParameter("@empcodenew", SqlDbType.VarChar);
    //            spars[3].Value = DDLJoinedemployee.SelectedValue;

    //            dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");


    //            string mailrec = "";
    //            if (dsInterviewSchedule.Tables[4].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < dsInterviewSchedule.Tables[4].Rows.Count; i++)
    //                {
    //                    mailrec += dsInterviewSchedule.Tables[4].Rows[i]["Emp_Emailaddress"].ToString() + ",";
    //                }
    //            }
    //            if (mailrec.ToString().Trim() != "")
    //            {
    //                mailrec = mailrec.TrimEnd(',');
    //                if (txtReqEmail.Text.Trim() != mailrec.ToString().Trim())
    //                {
    //                    mailrec = txtReqEmail.Text + "," + mailrec.ToString().Trim();
    //                }
    //                else
    //                {
    //                    mailrec = txtReqEmail.Text;
    //                }
    //            }
    //            else
    //            {
    //                mailrec = txtReqEmail.Text.Trim();
    //            }

    //            string mailsubject = "";
    //            string mailcontain1 = "";
    //            mailsubject = "Recruitment - Candidate  " + DDLJoinedemployee.SelectedItem.Text + " joined for requisition  " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //            mailcontain1 = "This is to inform you that the requisition " + txtReqNumber.Text + " is closed as the following candidate has joined against this requisition;";

    //            string strtomail = mailrec;
    //            string strrecmail = dsInterviewSchedule.Tables[1].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString();
    //            string strempnameddl = DDLJoinedemployee.SelectedItem.Text;
    //            string strempcodedll = DDLJoinedemployee.SelectedValue;
    //            string strDepartment = dsInterviewSchedule.Tables[0].Rows[0]["Department_Name"].ToString();
    //            string strLocation = dsInterviewSchedule.Tables[0].Rows[0]["Location_name"].ToString();
    //            string strDesignation = dsInterviewSchedule.Tables[0].Rows[0]["DesginationName"].ToString();
    //            string strBand = dsInterviewSchedule.Tables[0].Rows[0]["BAND"].ToString();

    //            spm.send_mailto_closerequisition_Mail(strrecmail, strtomail, mailsubject, mailcontain1, DDLJoinedemployee.SelectedItem.Text, DDLJoinedemployee.SelectedValue, strDepartment, strLocation, strDesignation, strBand);

    //            if (dsInterviewSchedule.Tables[3].Rows[0]["ResultCount"].ToString() == "1")
    //            {
    //                Response.Redirect("~/procs/Requisition_Index.aspx");
    //            }
    //            else
    //            {
    //                lblmessage.Text = "Please enter the joining information otherwise candidate not Finalized";
    //            }
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //        //throw;
    //    }
    //}

    //protected void Linkbtn_CandidateShortlisting_Click(object sender, EventArgs e)
    //{
    //    if (HDInterviewerSchedularEmpCode.Value == Convert.ToString(Session["Empcode"]).Trim())
    //    {

    //    }
    //    else
    //    {
    //        Response.Redirect("~/procs/RecruiterSendShortListing.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value + "&Flag=0");
    //    }
    //}

    //protected void Rec_AddjoinDetailbtn_Click(object sender, EventArgs e)
    //{
    //    if (Rec_AddjoinDetailbtn.Text == "-")
    //    {
    //        Rec_AddjoinDetailbtn.Text = "-";
    //        DivJoiningDetails2.Visible = true;
    //        DivJoiningDetailInformation.Visible = true;
    //    }
    //    else
    //    {
    //        Rec_AddjoinDetailbtn.Text = "+";
    //        DivJoiningDetails2.Visible = false;
    //        DivJoiningDetailInformation.Visible = false;
    //    }
    //}

    //protected void JobDetail_btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string confirmValue = hdnYesNo.Value.ToString();
    //        if (confirmValue != "Yes")
    //        {
    //            return;
    //        }
    //        DataSet dsInterviewSchedule = new DataSet();
    //        lblmessageJoining.Text = "";
    //        if (DDLStatusUpdate.SelectedValue == "")
    //        {
    //            DivMessageJoining.Visible = true;
    //            lblmessageJoining.Text = "Please Select the Status Update";
    //            return;
    //        }
    //        if (Txt_JoiningDate.Text == "")
    //        {
    //            DivMessageJoining.Visible = true;
    //            lblmessageJoining.Text = "Please Select the date";
    //            return;
    //        }

    //        if (DDLStatusUpdate.SelectedValue == "4" || DDLStatusUpdate.SelectedValue == "9" || DDLStatusUpdate.SelectedValue == "11")
    //        {
    //            if (txtRecruitercomment.Text == "")
    //            {
    //                DivMessageJoining.Visible = true;
    //                lblmessageJoining.Text = "Please enter  the recruiter comment";
    //                return;
    //            }
    //        }
    //        if (DDLStatusUpdate.SelectedValue == "12")
    //        {
    //            if (Convert.ToString(FileUpload1.FileName).Trim() == "")
    //            {
    //                DivMessageJoining.Visible = true;
    //                lblmessageJoining.Text = "Please upload file";
    //                return;
    //            }
    //        }
    //        DataSet ds = new DataSet();
    //        SqlParameter[] spars1 = new SqlParameter[5];
    //        spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
    //        spars1[0].Value = "RecruitmentReq_InterviewerFeedBackEdit";
    //        spars1[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
    //        spars1[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
    //        spars1[2] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
    //        spars1[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
    //        string[] strdatech;
    //        string strtoDateck = "";
    //        strdatech = Convert.ToString(Txt_JoiningDate.Text).Trim().Split('/');
    //        strtoDateck = Convert.ToString(strdatech[2]) + "-" + Convert.ToString(strdatech[1]) + "-" + Convert.ToString(strdatech[0]);
    //        DateTime ddtch = DateTime.ParseExact(strtoDateck, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    //        //spars1[3] = new SqlParameter("@JoiningDate", SqlDbType.DateTime);
    //        // spars1[3].Value = Convert.ToDateTime(ddtch);

    //        ds = spm.getDatasetList(spars1, "SP_GetRecruitment_Interviewerfeedback");

    //        if (ds.Tables[3].Rows.Count > 0)
    //        {
    //            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
    //            {
    //                if (Convert.ToDateTime(ds.Tables[3].Rows[i]["checkInterviewDate"]) <= Convert.ToDateTime(ddtch))
    //                {
    //                    //lblmessage.Text = "";
    //                }
    //                else
    //                {
    //                    DivMessageJoining.Visible = true;
    //                    lblmessageJoining.Text = "Please enter greater than or equal date as per Interview Date";
    //                    return;
    //                }
    //            }

    //        }

    //        if (lblmessageJoining.Text == "")
    //        {
    //            if (ds.Tables[10].Rows.Count > 0)
    //            {
    //                if (ds.Tables[10].Rows[0]["MaxJoiningDate"].ToString() == "")
    //                {
    //                    DivMessageJoining.Visible = false;
    //                    lblmessageJoining.Text = "";
    //                }
    //                else
    //                {
    //                    if (Convert.ToDateTime(ds.Tables[10].Rows[0]["MaxJoiningDate"]) <= Convert.ToDateTime(ddtch))
    //                    {
    //                        DivMessageJoining.Visible = false;
    //                        lblmessageJoining.Text = "";
    //                    }
    //                    else
    //                    {
    //                        Txt_JoiningDate.Text = "";
    //                        DivMessageJoining.Visible = true;
    //                        lblmessageJoining.Text = "Please enter greater than or equal date as per status update";
    //                        return;
    //                    }
    //                }
    //            }
    //        }


    //        if (lblmessageJoining.Text == "")
    //        {
    //            SqlParameter[] spars = new SqlParameter[9];
    //            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
    //            spars[0].Value = "InterviewFinalupdatefromRecruiter";
    //            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
    //            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
    //            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
    //            spars[2].Value = Session["Empcode"].ToString();
    //            spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
    //            spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);

    //            if (Txt_JoiningDate.Text != "")
    //            {
    //                string[] strdate1;
    //                string strtoDate = "";
    //                strdate1 = Convert.ToString(Txt_JoiningDate.Text).Trim().Split('/');
    //                strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
    //                DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    //                spars[4] = new SqlParameter("@JoiningDate", SqlDbType.DateTime);
    //                spars[4].Value = Convert.ToDateTime(ddt);
    //            }
    //            else
    //            {
    //                spars[4] = new SqlParameter("@JoiningDate", SqlDbType.VarChar);
    //                spars[4].Value = "";
    //            }
    //            spars[5] = new SqlParameter("@StatusUpdate", SqlDbType.Int);
    //            spars[5].Value = Convert.ToInt32(DDLStatusUpdate.SelectedValue);
    //            spars[6] = new SqlParameter("@RecruiterComments", SqlDbType.VarChar);
    //            spars[6].Value = txtRecruitercomment.Text;
    //            if (Convert.ToString(FileUpload1.FileName).Trim() != "")
    //            {
    //                string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
    //                filename = FileUpload1.FileName;
    //                string strfileName = "";
    //                string strremoveSpace = txtName.Text + "_" + str + "_" + filename;
    //                strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
    //                strfileName = strremoveSpace; //+ Path.GetExtension(FileUpload1.FileName);
    //                filename = strfileName;
    //                FileUpload1.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateJoiningtimefile"]).Trim()), strfileName));

    //                spars[7] = new SqlParameter("@AcceptanceFile", SqlDbType.VarChar);
    //                spars[7].Value = filename;
    //            }
    //            dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");

    //            string mailrec = "";
    //            if (dsInterviewSchedule.Tables[3].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < dsInterviewSchedule.Tables[3].Rows.Count; i++)
    //                {
    //                    mailrec += dsInterviewSchedule.Tables[3].Rows[i]["Emp_Emailaddress"].ToString() + ",";
    //                }
    //            }
    //            if (mailrec.ToString().Trim() != "")
    //            {
    //                mailrec = mailrec.TrimEnd(',');
    //                if (txtReqEmail.Text.Trim() != mailrec.ToString().Trim())
    //                {
    //                    mailrec = txtReqEmail.Text + "," + mailrec.ToString().Trim();
    //                }
    //                else
    //                {
    //                    mailrec = txtReqEmail.Text;
    //                }
    //            }
    //            else
    //            {
    //                mailrec = txtReqEmail.Text.Trim();
    //            }
    //            string mailsubject = "";
    //            string mailcontain = "";
    //            if (DDLStatusUpdate.SelectedValue == "1")
    //            {
    //                mailsubject = "Recruitment - finalized candidate under negotiation for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "2")
    //            {
    //                mailsubject = "Recruitment - finalized candidate negotiation done for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "3")
    //            {
    //                mailsubject = "Recruitment - finalized candidate offer accepted for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "4")
    //            {
    //                mailsubject = "Recruitment - finalized candidate offer rejected for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "5")
    //            {
    //                mailsubject = "Recruitment - finalized candidate offer released for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "6")
    //            {
    //                mailsubject = "Recruitment - finalized candidate joining on time for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "7")
    //            {
    //                mailsubject = "Recruitment - finalized candidate joining extended for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "8")
    //            {
    //                mailsubject = "Recruitment - finalized candidate joining for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "9")
    //            {
    //                mailsubject = "Recruitment - finalized candidate not joining for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }

    //            if (DDLStatusUpdate.SelectedValue == "10")
    //            {
    //                mailsubject = "Recruitment - finalized candidate Offer Approved for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "11")
    //            {
    //                mailsubject = "Recruitment - finalized candidate Backout for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }
    //            if (DDLStatusUpdate.SelectedValue == "12")
    //            {
    //                mailsubject = "Recruitment - finalized candidate Resignation Submission /Acceptance for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
    //                mailcontain = "Please find below is the status update for finalized candidate.";
    //            }


    //            spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain);

    //            if (dsInterviewSchedule.Tables[1].Rows.Count > 0)
    //            {
    //                dsCVSource = spm.GetCVSource();
    //                DataTable dt = new DataTable();
    //                dt = dsCVSource.Tables[7];
    //                for (int i = dt.Rows.Count - 1; i >= 0; i--)
    //                {

    //                    DataRow dr = dt.Rows[i];
    //                    if (DDLStatusUpdate.SelectedValue == "1")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "2")
    //                        {
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
    //                            {
    //                                dr.Delete();
    //                            }
    //                        }
    //                    }
    //                    if (DDLStatusUpdate.SelectedValue == "2")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "10")
    //                        {
    //                            //dr.Delete();
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
    //                            {
    //                                dr.Delete();
    //                            }
    //                        }
    //                    }

    //                    if (DDLStatusUpdate.SelectedValue == "10")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
    //                        {
    //                            //dr.Delete();
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
    //                            {
    //                                dr.Delete();
    //                            }
    //                        }
    //                    }
    //                    //if (DDLStatusUpdate.SelectedValue == "2")
    //                    //{
    //                    //    if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
    //                    //    {
    //                    //        dr.Delete();
    //                    //    } 
    //                    // }
    //                    if (DDLStatusUpdate.SelectedValue == "5")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "3")
    //                        {
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "4")
    //                            {
    //                                if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
    //                                {
    //                                    dr.Delete();
    //                                }
    //                                // dr.Delete();
    //                            }
    //                        }
    //                    }
    //                    //if (DDLStatusUpdate.SelectedValue == "3")
    //                    //{
    //                    //    if (dr["StatusUpdate_ID"].ToString().Trim() != "8")
    //                    //    {
    //                    //        if (dr["StatusUpdate_ID"].ToString().Trim() != "9")
    //                    //        {
    //                    //            dr.Delete();
    //                    //        }
    //                    //    }
    //                    //}

    //                    if (DDLStatusUpdate.SelectedValue == "3")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "12")
    //                        {
    //                            dr.Delete();
    //                        }
    //                    }
    //                    if (DDLStatusUpdate.SelectedValue == "12")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "8")
    //                        {
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "9")
    //                            {
    //                                dr.Delete();
    //                            }
    //                        }
    //                    }



    //                    if (DDLStatusUpdate.SelectedValue == "4")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
    //                        {
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
    //                            {
    //                                dr.Delete();
    //                            }
    //                        }
    //                    }
    //                    if (DDLStatusUpdate.SelectedValue == "8" || DDLStatusUpdate.SelectedValue == "6" || DDLStatusUpdate.SelectedValue == "7")
    //                    {
    //                        if (dr["StatusUpdate_ID"].ToString().Trim() != "6")
    //                        {
    //                            if (dr["StatusUpdate_ID"].ToString().Trim() != "7")
    //                            {
    //                                if (dr["StatusUpdate_ID"].ToString().Trim() != "9")
    //                                {
    //                                    dr.Delete();
    //                                }
    //                            }
    //                        }
    //                    }
    //                    if (DDLStatusUpdate.SelectedValue == "9")
    //                    {
    //                        dr.Delete();
    //                    }
    //                    if (DDLStatusUpdate.SelectedValue == "11")
    //                    {
    //                        dr.Delete();
    //                    }
    //                }
    //                dt.AcceptChanges();
    //                DDLStatusUpdate.DataSource = dt;
    //                DDLStatusUpdate.DataTextField = "StatusUpdate";
    //                DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
    //                DDLStatusUpdate.DataBind();
    //                DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
    //                string Result = "Negotiation Done";
    //                //Negotiation Done		
    //                if (dsInterviewSchedule.Tables[1].Rows.Count == 2)
    //                {
    //                    DataRow[] dr1 = dsInterviewSchedule.Tables[1].Select("StatusUpdate='" + Result + "'");
    //                    if (dr1.Length > 0)
    //                    {
    //                        string itemValue = "10";   /* 3 for BodyRepair */
    //                        if (DDLStatusUpdate.Items.FindByValue(itemValue) != null)
    //                        {
    //                            string itemText = DDLStatusUpdate.Items.FindByValue(itemValue).Text;
    //                            ListItem li = new ListItem();
    //                            li.Text = itemText;
    //                            li.Value = itemValue;
    //                            DDLStatusUpdate.Items.Remove(li);
    //                            trvl_accmo_btn.Visible = true;
    //                            //DDLStatusUpdate.Enabled = false;
    //                            //hdnFinalizedDate.Value = dr[0]["EnterviewDate"].ToString();
    //                        }
    //                    }
    //                }
    //                string OfferReject = "Offer Rejected";
    //                DataRow[] dr3 = dsInterviewSchedule.Tables[1].Select("StatusUpdate='" + OfferReject + "'");
    //                if (dr3.Length > 0)
    //                {

    //                    //DDLStatusUpdate.Enabled = false;
    //                    string itemValue = "5";
    //                    if (DDLStatusUpdate.Items.FindByValue(itemValue) != null)
    //                    {
    //                        string itemText = DDLStatusUpdate.Items.FindByValue(itemValue).Text;
    //                        ListItem li = new ListItem();
    //                        li.Text = itemText;
    //                        li.Value = itemValue;
    //                        DDLStatusUpdate.Items.Remove(li);
    //                        trvl_accmo_btn.Visible = true;
    //                    }
    //                }

    //                GVJoiningDetailInformation.DataSource = dsInterviewSchedule.Tables[1];
    //                GVJoiningDetailInformation.DataBind();
    //                GetecruitmentDetail();
    //            }
    //           // trvldeatils_btnSave.Visible = false;
    //            Rec_AddjoinDetailbtn.Text = "-";
    //            DivJoiningDetails2.Visible = true;
    //            DivJoiningDetailInformation.Visible = true;
    //            txtRecruitercomment.Text = "";
    //            Txt_JoiningDate.Text = "";
    //            DDLStatusUpdate.SelectedValue = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //        //throw;
    //    }
    //}

    //protected void CheckClosedRequisition_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (CheckClosedRequisition.Checked == true)
    //    {
    //        DivViewrowWiseCandidateInformation.Visible = false;
    //        DivCandidateRoundHistory.Visible = false;
    //        DivJoiningDetails1.Visible = false;
    //        DivJoiningDetails2.Visible = false;
    //        DivJoiningDetailInformation.Visible = false;
    //        trvldeatils_btnSave.Visible = true;
    //        DivButton.Visible = true;


    //    }
    //    else
    //    {
    //        trvldeatils_btnSave.Visible = false;
    //        DivButton.Visible = false;
    //    }
    //}

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

    #region  All_Methods

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

            //DDLJoinedemployee.DataSource = dtInterviewer;
            //DDLJoinedemployee.DataTextField = "EmployeeName";
            //DDLJoinedemployee.DataValueField = "EmployeeCode";
            //DDLJoinedemployee.DataBind();
            //DDLJoinedemployee.Items.Insert(0, new ListItem("Select Joined Employee", "0"));

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
                lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();
                Txt_lstCVSource.Text = lstCVSource.SelectedItem.Text;
                lnkuplodedfileResume.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                hdfilename.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                filename = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
                lnkuplodedfileResume.Visible = true;
                PopulateCadidateRecruitmentWiseData();
                if (lstCVSource.SelectedValue == "3")
                {
                    Txt_ReferredbyEmpcode.Visible = true;
                    Txt_ReferredBy.Visible = false;
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
                TxtAadharNo.Text = dsCandidateData.Tables[0].Rows[0]["AdharNo"].ToString();

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

                if (dsCandidateData.Tables[1].Rows.Count > 0)
                {
                    gvotherfile.DataSource = dsCandidateData.Tables[1];
                    gvotherfile.DataBind();
                    DivViewotherFiles1.Visible = true;
                    DivViewotherFiles2.Visible = true;
                }
                else
                {
                    gvotherfile.DataSource = null;
                    gvotherfile.DataBind();
                    DivViewotherFiles1.Visible = false;
                    DivViewotherFiles2.Visible = false;
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

                DataSet dsCandidateRoundInfo = new DataSet();

                SqlParameter[] spars = new SqlParameter[3];
                spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
                spars[0].Value = "RecruitmentReq_InterviewerFeedBackEdit";
                spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
                spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
                spars[2] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
                spars[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
                dsCandidateRoundInfo = spm.getDatasetList(spars, "SP_GetRecruitment_Interviewerfeedback");

                if (dsCandidateRoundInfo.Tables[3].Rows.Count > 0)
                {
                    string Result = "Finalized";
                    DataRow[] dr = dsCandidateRoundInfo.Tables[3].Select("InterviewFeedback='" + Result + "'");
                    if (dr.Length > 0)
                    {
                        hdnFinalizedDate.Value = dr[0]["EnterviewDate"].ToString();
                    }
                    GVCandidateRoundHistory.DataSource = dsCandidateRoundInfo.Tables[3];
                    GVCandidateRoundHistory.DataBind();

                }
                else
                {
                    GVCandidateRoundHistory.DataSource = null;
                    GVCandidateRoundHistory.DataBind();
                    //trvldeatils_btnSave.Enabled = false;

                }
                if (dsCandidateRoundInfo.Tables[4].Rows.Count > 0)
                {
                    GVUploadOtherFilesIRSheet.DataSource = dsCandidateRoundInfo.Tables[4];
                    GVUploadOtherFilesIRSheet.DataBind();
                    DivViewotherIRSheetFile1.Visible = true;
                    DivViewotherIRSheetFile2.Visible = true;
                    DivViewotherIRSheetFile3.Visible = true;
                }
                else
                {
                    GVUploadOtherFilesIRSheet.DataSource = null;
                    GVUploadOtherFilesIRSheet.DataBind();
                    DivViewotherIRSheetFile1.Visible = false;
                    DivViewotherIRSheetFile2.Visible = false;
                    DivViewotherIRSheetFile3.Visible = false;
                }
                if (dsCandidateRoundInfo.Tables[5].Rows.Count > 0)
                {
                    GVJoiningDetailInformation.DataSource = dsCandidateRoundInfo.Tables[5];
                    GVJoiningDetailInformation.DataBind();
                }
                else
                {
                    GVJoiningDetailInformation.DataSource = null;
                    GVJoiningDetailInformation.DataBind();
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
    //private void OfferApprovalStatus()
    //{
    //    DataTable Offer = new DataTable();
    //    int RecrutID = 0, Candidate_ID = 0;
    //    string Result = "", values = "";
    //    RecrutID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
    //    Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
    //    Offer = spm.GetOffer_Approval_Status(RecrutID, Candidate_ID);
    //    if (Offer.Rows.Count > 0)
    //    {
    //        Result = (string)Offer.Rows[0]["Action"];
    //        values = Offer.Rows[0]["Offer_App_ID"].ToString();
    //        hdncandidateOffer.Value = Offer.Rows[0]["Candidate_Status"].ToString();
    //        hdnOfferAppID.Value = values;
    //        //hdnapprid.Value = dtOfferApproverEmailIds.Rows[0]["APPR_ID"].ToString();			
    //    }
    //    if (Result == "Pending")
    //    {
    //        //trvl_accmo_btn.Visible = false;
    //        mobile_btnSave.Visible = false;
    //    }
    //    if (Result == "Approved" && hdncandidateOffer.Value == "")
    //    {
    //        hdnOfferStatus.Value = Result;
    //        //DDLStatusUpdate.Enabled = true;
    //        mobile_btnSave.Visible = false;
    //       // trvl_accmo_btn.Visible = false;
    //    }
    //    if (Result == "Reject")
    //    {
    //        mobile_btnSave.Visible = true;
    //       // trvl_accmo_btn.Visible = true;
    //    }
    //    if (Result == "Approved" && hdncandidateOffer.Value == "Rejected")
    //    {
    //        //hdnOfferStatus.Value = Result;
    //        //DDLStatusUpdate.Enabled = true;

    //        mobile_btnSave.Visible = true;
    //      //  trvl_accmo_btn.Visible = true;
    //    }

    //}
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

        //DDLStatusUpdate.DataSource = dsCVSource.Tables[7];
        //DDLStatusUpdate.DataTextField = "StatusUpdate";
        //DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
        //DDLStatusUpdate.DataBind();
        //DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
    }

    //private void getStatusUpdate()
    //{
    //    dsCVSource = spm.GetCVSource();

    //    DDLStatusUpdate.DataSource = dsCVSource.Tables[7];
    //    DDLStatusUpdate.DataTextField = "StatusUpdate";
    //    DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
    //    DDLStatusUpdate.DataBind();
    //    DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
    //}
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
            spars[0].Value = "RecruitmentReq_ViewRecruiter";
            spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
            spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
            spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            spars[2].Value = Session["Empcode"].ToString();
            dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_View");

            if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
            {
                txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
                txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();
                txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
                HDInterviewerSchedularEmpCode.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["InterviewerSchedularEmpCode"]).Trim();

                txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
                txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
                txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();

                lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
                GetInterviewerScreeningBy(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));

                lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
                lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
                //   lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
                txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
                lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
                hdncomp_code.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["comp_code"]).Trim();
                hdndept_Id.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
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
                //LstRecommPerson.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecommendedPerson"]).Trim();
                txtComments.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Comments"]).Trim();

                lstInterviewerOneView.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
                Lnk_Questionnaire.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
                txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();

                string StrStatusflag = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"]).Trim();
                if (StrStatusflag == "Closed")
                {
                    lblheading.Text = "Requisition Information-Closed";
                }
                else
                {
                    lblheading.Text = "Requisition Information-Open";
                }

                // GetFilterGD();
                if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
                {
                    if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
                    {
                        GVShortListInterviewer.DataSource = dsRecruitmentDetails.Tables[1];
                        GVShortListInterviewer.DataBind();
                    }
                    else
                    {
                        GVShortListInterviewer.DataSource = null;
                        GVShortListInterviewer.DataBind();

                        GVCandidateRoundHistory.DataSource = null;
                        GVCandidateRoundHistory.DataBind();
                    }
                }
                //if (dsRecruitmentDetails.Tables[2].Rows[0]["MaxJoiningDateClose"].ToString() == "")
                //{
                //    SpanJoinemployee.Visible = false;
                //    SpanCloseReqChk.Visible = false;
                //    trvldeatils_btnSave.Visible = false;
                //    JobDetail_btnSave.Visible = true;

                //}
                //else
                //{
                //    if (Convert.ToDateTime(dsRecruitmentDetails.Tables[2].Rows[0]["MaxJoiningDateClose"]) <= Convert.ToDateTime(DateTime.Now.ToString()))
                //    {
                //        SpanJoinemployee.Visible = true;
                //        SpanCloseReqChk.Visible = true;
                //        trvldeatils_btnSave.Visible = true;
                //        // JobDetail_btnSave.Visible = false;
                //    }
                //    else
                //    {
                //        SpanJoinemployee.Visible = false;
                //        SpanCloseReqChk.Visible = false;
                //        trvldeatils_btnSave.Visible = false;
                //        JobDetail_btnSave.Visible = true;
                //    }
                //}
                //string StrStatusflag = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"]).Trim();
                //if (StrStatusflag == "Closed")
                //{
                //    Linkbtn_CandidateShortlisting.Enabled = false;
                //    trvldeatils_btnSave.Visible = false;
                //    JobDetail_btnSave.Visible = false;
                //    CheckClosedRequisition.Checked = true;
                //    CheckClosedRequisition.Enabled = false;
                //    if (dsRecruitmentDetails.Tables[3].Rows.Count > 0)
                //    {
                //        DDLJoinedemployee.SelectedValue = dsRecruitmentDetails.Tables[3].Rows[0]["empcode"].ToString();
                //        DDLJoinedemployee.Enabled = false;
                //    }
                //}
                //if (dsRecruitmentDetails.Tables[4].Rows.Count > 0)
                //{
                //    trvldeatils_btnSave.Visible = false;
                //    JobDetail_btnSave.Visible = false;
                //}
                //if (Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Status_ID"]).Trim() == "3" && StrStatusflag == "Cancelled")
                //{
                //    trvldeatils_btnSave.Visible = false;
                //    JobDetail_btnSave.Visible = false;
                //}

            }

            //if (HDInterviewerSchedularEmpCode.Value == Convert.ToString(Session["Empcode"]).Trim())
            //{
            //    SpanCloseReqChk.Visible = false;
            //    SpanJoinemployee.Visible = false;
            //    trvldeatils_btnSave.Visible = false;
            //}

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    #endregion

    //protected void DDLStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (DDLStatusUpdate.SelectedValue == "4" || DDLStatusUpdate.SelectedValue == "9" || DDLStatusUpdate.SelectedValue == "11")
    //    {
    //        SpanRecruitercomment.Visible = true;
    //    }
    //    else
    //    {
    //        SpanRecruitercomment.Visible = false;
    //        if (DDLStatusUpdate.SelectedValue == "12")
    //        {
    //            LI1JoiningdetailCandidate.Visible = true;
    //            LI2JoiningdetailCandidate.Visible = true;
    //            LI3JoiningdetailCandidate.Visible = true;
    //        }
    //        else
    //        {
    //            LI1JoiningdetailCandidate.Visible = false;
    //            LI2JoiningdetailCandidate.Visible = false;
    //            LI3JoiningdetailCandidate.Visible = false;
    //        }
    //    }
    //}

    protected void GVJoiningDetailInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton lnkIBAcceptanceFile = (e.Row.FindControl("lnkAcceptanceFile") as ImageButton);

            if (e.Row.Cells[2].Text.Trim() == "Resignation Submission /Acceptance")
            {
                //lnkIBAcceptanceFile.Visible = true;
            }
            else
            {
                //lnkIBAcceptanceFile.Visible = false;
            }
        }
    }
    #region Bharat Mainkar 01-07-21
    protected void trvl_accmo_btn_Click(object sender, EventArgs e)
    {
        if (OfferCreate.Visible)
        {
            OfferCreate.Visible = false;
        }
        else
        {
            OfferCreate.Visible = true;
            //  GetOffer_Approverlist();

        }
    }
    //private void GetOffer_Approverlist()
    //{
    //    int RecrutID = 0, Offer_App_ID = 0;
    //    DataTable dtapprover = new DataTable();

    //    RecrutID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
    //    Offer_App_ID = Convert.ToString(hdnOfferAppID.Value).Trim() != "" ? Convert.ToInt32(hdnOfferAppID.Value) : 0;
    //    dtapprover = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID);
    //    DgvOfferApprover.DataSource = null;
    //    if (dtapprover.Rows.Count > 0)
    //    {
    //        DgvOfferApprover.DataSource = dtapprover;
    //        DgvOfferApprover.DataBind();
    //    }
    //}

    //protected void mobile_btnSave_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string confirmValue = hdnYesNo.Value.ToString();
    //        if (confirmValue != "Yes")
    //        {
    //            return;
    //        }
    //        string[] strdate;
    //        string strtoDate = "";
    //        DateTime Offerdate;
    //        lblmessage.Text = "";
    //        if (txtOfferDate.Text == "")
    //        {
    //            lblOffer.Text = "Please enter offer Date";
    //            return;

    //        }
    //        if (txtOfferAppcmt.Text.Trim() == "")
    //        {
    //            lblOffer.Text = "Please enter offer approval Comments";
    //            return;
    //        }
    //        if (Convert.ToString(FileOfferUpload.FileName).Trim() == "")
    //        {
    //            lblOffer.Text = "Please upload Offer Approval Document ";
    //            return;
    //        }
    //        if (Convert.ToString(FileOfferUpload.FileName).Trim() != "")
    //        {
    //            HttpFileCollection fileCollection = Request.Files;
    //            for (int i = 0; i < fileCollection.Count; i++)
    //            {
    //                HttpPostedFile uploadfileName = fileCollection[i];
    //                string fileName = Path.GetFileName(FileOfferUpload.FileName);
    //                if (uploadfileName.ContentLength > 0)
    //                {
    //                    multiplefilename = fileName;
    //                    string strfileName = "";
    //                    string strremoveSpace = hdCandidate_ID.Value + "_" + multiplefilename;
    //                    strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
    //                    strfileName = strremoveSpace;
    //                    multiplefilename = strfileName;
    //                    uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["OfferApprovalDocumentpath"]).Trim()), strfileName));
    //                    multiplefilenameadd += strfileName + ",";
    //                }
    //            }
    //            multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
    //        }


    //        strdate = Convert.ToString(txtOfferDate.Text).Trim().Split('/');
    //        strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
    //        Offerdate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

    //        String strOfferApprovalURL = "", approveremailaddress = "", strApproverlist = "", AppName = "";
    //        int Offer_AppID = 0, apprid = 0;
    //        dtofferApproval = spm.InsertCandidate_Offer_Approval(Convert.ToString(Session["Empcode"]).Trim(), Offerdate, Convert.ToInt32(0), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), txtOfferAppcmt.Text.Trim(), multiplefilenameadd);
    //        if (dtofferApproval.Rows.Count > 0)
    //        {
    //            Offer_AppID = Convert.ToInt32(dtofferApproval.Rows[0]["Offer_App_ID"]);
    //            hdnOfferAppID.Value = dtofferApproval.Rows[0]["Offer_App_ID"].ToString();
    //            dtOfferApproverEmailIds = spm.GetRequisition_Offer_Approver_Status(Offer_AppID, Convert.ToInt32(hdRecruitment_ReqID.Value));
    //            if (dtOfferApproverEmailIds.Rows.Count > 0)
    //            {
    //                approveremailaddress = (string)dtOfferApproverEmailIds.Rows[0]["Emp_Emailaddress"];
    //                hdnapprid.Value = dtOfferApproverEmailIds.Rows[0]["APPR_ID"].ToString();
    //                hdnofferappcode.Value = (string)dtOfferApproverEmailIds.Rows[0]["A_EMP_CODE"];
    //                AppName = (string)dtOfferApproverEmailIds.Rows[0]["Emp_Name"];
    //            }
    //            spm.Insert_Req_Offer_Approver_Request(hdnofferappcode.Value, Convert.ToInt32(hdnapprid.Value), Offer_AppID);

    //            strApproverlist = GetReq_offer_Approve_RejectList(Offer_AppID, Convert.ToInt32(hdRecruitment_ReqID.Value));
    //            strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Approval"]).Trim() + "?Offer_App_ID=" + Offer_AppID;
    //            //spm.send_mailto_Req_Requisition_Approver(txtReqName.Text, txtReqEmail.Text, hdnApproverid_LWPPLEmail.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), lstSkillset.SelectedItem.Text + " - " + lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);
    //            spm.send_mailto_Req_Requisition_Offer_Approver(hdnLoginUserName.Value, AppName, hdnLoginEmpEmail.Value, approveremailaddress, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text), lstSkillset.SelectedItem.Text + " - " + lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtOfferAppcmt.Text.Trim(), hdnFinalizedDate.Value, "", strOfferApprovalURL, strApproverlist, "");
    //            ClearOffer();
    //            GetOffer_Approverlist();

    //        }
    //        else
    //        {
    //            lblOffer.Text = "Please check record already exists";
    //            return;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message);
    //    }
    //}



    protected string GetReq_offer_Approve_RejectList(int Offer_App_ID, int RecrutID)
    {
        var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
        var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
        var Dept_id = 0;
        var qtype = "GETREQUISITION_OFFER_APPROVER_STATUS_COMP";
        if (getcompSelectedText.Contains("Head Office"))
        {
            Dept_id = Convert.ToInt32(hdndept_Id.Value);
            qtype = "GETREQUISITION_OFFER_APPROVER_STATUS";
        }
        StringBuilder sbapp = new StringBuilder();
        sbapp.Length = 0;
        sbapp.Capacity = 0;
        DataTable dtAppRej = new DataTable();
        dtAppRej = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID,qtype,getcompSelectedval,Dept_id);
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
	#endregion
	private void OfferCreatelist()
	{
		int RecrutID = 0, Candidate_ID = 0;
		RecrutID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
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
			spoffer.Visible = true;
			GRDOfferCreatelist.DataSource = dsOffercreate.Tables[0];
			GRDOfferCreatelist.DataBind();

		}
		else
		{
			GRDOfferCreatelist.DataSource = null;
			GRDOfferCreatelist.DataBind();
			spoffer.Visible = false;
			OfferCreate.Visible = false;
		}
		//OfferHistList(Convert.ToInt32(hdnOffer_App_ID.Value));
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
				OfferCreate.Visible = true;

				txtOfferDate.Text = dsOffercreate.Tables[0].Rows[0]["Offer_Date"].ToString();
				lstOfferBand.Text = dsOffercreate.Tables[0].Rows[0]["OfferBAND"].ToString();
				txtpositionOffer.Text = dsOffercreate.Tables[0].Rows[0]["PositionTitle"].ToString();
				txtOfferno1.Text = dsOffercreate.Tables[0].Rows[0]["OldCTC"].ToString();
				txtOfferno2.Text = dsOffercreate.Tables[0].Rows[0]["NewCTC"].ToString();
				txtExceptionamt.Text = dsOffercreate.Tables[0].Rows[0]["ExceptionAmount"].ToString();
				txtOfferAppcmt.Text = dsOffercreate.Tables[0].Rows[0]["Comment"].ToString();
				txtProbableJoiningDate.Text = dsOffercreate.Tables[0].Rows[0]["ProbableJoiningDate"].ToString();
				lstRecruitmentCharges.Text = dsOffercreate.Tables[0].Rows[0]["RecruitmentCharges"].ToString();
				if (dsOffercreate.Tables[0].Rows[0]["IsException"].ToString() == "No")
				{
					chk_exception.Checked = false;
				}
				else
				{
					chk_exception.Checked = true;
				}

				OfferhistoryS.Visible = true;

				GRDOfferHistory.DataSource = dsOffercreate.Tables[1];
				GRDOfferHistory.DataBind();
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
	#endregion
}