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
using Microsoft.Reporting.WebForms;

public partial class procs_RecruiterViewEdit : System.Web.UI.Page
{

	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public int did = 0;
	SP_Methods spm = new SP_Methods();
	private static Random random = new Random();
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

					Txt_JoiningDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtOfferDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
					txtProbableJoiningDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    TxtOfferAcceptanceByDate.Attributes.Add("onkeypress", "return noanyCharecters(event);");
                    
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
					GetCandidateEmployeeType();
					SearchGetSkillsetName();
					SearchGetCompany_Location();
					SearchGetlstPositionBand();
					SearchGetPositionName();
					GetecruitmentDetail();
					GetlstRequisitionNo();
					Get_OfferEmployeeType();
					GetOfferLocation();
                    GetOfferOfficeLocation();
					lnk_Offer_Cancle.Visible = false;
					if (Convert.ToString(Session["RecruiterList"]).Trim() != "" || Session["RecruiterList"] != null)
					{
						string Result = Convert.ToString(Session["RecruiterList"]).Trim();
						btnRecBack.HRef = "Rec_RecruiterInbox.aspx?type=" + Result;
					}
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
		//trvldeatils_btnSave.Visible = false;
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
		string StrShorlistingStatus = row.Cells[9].Text.Trim();
		string StrInterviewStatus = row.Cells[10].Text.Trim();
		string StrInterviewFeedback = row.Cells[11].Text.Trim();
		string StrHiringStatus = row.Cells[12].Text.Trim();
		string StrCandiadteStatusTransfer = row.Cells[13].Text.Trim();

		DDLsearchRequisitionnumber.SelectedValue = "0";
		if (HFRecruitmentStatus.Value == "Open")
		{

			if (StrShorlistingStatus == "Shortlisted")
			{
				Link_TransferHideUnhide.Text = "+";
				DIV_TransferCanInfo1.Visible = true;
				DIV_TransferCanInfo2.Visible = true;
				DIV_TransferCanInfo3.Visible = true;
			}
			if (StrInterviewStatus == "Backout")
			{
				CandidateTransferfalse();
			}
			if (StrInterviewFeedback == "Rejected")
			{
				CandidateTransferfalse();
			}
			if (StrHiringStatus.Trim() == "Offer Under Approval" || StrHiringStatus.Trim() == "Offer Released" || StrHiringStatus.Trim() == "Joining On Time" || StrHiringStatus.Trim() == "Joining Extended" || StrHiringStatus.Trim() == "Joined" || StrHiringStatus.Trim() == "Not Joining" || StrHiringStatus.Trim() == "Backout")
			{
				CandidateTransferfalse();
			}
			if (StrHiringStatus.Trim() == "Negotiation Done" || StrHiringStatus.Trim() == "Offer Rejected" || StrHiringStatus.Trim() == "Offer Approval Rejected")
			{
				trvl_accmo_btn.Visible = true;
			}
			else
			{
				trvl_accmo_btn.Visible = false;
			}

		}

