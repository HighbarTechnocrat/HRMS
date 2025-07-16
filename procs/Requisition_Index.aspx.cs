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
public partial class Requisition_Index : System.Web.UI.Page
{
	SqlConnection source;
	public SqlDataAdapter sqladp;
	public string userid;
	SP_Methods spm = new SP_Methods();
	public DataTable dtEmp, dtRectruter;
	public int Leaveid;
	public int leavetype;
	public string filename = "", approveremailaddress;
	DateTime holidaydate = new DateTime();
	DataSet dsRecruterInox, dsInterviewerInox, DSInterviewsh;
	protected void lnkhome_Click(object sender, EventArgs e)
	{
		Response.Redirect(ReturnUrl("sitepathmain") + "default");
	}
	public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
			{
				Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
			}
			hflEmpCode.Value = Convert.ToString(Session["Empcode"]);
			//  lblmsg.Text =Convert.ToString(Session["Empcode"]); 
			if (Page.User.Identity.IsAuthenticated == false)
			{
				//Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx?ReturnUrl=" + ReturnUrl("sitepathmain") + "procs/Leaves");
			}
			else
			{
				Page.SmartNavigation = true;
				if (!Page.IsPostBack)
				{
                    GetIsWorking();

                    CheckExtraApprovalEmp();
					GetRequisitionPendingCount();
					GetReq_Offer_Approval_PendingCount();
					//Harshad
					getRecruEnterviewertypeLogin();
					PopulateEmployeeData();
					CheckEmployeeHOD();
					CheckScreenerEmp();
					CheckInterviewerSchedularEmp();
					CheckInterviewrEmp();
					InboxInterviewSchedulePendingRecord();
					CheckProsepectCustEmp();
					GetReq_CTC_Approval_PendingCount();
					//26_12_2022
					IsCandidateApprover();
					CheckIsReferCandidateHistory();
					CheckIsDetail_Abstraction_Report();
                    CheckIsDetail_ViewRecruitmentRequests_Closed();

                    this.Title = creativeconfiguration.SiteName + ": Edit Profile ";
				}
			}
		}
		catch (Exception ex)
		{
			ErrorLog.WriteError(ex.ToString());
		}
	}

    public void CheckIsDetail_ViewRecruitmentRequests_Closed()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_ViewRecruitmentRequests_Closed";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "RecruitmentStatus_Reopen";
            getdtDetails = spm.getTeamReportAllDDL(spars, "sp_Ref_SearchCandidateReferral");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Link_Btn_Reopen.Visible = true;
                }
                else
                {
                    Link_Btn_Reopen.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            // return false;
        }
    }

    public void CheckIsDetail_Abstraction_Report()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_DetailAbstractCandidateReport";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "Rec_DetailAbstractionCandidateReport";
            getdtDetails = spm.getTeamReportAllDDL(spars, "sp_Ref_SearchCandidateReferral");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_DetailAbstractionReport.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            // return false;
        }
    }



    public void GetIsWorking()
    {
        try
        {
            DataSet DS = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_EmployeementTypeCheck";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);

            DS = spm.getDatasetList(spars, "Usp_getRecruitmentEmp_Details_All");
            if (DS.Tables[0].Rows[0]["EMPLOYMENT_TYPE"].ToString() == "7")
            {
                lnk_Employee_Referral.Visible = true;
                lnk_CreateQuestio.Visible = false;
                lnk_Req_Questio.Visible = false;
                lnk_CreateJDBank.Visible = false;
                lnk_MyJDBank.Visible = false;
                lnk_Vendor_Create.Visible = false;
                lnk_Vendor_Details.Visible = false;
                lnk_JobSite_Create.Visible = false;
                lnk_JobSite_Details.Visible = false;
                LINK_ScreenerMapping.Visible = false;
                Link_SkillSet.Visible = false;
                spprose.Visible = false;

                spprose.Visible = false;
                lnk_Prosepect_Cust_Details.Visible = false;
                lnk_Req_Requisiti_Create.Visible = false;
                lnk_Req_Requisiti_Details.Visible = false;
                span_App_head.Visible = false;
                lnk_Req_RequisIndex.Visible = false;
                lnk_Req_RequisApproval.Visible = false;
                lnk_Req_RequisApprovalRecruiterChange.Visible = false;
                lnk_Req_RequisApprovalInterviewerSchedularChange.Visible = false;
                CTCspan.Visible = false;

                lnk_CTC_Inbox.Visible = false;
                lnk_CTC_View.Visible = false;
                SpanRecruiterBoxs.Visible = false;
                lnk_mng_recInbox.Visible = false;
                lnk_mng_ViewRecRequest.Visible = false;
                lnk_RecCreateEditCandidate.Visible = false;
                lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter.Visible = false;
                trScreener.Visible = false;

                lnk_Screener_Inbox.Visible = false;
                lnk_Screener_View_Recruitment.Visible = false;
                trInterSchedular.Visible = false;
                lnk_mng_recInterviewerShortlisted.Visible = false;
                Lnk_mng_recRescheduleInterview.Visible = false;
                Lnk_RecRescheduledInterviewList.Visible = false;
                Lnk_RecSchedule_InterviewList.Visible = false;
                Link_Interviewermapping.Visible = false;
                SpanInterviewerBoxs.Visible = false;
                TrInterviewer.Visible = false;
                span_Offer_APP.Visible = false;
                span_Offer_APP.Visible = false;
                lnk_Rec_Offer_App_Index.Visible = false;
                lnk_Rec_Offer_Apprval_List.Visible = false;
                span_Report_head.Visible = false;
                lnk_Req_Report_Dept.Visible = false;
                lnk_Detail_Report.Visible = false;
                lnk_Req_Report_Recruiter.Visible = false;
                lnk_Req_detailapprovedRequest.Visible = false;
                lnk_Req_detailViewRequest.Visible = false;
                lnk_Req_Report_SkillSet.Visible = false;
                Lnk_Req_StatusofInterview_L1.Visible = false;
                Lnk_Req_StatusofInterview_L2.Visible = false;
                Lnk_Req_RequisitionStatus.Visible = false;
                lnk_Detail_Summary.Visible = false;
                lnk_candidatedetails.Visible = false;

            }
            else
            {

            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void CheckExtraApprovalEmp()
	{
		try
		{
			DataTable dtextraApp = new DataTable();
			dtextraApp = spm.Get_RecruiterChange_ExtraApprovalEmp(Convert.ToString(Session["Empcode"]).Trim());
			if (dtextraApp.Rows.Count > 0)
			{
				lnk_Req_RequisApprovalRecruiterChange.Visible = true;
				lnk_Req_RequisApprovalInterviewerSchedularChange.Visible = true;
				//lnk_Req_Report_Recruiter.Visible = true;
                Lnk_Req_StatusofInterview_L1.Visible = true;
                Lnk_Req_StatusofInterview_L2.Visible = true;
                Lnk_Req_RequisitionStatus.Visible = true;
                lnk_Req_detailapprovedRequest.Visible = true;
                span_Report_head.Visible = true;
				HODViewShow.Visible = false;
				//lnk_Req_Report_Dept.Visible = true;
               lnk_candidatedetails.Visible = true;
			  lnk_Detail_Summary.Visible = true;

				span_Offer_APP.Visible = true;
				lnk_Rec_Offer_App_Index.Visible = true;
				lnk_Rec_Offer_Apprval_List.Visible = true;

			}
			else
			{
				lnk_Req_RequisApprovalRecruiterChange.Visible = false;
				lnk_Req_RequisApprovalInterviewerSchedularChange.Visible = false;

                if (Convert.ToString(Session["Empcode"]) == "00631257")
                {
                    lnk_Req_RequisApprovalRecruiterChange.Visible = true;
                }

				//span_Report_head.Visible = false;
				//lnk_Req_Report_Recruiter.Visible = false;
			}

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();

			throw;
		}
	}
	public void CheckProsepectCustEmp()
	{
		//DataTable dtProsepect = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_EmployeeInterview");
		string ResultProsepect = Convert.ToString(Session["Empcode"]).Trim();
		if (ResultProsepect== "00630542" || ResultProsepect == "00631289")
		{
			lnk_Prosepect_Cust_Details.Visible = true;
			spprose.Visible = true;
		}
	}
	public void CheckInterviewrEmp()
	{
		DataTable dtInterviewr = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_EmployeeInterview");
		if (dtInterviewr.Rows.Count > 0)
		{
			SpanInterviewerBoxs.Visible = true;
			lnk_mng_InterviewrInbox.Visible = true;
			//lnk_mng_InterviewerShortlisting.Visible = true;
			lnk_mng_ViewRecRequestInterviewer.Visible = true;
		}
		else
		{
			SpanInterviewerBoxs.Visible = false;			
		}
	}
	public void CheckEmployeeHOD()
	{
		try
		{

			DataTable dtHOD = spm.Get_Requisition_EmployeeHOD(Convert.ToString(Session["Empcode"]).Trim());
			if (dtHOD.Rows.Count > 0)
			{
				//Report for HOD Only
				span_Report_head.Visible = true;
				lnk_Req_Report_Dept.Visible = true;
                lnk_Detail_Report.Visible = true;

                //Report for HOD Offer approval Only
                span_Offer_APP.Visible = true;
				lnk_Rec_Offer_App_Index.Visible = true;
				lnk_Rec_Offer_Apprval_List.Visible = true;
				

				//Report for HOD Offer approval Only
				span_Offer_APP.Visible = true;
				lnk_Rec_Offer_App_Index.Visible = true;
				lnk_Rec_Offer_Apprval_List.Visible = true;
				lnk_Req_detailViewRequest.Visible = true;
				Lnk_Req_RequisitionStatus.Visible = true;
				//lnk_Req_Report_SkillSet.Visible = true;
				lnk_Detail_Summary.Visible = true;
				//CTC Approval 
				CTCspan.Visible = true;
				lnk_CTC_Inbox.Visible = true;
				lnk_CTC_View.Visible = true;
				//Requisition for HOD view Show Only
				if (Convert.ToString(Session["Empcode"]).Trim() == "00002726" || Convert.ToString(Session["Empcode"]).Trim() == "00002082" || Convert.ToString(Session["Empcode"]).Trim() == "00002878")
				{
					lnk_Req_detailapprovedRequest.Visible = true;
					lnk_Req_detailViewRequest.Visible = false;
					lnk_Detail_Summary.Visible = true;

				}
			}

			//Mr. Aniket  Anil  Bhagwat. Details Report Show only 
			if (Convert.ToString(Session["Empcode"]).Trim() == "00630098")
			{
				lnk_Detail_Report.Visible = true;
		                Lnk_Req_RequisitionStatus.Visible = true;
				DeptHOD.Visible = false;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}


			
	public void CheckScreenerEmp()
	{
		DataTable dtScreener = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_EmployeeScreener");
		if (dtScreener.Rows.Count > 0)
		{
			trScreener.Visible = true;
			lnk_Screener_Inbox.Visible = true;
			lnk_Screener_View_Recruitment.Visible = true;		
		}
		
	}
	public void CheckInterviewerSchedularEmp()
	{
		DataTable dtSchedular = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_EmployeeInterviewerSchedular");
		if (dtSchedular.Rows.Count > 0)
		{
			trInterSchedular.Visible = true;
			lnk_mng_recInterviewerShortlisted.Visible = true;
			Lnk_mng_recRescheduleInterview.Visible = true;
			Lnk_RecRescheduledInterviewList.Visible = true;
			Lnk_RecSchedule_InterviewList.Visible = true;
            Link_Interviewermapping.Visible = true;
        }

	}
	protected void InboxRecruiterPendingRecord()
	{
		try
		{

			dsRecruterInox = spm.getRecruterInoxList(Convert.ToString(Session["Empcode"]).Trim(), "InRec");
			//lblheading.Text = "Recruiter - OneHR";
			if (dsRecruterInox.Tables[0].Rows.Count > 0)
			{
				lnk_mng_recInbox.Text = "Inbox(" + dsRecruterInox.Tables[0].Rows.Count + ")";
			}
			//if (dsRecruterInox.Tables[1].Rows.Count > 0)
			//{				
			//	lnk_mng_recInterviewerShortlisted.Text = "Schedule Interview(" + dsRecruterInox.Tables[1].Rows.Count + ")";				
			//}
			//if (dsRecruterInox.Tables[2].Rows.Count > 0)
			//{
			//	Lnk_mng_recRescheduleInterview.Text = "Reschedule Interview(" + dsRecruterInox.Tables[2].Rows.Count + ")";
			//}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}

	protected void InboxInterviewSchedulePendingRecord()
	{
		try
		{
			
			DSInterviewsh = spm.getRecruterInoxList(Convert.ToString(Session["Empcode"]).Trim(), "InterviewSchedule");			
			if (DSInterviewsh.Tables[0].Rows.Count > 0)
			{
				lnk_mng_recInterviewerShortlisted.Text = "Schedule Interview(" + DSInterviewsh.Tables[0].Rows.Count + ")";
			}
			if (DSInterviewsh.Tables[1].Rows.Count > 0)
			{
				Lnk_mng_recRescheduleInterview.Text = "Reschedule Interview(" + DSInterviewsh.Tables[1].Rows.Count + ")";
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}

	}
	protected void InboxInterviewerPendingRecord()
	{
		try
		{
			// lblheading.Text = "Interviewer - OneHR";
			dsInterviewerInox = spm.getInterviewerInoxList(Convert.ToString(Session["Empcode"]).Trim(), "InPInter");
			if (dsInterviewerInox.Tables[0].Rows.Count > 0)
			{
				lnk_Screener_Inbox.Text = "Inbox Shortlisting(" + dsInterviewerInox.Tables[0].Rows.Count + ")";

			}
			if (dsInterviewerInox.Tables[2].Rows.Count > 0)
			{
				lnk_mng_InterviewrInbox.Text = "Inbox Interview(" + dsInterviewerInox.Tables[2].Rows.Count + ")";

			}
			//lnk_mng_InterviewerShortlisting.Visible = true;
			//lnk_mng_ViewRecRequestInterviewer.Visible = true;
			//lnk_mng_InterviewrInbox.Visible = true;
			lnk_mng_recInbox.Visible = false;
			lnk_mng_recInterviewerShortlisted.Visible = false;
			lnk_RecCreateEditCandidate.Visible = false;
			lnk_mng_ViewRecRequest.Visible = false;
			Lnk_mng_recRescheduleInterview.Visible = false;

		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
		}
	}

	private void getRecruEnterviewertypeLogin()
	{
		DataSet DS = new DataSet();
		DS = spm.Get_RecruEnterviewertypeLogin(Convert.ToString(Session["Empcode"]));
		if (DS.Tables[0].Rows.Count > 0)
		{

			InboxRecruiterPendingRecord();
			SpanRecruiterBoxs.Visible = true;
			SpanInterviewerBoxs.Visible = false;
			//Add 14-07-21 harshad
			dsInterviewerInox = spm.getInterviewerInoxList(Convert.ToString(Session["Empcode"]).Trim(), "InPInter");
			if (dsInterviewerInox.Tables[0].Rows.Count > 0)
			{
				lnk_Screener_Inbox.Text = "Inbox Shortlisting(" + dsInterviewerInox.Tables[0].Rows.Count + ")";
			}
			//TypeCheckRecruiterOREnterviewerSchedular();
			if (DS.Tables[1].Rows.Count > 0)
			{
				InboxInterviewerPendingRecord();
				SpanInterviewerBoxs.Visible = true;
				SpanRecruiterBoxs.Visible = true;
				InboxRecruiterPendingRecord();
				lnk_mng_recInbox.Visible = true;
				//lnk_mng_recInterviewerShortlisted.Visible = true;
				lnk_RecCreateEditCandidate.Visible = true;
				lnk_mng_ViewRecRequest.Visible = true;
				//Lnk_mng_recRescheduleInterview.Visible = true;
			}
			if (DS.Tables[2].Rows.Count > 0)
			{
				InboxInterviewerPendingRecord();
				SpanInterviewerBoxs.Visible = true;
				SpanRecruiterBoxs.Visible = true;
				InboxRecruiterPendingRecord();
				lnk_mng_recInbox.Visible = true;
				//lnk_mng_recInterviewerShortlisted.Visible = true;
				lnk_RecCreateEditCandidate.Visible = true;
				lnk_mng_ViewRecRequest.Visible = true;
				Lnk_mng_recRescheduleInterview.Visible = true;
			}
		}
		else
		{
			InboxInterviewerPendingRecord();
			SpanInterviewerBoxs.Visible = true;
			SpanRecruiterBoxs.Visible = false;
		}

	}

	public void PopulateEmployeeData()
	{
		try
		{
			dtEmp = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_EmployeeBand");
			if (dtEmp.Rows.Count > 0)
			{
				hdnBand.Value = (string)dtEmp.Rows[0]["grade"].ToString().Trim();
				//lnk_RecCreateEditCandidate.Visible = false;
				//lnk_mng_ViewRecRequest.Visible = false;
				lnk_Req_Requisiti_Create.Visible = true;
				lnk_Req_Requisiti_Details.Visible = true;
				span_App_head.Visible = true;
				lnk_Req_RequisIndex.Visible = true;
				lnk_Req_RequisApproval.Visible = true;

                // This Requistioner status show
                span_Report_head.Visible = true;
                lnk_Req_detailViewRequest.Visible = true;

               
                //lnk_Req_Report_Dept.Visible = true;
                //lnk_Req_Report_Recruiter.Visible = true;

                //span_Offer_APP.Visible = true;
                //lnk_Rec_Offer_App_Index.Visible = true;
                //lnk_Rec_Offer_Apprval_List.Visible = true;
            }
			dtRectruter = spm.Get_Requisition_EmployeeBand(Convert.ToString(Session["Empcode"]).Trim(), "Get_RecruiterTeam");
			if (dtRectruter.Rows.Count > 0)
			{
				// hdnBand.Value = (string)dtRectruter.Rows[0]["grade"].ToString().Trim();
				lnk_CreateQuestio.Visible = true;
				lnk_Req_Questio.Visible = true;
				lnk_CreateJDBank.Visible = true;
				lnk_MyJDBank.Visible = true;
				//span_App_head.Visible = false;
				//lnk_Req_RequisIndex.Visible = false;
				//lnk_Req_RequisApproval.Visible = false;

				lnk_RecCreateEditCandidate.Visible = true;
				lnk_mng_ViewRecRequest.Visible = true;
				lnk_Vendor_Create.Visible = true;
				lnk_Vendor_Details.Visible = true;
				lnk_JobSite_Create.Visible = true;
                LINK_ScreenerMapping.Visible = true;
                Link_SkillSet.Visible = true;
                lnk_JobSite_Details.Visible = true;
				

				//Report for recruiter Show
				span_Report_head.Visible = true;
				lnk_Req_Report_Dept.Visible = true;
				lnk_Detail_Report.Visible = true;
				//lnk_Req_Report_Recruiter.Visible = true;
                Lnk_Req_StatusofInterview_L1.Visible = true;
                Lnk_Req_StatusofInterview_L2.Visible = true;
                Lnk_Req_RequisitionStatus.Visible = true;
                lnk_Req_detailapprovedRequest.Visible = true;
				lnk_Req_detailViewRequest.Visible = false;
				//lnk_Req_Report_SkillSet.Visible = true;
				HODViewShow.Visible = false;
				//lnk_Req_Requisiti_Create.Visible = true;
				//lnk_Req_Requisiti_Details.Visible = true;
               lnk_candidatedetails.Visible = true;
			   lnk_Detail_Summary.Visible = true;
			}
		}
		catch (Exception ex)
		{
			Response.Write(ex.Message.ToString());
			Response.End();
			throw;
		}
	}
	public void TypeCheckRecruiterOREnterviewerSchedular()
	{
		DataSet DSInterviewerSchedulars = new DataSet();
		DSInterviewerSchedulars = spm.GetInterviewerSchedularsEmpCode();
		string str = Convert.ToString(Session["Empcode"]).Trim();


		for (int i = 0; i < DSInterviewerSchedulars.Tables[0].Rows.Count; i++)
		{
			if (DSInterviewerSchedulars.Tables[0].Rows[i]["InterviewerSchedularEmpCode"].ToString().Trim() == str)
			{
				//lblheadingInterSchedular.Visible = true;
				lblheadingRecruiter.Visible = false;
				lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter.Visible = false;
				// lnk_mng_recInbox.Visible = false;
				lnk_RecCreateEditCandidate.Visible = false;				
				Lnk_RecRescheduledInterviewList.Visible = true;
                Link_Interviewermapping.Visible = true;

                break;
			}
			else
			{

				lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter.Visible = true;
				lnk_RecCreateEditCandidate.Visible = true;
				//   lnk_mng_recInbox.Visible = true;
				//lblheadingInterSchedular.Visible = false;
				lblheadingRecruiter.Visible = true;				
				Lnk_RecRescheduledInterviewList.Visible = false;
                Link_Interviewermapping.Visible = false;

            }
		}
	}

	protected void lnk_Requisition_Click(object sender, EventArgs e)
	{
		if (Convert.ToString(Session["Empcode"]).Trim() != "")
		{
			Response.Redirect("~/procs/Create_Recru_Requisition.aspx");
		}
		else
		{
			return;
		}
	}

	protected void lnk_Recru_Request_Click(object sender, EventArgs e)
	{
		if (Convert.ToString(Session["Empcode"]).Trim() != "")
		{
			Response.Redirect("~/procs/My_Recru_Request_Index.aspx");
		}
		else
		{
			return;
		}

	}

	protected void lnk_leaverequest_Click(object sender, EventArgs e)
	{
		if (Convert.ToString(Session["Empcode"]).Trim() != "")
		{
			Response.Redirect("~/procs/Req_Questionnaire_Create.aspx");
		}
		else
		{
			return;
		}
	}

	protected void GetRequisitionPendingCount()
	{
		int RequisitionCount = 0;
		RequisitionCount = spm.getRequisitionPending_InboxList_Count(Convert.ToString(hflEmpCode.Value).Trim());
		lnk_Req_RequisIndex.Text = "Inbox:(" + RequisitionCount.ToString() + ")";
	}

	protected void GetReq_Offer_Approval_PendingCount()
	{
		int OfferCount = 0;
		OfferCount = spm.getReq_Offer_Approval_Pending_InboxList_Count(Convert.ToString(hflEmpCode.Value).Trim());
		lnk_Rec_Offer_App_Index.Text = "Inbox:(" + OfferCount.ToString() + ")";
	}
	protected void GetReq_CTC_Approval_PendingCount()
	{
		DataTable dt = new DataTable();
		dt = spm.CTC_Exception_Approval("Select_CTC_Pending_Search", 0, 0, Convert.ToString(hflEmpCode.Value).Trim(), 0, 0,0);
		if (dt.Rows.Count > 0)
		{
			lnk_CTC_Inbox.Text = "Inbox(" + dt.Rows.Count + ")";
		}
		
	}
	
protected void lnk_mng_recInbox_Click(object sender, EventArgs e)
	{

		if (lnk_mng_recInbox.Text != "Inbox(0)")
		{
			Response.Redirect("~/procs/Rec_RecruiterInbox.aspx?type=InRec");
		}
	}

	protected void lnk_mng_InterviewrInbox_Click(object sender, EventArgs e)
	{
		if (lnk_mng_InterviewrInbox.Text != "Inbox Interview(0)")
		{
			Response.Redirect("~/procs/Rec_InterviewerInbox.aspx?type=InShPInter");
		}
	}

	protected void lnk_mng_InterviewerShortlisting_Click(object sender, EventArgs e)
	{
		if (lnk_Screener_Inbox.Text != "Inbox Shortlisting(0)")
		{
			Response.Redirect("~/procs/Rec_InterviewerInbox.aspx?type=InPInter");
		}
	}

	protected void lnk_mng_recInterviewerShortlisted_Click(object sender, EventArgs e)
	{
		if (lnk_mng_recInterviewerShortlisted.Text != "Schedule Interview(0)")
		{
			Response.Redirect("~/procs/Rec_RecruiterInbox.aspx?type=RECISL");
		}
	}

	protected void Lnk_mng_recRescheduleInterview_Click(object sender, EventArgs e)
	{
		if (Lnk_mng_recRescheduleInterview.Text != "Reschedule Interview(0)")
		{
			Response.Redirect("~/procs/Rec_RecruiterInbox.aspx?type=RECIRescedule");
		}
	}
	//
	private void IsCandidateApprover()
	{
		try
		{
			var ds = spm.getRecruitmentInboxByCandiate("getRecruitmentInbox", Convert.ToString(Session["Empcode"]).Trim());
		    if(ds !=null)
			{
				if(ds.Rows.Count>0)
				{
					lnk_Req_Candidate_Details_Approver_Inbox.Visible = true;
					var getCount = ds.Rows.Count;
					lnk_Req_Candidate_Details_Approver_Inbox.Text= "Candidate Approver Inbox ("+ Convert.ToString(getCount) + ")";
				}
				else
				{
					lnk_Req_Candidate_Details_Approver_Inbox.Visible = false;
				}
			}
			else
			{
				lnk_Req_Candidate_Details_Approver_Inbox.Visible = false;
			}

		}
		catch (Exception ex)
		{

		}
	}

    public void CheckIsReferCandidateHistory()
    {
        var getdtDetails = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@QueueType", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_RefCandidate";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(hflEmpCode.Value);
            spars[2] = new SqlParameter("@ReportName", SqlDbType.VarChar);
            spars[2].Value = "Rec_ReferCandidateHistory";
            getdtDetails = spm.getTeamReportAllDDL(spars, "sp_Ref_SearchCandidateReferral");
            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    Lnk_ReferCandidateHistory.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            // return false;
        }
    }
}