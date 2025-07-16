using AjaxControlToolkit;
using Microsoft.Ajax.Utilities;
using System;
using System.Activities.Expressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class procs_InterviewerFeedbac : System.Web.UI.Page
{
	public SqlDataAdapter sqladp;
	public static string dob1;
	public string userid;
	public int did = 0;
	SP_Methods spm = new SP_Methods();
	DataSet dsRecCandidate, dsRecEmpCodeInterviewer1, dsCandidateData, dsCVSource, dtIrSheetReport;
	public DataTable dtInterviewerSchedule, dtcandidateDetails, dtmainSkillSet, dtInterviewer1, dtIRsheetcount, dtMerge, NewDTValue, DTMaintable, DTInterviews, NewDT;
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
					//  hdfilefathIRSheet.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheet"]).Trim());
					HFQuestionnaire.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["QuestionnaireDocumentpath"]).Trim());
					hdfilefathIRSheet.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheetBLANKFile"]).Trim());
					hdnPhoto.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_InterviewerPhoto"]).Trim());
                   // InterviewFeedback.Value = Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["InterviewFeedback_ID"]).Trim());

                    hdRecruitment_ReqID.Value = Convert.ToString(Request.QueryString[0]).Trim();
					hdCandidate_ID.Value = Convert.ToString(Request.QueryString[1]).Trim();
					HFDCandidateScheduleRound_ID.Value = Convert.ToString(Request.QueryString[2]).Trim();
                   // InterviewFeedback.Value = Convert.ToString(Request.QueryString[3]).Trim();
                    checkInterviewerShortlistStatus_Submit();

					string[] filePaths = Directory.GetFiles(hdfilefathIRSheet.Value);
					string[] files = Directory.GetFiles(hdfilefathIRSheet.Value);



					for (int iFile = 0; iFile < files.Length; iFile++)
					{
						string fn = new FileInfo(files[iFile]).Name;
						//lnkbtnIRSheet.Text = fn;
						//hdfilenameIRSheet.Value = fn;
					}
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
					GetecruitmentDetail();
					InterviewerMappingRoundWiseDisplaySalarySection();
					FeedbackdropdownBind();
					Current_IRsheetdetails();
					getIrSheet();
					getIrSheetSummary();
					GetInterviewType();

				}
			}

			if(DDLInterviewStatus.SelectedValue=="1")
			{

				if (Convert.ToString(HiddenFieldInterviewFeedback.Value).Trim() == "7")
				{
                    liCandidatePhoto.Visible = false;
                      	FilePhoto.Visible = false;
					//viewphoto.Visible = true;
                }


            }
			if (DDLInterviewStatus.SelectedValue == "1")
			{

                if (Convert.ToString(Hiddenphoto.Value).Trim() != "")
                {

					viewphoto.Visible = true;
				}


			}

			//        if (DDLInterviewStatus.SelectedValue == "1")
			//        {
			//if(DDLInterviewFeedback.SelectedValue=="7")
			//{
			//                liCandidatePhoto.Visible = false;
			//                	FilePhoto.Visible = false;
			//            }
			//            //getInterviewAlldropDown();
			//            //            DataRow[] dr3 = dsCVSource.Tables[4].Select("InterviewFeedback_ID=7");
			//            //            if (dr3.Length > 0)
			//            //            {
			//            //                liCandidatePhoto.Visible = false;
			//            //	FilePhoto.Visible = false;
			//            //            }
			//        }


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
		try
		{
			string[] strdate;
			string PhotoFiles = "";
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			DataSet dsInterviewSchedule = new DataSet();
			lblmessage.Text = "";
			if (DDLInterviewStatus.SelectedValue == "")
			{
				lblmessage.Text = "Please Select the Interview Status";
				return;
			}

			if (DDLInterviewFeedback.SelectedValue == "")
			{
				lblmessage.Text = "Please Select the Interview Feedback";
				return;
			}
			if (lstInterviewType.SelectedValue == "" || lstInterviewType.SelectedValue == "0")
			{
				lblmessage.Text = "Please Select the Interview Type";
				return;
			}
			if (TxtInterviewerComments.Text == "")
			{
				lblmessage.Text = "Please Enter the Interview comment";
				return;
			}
			if (DDLInterviewFeedback.SelectedValue == "2")
			{
				if (chkbonxtround.Checked == false)
				{
					lblmessage.Text = "Please Select the Next round";
					return;
				}
			}
			if (DDLInterviewStatus.SelectedValue == "1")
			{
				dtIRsheetcount = spm.GetIRsheetCount(Convert.ToInt32(HFDCandidateScheduleRound_ID.Value), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
				if (dtIRsheetcount.Rows.Count == 0)
				{
					lblmessage.Text = "Please Fill Complete IR Sheet!";
					return;
				}
			}

			//        if (DDLInterviewStatus.SelectedValue == "1")
			//        {
			//if (DDLInterviewFeedback.SelectedValue == "7")
			//{
			//	liCandidatePhoto.Visible = false;
			//}
			//        }




			if (Convert.ToString(HiddenFieldInterviewFeedback.Value).Trim() != "7")
			{


				if (DDLInterviewStatus.SelectedValue == "1")
            {
                if (lstInterviewType.SelectedValue == "2")
                {
                    if (Convert.ToString(FilePhoto.FileName).Trim() == "")
                    {
                        lblmessage.Text = "Please upload candidate photo";
                        return;
                    }
                }
            }
            }//}

            if (DDLInterviewStatus.SelectedValue != "2")
			{
				if (DDLInterviewStatus.SelectedValue != "3")
				{
					var supportedTypes = new[] { "doc", "docx", "xls", "xlsx" };
					HttpFileCollection fileCollection1 = Request.Files;
					for (int i = 0; i < fileCollection1.Count; i++)
					{
						HttpPostedFile uploadfileName = fileCollection1[i];
						string fileName = Path.GetFileName(uploadfileName.FileName);
						if (fileName != "")
						{
							// var fileExta = System.IO.Path.GetExtension(fileName).Substring(1);
							//if (!supportedTypes.Contains(fileExta))
							//{
							//    lblmessage.Text = "File Extension Is InValid - Only Upload WORD/EXCEL";
							//    return;
							//}
						}
					}
				}
			}

			if (DDLInterviewStatus.SelectedValue == "1")
			{
				if (lstInterviewType.SelectedValue == "2")
				{
					var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".JPG", ".JPEG", ".PNG", ".BMP" };
					if (FilePhoto.HasFile)
					{
						string extension = System.IO.Path.GetExtension(FilePhoto.FileName);
						if (!supportedTypes.Contains(extension))
						{
							lblmessage.Text = "File Extension Is InValid - Only Upload jpg, jpeg,png, bmp";
							return;
						}

					}
				}
			}


            
            //if (DDLInterviewStatus > 1 && )
            //{

            //}

            //            if (DDLInterviewFeedback.SelectedValue == "3")
            //           {
            //                if (HFFlagCheckFinilized.Value == "1")
            //                {
            //                    if (HFFlagCheckFinilizedNotJoin.Value == "1")
            //                    {
            //                        lblmessage.Text = "";
            //                    }
            //                    else
            //                    {
            //                        lblmessage.Text = "Since One candidate is already finalized, you cannot finalize this candidate!";
            //                        return;
            //                    }
            //                }
            //                
            //            }



            if (lblmessage.Text == "")
			{
				if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
				{
					HttpFileCollection fileCollection = Request.Files;
					for (int i = 0; i < fileCollection.Count; i++)
					{
						HttpPostedFile uploadfileName = fileCollection[i];
						string fileName = Path.GetFileName(uploadfileName.FileName);
						string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
						if (uploadfileName.ContentLength > 0)
						{
							multiplefilename = fileName;
							string strfileName = "";
							//  string strremoveSpace = txtName.Text + "_" + Txt_CandidateMobile.Text + "_" + multiplefilename;
							string strremoveSpace = hdCandidate_ID.Value.Trim() + "_" + DDLInterviewRound.SelectedValue + "_" + str + "_" + multiplefilename;
							strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
							strfileName = strremoveSpace;
							multiplefilename = strfileName;
							uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheet"]).Trim()), strfileName));
							multiplefilenameadd += strfileName + ",";
						}
					}
				}

				if (Convert.ToString(FilePhoto.FileName).Trim() != "")
				{
					//HttpFileCollection fileCollection = Request.Files;					
					//HttpPostedFile uploadfileName = fileCollection[0];
					string fileName = FilePhoto.FileName;
					string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
					//if (uploadfileName.ContentLength > 0)
					PhotoFiles = fileName;
					string strfileName = "";
					string strremoveSpace = hdCandidate_ID.Value.Trim() + "_" + DDLInterviewRound.SelectedValue + "_" + str + "_" + PhotoFiles;
					//strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
					strremoveSpace = Regex.Replace(strremoveSpace, @"[^0-9a-zA-Z\._]", "_");
					strfileName = strremoveSpace;
					PhotoFiles = strfileName;
					FilePhoto.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_InterviewerPhoto"]).Trim()), strfileName));
				}

				if (Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "")
				{
					SqlParameter[] sparsE = new SqlParameter[3];
					sparsE[0] = new SqlParameter("@stype", SqlDbType.VarChar);
					sparsE[0].Value = "CheckHoldCandidate";

					sparsE[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.VarChar);
					sparsE[1].Value = Convert.ToString(hdRecruitment_ReqID.Value).Trim();

                    sparsE[2] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.VarChar);
                    sparsE[2].Value = Convert.ToString(hdCandidate_ID.Value).Trim();

                    DataTable dt = spm.getDropdownList(sparsE, "SP_GetRecruitment_Interviewerfeedback");

					if (DDLInterviewFeedback.SelectedValue == "3")
					{
						if (dt.Rows.Count > 0)
						{
                            lblmessage.Text = "This Requisition Against Onhold Candidate Please Contact Recruiter.";
                            mobile_btnBack.Visible = true;
                            return; 
						}
					}
				}
				//return;
				SqlParameter[] spars = new SqlParameter[14];
				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "InterviewScheduleFeedbackUpdate";
				spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
				spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
				spars[2].Value = Session["Empcode"].ToString();
				spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
				spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);
				spars[4] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.Int);
				spars[4].Value = Convert.ToInt32(HFDCandidateScheduleRound_ID.Value);
				spars[5] = new SqlParameter("@InterviewStatus_ID", SqlDbType.Int);
				spars[5].Value = Convert.ToInt32(DDLInterviewStatus.SelectedValue);
				spars[6] = new SqlParameter("@InterviewFeedback_ID", SqlDbType.Int);
				spars[6].Value = Convert.ToInt32(DDLInterviewFeedback.SelectedValue);
				spars[7] = new SqlParameter("@NextRound", SqlDbType.VarChar);
				spars[7].Value = chkbonxtround.Checked;
				spars[8] = new SqlParameter("@InterviewerComments", SqlDbType.VarChar);
				spars[8].Value = TxtInterviewerComments.Text;
				spars[9] = new SqlParameter("@IRSheet", SqlDbType.VarChar);
				spars[9].Value = "";
				spars[10] = new SqlParameter("@multiplefilenameadd", SqlDbType.VarChar);
				spars[10].Value = multiplefilenameadd;
				spars[11] = new SqlParameter("@Photo", SqlDbType.VarChar);
				spars[11].Value = PhotoFiles;
				spars[12] = new SqlParameter("@InterviewTypeID", SqlDbType.Int);
				spars[12].Value = Convert.ToInt32(lstInterviewType.SelectedValue);

				dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");

				string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_RecruiterReschedule"]).Trim() + "?ReqID=" + hdRecruitment_ReqID.Value + "&CanID=" + hdCandidate_ID.Value + "&CSRID=" + HFDCandidateScheduleRound_ID.Value;
				// string StrRecruitername = dsInterviewSchedule.Tables[0].Rows[0]["Recruitername"].ToString();
				string StrInterviewerSchedularName = dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_Name"].ToString();
				string StrInterviewerName = dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_NameCandidateScheduleRound"].ToString();

				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();
				string mailsubject = "";
				string mailcontain = "";
				if (DDLInterviewStatus.SelectedValue == "2")
				{
					mailsubject = "Recruitment - Re-schedule Interview for the Candidate against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please re-schedule interview for the below candidate;";
					spm.send_mailto_InterviewFeedbackAllStatus(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailsubject, StrInterviewerSchedularName, txtName.Text, DDLInterviewRound.SelectedItem.Text, DDLInterviewStatus.SelectedItem.Text, DDLInterviewFeedback.SelectedItem.Text, TxtInterviewerComments.Text, strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrInterviewerName);

				}
				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "2")
				{
					mailsubject = "Recruitment - Schedule Next round of Interview for the Candidate against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please schedule next round of interview for the below candidate;";
					spm.send_mailto_InterviewFeedbackAllStatus(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailsubject, StrInterviewerSchedularName, txtName.Text, DDLInterviewRound.SelectedItem.Text, DDLInterviewStatus.SelectedItem.Text, DDLInterviewFeedback.SelectedItem.Text, TxtInterviewerComments.Text, strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrInterviewerName);
				}
				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "1")
				{
					mailsubject = "Recruitment - Candidate Rejected against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate is rejected. Please send few more profiles for shortlisting.";
					spm.send_mailto_InterviewFeedbackAllStatus(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailsubject, StrInterviewerSchedularName, txtName.Text, DDLInterviewRound.SelectedItem.Text, DDLInterviewStatus.SelectedItem.Text, DDLInterviewFeedback.SelectedItem.Text, TxtInterviewerComments.Text, strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrInterviewerName);
				}

                if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "7")
                {
                    mailsubject = "Recruitment - Candidate OnHold against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
                    mailcontain = "Following candidate OnHold. Please send few more profiles for shortlisting.";
                    spm.send_mailto_InterviewFeedbackAllStatus(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailsubject, StrInterviewerSchedularName, txtName.Text, DDLInterviewRound.SelectedItem.Text, DDLInterviewStatus.SelectedItem.Text, DDLInterviewFeedback.SelectedItem.Text, TxtInterviewerComments.Text, strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrInterviewerName);
                }

                if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "3")
				{
					mailsubject = "Recruitment - Candidate Finalized against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate is finalized. Please start hiring process for the same.";

					string StrRecruitername = dsInterviewSchedule.Tables[0].Rows[0]["Recruitername"].ToString();
					string mailrec = "";
					string strinterviewshedulermail = dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString();
					//  DataTable dtApproverEmailIds = spm.Get_Requisition_ApproverEmailID(HFempcoderec.Value);
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
					mailrec = strinterviewshedulermail + "," + mailrec;
					spm.send_mailto_InterviewFeedbackAllStatus(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailrec, mailsubject, StrRecruitername, txtName.Text, DDLInterviewRound.SelectedItem.Text, DDLInterviewStatus.SelectedItem.Text, DDLInterviewFeedback.SelectedItem.Text, TxtInterviewerComments.Text, strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrInterviewerName);
				}
				if (DDLInterviewFeedback.SelectedValue == "1" || DDLInterviewFeedback.SelectedValue == "3")
				{
					CheckReferral_Candidated(Convert.ToString(hdCandidate_ID.Value), DDLInterviewFeedback.SelectedValue);
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
	protected void trvldeatils_btnSave_Click(object sender, EventArgs e)
	{
		try
		{
			string[] strdate; string PhotoFiles = "";
			string confirmValue = hdnYesNo.Value.ToString();
			if (confirmValue != "Yes")
			{
				return;
			}
			DataSet dsInterviewSchedule = new DataSet();
			lblmessage.Text = "";
			if (DDLInterviewStatus.SelectedValue == "")
			{
				lblmessage.Text = "Please Select the Interview Status";
				return;
			}

			if (DDLInterviewFeedback.SelectedValue == "")
			{
				lblmessage.Text = "Please Select the Interview Feedback";
				return;
			}
			if (lstInterviewType.SelectedValue == ""|| lstInterviewType.SelectedValue == "0")
			{
				lblmessage.Text = "Please Select the Interview Type";
				return;
			}
			if (DDLInterviewStatus.SelectedValue == "1")
			{
				dtIRsheetcount = spm.GetIRsheetCount(Convert.ToInt32(HFDCandidateScheduleRound_ID.Value), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
				if (dtIRsheetcount.Rows.Count == 0)
				{
					lblmessage.Text = "Please Fill Complete IR Sheet!";
					return;
				}
			}
			if (TxtInterviewerComments.Text == "")
			{
				lblmessage.Text = "Please Enter the Interview comment";
				return;
			}
			if (DDLInterviewFeedback.SelectedValue == "2")
			{
				if (chkbonxtround.Checked == false)
				{
					lblmessage.Text = "Please Select the Next round";
					return;
				}
			}
			if (DDLInterviewFeedback.SelectedValue == "2")
			{
				if (chkbonxtround.Checked == false)
				{
					lblmessage.Text = "Please Select the Next round";
					return;
				}
			}
			if (Convert.ToString(HiddenFieldInterviewFeedback.Value).Trim() != "7")
			{


				if (DDLInterviewStatus.SelectedValue == "1")
            {
                if (lstInterviewType.SelectedValue == "2")
                {
                    if (Convert.ToString(FilePhoto.FileName).Trim() == "")
                    {
                        lblmessage.Text = "Please upload candidate photo";
                        return;
                    }
                }
            }

            }

            if (DDLInterviewStatus.SelectedValue != "2")
			{
				var supportedTypes = new[] { "doc", "docx", "xls", "xlsx" };
				HttpFileCollection fileCollection1 = Request.Files;
				for (int i = 0; i < fileCollection1.Count; i++)
				{
					HttpPostedFile uploadfileName = fileCollection1[i];
					string fileName = Path.GetFileName(uploadfileName.FileName);
					if (fileName != "")
					{
						//var fileExta = System.IO.Path.GetExtension(fileName).Substring(1);
						//if (!supportedTypes.Contains(fileExta))
						//{
						//    lblmessage.Text = "File Extension Is InValid - Only Upload WORD/EXCEL";
						//    return;
						//}
					}
				}
			}
			if (DDLInterviewRound.SelectedValue == "5")
			{
				if (DDLInterviewFeedback.SelectedValue == "2")
				{
					lblmessage.Text = "You can not schedule Next round. It is last Interview Round";
					return;
				}
			}
			if (DDLInterviewStatus.SelectedValue == "1")
			{
				if (lstInterviewType.SelectedValue == "2")
				{
					var supportedTypes = new[] { ".jpg", ".jpeg", ".png", ".bmp", ".JPG", ".JPEG", ".PNG", ".BMP" };
					if (FilePhoto.HasFile)
					{
						string extension = System.IO.Path.GetExtension(FilePhoto.FileName);
						if (!supportedTypes.Contains(extension))
						{
							lblmessage.Text = "File Extension Is InValid - Only Upload jpg, jpeg, png, bmp";
							return;
						}

					}
				}
			}

			if (lblmessage.Text == "")
			{
				if (Convert.ToString(uploadotherfile.FileName).Trim() != "")
				{
					HttpFileCollection fileCollection = Request.Files;
					for (int i = 0; i < fileCollection.Count; i++)
					{
						HttpPostedFile uploadfileName = fileCollection[i];
						string fileName = Path.GetFileName(uploadfileName.FileName);
						if (fileName != "")
						{
							string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
							multiplefilename = fileName;
							string strfileName = "";
							//string strremoveSpace = multiplefilename;
							//strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
							//strfileName = Txt_CandidatePAN.Text + "_" + strremoveSpace;
							// string strremoveSpace = txtName.Text + "_" + str + "_" + multiplefilename;
							string strremoveSpace = hdCandidate_ID.Value.Trim() + "_" + DDLInterviewRound.SelectedValue + "_" + str + "_" + multiplefilename;
							strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
							strfileName = strremoveSpace;
							multiplefilename = strfileName;
							uploadfileName.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_RecruiterIRSheet"]).Trim()), strfileName));
							multiplefilenameadd += strfileName + ",";
						}
					}
				}
				if (Convert.ToString(FilePhoto.FileName).Trim() != "")
				{
					//HttpFileCollection fileCollection = Request.Files;					
					//HttpPostedFile uploadfileName = fileCollection[0];
					string fileName = FilePhoto.FileName;
					string str = DateTime.Now.ToString("yyyy_MM_dd_HHmmss");
					//if (uploadfileName.ContentLength > 0)
					PhotoFiles = fileName;
					string strfileName = "";
					string strremoveSpace = hdCandidate_ID.Value.Trim() + "_" + DDLInterviewRound.SelectedValue + "_" + str + "_" + PhotoFiles;
					//strremoveSpace = Regex.Replace(strremoveSpace, @"\s", "");
					strremoveSpace = Regex.Replace(strremoveSpace, @"[^0-9a-zA-Z\._]", "_");
					strfileName = strremoveSpace;
					PhotoFiles = strfileName;
					FilePhoto.SaveAs(Path.Combine(Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["Rec_InterviewerPhoto"]).Trim()), strfileName));
				}

				SqlParameter[] spars = new SqlParameter[14];
				spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
				spars[0].Value = "InterviewScheduleFeedbackUpdate";
				spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
				spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
				spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
				spars[2].Value = Session["Empcode"].ToString();
				spars[3] = new SqlParameter("@Candidate_ID", SqlDbType.Int);
				spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);
				spars[4] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.Int);
				spars[4].Value = Convert.ToInt32(HFDCandidateScheduleRound_ID.Value);
				spars[5] = new SqlParameter("@InterviewStatus_ID", SqlDbType.Int);
				spars[5].Value = Convert.ToInt32(DDLInterviewStatus.SelectedValue);
				spars[6] = new SqlParameter("@InterviewFeedback_ID", SqlDbType.Int);
				spars[6].Value = Convert.ToInt32(DDLInterviewFeedback.SelectedValue);
				spars[7] = new SqlParameter("@NextRound", SqlDbType.VarChar);
				spars[7].Value = chkbonxtround.Checked;
				spars[8] = new SqlParameter("@InterviewerComments", SqlDbType.NVarChar);
				spars[8].Value = TxtInterviewerComments.Text;
				spars[9] = new SqlParameter("@IRSheet", SqlDbType.VarChar);
				spars[9].Value = "";
				spars[10] = new SqlParameter("@multiplefilenameadd", SqlDbType.NVarChar);
				spars[10].Value = multiplefilenameadd;
				spars[11] = new SqlParameter("@Photo", SqlDbType.NVarChar);
				spars[11].Value = PhotoFiles;
				spars[12] = new SqlParameter("@InterviewTypeID", SqlDbType.Int);
				spars[12].Value = Convert.ToInt32(lstInterviewType.SelectedValue);

				dsInterviewSchedule = spm.getDatasetList(spars, "SP_Rec_Interview_Schedule_Insert");
				string strLeaveRstURL = Convert.ToString(ConfigurationManager.AppSettings["Link_RecruiterReschedule"]).Trim() + "?ReqID=" + hdRecruitment_ReqID.Value + "&CanID=" + hdCandidate_ID.Value + "&CSRID=" + HFDCandidateScheduleRound_ID.Value;
				string StrInterviewerSchedularName = dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_Name"].ToString();
				string StrInterviewerName = dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_NameCandidateScheduleRound"].ToString();

				string mailsubject = "";
				string mailcontain = "";
				if (DDLInterviewStatus.SelectedValue == "2")
				{
					mailsubject = "Recruitment - Re-schedule Interview for the Candidate against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please re-schedule interview for the below candidate;";
				}
				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "2")
				{
					mailsubject = "Recruitment - Schedule Next round of Interview for the Candidate against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Please schedule next round of interview for the below candidate;";
				}

				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "1")
				{
					mailsubject = "Recruitment - Candidate Rejected against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate is rejected. Please send few more profiles for shortlisting.";
				}
				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "3")
				{
					mailsubject = "Recruitment - Candidate Finalized against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate is finalized. Please start hiring process for the same.";
				}
		
				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "6")
				{
					mailsubject = "Recruitment - Candidate Rejected against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate is rejected. Please send few more profiles for shortlisting.";
				}

				if (DDLInterviewStatus.SelectedValue == "1" && DDLInterviewFeedback.SelectedValue == "7")
				{
					mailsubject = "Recruitment - Candidate OnHold against request for " + txtReqNumber.Text + " Of " + txtReqName.Text;
					mailcontain = "Following candidate On OnHold. Please send few more profiles for shortlisting.";
				}

				string RequiredByDate = "";
				RequiredByDate = GetRequiredByDate();
				spm.send_mailto_InterviewFeedbackAllStatus(txtReqName.Text, dsInterviewSchedule.Tables[0].Rows[0]["Interviewer_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["InterviewerSchedular_EmpCodeEmailAddress"].ToString(), dsInterviewSchedule.Tables[0].Rows[0]["Recruiter_EmpCodeEmailAddress"].ToString(), mailsubject, StrInterviewerSchedularName, txtName.Text, DDLInterviewRound.SelectedItem.Text, DDLInterviewStatus.SelectedItem.Text, DDLInterviewFeedback.SelectedItem.Text, TxtInterviewerComments.Text, strLeaveRstURL, mailcontain, txtReqNumber.Text.Trim(), txtFromdate.Text.Trim(), RequiredByDate, lstPositionLoca.SelectedItem.Text, lstSkillset.SelectedItem.Text, lstPositionName.SelectedItem.Text, lstPositionDept.SelectedItem.Text, lstPositionBand.SelectedItem.Text, txtNoofPosition.Text, StrInterviewerName);

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

	private void CheckReferral_Candidated(string CandidatedID, string Selected)
	{
		DataTable dtReferral = new DataTable();
		try
		{
			int StatusID = 0; string Result = "";
			if (Selected == "1")
			{
				StatusID = 8;
				//Result = "Rejected";
				Result = "Not Suitable for Current Requirements";
			}
			else
			{
				StatusID = 7;
				Result = "Finalized";
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
	protected void lstCVSource_SelectedIndexChanged(object sender, EventArgs e)
	{
		//if (lstCVSource.SelectedValue == "3")
		//{
		//    Txt_ReferredbyEmpcode.Visible = true;
		//    Txt_ReferredBy.Visible = false;
		//}
		//else
		//{
		//    Txt_ReferredbyEmpcode.Visible = false;
		//    Txt_ReferredBy.Visible = true;
		//}
	}

	protected void DDLInterviewStatus_SelectedIndexChanged(object sender, EventArgs e)
	{
		FeedbackdropdownBind();
		lstInterviewType.SelectedIndex = -1;
		if (DDLInterviewStatus.SelectedValue == "2")
		{
			chkbonxtround.Enabled = false;
			chkbonxtround.Checked = false;
			DDLInterviewFeedback.SelectedValue = "6";
            DDLInterviewFeedback.Enabled = true;
            DDLInterviewFeedback.Enabled = false;
			trvldeatils_btnSave.Enabled = true;
			mobile_btnBack.Visible = false;
			trvldeatils_btnSave.Visible = true;
			trvl_accmo_btn.Visible = false;
			IRsheetDetails.Visible = false;
			//SPPhoto.Visible = false;
		}
		//     else
		//     {
		//         DDLInterviewFeedback.Enabled = true;
		//trvl_accmo_btn.Visible = true;
		//chkbonxtround.Enabled = true;
		//         DDLInterviewFeedback.SelectedValue = "";
		//     }        
		else if (DDLInterviewStatus.SelectedValue == "3")
		{
			chkbonxtround.Enabled = false;
			chkbonxtround.Checked = false;
			DDLInterviewFeedback.SelectedValue = "6";
			DDLInterviewFeedback.Enabled = false;
			trvldeatils_btnSave.Enabled = true;
			mobile_btnBack.Visible = true;
			trvldeatils_btnSave.Visible = false;
			trvl_accmo_btn.Visible = false;
			IRsheetDetails.Visible = false;
		}
		else
		{
			DDLInterviewFeedback.Enabled = true;
			chkbonxtround.Enabled = false;
			chkbonxtround.Checked = true;
			trvl_accmo_btn.Visible = true;
			DDLInterviewFeedback.SelectedValue = "";
			//SPPhoto.Visible = true;
		}
       
        Current_IRsheetdetails();
		DDLInterviewFeedback.Focus();

	}
	protected void DDLInterviewFeedback_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (DDLInterviewFeedback.SelectedValue == "1")
		{
			chkbonxtround.Enabled = false;
			chkbonxtround.Checked = false;
			mobile_btnBack.Visible = true;
			DDLInterviewFeedback.Enabled = true;
			trvldeatils_btnSave.Visible = false;
		}
		if (DDLInterviewFeedback.SelectedValue == "2")
		{
			chkbonxtround.Enabled = false;
			chkbonxtround.Checked = true;
			mobile_btnBack.Visible = false;
			DDLInterviewFeedback.Enabled = true;
			trvldeatils_btnSave.Visible = true;
			chkbonxtround.Focus();
		}
		if (DDLInterviewFeedback.SelectedValue == "3")
		{
			chkbonxtround.Enabled = false;
			chkbonxtround.Checked = false;
			mobile_btnBack.Visible = true;
			DDLInterviewFeedback.Enabled = true;
			trvldeatils_btnSave.Visible = false;
		}
		if (DDLInterviewFeedback.SelectedValue == "6")
		{
			chkbonxtround.Enabled = false;
			mobile_btnBack.Visible = true;
			DDLInterviewFeedback.Enabled = true;
			trvldeatils_btnSave.Visible = false;
		}
		if (DDLInterviewFeedback.SelectedValue == "7")
		{
			mobile_btnBack.Visible = true;
			
        }

		
	}

	#region  All_Methods
	private void Current_IRsheetdetails()
	{
		int CandidateScheduleRound = 0, Recruitment_ReqID = 0, Candidate_ID = 0;
		CandidateScheduleRound = Convert.ToString(HFDCandidateScheduleRound_ID.Value).Trim() != "" ? Convert.ToInt32(HFDCandidateScheduleRound_ID.Value) : 0;
		Recruitment_ReqID = Convert.ToString(hdRecruitment_ReqID.Value).Trim() != "" ? Convert.ToInt32(hdRecruitment_ReqID.Value) : 0;
		Candidate_ID = Convert.ToString(hdCandidate_ID.Value).Trim() != "" ? Convert.ToInt32(hdCandidate_ID.Value) : 0;
		dtIRsheetcount = spm.GetIRsheetCount(Convert.ToInt32(CandidateScheduleRound), Convert.ToInt32(Recruitment_ReqID), Convert.ToInt32(Candidate_ID));
		if (dtIRsheetcount.Rows.Count == 1)
		{
			trvl_accmo_btn.Visible = false;
			lblmessage.Text = "You already actioned for this IR Sheet Details";
			return;
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
			SqlParameter[] spars = new SqlParameter[7];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "check_Interviewfeedback_Status";
			spars[1] = new SqlParameter("@req_id", SqlDbType.Decimal);
			spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@emp_code", SqlDbType.VarChar);
			spars[2].Value = Convert.ToString(Session["Empcode"]).Trim();
			spars[3] = new SqlParameter("@CandiadteID", SqlDbType.VarChar);
			spars[3].Value = hdCandidate_ID.Value;
			spars[4] = new SqlParameter("@CandidateRoundID", SqlDbType.VarChar);
			spars[4].Value = HFDCandidateScheduleRound_ID.Value;
            //spars[5] = new SqlParameter("@InterviewFeedback_ID", SqlDbType.VarChar);
            //spars[5].Value = InterviewFeedback.Value;
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
		string Result1 = "Canceled";
		DataRow[] dr3 = dsCVSource.Tables[3].Select("InterviewStatus='" + Result1 + "'");
		if (dr3.Length > 0)
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
		string Result2 = "Backout";
		DataRow[] dr4 = dsCVSource.Tables[3].Select("InterviewStatus='" + Result2 + "'");
		if (dr4.Length > 0)
		{
			string itemValue = "3";
			if (DDLInterviewStatus.Items.FindByValue(itemValue) != null)
			{
				string itemText = DDLInterviewStatus.Items.FindByValue(itemValue).Text;
				ListItem li = new ListItem();
				li.Text = itemText;
				li.Value = itemValue;
				DDLInterviewStatus.Items.Remove(li);
			}
		}

	}
	public void FeedbackdropdownBind()
	{
		dsCVSource = spm.GetCVSource();
		DataView dataView = dsCVSource.Tables[10].DefaultView;
		string strFilter = "(InterEmpCode ='" + Convert.ToString(Session["Empcode"]).Trim() + "' AND ModuleId ='" + lstSkillset.SelectedValue + "' )";
		dataView.RowFilter = strFilter;
		if (dataView.Count > 0)
		{
			DDLInterviewFeedback.DataSource = dsCVSource.Tables[4];
		}
		else
		{
			DDLInterviewFeedback.DataSource = dsCVSource.Tables[9];
		}
		DDLInterviewFeedback.DataTextField = "InterviewFeedback";
		DDLInterviewFeedback.DataValueField = "InterviewFeedback_ID";
		DDLInterviewFeedback.DataBind();
		DDLInterviewFeedback.Items.Insert(0, new ListItem("Select Feedback", ""));

		if (DDLInterviewStatus.SelectedValue == "1")
		{
			DataRow[] dr3 = dsCVSource.Tables[4].Select("InterviewFeedback_ID=6");
			if (dr3.Length > 0)
			{
				string itemValue = "6";
				if (DDLInterviewFeedback.Items.FindByValue(itemValue) != null)
				{
					string itemText = DDLInterviewFeedback.Items.FindByValue(itemValue).Text;
					ListItem li = new ListItem();
					li.Text = itemText;
					li.Value = itemValue;
					DDLInterviewFeedback.Items.Remove(li);
				}
			}
		}

       


        //DDLInterviewRound.SelectedValue == "2" || commnet for bharat 29-09-21
        if (DDLInterviewRound.SelectedValue == "3" || DDLInterviewRound.SelectedValue == "4" || DDLInterviewRound.SelectedValue == "5")
		{
			DataRow[] dr3 = dsCVSource.Tables[4].Select("InterviewFeedback_ID=2");
			if (dr3.Length > 0)
			{
				string itemValue = "2";
				if (DDLInterviewFeedback.Items.FindByValue(itemValue) != null)
				{
					string itemText = DDLInterviewFeedback.Items.FindByValue(itemValue).Text;
					ListItem li = new ListItem();
					li.Text = itemText;
					li.Value = itemValue;
					DDLInterviewFeedback.Items.Remove(li);
				}
			}
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
			lstInterviewerOneView.DataSource = dtInterviewer;
			lstInterviewerOneView.DataTextField = "EmployeeName";
			lstInterviewerOneView.DataValueField = "EmployeeCode";
			lstInterviewerOneView.DataBind();
			lstInterviewerOneView.Items.Insert(0, new ListItem("Select Screen By", "0"));

			LstRecommPerson.DataSource = dtInterviewer;
			LstRecommPerson.DataTextField = "EmployeeName";
			LstRecommPerson.DataValueField = "EmployeeCode";
			LstRecommPerson.DataBind();
			LstRecommPerson.Items.Insert(0, new ListItem("Select Recommended Person", "0"));

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
			SqlParameter[] spars = new SqlParameter[5];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "RecruitmentReq_InterviewerFeedBackEdit";
			spars[1] = new SqlParameter("@Recruitment_ReqID", SqlDbType.Int);
			spars[1].Value = Convert.ToInt32(hdRecruitment_ReqID.Value);
			spars[2] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
			spars[2].Value = Session["Empcode"].ToString();
			spars[3] = new SqlParameter("@strreqCandidate_ID", SqlDbType.VarChar);
			spars[3].Value = Convert.ToInt32(hdCandidate_ID.Value);
			spars[4] = new SqlParameter("@CandidateScheduleRound_ID", SqlDbType.VarChar);
			spars[4].Value = Convert.ToInt32(HFDCandidateScheduleRound_ID.Value);
			dsRecruitmentDetails = spm.getDatasetList(spars, "SP_GetRecruitment_Interviewerfeedback");

			PopulateCadidateRecruitmentWiseData();

			if (dsRecruitmentDetails.Tables[0].Rows.Count > 0)
			{
				if (dsRecruitmentDetails.Tables[8].Rows.Count > 0)
				{
					HFFlagCheckFinilized.Value = "1";
				}
				if (dsRecruitmentDetails.Tables[9].Rows.Count > 0)
				{
					HFFlagCheckFinilizedNotJoin.Value = "1";
				}
				HFempcoderec.Value = dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Code"].ToString();
				txtReqNumber.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionNumber"]).Trim();
				txtReqName.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["fullNmae"]).Trim();

				txtReqDept.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department"]).Trim();

				txtFromdate.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["RequisitionDate"]).Trim();
				txtReqDesig.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Designation"]).Trim();
				txtReqEmail.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Emp_Emailaddress"]).Trim();
				lstSkillset.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["ModuleId"]).Trim();
				lstPositionName.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionTitle_ID"]).Trim();
				lstPositionCriti.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["PositionCriticality_ID"]).Trim();
				//   lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				txtNoofPosition.Text = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["NoOfPosition"]).Trim();
				lstPositionDept.SelectedValue = Convert.ToString(dsRecruitmentDetails.Tables[0].Rows[0]["Department_id"]).Trim();
				Lnk_Questionnaire.Text = dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"].ToString();
				HFQuestionnairename.Value = dsRecruitmentDetails.Tables[0].Rows[0]["UploadData"].ToString();

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
				txtName.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateName"].ToString();
				txtEmail.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateEmail"].ToString();
				Txt_CandidateMobile.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateMobile"].ToString();
				lstCandidategender.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateGender"].ToString();
				lstMaritalStatus.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["Maritalstatus"].ToString();
				// Txt_CandidateCurrentLocation.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateCurrentLocation"].ToString();
				Txt_CandidateBirthday.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateBirthday"].ToString();
				Txt_CandidatePAN.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidatePAN"].ToString();
				TxtAadharNo.Text = dsRecruitmentDetails.Tables[1].Rows[0]["AdharNo"].ToString();

				//  Txt_CandidateExperence.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateExperience_Years"].ToString();
				// Txt_CandidateCurrentCTC.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateCurrentCTC"].ToString();
				//  Txt_CandidateExpectedCTC.Text = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateExpectedCTC"].ToString();
				lnkuplodedfileResume.Text = dsRecruitmentDetails.Tables[1].Rows[0]["UploadResume"].ToString();

				hdfilename.Value = dsRecruitmentDetails.Tables[1].Rows[0]["UploadResume"].ToString();
				filename = dsRecruitmentDetails.Tables[1].Rows[0]["UploadResume"].ToString();
				lnkuplodedfileResume.Visible = true;
				DDLmainSkillSet.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["ModuleId"].ToString();
				Txt_AdditionalSkillset.Text = dsRecruitmentDetails.Tables[1].Rows[0]["AdditionalSkillset"].ToString();
				//  Txt_EducationQualifacation.Text = dsRecruitmentDetails.Tables[1].Rows[0]["EducationQualification"].ToString();
				//  Txt_Certifications.Text = dsRecruitmentDetails.Tables[1].Rows[0]["Certifications"].ToString();
				TxtInterviewDate.Text = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewDate"].ToString();
				TxtInterviewTime.Text = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewTime"].ToString();
				DDLInterviewRound.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewRound_ID"].ToString();
				if (Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["InterviewFeedback_ID"]).Trim() == "7")
				{
					DDLInterviewStatus.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewStatus_ID"].ToString();
					DDLInterviewFeedback.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewFeedback_ID"].ToString();
					lstInterviewType.SelectedValue = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewTypeID"].ToString();
					TxtInterviewerComments.Text = dsRecruitmentDetails.Tables[1].Rows[0]["InterviewerComments"].ToString();
                    HiddenFieldInterviewFeedback.Value= dsRecruitmentDetails.Tables[1].Rows[0]["InterviewFeedback_ID"].ToString();

                    CandidatePhoto.Text = dsRecruitmentDetails.Tables[1].Rows[0]["Photo"].ToString();

                    Hiddenphoto.Value = dsRecruitmentDetails.Tables[1].Rows[0]["Photo"].ToString();
                    filename = dsRecruitmentDetails.Tables[1].Rows[0]["Photo"].ToString();
                    CandidatePhoto.Visible = true;
					//viewphoto.visible = true;
                }
				// HFDCandidateScheduleRound_ID.Value = dsRecruitmentDetails.Tables[1].Rows[0]["CandidateScheduleRound_ID"].ToString();
				if (dsRecruitmentDetails.Tables[2].Rows.Count > 0)
				{
					gvotherfile.DataSource = dsRecruitmentDetails.Tables[2];
					gvotherfile.DataBind();
				}
				else
				{
					gvotherfile.DataSource = null;
					gvotherfile.DataBind();
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

				if (dsRecruitmentDetails.Tables[11].Rows.Count > 0)
				{
					trvldeatils_btnSave.Visible = false;
					mobile_btnBack.Visible = false;
					DDLInterviewFeedback.Enabled = false;
					DDLInterviewStatus.Enabled = false;
					trvl_accmo_btn.Visible = false;
				}
				if (dsRecruitmentDetails.Tables[0].Rows[0]["Request_status"].ToString().Trim() == "Cancelled")
				{
					trvldeatils_btnSave.Visible = false;
					mobile_btnBack.Visible = false;
					DDLInterviewFeedback.Enabled = false;
					DDLInterviewStatus.Enabled = false;
					trvl_accmo_btn.Visible = false;

				}
				if (dsRecruitmentDetails.Tables[1].Rows.Count > 0)
				{
					if (Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["InterviewStatus_ID"]).Trim() == "1" && Convert.ToString(dsRecruitmentDetails.Tables[1].Rows[0]["InterviewFeedback_ID"]).Trim() == "7")
					{
						trvldeatils_btnSave.Visible = true;
						mobile_btnBack.Visible = true;
						DDLInterviewFeedback.Enabled = true;
						DDLInterviewStatus.Enabled = true;
						trvl_accmo_btn.Visible = true;
                       
                        

                    }
                }
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

	#region Bharat code 22-06-21
	protected void trvl_accmo_btn_Click(object sender, EventArgs e)
	{
		//IRsheetDetails.Visible = true;
		if (IRsheetDetails.Visible)
		{
			IRsheetDetails.Visible = false;
		}
		else
		{
			IRsheetDetails.Visible = true;
		}
	}

	protected void GRDOverallRating_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string lsDataKeyValue = GRDOverallRating.DataKeys[e.Row.RowIndex].Values[0].ToString();
			if (Convert.ToString(lsDataKeyValue).Trim() == "0")
			{
				e.Row.Visible = false;
			}
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			// Find the product drop-down list, you can id (or cell number)
			DropDownList ddlOverallrating = e.Row.FindControl("ddlOverallrating") as DropDownList;
			Label lblRoundName = e.Row.FindControl("lblRoundName") as Label;
			lblRoundName.Text = DDLInterviewRound.SelectedItem.Text.Trim();
			Label lblInterviewrName = e.Row.FindControl("lblInterviewrName") as Label;
			lblInterviewrName.Text = Convert.ToString(Session["emp_loginName"]).Trim();
			DataTable dtrating = spm.GetRecruitment_Req_IRSheet_Rating();
			ddlOverallrating.DataSource = dtrating;
			ddlOverallrating.DataTextField = "RatingName";
			ddlOverallrating.DataValueField = "RatingID";
			ddlOverallrating.DataBind();
			ddlOverallrating.Items.Insert(0, new ListItem("Select Rating", "0"));
		}

	}

	protected void DgvIrSheet_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string lsDataKeyValue = DgvIrSheet.DataKeys[e.Row.RowIndex].Values[1].ToString();
			string lsDataKeyValue2 = DgvIrSheet.DataKeys[e.Row.RowIndex].Values[2].ToString();
			//Bind dropdowllist
			DropDownList ddlRating = (e.Row.FindControl("ddlRating") as DropDownList);
			DataTable dtrating = spm.GetRecruitment_Req_IRSheet_Rating();
			ddlRating.DataSource = dtrating;
			ddlRating.DataTextField = "RatingName";
			ddlRating.DataValueField = "RatingID";
			ddlRating.DataBind();
			ddlRating.Items.Insert(0, new ListItem("Select Rating", "0"));
			if (e.Row.Cells[1].Text.Trim() == "&nbsp;")
			{
				//e.Row.Cells[1].Visible = false;
				e.Row.Visible = false;
			}

			if (Convert.ToString(lsDataKeyValue).Trim() == "2" || Convert.ToString(lsDataKeyValue).Trim() == "0")
			{
				TextBox Remark = (TextBox)e.Row.FindControl("txtObservationRemark");
				Remark.Visible = false;
				DropDownList Ratting = (DropDownList)e.Row.FindControl("ddlRating");
				Ratting.Visible = false;

				e.Row.Cells[2].CssClass = "hiddencol1";
				e.Row.Cells[3].CssClass = "hiddencol1";
			}

			if (Convert.ToString(lsDataKeyValue2).Trim() == "N" || Convert.ToString(lsDataKeyValue).Trim() == "0")
			{
				DropDownList Ratting = (DropDownList)e.Row.FindControl("ddlRating");
				Ratting.Visible = false;
				e.Row.Cells[3].CssClass = "hiddencol1";
				e.Row.Cells[2].CssClass = "hiddencol2";
			}
			if (Convert.ToString(lsDataKeyValue).Trim() == "0")
			{
				e.Row.Cells[2].CssClass = "hiddencol1";
				e.Row.Cells[1].Style["font-size"] = "16px";
				e.Row.Cells[1].Style["color"] = "#000066";
			}

			if (Convert.ToString(lsDataKeyValue).Trim() == "1")
			{
				e.Row.Cells[1].Font.Bold = true;
				e.Row.Cells[1].Style["font-size"] = "13px";
				e.Row.Cells[1].ForeColor = Color.DodgerBlue;
			}
		}
	}

	protected void ddlRecommendation_SelectedIndexChanged(object sender, EventArgs e)
	{
		DropDownList ddl = GRDOverallRating.FooterRow.FindControl("ddlRecommendation") as DropDownList;
		//GRDOverallRating.FooterRow.Cells[0].ForeColor = Color.Red;

		if (ddl.SelectedValue == "No")
		{
			GRDOverallRating.FooterRow.Cells[3].CssClass = "ShowCode";
		}
		else
		{
			GRDOverallRating.FooterRow.Cells[3].CssClass = "ShowHide";
		}

	}

	public void getDataIrSheet(DataTable dt1, DataTable dt2)
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

				//dt2 = view.ToTable(true, "Main_Type_ID", "Heading", "Ishedeing", "SubType_ID", "SubType_Rating");
				for (int j = 0; j < dt2.Rows.Count; j++)
				{
					//var valueFormain2
					if (MainID == Convert.ToInt32(dt2.Rows[j]["Main_Type_ID"]))
					{
						//if (Convert.ToString(dt2.Rows[j]["Heading"]).Trim() != "")
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
			}

			DgvIrSheet.DataSource = dtMerge;
			DgvIrSheet.DataBind();
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}
	private void getIrSheet()
	{
		txtCandidName.Text = txtName.Text;
		txtposotiontitile.Text = lstPositionName.SelectedItem.Text;
		txtExperinceFrom.Text = TxtTotalExperienceYrs.Text;
		dtIrSheetReport = spm.Get_Rec_Recruit_IrSheetDetails("GetIrSheetDetails", Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToInt32(hdCandidate_ID.Value));
		if (dtIrSheetReport.Tables[0].Rows.Count > 0)
		{
			getDataIrSheet(dtIrSheetReport.Tables[0], dtIrSheetReport.Tables[1]);

			if (dtIrSheetReport.Tables[2].Rows.Count > 0)
			{
				GRDOverallRating.DataSource = dtIrSheetReport.Tables[2];
			}
			else
			{
				GRDOverallRating.DataSource = ReturnEmptyDataTable();
			}
			GRDOverallRating.DataBind();
		}
	}
	public DataTable ReturnEmptyDataTable()
	{
		DataTable dtMenu = new DataTable(); //declaringa datatable		
		DataColumn dcMenuName = new DataColumn("Rec_Main_Irsheet_ID", typeof(System.String)); dtMenu.Columns.Add(dcMenuName);
		DataColumn dcMenuName2 = new DataColumn("InterviewRound", typeof(System.String)); dtMenu.Columns.Add(dcMenuName2);
		DataColumn dcMenuName3 = new DataColumn("RatingName", typeof(System.String)); dtMenu.Columns.Add(dcMenuName3);
		DataColumn dcMenuName4 = new DataColumn("Selection_Recommendation", typeof(System.String)); dtMenu.Columns.Add(dcMenuName4);
		DataColumn dcMenuName5 = new DataColumn("Notes", typeof(System.String)); dtMenu.Columns.Add(dcMenuName5);
		DataColumn dcMenuName6 = new DataColumn("Emp_Name", typeof(System.String));
		dtMenu.Columns.Add(dcMenuName6);
		DataRow datatRow = dtMenu.NewRow();
		datatRow["Rec_Main_Irsheet_ID"] = "0";
		dtMenu.Rows.Add(datatRow);//adding row to the datatable
		return dtMenu;
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
			lblmessage.Text = "";

			int IsHeading = 0, Main_Type_ID = 0, SubType_ID = 0;
			string SubType_Rating = "";

			foreach (GridViewRow gvrow in DgvIrSheet.Rows)
			{
				TextBox txtremark = (TextBox)gvrow.FindControl("txtObservationRemark");
				DropDownList ddlRating = (DropDownList)gvrow.FindControl("ddlRating");
				IsHeading = Convert.ToInt32(DgvIrSheet.DataKeys[gvrow.RowIndex].Values[1]);
				SubType_Rating = Convert.ToString(DgvIrSheet.DataKeys[gvrow.RowIndex].Values[2]).Trim();
				//if (IsHeading == 1)//&& SubType_Rating == "Y"
				//{
				//	if (txtremark.Text.Trim() == "")
				//	{
				//		lblmessage.Text = "Please enter Observation Remarks";
				//		return;
				//	}
				//}

				

				if (IsHeading == 1 && SubType_Rating == "Y")
				{
					if (ddlRating.SelectedValue.Trim() == "" || ddlRating.SelectedValue.Trim() == "0")
					{
						lblmessage.Text = "Please select Rating";
						return;
					}
				}

                if (txtremark.Text.Trim() == "")
                {
                    txtremark.Text = ddlRating.SelectedItem.Text.Trim();
                }
            }
			if (txtinterviewrRemark.Text.Trim() == "")
			{
				lblmessage.Text = "Please enter interviewer Remark";
				return;
			}

			//foreach (GridViewRow gvrow in GRDOverallRating.Rows)
			{
				DropDownList ddlRecommendation = GRDOverallRating.FooterRow.FindControl("ddlRecommendation") as DropDownList;
				DropDownList ddlOverallrating = GRDOverallRating.FooterRow.FindControl("ddlOverallrating") as DropDownList;
				TextBox txtNotes = GRDOverallRating.FooterRow.FindControl("txtNotes") as TextBox;
				//IsHeading = Convert.ToInt32(GRDOverallRating.DataKeys[gvrow.RowIndex].Values[1]);								
				if (ddlOverallrating.SelectedValue.Trim() == "" || ddlOverallrating.SelectedValue.Trim() == "0")
				{
					lblmessage.Text = "Please Select Overallrating";
					return;
				}
				if (ddlRecommendation.SelectedValue.Trim() == "" || ddlRecommendation.SelectedValue.Trim() == "0")
				{
					lblmessage.Text = "Please Select Selection Recommendation";
					return;
				}
				if (ddlRecommendation.SelectedValue.Trim() == "No")
				{
					if (txtNotes.Text.Trim() == "")
					{
						lblmessage.Text = "Please enter Notes for Reason";
						return;
					}
				}

			}


			if (lblmessage.Text == "")
			{
				DropDownList ddlRecommendation = GRDOverallRating.FooterRow.FindControl("ddlRecommendation") as DropDownList;
				DropDownList ddlOverallrating = GRDOverallRating.FooterRow.FindControl("ddlOverallrating") as DropDownList;
				TextBox txtNotes = GRDOverallRating.FooterRow.FindControl("txtNotes") as TextBox;
				spm.Insert_Recruit_IrSheetDetails("InsertIrSheetMaster", 1, 1, Convert.ToInt32(HFDCandidateScheduleRound_ID.Value), "", 0, Convert.ToInt32(ddlOverallrating.SelectedValue.Trim()), ddlRecommendation.SelectedValue, txtNotes.Text.Trim(), txtinterviewrRemark.Text.Trim(), Convert.ToInt32(hdCandidate_ID.Value), Convert.ToInt32(hdRecruitment_ReqID.Value), Convert.ToString(Session["Empcode"]).Trim());

				foreach (GridViewRow gvrow in DgvIrSheet.Rows)
				{
					TextBox txtremark = (TextBox)gvrow.FindControl("txtObservationRemark");
					DropDownList ddlRating = (DropDownList)gvrow.FindControl("ddlRating");
					Main_Type_ID = Convert.ToInt32(DgvIrSheet.DataKeys[gvrow.RowIndex].Values[0]);
					SubType_ID = Convert.ToInt32(DgvIrSheet.DataKeys[gvrow.RowIndex].Values[3]);
					IsHeading = Convert.ToInt32(DgvIrSheet.DataKeys[gvrow.RowIndex].Values[1]);
					if (IsHeading == 1)//&& SubType_Rating == "Y"
					{
						spm.Insert_Recruit_IrSheetDetails("InsertIrSheetDetails", Main_Type_ID, SubType_ID, Convert.ToInt32(HFDCandidateScheduleRound_ID.Value), txtremark.Text.Trim(), Convert.ToInt32(ddlRating.SelectedValue.Trim()), 0, "", "", "", 1, 1, Convert.ToString(Session["Empcode"]).Trim());
					}
				}
				getIrSheetSummary();
				IRsheetDetails.Visible = false;
				trvl_accmo_btn.Enabled = false;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message);

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

	private void getIrSheetSummary()
	{

		txtRec_No.Text = txtReqNumber.Text;
		txtCandidateName.Text = txtCandidName.Text;
		txtPositionInterviwed.Text = lstSkillset.SelectedItem.Text;
		txttotalExperince.Text = TxtTotalExperienceYrs.Text;
		txtRelevantExp.Text = TxtRelevantExpYrs.Text;
		txtpostionTitle.Text = lstPositionName.SelectedItem.Text;
		//this.DgvIrSheetSummary.Columns.RemoveAt(this.DgvIrSheetSummary.Columns.Count - 1);
		DgvIrSheetSummary.DataSource = null;
		DgvIrSheetSummary.DataBind();

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

	public void GetInterviewType()
	{
		DataSet dsReqNo = new DataSet();
		try
		{
			SqlParameter[] spars = new SqlParameter[2];
			spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
			spars[0].Value = "sp_Req_InterviewType";
			//spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
			//spars[1].Value = Convert.ToString(Session["Empcode"]);
			dsReqNo = spm.getDatasetList(spars, "SP_GETREQUISTIONLIST_DETAILS");
			if (dsReqNo.Tables[0].Rows.Count > 0)
			{
				lstInterviewType.DataSource = dsReqNo.Tables[0];
				lstInterviewType.DataTextField = "InterviewType";
				lstInterviewType.DataValueField = "InterviewTypeID";
				lstInterviewType.DataBind();
				lstInterviewType.Items.Insert(0, new ListItem("Select Interview Type", "0"));
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.ToString());
		}
	}

	protected void lstInterviewType_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (lstInterviewType.SelectedValue =="2" && DDLInterviewStatus.SelectedValue == "1")
		{
			SPPhoto.Visible = true;
		}
		else
		{
			SPPhoto.Visible = false;
		}
	}
}