		string str = hf.Value.ToString().Trim();
		Txt_ScreenerComments.Text = HFScreenerRemark.Value.ToString().Trim();
		PopulateCandidateData();
		DivViewrowWiseCandidateInformation.Visible = true;
		//trvldeatils_btnSave.Focus();
		DivCandidateRoundHistory.Visible = true;
		DivButton.Visible = true;
		if (str == "Finalized")
		{
			JobDetail_btnSave.Visible = true;
			DivJoiningDetails1.Visible = true;
			DivJoiningDetails2.Visible = true;
			DivJoiningDetailInformation.Visible = true;
			LIRecruiterComment.Visible = true;
			txtRecruitercomment.Visible = true;
			Rec_AddjoinDetailbtn.Text = "-";
			txtRecruitercomment.Text = "";
			Txt_JoiningDate.Text = "";
			DDLStatusUpdate.SelectedValue = "";
			//trvldeatils_btnSave.Visible = true;
			if (Linkbtn_CandidateShortlisting.Enabled == false)
			{
				DivJoiningDetails1.Visible = false;
				DivJoiningDetails2.Visible = false;
				//	trvldeatils_btnSave.Visible = false;
			}
			if (HFRecruitmentCancel.Value!= "")
			{
				JobDetail_btnSave.Visible = false;
				trvl_accmo_btn.Visible = false;
			}
		}
		else
		{
			DivJoiningDetails1.Visible = false;
			DivJoiningDetails2.Visible = false;
			DivJoiningDetailInformation.Visible = false;
			LIRecruiterComment.Visible = false;
			txtRecruitercomment.Visible = false;
		}
		//GetOffer_Approverlist();
		OfferCreate.Visible = false;
		DgvOfferApprover.DataSource = null;
		DgvOfferApprover.DataBind();
		GRDOfferHistory.DataSource = null;
		GRDOfferHistory.DataBind();
		OfferhistoryS.Visible = false;
		OfferCreatelist();
		//GetOffer_Approver_History_list();
		if (StrCandiadteStatusTransfer == "Transferred")
		{
			CandidateTransferfalse();
			DivJoiningDetails1.Visible = false;
			DivJoiningDetails2.Visible = false;
			//trvldeatils_btnSave.Visible = false;
		}
	}

	private void CandidateTransferfalse()
	{
		DIV_TransferCanInfo1.Visible = false;
		DIV_TransferCanInfo2.Visible = false;
		DIV_TransferCanInfo3.Visible = false;
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
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{
		DivViewrowWiseCandidateInformation.Visible = false;
		DivCandidateRoundHistory.Visible = false;
		DivButton.Visible = false;
		DivJoiningDetails1.Visible = false;
		DivJoiningDetails2.Visible = false;
		DivJoiningDetailInformation.Visible = false;
		OfferhistoryS.Visible = false;
		GRDOfferHistory.DataSource = null;
		GRDOfferHistory.DataBind();
		GRDOfferCreatelist.DataSource = null;
		GRDOfferCreatelist.DataBind();
		OfferCreate.Visible = false;
		Span2.Visible = false;

	}

	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		//try
		//{
		//    string confirmValue = hdnYesNo.Value.ToString();
		//    if (confirmValue != "Yes")
		//    {
		//        return;
		//    }
		//    if (DDLJoinedemployee.SelectedValue == "0")
		//    {
		//        lblmessage.Text = "Please enter joined employee";
		//        return;
		//    }
		//    if (txtemployeeJoiningDate.Text == "")
		//    {
		//        lblmessage.Text = "Please enter joining date";
		//        return;
		//    }
		//    DateTime? CandidatejoiningDate = null;
		//    if (txtemployeeJoiningDate.Text != "")
		//    {
		//        string[] strdate1;
		//        string strtoDate = "";
		//        strdate1 = Convert.ToString(txtemployeeJoiningDate.Text).Trim().Split('-');
		//        strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
		//        CandidatejoiningDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
		//    }

		//    DataSet dsInterviewSchedule = new DataSet();
		//    lblmessage.Text = "";
		//    if (lblmessage.Text == "")
		//    {
		//        SqlParameter[] spars = new SqlParameter[9];
		//        spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		//        spars[0].Value = "InterviewFinalupdateClosedfromRecruiter";
		//        spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
		//        spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
		//        spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		//        spars[2].Value = Session["Empcode"].ToString();
		//        spars[3] = new SqlParameter("@empcodenew", SqlDbType.VarChar);
		//        spars[3].Value = DDLJoinedemployee.SelectedValue;
		//        spars[4] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
		//        spars[4].Value = hdCandidate_ID.Value;
		//        spars[5] = new SqlParameter("@CandJoiningDate", SqlDbType.DateTime);
		//        spars[5].Value = CandidatejoiningDate;

		//        dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");


		//        string mailrec = "";
		//        if (dsInterviewSchedule.Tables[4].Rows.Count > 0)
		//        {
		//            for (int i = 0; i < dsInterviewSchedule.Tables[4].Rows.Count; i++)
		//            {
		//                if (dsInterviewSchedule.Tables[4].Rows[i]["Appr_id"].ToString() == "103")
		//                {
		//                    mailrec += dsInterviewSchedule.Tables[4].Rows[i]["Emp_Emailaddress"].ToString() + ",";
		//                }
		//            }
		//        }
		//        if (mailrec.ToString().Trim() != "")
		//        {
		//            mailrec = mailrec.TrimEnd(',');
		//            if (txtReqEmail.Text.Trim() != mailrec.ToString().Trim())
		//            {
		//                mailrec = txtReqEmail.Text + "," + mailrec.ToString().Trim();
		//            }
		//            else
		//            {
		//                mailrec = txtReqEmail.Text;
		//            }
		//        }

		//        string mailsubject = "";
		//        string mailcontain1 = "";
		//        mailsubject = "Recruitment - Candidate  " + DDLJoinedemployee.SelectedItem.Text + " joined for requisition  " + txtReqNumber.Text + " Of " + txtReqName.Text;
		//        mailcontain1 = "This is to inform you that the requisition " + txtReqNumber.Text + " is closed as the following candidate has joined against this requisition;";

		//        string strtomail = mailrec;
		//        string strrecmail = dsInterviewSchedule.Tables[1].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString();
		//        string strrecruitername = dsInterviewSchedule.Tables[1].Rows[0]["Recruitername1"].ToString();
		//        string strempnameddl = DDLJoinedemployee.SelectedItem.Text;
		//        string strempcodedll = DDLJoinedemployee.SelectedValue;
		//        string strDepartment = dsInterviewSchedule.Tables[0].Rows[0]["Department_Name"].ToString();
		//        string strLocation = dsInterviewSchedule.Tables[0].Rows[0]["Location_name"].ToString();
		//        string strDesignation = dsInterviewSchedule.Tables[0].Rows[0]["DesginationName"].ToString();
		//        string strBand = dsInterviewSchedule.Tables[0].Rows[0]["BAND"].ToString();


		//        string RequiredByDate = "";
		//        RequiredByDate = GetRequiredByDate();
		//        spm.send_mailto_closerequisition_Mail(strrecmail, strtomail, mailsubject, mailcontain1, DDLJoinedemployee.SelectedItem.Text, DDLJoinedemployee.SelectedValue, strDepartment, strLocation, strDesignation, strBand, txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
		//        lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, strrecruitername);

		//        if (dsInterviewSchedule.Tables[3].Rows[0]["ResultCount"].ToString() == "1")
		//        {
		//            Response.Redirect("~/procs/Requisition_Index.aspx");
		//        }
		//        else
		//        {
		//            lblmessage.Text = "Please enter the joining information otherwise candidate not Finalized";
		//        }
		//    }

		//}
		//catch (Exception ex)
		//{
		//    Response.Write(ex.Message);
		//    //throw;
		//}
	}

	protected void Linkbtn_CandidateShortlisting_Click(object sender, EventArgs e)
	{
		//if (HDInterviewerSchedularEmpCode.Value == Convert.ToString(Session["Empcode"]).Trim())
		//{

		//}
		//else
		//{
		Response.Redirect("~/procs/RecruiterSendShortListing.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value + "&Flag=0");
		//  }  
	}

	protected void Rec_AddjoinDetailbtn_Click(object sender, EventArgs e)
	{
		if (Rec_AddjoinDetailbtn.Text == "-")
		{
			Rec_AddjoinDetailbtn.Text = "-";
			DivJoiningDetails2.Visible = true;
			DivJoiningDetailInformation.Visible = true;
		}
		else
		{
			Rec_AddjoinDetailbtn.Text = "+";
			DivJoiningDetails2.Visible = false;
			DivJoiningDetailInformation.Visible = false;
		}
	}

	protected void JobDetail_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			DataSet dsInterviewSchedule = new DataSet();
			lblmessageJoining.Text = "";
			if (DDLStatusUpdate.SelectedValue == "")
			{
				DivMessageJoining.Visible = true;
				lblmessageJoining.Text = "Please Select the Status Update";
				return;
			}
			if (Txt_JoiningDate.Text == "")
			{
				DivMessageJoining.Visible = true;
				lblmessageJoining.Text = "Please Select the date";
				return;
			}

			if (DDLStatusUpdate.SelectedValue == "4" || DDLStatusUpdate.SelectedValue == "9" || DDLStatusUpdate.SelectedValue == "11")
			{
				if (txtRecruitercomment.Text == "")
				{
					DivMessageJoining.Visible = true;
					lblmessageJoining.Text = "Please enter  the recruiter comment";
					return;
				}
			}
			if (DDLStatusUpdate.SelectedValue == "12")
			{
				if (Convert.ToString(FileUpload1.FileName).Trim() == "")
				{
					DivMessageJoining.Visible = true;
					lblmessageJoining.Text = "Please upload file";
					return;
				}
			}
			if (DDLStatusUpdate.SelectedValue == "3")
			{
				if (lstJoinEmploymentType.SelectedValue == "" || lstJoinEmploymentType.SelectedValue == "0")
				{
					DivMessageJoining.Visible = true;
					lblmessageJoining.Text = "Please select employee type";
					return;
				}

				if (lstjoinband.SelectedValue == "" || lstjoinband.SelectedValue == "0")
				{
					DivMessageJoining.Visible = true;
					lblmessageJoining.Text = "Please select candidate joining band";
					return;
				}

				if (txtcandjoindate.Text == "")
				{
					DivMessageJoining.Visible = true;
					lblmessageJoining.Text = "Please enter candidate joining date";
					return;
				}

			}
			DataSet ds = new DataSet();
			SqlParameter[] spars1 = new SqlParameter[5];
			spars1[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars1[0].Value = "RecruitmentReq_InterviewerFeedBackEdit";
			spars1[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars1[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
			spars1[2] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
			spars1[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
			string[] strdatech;
			string strtoDateck = "";
			strdatech = Convert.ToString(Txt_JoiningDate.Text).Trim().Split('/');
			strtoDateck = Convert.ToString(strdatech[2]) + "-" + Convert.ToString(strdatech[1]) + "-" + Convert.ToString(strdatech[0]);
			DateTime ddtch = DateTime.ParseExact(strtoDateck, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			//spars1[3] = new SqlParameter("@JoiningDate", SqlDbType.DateTime);
			// spars1[3].Value = Convert.ToDateTime(ddtch);

			ds = spm.getDatasetList(spars1, "SP_GetRecruitment_Interviewerfeedback");

			if (ds.Tables[3].Rows.Count > 0)
			{
				for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
				{
					if (Convert.ToDateTime(ds.Tables[3].Rows[i]["checkInterviewDate"]) <= Convert.ToDateTime(ddtch))
					{
						//lblmessage.Text = "";
					}
					else
					{
						DivMessageJoining.Visible = true;
						lblmessageJoining.Text = "Please enter greater than or equal date as per Interview Date";
						return;
					}
				}

			}

			if (lblmessageJoining.Text == "")
			{
				if (ds.Tables[10].Rows.Count > 0)
				{
					if (ds.Tables[10].Rows[0]["MaxJoiningDate"].ToString() == "")
					{
						DivMessageJoining.Visible = false;
						lblmessageJoining.Text = "";
					}
					else
					{
						if (Convert.ToDateTime(ds.Tables[10].Rows[0]["MaxJoiningDate"]) <= Convert.ToDateTime(ddtch))
						{
							DivMessageJoining.Visible = false;
							lblmessageJoining.Text = "";
						}
						else
						{
							//Txt_JoiningDate.Text = "";
							//DivMessageJoining.Visible = true;
							//lblmessageJoining.Text = "Please enter greater than or equal date as per status update";
							//return;
							if (DDLStatusUpdate.SelectedValue == "9")
							{
								string strtodaydate = System.DateTime.Now.ToString();
								if (Convert.ToDateTime(strtodaydate) <= Convert.ToDateTime(ddtch))
								{
									Txt_JoiningDate.Text = "";
									DivMessageJoining.Visible = true;
									lblmessageJoining.Text = "Please enter today date";
									return;
								}
								else
								{
									DivMessageJoining.Visible = false;
									lblmessageJoining.Text = "";
								}
							}
							else
							{

								Txt_JoiningDate.Text = "";
								DivMessageJoining.Visible = true;
								lblmessageJoining.Text = "Please enter greater than or equal date as per status update";
								return;
							}
						}
					}
				}
			}
			DateTime? CandidatejoiningDate = null;
			if (txtcandjoindate.Text != "")
			{
				string[] strdate1;
				string strtoDate = "";
				strdate1 = Convert.ToString(txtcandjoindate.Text).Trim().Split('/');
				strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
				CandidatejoiningDate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
			}

			if (lblmessageJoining.Text == "")
			{
				SqlParameter[] spars = new SqlParameter[11];
				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "InterviewFinalupdatefromRecruiter";
				spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
				spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
				spars[2].Value = Session["Empcode"].ToString();
				spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
				spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);

				if (Txt_JoiningDate.Text != "")
				{
					string[] strdate1;
					string strtoDate = "";
					strdate1 = Convert.ToString(Txt_JoiningDate.Text).Trim().Split('/');
					strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
					DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
					spars[4] = new SqlParameter("@JoiningDate", SqlDbType.DateTime);
					spars[4].Value = Convert.ToDateTime(ddt);
				}
				else
				{
					spars[4] = new SqlParameter("@JoiningDate", SqlDbType.VarChar);
					spars[4].Value = "";
				}
				spars[5] = new SqlParameter("@StatusUpdate", SqlDbType.Int);
				spars[5].Value = Convert.ToInt32(DDLStatusUpdate.SelectedValue);
				spars[6] = new SqlParameter("@RecruiterComments", SqlDbType.VarChar);
				spars[6].Value = txtRecruitercomment.Text;
				if (Convert.ToString(FileUpload1.FileName).Trim() != "")
				{
					string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
					filename = FileUpload1.FileName;
					string strfileName = "";

					//string strremoveSpace = txtName.Text.Trim() + "_" + hdRecruitment_ReqID.Value + "_" + str + "_" + filename;
					string strremoveSpace = hdCandidate_ID.Value + "_" + hdRecruitment_ReqID.Value + "_" + str + "_" + filename;
					strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
					strfileName = strremoveSpace; //+ Path.GetExtension(FileUpload1.FileName);
					filename = strfileName;
					FileUpload1.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateJoiningtimefile"]).Trim()), strfileName));

					spars[7] = new SqlParameter("@AcceptanceFile", SqlDbType.VarChar);
					spars[7].Value = filename;
				}
				spars[8] = new SqlParameter("@CandJoiningDate", SqlDbType.DateTime);
				spars[8].Value = CandidatejoiningDate;

				spars[9] = new SqlParameter("@PID", SqlDbType.Int);
				spars[9].Value = Convert.ToInt32(lstJoinEmploymentType.SelectedValue);
				spars[10] = new SqlParameter("@JoiningBand", SqlDbType.VarChar);
				spars[10].Value = lstjoinband.SelectedValue;
				if (DDLStatusUpdate.SelectedValue == "3")
				{
					InsertCandidateLoginDetail_ELC();
				}

				if (DDLStatusUpdate.SelectedValue == "7")//Joining Extended revised offer letter for reguler employee
				{
					Joining_Extended_Revised_Offer_Letter();
				}

				dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");
				string recruitername = dsInterviewSchedule.Tables[0].Rows[0]["Recruitername1"].ToString();

				string mailrec = "";
				// DataTable dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(HFempcoderec.Value);
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

				string mailsubject = "";
				string mailcontain = "";
				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();

				if (DDLStatusUpdate.SelectedValue == "4")
				{
					mailsubject = "Recruitment - finalized candidate offer rejected for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please find below is the status update for finalized candidate.";

					spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
					lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, recruitername);

				}
				if (DDLStatusUpdate.SelectedValue == "5")
				{
					mailsubject = "Recruitment - finalized candidate offer released for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please find below is the status update for finalized candidate.";

					spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
					lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, recruitername);

				}
				if (DDLStatusUpdate.SelectedValue == "6")
				{
					mailsubject = "Recruitment - finalized candidate joining on time for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please find below is the status update for finalized candidate.";

					spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
					lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, recruitername);

				}
				if (DDLStatusUpdate.SelectedValue == "8")
				{
					mailsubject = "Recruitment - finalized candidate joining for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please find below is the status update for finalized candidate.";

					spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
					lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, recruitername);

				}
				if (DDLStatusUpdate.SelectedValue == "9")
				{
					mailsubject = "Recruitment - finalized candidate not joining for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please find below is the status update for finalized candidate.";

					spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
					lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, recruitername);

				}
				if (DDLStatusUpdate.SelectedValue == "11")
				{
					mailsubject = "Recruitment - finalized candidate Backout for the request " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please find below is the status update for finalized candidate.";

					spm.send_mailto_RecruiterAllStatusJoining(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
					lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, recruitername);

				}
				if (DDLStatusUpdate.SelectedValue == "11" || DDLStatusUpdate.SelectedValue == "1" || DDLStatusUpdate.SelectedValue == "9")
				{
					CheckReferral_Candidated(Convert.ToString(hdCandidate_ID.Value), DDLStatusUpdate.SelectedValue);
				}

				if (dsInterviewSchedule.Tables[1].Rows.Count > 0)
				{
					dsCVSource = spm.GetCVSource();
					DataTable dt = new DataTable();
					dt = dsCVSource.Tables[7];
					for (int i = dt.Rows.Count - 1; i >= 0; i--)
					{

						DataRow dr = dt.Rows[i];
						if (DDLStatusUpdate.SelectedValue == "1")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "2")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
								{
									dr.Delete();
								}
							}
						}
						if (DDLStatusUpdate.SelectedValue == "2")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "10")
							{
								//dr.Delete();
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
								{
									dr.Delete();
								}
							}
						}

						if (DDLStatusUpdate.SelectedValue == "10")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
							{
								//dr.Delete();
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11" && DDLStatusUpdate.SelectedValue == "4")
								{
									dr.Delete();
								}
							}
						}
						//if (DDLStatusUpdate.SelectedValue == "2")
						//{
						//    if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
						//    {
						//        dr.Delete();
						//    } 
						// }
						if (DDLStatusUpdate.SelectedValue == "5")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "3")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "4")
								{
									if (dr["StatusUpdate_ID"].ToString().Trim() != "11" && DDLStatusUpdate.SelectedValue == "4")
									{
										dr.Delete();
									}
									// dr.Delete();
								}
							}
						}
						//if (DDLStatusUpdate.SelectedValue == "3")
						//{
						//    if (dr["StatusUpdate_ID"].ToString().Trim() != "8")
						//    {
						//        if (dr["StatusUpdate_ID"].ToString().Trim() != "9")
						//        {
						//            dr.Delete();
						//        }
						//    }
						//}

						if (DDLStatusUpdate.SelectedValue == "3")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "12" && DDLStatusUpdate.SelectedValue == "4")
							{
								dr.Delete();
							}
						}
						if (DDLStatusUpdate.SelectedValue == "12")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "8")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "9" && DDLStatusUpdate.SelectedValue == "4")
								{
									dr.Delete();
								}
							}
						}



						if (DDLStatusUpdate.SelectedValue == "4")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
								{
									dr.Delete();
								}
							}
						}
						if (DDLStatusUpdate.SelectedValue == "8" || DDLStatusUpdate.SelectedValue == "6" || DDLStatusUpdate.SelectedValue == "7")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "6")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "7")
								{
									if (dr["StatusUpdate_ID"].ToString().Trim() != "9" && DDLStatusUpdate.SelectedValue == "4")
									{
										dr.Delete();
									}
								}
							}
						}
						if (DDLStatusUpdate.SelectedValue == "9")
						{
							dr.Delete();
						}
						if (DDLStatusUpdate.SelectedValue == "11")
						{
							dr.Delete();
						}
					}
					dt.AcceptChanges();
					DDLStatusUpdate.DataSource = dt;
					DDLStatusUpdate.DataTextField = "StatusUpdate";
					DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
					DDLStatusUpdate.DataBind();
					DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
					string Result = "Negotiation Done";
					//Negotiation Done		
					if (dsInterviewSchedule.Tables[1].Rows.Count == 2)
					{
						DataRow[] dr1 = dsInterviewSchedule.Tables[1].Select("StatusUpdate='" + Result + "'");
						if (dr1.Length > 0)
						{
							string itemValue = "10";   /* 3 for BodyRepair */
							if (DDLStatusUpdate.Items.FindByValue(itemValue) != null)
							{
								string itemText = DDLStatusUpdate.Items.FindByValue(itemValue).Text;
								ListItem li = new ListItem();
								li.Text = itemText;
								li.Value = itemValue;
								DDLStatusUpdate.Items.Remove(li);
								trvl_accmo_btn.Visible = true;
								//DDLStatusUpdate.Enabled = false;
								//hdnFinalizedDate.Value = dr[0]["EnterviewDate"].ToString();
							}
						}
					}
					string OfferReject = "Offer Rejected";
					DataRow[] dr3 = dsInterviewSchedule.Tables[1].Select("StatusUpdate='" + OfferReject + "'");
					if (dr3.Length > 0)
					{

						//DDLStatusUpdate.Enabled = false;
						string itemValue = "5";
						if (DDLStatusUpdate.Items.FindByValue(itemValue) != null)
						{
							string itemText = DDLStatusUpdate.Items.FindByValue(itemValue).Text;
							ListItem li = new ListItem();
							li.Text = itemText;
							li.Value = itemValue;
							DDLStatusUpdate.Items.Remove(li);
							trvl_accmo_btn.Visible = true;
						}
					}

					GVJoiningDetailInformation.DataSource = dsInterviewSchedule.Tables[1];
					GVJoiningDetailInformation.DataBind();
					GetecruitmentDetail();



				}
				//	trvldeatils_btnSave.Visible = false;
				Rec_AddjoinDetailbtn.Text = "-";
				DivJoiningDetails2.Visible = true;
				DivJoiningDetailInformation.Visible = true;
				txtRecruitercomment.Text = "";
				Txt_JoiningDate.Text = "";
				DDLStatusUpdate.SelectedValue = "";
				lstjoinband.SelectedValue = "0";
				lstJoinEmploymentType.SelectedValue = "0";
				txtcandjoindate.Text = "";
			}
			Response.Redirect("~/procs/Rec_RecruiterInbox.aspx?type=VRR");
			//Response.Redirect("~/procs/Requisition_Index.aspx");
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
			int StatusID = 0; string Result = "", A = "";
			if (Selected == "1")
			{
				StatusID = 10;
				Result = "Undergoing Joining Process";
				A = "is";
			}
			else
			{
				StatusID = 9;
				Result = "Backed Out";
				A = "has";
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
	protected void CheckClosedRequisition_CheckedChanged(object sender, EventArgs e)
	{
		if (CheckClosedRequisition.Checked == true)
		{
			DivViewrowWiseCandidateInformation.Visible = false;
			DivCandidateRoundHistory.Visible = false;
			DivJoiningDetails1.Visible = false;
			DivJoiningDetails2.Visible = false;
			DivJoiningDetailInformation.Visible = false;
			OfferhistoryS.Visible = false;
			GRDOfferHistory.DataSource = null;
			GRDOfferHistory.DataBind();

			//trvldeatils_btnSave.Visible = true;
			DivButton.Visible = true;


		}
		else
		{
			//trvldeatils_btnSave.Visible = false;
			DivButton.Visible = false;
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

	#region  All_Methods
	private void getApproverlist()
	{
		DataTable dtapprover = new DataTable();
		int Recruitment_ReqID = 0, CTCID = 0, Candidate_ID = 0;
		Recruitment_ReqID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
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
			if (DS.Tables[1].Rows[0]["CTCException"].ToString() != "")
			{
				Chk_Exception_CTC.Checked = Convert.ToBoolean(DS.Tables[1].Rows[0]["CTCException"].ToString());
				if (Chk_Exception_CTC.Checked)
				{
					txtRecruiterRemark.Text = DS.Tables[1].Rows[0]["RecruiterRemark"].ToString();
					ExceptionR.Visible = true;
					CTC1.Visible = true;
					getApproverlist();
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

			DDLJoinedemployee.DataSource = dtInterviewer;
			DDLJoinedemployee.DataTextField = "EmployeeName";
			DDLJoinedemployee.DataValueField = "EmployeeCode";
			DDLJoinedemployee.DataBind();
			DDLJoinedemployee.Items.Insert(0, new ListItem("Select Joined Employee", "0"));

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

	public void ClosetimeRequisitionHideShow()
	{
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "RecruitmentReq_ViewRecruiter";
		spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
		spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
		spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		DataSet dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");
		if (dsRecruitmentDetails.Tables[2].Rows[0]["MaxJoiningDateClose"].ToString() == "")
		{
			SpanJoinemployee.Visible = false;
			SpanCloseReqChk.Visible = false;
			SpanJoiningDate.Visible = false;
			//	trvldeatils_btnSave.Visible = false;
			JobDetail_btnSave.Visible = true;

		}
		else
		{
			if (Convert.ToDateTime(dsRecruitmentDetails.Tables[2].Rows[0]["MaxJoiningDateClose"]) <= Convert.ToDateTime(DateTime.Now.ToString()))
			{
				if (dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"].ToString() == "Closed")
				{
					SpanJoinemployee.Visible = true;
					SpanCloseReqChk.Visible = true;
					//	trvldeatils_btnSave.Visible = true;
					SpanJoiningDate.Visible = true;
					// JobDetail_btnSave.Visible = false;
				}
				else
				{
					SpanJoinemployee.Visible = false;
					SpanCloseReqChk.Visible = false;
					//	trvldeatils_btnSave.Visible = true;
					SpanJoiningDate.Visible = false;
					// JobDetail_btnSave.Visible = false;
				}

			}
			else
			{
				SpanJoinemployee.Visible = false;
				SpanCloseReqChk.Visible = false;
				SpanJoiningDate.Visible = false;
				//trvldeatils_btnSave.Visible = false;
				JobDetail_btnSave.Visible = true;
			}
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
				if (dsCandidateRoundInfo.Tables[5].Rows.Count > 0)
				{
					dsCVSource = spm.GetCVSource();
					DataTable dt = new DataTable();
					dt = dsCVSource.Tables[7];
					DataRow[] DRStatusUpdateMaxID = dsCandidateRoundInfo.Tables[5].Select("RecJobJoiningID = MAX(RecJobJoiningID)");
					string strStatusID = DRStatusUpdateMaxID[0][2].ToString();
					//for (int i = 0; i < dsCandidateRoundInfo.Tables[5].Rows.Count; i++)
					//{
					if (strStatusID == "6" || strStatusID == "7" || strStatusID == "8")
					{
						//ClosetimeRequisitionHideShow();
					}
					else
					{
						SpanJoinemployee.Visible = false;
						SpanCloseReqChk.Visible = false;
						SpanJoiningDate.Visible = false;
						//trvldeatils_btnSave.Visible = false;
						JobDetail_btnSave.Visible = true;
					}

					string strStatusUpdateID = strStatusID.Trim();

					for (int ii = dt.Rows.Count - 1; ii >= 0; ii--)
					{
						DataRow dr = dt.Rows[ii];
						if (strStatusUpdateID.Trim() == "1")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "2")
							{
								// dr.Delete();
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
								{
									dr.Delete();
								}
							}
						}
						if (strStatusUpdateID.Trim() == "2")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "10")
							{
								//dr.Delete();
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
								{
									dr.Delete();
								}
							}
						}
						if (strStatusUpdateID.Trim() == "13")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
							{
								dr.Delete();
							}
						}
						if (strStatusUpdateID.Trim() == "14")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
							{
								dr.Delete();
							}
						}

						if (strStatusUpdateID.Trim() == "10")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
							{
								//dr.Delete();
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11" && dr["StatusUpdate_ID"].ToString().Trim() != "4")
								{
									dr.Delete();
								}
							}
						}
						if (strStatusUpdateID.Trim() == "5")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "3")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "4")
								{
									// dr.Delete();
									if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
									{
										dr.Delete();
									}
								}
							}
						}
						if (strStatusUpdateID.Trim() == "3")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "12" && dr["StatusUpdate_ID"].ToString().Trim() != "4")
							{
								dr.Delete();
							}
						}
						if (strStatusUpdateID.Trim() == "12")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "7")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "9" && dr["StatusUpdate_ID"].ToString().Trim() != "4")
								{
									dr.Delete();
								}
							}
						}
						if (strStatusUpdateID.Trim() == "4")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "5")
							{
								// dr.Delete();
								if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
								{
									dr.Delete();
								}
							}
						}
						if (strStatusUpdateID.Trim() == "8" || strStatusUpdateID.Trim() == "6" || strStatusUpdateID.Trim() == "7")
						{
							//if (dr["StatusUpdate_ID"].ToString().Trim() != "6")
							//{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "7")
							{
								if (dr["StatusUpdate_ID"].ToString().Trim() != "9" && dr["StatusUpdate_ID"].ToString().Trim() != "4")
								{
									dr.Delete();
								}
							}
							//}
						}
						if (strStatusUpdateID.Trim() == "9")
						{
							dr.Delete();
						}
						if (strStatusUpdateID.Trim() == "11")
						{
							dr.Delete();
						}
						dt.AcceptChanges();
					}
					//   }

					DDLStatusUpdate.DataSource = dt;
					DDLStatusUpdate.DataTextField = "StatusUpdate";
					DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
					DDLStatusUpdate.DataBind();
					DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
					OfferApprovalStatus();
					string Result = "Negotiation Done";
					//Negotiation Done			
					if (dsCandidateRoundInfo.Tables[5].Rows.Count == 2)
					{
						DataRow[] dr2 = dsCandidateRoundInfo.Tables[5].Select("StatusUpdate='" + Result + "'");
						if (dr2.Length > 0 && hdnOfferStatus.Value != "Approved")
						{
							trvl_accmo_btn.Visible = true;
							//DDLStatusUpdate.Enabled = false;
							string itemValue = "10";
							if (DDLStatusUpdate.Items.FindByValue(itemValue) != null)
							{
								string itemText = DDLStatusUpdate.Items.FindByValue(itemValue).Text;
								ListItem li = new ListItem();
								li.Text = itemText;
								li.Value = itemValue;
								DDLStatusUpdate.Items.Remove(li);
							}
						}
					}
					else
					{
						//trvl_accmo_btn.Visible = false;
					}
					string OfferReject = "Offer Rejected";
					DataRow[] dr3 = dsCandidateRoundInfo.Tables[5].Select("StatusUpdate='" + OfferReject + "'");
					if (dr3.Length > 0 && hdnOfferStatus.Value != "Approved")
					{

						//DDLStatusUpdate.Enabled = false;
						string itemValue = "5";
						if (DDLStatusUpdate.Items.FindByValue(itemValue) != null)
						{
							string itemText = DDLStatusUpdate.Items.FindByValue(itemValue).Text;
							ListItem li = new ListItem();
							li.Text = itemText;
							li.Value = itemValue;
							DDLStatusUpdate.Items.Remove(li);
							trvl_accmo_btn.Visible = true;
						}
					}
					GVJoiningDetailInformation.DataSource = dsCandidateRoundInfo.Tables[5];
					GVJoiningDetailInformation.DataBind();
				}
				else
				{

					SpanJoinemployee.Visible = false;
					SpanCloseReqChk.Visible = false;
					SpanJoiningDate.Visible = false;
					//trvldeatils_btnSave.Visible = false;
					trvl_accmo_btn.Visible = false;

					dsCVSource = spm.GetCVSource();
					DataTable dt = new DataTable();
					dt = dsCVSource.Tables[7];
					for (int i = dt.Rows.Count - 1; i >= 0; i--)
					{
						// string StrStatusUpdate_ID = dt.Rows[i]["StatusUpdate_ID"].ToString();
						DataRow dr = dt.Rows[i];
						if (dr["StatusUpdate_ID"].ToString().Trim() != "1")
						{
							if (dr["StatusUpdate_ID"].ToString().Trim() != "11")
							{
								dr.Delete();
							}
						}
					}
					dt.AcceptChanges();
					DDLStatusUpdate.DataSource = dt;
					DDLStatusUpdate.DataTextField = "StatusUpdate";
					DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
					DDLStatusUpdate.DataBind();
					DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
					GVJoiningDetailInformation.DataSource = null;
					GVJoiningDetailInformation.DataBind();
				}

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
					//	trvldeatils_btnSave.Enabled = false;

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

			}


		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();

			throw;
		}

	}
	private void OfferApprovalStatus()
	{
		DataTable Offer = new DataTable();
		int RecrutID = 0, Candidate_ID = 0;
		string Result = "", values = "";
		RecrutID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
		Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
		Offer = spm.GetOffer_Approval_Status(RecrutID, Candidate_ID);
		if (Offer.Rows.Count > 0)
		{
			Result = (string)Offer.Rows[0]["Action"];
			values = Offer.Rows[0]["Offer_App_ID"].ToString();
			hdncandidateOffer.Value = Offer.Rows[0]["Candidate_Status"].ToString();
			hdnOfferAppID.Value = values;
			//hdnapprid.Value = dtOfferApproverEmailIds.Rows[0]["APPR_ID"].ToString();			
		}
		else
		{
			hdnOfferAppID.Value = "0";
		}
		if (Result == "Pending")
		{
			trvl_accmo_btn.Visible = false;
			mobile_btnSave.Visible = false;
		}
		if (Result == "Approved" && hdncandidateOffer.Value == "")
		{
			hdnOfferStatus.Value = Result;
			//DDLStatusUpdate.Enabled = true;
			mobile_btnSave.Visible = false;
			trvl_accmo_btn.Visible = false;
		}
		if (Result == "Reject")
		{
			mobile_btnSave.Visible = true;
			trvl_accmo_btn.Visible = true;
			hdnOfferAppID.Value = "0";
		}
		if (Result == "Approved" && hdncandidateOffer.Value == "Rejected")
		{
			//hdnOfferStatus.Value = Result;
			//DDLStatusUpdate.Enabled = true;
			mobile_btnSave.Visible = true;
			trvl_accmo_btn.Visible = true;
			hdnOfferAppID.Value = "0";
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

		DDLStatusUpdate.DataSource = dsCVSource.Tables[7];
		DDLStatusUpdate.DataTextField = "StatusUpdate";
		DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
		DDLStatusUpdate.DataBind();
		DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
	}

	private void getStatusUpdate()
	{
		dsCVSource = spm.GetCVSource();

		DDLStatusUpdate.DataSource = dsCVSource.Tables[7];
		DDLStatusUpdate.DataTextField = "StatusUpdate";
		DDLStatusUpdate.DataValueField = "StatusUpdate_ID";
		DDLStatusUpdate.DataBind();
		DDLStatusUpdate.Items.Insert(0, new ListItem("Select Status Update", ""));
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

			lstOfferPositionName.DataSource = dtPositionName;
			lstOfferPositionName.DataTextField = "PositionTitle";
			lstOfferPositionName.DataValueField = "PositionTitle_ID";
			lstOfferPositionName.DataBind();
			lstOfferPositionName.Items.Insert(0, new ListItem("Select Offer Position", "0"));

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
	public void GetCandidateEmployeeType()
	{
		DataTable dtEmployeeType = new DataTable();

		try
		{
			SqlParameter[] spars = new SqlParameter[1];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_Req_Candi_Employment_Type";
			//spars[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			//spars[1].Value = Convert.ToString(Session["Empcode"]);			
			dtEmployeeType = spm.getMobileRemDataList(spars, "SP_GETREQUISTIONLIST_DETAILS");
			if (dtEmployeeType.Rows.Count > 0)
			{
				lstJoinEmploymentType.DataSource = dtEmployeeType;
				lstJoinEmploymentType.DataTextField = "Particulars";
				lstJoinEmploymentType.DataValueField = "PID";
				lstJoinEmploymentType.DataBind();
				lstJoinEmploymentType.Items.Insert(0, new ListItem("Select Employee Type", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	public void GetOfferLocation()
	{
		DataTable dtLocation = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
			spars[0].Value = "Offer_company_Location";
			spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
			spars[1].Value = Convert.ToString(Session["Empcode"]);
			dtLocation = spm.getMobileRemDataList(spars, "sp_Offer_Approval_Details");
			if (dtLocation.Rows.Count > 0)
			{
				lstOfferLocation.DataSource = dtLocation;
				lstOfferLocation.DataTextField = "Location_name";
				lstOfferLocation.DataValueField = "comp_code";
				lstOfferLocation.DataBind();
				lstOfferLocation.Items.Insert(0, new ListItem("Select Offer Location", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
    public void GetOfferOfficeLocation()
    {
        DataTable dtLocation = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@mode", SqlDbType.VarChar);
            spars[0].Value = "Offer_Office_Location";
            spars[1] = new SqlParameter("@empCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dtLocation = spm.getMobileRemDataList(spars, "sp_Offer_Approval_Details");
            if (dtLocation.Rows.Count > 0)
            {
                lstOfferOfficeLocation.DataSource = dtLocation;
                lstOfferOfficeLocation.DataTextField = "locOffice_name";
                lstOfferOfficeLocation.DataValueField = "locoffice_Code";
                lstOfferOfficeLocation.DataBind();
                lstOfferOfficeLocation.Items.Insert(0, new ListItem("Select Offer Office Location", "0"));
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void GetCandidateEmployeeJoiningDate(string EmpCode)
	{
		DataTable dtEmployeeDate = new DataTable();

		try
		{
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "Get_EmployeeCandidateOnboard";
			spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			spars[1].Value = EmpCode;
			dtEmployeeDate = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
			if (dtEmployeeDate.Rows.Count > 0)
			{
				txtemployeeJoiningDate.Text = dtEmployeeDate.Rows[0]["emp_doj"].ToString();
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
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

			lstjoinband.DataSource = dtPositionBand;
			lstjoinband.DataTextField = "BAND";
			lstjoinband.DataValueField = "BAND";
			lstjoinband.DataBind();
			lstjoinband.Items.Insert(0, new ListItem("Select BAND", "0"));

			lstOfferBand.DataSource = dtPositionBand;
			lstOfferBand.DataTextField = "BAND";
			lstOfferBand.DataValueField = "BAND";
			lstOfferBand.DataBind();
			lstOfferBand.Items.Insert(0, new ListItem("Select Offer Band", "0"));


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
			DDLsearchRequisitionnumber.Items.Insert(0, new ListItem("Select Requisition Number", "0"));


			DataRow[] dr3 = DS.Tables[0].Select("Recruitment_ReqID='" + hdRecruitment_ReqID.Value + "'");
			if (dr3.Length > 0)
			{
				//  string itemValue = "4";
				if (DDLsearchRequisitionnumber.Items.FindByValue(hdRecruitment_ReqID.Value) != null)
				{
					string itemText = DDLsearchRequisitionnumber.Items.FindByValue(hdRecruitment_ReqID.Value).Text;
					ListItem li = new ListItem();
					li.Text = itemText;
					li.Value = hdRecruitment_ReqID.Value;
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
			dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				HFempcoderec.Value = dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code"].ToString();
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

				string StrStatusflag = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"]).Trim();
				HFRecruitmentStatus.Value = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RecruitmentStatus"]).Trim();
				if (StrStatusflag == "Closed")
				{
					Linkbtn_CandidateShortlisting.Enabled = false;
					Linkbtn_CandidateShortlisting.Visible = false;
					//trvldeatils_btnSave.Visible = false;
					JobDetail_btnSave.Visible = false;
					CheckClosedRequisition.Checked = true;
					CheckClosedRequisition.Enabled = false;
					DivJoiningDetails1.Visible = false;
					DivJoiningDetails2.Visible = false;
					if (dsRecruitmentDetails.Tables[3].Rows.Count > 0)
					{
						DDLJoinedemployee.SelectedValue = dsRecruitmentDetails.Tables[3].Rows[0]["empcode"].ToString();
						DDLJoinedemployee.Enabled = false;
						txtemployeeJoiningDate.Text = dsRecruitmentDetails.Tables[3].Rows[0]["CandJoiningDate"].ToString();
					}
					ClosetimeRequisitionHideShow();
				}
				if (dsRecruitmentDetails.Tables[4].Rows.Count > 0)
				{
					//trvldeatils_btnSave.Visible = false;
					JobDetail_btnSave.Visible = false;
					HFRecruitmentCancel.Value = "Cancel";
				}
				if (Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Status_ID"]).Trim() == "3" && StrStatusflag == "Cancelled")
				{
					//	trvldeatils_btnSave.Visible = false;
					JobDetail_btnSave.Visible = false;
					Linkbtn_CandidateShortlisting.Visible = false;
				}

			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	#endregion

	protected void DDLStatusUpdate_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (DDLStatusUpdate.SelectedValue == "4" || DDLStatusUpdate.SelectedValue == "9" || DDLStatusUpdate.SelectedValue == "11")
		{
			SpanRecruitercomment.Visible = true;
			LI1JoiningdetailCandidate.Visible = false;
			LI2JoiningdetailCandidate.Visible = false;
			LI3JoiningdetailCandidate.Visible = false;
			joiningEmpType.Visible = false;
			joinBand.Visible = false;
			JoiningDate.Visible = false;
			FileValidation.Visible = true;
			lstjoinband.SelectedIndex = -1;
			lstJoinEmploymentType.SelectedIndex = -1;
			txtcandjoindate.Text = "";
		}
		else
		{
			SpanRecruitercomment.Visible = false;
			if (DDLStatusUpdate.SelectedValue == "12")
			{
				LI1JoiningdetailCandidate.Visible = true;
				LI2JoiningdetailCandidate.Visible = true;
				LI3JoiningdetailCandidate.Visible = true;
				FileValidation.Visible = true;
			}
			else if (DDLStatusUpdate.SelectedValue == "5")
			{
				LI1JoiningdetailCandidate.Visible = true;
				LI2JoiningdetailCandidate.Visible = true;
				LI3JoiningdetailCandidate.Visible = true;
				FileValidation.Visible = false;
			}
			else if (DDLStatusUpdate.SelectedValue == "3")
			{
				LI1JoiningdetailCandidate.Visible = true;
				LI2JoiningdetailCandidate.Visible = true;
				LI3JoiningdetailCandidate.Visible = true;
				joiningEmpType.Visible = true;
				joinBand.Visible = true;
				JoiningDate.Visible = true;
				FileValidation.Visible = false;
			}
			else
			{
				LI1JoiningdetailCandidate.Visible = false;
				LI2JoiningdetailCandidate.Visible = false;
				LI3JoiningdetailCandidate.Visible = false;
				joiningEmpType.Visible = false;
				joinBand.Visible = false;
				JoiningDate.Visible = false;
			}
		}
	}

	protected void GVJoiningDetailInformation_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			ImageButton lnkIBAcceptanceFile = (e.Row.FindControl("lnkAcceptanceFile") as ImageButton);

			if (e.Row.Cells[2].Text.Trim() == "Resignation Submission /Acceptance")// ||  e.Row.Cells[2].Text.Trim() == "Offer Accepted")
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
		//if (OfferCreate.Visible)
		//{
		//	OfferCreate.Visible = false;
		//}
		//else
		//{
		ClearOffer();
		OfferCreate.Visible = true;
		//txtpositionOffer.Text = lstPositionName.SelectedItem.Text;
		GetOffer_Approverlist();
		//GetOffer_Approver_History_list();
		OfferCreatelist();
		Offer_Compensation_Candidate();
		//}
	}
	private void GetOffer_Approverlist()
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
		int RecrutID = 0, Offer_App_ID = 0;
		DataTable dtapprover = new DataTable();

		RecrutID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
		Offer_App_ID = Convert.ToString(hdnOfferAppID.Value).Trim() != "" ? Convert.ToInt32(hdnOfferAppID.Value) : 0;
		dtapprover = spm.GetRequisition_Offer_Approver_Status(Offer_App_ID, RecrutID, qtype, getcompSelectedval, Dept_id);
		DgvOfferApprover.DataSource = null;
		if (dtapprover.Rows.Count > 0)
		{
			DgvOfferApprover.DataSource = dtapprover;
			DgvOfferApprover.DataBind();
		}
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
	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			string[] strdate, strProbable, strProbableAccept;
            string strtoDate = "", Probable = "", Exception = "", ProbableAccept = "";
            DateTime Offerdate, ProbableDate, ProbableDateAccept;
			lblOffer.Text = "";
			int IsException = 0, PID = 0, PositionTitle_ID=0;
			decimal Amount1 = 0, Amount2 = 0;
			DataTable DtOfferCount = new DataTable();
			DtOfferCount = spm.Get_Generate_Offer_Count("Get_generate_Offer_Count", Convert.ToInt32(0), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());
			if (txtOfferDate.Text == "")
			{
				lblOffer.Text = "Please enter offer Date";
				return;
			}

			if (lstOfferPositionName.SelectedValue == "0" || lstOfferPositionName.SelectedValue == "")
			{
				lblOffer.Text = "Please select offer Position Name";
				return;
			}

			if (lstOfferLocation.SelectedValue == "0" || lstOfferLocation.SelectedValue == "")
			{
				lblOffer.Text = "Please select offer Position Location";
				return;
			}
            if (lstOfferOfficeLocation.SelectedValue == "0" || lstOfferOfficeLocation.SelectedValue == "") 
            {
                lblOffer.Text = "Please select offer Office Location";
                return;
            }

            if (lstOfferBand.SelectedValue == "0" || lstOfferBand.SelectedValue == "")
			{
				lblOffer.Text = "Please select offer Band";
				return;
			}

			if (ddl_Offer_EmploymentType.SelectedValue == "0" || ddl_Offer_EmploymentType.SelectedValue == "")
			{
				lblOffer.Text = "Please select Employment Type";
				return;
			}

			if (ddl_Offer_EmploymentType.SelectedValue == "1")
			{
				if (txt_Offer_Basic.Text == "" || txt_Offer_Basic.Text == "0")
				{
					lblOffer.Text = "Please enter offer Basic Amount";
					return;
				}
				if (DtOfferCount.Rows.Count == 0)
				{
					lblOffer.Text = "Please Fill Complete Generate Offer latter..!";
					return;
				}
			}
			if (ddl_Offer_EmploymentType.SelectedValue != "1")
			{
				if (DtOfferCount.Rows.Count > 0)
				{
					spm.Get_Generate_Offer_Count("Get_Temp_Offer_Pending_Delete", Convert.ToInt32(0), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]).Trim());
				}
			}
			if (txtOfferno1.Text.Trim() == "")
			{
				lblOffer.Text = "Please enter Total CTC Offered (LPA)";
				return;
			}
			if (txtOfferno2.Text.Trim() == "")
			{
				lblOffer.Text = "Please enter CTC as per Band Eligibility & Other Allowances (Max Amount) LP";
				return;
			}
			if (txtOfferno2.Text.Trim() != "" && txtOfferno1.Text.Trim() != "")
			{
				if (Convert.ToDecimal(txtOfferno2.Text) > Convert.ToDecimal(txtOfferno1.Text))
				{
					//lblOffer.Text = "Please enter New CTC (Lakh) greater than to Old CTC (Lakh)";
					//return;
				}
			}
			if (txtOfferAppcmt.Text.Trim() == "")
			{
				lblOffer.Text = "Please enter offer approval Comments";
				return;
			}
			if (txtProbableJoiningDate.Text.Trim() == "")
			{
				lblOffer.Text = "Please enter Probable Joining Date";
				return;
			}

			if (lstRecruitmentCharges.SelectedValue == "0" || lstRecruitmentCharges.SelectedValue == "")
			{
				lblOffer.Text = "Please select Recruitment Charges";
				return;
			}
			if (ddl_Offer_EmploymentType.SelectedValue != "1")
			{
				if (Convert.ToString(FileOfferUpload.FileName).Trim() == "")
				{
					lblOffer.Text = "Please upload Offer Approval Document ";
					return;
				}
			}
			if (Convert.ToString(FileOfferUpload.FileName).Trim() != "")
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
						//string strremoveSpace = hdCandidate_ID.Value + "_" + hdRecruitment_ReqID.Value + "_" + Dates + "_" + multiplefilename;
						strfileName = hdCandidate_ID.Value + "_" + hdRecruitment_ReqID.Value + "_" + Dates + "_" + i + Path.GetExtension(uploadfileName.FileName);
						//strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
						//strremoveSpace = Regex.Replace(strremoveSpace, @"[^0-9a-zA-Z\._]", "_");
						//strfileName = strremoveSpace;
						multiplefilename = strfileName;
						uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["OfferApprovalDocumentpath"]).Trim()), strfileName));
						multiplefilenameadd += strfileName + ",";
					}
				}
				multiplefilenameadd = multiplefilenameadd.TrimEnd(',');
			}
			if (chk_exception.Checked)
			{
				IsException = 1;
				Exception = "Yes";
			}
			else
			{
				IsException = 0;
				Exception = "No";
			}

			strdate = Convert.ToString(txtOfferDate.Text).Trim().Split('/');
			strtoDate = Convert.ToString(strdate[2]) + "-" + Convert.ToString(strdate[1]) + "-" + Convert.ToString(strdate[0]);
			Offerdate = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

			strProbable = Convert.ToString(txtProbableJoiningDate.Text).Trim().Split('/');
			Probable = Convert.ToString(strProbable[2]) + "-" + Convert.ToString(strProbable[1]) + "-" + Convert.ToString(strProbable[0]);
			ProbableDate = DateTime.ParseExact(Probable, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            if (TxtOfferAcceptanceByDate.Text == "")
            {
                var defaultDateTimeValue = default(DateTime?);
                ProbableDateAccept = Convert.ToDateTime(defaultDateTimeValue);
            }
            else
            {
                strProbableAccept = Convert.ToString(TxtOfferAcceptanceByDate.Text).Trim().Split('/');
                ProbableAccept = Convert.ToString(strProbableAccept[2]) + "-" + Convert.ToString(strProbableAccept[1]) + "-" + Convert.ToString(strProbableAccept[0]);
                ProbableDateAccept = DateTime.ParseExact(ProbableAccept, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }



            String strOfferApprovalURL = "", approveremailaddress = "", strApproverlist = "", AppName = "";
			int Offer_AppID = 0, apprid = 0;
			Amount1 = Convert.ToString(txtOfferno1.Text).Trim() != "" ? Convert.ToDecimal(txtOfferno1.Text) : 0;
			Amount2 = Convert.ToString(txtOfferno2.Text).Trim() != "" ? Convert.ToDecimal(txtOfferno2.Text) : 0;
			PID = string.IsNullOrEmpty(ddl_Offer_EmploymentType.SelectedValue) ? 0 : Convert.ToInt32(ddl_Offer_EmploymentType.SelectedValue);
			PositionTitle_ID = string.IsNullOrEmpty(lstOfferPositionName.SelectedValue) ? 0 : Convert.ToInt32(lstOfferPositionName.SelectedValue);
            //dtofferApproval = spm.InsertCandidate_Offer_Approval(Convert.ToString(Session["Empcode"]).Trim(), Offerdate, Convert.ToInt32(0), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), txtOfferAppcmt.Text.Trim(), multiplefilenameadd, Amount1, Amount2, IsException, lstOfferBand.SelectedValue.ToString(), lstRecruitmentCharges.SelectedValue.ToString(), ProbableDate);
            if (TxtOfferAcceptanceByDate.Text == "")
            {
                
                dtofferApproval = spm.InsertCandidate_Offer_Approval_New(Convert.ToString(Session["Empcode"]).Trim(), Offerdate, Convert.ToInt32(0), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), txtOfferAppcmt.Text.Trim(), multiplefilenameadd, Amount1, Amount2, IsException, lstOfferBand.SelectedValue.ToString(), lstRecruitmentCharges.SelectedValue.ToString(), ProbableDate, PID, PositionTitle_ID, Convert.ToString(lstOfferLocation.SelectedValue), Convert.ToString(lstOfferOfficeLocation.SelectedValue), ProbableDateAccept, "0");

            }
            else
            {
                dtofferApproval = spm.InsertCandidate_Offer_Approval_New(Convert.ToString(Session["Empcode"]).Trim(), Offerdate, Convert.ToInt32(0), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), txtOfferAppcmt.Text.Trim(), multiplefilenameadd, Amount1, Amount2, IsException, lstOfferBand.SelectedValue.ToString(), lstRecruitmentCharges.SelectedValue.ToString(), ProbableDate, PID, PositionTitle_ID, Convert.ToString(lstOfferLocation.SelectedValue), Convert.ToString(lstOfferOfficeLocation.SelectedValue), ProbableDateAccept,"1");
            }
            if (dtofferApproval.Rows.Count > 0)
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
				Offer_AppID = Convert.ToInt32(dtofferApproval.Rows[0]["Offer_App_ID"]);
				hdnOfferAppID.Value = dtofferApproval.Rows[0]["Offer_App_ID"].ToString();
				dtOfferApproverEmailIds = spm.GetRequisition_Offer_Approver_Status(Offer_AppID, Convert.ToInt32(hdRecruitment_ReqID.Value), qtype, getcompSelectedval, Dept_id);
				if (dtOfferApproverEmailIds.Rows.Count > 0)
				{
					approveremailaddress = (string)dtOfferApproverEmailIds.Rows[0]["Emp_Emailaddress"];
					hdnapprid.Value = dtOfferApproverEmailIds.Rows[0]["APPR_ID"].ToString();
					hdnofferappcode.Value = (string)dtOfferApproverEmailIds.Rows[0]["A_EMP_CODE"];
					AppName = (string)dtOfferApproverEmailIds.Rows[0]["Emp_Name"];
				}
				spm.Insert_Req_Offer_Approver_Request(hdnofferappcode.Value, Convert.ToInt32(hdnapprid.Value), Offer_AppID);

				strApproverlist = GetReq_offer_Approve_RejectList(Offer_AppID, Convert.ToInt32(hdRecruitment_ReqID.Value));
				strOfferApprovalURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Approval"]).Trim() + "?Offer_App_ID=" + Offer_AppID + "&Rec_ID=" + hdRecruitment_ReqID.Value;
				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();
				getLWPML_HR_ApproverCode();
				string StrCandidateNameandSkillSet = txtName.Text.Trim() + " - " + DDLmainSkillSet.SelectedItem.ToString();
				string ProbableJoiningDate = Convert.ToString(txtProbableJoiningDate.Text).Trim() != "" ? Convert.ToString(txtProbableJoiningDate.Text.Replace("/", "-")) : "";
				//spm.send_mailto_Req_Requisition_Approver(txtReqName.Text, txtReqEmail.Text, hdnApproverid_LWPPLEmail.Value, "Recruitment - Request for " + Convert.ToString(txtReqNumber.Text), lstSkillset.SelectedItem.Text + " - " + lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtComments.Text, txtNoofPosition.Text, strHREmailForCC, strLeaveRstURL, strApproverlist, strInsertmediaterlist);
				spm.send_mailto_Req_Requisition_Offer_Approver(StrCandidateNameandSkillSet, hdnLoginUserName.Value, AppName, hdnLoginEmpEmail.Value, approveremailaddress, "Recruitment - Request for Offer Approval " + Convert.ToString(txtReqNumber.Text), txtReqName.Text.Trim(), txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, txtName.Text.Trim(), lstPositionBand.SelectedItem.Text, txtOfferAppcmt.Text.Trim(), hdnFinalizedDate.Value, hdnExtraApproverEmail.Value, strOfferApprovalURL, strApproverlist, "", txtOfferno1.Text.Trim(), txtOfferno2.Text.Trim(), Exception, lstOfferBand.SelectedValue.ToString(), txtExceptionamt.Text.Trim(), lstRecruitmentCharges.SelectedValue.ToString(), ProbableJoiningDate, lstOfferPositionName.SelectedItem.Text, lstOfferLocation.SelectedItem.Text);
				ClearOffer();
				GetOffer_Approverlist();

				Response.Redirect("~/procs/Rec_RecruiterInbox.aspx?type=VRR");
			}
			else
			{
				lblOffer.Text = "Please check record already exists";
				return;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void ClearOffer()
	{
		txtOfferAppcmt.Text = "";
		txtOfferDate.Text = "";
		txtOfferno1.Text = "";
		txtOfferno2.Text = "";
		//txtpositionOffer.Text = "";
		lstOfferBand.SelectedIndex = -1;
		lstOfferLocation.SelectedIndex = -1;
        lstOfferOfficeLocation.SelectedIndex = -1;
        lstOfferPositionName.SelectedIndex = -1;
		ddl_Offer_EmploymentType.SelectedIndex = -1;
		txtExceptionamt.Text = "";
		txtOfferAppcmt.Text = "";
		txtProbableJoiningDate.Text = "";
        TxtOfferAcceptanceByDate.Text = ""; 
        lstRecruitmentCharges.SelectedIndex = -1;
		chk_exception.Checked = false;
		txtOfferDate.Enabled = true;
		//txtpositionOffer.Enabled = false;
		lstOfferPositionName.Enabled = true;
		lstOfferBand.Enabled = true;
		lstOfferLocation.Enabled = true;
        lstOfferOfficeLocation.Enabled = true;
        ddl_Offer_EmploymentType.Enabled = true;
		txtOfferno1.Enabled = true;
		txtExceptionamt.Enabled = false;
		txtOfferno2.Enabled = true;
		txtOfferAppcmt.Enabled = true;
		txtProbableJoiningDate.Enabled = true;
        TxtOfferAcceptanceByDate.Enabled = true;
		lstRecruitmentCharges.Enabled = true;
		chk_exception.Enabled = true;
		lblOfferCreate.Text = "Offer Create";
		mobile_btnSave.Style.Add("display", "inline-block");
	}
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

	protected void DDLJoinedemployee_SelectedIndexChanged(object sender, EventArgs e)
	{
		txtemployeeJoiningDate.Text = "";
		if (DDLJoinedemployee.SelectedIndex > 0)
		{
			string EmpCode = DDLJoinedemployee.SelectedValue.ToString();
			GetCandidateEmployeeJoiningDate(EmpCode);
		}
	}

	protected void Link_BtnTransferCandidate_Click(object sender, EventArgs e)
	{
		try
		{
			lblmessage.Text = "";

			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			if (DDLsearchRequisitionnumber.SelectedValue == "0" || DDLsearchRequisitionnumber.SelectedValue == "")
			{
				lblmessage.Text = "Please select requisition number";
				return;
			}

			if (lblmessage.Text == "")
			{
				SqlParameter[] spars = new SqlParameter[6];
				DataSet DS = new DataSet();
				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "Rec_TransferCandidate";
				spars[1] = new SqlParameter("@CurrentRecruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
				spars[2] = new SqlParameter("@CandidateID", SqlDbType.Int);
				spars[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
				spars[3] = new SqlParameter("@NewRecruitment_ReqID", SqlDbType.Int);
				spars[3].Value = Convert.ToInt32(DDLsearchRequisitionnumber.SelectedValue);
				spars[4] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
				spars[4].Value = Session["Empcode"].ToString();
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
								StrCCmail = StrCCmail + ";" + DS.Tables[1].Rows[0]["Emp_EmailaddressInterviewSheduler"].ToString();
							}
						}
						string RequiredByDate = "";
						RequiredByDate = GetRequiredByDate();
						spm.send_mailto_TransferredRequisitionNo(txtReqName.Text, StrFrommail, StrTomail, StrCCmail, mailsubject, StrName, txtName.Text, txtReqNumber.Text, DDLsearchRequisitionnumber.SelectedItem.Text.Trim(), mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrNewRecDepartment_Name, StrNewReccompany_name, StrNewRecModuleDesc, StrNewRecBAND, StrNewRecPositionTitle);
					}

					Response.Redirect("~/procs/Rec_RecruiterInbox.aspx?type=VRR");
				}
				else
				{
					lblmessage.Text = "Can not transfer Requisition.Already transfered same candidate";
				}


			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}


	protected void txtOfferno1_TextChanged(object sender, EventArgs e)
	{
		if (txtOfferno1.Text != "" && txtOfferno2.Text != "")
		{
			decimal result = Convert.ToDecimal(txtOfferno1.Text) - Convert.ToDecimal(txtOfferno2.Text);
			txtExceptionamt.Text = Convert.ToString(result.ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")));
		}
		else
		{
			txtExceptionamt.Text = "";
		}
	}

	protected void Link_TransferHideUnhide_Click(object sender, EventArgs e)
	{
		if (Link_TransferHideUnhide.Text.Trim() == "+")
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
		}
		else if (Link_TransferHideUnhide.Text.Trim() == "-")
		{
			Link_TransferHideUnhide.Text = "+";
			CandidateTransferfalse();
			DIV_TransferCanInfo1.Visible = true;
			DIV_TransferCanInfo2.Visible = true;
			DIV_TransferCanInfo3.Visible = true;
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

			DataRow[] dr3 = DS.Tables[0].Select("Recruitment_ReqID='" + hdRecruitment_ReqID.Value + "'");
			if (dr3.Length > 0)
			{ //  string itemValue = "4";
				if (DDLsearchRequisitionnumber.Items.FindByValue(hdRecruitment_ReqID.Value) != null)
				{
					string itemText = DDLsearchRequisitionnumber.Items.FindByValue(hdRecruitment_ReqID.Value).Text;
					ListItem li = new ListItem();
					li.Text = itemText;
					li.Value = hdRecruitment_ReqID.Value;
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
			Span2.Visible = true;
			GRDOfferCreatelist.DataSource = dsOffercreate.Tables[0];
			GRDOfferCreatelist.DataBind();

		}
	}

	protected void lnnOfferedit_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			int Offer_App_ID = Convert.ToInt32(GRDOfferCreatelist.DataKeys[row.RowIndex].Values[0]);
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
				mobile_btnSave.Style.Add("display", "none");
				txtOfferDate.Text = dsOffercreate.Tables[0].Rows[0]["Offer_Date"].ToString();
				lstOfferBand.SelectedValue = dsOffercreate.Tables[0].Rows[0]["OfferBAND"].ToString();
				//txtpositionOffer.Text = dsOffercreate.Tables[0].Rows[0]["PositionTitle"].ToString();
				lstOfferPositionName.SelectedValue = dsOffercreate.Tables[0].Rows[0]["PositionTitle_ID"].ToString();
				txtOfferno1.Text =Convert.ToDecimal(dsOffercreate.Tables[0].Rows[0]["OldCTC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txtOfferno2.Text = Convert.ToDecimal(dsOffercreate.Tables[0].Rows[0]["NewCTC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txtExceptionamt.Text =Convert.ToDecimal(dsOffercreate.Tables[0].Rows[0]["ExceptionAmount"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txtOfferAppcmt.Text = dsOffercreate.Tables[0].Rows[0]["Comment"].ToString();
				txtProbableJoiningDate.Text = dsOffercreate.Tables[0].Rows[0]["ProbableJoiningDate"].ToString();
                TxtOfferAcceptanceByDate.Text = dsOffercreate.Tables[0].Rows[0]["OfferAcceptanceByDate"].ToString(); 
                lstRecruitmentCharges.Text = dsOffercreate.Tables[0].Rows[0]["RecruitmentCharges"].ToString();
				if (dsOffercreate.Tables[0].Rows[0]["PID"].ToString() != "")
				{
					ddl_Offer_EmploymentType.SelectedValue = dsOffercreate.Tables[0].Rows[0]["PID"].ToString();
				}
				if (dsOffercreate.Tables[0].Rows[0]["comp_code"].ToString() != "")
				{
					lstOfferLocation.SelectedValue = dsOffercreate.Tables[0].Rows[0]["comp_code"].ToString();
				}
				else
				{
					lstOfferLocation.SelectedIndex = -1;
				}
                if (dsOffercreate.Tables[0].Rows[0]["locoffice_Code"].ToString() != "")
                {
                    lstOfferOfficeLocation.SelectedValue = dsOffercreate.Tables[0].Rows[0]["locoffice_Code"].ToString();
                }
                else
                {
                    lstOfferOfficeLocation.SelectedIndex = -1; 
                }


                if (dsOffercreate.Tables[0].Rows[0]["IsException"].ToString() == "No")
				{
					chk_exception.Checked = false;
				}
				else
				{
					chk_exception.Checked = true;
				}
				lblOfferCreate.Text = "Offer Edit";
				txtOfferDate.Enabled = false;
				lstOfferPositionName.Enabled = false;
				lstOfferBand.Enabled = false;
				lstOfferLocation.Enabled = false;
                lstOfferOfficeLocation.Enabled = false;
				ddl_Offer_EmploymentType.Enabled = false;
				txtOfferno1.Enabled = false;
				txtExceptionamt.Enabled = false;
				txtOfferno2.Enabled = false;
				txtOfferAppcmt.Enabled = false;
				txtProbableJoiningDate.Enabled = false;
                TxtOfferAcceptanceByDate.Enabled = false;
                lstRecruitmentCharges.Enabled = false; 
				chk_exception.Enabled = false;
				OfferhistoryS.Visible = true;

				GRDOfferHistory.DataSource = dsOffercreate.Tables[1];
				GRDOfferHistory.DataBind();

				GRDGenerate_Offer.DataSource = null;
				GRDGenerate_Offer.DataBind();
				if (dsOffercreate.Tables[2].Rows.Count > 0)
				{
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
					if (dsOffercreate.Tables[2].Rows[0]["Candidate_Status"].ToString() == "Accepted")
					{
						GRDGenerate_Offer.Columns[6].Visible = true;
					}
					else
					{
						GRDGenerate_Offer.Columns[6].Visible = false;
					}
					GRDGenerate_Offer.DataSource = dsOffercreate.Tables[2];
					GRDGenerate_Offer.DataBind();
					btn_Generate_Offer.Style.Add("display", "none");
                    btn_ViewDraft_Offer.Style.Add("display", "none");

                }
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}
	public void getLWPML_HR_ApproverCode()
	{

		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "LWPML_HREmpCode";
		spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			hdnExtraApproverEmail.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
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

	#region ELC_CandidateLoginDetail

	private void InsertCandidateLoginDetail_ELC()
	{
		try
		{
			DataSet DSCandidateLoginDetail = new DataSet();

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
			spars[0].Value = "Insert";
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
					foreach (DataRow item in DSCandidateLoginDetail.Tables[1].Rows)
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
				}
				string mailsubject = "";
				string mailcontain = "";
				mailsubject = "Candidate Login Detail";
				mailcontain = "Please follow these instructions for logging in to the system";
				string strCandidateLoginURL = Convert.ToString(ConfigurationManager.AppSettings["Link_CandidateLogin"]).Trim();
				spm.send_mailto_CandidateLoginDetail_ELC(StrCanName.Trim(), txtEmail.Text.Trim(), txtEmail.Text.Trim(), mailsubject, Candidatemailpwd.ToString(), mailcontain, strCandidateLoginURL, emailCC);

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

	#endregion
	#region Generate Offer Latter  19-08-22
	private void Get_Offer_Compensation_Parameters(int EmploymentTypeID, string Band)
	{
		try
		{
			DataSet Result = new DataSet();
			Result = spm.Get_Offer_Compensation_Parameters("Get_Offer_Compensation_Parameters", Session["Empcode"].ToString(), EmploymentTypeID, Band);
			if (Result.Tables[0].Rows.Count > 0)
			{
				SP_HRA.InnerText = Result.Tables[0].Rows[0]["Result"].ToString();
				hdn_HRA_Per.Value = Result.Tables[0].Rows[0]["PercentageAmt"].ToString();
				hdn_HRA_StructureID.Value = Result.Tables[0].Rows[0]["StructureID"].ToString();
			}
			else
			{
				SP_HRA.InnerText = string.Empty;
				hdn_HRA_Per.Value = string.Empty;
				hdn_HRA_StructureID.Value = string.Empty;
			}
			if (Result.Tables[1].Rows.Count > 0)
			{

				SP_Superannucation_All.InnerText = Result.Tables[1].Rows[0]["Result"].ToString();
				hdn_Superan_All.Value = Result.Tables[1].Rows[0]["PercentageAmt"].ToString();
				hdn_Superan_All_StructureID.Value = Result.Tables[1].Rows[0]["StructureID"].ToString();
			}
			else
			{
				SP_Superannucation_All.InnerText = string.Empty;
				hdn_Superan_All.Value = string.Empty;
				hdn_Superan_All_StructureID.Value = string.Empty;
			}
			if (Result.Tables[2].Rows.Count > 0)
			{
				SP_LTA.InnerText = Result.Tables[2].Rows[0]["Result"].ToString();
				hdn_LTA.Value = Result.Tables[2].Rows[0]["PercentageAmt"].ToString();
				hdn_LTA_StructureID.Value = Result.Tables[2].Rows[0]["StructureID"].ToString();
			}
			else
			{
				SP_LTA.InnerText = string.Empty;
				hdn_LTA.Value = string.Empty;
				hdn_LTA_StructureID.Value = string.Empty;
			}
			if (Result.Tables[3].Rows.Count > 0)
			{
				SP_PF.InnerText = Result.Tables[3].Rows[0]["Result"].ToString();
				hdn_PF.Value = Result.Tables[3].Rows[0]["PercentageAmt"].ToString();
				hdn_PF_StructureID.Value = Result.Tables[3].Rows[0]["StructureID"].ToString();
			}
			else
			{
				SP_PF.InnerText = string.Empty;
				hdn_PF.Value = string.Empty;
				hdn_PF_StructureID.Value = string.Empty;
			}

			if (Result.Tables[4].Rows.Count > 0)
			{
				SP_Gratuity.InnerText = Result.Tables[4].Rows[0]["Result"].ToString();
				hdn_Gratuity.Value = Result.Tables[4].Rows[0]["PercentageAmt"].ToString();
				hdn_Gratuity_StructureID.Value = Result.Tables[4].Rows[0]["StructureID"].ToString();
			}
			else
			{
				SP_Gratuity.InnerText = string.Empty;
				hdn_Gratuity.Value = string.Empty;
				hdn_Gratuity_StructureID.Value = string.Empty;
			}
			if (Result.Tables[5].Rows.Count > 0)
			{
				hdn_band.Value = Result.Tables[5].Rows[0]["BAND"].ToString();
				hdn_Skill_ALL_StructureID.Value = Result.Tables[5].Rows[0]["StructureID"].ToString();
				if (!string.IsNullOrEmpty(Result.Tables[5].Rows[0]["SkillAmount"].ToString()))
				{
					txt_offer_Skill_Allowance.Text = Convert.ToDecimal(Result.Tables[5].Rows[0]["SkillAmount"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
			}
			else
			{
				hdn_band.Value = string.Empty;
				hdn_Skill_ALL_StructureID.Value = string.Empty;
				txt_offer_Skill_Allowance.Text = string.Empty;
			}

        }
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

    private void GenerateViewDraftOffer()
    {
        try
        {
            Offer_Compensation_Candidate();
            int TempLatterID = Convert.ToInt32(HDnOfferDrftCopy.Value);
            int Offer_App_ID = 0;
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
               // this.ModalPopupExtenderGenerateOfferDraft.Show();
                txt_Offer_band.Text = lstOfferBand.SelectedItem.Text;
                txt_Offer_Designationdraft.Text = lstOfferPositionName.SelectedItem.Text;

                txtOffer_Can_Namedraft.Text = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString(); 
                txt_Offer_Location.Text = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
                SPOffer_Can_Address.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
                SP_Offer_Address2.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
                SPOffer_CandidateName.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();
                SP_candidate_Name1.InnerHtml = SPOffer_CandidateName.InnerHtml;
                SP_Candidate_Name4.InnerHtml = SPOffer_CandidateName.InnerHtml;

                if (dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString() == "")
                {
                    SPOffer_latterNo.InnerHtml = "HRD/HBT/OL/XXX";
                    SP_OfferNo2.InnerHtml = "HRD/HBT/OL/XXX";
                }
                else
                {
                    SPOffer_latterNo.InnerHtml = dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString();
                    SP_OfferNo2.InnerHtml = dsOffer.Tables[0].Rows[0]["OfferLetterNo"].ToString();
                }
                SP_Design.InnerHtml = lstOfferPositionName.SelectedItem.Text; 
                SP_Band.InnerHtml = lstOfferBand.SelectedItem.Text; 

                SP_Greetings.InnerHtml = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString(); 
                SP_ApprovalDate.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString(); 
                SP_ApprovalDate1.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
                SP_ApprovalDate3.InnerHtml = dsOffer.Tables[0].Rows[0]["ApprovalDate"].ToString();
                SP_Candidate_Name2.InnerHtml = SPOffer_CandidateName.InnerHtml;
                txt_Offer_BasicDraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["BASIC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                txt_Offer_HRAdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["HRA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));

                

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
                    TSpecialdraft.Visible = true;  
                    adhoc.Visible = false;
                }
                else
                {
                    TSpecialdraft.Visible = false;
                    adhoc.Visible = true;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CONVEYANCE"].ToString()))
                {
                    txt_Offer_Conveyancedraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CONVEYANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TConveyancedraft.Visible = true;  
                }
                else
                {
                    TConveyancedraft.Visible = false;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ADHOC_ALLOWANCE"].ToString()))
                {
                    txt_Offer_ADHOC_Allowancedraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ADHOC_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TADHOCdraft.Visible = true;  
                }
                else
                {
                    TADHOCdraft.Visible = false;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SKILL_ALLOWANCE"].ToString()))
                {
                    txt_offer_Skill_Allowancedraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TSkilldraft.Visible = true;  
                }
                else
                {
                    TSkilldraft.Visible = false;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SUPERAN_ALLOWANCE"].ToString()))
                {
                    txt_Offer_Superannucationdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SUPERAN_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TSuperannucationdraft.Visible = true;  
                }
                else
                {
                    TSuperannucationdraft.Visible = false;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()))
                {
                    txt_Offer_Certificate_Alldraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TCertificatedraft.Visible = true; 
                }
                else
                {
                    TCertificatedraft.Visible = false;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()))
                {
                    txt_Offer_Multi_Skill_Alldraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    Tr1draft.Visible = true; 
                }
                else
                {
                    Tr1draft.Visible = false;
                    t1 = --t1;
                }

                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()))
                {
                    txt_Offer_Additional_Skilldraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    Tr2draft.Visible = true;
                }
                else
                {
                    Tr2draft.Visible = false;
                    t1 = --t1;
                }

                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CAR_ALLOWANCE"].ToString()))
                {
                    txtCar_Alldraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CAR_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    Tr3draft.Visible = true;
                }
                else
                {
                    Tr3draft.Visible = false;
                    t1 = --t1;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["FOOD_ALLOWANCE"].ToString()))
                {
                    txt__Offer_Food_Alldraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["FOOD_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    Tr4draft.Visible = true;
                }
                else
                {
                    Tr4draft.Visible = false;
                    t1 = --t1;
                }


                txt_Offer_Total1draft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL1"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                tb1.RowSpan = t1;
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["LTA"].ToString()))
                {
                    txt_Offer_LTAdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["LTA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MEDICAL"].ToString()))
                {
                    txt_offer_Medicaldraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MEDICAL"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TMedical.Visible = true;
                }
                else
                {
                    TMedical.Visible = false;
                    t2 = --t2;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["DRIVER_SALARY"].ToString()))
                {
                    txt_Offer_Driver_Salarydraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["DRIVER_SALARY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TDriver.Visible = true;
                }
                else
                {
                    TDriver.Visible = false;
                    t2 = --t2;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CAR_LEASE"].ToString()))
                {
                    txt_Offer_Car_leasedraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CAR_LEASE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    TCar.Visible = true;
                }
                else
                {
                    TCar.Visible = false;
                    t2 = --t2;
                }
                txt_Offer_Total2draft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL2"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                tb2.RowSpan = t2;
                txt_Offer_PFdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["PF"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["GRATUITY_B"].ToString()))
                {
                    txt_Offer_Gratuitydraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["GRATUITY_B"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                }
                txt_Offer_Total3draft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL3"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                txt_Offer_Mediclaimdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MEDICLAIM"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                txt_Offer_Group_Policydraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["GROUP_ACC_POLICY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));

                txtTotal4draft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL4"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                txt_Offer_CTC_Monthdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_MONTH"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                txt_Offer_CTC_Annumdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()))
                {
                    txt_Offer_PLP_variabledraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    txt_Offer_PLP_Perdraft.Text = dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_Percentage"].ToString();
                    TPLP.Visible = true;
                    Tr9draft.Visible = true;
                }
                else
                {
                    TPLP.Visible = false;
                    Tr9draft.Visible = false;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()))
                {
                    txt_Offer_CTC_Annum_Incl_PLPdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    Tr5draft.Visible = true;
                }
                else
                {
                    Tr5draft.Visible = false;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ANNUAL_BONUS"].ToString()))
                {
                    txt_Annual_Bonusdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ANNUAL_BONUS"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    Tr7draft.Visible = true;
                }
                else
                {
                    Tr7draft.Visible = false;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["OTHER"].ToString()))
                {
                    txt_Offer_Otherdraft.Text = dsOffer.Tables[0].Rows[0]["OTHER"].ToString();
                    Tr8draft.Visible = true;
                }
                else
                {
                    Tr8draft.Visible = false;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["JOINING_BONUS"].ToString()))
                {
                    txt_Retention_Bonusdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["JOINING_BONUS"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    //txt_Retention_Remark.Text = Convert.ToString(dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString());
                    TDJoiningRemark.InnerHtml = "d - " + dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString();
                    TRJoining.Visible = true;
                    Tr6draft.Visible = true;
                }
                else
                {
                    Tr6draft.Visible = false;
                }
                if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ALP_ALLOWANCE"].ToString()))
                {
                    txt_ALP_Amountdraft.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ALP_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
                    //txt_ALP_Remark.Text = Convert.ToString(dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString());
                    TDALPRemark.InnerHtml = "e - " + dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString();
                    TRALP.Visible = true;
                    Tr10draft.Visible = true;
                }
                else
                {
                    Tr10draft.Visible = false;
                }

                SP_HRAdraft.InnerText = dsOffer.Tables[0].Rows[0]["RHR"].ToString();
                SP_Superannucation_Alldraft.InnerText = dsOffer.Tables[0].Rows[0]["RSUPERAN"].ToString();
                SP_LTAdraft.InnerText = dsOffer.Tables[0].Rows[0]["RLTA"].ToString();
                SP_PFdraft.InnerText = dsOffer.Tables[0].Rows[0]["RPF"].ToString();
                SP_Gratuitydraft.InnerText = dsOffer.Tables[0].Rows[0]["RGRA"].ToString();

                if (dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString()== "0" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() =="0.00")
                {
                    TD_HEALTHCHECKUPDraft1.Visible = false;
                    TD_HEALTHCHECKUPDraft2.Visible = false;
                    TD_HEALTHCHECKUPDraft3.Visible = false;
                }
                else
                {
                    txt_Offer_Health_CheckupDraft.Text = dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString();
                    TD_HEALTHCHECKUPDraft1.Visible = true;
                    TD_HEALTHCHECKUPDraft2.Visible = true;
                    TD_HEALTHCHECKUPDraft3.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0.00")
                {
                    Tr_CarHireCostDraft.Visible = false;
                }
                else
                {
                    txt_offer_Car_Hire_CostDraft.Text = dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString();
                    Tr_CarHireCostDraft.Visible = true;
                }


                if (dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "0.00")
                {
                    Tr_CarExpensesReimbursementDraft.Visible = false;
                }
                else
                {
                    txt_Offer_Car_Expenses_ReimbursmentDraft.Text = dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString();
                    Tr_CarExpensesReimbursementDraft.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "0.00")
                {
                    Tr_CarFuelExpencesReimbursementDraft.Visible = false;
                }
                else
                {
                    txt_Car_Fuel_Expenses_ReimbursmentDraft.Text = dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString();
                    Tr_CarFuelExpencesReimbursementDraft.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "0.00")
                {
                    Tr_Total5Draft.Visible = false;
                }
                else
                {
                    txtTotal5Draft.Text = dsOffer.Tables[0].Rows[0]["Total5"].ToString();
                    Tr_Total5Draft.Visible = true;
                }

            }

            //SP_Recruitment_Head.InnerHtml = dsOffer.Tables[1].Rows[0]["RecruitmentHead"].ToString();
            //SP_Recruitment2.InnerHtml = SP_Recruitment_Head.InnerHtml;
            //SP_Recruitment3.InnerHtml = SP_Recruitment_Head.InnerHtml;
            //SP_Recruiter_Name.InnerHtml = dsOffer.Tables[1].Rows[0]["RecruiterName"].ToString();
            //SP_Design.InnerHtml = dsOffer.Tables[1].Rows[0]["PositionTitle"].ToString();
            //SP_Band.InnerHtml = dsOffer.Tables[1].Rows[0]["OfferBAND"].ToString();
            txtOffer_Position_Location.Text = lstOfferLocation.SelectedItem.Text;

            SP_Location.InnerHtml = lstOfferOfficeLocation.SelectedItem.Text;

            
                SPOffer_Date.InnerHtml = "DateXXX";
                SP_Offer_Date2.InnerHtml = "DateXXX";
                SP_Offer_Date3.InnerHtml = SPOffer_Date.InnerHtml;
            
            SP_JoiningDate.InnerHtml = txtProbableJoiningDate.Text.Trim();
            SP_SP_JoiningDate1.InnerHtml = txtProbableJoiningDate.Text.Trim();
            SP_JoiningDate2.InnerHtml = SP_JoiningDate.InnerHtml;

            SP_Candidate_Jo.InnerHtml = SP_JoiningDate.InnerHtml + " " + SPOffer_CandidateName.InnerHtml;
            SP_Greetings2.InnerHtml = SPOffer_CandidateName.InnerHtml;

            //if (dsOffer.Tables[1].Rows.Count > 0)
            //{

            //    SP_Recruitment_Head.InnerHtml = dsOffer.Tables[1].Rows[0]["RecruitmentHead"].ToString();
            //    SP_Recruitment2.InnerHtml = SP_Recruitment_Head.InnerHtml;
            //    SP_Recruitment3.InnerHtml = SP_Recruitment_Head.InnerHtml;
            //    SP_Recruiter_Name.InnerHtml = dsOffer.Tables[1].Rows[0]["RecruiterName"].ToString();
            //    SP_Design.InnerHtml = dsOffer.Tables[1].Rows[0]["PositionTitle"].ToString();
            //    SP_Band.InnerHtml = dsOffer.Tables[1].Rows[0]["OfferBAND"].ToString();
            //    txtOffer_Position_Location.Text = lstOfferLocation.SelectedItem.Text;

            //    SP_Location.InnerHtml = lstOfferOfficeLocation.SelectedItem.Text;

            //    if (dsOffer.Tables[1].Rows[0]["Offer_Date"].ToString() == "")
            //    {
            //        SPOffer_Date.InnerHtml = "Offer Latter Date Display";
            //        SP_Offer_Date2.InnerHtml = "Offer Latter Date Display";
            //        SP_Offer_Date3.InnerHtml = SPOffer_Date.InnerHtml;
            //    }
            //    else
            //    {
            //        SPOffer_Date.InnerHtml = dsOffer.Tables[1].Rows[0]["Offer_Date"].ToString();
            //        SP_Offer_Date2.InnerHtml = dsOffer.Tables[1].Rows[0]["Offer_Date"].ToString();
            //        SP_Offer_Date3.InnerHtml = SPOffer_Date.InnerHtml;
            //    }
            //    SP_JoiningDate.InnerHtml = txtProbableJoiningDate.Text.Trim();
            //    SP_SP_JoiningDate1.InnerHtml = txtProbableJoiningDate.Text.Trim();
            //    SP_JoiningDate2.InnerHtml = SP_JoiningDate.InnerHtml;

            //    SP_Candidate_Jo.InnerHtml = SP_JoiningDate.InnerHtml + " " + SPOffer_CandidateName.InnerHtml;
            //    SP_Greetings2.InnerHtml = SPOffer_CandidateName.InnerHtml;
            //    //txt_Offer_HRA.Text = dsOffer.Tables[1].Rows[0]["JoiningDate"].ToString();
            //}
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

	private void Get_Candidate_Offer_Gemerate_Details(DataTable dsOffer)
	{
		try
		{
			CultureInfo myCIintl = new CultureInfo("hi-IN", false);
			if (dsOffer.Rows.Count > 0)
			{

				txtOffer_Can_Name.Text = dsOffer.Rows[0]["CandidateName"].ToString();
				txtCandAddress.Text = dsOffer.Rows[0]["CandidateAddress"].ToString();
				if (string.IsNullOrEmpty(dsOffer.Rows[0]["Offer_App_ID"].ToString()))
				{
					hdnOffer_Generate.Value = "Update_Offer";
					hdnTempLatterID.Value = dsOffer.Rows[0]["TempLatterID"].ToString();
				}
				else
				{
					hdnTempLatterID.Value = "";
					hdnOffer_Generate.Value = "";
				}

				if (dsOffer.Rows[0]["Loc_code"].ToString() == Convert.ToString(lstOfferBand.SelectedValue))
				{
					txt_Offer_Basic.Text = Convert.ToDecimal(dsOffer.Rows[0]["BASIC"].ToString()).ToString("N2", myCIintl);
				}
				else
				{
					txt_Offer_Basic.Text = "";
				}

				txt_Offer_HRA.Text = Convert.ToDecimal(dsOffer.Rows[0]["HRA"].ToString()).ToString("N2", myCIintl);
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["CONVEYANCE"].ToString()))
				{
					txt_Offer_Conveyance.Text = Convert.ToDecimal(dsOffer.Rows[0]["CONVEYANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["SPECIAL_ALLOWANCE"].ToString()))
				{
					txt_Offer_Special_Allowance.Text = Convert.ToDecimal(dsOffer.Rows[0]["SPECIAL_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["ADHOC_ALLOWANCE"].ToString()))
				{
					txt_Offer_ADHOC_Allowance.Text = Convert.ToDecimal(dsOffer.Rows[0]["ADHOC_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["SKILL_ALLOWANCE"].ToString()))
				{
					txt_offer_Skill_Allowance.Text = Convert.ToDecimal(dsOffer.Rows[0]["SKILL_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["SUPERAN_ALLOWANCE"].ToString()))
				{
					txt_Offer_Superannucation.Text = Convert.ToDecimal(dsOffer.Rows[0]["SUPERAN_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
			
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()))
				{
					txt_Offer_Certificate_All.Text = Convert.ToDecimal(dsOffer.Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()))
				{
					txt_Offer_Multi_Skill_All.Text = Convert.ToDecimal(dsOffer.Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()))
				{
					txt_Offer_Additional_Skill.Text = Convert.ToDecimal(dsOffer.Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["CAR_ALLOWANCE"].ToString()))
				{
					txtCar_All.Text = Convert.ToDecimal(dsOffer.Rows[0]["CAR_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["FOOD_ALLOWANCE"].ToString()))
				{
					txt__Offer_Food_All.Text = Convert.ToDecimal(dsOffer.Rows[0]["FOOD_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				txt_Offer_Total1.Text = Convert.ToDecimal(dsOffer.Rows[0]["TOTAL1"].ToString()).ToString("N2", myCIintl);
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["LTA"].ToString()))
				{
					txt_Offer_LTA.Text = Convert.ToDecimal(dsOffer.Rows[0]["LTA"].ToString()).ToString("N2", myCIintl);
				}
				txt_offer_Medical.Text = dsOffer.Rows[0]["MEDICAL"].ToString();
				txt_Offer_Driver_Salary.Text = dsOffer.Rows[0]["DRIVER_SALARY"].ToString();
				txt_Offer_Car_lease.Text = dsOffer.Rows[0]["CAR_LEASE"].ToString();
				txt_Offer_Total2.Text = Convert.ToDecimal(dsOffer.Rows[0]["TOTAL2"].ToString()).ToString("N2", myCIintl);
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["PF"].ToString()))
				{
					txt_Offer_PF.Text = Convert.ToDecimal(dsOffer.Rows[0]["PF"].ToString()).ToString("N2", myCIintl);
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["GRATUITY_B"].ToString()))
				{
					txt_Offer_Gratuity.Text = Convert.ToDecimal(dsOffer.Rows[0]["GRATUITY_B"].ToString()).ToString("N2", myCIintl);
				}
				txt_Offer_Total3.Text = Convert.ToDecimal(dsOffer.Rows[0]["TOTAL3"].ToString()).ToString("N2", myCIintl);
				txt_Offer_Mediclaim.Text = dsOffer.Rows[0]["MEDICLAIM"].ToString();
				txt_Offer_Group_Policy.Text = dsOffer.Rows[0]["GROUP_ACC_POLICY"].ToString();
				
                       if (!string.IsNullOrEmpty(dsOffer.Rows[0]["JOINING_BONUS"].ToString()))
				{
					txt_Retention_Bonus.Text = Convert.ToDecimal(dsOffer.Rows[0]["JOINING_BONUS"].ToString()).ToString("N2", myCIintl);
				}

				txtTotal4.Text = dsOffer.Rows[0]["TOTAL4"].ToString();
				txt_Offer_CTC_Month.Text = Convert.ToDecimal(dsOffer.Rows[0]["CTC_PER_MONTH"].ToString()).ToString("N2", myCIintl);
				txt_Offer_CTC_Annum.Text = Convert.ToDecimal(dsOffer.Rows[0]["CTC_PER_ANNUM"].ToString()).ToString("N2", myCIintl);
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["PLP_VARIABLE_PAY"].ToString()))
				{
					txt_Offer_PLP_variable.Text = dsOffer.Rows[0]["PLP_VARIABLE_PAY"].ToString();
				}
				txt_Offer_PLP_Per.Text = dsOffer.Rows[0]["PLP_VARIABLE_Percentage"].ToString();
				txt_Offer_CTC_Annum_Incl_PLP.Text = Convert.ToDecimal(dsOffer.Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2", myCIintl);
                        if (!string.IsNullOrEmpty(dsOffer.Rows[0]["ANNUAL_BONUS"].ToString()))
				{
                          txt_Annual_Bonus.Text =Convert.ToDecimal(dsOffer.Rows[0]["ANNUAL_BONUS"].ToString()).ToString("N2", myCIintl);
                        }
				txt_Offer_Other.Text = dsOffer.Rows[0]["OTHER"].ToString();
				txt_Retention_Comment.Text = dsOffer.Rows[0]["Joining_Comment"].ToString();
                       if (!string.IsNullOrEmpty(dsOffer.Rows[0]["ALP_ALLOWANCE"].ToString()))
				{
					txt_ALP_Amount.Text = Convert.ToDecimal(dsOffer.Rows[0]["ALP_ALLOWANCE"].ToString()).ToString("N2", myCIintl);
				}
				txt_ALP_Comment.Text = dsOffer.Rows[0]["ALP_Comment"].ToString();
				//StructureID_Skill_All
				hdn_Skill_ALL_StructureID.Value = dsOffer.Rows[0]["StructureID_Skill_All"].ToString();
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["StructureID_HRA"].ToString()))
				{
					SP_HRA.InnerText = dsOffer.Rows[0]["RHR"].ToString();
					hdn_HRA_StructureID.Value = dsOffer.Rows[0]["StructureID_HRA"].ToString();
				}
				if (!string.IsNullOrEmpty(dsOffer.Rows[0]["StructureID_SUPERAN"].ToString()))
				{
					SP_Superannucation_All.InnerText = dsOffer.Rows[0]["RSUPERAN"].ToString();
					hdn_Superan_All_StructureID.Value = dsOffer.Rows[0]["StructureID_SUPERAN"].ToString();
				}
				SP_LTA.InnerText = dsOffer.Rows[0]["RLTA"].ToString();
				hdn_LTA_StructureID.Value = dsOffer.Rows[0]["StructureID_LTA"].ToString();

				SP_PF.InnerText = dsOffer.Rows[0]["RPF"].ToString();
				hdn_PF_StructureID.Value = dsOffer.Rows[0]["StructureID_PF"].ToString();

                if (!string.IsNullOrEmpty(dsOffer.Rows[0]["health_Check_Up"].ToString()))
                {
                    txt_Offer_Health_Checkup.Text = dsOffer.Rows[0]["health_Check_Up"].ToString();
                }

                if (!string.IsNullOrEmpty(dsOffer.Rows[0]["Car_Hire_Cost"].ToString()))
                {
                    txt_offer_Car_Hire_Cost.Text = dsOffer.Rows[0]["Car_Hire_Cost"].ToString();

                }
                if (!string.IsNullOrEmpty(dsOffer.Rows[0]["Car_Expenses_Reimursement"].ToString()))
                {
                    txt_Offer_Car_Expenses_Reimbursment.Text = dsOffer.Rows[0]["Car_Expenses_Reimursement"].ToString();
                }
                if (!string.IsNullOrEmpty(dsOffer.Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString()))
                {
                    txt_Car_Fuel_Expenses_Reimbursment.Text = dsOffer.Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString();
                }
                if (!string.IsNullOrEmpty(dsOffer.Rows[0]["Total5"].ToString()))
                {
                    txtTotal5.Text = dsOffer.Rows[0]["Total5"].ToString();
                }



                hdn_Gratuity_StructureID.Value = dsOffer.Rows[0]["StructureID_GRATUTY"].ToString();

                


                if (!string.IsNullOrEmpty(dsOffer.Rows[0]["Status_ID"].ToString()))
				{
					if (Convert.ToString(dsOffer.Rows[0]["Status_ID"].ToString())=="4")
					{
						ClearSalary();
					}

					if (!string.IsNullOrEmpty(dsOffer.Rows[0]["Candidate_Status"].ToString()))
					{
						if (Convert.ToString(dsOffer.Rows[0]["Candidate_Status"].ToString()) == "Rejected")
						{
							ClearSalary();
						}
					}

				}
					updatePanel.Update();
				this.ModalPopupExtenderGenerateOffer.Show();

			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void ClearSalary()
	{
		txt_Offer_ADHOC_Allowance.Text = "0";
		txt_offer_Skill_Allowance.Text = "0";
		txt_Offer_Conveyance.Text = "0";
		txt__Offer_Food_All.Text = "0";
		txt_Offer_Superannucation.Text = "0";
		txt_Offer_Certificate_All.Text = "0";
		txt_Offer_Multi_Skill_All.Text = "0";
		txt_Offer_Additional_Skill.Text = "0";
		SP_Superannucation_All.InnerText = "";
		hdn_Superan_All_StructureID.Value = "";
		hdn_Skill_ALL_StructureID.Value = "";
		Generate_Offer_Basic();
		Generate_CTC_Per_Month();
	}
	protected void lnk_Offer_Submit_Click(object sender, EventArgs e)
	{
		DataTable DtResult = new DataTable();
		string confirmValue = hdnYesNo.Value.ToString();
		if (confirmValue != "Yes")
		{
			return;
		}
		string[] strdate, strProbable;
		#region Validation
		//if (ddl_Offer_EmploymentType.SelectedValue == "" || ddl_Offer_EmploymentType.SelectedValue == "0")
		//{
		//	lblGenerateMsg.Text = "Please Select Employment Type.";
		//	ModalPopupExtenderGenerateOffer.Show();
		//	return;
		//}
		if (txtOffer_Can_Name.Text == "")
		{
			lblGenerateMsg.Text = "Please Enter Candidate Name As Aadhar.";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txtCandAddress.Text == "")
		{
			lblGenerateMsg.Text = "Please Enter Candidate Address As Aadhar.";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}

		if (txt_Offer_Basic.Text == "" || txt_Offer_Basic.Text == "0" || txt_Offer_Basic.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Offer Basic";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_HRA.Text == "" || txt_Offer_HRA.Text == "0.00" || txt_Offer_HRA.Text == "0")
		{
			lblGenerateMsg.Text = "Please Enter House Rent Allowance (HRA)";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}

		if (txt_Offer_Special_Allowance.Text == "" || txt_Offer_Special_Allowance.Text == "0.00" || txt_Offer_Special_Allowance.Text == "0")
		{
			lblGenerateMsg.Text = "Please Enter Special Allowance";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}

		if (txt_Offer_Total1.Text == "" || txt_Offer_Total1.Text == "0" || txt_Offer_Total1.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Total 1 ";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_LTA.Text == "" || txt_Offer_LTA.Text == "0" || txt_Offer_LTA.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter LTA ";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_Total2.Text == "" || txt_Offer_Total2.Text == "0.00" || txt_Offer_Total2.Text == "0")
		{
			lblGenerateMsg.Text = "Please Enter Total 2 ";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_PF.Text == "" || txt_Offer_PF.Text == "0"||  txt_Offer_PF.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter PF ";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_Gratuity.Text == "" || txt_Offer_Gratuity.Text == "0" || txt_Offer_Gratuity.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Gratuity";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_Total3.Text == "" || txt_Offer_Total3.Text == "0" || txt_Offer_Total3.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Total 3";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_Mediclaim.Text == "" || txt_Offer_Mediclaim.Text == "0" || txt_Offer_Mediclaim.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Mediclaim";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
        
        if (txt_Offer_Health_Checkup.Text == "" || txt_Offer_Health_Checkup.Text == "0" || txt_Offer_Health_Checkup.Text == "0.00")
        {
            if (lstOfferBand.SelectedItem.Text == "I" || lstOfferBand.SelectedItem.Text == "II" || lstOfferBand.SelectedItem.Text == "III" || lstOfferBand.SelectedItem.Text == "IV" || lstOfferBand.SelectedItem.Text == "V")
            {
            }
            else
            {
                lblGenerateMsg.Text = "Please Enter HEALTH CHECK UP";
                ModalPopupExtenderGenerateOffer.Show();
                return;
            }
        }
        
        
            if (txt_offer_Car_Hire_Cost.Text == "" || txt_offer_Car_Hire_Cost.Text == "0" || txt_offer_Car_Hire_Cost.Text == "0.00")
            {
            if (lstOfferBand.SelectedItem.Text == "I" || lstOfferBand.SelectedItem.Text == "II" || lstOfferBand.SelectedItem.Text == "III" || lstOfferBand.SelectedItem.Text == "IV" || lstOfferBand.SelectedItem.Text == "V" || lstOfferBand.SelectedItem.Text == "VI")
            {
            }
            else
            {
                lblGenerateMsg.Text = "Please Enter CAR HIRE COST";
                ModalPopupExtenderGenerateOffer.Show();
                return;
            }  
            }
            if (txt_Offer_Car_Expenses_Reimbursment.Text == "" || txt_Offer_Car_Expenses_Reimbursment.Text == "0" || txt_Offer_Car_Expenses_Reimbursment.Text == "0.00")
            {
               if (lstOfferBand.SelectedItem.Text == "I" || lstOfferBand.SelectedItem.Text == "II" || lstOfferBand.SelectedItem.Text == "III" || lstOfferBand.SelectedItem.Text == "IV" || lstOfferBand.SelectedItem.Text == "V" || lstOfferBand.SelectedItem.Text == "VI")
                 {
                 }
                else
                {
                   lblGenerateMsg.Text = "Please Enter CAR EXPENSES REIMBURSEMENT";
                   ModalPopupExtenderGenerateOffer.Show();
                   return;
                }  
            }
        

        
            if (txt_Car_Fuel_Expenses_Reimbursment.Text == "" || txt_Car_Fuel_Expenses_Reimbursment.Text == "0" || txt_Car_Fuel_Expenses_Reimbursment.Text == "0.00")
            {
            if (lstOfferBand.SelectedItem.Text == "I" || lstOfferBand.SelectedItem.Text == "II" || lstOfferBand.SelectedItem.Text == "III" || lstOfferBand.SelectedItem.Text == "IV" || lstOfferBand.SelectedItem.Text == "V" || lstOfferBand.SelectedItem.Text == "VI")
            {

            }
            else
            {
                lblGenerateMsg.Text = "Please Enter CAR FUEL EXPENSES REIMBURSEMENT";
                ModalPopupExtenderGenerateOffer.Show();
                return;
            }  
            }
        
        if (txt_Offer_Group_Policy.Text == "" || txt_Offer_Group_Policy.Text == "0" || txt_Offer_Group_Policy.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Group Acc Policy";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txtTotal4.Text == "" || txtTotal4.Text == "0" || txtTotal4.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter Total 4";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}

		if (txt_Offer_CTC_Month.Text == "" || txt_Offer_CTC_Month.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter CTC Per Month ";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (txt_Offer_CTC_Annum.Text == "" || txt_Offer_CTC_Annum.Text == "0.00")
		{
			lblGenerateMsg.Text = "Please Enter CTC Per Annum";
			ModalPopupExtenderGenerateOffer.Show();
			return;
		}
		if (Convert.ToString(txt_Retention_Bonus.Text) != "")
		{
			if(Convert.ToString(txt_Retention_Comment.Text) == "")
              {
			lblGenerateMsg.Text = "Please Enter Joning Bonus Remark";
			ModalPopupExtenderGenerateOffer.Show();
			return;
             }
		}
		if (Convert.ToString(txt_ALP_Amount.Text) != "")
		{
			if (Convert.ToString(txt_ALP_Comment.Text) == "")
                   {
				lblGenerateMsg.Text = "Please Enter ALP Remark";
			      ModalPopupExtenderGenerateOffer.Show();
			      return;
                 }
		}
		#endregion

		Generate_Offer_Basic();
		Generate_Retirals_Taxability();
		Generate_Offer_PF();
        Generate_Offer_Car_ExpencesCost();
        Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();


        decimal Offer_Basic = 0, Offer_HRA = 0, Offer_Conveyance = 0, Offer_ADHOC_Allowance = 0, offer_Skill_Allowance = 0,
        Offer_Superannucation = 0, Offer_Certificate_All = 0, Offer_Multi_Skill_All = 0, Offer_Additional_Skill = 0, Car_All = 0, Offer_Food_All = 0,
        Total1 = 0, Total2 = 0, Total3 = 0, Total4 = 0, Offer_LTA = 0, offer_Medical = 0, Offer_Driver_Salary = 0, Offer_Car_lease = 0, PLP_VARIABLE_Percentage = 0, Offer_Special_Allow = 0,
        Offer_PF = 0, Offer_Gratuity = 0, Offer_Mediclaim = 0, Offer_Group_Policy = 0, Retention_Bonus = 0, Offer_CTC_Month = 0, Offer_CTC_Annum = 0, PLP_variable = 0, CTC_Annum_Incl_PLP = 0, Annual_Bonus = 0, Offer_Other = 0, ALP_Amount = 0,
        Total5 = 0, Offer_Health_Checkup = 0, offer_Car_Hire_Cost = 0, Offer_Car_Expenses_Reimbursment = 0, Offer_Car_Fuel_Expenses_Reimbursment = 0;


        int StructureID_HRA = 0, StructureID_SUPERAN = 0, StructureID_LTA = 0, StructureID_PF = 0, StructureID_GRATUTY = 0, Skill_ALL_StructureID = 0;


        string Retention_Comment = string.Empty, ALP_Comment = string.Empty;
		Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
		Offer_HRA = string.IsNullOrEmpty(txt_Offer_HRA.Text) ? 0 : Convert.ToDecimal(txt_Offer_HRA.Text);
		Offer_Conveyance = string.IsNullOrEmpty(txt_Offer_Conveyance.Text) ? 0 : Convert.ToDecimal(txt_Offer_Conveyance.Text);
		Offer_ADHOC_Allowance = string.IsNullOrEmpty(txt_Offer_ADHOC_Allowance.Text) ? 0 : Convert.ToDecimal(txt_Offer_ADHOC_Allowance.Text);

		offer_Skill_Allowance = string.IsNullOrEmpty(txt_offer_Skill_Allowance.Text) ? 0 : Convert.ToDecimal(txt_offer_Skill_Allowance.Text);
		Offer_Superannucation = string.IsNullOrEmpty(txt_Offer_Superannucation.Text) ? 0 : Convert.ToDecimal(txt_Offer_Superannucation.Text);
		Offer_Certificate_All = string.IsNullOrEmpty(txt_Offer_Certificate_All.Text) ? 0 : Convert.ToDecimal(txt_Offer_Certificate_All.Text);
		Offer_Multi_Skill_All = string.IsNullOrEmpty(txt_Offer_Multi_Skill_All.Text) ? 0 : Convert.ToDecimal(txt_Offer_Multi_Skill_All.Text);
		Offer_Additional_Skill = string.IsNullOrEmpty(txt_Offer_Additional_Skill.Text) ? 0 : Convert.ToDecimal(txt_Offer_Additional_Skill.Text);
		Car_All = string.IsNullOrEmpty(txtCar_All.Text) ? 0 : Convert.ToDecimal(txtCar_All.Text);
		Offer_Food_All = string.IsNullOrEmpty(txt__Offer_Food_All.Text) ? 0 : Convert.ToDecimal(txt__Offer_Food_All.Text);
		Offer_Special_Allow = string.IsNullOrEmpty(txt_Offer_Special_Allowance.Text) ? 0 : Convert.ToDecimal(txt_Offer_Special_Allowance.Text);

		Total1 = string.IsNullOrEmpty(txt_Offer_Total1.Text) ? 0 : Convert.ToDecimal(txt_Offer_Total1.Text);

		Offer_LTA = string.IsNullOrEmpty(txt_Offer_LTA.Text) ? 0 : Convert.ToDecimal(txt_Offer_LTA.Text);
		offer_Medical = string.IsNullOrEmpty(txt_offer_Medical.Text) ? 0 : Convert.ToDecimal(txt_offer_Medical.Text);
		Offer_Driver_Salary = string.IsNullOrEmpty(txt_Offer_Driver_Salary.Text) ? 0 : Convert.ToDecimal(txt_Offer_Driver_Salary.Text);
		Offer_Car_lease = string.IsNullOrEmpty(txt_Offer_Car_lease.Text) ? 0 : Convert.ToDecimal(txt_Offer_Car_lease.Text);
		Total2 = string.IsNullOrEmpty(txt_Offer_Total2.Text) ? 0 : Convert.ToDecimal(txt_Offer_Total2.Text);

		Offer_PF = string.IsNullOrEmpty(txt_Offer_PF.Text) ? 0 : Convert.ToDecimal(txt_Offer_PF.Text);
		Offer_Gratuity = string.IsNullOrEmpty(txt_Offer_Gratuity.Text) ? 0 : Convert.ToDecimal(txt_Offer_Gratuity.Text);
		Total3 = string.IsNullOrEmpty(txt_Offer_Total3.Text) ? 0 : Convert.ToDecimal(txt_Offer_Total3.Text);

		Offer_Mediclaim = string.IsNullOrEmpty(txt_Offer_Mediclaim.Text) ? 0 : Convert.ToDecimal(txt_Offer_Mediclaim.Text);
        Offer_Health_Checkup = string.IsNullOrEmpty(txt_Offer_Health_Checkup.Text) ? 0 : Convert.ToDecimal(txt_Offer_Health_Checkup.Text);
        Offer_Group_Policy = string.IsNullOrEmpty(txt_Offer_Group_Policy.Text) ? 0 : Convert.ToDecimal(txt_Offer_Group_Policy.Text);
		Retention_Bonus = string.IsNullOrEmpty(txt_Retention_Bonus.Text) ? 0 : Convert.ToDecimal(txt_Retention_Bonus.Text);
		Total4 = string.IsNullOrEmpty(txtTotal4.Text) ? 0 : Convert.ToDecimal(txtTotal4.Text);

        offer_Car_Hire_Cost = string.IsNullOrEmpty(txt_offer_Car_Hire_Cost.Text) ? 0 : Convert.ToDecimal(txt_offer_Car_Hire_Cost.Text);
        Offer_Car_Expenses_Reimbursment = string.IsNullOrEmpty(txt_Offer_Car_Expenses_Reimbursment.Text) ? 0 : Convert.ToDecimal(txt_Offer_Car_Expenses_Reimbursment.Text);
        Offer_Car_Fuel_Expenses_Reimbursment = string.IsNullOrEmpty(txt_Car_Fuel_Expenses_Reimbursment.Text) ? 0 : Convert.ToDecimal(txt_Car_Fuel_Expenses_Reimbursment.Text);
        Total5 = string.IsNullOrEmpty(txtTotal5.Text) ? 0 : Convert.ToDecimal(txtTotal5.Text);


        Offer_CTC_Month = string.IsNullOrEmpty(txt_Offer_CTC_Month.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Month.Text);
		Offer_CTC_Annum = string.IsNullOrEmpty(txt_Offer_CTC_Annum.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Annum.Text);
		PLP_VARIABLE_Percentage = string.IsNullOrEmpty(txt_Offer_PLP_Per.Text) ? 0 : Convert.ToDecimal(txt_Offer_PLP_Per.Text);
		PLP_variable = string.IsNullOrEmpty(txt_Offer_PLP_variable.Text) ? 0 : Convert.ToDecimal(txt_Offer_PLP_variable.Text);
		CTC_Annum_Incl_PLP = string.IsNullOrEmpty(txt_Offer_CTC_Annum_Incl_PLP.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Annum_Incl_PLP.Text);

		Annual_Bonus = string.IsNullOrEmpty(txt_Annual_Bonus.Text) ? 0 : Convert.ToDecimal(txt_Annual_Bonus.Text);
		Offer_Other = string.IsNullOrEmpty(txt_Offer_Other.Text) ? 0 : Convert.ToDecimal(txt_Offer_Other.Text);

		Retention_Comment = string.IsNullOrEmpty(txt_Retention_Comment.Text) ? "" : Convert.ToString(txt_Retention_Comment.Text);
		ALP_Amount = string.IsNullOrEmpty(txt_ALP_Amount.Text) ? 0 : Convert.ToDecimal(txt_ALP_Amount.Text);
		ALP_Comment = string.IsNullOrEmpty(txt_ALP_Comment.Text) ? "" : Convert.ToString(txt_ALP_Comment.Text);

		
		StructureID_HRA = string.IsNullOrEmpty(hdn_HRA_StructureID.Value) ? 0 : Convert.ToInt32(hdn_HRA_StructureID.Value);
		StructureID_SUPERAN = string.IsNullOrEmpty(hdn_Superan_All_StructureID.Value) ? 0 : Convert.ToInt32(hdn_Superan_All_StructureID.Value);
		StructureID_LTA = string.IsNullOrEmpty(hdn_LTA_StructureID.Value) ? 0 : Convert.ToInt32(hdn_LTA_StructureID.Value);
		StructureID_PF = string.IsNullOrEmpty(hdn_PF_StructureID.Value) ? 0 : Convert.ToInt32(hdn_PF_StructureID.Value);
		StructureID_GRATUTY = string.IsNullOrEmpty(hdn_Gratuity_StructureID.Value) ? 0 : Convert.ToInt32(hdn_Gratuity_StructureID.Value);
		Skill_ALL_StructureID = string.IsNullOrEmpty(hdn_Skill_ALL_StructureID.Value) ? 0 : Convert.ToInt32(hdn_Skill_ALL_StructureID.Value);

       
        var PID = string.IsNullOrEmpty(ddl_Offer_EmploymentType.SelectedValue) ? 0 : Convert.ToInt32(ddl_Offer_EmploymentType.SelectedValue);
		string Qtype = "";
		int TempLatterID = 0;
		if (!string.IsNullOrEmpty(hdnOffer_Generate.Value))
		{
			Qtype = "Update_Generate_Offer";
			TempLatterID = string.IsNullOrEmpty(hdnTempLatterID.Value) ? 0 : Convert.ToInt32(hdnTempLatterID.Value);

		}
		else
		{
			Qtype = "Insert_Generate_Offer";
		}
		DtResult = spm.Insert_Generate_Offer_Candidate(Qtype, TempLatterID, Convert.ToString(Session["Empcode"]).Trim(), lstOfferBand.SelectedValue, 0, txtOffer_Can_Name.Text.Trim(), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), Offer_Basic, Offer_HRA, Offer_Conveyance, Offer_ADHOC_Allowance, offer_Skill_Allowance,
		Offer_Superannucation, Offer_Certificate_All, Offer_Multi_Skill_All, Offer_Additional_Skill, Car_All, Offer_Food_All,
		Offer_Special_Allow, Total1, Offer_LTA, offer_Medical, Offer_Driver_Salary, Offer_Car_lease, Total2,
		Offer_PF, Offer_Gratuity, Total3, Offer_Mediclaim, Offer_Group_Policy, Retention_Bonus, Total4, Offer_CTC_Month, Offer_CTC_Annum,
        PLP_variable, CTC_Annum_Incl_PLP, Annual_Bonus, Offer_Other, StructureID_HRA, StructureID_SUPERAN, StructureID_LTA, StructureID_PF,
        StructureID_GRATUTY, PLP_VARIABLE_Percentage, Skill_ALL_StructureID, txtCandAddress.Text, Retention_Comment, ALP_Amount, ALP_Comment,
        Total5,Offer_Health_Checkup,offer_Car_Hire_Cost,Offer_Car_Expenses_Reimbursment, Offer_Car_Fuel_Expenses_Reimbursment);
		GRDGenerate_Offer.DataSource = null;
		GRDGenerate_Offer.DataBind();
		if (DtResult.Rows.Count > 0)
		{
			//btn_Generate_Offer.Visible = false;
			if (!string.IsNullOrEmpty(DtResult.Rows[0]["PLP_VARIABLE_PAY"].ToString()))
			{
				GRDGenerate_Offer.Columns[0].Visible = true;
				GRDGenerate_Offer.Columns[1].Visible = true;
			}
			else
			{
				GRDGenerate_Offer.Columns[0].Visible = false;
				GRDGenerate_Offer.Columns[1].Visible = false;
			}
			if ((DtResult.Rows[0]["Candidate_Status"].ToString() == "Accepted"))
			{
				GRDGenerate_Offer.Columns[6].Visible = true;
			}
			else
			{
				GRDGenerate_Offer.Columns[6].Visible = false;
			}
			GRDGenerate_Offer.DataSource = DtResult;
			GRDGenerate_Offer.DataBind();
			//txtOfferno1.Text =((DtResult.Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2");
			txtOfferno1.Text = Convert.ToDecimal(DtResult.Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			//btn_Generate_Offer.Style.Add("display", "none");
			txtOfferno1.Enabled = false;
			//txt_Off_Dis_HRA.Text = dsOffer.Tables[0].Rows[0]["HRA"].ToString();
		}

	}

	protected void txt_Offer_Basic_TextChanged(object sender, EventArgs e)
	{

		Set_Offer_HRA();
		Set_Offer_Superannucation();
		Set_Offer_LTA();
		Set_Offer_PF();
		Set_Offer_Gratuity();
        Generate_Offer_Basic();
		Generate_Retirals_Taxability();
		Generate_Offer_PF();
		Generate_Offer_Facilities();
        Generate_Offer_Car_ExpencesCost();
        Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
        

    }
	private void Set_Offer_HRA()
	{
		try
		{
			decimal Result = 0;
			decimal Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
			decimal Offer_HRA = string.IsNullOrEmpty(hdn_HRA_Per.Value) ? 0 : Convert.ToDecimal(hdn_HRA_Per.Value);
			Result = (Offer_Basic * Offer_HRA / 100);
			txt_Offer_HRA.Text = Convert.ToString(Math.Round(Result).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")));
			updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Set_Offer_Superannucation()
	{
		try
		{
			decimal Result = 0;
			decimal Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
			decimal Offer_Superan_All = string.IsNullOrEmpty(hdn_Superan_All.Value) ? 0 : Convert.ToDecimal(hdn_Superan_All.Value);
			Result = Math.Round(Offer_Basic * Offer_Superan_All / 100);
			txt_Offer_Superannucation.Text = Convert.ToString(Result.ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")));
			updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Set_Offer_LTA()
	{
		try
		{
			decimal Result = 0;
			decimal Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
			decimal Offer_LTA = string.IsNullOrEmpty(hdn_LTA.Value) ? 0 : Convert.ToDecimal(hdn_LTA.Value);
			Result = Math.Round(Offer_Basic * Offer_LTA / 100);
			txt_Offer_LTA.Text = Convert.ToString(Result.ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")));
			updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Set_Offer_PF()
	{
		try
		{
			decimal Result = 0;
			decimal Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
			decimal Offer_PF = string.IsNullOrEmpty(hdn_PF.Value) ? 0 : Convert.ToDecimal(hdn_PF.Value);
			Result = Math.Round(Offer_Basic * Offer_PF / 100);
			txt_Offer_PF.Text = Convert.ToString(Result.ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")));
			updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Set_Offer_Gratuity()
	{
		try
		{
			decimal Result = 0;
			decimal Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
			decimal Offer_Gratuity = string.IsNullOrEmpty(hdn_Gratuity.Value) ? 0 : Convert.ToDecimal(hdn_Gratuity.Value);
			Result = Math.Round(Offer_Basic * Offer_Gratuity / 100);
			txt_Offer_Gratuity.Text = Convert.ToString(Result.ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")));
			updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

    

    private void Generate_Offer_Basic()
	{
		try
		{

			decimal Offer_Basic = string.IsNullOrEmpty(txt_Offer_Basic.Text) ? 0 : Convert.ToDecimal(txt_Offer_Basic.Text);
			decimal Offer_HRA = string.IsNullOrEmpty(txt_Offer_HRA.Text) ? 0 : Convert.ToDecimal(txt_Offer_HRA.Text);
			decimal Offer_Conveyance = string.IsNullOrEmpty(txt_Offer_Conveyance.Text) ? 0 : Convert.ToDecimal(txt_Offer_Conveyance.Text);
			decimal Offer_ADHOC_Allowance = string.IsNullOrEmpty(txt_Offer_ADHOC_Allowance.Text) ? 0 : Convert.ToDecimal(txt_Offer_ADHOC_Allowance.Text);

			decimal offer_Skill_Allowance = string.IsNullOrEmpty(txt_offer_Skill_Allowance.Text) ? 0 : Convert.ToDecimal(txt_offer_Skill_Allowance.Text);
			decimal Offer_Superannucation = string.IsNullOrEmpty(txt_Offer_Superannucation.Text) ? 0 : Convert.ToDecimal(txt_Offer_Superannucation.Text);
			decimal Offer_Certificate_All = string.IsNullOrEmpty(txt_Offer_Certificate_All.Text) ? 0 : Convert.ToDecimal(txt_Offer_Certificate_All.Text);
			decimal Offer_Multi_Skill_All = string.IsNullOrEmpty(txt_Offer_Multi_Skill_All.Text) ? 0 : Convert.ToDecimal(txt_Offer_Multi_Skill_All.Text);
			decimal Offer_Additional_Skill = string.IsNullOrEmpty(txt_Offer_Additional_Skill.Text) ? 0 : Convert.ToDecimal(txt_Offer_Additional_Skill.Text);
			decimal Car_All = string.IsNullOrEmpty(txtCar_All.Text) ? 0 : Convert.ToDecimal(txtCar_All.Text);
			decimal Offer_Food_All = string.IsNullOrEmpty(txt__Offer_Food_All.Text) ? 0 : Convert.ToDecimal(txt__Offer_Food_All.Text);
			decimal Offer_Special_Allowance = string.IsNullOrEmpty(txt_Offer_Special_Allowance.Text) ? 0 : Convert.ToDecimal(txt_Offer_Special_Allowance.Text);

			txt_Offer_Total1.Text = (Offer_Basic + Offer_HRA + Offer_Conveyance + Offer_ADHOC_Allowance + offer_Skill_Allowance + Offer_Superannucation + Offer_Certificate_All + Offer_Multi_Skill_All + Offer_Additional_Skill + Car_All + Offer_Food_All + Offer_Special_Allowance ).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			updatePanel.Update();
			//ModalPopupExtenderGenerateOffer.Show();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Generate_Retirals_Taxability()
	{
		try
		{
			decimal Offer_LTA = string.IsNullOrEmpty(txt_Offer_LTA.Text) ? 0 : Convert.ToDecimal(txt_Offer_LTA.Text);
			decimal offer_Medical = string.IsNullOrEmpty(txt_offer_Medical.Text) ? 0 : Convert.ToDecimal(txt_offer_Medical.Text);
			decimal Offer_Driver_Salary = string.IsNullOrEmpty(txt_Offer_Driver_Salary.Text) ? 0 : Convert.ToDecimal(txt_Offer_Driver_Salary.Text);
			decimal Offer_Car_lease = string.IsNullOrEmpty(txt_Offer_Car_lease.Text) ? 0 : Convert.ToDecimal(txt_Offer_Car_lease.Text);
			txt_Offer_Total2.Text = (Offer_LTA + offer_Medical + Offer_Driver_Salary + Offer_Car_lease).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN")); ;
			updatePanel.Update();
			//ModalPopupExtenderGenerateOffer.Show();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Generate_Offer_PF()
	{
		try
		{
			decimal Offer_PF = string.IsNullOrEmpty(txt_Offer_PF.Text) ? 0 : Convert.ToDecimal(txt_Offer_PF.Text);
			decimal Offer_Gratuity = string.IsNullOrEmpty(txt_Offer_Gratuity.Text) ? 0 : Convert.ToDecimal(txt_Offer_Gratuity.Text);
			txt_Offer_Total3.Text = (Offer_PF + Offer_Gratuity).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			updatePanel.Update();
			//ModalPopupExtenderGenerateOffer.Show();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

    private void Generate_Offer_Car_ExpencesCost()
    {
        try
        {
            decimal Offer_Car1 = string.IsNullOrEmpty(txt_offer_Car_Hire_Cost.Text) ? 0 : Convert.ToDecimal(txt_offer_Car_Hire_Cost.Text);
            decimal Offer_Car2 = string.IsNullOrEmpty(txt_Offer_Car_Expenses_Reimbursment.Text) ? 0 : Convert.ToDecimal(txt_Offer_Car_Expenses_Reimbursment.Text);
            decimal Offer_Car3 = string.IsNullOrEmpty(txt_Car_Fuel_Expenses_Reimbursment.Text) ? 0 : Convert.ToDecimal(txt_Car_Fuel_Expenses_Reimbursment.Text);
            txtTotal5.Text = (Offer_Car1 + Offer_Car2 + Offer_Car3).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
            updatePanel.Update(); 
            //ModalPopupExtenderGenerateOffer.Show();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }

    private void Generate_Offer_Facilities()
	{
		try
		{
			decimal Offer_Mediclaim = string.IsNullOrEmpty(txt_Offer_Mediclaim.Text) ? 0 : Convert.ToDecimal(txt_Offer_Mediclaim.Text);
			decimal Offer_Group_Policy = string.IsNullOrEmpty(txt_Offer_Group_Policy.Text) ? 0 : Convert.ToDecimal(txt_Offer_Group_Policy.Text);
            decimal Offer_Health_Checkup = string.IsNullOrEmpty(txt_Offer_Health_Checkup.Text) ? 0 : Convert.ToDecimal(txt_Offer_Health_Checkup.Text);
            //decimal Retention_Bonus = string.IsNullOrEmpty(txt_Retention_Bonus.Text) ? 0 : Convert.ToDecimal(txt_Retention_Bonus.Text);
            txtTotal4.Text = (Offer_Mediclaim + Offer_Group_Policy + Offer_Health_Checkup).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			updatePanel.Update();
			//ModalPopupExtenderGenerateOffer.Show();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Generate_CTC_Per_Month()
	{
		try
		{
			decimal Offer_Total1 = string.IsNullOrEmpty(txt_Offer_Total1.Text) ? 0 : Convert.ToDecimal(txt_Offer_Total1.Text);
			decimal Offer_Total2 = string.IsNullOrEmpty(txt_Offer_Total2.Text) ? 0 : Convert.ToDecimal(txt_Offer_Total2.Text);
			decimal Offer_Total3 = string.IsNullOrEmpty(txt_Offer_Total3.Text) ? 0 : Convert.ToDecimal(txt_Offer_Total3.Text);
			decimal Offer_Total4 = string.IsNullOrEmpty(txtTotal4.Text) ? 0 : Convert.ToDecimal(txtTotal4.Text);
            decimal Offer_Total5 = string.IsNullOrEmpty(txtTotal5.Text) ? 0 : Convert.ToDecimal(txtTotal5.Text);
            txt_Offer_CTC_Month.Text = (Offer_Total1 + Offer_Total2 + Offer_Total3 + Offer_Total4  + Offer_Total5).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			txt_Offer_CTC_Annum.Text = ((Offer_Total1 + Offer_Total2 + Offer_Total3 + Offer_Total4 + Offer_Total5) * 12).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));

			//decimal Offer_CTC_Annum = string.IsNullOrEmpty(txt_Offer_CTC_Annum.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Annum.Text);
			//decimal Offer_PLP_variable = string.IsNullOrEmpty(txt_Offer_PLP_variable.Text) ? 0 : Convert.ToDecimal(txt_Offer_PLP_variable.Text);
			//txt_Offer_CTC_Annum_Incl_PLP.Text = (Offer_CTC_Annum + Offer_PLP_variable).ToString("N2");
			updatePanel.Update();

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Generate_CTC_PLP_Percentage()
	{
		try
		{
			decimal Result = 0;
			decimal CTC_ANNUM_PER = string.IsNullOrEmpty(txt_Offer_CTC_Annum.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Annum.Text);
			decimal PLP_PER = string.IsNullOrEmpty(txt_Offer_PLP_Per.Text) ? 0 : Convert.ToDecimal(txt_Offer_PLP_Per.Text);
			Result = Math.Round(CTC_ANNUM_PER * PLP_PER / 100);
			txt_Offer_PLP_variable.Text = (Result).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			txt_Offer_CTC_Annum_Incl_PLP.Text = (CTC_ANNUM_PER + Result).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
			updatePanel.Update();

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

	protected void txt_Offer_LTA_TextChanged(object sender, EventArgs e)
	{
		Generate_Retirals_Taxability();
		Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
	}
	protected void txt_Offer_PF_TextChanged(object sender, EventArgs e)
	{
		Generate_Offer_PF();
		Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
	}
	protected void txt_Offer_Mediclaim_TextChanged(object sender, EventArgs e)
	{
		Generate_Offer_Facilities();
		Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
	}
	protected void txt_Offer_Group_Policy_TextChanged(object sender, EventArgs e)
	{
		Generate_Offer_Facilities();
		Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
	}
	protected void txt_Retention_Bonus_TextChanged(object sender, EventArgs e)
	{
		//Generate_Offer_Facilities();
		//Generate_CTC_Per_Month();
	}
	protected void txt_Offer_ADHOC_Allowance_TextChanged(object sender, EventArgs e)
	{
		Generate_Offer_Basic();
		Generate_Retirals_Taxability();
		Generate_Offer_PF();
		Generate_Offer_Facilities();
		Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
        Generate_Offer_Car_ExpencesCost();

    }
	protected void txt_Offer_CTC_Month_TextChanged(object sender, EventArgs e)
	{
		try
		{
			//decimal Offer_Total1 = string.IsNullOrEmpty(txt_Offer_CTC_Month.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Month.Text);
			//txt_Offer_CTC_Annum.Text = (Offer_Total1 * 12).ToString("N2");
			//updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	protected void txt_Offer_PLP_variable_TextChanged(object sender, EventArgs e)
	{
		try
		{
			Generate_CTC_Per_Month();
			Generate_CTC_PLP_Percentage();
			//decimal Offer_CTC_Annum = string.IsNullOrEmpty(txt_Offer_CTC_Annum.Text) ? 0 : Convert.ToDecimal(txt_Offer_CTC_Annum.Text);
			//decimal Offer_PLP_variable = string.IsNullOrEmpty(txt_Offer_PLP_variable.Text) ? 0 : Convert.ToDecimal(txt_Offer_PLP_variable.Text);
			//txt_Offer_CTC_Annum_Incl_PLP.Text = (Offer_CTC_Annum + Offer_PLP_variable).ToString("N2");
			//updatePanel.Update();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}
	private void Offer_Compensation_Candidate()
	{
		try
		{
			DataTable DTResult = new DataTable();
			SqlParameter[] spars = new SqlParameter[4];
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Get_Offer_Pending_Candidate";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
			spars[2].Value = Convert.ToInt32(hdCandidate_ID.Value);
			spars[3] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
			spars[3].Value = Convert.ToString(Session["Empcode"].ToString()).Trim();
			DTResult = spm.getMobileRemDataList(spars, "SP_Rec_Generate_Offer_Candidate");
			GRDGenerate_Offer.DataSource = null;
			GRDGenerate_Offer.DataBind();
			//btn_Generate_Offer.Visible = true;
			//btn_Generate_Offer.Style.Add("display", "inline-block ");
			if (DTResult.Rows.Count > 0)
			{

				txtOfferno1.Text = Convert.ToDecimal(DTResult.Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				if (!string.IsNullOrEmpty(DTResult.Rows[0]["PLP_VARIABLE_PAY"].ToString()))
				{
					GRDGenerate_Offer.Columns[0].Visible = true;
					GRDGenerate_Offer.Columns[1].Visible = true;
                    HDnOfferDrftCopy.Value = "0";

                }
				else
				{
                    HDnOfferDrftCopy.Value = DTResult.Rows[0]["TempLatterID"].ToString();
                    GRDGenerate_Offer.Columns[0].Visible = false;
					GRDGenerate_Offer.Columns[1].Visible = false;
				}
				if ((DTResult.Rows[0]["Candidate_Status"].ToString() == "Accepted"))
				{
					GRDGenerate_Offer.Columns[6].Visible = true;
				}
				else
				{
					GRDGenerate_Offer.Columns[6].Visible = false;
				}
				GRDGenerate_Offer.DataSource = DTResult;
				GRDGenerate_Offer.DataBind();
				//btn_Generate_Offer.Visible = false;
				//btn_Generate_Offer.Style.Add("display", "none");
				txtOfferno1.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	protected void lnkGenerateFiles_Click(object sender, ImageClickEventArgs e)
	{
		try
		{
			int t1 = 12, t2 = 4;
			ImageButton btn = (ImageButton)sender;
			GridViewRow row = (GridViewRow)btn.NamingContainer;
			int TempLatterID = Convert.ToInt32(GRDGenerate_Offer.DataKeys[row.RowIndex].Values[0]);
			SqlParameter[] spars = new SqlParameter[4];
			DataSet dsOffer = new DataSet();
			spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
			spars[0].Value = "Get_generate_Offer_Details";
			spars[1] = new SqlParameter("@StructureID", SqlDbType.Int);
			spars[1].Value = TempLatterID;
			dsOffer = spm.getDatasetList(spars, "SP_Rec_Generate_Offer_Candidate");
			if (dsOffer.Tables[0].Rows.Count > 0)
			{
				txt_Off_Dis_Name.Text = dsOffer.Tables[0].Rows[0]["CandidateName"].ToString();
				txt_Off_Dis_Address.Text = dsOffer.Tables[0].Rows[0]["CandidateAddress"].ToString();
				//txt_Off_Dis_Location.Text = dsOffer.Tables[0].Rows[0]["Loc_code"].ToString();
				//txt_Off_Dis_Location.Text = dsOffer.Tables[0].Rows[0]["Particulars"].ToString();
				//txt_Off_Dis_Band.Text = lstOfferBand.SelectedItem.Text;
				//txt_Off_Dis_Designation.Text = txtpositionOffer.Text;
				txt_Off_Dis_Basic.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["BASIC"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				txt_Off_Dis_HRA.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["HRA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CONVEYANCE"].ToString()))
				{
					txt_Off_Dis_Conveyance.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CONVEYANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TConveyance.Visible = true;
				}
				else
				{
					TConveyance.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SPECIAL_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Special_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SPECIAL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TSpecial.Visible = true;
				}
				else
				{
					TSpecial.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ADHOC_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_ADHOC.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ADHOC_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TADHOC.Visible = true;
				}
				else
				{
					TADHOC.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SKILL_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Skill_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TSkill.Visible = true;
				}
				else
				{
					TSkill.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["SUPERAN_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Superann_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["SUPERAN_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TSuperannucation.Visible = true;
				}
				else
				{
					TSuperannucation.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Certificate_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CERTIFICATE_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					TCertificate.Visible = true;
				}
				else
				{
					TCertificate.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Multi.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MULTI_SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr1.Visible = true;
				}
				else
				{
					Tr1.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Additional_Skill.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ADDITIONAL_SKILL_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr2.Visible = true;
				}
				else
				{
					Tr2.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CAR_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Car_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CAR_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr3.Visible = true;
				}
				else
				{
					Tr3.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["FOOD_ALLOWANCE"].ToString()))
				{
					txt_Off_Dis_Food_All.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["FOOD_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr4.Visible = true;
				}
				else
				{
					Tr4.Visible = false;
					t1 = --t1;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["TOTAL1"].ToString()))
				{
					txt_Off_Dis_Total.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL1"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					
				}
				tb1.RowSpan = t1;
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["LTA"].ToString()))
				{
					txt_Off_Dis_LTA.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["LTA"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}

				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MEDICAL"].ToString()))
				{
					txt_Off_Dis_Medical.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MEDICAL"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr5.Visible = true;
				}
				else
				{
					Tr5.Visible = false;
					t2 = --t2;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["DRIVER_SALARY"].ToString()))
				{
					txt_Off_Dis_Driver_Salary.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["DRIVER_SALARY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr6.Visible = true;
				}
				else
				{
					Tr6.Visible = false;
					t2 = --t2;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CAR_LEASE"].ToString()))
				{
					txt_Off_Dis_Car_lease.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CAR_LEASE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr7.Visible = true;
				}
				else
				{
					Tr7.Visible = false;
					t2 = --t2;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["TOTAL2"].ToString()))
				{
					txt_Off_Dis_Total2.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL2"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				tb2.RowSpan = t2;
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PF"].ToString()))
				{
					txt_Off_Dis_PF.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["PF"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["GRATUITY_B"].ToString()))
				{
					txt_Off_Dis_Gratuity.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["GRATUITY_B"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["TOTAL3"].ToString()))
				{
					txt_Off_Dis_Total3.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL3"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["MEDICLAIM"].ToString()))
				{
					txt_Off_Dis_Mediclaim.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["MEDICLAIM"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}

				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["GROUP_ACC_POLICY"].ToString()))
				{
					txt_Off_Dis_Group_Acc_Po.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["GROUP_ACC_POLICY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["JOINING_BONUS"].ToString()))
				{
					txt_Off_Dis_Joining_Bonus.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["JOINING_BONUS"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr8.Visible = true;
				}
				else
				{
					Tr8.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["TOTAL4"].ToString()))
				{
					txt_Off_Dis_Total4.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["TOTAL4"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CTC_PER_MONTH"].ToString()))
				{
					txt_Off_Dis_CTC_Per_Month.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_MONTH"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM"].ToString()))
				{
					txt_Off_Dis_CTC_Per_Annum.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()))
				{
					txt_Off_Dis_PLP_Variable_Pay.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_PAY"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_Percentage"].ToString()))
				{
					txt_Off_Dis_Percentage.Text = dsOffer.Tables[0].Rows[0]["PLP_VARIABLE_Percentage"].ToString();
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()))
				{
					txt_Off_Dis_CTC_Per_Annum_PLP.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["CTC_PER_ANNUM_INCLUDING_PLP"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
				}

				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ANNUAL_BONUS"].ToString()))
				{
					txt_Off_Dis_Annual_Bonus.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ANNUAL_BONUS"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr9.Visible = true;
				}
				else
				{
					Tr9.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["OTHER"].ToString()))
				{
					txt_Off_Dis_Other.Text = dsOffer.Tables[0].Rows[0]["OTHER"].ToString();
					Tr10.Visible = true;
				}
				else
				{
					Tr10.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString()))
				{
					txt_Dis_Joining_Remark.Text = dsOffer.Tables[0].Rows[0]["Joining_Comment"].ToString();
					Tr8.Visible = true;
				}
				else
				{
					Tr8.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ALP_ALLOWANCE"].ToString()))
				{
					
                              txt_Dis_ALP_Amount.Text = Convert.ToDecimal(dsOffer.Tables[0].Rows[0]["ALP_ALLOWANCE"].ToString()).ToString("N2", CultureInfo.CreateSpecificCulture("hi-IN"));
					Tr12.Visible = true;
				}
				else
				{
					Tr12.Visible = false;
				}
				if (!string.IsNullOrEmpty(dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString()))
				{
					txt_Dis_ALP_Remark.Text = dsOffer.Tables[0].Rows[0]["ALP_Comment"].ToString();
					Tr12.Visible = true;
				}
				else
				{
					Tr12.Visible = false;
				}

                if (dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString()=="" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString() == "0.00")
                {

                    TD_HEALTHCHECKUP1.Visible = false;
                    TD_HEALTHCHECKUP2.Visible = false;
                    TD_HEALTHCHECKUP3.Visible = false;
                }
                else
                {
                    txt_Off_Dis_HEALTHCHECKUP.Text = dsOffer.Tables[0].Rows[0]["health_Check_Up"].ToString();
                    TD_HEALTHCHECKUP1.Visible = true;
                    TD_HEALTHCHECKUP2.Visible = true;
                    TD_HEALTHCHECKUP3.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString() == "0.00")
                {
                    TR_CARHIRECOST.Visible = false;
                }
                else
                {
                    txt_Off_Dis_CARHIRECOST.Text = dsOffer.Tables[0].Rows[0]["Car_Hire_Cost"].ToString();
                    TR_CARHIRECOST.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString() == "0.00")
                {
                    TR_CAREXPENSESREIMBURSEMENT.Visible = false;
                }
                else
                {
                    txt_Off_Dis_CAREXPENSESREIMBURSEMENT.Text = dsOffer.Tables[0].Rows[0]["Car_Expenses_Reimursement"].ToString();
                    TR_CAREXPENSESREIMBURSEMENT.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString() == "0.00")
                {
                    TR_CARFUELEXPENSESREIMBURSEMENT.Visible = false;
                }
                else
                {
                    txt_Off_Dis_CARFUELEXPENSESREIMBURSEMENT.Text = dsOffer.Tables[0].Rows[0]["Car_FUEL_Expenses_Reimursement"].ToString();
                    TR_CARFUELEXPENSESREIMBURSEMENT.Visible = true;
                }

                if (dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "" || dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "0" || dsOffer.Tables[0].Rows[0]["Total5"].ToString() == "0.00")
                {
                    
                    Tr_Total5.Visible = false;
                }
                else
                {
                    txt_Off_Dis_Total5.Text = dsOffer.Tables[0].Rows[0]["Total5"].ToString();
                    Tr_Total5.Visible = true;
                }



                SP_Dis_HRA.InnerText = dsOffer.Tables[0].Rows[0]["RHR"].ToString();
				SP_Dis_Supperannum.InnerText = dsOffer.Tables[0].Rows[0]["RSUPERAN"].ToString();
				SP_dis_LTA.InnerText = dsOffer.Tables[0].Rows[0]["RLTA"].ToString();
				SP_Dis_PF.InnerText = dsOffer.Tables[0].Rows[0]["RPF"].ToString();
				SP_Dis_Gratuity.InnerText = dsOffer.Tables[0].Rows[0]["RGRA"].ToString();

                this.ModalPopupExtenderDisplayOffer.Show();

			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

	public void Get_OfferEmployeeType()
	{
		DataTable dtEmployeeType = new DataTable();
		try
		{
			SqlParameter[] spars = new SqlParameter[1];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_Req_Candi_Employment_Type";
			dtEmployeeType = spm.getMobileRemDataList(spars, "SP_GETREQUISTIONLIST_DETAILS");
			if (dtEmployeeType.Rows.Count > 0)
			{
				ddl_Offer_EmploymentType.DataSource = dtEmployeeType;
				ddl_Offer_EmploymentType.DataTextField = "Particulars";
				ddl_Offer_EmploymentType.DataValueField = "PID";
				ddl_Offer_EmploymentType.DataBind();
				ddl_Offer_EmploymentType.Items.Insert(0, new ListItem("Select Employee Type", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}


	protected void lnk_Offer_Cancle_Click(object sender, EventArgs e)
	{
		ClearValues();
	}
	private void ClearValues()
	{
		txtOffer_Can_Name.Text = string.Empty;
		txtCandAddress.Text = string.Empty;
		//ddl_Offer_EmploymentType.SelectedIndex = -1;
		txt_Offer_Basic.Text = string.Empty;
		txt_Offer_HRA.Text = string.Empty;

		txt_Offer_Conveyance.Text = string.Empty;
		txt_Offer_ADHOC_Allowance.Text = string.Empty;
		txt_Offer_Special_Allowance.Text = string.Empty;
		txt_offer_Skill_Allowance.Text = string.Empty;
		txt_Offer_Superannucation.Text = string.Empty;
		txt_Offer_Certificate_All.Text = string.Empty;
		txt_Offer_Multi_Skill_All.Text = string.Empty;
		txt_Offer_Additional_Skill.Text = string.Empty;
		txtCar_All.Text = string.Empty;
		txt__Offer_Food_All.Text = string.Empty;
		txt_Offer_Total1.Text = string.Empty;
		txt_Offer_LTA.Text = string.Empty;
		txt_offer_Medical.Text = string.Empty;
		txt_Offer_Driver_Salary.Text = string.Empty;
		txt_Offer_Car_lease.Text = string.Empty;
		txt_Offer_Total2.Text = string.Empty;
		txt_Offer_PF.Text = string.Empty;
        txt_Offer_Car_Expenses_Reimbursment.Text = string.Empty;
        txt_offer_Car_Hire_Cost.Text = string.Empty;
        txt_Car_Fuel_Expenses_Reimbursment.Text = string.Empty;

        txt_Offer_Gratuity.Text = string.Empty;
		txt_Offer_Total3.Text = string.Empty;
		txt_Offer_Mediclaim.Text = string.Empty;
		txt_Offer_Group_Policy.Text = string.Empty;
		txtTotal4.Text = string.Empty;
        txtTotal5.Text = string.Empty;
        txt_Offer_CTC_Month.Text = string.Empty;
		txt_Offer_CTC_Annum.Text = string.Empty;
		txt_Offer_PLP_variable.Text = string.Empty;
		txt_Offer_PLP_Per.Text = string.Empty;
		txt_Offer_CTC_Annum_Incl_PLP.Text = string.Empty;
		txt_Retention_Bonus.Text = string.Empty;
		txt_Annual_Bonus.Text = string.Empty;
		txt_Offer_Other.Text = string.Empty;
		txt_Retention_Comment.Text = string.Empty;
		txt_ALP_Comment.Text = string.Empty;
		txt_ALP_Amount.Text = string.Empty;
		//SP_HRA.InnerText = string.Empty;
		//hdn_HRA_Per.Value = string.Empty;
		//hdn_HRA_StructureID.Value = string.Empty;
		//SP_Superannucation_All.InnerText = string.Empty;
		//hdn_Superan_All.Value = string.Empty;
		//hdn_Superan_All_StructureID.Value = string.Empty;
		//SP_LTA.InnerText = string.Empty;
		//hdn_LTA.Value = string.Empty;
		//hdn_LTA_StructureID.Value = string.Empty;
		//SP_PF.InnerText = string.Empty;
		//hdn_PF.Value = string.Empty;
		//hdn_PF_StructureID.Value = string.Empty;
		//SP_Gratuity.InnerText = string.Empty;
		//hdn_Gratuity.Value = string.Empty;
		//hdn_Gratuity_StructureID.Value = string.Empty;
		//hdn_band.Value = string.Empty;
		//hdn_Skill_ALL_StructureID.Value = string.Empty;
		updatePanel.Update();
		ModalPopupExtenderGenerateOffer.Show();
	}

	private void HideTextBox()
	{
		txtOffer_Can_Name.Enabled = false;
		txt_Offer_Basic.Enabled = false;
		txt_Offer_HRA.Enabled = false;
		txt_Offer_Conveyance.Enabled = false;
		txt_Offer_ADHOC_Allowance.Enabled = false;
		txt_Offer_Special_Allowance.Enabled = false;
		txt_offer_Skill_Allowance.Enabled = false;
		txt_Offer_Superannucation.Enabled = false;
		txt_Offer_Certificate_All.Enabled = false;
		txt_Offer_Multi_Skill_All.Enabled = false;
		txt_Offer_Additional_Skill.Enabled = false;
		txtCar_All.Enabled = false;
		txt__Offer_Food_All.Enabled = false;
		txt_Offer_Total1.Enabled = false;
		txt_Offer_LTA.Enabled = false;
		txt_offer_Medical.Enabled = false;
		txt_Offer_Driver_Salary.Enabled = false;
		txt_Offer_Car_lease.Enabled = false;
		txt_Offer_Total2.Enabled = false;
		txt_Offer_PF.Enabled = false;
		txt_Offer_Gratuity.Enabled = false;
		txt_Offer_Total3.Enabled = false;
		txt_Offer_Mediclaim.Enabled = false;
		txt_Offer_Group_Policy.Enabled = false;
		txtTotal4.Enabled = false;
		txt_Offer_CTC_Month.Enabled = false;
		txt_Offer_CTC_Annum.Enabled = false;
		txt_Offer_PLP_variable.Enabled = false;
		txt_Offer_PLP_Per.Enabled = false;
		txt_Offer_CTC_Annum_Incl_PLP.Enabled = false;
		txt_Retention_Bonus.Enabled = false;
		txt_Annual_Bonus.Enabled = false;
		txt_Offer_Other.Enabled = false;
		updatePanel.Update();
		ModalPopupExtenderGenerateOffer.Show();
	}

	protected void ddl_Offer_EmploymentType_SelectedIndexChanged(object sender, EventArgs e)
	{
		try
		{
            if (lstOfferBand.SelectedItem.Text.Trim() == "I" || lstOfferBand.SelectedItem.Text.Trim() == "II" || lstOfferBand.SelectedItem.Text.Trim() == "III" || lstOfferBand.SelectedItem.Text.Trim() == "IV" || lstOfferBand.SelectedItem.Text.Trim() == "V")
            {
                txt_Offer_Health_Checkup.Visible = false;
                Span_Helthcheckup.Visible = false;
                Span_Helthcheckup1.Visible = false;
            }
            else
            {
                txt_Offer_Health_Checkup.Visible = true;
                Span_Helthcheckup.Visible = true;
                Span_Helthcheckup1.Visible = true;
            }

            if (lstOfferBand.SelectedItem.Text.Trim() == "I" || lstOfferBand.SelectedItem.Text.Trim() == "II" || lstOfferBand.SelectedItem.Text.Trim() == "III" || lstOfferBand.SelectedItem.Text.Trim() == "IV" || lstOfferBand.SelectedItem.Text.Trim() == "V" || lstOfferBand.SelectedItem.Text.Trim() == "VI")
            {
                TRHide1.Visible = false;
                TRHide2.Visible = false;
                TRHide3.Visible = false;
                TRHide4.Visible = false;
            }
            else
            {
                TRHide1.Visible = true;
                TRHide2.Visible = true;
                TRHide3.Visible = true;
                TRHide4.Visible = true;
            }

            lblOffer.Text = "";

			if (ddl_Offer_EmploymentType.SelectedIndex > 0)
			{
				Cal_Offer_Generate();
			}
			else
			{
				btn_Generate_Offer.Style.Add("display", "none");
                btn_ViewDraft_Offer.Style.Add("display", "none");
            }
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void Cal_Offer_Generate()
	{
		try
		{
			int EmploymentTypeID = 0;
			string Band = string.Empty;
			DataTable DTResult = new DataTable();
			ClearValues();
			if (ddl_Offer_EmploymentType.SelectedValue == "1")
			{
				FileSupport.Visible = false;
				if (txtOfferDate.Text == "")
				{
					lblOffer.Text = "Please enter offer Date";
					ModalPopupExtenderGenerateOffer.Hide();
					return;
				}
				if (lstOfferPositionName.SelectedValue == "0" || lstOfferPositionName.SelectedValue == "")
				{
					lblOffer.Text = "Please select offer Position Name";
					ModalPopupExtenderGenerateOffer.Hide();
					return;
				}
				if (lstOfferLocation.SelectedValue == "0" || lstOfferLocation.SelectedValue == "")
				{
					lblOffer.Text = "Please select offer Position Location";
					ModalPopupExtenderGenerateOffer.Hide();
					return;
				}
                if (lstOfferOfficeLocation.SelectedValue == "0" || lstOfferOfficeLocation.SelectedValue == "")
                {
                    lblOffer.Text = "Please select offer Office Location"; 
                    ModalPopupExtenderGenerateOffer.Hide();
                    return;
                }

                if (lstOfferBand.SelectedValue == "0" || lstOfferBand.SelectedValue == "")
				{
					lblOffer.Text = "Please select offer Band";
					ModalPopupExtenderGenerateOffer.Hide();
					return;
				}
				EmploymentTypeID = Convert.ToInt32(ddl_Offer_EmploymentType.SelectedValue);
				Band = Convert.ToString(lstOfferBand.SelectedValue);
				txtOfferno1.Enabled = false;
				FileSupport1.InnerHtml = "Other Files";
				DTResult = spm.Get_Generate_Offer_Count("Get_generate_Offer_Count_Last", 0, Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value), Convert.ToString(Session["Empcode"]));
				if (DTResult.Rows.Count > 0)
				{
					//int ss = Convert.ToInt32(DTResult.Rows[0][""].ToString());
					Get_Candidate_Offer_Gemerate_Details(DTResult);
					Get_Offer_Compensation_Parameters(EmploymentTypeID, Band);
				}
				else
				{
					Get_Offer_Compensation_Parameters(EmploymentTypeID, Band);//Offer generation
				}
               // GenerateViewDraftOffer();
                btn_Generate_Offer.Style.Add("display", "inline-block");
                ModalPopupExtenderGenerateOffer.Show();

            }
			else
			{
				ModalPopupExtenderGenerateOffer.Hide();
				txtOfferno1.Enabled = true;
				FileSupport.Visible = true;
				FileSupport1.InnerHtml = "Upload Files";
				//txtOfferno1.Text = "";
				btn_Generate_Offer.Style.Add("display", "none");
                btn_ViewDraft_Offer.Style.Add("display", "none");
            }
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	protected void lstOfferBand_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lstOfferBand.SelectedIndex > 0)
		{
            if (lstOfferBand.SelectedItem.Text.Trim() == "I" || lstOfferBand.SelectedItem.Text.Trim() == "II" || lstOfferBand.SelectedItem.Text.Trim() == "III" || lstOfferBand.SelectedItem.Text.Trim() == "IV" || lstOfferBand.SelectedItem.Text.Trim() == "V")
            {
                txt_Offer_Health_Checkup.Visible = false;
                Span_Helthcheckup.Visible = false;
                Span_Helthcheckup1.Visible = false;
            }
            else
            {
                txt_Offer_Health_Checkup.Visible = true;
                Span_Helthcheckup.Visible = true;
                Span_Helthcheckup1.Visible = true;
            }

            if (lstOfferBand.SelectedItem.Text.Trim() == "I" || lstOfferBand.SelectedItem.Text.Trim() == "II" || lstOfferBand.SelectedItem.Text.Trim() == "III" || lstOfferBand.SelectedItem.Text.Trim() == "IV" || lstOfferBand.SelectedItem.Text.Trim() == "V" || lstOfferBand.SelectedItem.Text.Trim() == "VI")
            {
                TRHide1.Visible = false;
                TRHide2.Visible = false;
                TRHide3.Visible = false;
                TRHide4.Visible = false;
            }
            else
            {
                TRHide1.Visible = true;
                TRHide2.Visible = true;
                TRHide3.Visible = true;
                TRHide4.Visible = true;
            }




            if (txtOfferDate.Text == "")
			{
				lblOffer.Text = "Please enter offer Date";
				return;
			}
			if (lstOfferPositionName.SelectedValue == "0" || lstOfferPositionName.SelectedValue == "")
			{
				lblOffer.Text = "Please select offer Position Name";
				return;
			}
			if (lstOfferLocation.SelectedValue == "0" || lstOfferLocation.SelectedValue == "")
			{
				lblOffer.Text = "Please select offer Position Location";
				return;
			}
            if (lstOfferOfficeLocation.SelectedValue == "0" || lstOfferOfficeLocation.SelectedValue == "")
            {
                lblOffer.Text = "Please select offer Office Location"; 
                return;
            }
            Cal_Offer_Generate();
		}
	}

	protected void lnkOffersFiles_Click(object sender, ImageClickEventArgs e)
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
			if (dsOffer.Tables[0].Rows.Count > 0)
			{
				LocalReport ReportViewer1 = new LocalReport();
				string filename = "";
				string dsysdate = DateTime.Now.ToString("ddMMyyyy_HHmmss");
				filename = dsOffer.Tables[0].Rows[0]["Candidate_ID"].ToString();
				filename = filename + "_" + dsysdate + ".PDF";
				ReportViewer1.ReportPath = Server.MapPath("~/procs/Candidate_Offer_Accepted.rdlc");
				ReportDataSource rds = new ReportDataSource("EmployeeD", dsOffer.Tables[1]);
				ReportDataSource rd1 = new ReportDataSource("Compensation", dsOffer.Tables[0]);
				ReportViewer1.DataSources.Clear();
				ReportViewer1.DataSources.Add(rds);
				ReportViewer1.DataSources.Add(rd1);
				Warning[] warnings;
				string[] streamIds;
				string mimeType = string.Empty;
				string encoding = string.Empty;
				string extension = string.Empty;
				byte[] bytes = ReportViewer1.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
				Response.Buffer = true;
				Response.Clear();
				Response.ContentType = mimeType;
				Response.AddHeader("content-disposition", "attachment; filename=" + filename);
				Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
				Response.Flush(); // send it to the client to download  
				Response.End();
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

	private void Joining_Extended_Revised_Offer_Letter()
	{
		try
		{
			string FileName = "";
			DataTable DTResult = new DataTable();
			DataTable DTOffer = new DataTable();
			string RecruiterName = "", RecruiterEmail = "", Recruitermobile = "", Designation = "", ReleaseDate = "";
			string[] strdate1;
			string strtoDate = "", OldDate = "", NewDate = "";
			DateTime dsysdate = DateTime.Now.Date;
			var year = dsysdate.Year;
			var month = dsysdate.Month;
			var day = dsysdate.Day;
			int Offer_App_ID = 0, Candidate_ID = 0, Recruitment_ReqID = 0;
			Recruitment_ReqID = Convert.ToInt32(hdRecruitment_ReqID.Value);
			Candidate_ID = Convert.ToInt32(hdCandidate_ID.Value);
			if (Txt_JoiningDate.Text != "")
			{

				strdate1 = Convert.ToString(Txt_JoiningDate.Text).Trim().Split('/');
				strtoDate = Convert.ToString(strdate1[2]) + "-" + Convert.ToString(strdate1[1]) + "-" + Convert.ToString(strdate1[0]);
				DateTime ddt = DateTime.ParseExact(strtoDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
				NewDate = Convert.ToString(Txt_JoiningDate.Text).Replace("/", "-");
			}
			DTResult = spm.Get_Generate_Offer_Count("Get_Check_Offer_Candidate", 0, Recruitment_ReqID, Candidate_ID, Convert.ToString(Session["Empcode"]));
			if (DTResult.Rows.Count > 0)
			{
				DataSet DSCandidateLogin = new DataSet();
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
				DSCandidateLogin = spm.getDatasetList(spars, "SP_ELC_CandidateLoginDetails");

				if (DSCandidateLogin.Tables[0].Rows[0]["Records"].ToString() == "Insert")
				{
					var emailCC = "";
					if (DSCandidateLogin.Tables[1].Rows.Count > 0)
					{
						var RecruiterEmails = DSCandidateLogin.Tables[1].Rows[0]["RecruiterEmail"].ToString();
						var RecruitmentHeadEmail = DSCandidateLogin.Tables[1].Rows[0]["RecruitmentHeadEmail"].ToString();
						{
							emailCC = RecruiterEmails + ";" + RecruitmentHeadEmail;
						}

					}

					FileName = Convert.ToString(Candidate_ID).Trim() + "_" + day + "." + month + "." + year + ".pdf"; //"testing.pdf";
					Offer_App_ID = Convert.ToInt32(DTResult.Rows[0]["Offer_App_ID"].ToString());
					OldDate = Convert.ToString(DTResult.Rows[0]["ProbableJoiningDate"].ToString());
					DTOffer = spm.Set_Generate_Offer_FileName("Insert_Revised_Offer_Letter", Offer_App_ID, Recruitment_ReqID, Candidate_ID, Convert.ToString(Session["Empcode"]), FileName, strtoDate);
					if (DTResult.Rows.Count > 0)
					{
						DataSet Offerlatter = new DataSet();
						SqlParameter[] spars1 = new SqlParameter[5];
						spars1[0] = new SqlParameter("@qtype", SqlDbType.NVarChar);
						spars1[0].Value = "Get_Offer_Candidate_Details";
						spars1[1] = new SqlParameter("@Offer_App_ID", SqlDbType.Int);
						spars1[1].Value = Convert.ToInt32(Offer_App_ID);
						spars1[2] = new SqlParameter("@EmpCode", SqlDbType.NVarChar);
						spars1[2].Value = Session["Empcode"].ToString();
						Offerlatter = spm.getDatasetList(spars1, "SP_Rec_Generate_Offer_Candidate");
						if (Offerlatter.Tables[0].Rows.Count > 0)
						{
							RecruiterName = Offerlatter.Tables[0].Rows[0]["RecruiterName"].ToString();
							RecruiterEmail = Offerlatter.Tables[0].Rows[0]["RecruiterEmail"].ToString();
							Recruitermobile = Offerlatter.Tables[0].Rows[0]["Recruitermobile"].ToString();
							Designation = Offerlatter.Tables[0].Rows[0]["PositionTitle"].ToString();
							//ReleaseDate = Offerlatter.Tables[0].Rows[0]["ReleaseDate"].ToString();
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
							string mailsubject = "";
							string mailcontain = "";
							string strCandidateLoginURL = "";
							
							mailsubject = "Revised Offer Letter with new joining date " + NewDate + " - Highbar Technocrat Limited";
							mailcontain = "Please follow these instructions for logging in to the system";
							DateTime D1 = DateTime.Now.Date;
							D1 = D1.AddDays(3);
							var Y1 = D1.Year;
							var M1 = D1.Month;
							var D3 = D1.Day;
							ReleaseDate = D3 + "-" + M1 + "-" + Y1;
							string sattchedfileName = Server.MapPath(ConfigurationManager.AppSettings["CandidateOfferLatter"]) + FileName;
							//string strCandidateLoginURL = Convert.ToString(ConfigurationManager.AppSettings["Link_CandidateLogin"]).Trim();
							strCandidateLoginURL = Convert.ToString(ConfigurationManager.AppSettings["Link_Offer_Accept"]).Trim() + "?Offer_App_ID=" + Offer_App_ID;
							spm.send_mailto_CandidateLoginDetail_ELC_Revised_Offer(StrCanName.Trim(), txtEmail.Text.Trim(), txtEmail.Text.Trim(), mailsubject, Candidatemailpwd.ToString(), mailcontain, strCandidateLoginURL, emailCC, sattchedfileName, Designation, ReleaseDate, RecruiterName, RecruiterEmail, Recruitermobile);
							string RequiredByDate = "", HODMail = "";
							RequiredByDate = GetRequiredByDate();
							DataTable dtEmail = new DataTable();
							var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
							var getcompSelectedval = Convert.ToString(hdncomp_code.Value);
							var Dept_id = 0;
							var qtype = "1";
							if (getcompSelectedText.Contains("Head Office"))
							{
								Dept_id = Convert.ToInt32(hdndept_Id.Value);
								qtype = "0";
							}
							dtEmail = spm.GetRequisition_OffER_Project_Detail(qtype,0, 123, "", getcompSelectedval, Dept_id);
							if (dtEmail.Rows.Count > 0)
							{
								HODMail = dtEmail.Rows[0]["Emp_Emailaddress"].ToString();
							}
							mailsubject = "OneHR - Recruitment: Joining Date Extended for Candidate - " + txtName.Text + " for Requisition No -" + txtReqNumber.Text;
							mailcontain = "This is to inform you that Joining Date for Candidate - " + txtName.Text + " is Extended from " + OldDate + " to " + NewDate;
							spm.send_mailto_Recruiter_Joining_Extended(txtReqName.Text, "", emailCC, mailsubject, txtName.Text, Txt_JoiningDate.Text, DDLStatusUpdate.SelectedItem.Text, txtRecruitercomment.Text, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text,
						   lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, RecruiterName, HODMail);

						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
		}
	}

	protected void txt_Offer_Special_Allowance_TextChanged(object sender, EventArgs e)
	{
		Generate_Offer_Basic();
		Generate_Retirals_Taxability();
		Generate_Offer_PF();
		Generate_Offer_Facilities();
		Generate_CTC_Per_Month();
		Generate_CTC_PLP_Percentage();
        Generate_Offer_Car_ExpencesCost();
    }
    #endregion



    protected void txtProbableJoiningDate_TextChanged(object sender, EventArgs e)
    {
        if (txtProbableJoiningDate.Text == "")
        {
        }
        else
        {
            if (ddl_Offer_EmploymentType.SelectedValue == "1")
            {
                GenerateViewDraftOffer();
                updatePanel3.Update();
                btn_ViewDraft_Offer.Style.Add("display", "inline-block");
            }
            else
            {

            }
        }
    }

    protected void btn_ViewDraft_Offer_Click(object sender, EventArgs e)
    {
        
        
        btn_ViewDraft_Offer.Style.Add("display", "inline-block");
        ModalPopupExtenderGenerateOfferDraft.Show();
    }

    protected void txt_Offer_Health_Checkup_TextChanged(object sender, EventArgs e)
    {
        Generate_Offer_Facilities();
        Generate_CTC_Per_Month();
        Generate_CTC_PLP_Percentage();
    }

    protected void txt_offer_Car_Hire_Cost_TextChanged(object sender, EventArgs e)
    {
        Generate_Offer_Car_ExpencesCost();
        Generate_CTC_Per_Month();
        Generate_CTC_PLP_Percentage();
    }

    protected void txt_Offer_Car_Expenses_Reimbursment_TextChanged(object sender, EventArgs e)
    {
        Generate_Offer_Car_ExpencesCost();
        Generate_CTC_Per_Month();
        Generate_CTC_PLP_Percentage();
    }

    protected void txt_Car_Fuel_Expenses_Reimbursment_TextChanged(object sender, EventArgs e)
    {
        Generate_Offer_Car_ExpencesCost();
        Generate_CTC_Per_Month();
        Generate_CTC_PLP_Percentage();
    }
}