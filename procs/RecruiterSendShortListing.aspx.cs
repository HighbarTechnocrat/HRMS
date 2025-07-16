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

public partial class procs_RecruiterSendShortListing : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public int did = 0;
	SP_Methods spm = new SP_Methods();
	DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource, dtIrSheetReport;
	public DataTable dtRecCandidate, dtcandidateDetails, dtmainSkillSet, dtInterviewer1, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
	public string filename = "";
	public string InterviewernameEmail = "";

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
			   
			if (Page.User.Identity.IsAuthenticated == false)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
					//TxtExpectedCTCSerchFrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//TxtExpectedCTCSerchTo.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//txtExperienceYearSearchFrom.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					//txtExperienceYearSearchTo.Attributes.Add("onkeypress", "return onCharOnlyNumber(event);");
					
					//ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ASD", "StartLoader();", true);
					hdfilefath.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_CandidateResumeDocpath"]).Trim());
					hdRecruitment_ReqID.Value = Request.QueryString["Recruitment_ReqID"];
					if (Request.QueryString.Count == 2)
					{
						HDFlagCheckBackPage.Value = Convert.ToString(Request.QueryString[1]);
					}
					else
					{
						checkInterviewerShortlistStatus_Submit();
					}
					RecruiterSendShortListingInterviewer();

					if (HDFlagCheckBackPage.Value == "0")
					{
						DDLAssignInterviewSchedulars.Enabled = false;
					}


					getMainSkillsetView();
					getCVSource();
					GetCandidateInfoRecruitmentwisedataBind();
					GetSkillsetName();
					GetPositionName();
					GetPositionCriticality();
					GetDepartmentMaster();
					GetCompany_Location();
					GetReasonRequisition();
					GetPositionDesign();
					GetPreferredEmpType();
					GetlstPositionBand();
					Get_InterviewerSchedulars();
					if (Request.QueryString.Count == 2)
					{
						HDFlagCheckBackPage.Value = Convert.ToString(Request.QueryString[1]);
					}
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

	protected void lnkEdit_Click(object sender, ImageClickEventArgs e)
	{
		ImageButton btn = (ImageButton)sender;
		GridViewRow row = (GridViewRow)btn.NamingContainer;
		hdCandidate_ID.Value = Convert.ToString(gvSearchCandidateList.DataKeys[row.RowIndex].Values[0]).Trim();
		PopulateCandidateData();
		GetRequisitiodetailsCandidate();
		DivViewrowWiseCandidateInformation.Visible = true;
		JobDetail_btnSave.Visible = true;
		// mobile_cancel.Visible = true;
		trvldeatils_btnSave.Visible = true;
		mobile_cancel.Visible = false;
	}

	protected void mobile_btnBack_Click(object sender, EventArgs e)
	{

		Txt_CandidateEmail.Text = "";
		Txt_CandidateName.Text = "";
		DDLEducationQualification.SelectedValue = "";
		gvSearchCandidateList.DataSource = null;
		gvSearchCandidateList.DataBind();


		DivInterviewer1.Visible = false;
		getMainSkillsetView();
		DivViewrowWiseCandidateInformation.Visible = false;
		// TxtEducationQualificationSearch.Text = "";
	}

	protected void mobile_btnSave_Click(object sender, EventArgs e)
	{
		hdnSaveStatusFlag.Value = "1";
		getMngCandidateInfoList();
		DivViewrowWiseCandidateInformation.Visible = false;

	}

	protected void trvl_localbtn_Click(object sender, EventArgs e)
	{
		if (DivSendshortlisting1.Visible)
		{
			DivSendshortlisting1.Visible = false;
			DivSendshortlisting2.Visible = false;
			DivSendshortlisting3.Visible = false;
			trvl_localbtn.Text = "+";
			DivInterviewer1.Visible = false;
			DivViewrowWiseCandidateInformation.Visible = false;
			btnTra_Details.Text = "+";
			DivRecruitment.Visible = false;
		}
		else
		{

			btnTra_Details.Text = "+";
			DivRecruitment.Visible = false;
			trvl_localbtn.Text = "-";
			DivSendshortlisting1.Visible = true;
			DivSendshortlisting2.Visible = true;
			DivSendshortlisting3.Visible = true;
			if (hdnSaveStatusFlag.Value == "1")
			{
				DivInterviewer1.Visible = true;
			}
			else
			{
				DivInterviewer1.Visible = false;
			}
		}


	}

	//  View click button in GridView perticular row below All Codeing

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
		if (HDFlagCheckBackPage.Value == "")
		{
			DivViewrowWiseCandidateInformation.Visible = false;
			JobDetail_btnSave.Visible = false;
			trvldeatils_btnSave.Visible = false;
			lblmessage.Text = "";
			lblmessageOnsave.Text = "";
			validationCheckforSendforshortlisting();
		}
		else
		{
			Response.Redirect("~/procs/RecruiterViewEdit.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value);
		}

	}

	protected void mobile_cancel_Click(object sender, EventArgs e)
	{
		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			lblmessage.Text = "";
			string MultipleCandidateName = "";
			string Oneselectrecord = "";

			if (DDLAssignInterviewSchedulars.SelectedValue == "0" || DDLAssignInterviewSchedulars.SelectedValue == "")
			{
				lblmessage.Text = "Please enter Interview Schedular";
				return;
			}

			if (lstInterviewerOne.SelectedValue == "0")
			{
				lblmessage.Text = "Please enter Screening By";
				return;
			}
			else
			{
				DataSet dsRecruitmentDetails = new DataSet();
				SqlParameter[] spars = new SqlParameter[3];
				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "RecruitmentReq_ViewRecruiter";
				spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
				spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
				spars[2].Value = Session["Empcode"].ToString();
				dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

				foreach (GridViewRow gvrow in gvSearchCandidateList.Rows)
				{
					CheckBox chk = (CheckBox)gvrow.FindControl("lstboxChecksendforshortlisting");
					hdCandidate_ID.Value = Convert.ToString(gvSearchCandidateList.DataKeys[gvrow.RowIndex].Values[0]).Trim();
					if (chk.Checked == true)
					{
						Oneselectrecord = "Selectrecord";
						if (dsRecruitmentDetails.Tables[6].Rows.Count > 0)
						{
							DataRow[] dr1 = dsRecruitmentDetails.Tables[6].Select("Candidate_ID='" + hdCandidate_ID.Value + "'");
							if (dr1.Length > 0)
							{
							}
							else
							{
								lblmessage.Text = "Please Fill up the other information";
								break;
							}
						}

					}
				}

				if (Oneselectrecord == "")
				{
					lblmessage.Text = "Please select candidate Send for Shortlisting";
				}
				if (lblmessage.Text == "")
				{
					string InterviewerSchedularEmail = "";
					string Interviewermail = "";
					string emailCCRecruter = "";
					int CountCTCException = 0;
					foreach (GridViewRow gvrow in gvSearchCandidateList.Rows)
					{
						DataTable dt = new DataTable();
						CheckBox chk = (CheckBox)gvrow.FindControl("lstboxChecksendforshortlisting");
						hdCandidate_ID.Value = Convert.ToString(gvSearchCandidateList.DataKeys[gvrow.RowIndex].Values[0]).Trim();
						if (chk.Checked == true)
						{
							dt = spm.InsertSendForShortlisting(lstInterviewerOne.SelectedValue, Convert.ToString(Session["Empcode"]).Trim(), hdRecruitment_ReqID.Value, hdCandidate_ID.Value, DDLAssignInterviewSchedulars.SelectedValue);
							InterviewerSchedularEmail = dt.Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString();
							Interviewermail = dt.Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString();
							emailCCRecruter = dt.Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString();
							CountCTCException =Convert.ToInt32(dt.Rows[0]["CountCTCException"].ToString());

							CheckReferral_Candidated(hdCandidate_ID.Value);
							CTCException_Candidated(hdRecruitment_ReqID.Value,hdCandidate_ID.Value);
							if (CountCTCException>0)
							{
								HDNCTCException.Value =Convert.ToString(CountCTCException);
							}
						}
					}

					DataTable dttempdelete = spm.InsertSendForShortlisting(lstInterviewerOne.SelectedValue, Convert.ToString(Session["Empcode"]).Trim(), hdRecruitment_ReqID.Value, "0", DDLAssignInterviewSchedulars.SelectedValue);
					string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_InterviewerShortList"]).Trim() + "?ReqID=" + hdRecruitment_ReqID.Value;
					string RequiredByDate = "";
					RequiredByDate = GetRequiredByDate();
					if (HDNCTCException.Value != "")
					{
						string mailsubject = "Recruitment - Shortlist Candidates against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
						string mailcontain = "has applied for new requisition as per the details below. Please shortlist candidates";
						spm.send_mailto_RecruiterSendShortListingfor_EnterviewerRecruiter(txtReqName.Text, emailCCRecruter, Interviewermail, InterviewerSchedularEmail, mailsubject, MultipleCandidateName, lstInterviewerOne.SelectedItem.Text, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionDesign.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, "", strLeaveRstURL, mailcontain);
					}
					if (HDFlagCheckBackPage.Value == "")
					{
						Response.Redirect("~/procs/Requisition_Index.aspx");
					}
					else
					{
						Response.Redirect("~/procs/RecruiterViewEdit.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value);
					}
				}
			}


		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}

	}
	private void CTCException_Candidated(string Recruitment_ReqID, string CandidatedID)
	{
		DataTable dtCTC = new DataTable();
		try
		{
			//New Code
			var getcompSelectedText = lstPositionLoca.SelectedItem.Text;		
			var qtype = "GET_CTC_Exception_Matrix_COMP";
			if (getcompSelectedText.Contains("Head Office"))
			{
				qtype = "GET_CTC_Exception_Matrix";
			}
			string approveremailaddress = string.Empty, Approvers_code = string.Empty, Approvers_Name=string.Empty;
			int apprid = 0;
			//int DeptID = Convert.ToString(lstPositionDept.SelectedValue).Trim() != "" ? Convert.ToInt32(lstPositionDept.SelectedValue) : 0;
			DataTable dtApproverEmailIds = spm.CTC_Exception_Shortlisting(qtype, Recruitment_ReqID, CandidatedID,"",0);
			if (dtApproverEmailIds.Rows.Count > 0)
			{
				approveremailaddress = (string)dtApproverEmailIds.Rows[0]["Emp_Emailaddress"];
				apprid = (int)dtApproverEmailIds.Rows[0]["APPR_ID"];
				Approvers_code = (string)dtApproverEmailIds.Rows[0]["A_EMP_CODE"];
				Approvers_Name = (string)dtApproverEmailIds.Rows[0]["Emp_Name"];
			}
			int StatusID = 1;
			dtCTC = spm.CTC_Exception_Shortlisting("INSERTCTC", Recruitment_ReqID, CandidatedID, Convert.ToString(Session["Empcode"]).Trim(), StatusID);
			if (dtCTC.Rows.Count > 0)
			{
				var CTCID = Convert.ToInt32(dtCTC.Rows[0]["RESULT"].ToString());
				spm.Insert_CTC_Exception_ApproverRequest("INSERTCTC_APPROVAL", Approvers_code, "Pending", apprid, CTCID);
				DataTable dtCandiadteInfo = spm.CTC_Exception_Shortlisting("GET_Candidated_CTC", Recruitment_ReqID, CandidatedID, "", 0);
				if (dtCandiadteInfo.Rows.Count > 0)
				{
					var CandidateName = dtCandiadteInfo.Rows[0]["CandidateName"].ToString();
					var CandidateCurrentCTC = dtCandiadteInfo.Rows[0]["CandidateCurrentCTC"].ToString();
					var ExpCTC_Total = dtCandiadteInfo.Rows[0]["ExpCTC_Total"].ToString();
					var Emp_Name = dtCandiadteInfo.Rows[0]["Emp_Name"].ToString();
					var Emp_Emailaddress = dtCandiadteInfo.Rows[0]["Emp_Emailaddress"].ToString();
					var RecruiterRemark = dtCandiadteInfo.Rows[0]["RecruiterRemark"].ToString();
					string RequiredByDate = ""; string ApprovalList = "";
					RequiredByDate = GetRequiredByDate();
					ApprovalList = Get_CTC_Approve_RejectList(CTCID);
					Get_Recruitment_HeadCode();
					SendMail_CTC_Approval(Approvers_Name, approveremailaddress, RequiredByDate, CandidateName, CandidateCurrentCTC, ExpCTC_Total, Emp_Name, Emp_Emailaddress, RecruiterRemark, ApprovalList, CTCID,hdnRecruitmentHead.Value);
				}
			}
		}
		catch (Exception)
		{

			throw;
		}

	}
	public void SendMail_CTC_Approval(string Approvers_Name, string ApprovalEmailID,string RequiredByDate, string CandidateName, string CandidateCurrentCTC, string ExpCTC_Total, string Emp_Name, string Emp_Emailaddress, string RecruiterRemark,string applist, int CTCID,string RecruitmentHead)
	{
		StringBuilder strbuild = new StringBuilder();
		try
		{
			string redirectURL = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/RecruiterCTCApproval.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value + "&CTCID=" + CTCID + "";
			//string redirectURL = "https://ess.highbartech.com/hrms/login.aspx?ReturnUrl=procs/RecruiterCTCApproval.aspx.aspx?Recruitment_ReqID=" + hdRecruitment_ReqID.Value +"&CTCID=" + CTCID +"";
			strbuild = new StringBuilder();
			strbuild.Capacity = 0;
			strbuild.Length = 0;
			strbuild.Append("<table style='color:#000000;font-size:11pt;font-family:Arial;font-style:Regular;width:100%'><tr><td>   ");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td> Dear " + Approvers_Name + ",</ td></tr>");
			strbuild.Append("<tr><td style='height:15px'></td></tr>");
			strbuild.Append("<tr><td><b>"+ Emp_Name + "</b> has requested for a CTC Exception approval for the below candidate. Please submit your appropriate feedback.</td></tr>");
			strbuild.Append("<tr><td><hr></td></tr>");
			strbuild.Append("<tr><td>");
			strbuild.Append("<table style='width:100%';'color:#000000;font-size:11pt;font-family:Arial;font-style:Regular>");
			//strbuild.Append("<tr><td>This is in reference to your job application for the profile of   </td></tr>");
			strbuild.Append("<tr><td style='height:5px'></td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Requisition No :</td><td style='width:70%'> " + txtReqNumber.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Requisitioner Name :</td><td style='width:70%'> " + txtReqName.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Requisition Date :</td><td style='width:70%'> " + txtFromdate.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Required By Date :</td><td style='width:70%'> " + RequiredByDate + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Department :</td><td style='width:70%'> " + lstPositionDept.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Location :</td><td style='width:70%'> " + lstPositionLoca.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Skill set :</td><td style='width:70%'> " + lstSkillset.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Position Title :</td><td style='width:70%'> " + lstPositionName.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Band :</td><td style='width:70%'> " + lstPositionBand.SelectedItem.Text + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>NoOfPosition :</td><td style='width:70%'> " + txtNoofPosition.Text + "</td></tr>");
			strbuild.Append("<tr><td colspan='2'><hr></td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Candidate Name :</td><td style='width:70%'> " + CandidateName + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Current CTC :</td><td style='width:70%'> " + CandidateCurrentCTC + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Expected CTC :</td><td style='width:70%'> " + ExpCTC_Total + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Recruiter Remark :</td><td style='width:70%'> " + RecruiterRemark + "</td></tr>");
			strbuild.Append("<tr><td style='width:30%'>Is Exception :</td><td style='width:70%'> Yes</td></tr>");
			strbuild.Append("<tr><td style='width:20%'>Status :</td><td style='width:80%'>" + applist + "</td></tr>");
			strbuild.Append("</table>");
			strbuild.Append("</td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td><a href='" + redirectURL + "' target='_blank'> Please click here to take action on CTC Exception Approval of a candidate </td></tr>");
			strbuild.Append("<tr><td style='height:20px'></td></tr>");
			strbuild.Append("<tr><td style='height:20px'>This is system generated mail, please do not reply.</td></tr>");
			strbuild.Append("</table>");
			var strsubject = "OneHR:- Recruitment - CTC Exception Approval of a Candidate against request for - " + Emp_Name;
			spm.sendMail(ApprovalEmailID, strsubject, Convert.ToString(strbuild).Trim(), "", RecruitmentHead);
		}
		catch (Exception)
		{
			throw;
		}
	}

	public void Get_Recruitment_HeadCode()
	{
		try
		{
		DataSet dsTrDetails = new DataSet();
		SqlParameter[] spars = new SqlParameter[3];
		spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
		spars[0].Value = "LWPML_HREmpCode";
		spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
		spars[2].Value = Session["Empcode"].ToString();
		dsTrDetails = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
		hdnRecruitmentHead.Value = "";
		if (dsTrDetails.Tables[0].Rows.Count > 0)
		{
			//hdnnextappcode.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_code"]).Trim();
			hdnRecruitmentHead.Value = Convert.ToString(dsTrDetails.Tables[0].Rows[0]["approver_emp_mail"]).Trim();
		}
		}
		catch (Exception ex)
		{

			Response.Write(ex.ToString());
		}

	}
	protected string Get_CTC_Approve_RejectList(int CTCID)
	{
		var getcompSelectedText = lstPositionLoca.SelectedItem.Text;
		var qtype = "GET_CTC_Exception_Matrix_COMP";
		if (getcompSelectedText.Contains("Head Office"))
		{
			qtype = "GET_CTC_Exception_Matrix";
		}
		StringBuilder sbapp = new StringBuilder();
		sbapp.Length = 0;
		sbapp.Capacity = 0;
		DataTable dtAppRej = new DataTable();
		int Recruitment_ReqID = 0;
		Recruitment_ReqID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
		dtAppRej = spm.CTC_Exception_Approval(qtype, Recruitment_ReqID, 0, "", 0, CTCID,0);
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
	private void CheckReferral_Candidated(string CandidatedID)
	{
		DataTable dtReferral = new DataTable();
		try
		{
			int StatusID = 4;
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
				var Subject = "Referred Candidate “" + Ref_CandidateName + "” is “In Process”";
				var Body = "This is to inform you that the candidate referred by you is “In Process”.Refer following details.";
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
			DivSendshortlisting1.Visible = false;
			DivSendshortlisting2.Visible = false;
			DivSendshortlisting3.Visible = false;

			DivInterviewer1.Visible = false;


			DivViewrowWiseCandidateInformation.Visible = false;
			btnTra_Details.Text = "+";
			DivRecruitment.Visible = false;


		}
		else
		{
			trvl_localbtn.Text = "+";
			btnTra_Details.Text = "-";
			DivRecruitment.Visible = true;

			DivSendshortlisting1.Visible = false;
			DivSendshortlisting2.Visible = false;
			DivSendshortlisting3.Visible = false;
			if (hdnSaveStatusFlag.Value == "1")
			{
				DivInterviewer1.Visible = false;


			}
			else
			{
				DivInterviewer1.Visible = false;


			}

		}
	}

	#region  All_method

	private void checkInterviewerShortlistStatus_Submit()
	{
		try
		{
			DataTable dtTrDetails = new DataTable();
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_RecruiterSendShortListing_Status";
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
		DataTable dtInterview = new DataTable();
		dtInterview = spm.GetRecruitment_Req_Employee_Mst();
		if (dtInterview.Rows.Count > 0)
		{
			//lstInterviewerOne.DataSource = dtInterview;
			//lstInterviewerOne.DataTextField = "EmployeeName";
			//lstInterviewerOne.DataValueField = "EmployeeCode";
			//lstInterviewerOne.DataBind();
			//lstInterviewerOne.Items.Insert(0, new ListItem("Select Screening By", "0"));

			//lstInterviewerOneView.DataSource = dtInterview;
			//lstInterviewerOneView.DataTextField = "EmployeeName";
			//lstInterviewerOneView.DataValueField = "EmployeeCode";
			//lstInterviewerOneView.DataBind();
			//lstInterviewerOneView.Items.Insert(0, new ListItem("Select Screening By", "0"));

			LstRecommPerson.DataSource = dtInterview;
			LstRecommPerson.DataTextField = "EmployeeName";
			LstRecommPerson.DataValueField = "EmployeeCode";
			LstRecommPerson.DataBind();
			LstRecommPerson.Items.Insert(0, new ListItem("Select Recommended Person", "0"));
		}
	}

	public void GetScreeners(int ModuleId)
	{
		DataTable dtInterview = new DataTable();
		dtInterview = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);

		lstInterviewerOne.DataSource = dtInterview;
		lstInterviewerOne.DataTextField = "EmployeeName";
		lstInterviewerOne.DataValueField = "EmployeeCode";
		lstInterviewerOne.DataBind();
		lstInterviewerOne.Items.Insert(0, new ListItem("Select Screening By", "0"));
		//updated code
		DataRow[] dr = dtInterview.Select("EmployeeCode = '" + lstInterviewerOneView.SelectedValue.ToString().Trim() + "'");
		if (dr.Length > 0)
		{
			string avalue = dr[0]["EmployeeCode"].ToString();
			lstInterviewerOne.SelectedValue = avalue;
		}
	}

	public void GetScreenerBind(int ModuleId)
	{
		DataTable dtInterview = new DataTable();
		dtInterview = spm.GetRecruitment_Req_Screeners_Mst(ModuleId);

		lstInterviewerOne.DataSource = dtInterview;
		lstInterviewerOne.DataTextField = "EmployeeName";
		lstInterviewerOne.DataValueField = "EmployeeCode";
		lstInterviewerOne.DataBind();
		lstInterviewerOne.Items.Insert(0, new ListItem("Select Screening By", "0"));
		//DataRow[] dr = dtInterview.Select("EmployeeCode = '" + lstInterviewerOneView.SelectedValue.ToString().Trim() + "'");
		//if (dr.Length > 0)
		//{
		//    string avalue = dr[0]["EmployeeCode"].ToString();
		//    lstInterviewerOne.SelectedValue = avalue;
		//}
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
	public void RecruiterSendShortListingInterviewer()
	{
		string strRecruitment_ReqID = Request.QueryString["Recruitment_ReqID"];
		hdRecruitment_ReqID.Value = Request.QueryString["Recruitment_ReqID"];
		dsRecEmpCodeInterviewer1 = spm.getRecruiterInterviewerCode1(strRecruitment_ReqID);
		// TxtinterviewerfirstLevel.Text = dsRecEmpCodeInterviewer1.Tables[0].Rows[0]["EmpName"].ToString();
		Session["SendEmailInterviername"] = dsRecEmpCodeInterviewer1.Tables[0].Rows[0]["SendEmail"].ToString();

	}
	public void getMngCandidateInfoList()
	{
		try
		{
			lblmessage.Text = "";
			//if (Convert.ToString(lstMainSkillset.SelectedValue).Trim() == "")
			//{
			//	//lblmessage.Text = "Please select MainSkillset";
			//	//return;
			//}
			//else
			{
				if (HDscreenerIDCheck.Value == "0")
				{

				}
				else
				{
					//GetScreeners(Convert.ToInt32(lstMainSkillset.SelectedValue));
					GetScreeners(Convert.ToInt32(lstSkillset.SelectedValue));
				}

				dsRecCandidate = spm.getSearchCandidateListSearchonRecruiterAction(hdRecruitment_ReqID.Value, Txt_CandidateName.Text.Trim(), Txt_CandidateEmail.Text.Trim(), lstMainSkillset.SelectedValue, DDLEducationQualification.SelectedValue);
				DataView dataView = dsRecCandidate.Tables[0].DefaultView;
				if (dataView.Count > 0)
				{

					gvSearchCandidateList.DataSource = dataView;
					gvSearchCandidateList.DataBind();
					lblCOUNT.Text = Convert.ToString("Record Count : "+ dataView.Count);
					DivInterviewer1.Visible = true;

				}
				else
				{
					lblCOUNT.Text = "";
					lblmessage.Text = "No record found. Search other criteria";
					gvSearchCandidateList.DataSource = null;
					gvSearchCandidateList.DataBind();
					DivInterviewer1.Visible = false;
				}
			}
		}
		catch (Exception ex)
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
				Txt_CandidateBirthday.Text = dsCandidateData.Tables[0].Rows[0]["CandidateBirthday"].ToString();
				Txt_CandidatePAN.Text = dsCandidateData.Tables[0].Rows[0]["CandidatePAN"].ToString();
				TxtAadharNo.Text = Convert.ToString(dsCandidateData.Tables[0].Rows[0]["AdharNo"]).Trim();
				lstCVSource.SelectedValue = dsCandidateData.Tables[0].Rows[0]["CVSource_ID"].ToString();
				Txt_lstCVSource.Text = lstCVSource.SelectedItem.Text;
				lnkuplodedfileResume.Text = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				hdfilename.Value = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				filename = dsCandidateData.Tables[0].Rows[0]["UploadResume"].ToString();
				hdnRef_Candidate_ID.Value = dsCandidateData.Tables[0].Rows[0]["Ref_Candidate_ID"].ToString();
				PopulateCadidateRecruitmentWiseData();
				lnkuplodedfileResume.Visible = true;
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
				getMainSkillsetView();

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
					SpanViewOtherFile.Visible = true;
					gvotherfile.DataSource = dsCandidateData.Tables[1];
					gvotherfile.DataBind();
				}
				else
				{
					SpanViewOtherFile.Visible = false;
					gvotherfile.DataSource = null;
					gvotherfile.DataBind();
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

				if (lstPositionDept.SelectedValue.ToString() == "14")
				{
					AgreedBG.Visible = true;
					AgreedPDC.Visible = true;
					SPBG.Visible = true;
					SPPDC.Visible = true;
					AgreedPDC1.Visible = true;
				}
				else
				{
					AgreedBG.Visible = false;
					AgreedPDC.Visible = false;
					AgreedPDC1.Visible = false;
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

		if (DS.Tables[0].Rows.Count > 0)
		{
			if (DS.Tables[0].Rows[0]["InterviewShortListMain_ID"].ToString() == "")
			{
			}
			else
			{
				HDTempInterviewShortListMain_ID.Value = DS.Tables[0].Rows[0]["InterviewShortListMain_ID"].ToString();
			}
			HDHDTempInterviewShortListMain_IDNew.Value = "";

			Txt_NoticePeriodInday.Text = DS.Tables[0].Rows[0]["NoticePeriod"].ToString();
			TxtCurrentCTC_Total.Text = DS.Tables[0].Rows[0]["CurrentCTC"].ToString();
			TxtExpCTC_Total.Text = DS.Tables[0].Rows[0]["ExpectedCTC"].ToString();
			TxtTotalExperienceYrs.Text = DS.Tables[0].Rows[0]["ExperienceYear"].ToString();
			TxtRelevantExpYrs.Text = DS.Tables[0].Rows[0]["RelevantExpYrs"].ToString();
			if (DS.Tables[0].Rows[0]["ExpectedCTC"].ToString() != "")
			{
				if (Convert.ToDecimal(TxtExpCTC_Total.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
				{
					Chk_Exception.Visible = true;
				}
				else
				{
					Chk_Exception.Visible = false;
				}
			}
			else
			{
				Chk_Exception.Visible = false;
			}
			if (DS.Tables[0].Rows[0]["CurrentCTC_Fixed"].ToString() == "0.00")
			{
				Txt_CurrentCTC_Fixed.Text = "0";
			}
			else
			{
				Txt_CurrentCTC_Fixed.Text = DS.Tables[0].Rows[0]["CurrentCTC_Fixed"].ToString();
			}
			if (DS.Tables[0].Rows[0]["CurrentCTC_Variable"].ToString() == "0.00")
			{
				TxtCurrentCTC_Variable.Text = DS.Tables[0].Rows[0]["CurrentCTC_Variable"].ToString();
			}
			else
			{
				TxtCurrentCTC_Variable.Text = DS.Tables[0].Rows[0]["CurrentCTC_Variable"].ToString();
			}
			if (DS.Tables[0].Rows[0]["ExpCTC_Fixed"].ToString() == "0.00")
			{
				TxtExpCTC_Fixed.Text = "0";
			}
			else
			{
				TxtExpCTC_Fixed.Text = DS.Tables[0].Rows[0]["ExpCTC_Fixed"].ToString();
			}
			if (DS.Tables[0].Rows[0]["ExpCTC_Variable"].ToString() == "0.00")
			{
				TxtExpCTC_Variable.Text = DS.Tables[0].Rows[0]["ExpCTC_Variable"].ToString();
			}
			else
			{
				TxtExpCTC_Variable.Text = DS.Tables[0].Rows[0]["ExpCTC_Variable"].ToString();
			}
			Txt_BaseLocationcurrentcompany.Text = DS.Tables[0].Rows[0]["LocationCurrentCompany"].ToString();
			Txt_CurrentLocation.Text = DS.Tables[0].Rows[0]["CurrentLocation"].ToString();
			Txt_NativeHomeLocation.Text = DS.Tables[0].Rows[0]["NativeLocation"].ToString();
			Txt_CurrentRoleorganization.Text = DS.Tables[0].Rows[0]["CurrentRoleorganization"].ToString();
			Txt_TravelContraintPandemicSituation.Text = DS.Tables[0].Rows[0]["TravelPandemicSituation"].ToString();
			Txt_RoleDomaincompany.Text = DS.Tables[0].Rows[0]["RoleDomainCompany"].ToString();
			Txt_lookingforChange.Text = DS.Tables[0].Rows[0]["lookingChangeReason"].ToString();


			if (DS.Tables[0].Rows[0]["CurrentlyOnNotice"].ToString() != "0")
			{

				DDlCurrentlyonnotice.SelectedValue = DS.Tables[0].Rows[0]["CurrentlyOnNotice"].ToString();
			}
			if (DS.Tables[0].Rows[0]["BaseLocationPreferenceID"].ToString() != "0")
			{

				DDLBaseLocationPreference.SelectedValue = DS.Tables[0].Rows[0]["BaseLocationPreferenceID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["Travelanylocations"].ToString() != "0")
			{

				DDLRelocateTravelAnyLocation.SelectedValue = DS.Tables[0].Rows[0]["Travelanylocations"].ToString();
			}
			if (DS.Tables[0].Rows[0]["OpentoTravel"].ToString() != "0")
			{

				DDLOpenToTravel.SelectedValue = DS.Tables[0].Rows[0]["OpentoTravel"].ToString();
			}
			if (DS.Tables[0].Rows[0]["Candidatepayroll"].ToString() != "0")
			{

				DDlpayrollsCompany.SelectedValue = DS.Tables[0].Rows[0]["Candidatepayroll"].ToString();
			}
			if (DS.Tables[0].Rows[0]["Candidateanybreakservice"].ToString() != "0")
			{

				DDLBreakInService.SelectedValue = DS.Tables[0].Rows[0]["Candidateanybreakservice"].ToString();

				if (DDLBreakInService.SelectedValue == "1")
				{
					TxtReasonforBreak.Text = DS.Tables[0].Rows[0]["Reasonforbreak"].ToString();
					SpanTxtReasonforBreak1.Visible = true;
					TxtReasonforBreak.Visible = true;
				}
				else
				{
					SpanTxtReasonforBreak1.Visible = false;
					TxtReasonforBreak.Visible = false;
				}
			}
			if (DS.Tables[0].Rows[0]["TypeProjecthandledID"].ToString() != "0")
			{

				DDLprojecthandled.SelectedValue = DS.Tables[0].Rows[0]["TypeProjecthandledID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["DomainExperienceID"].ToString() != "0")
			{

				DDLDomainExperence.SelectedValue = DS.Tables[0].Rows[0]["DomainExperienceID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["SAPExperienceID"].ToString() != "0")
			{

				DDLSAPExperence.SelectedValue = DS.Tables[0].Rows[0]["SAPExperienceID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["ImplementationProjectsWorkOnID"].ToString() != "0")
			{

				DDLImplementationprojectWorkedOn.SelectedValue = DS.Tables[0].Rows[0]["ImplementationProjectsWorkOnID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["TotalSupportProjectID"].ToString() != "0")
			{

				DDLSupportproject.SelectedValue = DS.Tables[0].Rows[0]["TotalSupportProjectID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["PhasesImplementationWorkId"].ToString() != "0")
			{

				DDLPhaseWorkimplementation.SelectedValue = DS.Tables[0].Rows[0]["PhasesImplementationWorkId"].ToString();
			}
			if (DS.Tables[0].Rows[0]["RoleImplementationProjectID"].ToString() != "0")
			{

				DDLRolesPlaydImplementation.SelectedValue = DS.Tables[0].Rows[0]["RoleImplementationProjectID"].ToString();
			}
			if (DS.Tables[0].Rows[0]["NatureIndustryClientID"].ToString() != "0")
			{

				DDLnatureOfIndustryClient.SelectedValue = DS.Tables[0].Rows[0]["NatureIndustryClientID"].ToString();
				if (DDLnatureOfIndustryClient.SelectedValue == "5")
				{
					Txt_OtherNatureOfIndustryClient.Visible = true;
					SpanTxtOtherNatureOfIndustryClient.Visible = true;
					SpanTxtOtherNatureOfIndustryClient1.Visible = true;
					Txt_OtherNatureOfIndustryClient.Text = DS.Tables[0].Rows[0]["OtherNatureOfIndustryClient"].ToString();
				}
				else
				{
					Txt_OtherNatureOfIndustryClient.Visible = false;
					SpanTxtOtherNatureOfIndustryClient.Visible = false;
					SpanTxtOtherNatureOfIndustryClient1.Visible = false;
				}

			}
			if (DS.Tables[0].Rows[0]["CheckCommunicationSkillID"].ToString() != "0")
			{

				DDLCommunicationSkill.SelectedValue = DS.Tables[0].Rows[0]["CheckCommunicationSkillID"].ToString();
			}

			if (DS.Tables[0].Rows[0]["CTCException"].ToString()!="")
			{

				Chk_Exception.Checked =Convert.ToBoolean(DS.Tables[0].Rows[0]["CTCException"].ToString());
				if (Chk_Exception.Checked)
				{
					txtRecruiterRemark.Text = DS.Tables[0].Rows[0]["RecruiterRemark"].ToString();
					ExceptionR.Visible = true;
				}
				else
				{
					Chk_Exception.Checked = false;
					txtRecruiterRemark.Text = "";
					ExceptionR.Visible = false;
				}
			}
		}
		else
		{
			if (HDTempInterviewShortListMain_ID.Value == "")
			{
				HDHDTempInterviewShortListMain_IDNew.Value = "";
			}
			else
			{
				HDHDTempInterviewShortListMain_IDNew.Value = "UpdateNew";
			}

			GetCandidateInfoRecruitmentwisedataBind();
			//DDLRelocateTravelAnyLocation.SelectedValue = "0";
			//DDlCurrentlyonnotice.SelectedValue = "0";
			//DDLOpenToTravel.SelectedValue = "0";
			//DDlpayrollsCompany.SelectedValue = "0";
			//DDLBreakInService.SelectedValue = "0";

			//Txt_NoticePeriodInday.Text = "";
			//TxtCurrentCTC_Total.Text = "0";
			//TxtExpCTC_Total.Text = "0";
			//TxtTotalExperienceYrs.Text = "0";
			//TxtRelevantExpYrs.Text = "0";
			//Txt_CurrentCTC_Fixed.Text = "0";
			//TxtCurrentCTC_Variable.Text = "0";
			//TxtExpCTC_Fixed.Text = "0";
			//TxtExpCTC_Variable.Text = "0";
			//Txt_BaseLocationcurrentcompany.Text = "";
			//Txt_CurrentLocation.Text = "";
			//Txt_NativeHomeLocation.Text = "";
			//Txt_CurrentRoleorganization.Text = "";
			//Txt_TravelContraintPandemicSituation.Text = "";
			//TxtReasonforBreak.Text = "";
			//Txt_RoleDomaincompany.Text = "";
			//Txt_lookingforChange.Text = "";
			//Txt_OtherNatureOfIndustryClient.Text = "";

			Txt_NoticePeriodInday.Text = DS.Tables[2].Rows[0]["NoticePeriod"].ToString();
			if (DS.Tables[2].Rows[0]["CandidateCurrentCTC"].ToString() == "")
			{
				TxtCurrentCTC_Total.Text = "0.00";
			}
			else
			{
				TxtCurrentCTC_Total.Text = DS.Tables[2].Rows[0]["CandidateCurrentCTC"].ToString();
			}

			if (DS.Tables[2].Rows[0]["ExpCTC_Total"].ToString() == "")
			{
				TxtExpCTC_Total.Text = "0.00";
			}
			else
			{
				TxtExpCTC_Total.Text = DS.Tables[2].Rows[0]["ExpCTC_Total"].ToString();
			}
			if (DS.Tables[2].Rows[0]["ExpCTC_Total"].ToString() != "")
			{
				if (Convert.ToDecimal(TxtExpCTC_Total.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
				{
					Chk_Exception.Visible = true;
				}
				else
				{
					Chk_Exception.Visible = false;
				}
			}
			else
			{
				Chk_Exception.Visible = false;
			}
			TxtTotalExperienceYrs.Text = DS.Tables[2].Rows[0]["CandidateExperience_Years"].ToString();
			TxtRelevantExpYrs.Text = DS.Tables[2].Rows[0]["RelevantExpYrs"].ToString();
			if (DS.Tables[2].Rows[0]["CurrentCTC_Fixed"].ToString() == "0.00" || DS.Tables[2].Rows[0]["CurrentCTC_Fixed"].ToString() == "")
			{
				Txt_CurrentCTC_Fixed.Text = "0";
			}
			else
			{
				Txt_CurrentCTC_Fixed.Text = DS.Tables[2].Rows[0]["CurrentCTC_Fixed"].ToString();
			}
			if (DS.Tables[2].Rows[0]["CurrentCTC_Variable"].ToString() == "0.00" || DS.Tables[2].Rows[0]["CurrentCTC_Variable"].ToString() == "")
			{
				TxtCurrentCTC_Variable.Text = "0";
			}
			else
			{
				TxtCurrentCTC_Variable.Text = DS.Tables[2].Rows[0]["CurrentCTC_Variable"].ToString();
			}
			if (DS.Tables[2].Rows[0]["ExpCTC_Fixed"].ToString() == "0.00" || DS.Tables[2].Rows[0]["ExpCTC_Fixed"].ToString() == "")
			{
				TxtExpCTC_Fixed.Text = "0";
			}
			else
			{
				TxtExpCTC_Fixed.Text = DS.Tables[2].Rows[0]["ExpCTC_Fixed"].ToString();
			}
			if (DS.Tables[2].Rows[0]["ExpCTC_Variable"].ToString() == "0.00" || DS.Tables[2].Rows[0]["ExpCTC_Variable"].ToString() == "")
			{
				TxtExpCTC_Variable.Text = "0";
			}
			else
			{
				TxtExpCTC_Variable.Text = DS.Tables[2].Rows[0]["ExpCTC_Variable"].ToString();
			}
			Txt_BaseLocationcurrentcompany.Text = DS.Tables[2].Rows[0]["LocationCurrentCompany"].ToString();
			Txt_CurrentLocation.Text = DS.Tables[2].Rows[0]["CandidateCurrentLocation"].ToString();
			Txt_NativeHomeLocation.Text = DS.Tables[2].Rows[0]["NativeLocation"].ToString();
			Txt_CurrentRoleorganization.Text = DS.Tables[2].Rows[0]["CurrentRoleorganization"].ToString();
			Txt_TravelContraintPandemicSituation.Text = DS.Tables[2].Rows[0]["TravelPandemicSituation"].ToString();
			Txt_RoleDomaincompany.Text = DS.Tables[2].Rows[0]["RoleDomainCompany"].ToString();
			Txt_lookingforChange.Text = DS.Tables[2].Rows[0]["lookingChangeReason"].ToString();


			if (DS.Tables[2].Rows[0]["CurrentlyOnNotice"].ToString() != "0")
			{

				DDlCurrentlyonnotice.SelectedValue = DS.Tables[2].Rows[0]["CurrentlyOnNotice"].ToString();
			}
			if (DS.Tables[2].Rows[0]["BaseLocationPreferenceID"].ToString() != "0")
			{

				DDLBaseLocationPreference.SelectedValue = DS.Tables[2].Rows[0]["BaseLocationPreferenceID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["Travelanylocations"].ToString() != "0")
			{

				DDLRelocateTravelAnyLocation.SelectedValue = DS.Tables[2].Rows[0]["Travelanylocations"].ToString();
			}
			if (DS.Tables[2].Rows[0]["OpentoTravel"].ToString() != "0")
			{

				DDLOpenToTravel.SelectedValue = DS.Tables[2].Rows[0]["OpentoTravel"].ToString();
			}
			if (DS.Tables[2].Rows[0]["Candidatepayroll"].ToString() != "0")
			{

				DDlpayrollsCompany.SelectedValue = DS.Tables[2].Rows[0]["Candidatepayroll"].ToString();
			}
			if (DS.Tables[2].Rows[0]["Candidateanybreakservice"].ToString() != "0")
			{

				DDLBreakInService.SelectedValue = DS.Tables[2].Rows[0]["Candidateanybreakservice"].ToString();

				if (DDLBreakInService.SelectedValue == "1")
				{
					TxtReasonforBreak.Text = DS.Tables[2].Rows[0]["Reasonforbreak"].ToString();
					SpanTxtReasonforBreak1.Visible = true;
					TxtReasonforBreak.Visible = true;
				}
				else
				{
					SpanTxtReasonforBreak1.Visible = false;
					TxtReasonforBreak.Visible = false;
				}
			}
			if (DS.Tables[2].Rows[0]["TypeProjecthandledID"].ToString() != "0")
			{

				DDLprojecthandled.SelectedValue = DS.Tables[2].Rows[0]["TypeProjecthandledID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["DomainExperienceID"].ToString() != "0")
			{

				DDLDomainExperence.SelectedValue = DS.Tables[2].Rows[0]["DomainExperienceID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["SAPExperienceID"].ToString() != "0")
			{

				DDLSAPExperence.SelectedValue = DS.Tables[2].Rows[0]["SAPExperienceID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["ImplementationProjectsWorkOnID"].ToString() != "0")
			{

				DDLImplementationprojectWorkedOn.SelectedValue = DS.Tables[2].Rows[0]["ImplementationProjectsWorkOnID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["TotalSupportProjectID"].ToString() != "0")
			{

				DDLSupportproject.SelectedValue = DS.Tables[2].Rows[0]["TotalSupportProjectID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["PhasesImplementationWorkId"].ToString() != "0")
			{

				DDLPhaseWorkimplementation.SelectedValue = DS.Tables[2].Rows[0]["PhasesImplementationWorkId"].ToString();
			}
			if (DS.Tables[2].Rows[0]["RoleImplementationProjectID"].ToString() != "0")
			{

				DDLRolesPlaydImplementation.SelectedValue = DS.Tables[2].Rows[0]["RoleImplementationProjectID"].ToString();
			}
			if (DS.Tables[2].Rows[0]["NatureIndustryClientID"].ToString() != "0")
			{

				DDLnatureOfIndustryClient.SelectedValue = DS.Tables[2].Rows[0]["NatureIndustryClientID"].ToString();
				if (DDLnatureOfIndustryClient.SelectedValue == "5")
				{
					Txt_OtherNatureOfIndustryClient.Visible = true;
					SpanTxtOtherNatureOfIndustryClient.Visible = true;
					SpanTxtOtherNatureOfIndustryClient1.Visible = true;
					Txt_OtherNatureOfIndustryClient.Text = DS.Tables[2].Rows[0]["OtherNatureOfIndustryClient"].ToString();
				}
				else
				{
					Txt_OtherNatureOfIndustryClient.Visible = false;
					SpanTxtOtherNatureOfIndustryClient.Visible = false;
					SpanTxtOtherNatureOfIndustryClient1.Visible = false;
				}
			}
			if (DS.Tables[2].Rows[0]["CheckCommunicationSkillID"].ToString() != "0")
			{

				DDLCommunicationSkill.SelectedValue = DS.Tables[2].Rows[0]["CheckCommunicationSkillID"].ToString();
			}
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

		lstMainSkillset.DataSource = dtmainSkillSet;
		lstMainSkillset.DataTextField = "ModuleDesc";
		lstMainSkillset.DataValueField = "ModuleId";
		lstMainSkillset.DataBind();
		lstMainSkillset.Items.Insert(0, new ListItem("Select SkillSet", ""));
	}
	private void getCVSource()
	{
		dsCVSource = spm.GetCVSource();
		lstCVSource.DataSource = dsCVSource;
		lstCVSource.DataTextField = "CVSource";
		lstCVSource.DataValueField = "CVSource_ID";
		lstCVSource.DataBind();
		lstCVSource.Items.Insert(0, new ListItem("Select CVSource", ""));

		DDLEducationQualification.DataSource = dsCVSource.Tables[11];
		DDLEducationQualification.DataTextField = "EducationType";
		DDLEducationQualification.DataValueField = "EducationTypeID";
		DDLEducationQualification.DataBind();
		DDLEducationQualification.Items.Insert(0, new ListItem("Select Education Qualification", ""));
	}

	private void Get_InterviewerSchedulars()
	{
		DataSet DSInterviewerSchedulars = new DataSet();
		DSInterviewerSchedulars = spm.GetInterviewerSchedularsEmpCode();

		DDLAssignInterviewSchedulars.DataSource = DSInterviewerSchedulars.Tables[0];
		DDLAssignInterviewSchedulars.DataTextField = "Emp_Name";
		DDLAssignInterviewSchedulars.DataValueField = "InterviewerSchedularEmpCode";
		DDLAssignInterviewSchedulars.DataBind();
		DDLAssignInterviewSchedulars.Items.Insert(0, new ListItem("Select Interview Schedulars", ""));
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
			dsRecruitmentDetails = spm.getDatasetList(spars, "SP_Recruitment_Requisition_INSERT");

			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{

				txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
				txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();
				DDLAssignInterviewSchedulars.SelectedValue = dsRecruitmentDetails.Tables[0].Rows[0]["InterviewerSchedularEmpCode"].ToString();
				txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
				txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
				txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();
				txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
				lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
				GetInterviewerScreeningBy(Convert.ToInt32(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]));
				lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
				lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
				lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
				lstPositionDesign.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation_iD"]).Trim();
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
				lstInterviewerOne.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code_Inter1"]).Trim();
				lnkuplodedfileResume.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"]).Trim();
				txtJobDescription.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["JobDescription"]).Trim();
				if (dsRecruitmentDetails.Tables[4].Rows.Count > 0)
				{
					mobile_btnSave.Visible = false;
				}
				if (Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Status_ID"]).Trim() == "3")
				{
					mobile_btnSave.Visible = false;
				}

				if (dsRecruitmentDetails.Tables[5].Rows.Count > 0)
				{
					HDscreenerIDCheck.Value = "0";
					GetScreenerBind(Convert.ToInt32(lstSkillset.SelectedValue));
					lstInterviewerOne.SelectedValue = dsRecruitmentDetails.Tables[5].Rows[0]["InterviewerEmpCode"].ToString();
					gvSearchCandidateList.DataSource = dsRecruitmentDetails.Tables[5];
					gvSearchCandidateList.DataBind();
					lblCOUNT.Text = Convert.ToString("Record Count : " + dsRecruitmentDetails.Tables[5].Rows.Count);
					DivInterviewer1.Visible = true;
					validationCheckforSendforshortlisting();
				}
				else
				{
					DivInterviewer1.Visible = false;
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	public void validationCheckforSendforshortlisting()
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
				if (dsRecruitmentDetails.Tables[6].Rows.Count > 0)
				{
					HDTempInterviewShortListMain_ID.Value = dsRecruitmentDetails.Tables[6].Rows[0]["InterviewShortListMain_ID"].ToString();
					for (int i = 0; i < dsRecruitmentDetails.Tables[6].Rows.Count; i++)
					{
						string str1 = dsRecruitmentDetails.Tables[6].Rows[i]["CurrentlyOnNotice"].ToString();
						string str2 = dsRecruitmentDetails.Tables[6].Rows[i]["NoticePeriod"].ToString();
						string str3 = dsRecruitmentDetails.Tables[6].Rows[i]["CurrentCTC"].ToString();
						string str4 = dsRecruitmentDetails.Tables[6].Rows[i]["ExpectedCTC"].ToString();
						string str5 = dsRecruitmentDetails.Tables[6].Rows[i]["ExperienceYear"].ToString();
						string str6 = dsRecruitmentDetails.Tables[6].Rows[i]["RelevantExpYrs"].ToString();
						string str7 = dsRecruitmentDetails.Tables[6].Rows[i]["CurrentCTC_Fixed"].ToString();
						string str8 = dsRecruitmentDetails.Tables[6].Rows[i]["CurrentCTC_Variable"].ToString();
						string str9 = dsRecruitmentDetails.Tables[6].Rows[i]["ExpCTC_Fixed"].ToString();
						string str10 = dsRecruitmentDetails.Tables[6].Rows[i]["ExpCTC_Variable"].ToString();
						string str12 = dsRecruitmentDetails.Tables[6].Rows[i]["CurrentLocation"].ToString();
						string str13 = dsRecruitmentDetails.Tables[6].Rows[i]["NativeLocation"].ToString();
						string str14 = dsRecruitmentDetails.Tables[6].Rows[i]["LocationCurrentCompany"].ToString();
						string str15 = dsRecruitmentDetails.Tables[6].Rows[i]["BaseLocationPreferenceID"].ToString();
						string str16 = dsRecruitmentDetails.Tables[6].Rows[i]["Travelanylocations"].ToString();
						string str17 = dsRecruitmentDetails.Tables[6].Rows[i]["TravelPandemicSituation"].ToString();
						string str18 = dsRecruitmentDetails.Tables[6].Rows[i]["OpentoTravel"].ToString();
						string str19 = dsRecruitmentDetails.Tables[6].Rows[i]["Candidatepayroll"].ToString();
						string str20 = dsRecruitmentDetails.Tables[6].Rows[i]["CandidateCurrentRole"].ToString();
						string str21 = dsRecruitmentDetails.Tables[6].Rows[i]["Candidateanybreakservice"].ToString();
						string str22 = dsRecruitmentDetails.Tables[6].Rows[i]["Reasonforbreak"].ToString();
						string str23 = dsRecruitmentDetails.Tables[6].Rows[i]["TypeProjecthandledID"].ToString();
						string str24 = dsRecruitmentDetails.Tables[6].Rows[i]["DomainExperienceID"].ToString();
						string str25 = dsRecruitmentDetails.Tables[6].Rows[i]["SAPExperienceID"].ToString();
						string str26 = dsRecruitmentDetails.Tables[6].Rows[i]["ImplementationProjectsWorkOnID"].ToString();
						string str27 = dsRecruitmentDetails.Tables[6].Rows[i]["TotalSupportProjectID"].ToString();
						string str28 = dsRecruitmentDetails.Tables[6].Rows[i]["PhasesImplementationWorkId"].ToString();
						string str29 = dsRecruitmentDetails.Tables[6].Rows[i]["RoleImplementationProjectID"].ToString();
						string str30 = dsRecruitmentDetails.Tables[6].Rows[i]["RoleDomainCompany"].ToString();
						string str31 = dsRecruitmentDetails.Tables[6].Rows[i]["lookingChangeReason"].ToString();
						string str32 = dsRecruitmentDetails.Tables[6].Rows[i]["NatureIndustryClientID"].ToString();
						string str33 = dsRecruitmentDetails.Tables[6].Rows[i]["CheckCommunicationSkillID"].ToString();
						string str34 = dsRecruitmentDetails.Tables[6].Rows[i]["CurrentRoleorganization"].ToString();
						string str35 = dsRecruitmentDetails.Tables[6].Rows[i]["OtherNatureOfIndustryClient"].ToString();
						if (str32 == "5")
						{
							if (str35 == "")
							{
								mobile_cancel.Visible = false;
								break;
							}
						}
						if (str21 == "1")
						{
							if (str22 == "")
							{
								mobile_cancel.Visible = false;
								break;
							}
						}
						if (str1 == "" || str2 == "" || str3 == "" || str4 == "" || str5 == "" || str6 == "" || str7 == "0.00" || str9 == "0.00" || str12 == "" || str13 == "" || str14 == "" || str15 == "" || str16 == "" || str17 == "")
						{
							mobile_cancel.Visible = false;
							break;
						}
						else if (str18 == "" || str19 == "" || str20 == "" || str21 == "" || str23 == "" || str24 == "" || str25 == "" || str26 == "" || str27 == "" || str28 == "" || str29 == "" || str30 == "" || str31 == "" || str32 == "" || str33 == "" || str34 == "")
						{
							mobile_cancel.Visible = false;
							break;
						}
						else
						{
							mobile_cancel.Visible = true;
						}
					}
					foreach (GridViewRow gvrow in gvSearchCandidateList.Rows)
					{
						CheckBox chk = (CheckBox)gvrow.FindControl("lstboxChecksendforshortlisting");
						hdCandidate_ID.Value = Convert.ToString(gvSearchCandidateList.DataKeys[gvrow.RowIndex].Values[0]).Trim();
						if (chk.Checked == true)
						{
							if (dsRecruitmentDetails.Tables[6].Rows.Count > 0)
							{
								DataRow[] dr1 = dsRecruitmentDetails.Tables[6].Select("Candidate_ID='" + hdCandidate_ID.Value + "'");
								if (dr1.Length > 0)
								{

								}
								else
								{
									mobile_cancel.Visible = false;
									// lblmessage.Text = "Please Fill up the other information";
									break;
								}
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	#endregion

	protected void JobDetail_btnSave_Click(object sender, EventArgs e)
	{

		try
		{
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			lblmessage.Text = "";
			lblmessageOnsave.Text = "";
			if (lstPositionDept.SelectedValue == "14")
			{
				if (TxtTotalExperienceYrs.Text.Trim() == "")
				{
					lblmessageOnsave.Text = "Please Enter Total Experience In(Year)";
					return;
				}
				if (TxtRelevantExpYrs.Text.Trim() == "")
				{
					lblmessageOnsave.Text = "Please Enter Relevant Experience In(Year)";
					return;
				}
			}
			else
			{
				if (TxtTotalExperienceYrs.Text.Trim() == "" || TxtTotalExperienceYrs.Text.Trim() == "0" || TxtTotalExperienceYrs.Text.Trim() == "0.00")
				{
					lblmessageOnsave.Text = "Please Enter Total Experience In(Year)";
					return;
				}
				if (TxtRelevantExpYrs.Text.Trim() == "" || TxtRelevantExpYrs.Text.Trim() == "0" || TxtRelevantExpYrs.Text.Trim() == "0.00")
				{
					lblmessageOnsave.Text = "Please Enter Relevant Experience In(Year)";
					return;
				}
			}
			if (lstPositionDept.SelectedValue == "14")
			{
				if (lstAgreedBG.SelectedValue.Trim() == "0" || lstAgreedBG.Text.Trim() == "-- Select --")
				{
					lblmessageOnsave.Text = "Please Select Agreed for BG";
					return;
				}
				if (lstAgreedPDC.Text.Trim() == "-- Select --" || lstAgreedPDC.SelectedValue.Trim() == "0")
				{
					lblmessageOnsave.Text = "Please Select Agreed for PDC";
					return;
				}

			}


			validationCheckonSaveTemp();




			if (Convert.ToString(TxtTotalExperienceYrs.Text).Trim() != "" && Convert.ToString(TxtRelevantExpYrs.Text).Trim() != "")
			{
				if (Convert.ToDecimal(TxtRelevantExpYrs.Text.Trim()) > Convert.ToDecimal(TxtTotalExperienceYrs.Text.Trim()))
				{
					lblmessage.Text = "Please enter Required Relevant Exp.Yrs To Less than from Total Experience Yrs";
					return;
				}
				if (Convert.ToDecimal(TxtRelevantExpYrs.Text.Trim()) == Convert.ToDecimal(TxtTotalExperienceYrs.Text.Trim()))
				{
					lblmessage.Text = "";
				}

			}

			if (DDLAssignInterviewSchedulars.SelectedValue == "")
			{
				lblmessage.Text = "Please Select Interview Schedulars";
				return;
			}

			if (lblmessage.Text == "" && lblmessageOnsave.Text == "")
			{
				SqlParameter[] spars = new SqlParameter[49];
				DataSet DS = new DataSet();

				if (HDTempInterviewShortListMain_ID.Value == "")
				{
					spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
					spars[0].Value = "INSERT";
				}
				if (HDTempInterviewShortListMain_ID.Value != "" && HDHDTempInterviewShortListMain_IDNew.Value == "")
				{
					spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
					spars[0].Value = "Update";
				}
				if (HDTempInterviewShortListMain_ID.Value != "" && HDHDTempInterviewShortListMain_IDNew.Value == "UpdateNew")
				{
					spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
					spars[0].Value = "UpdateNew";
				}


				spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
				spars[2] = new SqlParameter("@InterviewerEmpCode", SqlDbType.VarChar);
				spars[2].Value = lstInterviewerOne.SelectedValue;
				spars[3] = new SqlParameter("@Recruiter_EmpCode", SqlDbType.VarChar);
				spars[3].Value = Session["Empcode"].ToString();
				spars[4] = new SqlParameter("@CurrentlyOnNotice", SqlDbType.VarChar);
				spars[4].Value = DDlCurrentlyonnotice.SelectedValue;
				spars[5] = new SqlParameter("@NoticePeriod", SqlDbType.VarChar);
				spars[5].Value = Txt_NoticePeriodInday.Text.Trim();
				spars[6] = new SqlParameter("@CurrentCTC", SqlDbType.VarChar);
				spars[6].Value = TxtCurrentCTC_Total.Text;
				spars[7] = new SqlParameter("@ExpectedCTC", SqlDbType.VarChar);
				spars[7].Value = TxtExpCTC_Total.Text.Trim();
				spars[8] = new SqlParameter("@ExperienceYear", SqlDbType.VarChar);
				spars[8].Value = TxtTotalExperienceYrs.Text.Trim();
				spars[9] = new SqlParameter("@Remarks", SqlDbType.VarChar);
				spars[9].Value = "";
				spars[10] = new SqlParameter("@RelevantExpYrs", SqlDbType.VarChar);
				spars[10].Value = TxtRelevantExpYrs.Text.Trim();
				if (Txt_CurrentCTC_Fixed.Text.Trim() == "")
				{
					spars[11] = new SqlParameter("@CurrentCTC_Fixed", SqlDbType.VarChar);
					spars[11].Value = "0";
				}
				else
				{
					spars[11] = new SqlParameter("@CurrentCTC_Fixed", SqlDbType.VarChar);
					spars[11].Value = Txt_CurrentCTC_Fixed.Text.Trim();
				}

				if (TxtCurrentCTC_Variable.Text.Trim() == "")
				{
					spars[12] = new SqlParameter("@CurrentCTC_Variable", SqlDbType.VarChar);
					spars[12].Value = "0";
				}
				else
				{
					spars[12] = new SqlParameter("@CurrentCTC_Variable", SqlDbType.VarChar);
					spars[12].Value = TxtCurrentCTC_Variable.Text.Trim();
				}

				if (TxtExpCTC_Fixed.Text.Trim() == "")
				{
					spars[13] = new SqlParameter("@ExpCTC_Fixed", SqlDbType.VarChar);
					spars[13].Value = "0";
				}
				else
				{
					spars[13] = new SqlParameter("@ExpCTC_Fixed", SqlDbType.VarChar);
					spars[13].Value = TxtExpCTC_Fixed.Text.Trim();
				}

				if (TxtExpCTC_Variable.Text.Trim() == "")
				{
					spars[14] = new SqlParameter("@ExpCTC_Variable", SqlDbType.VarChar);
					spars[14].Value = "0";
				}
				else
				{
					spars[14] = new SqlParameter("@ExpCTC_Variable", SqlDbType.VarChar);
					spars[14].Value = TxtExpCTC_Variable.Text.Trim();
				}

				if (TxtExpCTC_Total.Text.Trim() == "")
				{
					spars[15] = new SqlParameter("@ExpCTC_Total", SqlDbType.VarChar);
					spars[15].Value = "0";
				}
				else
				{
					spars[15] = new SqlParameter("@ExpCTC_Total", SqlDbType.VarChar);
					spars[15].Value = TxtExpCTC_Total.Text.Trim();
				}

				spars[16] = new SqlParameter("@LocationCurrentCompany", SqlDbType.VarChar);
				spars[16].Value = Txt_BaseLocationcurrentcompany.Text.Trim();
				spars[17] = new SqlParameter("@BaseLocationPreferenceID", SqlDbType.VarChar);
				spars[17].Value = DDLBaseLocationPreference.SelectedValue;
				spars[18] = new SqlParameter("@Travelanylocations", SqlDbType.VarChar);
				spars[18].Value = DDLRelocateTravelAnyLocation.SelectedValue;
				spars[19] = new SqlParameter("@TravelPandemicSituation", SqlDbType.VarChar);
				spars[19].Value = Txt_TravelContraintPandemicSituation.Text.Trim();
				spars[20] = new SqlParameter("@OpentoTravel", SqlDbType.VarChar);
				spars[20].Value = DDLOpenToTravel.SelectedValue;
				spars[21] = new SqlParameter("@Candidatepayroll", SqlDbType.VarChar);
				spars[21].Value = DDlpayrollsCompany.SelectedValue;
				spars[22] = new SqlParameter("@CandidateCurrentRole", SqlDbType.VarChar);
				spars[22].Value = Txt_RoleDomaincompany.Text.Trim();
				spars[23] = new SqlParameter("@Candidateanybreakservice", SqlDbType.VarChar);
				spars[23].Value = DDLBreakInService.SelectedValue;

				if (DDLBreakInService.SelectedValue == "1")
				{
					spars[24] = new SqlParameter("@Reasonforbreak", SqlDbType.VarChar);
					spars[24].Value = TxtReasonforBreak.Text.Trim();
				}
				else
				{
					spars[24] = new SqlParameter("@Reasonforbreak", SqlDbType.VarChar);
					spars[24].Value = "";
				}



				spars[25] = new SqlParameter("@TypeProjecthandledID", SqlDbType.VarChar);
				spars[25].Value = DDLprojecthandled.SelectedValue;
				spars[26] = new SqlParameter("@DomainExperienceID", SqlDbType.VarChar);
				spars[26].Value = DDLDomainExperence.SelectedValue;
				spars[27] = new SqlParameter("@SAPExperienceID", SqlDbType.VarChar);
				spars[27].Value = DDLSAPExperence.SelectedValue;
				spars[28] = new SqlParameter("@ImplementationProjectsWorkOnID", SqlDbType.VarChar);
				spars[28].Value = DDLImplementationprojectWorkedOn.SelectedValue;
				spars[29] = new SqlParameter("@TotalSupportProjectID", SqlDbType.VarChar);
				spars[29].Value = DDLSupportproject.SelectedValue;
				spars[30] = new SqlParameter("@PhasesImplementationWorkId", SqlDbType.VarChar);
				spars[30].Value = DDLPhaseWorkimplementation.SelectedValue;
				spars[31] = new SqlParameter("@RoleImplementationProjectID", SqlDbType.VarChar);
				spars[31].Value = DDLRolesPlaydImplementation.SelectedValue;
				spars[32] = new SqlParameter("@RoleDomainCompany", SqlDbType.VarChar);
				spars[32].Value = Txt_RoleDomaincompany.Text.Trim();
				spars[33] = new SqlParameter("@lookingChangeReason", SqlDbType.VarChar);
				spars[33].Value = Txt_lookingforChange.Text.Trim();
				spars[34] = new SqlParameter("@NatureIndustryClientID", SqlDbType.VarChar);
				spars[34].Value = DDLnatureOfIndustryClient.SelectedValue;
				spars[35] = new SqlParameter("@CheckCommunicationSkillID", SqlDbType.VarChar);
				spars[35].Value = DDLCommunicationSkill.SelectedValue;
				spars[36] = new SqlParameter("@Candidate_ID", SqlDbType.VarChar);
				spars[36].Value = hdCandidate_ID.Value;
				spars[37] = new SqlParameter("@CurrentLocation", SqlDbType.VarChar);
				spars[37].Value = Txt_CurrentLocation.Text.Trim();
				spars[38] = new SqlParameter("@NativeLocation", SqlDbType.VarChar);
				spars[38].Value = Txt_NativeHomeLocation.Text.Trim();
				spars[39] = new SqlParameter("@CurrentRoleorganization", SqlDbType.VarChar);
				spars[39].Value = Txt_CurrentRoleorganization.Text.Trim();
				spars[40] = new SqlParameter("@InterviewShortListMain_ID", SqlDbType.VarChar);
				spars[40].Value = HDTempInterviewShortListMain_ID.Value;
				spars[41] = new SqlParameter("@InterviewerSchedularEmpCode", SqlDbType.VarChar);
				spars[41].Value = DDLAssignInterviewSchedulars.SelectedValue;
				if (DDLnatureOfIndustryClient.SelectedValue == "5")
				{
					spars[42] = new SqlParameter("@OtherNatureOfIndustryClient", SqlDbType.VarChar);
					spars[42].Value = Txt_OtherNatureOfIndustryClient.Text;
				}
				else
				{
					spars[42] = new SqlParameter("@OtherNatureOfIndustryClient", SqlDbType.VarChar);
					spars[42].Value = "";
				}
				spars[43] = new SqlParameter("@AgreedBG", SqlDbType.VarChar);
				spars[43].Value = lstAgreedBG.SelectedValue;
				spars[44] = new SqlParameter("@AgreedPDC", SqlDbType.VarChar);
				spars[44].Value = lstAgreedPDC.SelectedValue;

				int CTCException = 0;
				if (Chk_Exception.Checked)
				{
					CTCException = 1;
				}
				spars[45] = new SqlParameter("@CTCException", SqlDbType.Int);
				spars[45].Value = CTCException;
				spars[46] = new SqlParameter("@RecruiterRemark", SqlDbType.NVarChar);
				spars[46].Value = txtRecruiterRemark.Text;

				DS = spm.getDatasetList(spars, "sp_TempData_SendForShortlisting");
				if (DS.Tables[0].Rows.Count > 0)
				{
					HDTempInterviewShortListMain_ID.Value = DS.Tables[0].Rows[0]["InterviewShortListMain_ID"].ToString();
				}

				DivViewrowWiseCandidateInformation.Visible = false;
				JobDetail_btnSave.Visible = false;
				trvldeatils_btnSave.Visible = false;
				validationCheckforSendforshortlisting();
				txtRecruiterRemark.Text = "";
				Chk_Exception.Checked = false;
				ExceptionR.Visible = false;
				//txtRecruiterRemark.Visible = false;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);
			//throw;
		}
	}

	public void validationCheckonSaveTemp()
	{
		// 

		if (TxtTotalExperienceYrs.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Total Experience In(Year)";
			return;
		}
		else if (TxtRelevantExpYrs.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Relevant Experience In(Year)";
			return;
		}

		else if (Txt_CurrentCTC_Fixed.Text.Trim() == "" || Txt_CurrentCTC_Fixed.Text.Trim() == "0" || Txt_CurrentCTC_Fixed.Text.Trim() == "0.00")
		{
			lblmessageOnsave.Text = "Please Enter Current CTC_Fixed In(lakh)";
			return;
		}
		else if (TxtCurrentCTC_Variable.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Current CTC_Variable In(lakh)";
			return;
		}
		else if (TxtExpCTC_Fixed.Text.Trim() == "" || TxtExpCTC_Fixed.Text.Trim() == "0" || TxtExpCTC_Fixed.Text.Trim() == "0.00")
		{
			lblmessageOnsave.Text = "Please Enter Exp. CTC_Fixed In(lakh)";
			return;
		}
		else if (TxtExpCTC_Variable.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Exp. CTC_Variable In(lakh)";
			return;
		}
		else if (TxtExpCTC_Total.Text.Trim() != "")
		{
			if (!Chk_Exception.Checked)
			{
				if (Convert.ToDecimal(TxtExpCTC_Total.Text) > Convert.ToDecimal(txtSalaryRangeTo.Text))
				{
					//TxtExpCTC_Fixed.Text = "";
					//TxtExpCTC_Variable.Text = "";
					//TxtExpCTC_Total.Text = "";
					lblmessageOnsave.Text = "Salary Range for this requisition is " + txtSalaryRangeFrom.Text + " - " + txtSalaryRangeTo.Text + " lacs.You are allowed to enter Salary range within " + txtSalaryRangeTo.Text + " lacs.";
					return;
				}
			}
		}

		if (Chk_Exception.Checked)
		{
			if (txtRecruiterRemark.Text.Trim()=="")
			{
				lblmessageOnsave.Text = "Please Enter Recruiter Remark ";
				return;
			}
		}
		else if (DDlCurrentlyonnotice.SelectedValue == "" || DDlCurrentlyonnotice.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Currently On Notice";
			return;
		}
		else if (Txt_NoticePeriodInday.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Notice Period( In Days)";
			return;
		}
		else if (Txt_CurrentLocation.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Current Location";
			return;
		}
		else if (Txt_NativeHomeLocation.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Native /Home Location";
			return;
		}
		else if (Txt_BaseLocationcurrentcompany.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Base Location in current company";
			return;
		}
		else if (DDLBaseLocationPreference.SelectedValue == "" || DDLBaseLocationPreference.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Base Location Preference";
			return;
		}
		else if (DDLRelocateTravelAnyLocation.SelectedValue == "" || DDLRelocateTravelAnyLocation.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Is he ready to relocate and travel to any locations in India & Abroad for project implementations";
			return;
		}
		else if (Txt_TravelContraintPandemicSituation.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Travel Contraint in Pandemic Situation";
			return;
		}
		else if (DDLOpenToTravel.SelectedValue == "" || DDLOpenToTravel.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Open to Travel";
			return;
		}
		else if (DDlpayrollsCompany.SelectedValue == "" || DDlpayrollsCompany.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Candidate is on whose payrolls today—IT company or third party";
			return;
		}
		else if (DDLImplementationprojectWorkedOn.SelectedValue == "" || DDLImplementationprojectWorkedOn.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select How many full life E2E implementation projects have you worked on?";
			return;
		}
		else if (DDLDomainExperence.SelectedValue == "" || DDLDomainExperence.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select What is your TOTAL Domain experience";
			return;
		}
		else if (DDLSAPExperence.SelectedValue == "" || DDLSAPExperence.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select What is your TOTAL SAP experience";
			return;
		}

		else if (DDLSupportproject.SelectedValue == "" || DDLSupportproject.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select How many Support Project";
			return;
		}
		else if (DDLPhaseWorkimplementation.SelectedValue == "" || DDLPhaseWorkimplementation.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Which of the phases in implementation you have worked";
			return;
		}
		else if (DDLRolesPlaydImplementation.SelectedValue == "" || DDLRolesPlaydImplementation.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select What roles have you played in implementation projects so far?";
			return;
		}
		else if (DDLprojecthandled.SelectedValue == "" || DDLprojecthandled.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select What type of projects have you handled?";
			return;
		}

		else if (DDLBreakInService.SelectedValue == "" || DDLBreakInService.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Whether there is any break in service. If Yes - reason";
			return;
		}
		else if (DDLBreakInService.SelectedValue == "1")
		{
			if (TxtReasonforBreak.Text.Trim() == "")
			{
				lblmessageOnsave.Text = "Please Enter Reason for Break";
				return;
			}
		}
		else if (DDLnatureOfIndustryClient.SelectedValue == "" || DDLnatureOfIndustryClient.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Nature of Industry of the clients";
			return;
		}
		else if (DDLnatureOfIndustryClient.SelectedValue == "5")
		{
			if (Txt_OtherNatureOfIndustryClient.Text.Trim() == "")
			{
				lblmessageOnsave.Text = "Please Enter Other Nature Of Industry Client";
				return;
			}
		}

		if (DDLCommunicationSkill.SelectedValue == "" || DDLCommunicationSkill.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Check communication skill--Virtual";
			return;
		}
		if (Txt_lookingforChange.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Why is he looking for a change";
			return;
		}

		if (Txt_CurrentRoleorganization.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter His current Role in the organization- Consultant, Team lead, Solution architect, Project Manager.";
			return;
		}
		if (Txt_RoleDomaincompany.Text.Trim() == "")
		{
			lblmessageOnsave.Text = "Please Enter Role in Domain company";
			return;
		}
		if (DDLAssignInterviewSchedulars.SelectedValue == "" || DDLAssignInterviewSchedulars.SelectedValue == "0")
		{
			lblmessageOnsave.Text = "Please Select Interview Schedular";
			return;
		}

	}

	protected void lstboxChecksendforshortlisting_CheckedChanged(object sender, EventArgs e)
	{
		foreach (GridViewRow row in gvSearchCandidateList.Rows)
		{
			if (row.RowType == DataControlRowType.DataRow)
			{
				CheckBox chkRow = (row.Cells[5].FindControl("lstboxChecksendforshortlisting") as CheckBox);
				ImageButton lnkEditt = (row.Cells[6].FindControl("lnkEdit") as ImageButton);
				if (chkRow.Checked == true)
				{
					lnkEditt.Enabled = true;
				}
				else
				{
					lnkEditt.Enabled = false;
				}
			}
		}
	}

	protected void gvSearchCandidateList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			CheckBox Checkboxsendforshortlist = e.Row.FindControl("lstboxChecksendforshortlisting") as CheckBox;
			HiddenField hdCandidateBlockBy1 = e.Row.FindControl("hdCandidateBlockBy") as HiddenField;
			ImageButton lnkEditt = e.Row.FindControl("lnkEdit") as ImageButton;

			if (hdCandidateBlockBy1.Value != "")
			{
				Checkboxsendforshortlist.Checked = true;
				lnkEditt.Enabled = true;

			}

		}
	}

	protected void Txt_CurrentCTC_Fixed_TextChanged(object sender, EventArgs e)
	{
		decimal valueOne = string.IsNullOrEmpty(Txt_CurrentCTC_Fixed.Text) ? 0 : Convert.ToDecimal(Txt_CurrentCTC_Fixed.Text);
		decimal valueTwo = string.IsNullOrEmpty(TxtCurrentCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtCurrentCTC_Variable.Text);
		TxtCurrentCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
	}

	protected void TxtCurrentCTC_Variable_TextChanged(object sender, EventArgs e)
	{
		decimal valueOne = string.IsNullOrEmpty(Txt_CurrentCTC_Fixed.Text) ? 0 : Convert.ToDecimal(Txt_CurrentCTC_Fixed.Text);
		decimal valueTwo = string.IsNullOrEmpty(TxtCurrentCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtCurrentCTC_Variable.Text);
		TxtCurrentCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
	}

	protected void TxtExpCTC_Fixed_TextChanged(object sender, EventArgs e)
	{
		//txtSalaryRangeTo  txtSalaryRangeFrom
		string msg = "";
		decimal Result = 0;
		decimal SalaryRageTo = string.IsNullOrEmpty(txtSalaryRangeTo.Text) ? 0 : Convert.ToDecimal(txtSalaryRangeTo.Text);
		decimal SalaryRageFrom = string.IsNullOrEmpty(txtSalaryRangeFrom.Text) ? 0 : Convert.ToDecimal(txtSalaryRangeFrom.Text);
		decimal valueOne = string.IsNullOrEmpty(TxtExpCTC_Fixed.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Fixed.Text);
		decimal valueTwo = string.IsNullOrEmpty(TxtExpCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Variable.Text);
		Result = Convert.ToDecimal(valueOne + valueTwo);
		if (!Chk_Exception.Checked)
		{
			if (Result > SalaryRageTo)
			{
				//TxtExpCTC_Fixed.Text = "";
				//TxtExpCTC_Variable.Text = "";
				//valueOne = 0; valueTwo = 0;
				Chk_Exception.Visible = true;
				msg = "Salary Range for this requisition is " + SalaryRageFrom + " - " + SalaryRageTo + " lacs. You are allowed to enter Salary range within " + SalaryRageTo + " lacs.";
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + msg + "')", true);
				TxtExpCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
				TxtExpCTC_Fixed.Focus();
				return;
			}
			
		}
		if (Result > SalaryRageTo)
		{
			Chk_Exception.Visible = true;
		}
		else
		{
			Chk_Exception.Checked = false;
			Chk_Exception.Visible = false;
			ExceptionR.Visible = false;
		}
			TxtExpCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
	}

	protected void TxtExpCTC_Variable_TextChanged(object sender, EventArgs e)
	{
		decimal valueOne = string.IsNullOrEmpty(TxtExpCTC_Fixed.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Fixed.Text);
		decimal valueTwo = string.IsNullOrEmpty(TxtExpCTC_Variable.Text) ? 0 : Convert.ToDecimal(TxtExpCTC_Variable.Text);
		TxtExpCTC_Total.Text = (valueOne + valueTwo).ToString("N2");
	}

	protected void DDLnatureOfIndustryClient_SelectedIndexChanged(object sender, EventArgs e)
	{
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
	}

	protected void DDLBreakInService_SelectedIndexChanged(object sender, EventArgs e)
	{
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



	protected void Chk_Exception_CheckedChanged(object sender, EventArgs e)
	{
		if (Chk_Exception.Checked)
		{
			ExceptionR.Visible = true;
		}
		else
		{
			ExceptionR.Visible = false;
			txtRecruiterRemark.Text = "";
		}
	}
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