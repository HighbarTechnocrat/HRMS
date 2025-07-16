
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class control_header : System.Web.UI.UserControl
{
    public Int32 PendingODApplication = 0;
    public Int32 Acceptance_Cnt = 0, Acceptance_APP_Cnt = 0;
    public Int32 Resignation_Count = 0, TeamExit_Count = 0, Clearance_Count = 0;
    string username = string.Empty;
    string m_flag = string.Empty;
    string online = string.Empty;
    public static string skey = "";
    public Int32 Leave_Cnt = 0;
    public Int32 Mobile_Cnt = 0;
    public Int32 Fuel_Cnt = 0;
    public Int32 Payment_Cnt = 0;
    public Int32 Trvl_Exp_Cnt = 0;
    public Int32 Tot_Cnt = 0;
    public Int32 Service_Request_Count = 0;
    public Int32 Customer_FIRST_Count = 0;
    public Int32 Attedance_Reg_Count = 0;
    public Int32 Timesheet_Count = 0;
    public Int32 IT_Asset_Count = 0;
    public Int32 Recruit_Req_Cnt = 0, Recruiter_Cnt = 0, Screener_Cnt = 0, ScheduleInt_cnt = 0, RescheduleInt_cnt = 0, Interview_cnt = 0, OfferApproval_cnt = 0;
    public Int32 TaskPending = 0, TaskCloseRequest = 0, TaskDueDateChange = 0;
    public Int32 Cust_Escalation_Count = 0;
    public Int32 Cust_Pending_Confir_Count = 0;
    public Int32 EmpModerator_Count = 0;
    public Int32 Pending_KRA_Cnt = 0;
    public Int32 ExceptionAPP = 0;
    public Int32 Salary_Status_Count = 0;
    public Int32 Payment_App_Count = 0, Payment_Corr_Count = 0, Payment_Partial_Count = 0, Invoice_App_Count = 0, Batch_App_Count = 0,POWO_APP_Count=0,Payreq_APP_Count = 0,ReviewDelayedTasks_Count = 0;
    public Int32 CandidateRequestSubmited_Count = 0, PendingCVUpdate = 0, PendingCVReviewInboxCount = 0;
    public Int32 Retention_M_Cnt = 0,ETR_Count=0;
    public Int32 Adv_Pay_Cnt = 0, Appr_Performance_ReviewCnt = 0, Appr_Performance_RecommendationCnt = 0;
    public Int32 ABAPObjectCompletion_Count = 0,EmployeeMediclaim_Data_Count = 0;

    public Int32 CustomerServicePendingCnt = 0;
    public Int32 Custs_ServiceRequest_Count = 0;
    public Int32 Custs_ServiceRequest_Count_CS = 0;
    public Int32 Custs_ServiceRequest_PendingCount_CS = 0;
    public int iKRANotAccept_Cnt = 0;
    public int iUpdatePhotoCnt = 0;


    SP_Methods spm = new SP_Methods();
    public string ReturnUrl(object path) { string url = ""; url = UrlRewritingVM.ChangeURL(path.ToString()); return url; }
    protected void Page_Load(object sender, EventArgs e)
    {

        //if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        //{
        //    Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        //}


        if (Convert.ToString(Session["Empcode"]).Trim() == "")
            Response.Redirect(ReturnUrl("sitepathmain") + "login.aspx");


        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            Response.Redirect(ReturnUrl("sitepathmain") + "sessionend.aspx");
        }


        if (Page.User.Identity.IsAuthenticated)
        {
            //SAGAR ADDED this GridLines HIDING username FROM control_header 23OCT2017
            lblname.Visible = false;
            //bool flagleader = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Location Leader");
            //bool flagadmin = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Administrator");
            //bool flagsadmin = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Super Administrator");
            //bool flagsuperadmin = Roles.IsUserInRole(Page.User.Identity.Name.ToString().Trim(), "Super Admin");

            //if (flagleader == true || flagadmin == true || flagsadmin == true || flagsuperadmin == true)
            //{
            //    admin.Visible = true;
            //}
            //else
            //{
            //    admin.Visible = false;
            //}
            if (!IsPostBack)
            {
                login();
                loadorder();
                //Comment by Sanjay on 14.01.2025 Coloadgrid();
                //Comment by Sanjay on 14.01.2025 loadmsg();


               
                Leave_Cnt = 0;
                
                string emp_code = "";
                string popup = "";
                if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
                {
                    btnShowMsg.Text = "Notifications (0)";
                }
                else
                {

                   
                    emp_code = Convert.ToString(Session["Empcode"]).Trim();
                    int icnt = 0;
                    icnt= icnt +  getEmployeeNotification(emp_code);

                    icnt = icnt +  getABAPObjectPlanStageWiseCount(emp_code);

                    /* LeaveCount = spm.GetLeaveInboxCount(emp_code);
                     Leave_Cnt = Leave_Cnt + LeaveCount;
                     get_Pending_LeaveReqstList_cnt(emp_code);
                     if (Leave_Cnt > 0)
                     {
                         popup = popup + Environment.NewLine + "(" + Convert.ToString(Leave_Cnt) + ") Leave approvals pending in Inbox" + "\n";
                         aL.InnerText = "Pending Leave Approvals (" + Convert.ToString(Leave_Cnt) + ")";
                         aL.Title = "Pending Leave Approvals (" + Convert.ToString(Leave_Cnt) + ")";
                         lilblMsg.Visible = true;
                     }
                     else
                     {
                         lilblMsg.Visible = false;
                     }
                     */

                    /*  Mobile_Approver_Count(emp_code);
                      check_COS_ACC("RCOS", emp_code);
                      check_COS_ACC("RACC", emp_code);
                      check_COS_ACC("RCFO", emp_code);

                      if (Mobile_Cnt > 0)
                      {
                          popup = popup + Environment.NewLine + "(" + Convert.ToString(Mobile_Cnt) + ") Mobile approvals pending in Inbox" + "\n";
                          aM.InnerText = "Pending Mobile Approvals (" + Convert.ToString(Mobile_Cnt) + ")";
                          aM.Title = "Pending Mobile Approvals (" + Convert.ToString(Mobile_Cnt) + ")";
                          lilblMsg_Mob.Visible = true;
                      }
                      else
                      {
                          lilblMsg_Mob.Visible = false;
                      }
                      */

                    /*Fuel_Approver_Count(emp_code);
                    check_COS_ACC_Fuel("RCOS", emp_code);
                    check_COS_ACC_Fuel("RACC", emp_code);
                    check_COS_ACC_Fuel("RCFO", emp_code);

                    if (Fuel_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Fuel_Cnt) + ") Fuel approvals pending in Inbox" + "\n";
                        aF.InnerText = "Pending Fuel Approvals (" + Convert.ToString(Fuel_Cnt) + ")";
                        aF.Title = "Pending Fuel Approvals (" + Convert.ToString(Fuel_Cnt) + ")";
                        lilblMsg_Fuel.Visible = true;
                    }
                    else
                    {
                        lilblMsg_Fuel.Visible = false;
                    }
                    */

                    /*  Payment_Approver_Count(emp_code);
                      check_COS_ACC_Payment("RCOS", emp_code);
                      check_COS_ACC_Payment("RACC", emp_code);
                      check_COS_ACC_Payment("RCFO", emp_code);

                      if (Payment_Cnt > 0)
                      {
                          popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Cnt) + ") Payment approvals pending in Inbox";
                          aP.InnerText = "Pending Voucher Approvals (" + Convert.ToString(Payment_Cnt) + ")";
                          aP.Title = "Pending Voucher Approvals (" + Convert.ToString(Payment_Cnt) + ")";
                          lilblMsg_Pay.Visible = true;
                          //lblMsg_Pay.Text = "(" + Convert.ToString(Payment_Cnt) + ") Payment Voucher approvals pending in Inbox";
                      }
                      else
                      {
                          lilblMsg_Pay.Visible = false;
                      }*/

                    /*getTravel_Expenses_PendingList_cnt_Approver(emp_code);
                    checkTD_COS_ACC_Trvl("TD", emp_code);
                    checkTD_COS_ACC_Trvl("COS", emp_code);
                    checkTD_COS_ACC_Trvl("ACC", emp_code);

                    if (Trvl_Exp_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Trvl_Exp_Cnt) + ") Travel Expense approvals pending in Inbox";
                        aE.InnerText = "Travel expense Approvals (" + Convert.ToString(Trvl_Exp_Cnt) + ")";
                        aE.Title = "Travel expense Approvals (" + Convert.ToString(Trvl_Exp_Cnt) + ")";
                        lilblMsg_Travel.Visible = true;
                        //lblMsg_Pay.Text = "(" + Convert.ToString(Payment_Cnt) + ") Payment Voucher approvals pending in Inbox";
                    }
                    else
                    {
                        lilblMsg_Travel.Visible = false;
                    }*/


                    //Add by Vidhyadhar 04-02-2021 EmployeeFIRST Request Count

                    //Tot_Cnt = Leave_Cnt + Mobile_Cnt + Fuel_Cnt + Payment_Cnt + Trvl_Exp_Cnt;
                    //if (Tot_Cnt > 0)
                    //{
                    //    btnShowMsg.Text = "Notifications (" + Tot_Cnt + ")";
                    //    //lblMsg.Text = popup;
                    //}
                    //else
                    //{
                    //    btnShowMsg.Text = "Notifications (0)";
                    //    //lblMsg.Text = popup;
                    //}
                    /*GetServiceRequestCount(emp_code);
                    if (Service_Request_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Service_Request_Count) + ") EmployeeFIRST Request pending in Inbox" + "\n";
                        aEF.InnerText = "Pending EmployeeFIRST Request (" + Convert.ToString(Service_Request_Count) + ")";
                        aEF.Title = "Pending EmployeeFIRST Request (" + Convert.ToString(Service_Request_Count) + ")";
                        lilblServiceRequest.Visible = true;
                    }
                    else
                    {
                        lilblServiceRequest.Visible = false;
                    }*/

                    //Customer FIRST
                    GetCustomerFIRSTCount(emp_code);
                    if (Customer_FIRST_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Customer_FIRST_Count) + ") Customer Feedback Response in Inbox" + "\n";
                        aEF1.InnerText = "Pending Customer Feedback Response (" + Convert.ToString(Customer_FIRST_Count) + ")";
                        aEF1.Title = "Pending Customer Feedback Response (" + Convert.ToString(Customer_FIRST_Count) + ")";
                        lilblCustomerFIRST.Visible = true;
                    }
                    else
                    {
                        lilblCustomerFIRST.Visible = false;
                    }

                    //Attedance And Timesheet

                    GetAttednceRegCount(emp_code);
                    if (Attedance_Reg_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Attedance_Reg_Count) + ") Attedance Regularization in Inbox" + "\n";
                        aEF2.InnerText = "Pending Attedance Regularization (" + Convert.ToString(Attedance_Reg_Count) + ")";
                        aEF2.Title = "Pending Attedance Regularization (" + Convert.ToString(Attedance_Reg_Count) + ")";
                        lilblAttedanceRegularization.Visible = true;
                    }
                    else
                    {
                        lilblAttedanceRegularization.Visible = false;
                    }

                   /* GetTimesheetCount(emp_code);
                    if (Timesheet_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Timesheet_Count) + ") Timesheet In Inbox" + "\n";
                        aEF3.InnerText = "Pending Timesheet (" + Convert.ToString(Timesheet_Count) + ")";
                        aEF3.Title = "Pending Timesheet (" + Convert.ToString(Timesheet_Count) + ")";
                        lilblTimesheet.Visible = true;
                    }
                    else
                    {
                        lilblTimesheet.Visible = false;
                    }*/

                    //ITAsset
                    GetITAssetCount(emp_code);
                    if (IT_Asset_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(IT_Asset_Count) + ") IT Asset Request in Inbox" + "\n";
                        aIT1.InnerText = "Pending IT Asset Request (" + Convert.ToString(IT_Asset_Count) + ")";
                        aIT1.Title = "Pending IT Asset Request (" + Convert.ToString(IT_Asset_Count) + ")";
                        lilblITAsset.Visible = true;
                    }
                    else
                    {
                        lilblITAsset.Visible = false;
                    }


                    //Recruitment & Requisition start 
                    /*GetRecruitment_Requisition_APP_Count(emp_code);
                    if (Recruit_Req_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Recruit_Req_Cnt) + ") Requisition Approval in Inbox" + "\n";
                        ARECRQ.InnerText = "Requisition Approval (" + Convert.ToString(Recruit_Req_Cnt) + ")";
                        ARECRQ.Title = "Requisition Approval (" + Convert.ToString(Recruit_Req_Cnt) + ")";
                        liRecruit_Req_APP.Visible = true;
                    }
                    else
                    {
                        liRecruit_Req_APP.Visible = false;
                    }*/

                    //Recruiter Pending
                    /*Get_Recruitment_Req_Recruiter();
                    if (Recruiter_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Recruiter_Cnt) + ") Recruiter Inbox" + "\n";
                        ARecruiter.InnerText = "Recruiter Inbox (" + Convert.ToString(Recruiter_Cnt) + ")";
                        ARecruiter.Title = "Recruiter Inbox (" + Convert.ToString(Recruiter_Cnt) + ")";
                        liRecruiter.Visible = true;
                    }
                    else
                    {
                        liRecruiter.Visible = false;
                    }
                    */

                    //Screener Pending
                    /* Get_Recruitment_Req_Screener();
                     if (Screener_Cnt > 0)
                     {
                         popup = popup + Environment.NewLine + "(" + Convert.ToString(Screener_Cnt) + ") Screener Inbox" + "\n";
                         AScreener.InnerText = "Screener Inbox (" + Convert.ToString(Screener_Cnt) + ")";
                         AScreener.Title = "Screener Inbox (" + Convert.ToString(Screener_Cnt) + ")";
                         liScreener.Visible = true;
                     }
                     else
                     {
                         liScreener.Visible = false;
                     }
                     */

                    /*Get_Recruitment_Req_ScheduleInt();
                    if (ScheduleInt_cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(ScheduleInt_cnt) + ") Schedule Interviews" + "\n";
                        AScheduleInt.InnerText = "Schedule Interviews (" + Convert.ToString(ScheduleInt_cnt) + ")";
                        AScheduleInt.Title = "Schedule Interviews(" + Convert.ToString(ScheduleInt_cnt) + ")";
                        liScheduleInt.Visible = true;
                    }
                    else
                    {
                        liScheduleInt.Visible = false;
                    }*/

                    /* Get_Recruitment_Req_Reschedule_Int();
                     if (RescheduleInt_cnt > 0)
                     {
                         popup = popup + Environment.NewLine + "(" + Convert.ToString(RescheduleInt_cnt) + ") Reschedule Interviews" + "\n";
                         ARescheduleInt.InnerText = "Reschedule Interviews (" + Convert.ToString(RescheduleInt_cnt) + ")";
                         ARescheduleInt.Title = "Reschedule Interviews(" + Convert.ToString(RescheduleInt_cnt) + ")";
                         liRescheduleInt.Visible = true;
                     }
                     else
                     {
                         liRescheduleInt.Visible = false;
                     }*/

                    /*  GetReq_Offer_Approval_PendingCount();
                      if (OfferApproval_cnt > 0)
                      {
                          popup = popup + Environment.NewLine + "(" + Convert.ToString(OfferApproval_cnt) + ") Offer Approval " + "\n";
                          AOffer.InnerText = "Offer Approval (" + Convert.ToString(OfferApproval_cnt) + ")";
                          AOffer.Title = "Offer Approval (" + Convert.ToString(OfferApproval_cnt) + ")";
                          liOfferAPP.Visible = true;
                      }
                      else
                      {
                          liOfferAPP.Visible = false;
                      }*/
                      
                   /* Get_Recruitment_Req_Interviewr(); //harshad
                    if (Interview_cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Interview_cnt) + ") Interviewer Inbox " + "\n";
                        AInterviews.InnerText = "Interviewer Inbox (" + Convert.ToString(Interview_cnt) + ")";
                        AInterviews.Title = "Interviewer Inbox (" + Convert.ToString(Interview_cnt) + ")";
                        liInterviewr.Visible = true;
                    }
                    else
                    {
                        liInterviewr.Visible = false;
                    }*/

                   /* GetCustEscalationCount(emp_code); //harshad
                    if (Cust_Escalation_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Cust_Escalation_Count) + ") CustomerFirst Request pending in Inbox" + "\n";
                        ACustEcla.InnerText = "Pending CustomerFirst Request (" + Convert.ToString(Cust_Escalation_Count) + ")";
                        ACustEcla.Title = "Pending CustomerFirst Request (" + Convert.ToString(Cust_Escalation_Count) + ")";
                        liCustEscala.Visible = true;
                    }
                    else
                    {
                        liCustEscala.Visible = false;
                    }*/


                    // CustomerFirst Pending Confirmation  
                     GetCustPendingConfirmationCount(emp_code);  // Harshad
                    if (Cust_Pending_Confir_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Cust_Pending_Confir_Count) + ") CustomerFirst Pending Confirmation in Inbox" + "\n";
                        ACustconfir.InnerText = " Pending CustomerFirst Confirmation Request (" + Convert.ToString(Cust_Pending_Confir_Count) + ")";
                        ACustconfir.Title = "Pending CustomerFirst Confirmation Request (" + Convert.ToString(Cust_Pending_Confir_Count) + ")";
                        liCustConfir.Visible = true;
                    }
                    else
                    {
                        liCustConfir.Visible = false;
                    } 

                    /*  GetReferralPendingCount(emp_code); // harshad
                      if (EmpModerator_Count > 0)
                      {
                          popup = popup + Environment.NewLine + "(" + Convert.ToString(EmpModerator_Count) + ") Moderator Pending in Inbox" + "\n";
                          AModerator.InnerText = " Moderator Pending (" + Convert.ToString(EmpModerator_Count) + ")";
                          AModerator.Title = "Moderator Pending (" + Convert.ToString(EmpModerator_Count) + ")";
                          liEmpModerator.Visible = true;
                      }
                      else
                      {
                          liEmpModerator.Visible = false;
                      }*/

                    //Exit Process

                    //InboxResignation

                    /* InboxResignationscount(emp_code); //Harshad
                     if (Resignation_Count > 0)
                     {
                         popup = popup + Environment.NewLine + "(" + Convert.ToString(Resignation_Count) + ") Resignation Request in Inbox" + "\n";
                         AResig.InnerText = "Pending Resignation Request (" + Convert.ToString(Resignation_Count) + ")";
                         AResig.Title = "Pending Resignation Request (" + Convert.ToString(Resignation_Count) + ")";
                         liResignation.Visible = true;
                     }
                     else
                     {
                         liResignation.Visible = false;
                     }*/

                    //Inbox Team Exit

                    /*  LoadTeamExitCount(emp_code); //Harshad
                      if (TeamExit_Count > 0)
                      {
                          popup = popup + Environment.NewLine + "(" + Convert.ToString(TeamExit_Count) + ") Team Exit Interview Request in Inbox" + "\n";

                          ATeamExit.InnerText = "Pending Team Exit Interview Request (" + Convert.ToString(TeamExit_Count) + ")";

                          ATeamExit.Title = "Pending Team Exit Interview Request (" + Convert.ToString(TeamExit_Count) + ")";

                          liTeamExit.Visible = true;

                      }

                      else

                      {

                          liTeamExit.Visible = false;

                      }*/

                    //Inbox clearance

                    /* LoadClearanceCount(emp_code); // harshad
                     if (Clearance_Count > 0)
                     {
                         popup = popup + Environment.NewLine + "(" + Convert.ToString(Clearance_Count) + ") Clearance Request in Inbox" + "\n";
                         AClearance.InnerText = "Pending Clearance Request (" + Convert.ToString(Clearance_Count) + ")";
                         AClearance.Title = "Pending Clearance Request (" + Convert.ToString(Clearance_Count) + ")";
                         liClearance.Visible = true;
                     }
                     else
                     {
                         liClearance.Visible = false;
                     }*/

                    //Task Monitoring
                    /* GetTaskPendingRequestCount(emp_code); //Harshad
                     if (TaskPending > 0)
                     {
                         popup = popup + Environment.NewLine + "(" + Convert.ToString(Interview_cnt) + ") Task Inbox " + "\n";
                         Task1.InnerText = " Pending Task (" + Convert.ToString(TaskPending) + ")";
                         Task1.Title = "Pending Task (" + Convert.ToString(TaskPending) + ")";
                         liTaskPending.Visible = true;
                     }
                     else
                     {
                         liTaskPending.Visible = false;
                     }*/

                    /*GetTaskCloseRequestCount(emp_code); // harshad
                    if (TaskCloseRequest > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Interview_cnt) + ") Task Closure Inbox " + "\n";
                        Task2.InnerText = "Task Closure Request (" + Convert.ToString(TaskCloseRequest) + ")";
                        Task2.Title = "Task Closure Request (" + Convert.ToString(TaskCloseRequest) + ")";
                        liTaskCloseRequest.Visible = true;
                    }
                    else
                    {
                        liTaskCloseRequest.Visible = false;
                    }*/

                   /* GetTaskDueDateRequestCount(emp_code);  // harshad
                    if (TaskDueDateChange > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Interview_cnt) + ") Due Date Change Request Inbox " + "\n";
                        Task3.InnerText = "Task Due Date Change Request (" + Convert.ToString(TaskDueDateChange) + ")";
                        Task3.Title = "Task Due Date Change Request (" + Convert.ToString(TaskDueDateChange) + ")";
                        liTaskDueDueDateRequest.Visible = true;
                    }
                    else
                    {
                        liTaskDueDueDateRequest.Visible = false;
                    }*/

                    // Harshad Start the Code for Review Delayed Tasks --- task Monitoring
                   /* getDelayedTaskPendingCount(emp_code);  //Harshad
                    if (ReviewDelayedTasks_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(ReviewDelayedTasks_Count) + ") Review Delayed Tasks in Inbox" + "\n";
                        AReviewDelayedTaskPending.InnerText = "Review Delayed Tasks (" + Convert.ToString(ReviewDelayedTasks_Count) + ")";
                        AReviewDelayedTaskPending.Title = "Review Delayed Tasks (" + Convert.ToString(ReviewDelayedTasks_Count) + ")";
                        LiReviewDelayedTaskPending.Visible = true;
                    }
                    else
                    {
                        LiReviewDelayedTaskPending.Visible = false;
                    }*/
                    // Harshad END the Code

                    // KRA Pending Count  
                  /*  GetKRAPendingCount(emp_code); //Harshad
                    if (Pending_KRA_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Pending_KRA_Cnt) + ") KRA Pending in Inbox" + "\n";
                        A_KRAPending.InnerText = " Pending KRA Request (" + Convert.ToString(Pending_KRA_Cnt) + ")";
                        A_KRAPending.Title = "Pending KRA Request (" + Convert.ToString(Pending_KRA_Cnt) + ")";
                        liKRAReviewer.Visible = true;
                    }
                    else
                    {
                        liKRAReviewer.Visible = false;
                    }*/

                    //OD Application 
                   /* InboxODApplicationCount(emp_code);  //harshad
                    if (PendingODApplication > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(PendingODApplication) + ") Pending OD Application Request " + "\n";
                        A_ODAPP.InnerText = "Pending OD Application Request (" + Convert.ToString(PendingODApplication) + ")";
                        A_ODAPP.Title = "Pending OD Application Request (" + Convert.ToString(PendingODApplication) + ")";
                        liODAPP.Visible = true;
                    }
                    else
                    {
                        liODAPP.Visible = false;
                    }*/

                    /* Inbox_Mode_PendingRecord(emp_code); //Harshad
                    if (Acceptance_APP_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Acceptance_APP_Cnt) + ") Appointment letter Acceptance Approval in Inbox" + "\n";
                        AAppLatterM.InnerText = "Appointment letter Acceptance Approval (" + Convert.ToString(Acceptance_APP_Cnt) + ")";
                        AAppLatterM.Title = "Appointment letter Acceptance Approval (" + Convert.ToString(Acceptance_APP_Cnt) + ")";
                        liAPPApproval.Visible = true;
                    }
                    else
                    {
                        liAPPApproval.Visible = false;
                    }*/

                   /* Inbox_APP_Acceptance(emp_code);  //Harshad
                    if (Acceptance_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Acceptance_Cnt) + ") Appointment Letter Acceptance in Inbox" + "\n";
                        AAcceptance.InnerText = "Appointment Letter Acceptance (" + Convert.ToString(Acceptance_Cnt) + ")";
                        AAcceptance.Title = "Appointment Letter Acceptance (" + Convert.ToString(Acceptance_Cnt) + ")";
                        liAPPAccept.Visible = true;
                    }
                    else
                    {
                        liAPPAccept.Visible = false;
                    }*/

                   /* Inbox_CTC_ExceptionAPP(emp_code);  // harshad
                    if (ExceptionAPP > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(ExceptionAPP) + ") CTC Exception Approval in Inbox" + "\n";
                        ACTCException.InnerText = "CTC Exception Approval (" + Convert.ToString(ExceptionAPP) + ")";
                        ACTCException.Title = "CTC Exception Approval (" + Convert.ToString(ExceptionAPP) + ")";
                        liExceptionAPP.Visible = true;
                    }
                    else
                    {
                        liExceptionAPP.Visible = false;
                    }*/

                  /*  GetUpdatSalaryStatusCount();  // harshad
                    if (Salary_Status_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Salary_Status_Count) + ") Salary Status Update" + "\n";
                        ASalaryStatusUpdate.InnerText = "Salary Status Update (" + Convert.ToString(Salary_Status_Count) + ")";
                        ASalaryStatusUpdate.Title = "Salary Status Update (" + Convert.ToString(Salary_Status_Count) + ")";
                        liSalStatusUpdate.Visible = true;
                    }
                    else
                    {
                        liSalStatusUpdate.Visible = false;
                    }*/

		          /* GetPaymentRequestPendingCount(emp_code); //Harshad
                    if (Payment_App_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_App_Count) + ") Payment Request Approvals in Inbox" + "\n";
                        APayApp.InnerText = "Payment Request Approvals (" + Convert.ToString(Payment_App_Count) + ")";
                        APayApp.Title = "Payment Request Approvals (" + Convert.ToString(Payment_App_Count) + ")";
                        liVendorPayApp.Visible = true;
                    }
                    else
                    {
                        liVendorPayApp.Visible = false;
                    }*/

                    /* GetPartialPaymentPendingCount(emp_code);  // harshad
                    if (Payment_Partial_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Partial_Count) + ") Partial Payment Requests in Inbox" + "\n";
                        APayPartial.InnerText = "Partial Payment Requests (" + Convert.ToString(Payment_Partial_Count) + ")";
                        APayPartial.Title = "Partial Payment Requests (" + Convert.ToString(Payment_Partial_Count) + ")";
                        liVendorPayPartial.Visible = true;
                    }
                    else
                    {
                        liVendorPayPartial.Visible = false;
                    }*/

                   /* GetPaymentCorrectionCount(emp_code); //Harshad
                    if (Payment_Corr_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Corr_Count) + ") Payment Request Correction in Inbox" + "\n";
                        APayCorr.InnerText = "Payment Request Correction (" + Convert.ToString(Payment_Corr_Count) + ")";
                        APayCorr.Title = "Payment Request Correction (" + Convert.ToString(Payment_Corr_Count) + ")";
                        liVendorPayCorr.Visible = true;
                    }
                    else
                    {
                        liVendorPayCorr.Visible = false;
                    }*/

                    /* getMngInvoiceReqstCount(emp_code); // Harshad
                    if (Invoice_App_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Invoice_App_Count) + ") Invoice Approvals in Inbox" + "\n";
                        AInvoiceApp.InnerText = "Invoice Approvals (" + Convert.ToString(Invoice_App_Count) + ")";
                        AInvoiceApp.Title = "Invoice Approvals (" + Convert.ToString(Invoice_App_Count) + ")";
                        liVendorInvoiceApp.Visible = true;
                    }
                    else
                    {
                        liVendorInvoiceApp.Visible = false;
                    }*/

                    /*getMng_PendingBatchCount(emp_code); // harshad
                    if (Batch_App_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Batch_App_Count) + ") Batch Approvals in Inbox" + "\n";
                        ABatchApp.InnerText = "Batch Approvals (" + Convert.ToString(Batch_App_Count) + ")";
                        ABatchApp.Title = "Batch Approvals (" + Convert.ToString(Batch_App_Count) + ")";
                        liVendorBatch.Visible = true;
                    }
                    else
                    {
                        liVendorBatch.Visible = false;
                    }*/

                   /* getMng_PendingPOWOCount(emp_code);  // harshad
                    if (POWO_APP_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(POWO_APP_Count) + ") Pending PO/ WO Approvals in Inbox" + "\n";
                        APOWOApp.InnerText = "Pending PO/ WO Approvals (" + Convert.ToString(POWO_APP_Count) + ")";
                        APOWOApp.Title = "Pending PO/ WO Approvals (" + Convert.ToString(POWO_APP_Count) + ")";
                        liVendorPOWOApp.Visible = true;
                    }
                    else
                    {
                        liVendorPOWOApp.Visible = false;
                    }*/


                   /* check_LoginEmployee_InvoiceCreateInboxpaymentReq(emp_code); //harshad
                    if (Payreq_APP_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Payreq_APP_Count) + ") Due Payment Request in Inbox" + "\n";
                        ADuePaymentRequest.InnerText = "Due Payment Request (" + Convert.ToString(Payreq_APP_Count) + ")";
                        ADuePaymentRequest.Title = "Due Payment Request (" + Convert.ToString(Payreq_APP_Count) + ")";
                        LiVendorPayRequestCre.Visible = true;
                    }
                    else
                    {
                        LiVendorPayRequestCre.Visible = false;
                    }*/

                  /*  CheckAccountDept(emp_code); //Harshad
                    if (Retention_M_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Retention_M_Cnt) + ") Employee Retention in Inbox" + "\n";
                        ARetentionAcc.InnerText = "Employee Retention (" + Convert.ToString(Retention_M_Cnt) + ")";
                        ARetentionAcc.Title = "Employee Retention (" + Convert.ToString(Retention_M_Cnt) + ")";
                        liRetentionAcc.Visible = true;
                    }
                    else
                    {
                        liRetentionAcc.Visible = false;
                    }*/

					// ADD New 13_01_2023 Vidhyadhar
                   /* getCountIsCandidateApprover(emp_code); // harshad
                    if (CandidateRequestSubmited_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(CandidateRequestSubmited_Count) + ") Verify Candidate Data" + "\n";
                        APendingCandidateDetailApprove.InnerText = "Verify Candidate Data (" + Convert.ToString(CandidateRequestSubmited_Count) + ")";
                        APendingCandidateDetailApprove.Title = "Verify Candidate Data (" + Convert.ToString(CandidateRequestSubmited_Count) + ")";
                        LiPendingCandidateDetailApprove.Visible = true;
                    }
                    else
                    {
                        LiPendingCandidateDetailApprove.Visible = false;
                    } */

                    // Add New Code CV Update
                   EmployeeCVUpdateStatus(emp_code); 
                    if (PendingCVUpdate > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(PendingCVUpdate) + ") Pending CV Update" + "\n";
                        APendingCVUpdate.InnerText = "Pending CV Update (" + Convert.ToString(PendingCVUpdate) + ")";
                        APendingCVUpdate.Title = "Pending CV Update (" + Convert.ToString(PendingCVUpdate) + ")";
                        LiPendingCVUpdate.Visible = true;
                    }
                    else
                    {
                        LiPendingCVUpdate.Visible = false;
                    }


                    /*EmployeeCVReviewInbox(emp_code); // harshad
                    if (PendingCVReviewInboxCount > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(PendingCVReviewInboxCount) + ") Pending Employee CV Review" + "\n";
                        ACVReviewInbox.InnerText = "Pending Employee CV Review (" + Convert.ToString(PendingCVReviewInboxCount) + ")";
                        ACVReviewInbox.Title = "Pending Employee CV Review (" + Convert.ToString(PendingCVReviewInboxCount) + ")";
                        LiCVReviewInbox.Visible = true;
                    }
                    else
                    {
                        LiCVReviewInbox.Visible = false;
                    }*/
                    
                   /* ETRInbox(emp_code); //Harshad
                    if (ETR_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(ETR_Count) + ") Pending Employee Transfer Request" + "\n";
                        AETRInbox.InnerText = "Pending Employee Transfer Request (" + Convert.ToString(ETR_Count) + ")";
                        AETRInbox.Title = "Pending Employee Transfer Request (" + Convert.ToString(ETR_Count) + ")";
                        LiETRInbox.Visible = true;
                    }
                    else
                    {
                        LiETRInbox.Visible = false;
                    }*/


                   /* Check_Advance_Pay_Count(emp_code); // Harshad
                    if (Adv_Pay_Cnt > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Adv_Pay_Cnt) + ") Advance Payment Approval in Inbox" + "\n";
                        AAdvPay.InnerText = "Advance Payment Approval (" + Convert.ToString(Adv_Pay_Cnt) + ")";
                        AAdvPay.Title = "Advance Payment Approval (" + Convert.ToString(Adv_Pay_Cnt) + ")";
                        liAdvPayAPP.Visible = true;
                    }
                    else
                    {
                        liAdvPayAPP.Visible = false;
                    }*/

 		           //Appraisal Notification

                    //Appraisal Performance Review :- Performance Review List  // SP Not Found
                    Check_Performance_Review_List(emp_code);
                    if (Appr_Performance_ReviewCnt > 0)
                    { 
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Appr_Performance_ReviewCnt) + ") Performance Review Inbox" + "\n";
                        A_ApprPerformanceReviewPending.InnerText = "Performance Review (" + Convert.ToString(Appr_Performance_ReviewCnt) + ")";
                        A_ApprPerformanceReviewPending.Title = "Performance Review (" + Convert.ToString(Appr_Performance_ReviewCnt) + ")";
                        liPerformanceReviewPendingCnt.Visible = true;
                    }
                    else
                    {
                        liPerformanceReviewPendingCnt.Visible = false;
                    }

                    //Appraisal Performance Recommendation :- Performance Recommendation List
                    Check_Performance_Recommendation_List(emp_code); // SP Not Found
                    if (Appr_Performance_RecommendationCnt > 0)
                    { 

                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Appr_Performance_RecommendationCnt) + ") Performance Recommendation Inbox" + "\n";
                        A_ApprPerformanceRecommendationPending.InnerText = "Performance Recommendation (" + Convert.ToString(Appr_Performance_RecommendationCnt) + ")";
                        A_ApprPerformanceRecommendationPending.Title = "Performance Recommendation (" + Convert.ToString(Appr_Performance_RecommendationCnt) + ")";
                        liPerformanceRecommendationPendingCnt.Visible = true;
                    }
                    else
                    {
                        liPerformanceRecommendationPendingCnt.Visible = false;
                    }


		           //ABAP Object Completion 
                   /* getABAPObjectCompletionCount(emp_code); // harshad
                    if (ABAPObjectCompletion_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(ABAPObjectCompletion_Count) + ") ABAP Object Completion in Inbox" + "\n";
                        AABAPObjectCompletion.InnerText = "ABAP Object Completion (" + Convert.ToString(ABAPObjectCompletion_Count) + ")";
                        AABAPObjectCompletion.Title = "ABAP Object Completion (" + Convert.ToString(ABAPObjectCompletion_Count) + ")";
                        LiABAPObjectCompletion.Visible = true;
                    }
                    else
                    {
                        LiABAPObjectCompletion.Visible = false;
                    }*/

 
		            //Empoyee Mediclaim Data
                   /* getEmployeeMediclaimDataCount(emp_code);  // Harshad
                    if (EmployeeMediclaim_Data_Count > 0)
                    {
                        popup = popup + Environment.NewLine + "(" + Convert.ToString(EmployeeMediclaim_Data_Count) + ") Employee Mediclaim Data in Inbox" + "\n";
                        AEmployeeMediclaimData.InnerText = "Employee Mediclaim Data (" + Convert.ToString(EmployeeMediclaim_Data_Count) + ")";
                        AEmployeeMediclaimData.Title = "Employee Mediclaim Data (" + Convert.ToString(EmployeeMediclaim_Data_Count) + ")";
                        liEmployeeMediclaimData.Visible = true;
                    }
                    else
                    {
                        liEmployeeMediclaimData.Visible = false;
                    }*/

                     

                    //Custs Service Request New 
                    GetCustsServiceRequestCount(emp_code);
                    if (Custs_ServiceRequest_Count > 0)
                    {

                        popup = popup + Environment.NewLine + "(" + Convert.ToString(Custs_ServiceRequest_Count) + ") CustomerFIRST Request in Inbox" + "\n";

                        if (HDCusts_HOD.Value == "1")
                        {
                            aEFCSRHOD.InnerText = "Inprocess CustomerFIRST Requests  (" + Convert.ToString(Custs_ServiceRequest_Count) + ")";
                            aEFCSRHOD.Title = "Inprocess CustomerFIRST Requests  (" + Convert.ToString(Custs_ServiceRequest_Count) + ")";
                            LilblCusts_ServiceRequestHOD.Visible = true;

                            getCustomerServiceRequestCount_CSHead(emp_code);
                            if (Custs_ServiceRequest_Count_CS > 0)
                            {
                                popup = popup + Environment.NewLine + "(" + Convert.ToString(Custs_ServiceRequest_Count_CS) + ") CustomerFIRST Request in Inbox" + "\n";
                                ACustomerServiceRequest.InnerText = "CustomerFIRST Request Approval (" + Convert.ToString(Custs_ServiceRequest_Count_CS) + ")";
                                ACustomerServiceRequest.Title = "CustomerFIRST Request Approval (" + Convert.ToString(Custs_ServiceRequest_Count_CS) + ")";
                                liCustomerServiceRequest.Visible = true;
                            }
                            else
                            {
                                liCustomerServiceRequest.Visible = false;
                            }

                            getCustomerServiceRequestPendingCount_CSHead(emp_code);
                            if (Custs_ServiceRequest_PendingCount_CS > 0)
                            {
                                popup = popup + Environment.NewLine + "(" + Convert.ToString(Custs_ServiceRequest_PendingCount_CS) + ") CustomerFIRST Request in Inbox" + "\n";
                                aCSPending.InnerText = "Pending CustomerFIRST Request(" + Convert.ToString(Custs_ServiceRequest_PendingCount_CS) + ")";
                                aCSPending.Title = "Pending CustomerFIRST Request (" + Convert.ToString(Custs_ServiceRequest_PendingCount_CS) + ")";
                                liCustomerServiceRequest_CS.Visible = true;
                            }
                            else
                            {
                                liCustomerServiceRequest_CS.Visible = false;
                            }

                        }
                        else
                        {
                            aEFCSR.InnerText = "Pending CustomerFIRST Requests  (" + Convert.ToString(Custs_ServiceRequest_Count) + ")";
                            aEFCSR.Title = "Pending CustomerFIRST Requests  (" + Convert.ToString(Custs_ServiceRequest_Count) + ")";
                            LilblCusts_ServiceRequest.Visible = true;
                        }

                    }
                    else
                    {
                        LilblCusts_ServiceRequest.Visible = false;
                        LilblCusts_ServiceRequestHOD.Visible = false;
                    }

                    /* iKRANotAccept_Cnt = PendingRequest.get_KRA_NotAccepted_Count(emp_code); // Harshad
                     if (iKRANotAccept_Cnt > 0)
                     {
                         AKRANotAccepted.InnerText = "Pending KRA Acceptance (" + Convert.ToString(iKRANotAccept_Cnt) + ")";
                         AKRANotAccepted.Title = "Pending KRA Acceptance (" + Convert.ToString(iKRANotAccept_Cnt) + ")";
                         liKRANotAccepted.Visible = true;
                     }
                     else
                     {
                         liKRANotAccepted.Visible = false;
                     }*/

                    /*  employeephotoupdate(emp_code); // harshad
                      if (iUpdatePhotoCnt > 0)
                      {
                          employeephoto.InnerText = "Employee Update Photo (" + Convert.ToString(iUpdatePhotoCnt) + ")";
                          employeephoto.Title = "Employee Update Photo (" + Convert.ToString(iUpdatePhotoCnt) + ")";
                          liphoto.Visible = true;
                      }
                      else
                      {
                          liphoto.Visible = false;
                      }*/


                    ///
                    /* Tot_Cnt = Leave_Cnt + Mobile_Cnt + Fuel_Cnt + Payment_Cnt + Trvl_Exp_Cnt + Service_Request_Count 
                         + Customer_FIRST_Count + Attedance_Reg_Count + Timesheet_Count + IT_Asset_Count + Recruit_Req_Cnt + Recruiter_Cnt + 
                         Screener_Cnt + ScheduleInt_cnt + RescheduleInt_cnt + Interview_cnt + OfferApproval_cnt + Cust_Escalation_Count + 
                         Cust_Pending_Confir_Count + EmpModerator_Count + Resignation_Count + TeamExit_Count + Clearance_Count + 
                         TaskPending + TaskCloseRequest + TaskDueDateChange + Pending_KRA_Cnt + PendingODApplication + 
                         Acceptance_APP_Cnt + Acceptance_Cnt + ExceptionAPP + Salary_Status_Count + Payment_App_Count + Payment_Corr_Count + Payment_Partial_Count + Invoice_App_Count + Batch_App_Count + POWO_APP_Count + Payreq_APP_Count + ReviewDelayedTasks_Count + CandidateRequestSubmited_Count + Retention_M_Cnt + PendingCVUpdate + PendingCVReviewInboxCount + ETR_Count + Adv_Pay_Cnt  + Appr_Performance_ReviewCnt + Appr_Performance_RecommendationCnt 
                 + ABAPObjectCompletion_Count + EmployeeMediclaim_Data_Count + CustomerServicePendingCnt + Custs_ServiceRequest_Count+ Custs_ServiceRequest_Count_CS + Custs_ServiceRequest_PendingCount_CS+iKRANotAccept_Cnt+iUpdatePhotoCnt;
                     */


                    Tot_Cnt = icnt + Customer_FIRST_Count + Attedance_Reg_Count + IT_Asset_Count + Cust_Pending_Confir_Count + PendingCVUpdate + Appr_Performance_ReviewCnt + Appr_Performance_RecommendationCnt + Custs_ServiceRequest_Count;

                    if (Tot_Cnt > 0)
                    {
                        btnShowMsg.Text = "Notifications (" + Tot_Cnt + ")";
                        //lblMsg.Text = popup;
                    }
                    else
                    {
                        btnShowMsg.Text = "Notifications (0)";
                        //lblMsg.Text = popup;
                    }
                    //Tot_Cnt = Leave_Cnt + Mobile_Cnt + Fuel_Cnt + Payment_Cnt + Trvl_Exp_Cnt + Service_Request_Count+ Customer_FIRST_Count;
                    //if (Tot_Cnt > 0)
                    //{
                    //    btnShowMsg.Text = "Notifications (" + Tot_Cnt + ")";
                    //    //lblMsg.Text = popup;
                    //}
                    //else
                    //{
                    //    btnShowMsg.Text = "Notifications (0)";
                    //    //lblMsg.Text = popup;
                    //}

                }
                popularcontrol2();
                loadhcclinks();
            }
        }
    }

    public void CheckAccountDept(string emp_code)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = spm.Get_Dept_Retention_Detail("Retention_Pending_Mode", Convert.ToString(Session["Empcode"]).Trim());
            if (dt.Rows.Count > 0)
            {
                Retention_M_Cnt = dt.Rows.Count;
            }


        }
        catch (Exception ex)
        {

        }
    }
    private void getDelayedTaskPendingCount(string emp_code)
    {
        DataTable dtresult = new DataTable();
        try
        {
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GetDelayedTaskPendingCount";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]);
            dtresult = spm.getMobileRemDataList(spars, "SP_TASK_M_DETAILS_DASHBOARD");
            if (dtresult.Rows.Count > 0)
            {
                if (Convert.ToInt32(dtresult.Rows[0]["DelayedTask"]) > 0)
                {
                    ReviewDelayedTasks_Count = Convert.ToInt32(dtresult.Rows[0]["DelayedTask"]);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void GetKRAPendingCount(string emp_code)
    {
        try
        {
            Pending_KRA_Cnt = 0;
            DataTable dtScreener = spm.getPendingKRACount(emp_code);
            if (dtScreener.Rows.Count > 0)
            {
                Pending_KRA_Cnt = Convert.ToInt32(dtScreener.Rows[0]["KRA_Pending_Cnt"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    //CustFirst Pending Confirmation  25-10-21
    public void GetCustPendingConfirmationCount(string emp_code)
    {
        try
        {
            DataTable dtconfirmation = new DataTable();
            int PendingCount = 0;
            dtconfirmation = spm.GetCustEscalation_Approval("EscalatedApproval", emp_code);
            if (dtconfirmation.Rows.Count > 0)
            {

                foreach (DataRow row in dtconfirmation.Rows)
                {
                    int AUTO_ESCALATIONDay = 8;
                    var diffDay = 0.00;
                    var Id = Convert.ToInt32(row["Id"].ToString());
                    var ServicesRequestID = Convert.ToString(row["ServicesRequestID"].ToString());
                    var ServiceDepartment = Convert.ToString(row["ServiceDepartment"].ToString());
                    var AssignedDate = Convert.ToDateTime(row["AssignedDates"].ToString()).Date;
                    var todayDate = DateTime.Now.Date;
                    string officeLocation = "HO-NaviMum";
                    var getDayesdt = spm.GetCustEscalationServiceCount(officeLocation, AssignedDate, todayDate);
                    if (getDayesdt.Rows.Count > 0)
                    {
                        diffDay = Convert.ToInt32(getDayesdt.Rows[0]["WORKINGDAY"]);
                    }
                    else
                    {
                        diffDay = (todayDate - AssignedDate).TotalDays;
                    }
                    DataTable dtSPOC = spm.GetCustEscalationSPOCData(ServiceDepartment);
                    if (dtSPOC.Rows.Count > 0)
                    {
                        AUTO_ESCALATIONDay = Convert.ToInt32(dtSPOC.Rows[0]["USER_ESCALATION"]);
                    }

                    if (diffDay <= AUTO_ESCALATIONDay)
                    {
                        PendingCount = PendingCount + 1;
                    }
                }
                if (PendingCount > 0)
                {
                    Cust_Pending_Confir_Count = PendingCount;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    //Cust Escalation  05-10-21
    public void GetCustEscalationCount(string emp_code)
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetCustEscalationSPOCInbox(emp_code);
            if (dtleaveInbox.Rows.Count > 0)
            {
                Cust_Escalation_Count = dtleaveInbox.Rows.Count;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getTravel_Expenses_PendingList_cnt_Approver(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_get_Expenses_travel_Reqst_Pending_cnt";

            spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");

            //Travel Request Count
            //if (dsTrDetails.Tables[0].Rows.Count > 0)
            //{
            //    lnk_trvlinbox.Text = " Inbox Travel  requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
            //}

            //Expenses Request Count
            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                Trvl_Exp_Cnt = Trvl_Exp_Cnt + Convert.ToInt32(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void get_Pending_LeaveReqstList_cnt(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_inboxlst_cnt_HR";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "Usp_getEmployee_Details_All");

            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Leave_Cnt = Leave_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["leave_reqst_pending"]);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Mobile_Approver_Count(string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");


            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Mobile_Cnt = Mobile_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Mobile_ACC_COS_Count(strtype, emp_code);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Mobile_ACC_COS_Count(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");



            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    Mobile_Cnt = Mobile_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }
                if (strtype == "RCFO")
                {
                    Mobile_Cnt = Mobile_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }

            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Fuel_Approver_Count(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[1].Rows.Count > 0)
            {
                Fuel_Cnt = Fuel_Cnt + Convert.ToInt32(dsTrDetails.Tables[1].Rows[0]["trvl_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC_Fuel(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Fuel_ACC_COS_Count(strtype, emp_code);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Fuel_ACC_COS_Count(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Fuel_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {

                if (strtype == "RCOS")
                {
                    Fuel_Cnt = Fuel_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }
                if (strtype == "RACC")
                {
                    Fuel_Cnt = Fuel_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Payment_Approver_Count(string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Mobile_claim_Reqst_Pending_cnt_Appr";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");

            //Mobile Claim Request Count
            if (dsTrDetails.Tables[2].Rows.Count > 0)
            {
                Payment_Cnt = Payment_Cnt + Convert.ToInt32(dsTrDetails.Tables[2].Rows[0]["trvl_reqst_pending"]);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void checkTD_COS_ACC_Trvl(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_TD_COS_apprver_code_byType";

            spars[1] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {

                getTravel_Expenses_PendingList_cnt_TDCOSACC(strtype, emp_code);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void check_COS_ACC_Payment(string strtype, string emp_code)
    {
        try
        {

            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_Check_COS_ACC_apprver_code_byType";

            spars[1] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[1].Value = strtype;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");
            //Travel Request Count
            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                Payment_ACC_COS_Count(strtype, emp_code);

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void Payment_ACC_COS_Count(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[3];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "get_Payment_claim_Reqst_Pending_cnt_COSACC";

            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            spars[2] = new SqlParameter("@trappr", SqlDbType.VarChar);
            spars[2].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALLreembursement_DETAILS");



            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                if (strtype == "RACC")
                {
                    Payment_Cnt = Payment_Cnt + Convert.ToInt32(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]);
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getTravel_Expenses_PendingList_cnt_TDCOSACC(string strtype, string emp_code)
    {
        try
        {
            DataSet dsTrDetails = new DataSet();
            SqlParameter[] spars = new SqlParameter[4];

            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Sp_get_Expenses_travel_Reqst_Pending_cnt_TDCOSACC";

            spars[1] = new SqlParameter("@exp_sr_no", SqlDbType.Int);
            spars[1].Value = DBNull.Value;

            spars[2] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[2].Value = emp_code;

            spars[3] = new SqlParameter("@exp_trvl", SqlDbType.VarChar);
            spars[3].Value = strtype;

            dsTrDetails = spm.getDatasetList(spars, "SP_GETALL_Expense_DETAILS");


            //lnk_trvl_COSInbox.Visible = false;
            //lnk_trvl_AccInbox.Visible = false;

            //Travel Request Count


            if (dsTrDetails.Tables[0].Rows.Count > 0)
            {
                //if (strtype == "TD")
                //{
                //    lnk_trvl_TDInbox.Text = "Travel Desk Inbox requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                //}
                //if (strtype == "COS")
                //{
                //    lnk_trvl_COSInbox.Text = "COS Inbox Travel requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                //}
                if (strtype == "ACC")
                {
                    Trvl_Exp_Cnt = Trvl_Exp_Cnt + Convert.ToInt32(dsTrDetails.Tables[1].Rows[0]["expenses_reqst_pending"]);
                    //lnk_trvl_AccInbox.Text = "ACC Inbox Travel requests:(" + Convert.ToString(dsTrDetails.Tables[0].Rows[0]["trvl_reqst_pending"]).Trim() + ")";
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    protected void loadorder()
    {
        if (Page.User.Identity.IsAuthenticated)
        {

            navuser.Visible = true;
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (user.Rows.Count > 0)
            {
                hduserid.Value = user.Rows[0]["indexid"].ToString();
                HttpCookie ckey = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
                if (ckey != null)
                {
                    skey = ckey.Value.ToString().Trim();
                }
                HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
                if (cookie != null)
                {
                    if (cookie.Value.ToString().Trim() == "true")
                    {
                        navchattrigger.HRef = ConfigurationManager.AppSettings["chatURL"] + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()) + "&skey=" + skey + "&internet=true";
                    }
                    else
                    {
                        navchattrigger.HRef = "https://ess.highbartech.com/hccchat/" + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()) + "&skey=" + skey + "&internet=false";
                    }
                }
                else
                {
                    navchattrigger.HRef = "https://ess.highbartech.com/hccchat/" + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()) + "&skey=" + skey + "&internet=false";
                }

                lnkchatlink.HRef = ReturnUrl("chatapp").ToString() + "default.aspx?id=" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkprof.HRef = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkrating.HRef = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkfollowing.HRef = ReturnUrl("sitepathmain") + "following/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkfollower.HRef = ReturnUrl("sitepathmain") + "followers/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkviewprofile.HRef = ReturnUrl("sitepathmain") + "profile/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                lnkeditprofile.HRef = ReturnUrl("sitepathmain") + "procs/editprofile";

                //SAGAR COMMENTED THIS LINE FOR REMOVING GROUP CONTROL FROM FRONT END 19SEPT2017
                //lnkgroup.HRef = ReturnUrl("sitepathmain") + "groups/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());

                lnkfavorites.HRef = ReturnUrl("sitepathmain") + "favorites/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                // NEW CODE ADDED BY SONY SHOWING USER NAME OF USER ONLY ON DEFAULT PAGE 5OCT2017
                //if (Request.Url.AbsoluteUri.Contains("http://localhost/hrms/default.aspx") || Request.Url.AbsoluteUri.Contains("http://localhost/hrms/default"))
                //{
                //    lblname.Text = "Welcome " + user.Rows[0]["firstname"].ToString().Trim() + " " + user.Rows[0]["lastname"].ToString().Trim();
                //}
                //else
                //{
                //    lblname.Text = " ";
                //}
                lblfname.Text = user.Rows[0]["firstname"].ToString().Trim();

                DataTable dtadd = classaddress.getuserinfodetails(Page.User.Identity.Name);

                string proimg = "";
                if (dtadd.Rows.Count > 0)
                {
                    if (dtadd.Rows[0]["profilephoto"].ToString().Trim() == "" || dtadd.Rows[0]["profilephoto"].ToString().Trim() == null)
                    {
                        proimg = "noimage1.png";
                    }
                    else
                    {
                        proimg = dtadd.Rows[0]["profilephoto"].ToString().Trim();
                    }
                }
                else
                {
                    proimg = "noimage1.png";
                }
                //profileimg.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + proimg;
                imgbigpic.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + proimg;
                m_flag = "U";
            }
        }
    }
    public void loadgrid()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            if (user.Rows.Count > 0)
            {
                username = user.Rows[0]["username"].ToString();
            }
            else
            {
                username = Page.User.Identity.Name.ToString();
            }
            DataTable dt2 = new DataTable();

            DataTable dt1 = classreviews.getNotificationHeader(username);
            if (dt1.Rows.Count > 0)
            {
                dt2 = dt1.AsEnumerable().Take(10).CopyToDataTable();
                rptnotification.DataSource = dt2;
                rptnotification.DataBind();
                int count1 = dt1.Rows.Count;
                if (rptnotification.Items.Count > 0)
                {
                    divallnotify.Visible = true;
                    divmsg.Visible = false;
                    notificationsbody.Visible = true;


                    //notification_count.Text = count1.ToString();
                    hdcount.Value = count1.ToString();
                    for (int i = 0; i < rptnotification.Items.Count; i++)
                    {
                        Label lbldate = (Label)rptnotification.Items[i].FindControl("lbldate");
                        Image imgprofile = (Image)rptnotification.Items[i].FindControl("imgprofile");
                        if (dt2.Rows[i]["profilephoto"].ToString() != "")
                        {
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dt2.Rows[i]["profilephoto"].ToString())))
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dt2.Rows[i]["profilephoto"].ToString();
                            }
                            else
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                            }
                        }
                        else
                        {
                            imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                        }
                        DateTime dat = Convert.ToDateTime(dt2.Rows[i]["eventdate"].ToString());
                        lbldate.Text = dat.ToString("dd-MMM-yyyy") + " " + dat.ToString("h:mm tt");
                    }
                    if (count1 > 10)
                    {
                        divallnotify.Visible = true;
                    }
                    else
                    {
                        divallnotify.Visible = true;
                    }
                }
                else
                {
                    divallnotify.Visible = true;
                    divmsg.Visible = true;
                    notification_count.Visible = false;
                    notification_count.Attributes.Add("style", "display:none;");
                    notificationsbody.Visible = false;
                }
            }
            else
            {
                divallnotify.Visible = true;
                divmsg.Visible = true;
                notification_count.Visible = false;
                notification_count.Attributes.Add("style", "display:none;");
                notificationsbody.Visible = false;
            }
        }
        else
        {
            divallnotify.Visible = true;
            divmsg.Visible = true;
            notification_count.Visible = false;
            notification_count.Attributes.Add("style", "display:none;");
            notificationsbody.Visible = false;
        }
    }

     protected void btnShowMsg_Click(object sender, EventArgs e)
    {
       // Response.Redirect("http://localhost/hrms/procs/notifications.aspx");
 	
       Response.Redirect("https://ess.highbartech.com/hrms/procs/notifications.aspx");
        #region code comment by Sanjay on 21.11.2024
        /*
        Leave_Cnt = 0;
        int LeaveCount = 0;
        string emp_code = "";
        string popup = "";
        if (Convert.ToString(Session["Empcode"]).Trim() == "" || Session["Empcode"] == null)
        {
            btnShowMsg.Text = "Notifications (0)";
        }
        else
        {
            emp_code = Convert.ToString(Session["Empcode"]).Trim();

            LeaveCount = spm.GetLeaveInboxCount(emp_code);
            Leave_Cnt = Leave_Cnt + LeaveCount;
            get_Pending_LeaveReqstList_cnt(emp_code);
            if (Leave_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Leave_Cnt) + ") Leave approvals pending in Inbox";
                //lblMsg.Text = "(" + Convert.ToString(Leave_Cnt) + ") Leave approvals pending in Inbox";
            }
            Mobile_Approver_Count(emp_code);
            check_COS_ACC("RCOS", emp_code);
            check_COS_ACC("RACC", emp_code);
            check_COS_ACC("RCFO", emp_code);

            if (Mobile_Cnt > 0)
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Mobile_Cnt) + ") Mobile approvals pending in Inbox";
            
            Fuel_Approver_Count(emp_code);
            check_COS_ACC_Fuel("RCOS", emp_code);
            check_COS_ACC_Fuel("RACC", emp_code);
            check_COS_ACC_Fuel("RCFO", emp_code);

            if (Fuel_Cnt > 0)
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Fuel_Cnt) + ") Fuel approvals pending in Inbox";
            
            Payment_Approver_Count(emp_code);
            check_COS_ACC_Payment("RCOS", emp_code);
            check_COS_ACC_Payment("RACC", emp_code);
            check_COS_ACC_Payment("RCFO", emp_code);

            if (Payment_Cnt > 0)
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Cnt) + ") Payment approvals pending in Inbox";
            
            getTravel_Expenses_PendingList_cnt_Approver(emp_code);
            checkTD_COS_ACC_Trvl("TD", emp_code);
            checkTD_COS_ACC_Trvl("COS", emp_code);
            checkTD_COS_ACC_Trvl("ACC", emp_code);

            if (Trvl_Exp_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Trvl_Exp_Cnt) + ") Travel Expense approvals pending in Inbox";
            }


            GetServiceRequestCount(emp_code);
            if (Service_Request_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Service_Request_Count) + ") EmployeeFIRST Request pending in Inbox" + "\n";
            }
            //Customer FIRST
            GetCustomerFIRSTCount(emp_code);
            if (Customer_FIRST_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Customer_FIRST_Count) + ") Customer Feedback Response in Inbox" + "\n";
            }
            //Attedance And Timesheet

            GetAttednceRegCount(emp_code);
            if (Attedance_Reg_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Attedance_Reg_Count) + ") Attedance Regularization in Inbox" + "\n";
            }
            GetTimesheetCount(emp_code);
            if (Timesheet_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Timesheet_Count) + ") Timesheet In Inbox" + "\n";
            }
            //ITAsset
            GetITAssetCount(emp_code);
            if (IT_Asset_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(IT_Asset_Count) + ") IT Asset Request in Inbox" + "\n";

            }

            //Recruitment & Requisition start 
            GetRecruitment_Requisition_APP_Count(emp_code);
            if (Recruit_Req_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Recruit_Req_Cnt) + ") Requisition Approval in Inbox" + "\n";

            }

            //Recruiter Pending
            Get_Recruitment_Req_Recruiter();
            if (Recruiter_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Recruiter_Cnt) + ") Recruiter Inbox" + "\n";
            }

            //Screener Pending
            Get_Recruitment_Req_Screener();
            if (Screener_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Screener_Cnt) + ") Screener Inbox" + "\n";
            }

            Get_Recruitment_Req_ScheduleInt();
            if (ScheduleInt_cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(ScheduleInt_cnt) + ") Schedule Interviews" + "\n";
            }

            Get_Recruitment_Req_Reschedule_Int();
            if (RescheduleInt_cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(RescheduleInt_cnt) + ") Reschedule Interviews" + "\n";
            }

            GetReq_Offer_Approval_PendingCount();
            if (OfferApproval_cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(OfferApproval_cnt) + ") Offer Approval " + "\n";
            }

            Get_Recruitment_Req_Interviewr();
            if (Interview_cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Interview_cnt) + ") Interviewer Inbox " + "\n";
            }
            GetCustEscalationCount(emp_code);
            if (Cust_Escalation_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Cust_Escalation_Count) + ") CustomerFirst Request pending in Inbox" + "\n";
            }

            GetCustPendingConfirmationCount(emp_code);
            if (Cust_Pending_Confir_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Cust_Pending_Confir_Count) + ") CustomerFirst Pending Confirmation in Inbox" + "\n";
            }
            GetReferralPendingCount(emp_code);
            if (EmpModerator_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(EmpModerator_Count) + ") Moderator Pending in Inbox" + "\n";
            }

            Inbox_Mode_PendingRecord(emp_code);
            if (Acceptance_APP_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Acceptance_APP_Cnt) + ") Appointment letter Acceptance Approval in Inbox" + "\n";
                liAPPApproval.Visible = true;
            }


            Inbox_APP_Acceptance(emp_code);
            if (Acceptance_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Acceptance_Cnt) + ") Appointment Letter Acceptance in Inbox" + "\n";
                liAPPAccept.Visible = true;
            }
            Inbox_CTC_ExceptionAPP(emp_code);
            if (ExceptionAPP > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(ExceptionAPP) + ") CTC Exception Approval in Inbox" + "\n";
                liExceptionAPP.Visible = true;
            }
            
            GetUpdatSalaryStatusCount();
            if (Salary_Status_Count > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Salary_Status_Count) + ") Salary Status Update" + "\n";
                liSalStatusUpdate.Visible = true;
            }

            Check_Advance_Pay_Count(emp_code);
            if (Adv_Pay_Cnt > 0)
            {
                popup = popup + Environment.NewLine + "(" + Convert.ToString(Adv_Pay_Cnt) + ") Advance Payment Approval in Inbox" + "\n";
                liAdvPayAPP.Visible = true;
            }


            Tot_Cnt = Leave_Cnt + Mobile_Cnt + Fuel_Cnt + Payment_Cnt + Trvl_Exp_Cnt + Service_Request_Count + Customer_FIRST_Count + Attedance_Reg_Count + Timesheet_Count + IT_Asset_Count + Recruit_Req_Cnt + Recruiter_Cnt + Screener_Cnt + ScheduleInt_cnt + RescheduleInt_cnt + Interview_cnt + OfferApproval_cnt + Cust_Escalation_Count + Cust_Pending_Confir_Count + EmpModerator_Count + Acceptance_APP_Cnt + Acceptance_Cnt + ExceptionAPP+Salary_Status_Count + Adv_Pay_Cnt;


            if (Tot_Cnt > 0)
            {
                btnShowMsg.Text = "Notifications (" + Tot_Cnt + ")";
                //lblMsg.Text = popup;
            }
            else
            {
                btnShowMsg.Text = "Notifications (0)";
                //lblMsg.Text = popup;
            }
        }
        */
#endregion


                }

                //SAGAR COMMENTED THIS FOR REMOVING ADDPOST LOGIC 21SEPT2017 STARTS HERE
                //public void loadaddpost()
                //{
                //    try
                //    {
                //        DataSet dtcat = new DataSet();
                //        dtcat.ReadXml(ReturnUrl("adminsitepath") + "xml/addcats.xml");
                //        if (dtcat.Tables.Count > 0)
                //        {
                //            if (dtcat.Tables.Count > 0)
                //            {
                //                rptcat.DataSource = dtcat;
                //                rptcat.DataBind();
                //            }
                //        }
                //        else
                //        {
                //        }
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}
                //SAGAR COMMENTED THIS FOR REMOVING ADDPOST LOGIC 21SEPT2017 ENDS HERE


                //SAGAR COMMENTED THIS FOR REMOVING BROWSE SECTION LOGIC 21SEPT2017 STARTS HERE
                //public void loadcats()
                //{
                //    try
                //    {
                //        DataSet dtcat = new DataSet();
                //        dtcat.ReadXml(ReturnUrl("adminsitepath") + "xml/cats.xml");
                //        if (dtcat.Tables.Count > 0)
                //        {
                //            if (dtcat.Tables[0].Rows.Count > 0)
                //            {
                //                rptcats.DataSource = dtcat.Tables[0];
                //                rptcats.DataBind();
                //            }
                //        }
                //        else
                //        {
                //        }
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //}
                //SAGAR COMMENTED THIS FOR REMOVING BROWSE SECTION LOGIC 21SEPT2017 ENDS HERE

                public string getAddPostURL(object catname, object cid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingpost(catname.ToString().Trim(), UrlRewritingVM.Encrypt(cid.ToString().Trim()), "APS");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public string getcategoryURL(object catname, object cid)
    {
        string strurl = "";
        try
        {
            strurl = UrlRewritingVM.getUrlRewritingInfo(catname.ToString().Trim(), UrlRewritingVM.Encrypt(cid.ToString().Trim()), "PS");
            return strurl;
        }
        catch (Exception ex)
        {
            return strurl;
        }
    }
    public void loadmsg()
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            DataTable user = classreviews.getuseridbyemail(Page.User.Identity.Name);
            DataTable dt2 = new DataTable();
            DataTable dt1 = classreviews.getnewmessage(Page.User.Identity.Name.ToString().Trim());
            if (dt1.Rows.Count > 0)
            {
                dt2 = dt1.AsEnumerable().Take(10).CopyToDataTable();
                rptmsg.DataSource = dt2;
                rptmsg.DataBind();
                int count = dt1.Rows.Count;
                if (rptmsg.Items.Count > 0)
                {
                    msg_count.Text = count.ToString();
                    msghdcount.Value = count.ToString();
                    for (int i = 0; i < rptmsg.Items.Count; i++)
                    {
                        Label lbldate = (Label)rptmsg.Items[i].FindControl("lbldate");
                        Image imgprofile = (Image)rptmsg.Items[i].FindControl("imgprofile");
                        if (dt2.Rows[i]["profilepic"].ToString() != "")
                        {
                            if (File.Exists(Server.MapPath("~/themes/creative1.0/images/profile55x55/" + dt2.Rows[i]["profilepic"].ToString())))
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/" + dt2.Rows[i]["profilepic"].ToString();
                            }
                            else
                            {
                                imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                            }
                        }
                        else
                        {
                            imgprofile.ImageUrl = ReturnUrl("sitepath") + "images/profile55x55/noimage1.png";
                        }
                        DateTime dat = Convert.ToDateTime(dt2.Rows[i]["eventdate"].ToString());
                        lbldate.Text = dat.ToString("dd-MMM-yyyy") + " " + dat.ToString("h:mm tt"); ;
                    }
                    if (count > 2)
                    {
                        msgtitleall.Visible = true;
                    }
                    else
                    {
                        msgtitleall.Visible = false;
                    }
                }
                else
                {
                    divmsg1.Visible = true;
                    pnkmsg.Visible = false;
                    msgtitleall.Visible = false;
                    msg_count.Visible = false;
                    msg_count.Attributes.Add("style", "display:none;");
                }
            }
            else
            {
                msghdcount.Value = dt1.Rows.Count.ToString();
                pnkmsg.Visible = false;
                msgtitleall.Visible = false;
                divmsg1.Visible = true;
                msg_count.Attributes.Add("style", "display:none !important;");
                msg_count.Visible = false;
            }
        }
        else
        {
            int hdcnt = 0;
            msghdcount.Value = hdcnt.ToString();
            divmsg1.Visible = true;
            msg_count.Attributes.Add("style", "display:none;");
            msg_count.Visible = false;
        }
    }
    public string getnotificationURL(object DisplayText)
    {
        string strAttrValue = "";
        bool iserror = false;
        try
        {
            iserror = classreviews.updatenotification(Page.User.Identity.Name, DisplayText.ToString());
            if (iserror == true)
            {
                DataTable user = classreviews.getuseridbyemail(DisplayText.ToString());
                if (user.Rows.Count > 0)
                {
                    strAttrValue = ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString());
                }
            }
            return strAttrValue;
        }
        catch (Exception ex)
        {
            return strAttrValue;
        }
    }
    protected void rptnotification_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    protected void rptmsg_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }
    protected void lnkuser_Click(object sender, EventArgs e)
    {
        var btn = (LinkButton)sender;
        var item = (RepeaterItem)btn.NamingContainer;
        var ddl = (Label)item.FindControl("follow");
        var ddlevent = (Label)item.FindControl("lblevent");
        var lblid = (Label)item.FindControl("lblpid");
        var lblindexid = (Label)item.FindControl("indexid");
        bool iserror = false;
        iserror = true;
        if (iserror == true)
        {
            if (ddlevent.Text == "follow")
            {
                classreviews.updatenotification(Page.User.Identity.Name, ddl.Text.ToString());
                DataTable user = classreviews.getuseridbyemail(ddl.Text.ToString());
                if (user.Rows.Count > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "user/" + UrlRewritingVM.Encrypt(user.Rows[0]["indexid"].ToString()));
                }
            }
            else if (ddlevent.Text == "like" || ddlevent.Text == "comment" || ddlevent.Text == "post" || ddlevent.Text == "followerpost")
            {
                int j = 0;
                j = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(lblindexid.Text.ToString()), "read"));
                if (j > 0)
                {
                    DataTable ds = classproduct.get_proc_ProductDescription_ProdFeature(Convert.ToInt32(lblid.Text.ToString().Trim()));
                    if (ds.Rows.Count > 0)
                    {
                        Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(ds.Rows[0]["productid"].ToString()), "PDID"));
                    }
                }
            }
            else if (ddlevent.Text == "review")
            {
                int j = 0;
                j = Convert.ToInt32(classreviews.updateNotificationReadStatus(Convert.ToInt32(lblindexid.Text.ToString()), "read"));
                if (j > 0)
                {
                    Response.Redirect(ReturnUrl("sitepathmain") + "comments/" + UrlRewritingVM.Encrypt(lblid.Text.ToString()));
                }
            }
            else if (ddlevent.Text == "survey")
            {
                Response.Redirect(UrlRewritingVM.getUrlRewritingInfo("", UrlRewritingVM.Encrypt(lblid.Text.ToString().Trim()), "SR"));
            }
        }
    }
    protected void lnkuser1_Click(object sender, EventArgs e)
    {
        var btn = (LinkButton)sender;
        var item = (RepeaterItem)btn.NamingContainer;
        var id = (Label)item.FindControl("lblmsgid");
        var ddlevent = (Label)item.FindControl("lblevent");
        if (ddlevent.Text == "message")
        {
            bool flag = classmailsend.UpdateUnreadStatus(Convert.ToInt32(id.Text.ToString()), "N");
            Response.Redirect(string.Format(ReturnUrl("sitepathmain") + "composemail.aspx?mid={0}", UrlRewritingVM.Encrypt(id.Text.ToString())));
        }
    }
    protected void btnSingOut_Click(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            classaddress.UpdateUserStatus(Page.User.Identity.Name.ToString().Trim(), 0);
        }
        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        if (cookie != null)
        {
            if (cookie.Value.ToString().Trim() == "true")
            {
                Session.Clear();
                Session.Abandon();
                Request.Cookies.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect(ConfigurationManager.AppSettings["internetURL"].ToString().Trim());
            }
            else
            {
                Session.Clear();
                Session.Abandon();
                Request.Cookies.Clear();
                FormsAuthentication.SignOut();
                Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
            }
        }
        else
        {
            Session.Clear();
            Session.Abandon();
            Request.Cookies.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect(ConfigurationManager.AppSettings["intranetURL"].ToString().Trim());
        }
    }
    protected void popularcontrol2()
    {
        try
        {
            creative.Common clsCommon = new creative.Common();
            DataSet ds = clsCommon.Read_XML("HomeBannermiddle.xml", ReturnUrl("sitepathadmin") + "xml/");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    topheadpromo.Visible = true;
                    rptpromo.DataSource = ds.Tables[0];
                    rptpromo.DataBind();
                }
            }
            else
            {
                topheadpromo.Visible = false;
                lbllnkmsg.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLog.WriteError(ex.ToString());
        }

    }
    public void loadhcclinks()
    {
        HttpCookie cookie = HttpContext.Current.Request.Cookies["internet"];
        if (cookie != null)
        {
            if (cookie.Value.ToString().Trim() == "true")
            {
                lnkpass.HRef = ConfigurationManager.AppSettings["ChangePasswordTrue"].ToString();
                lnkqst.HRef = ConfigurationManager.AppSettings["SecurityQstTrue"].ToString();
            }
            else
            {
                lnkpass.HRef = ConfigurationManager.AppSettings["ChangePassword"].ToString();
                lnkqst.HRef = ConfigurationManager.AppSettings["SecurityQst"].ToString();
            }
        }
        else
        {
            lnkpass.HRef = ConfigurationManager.AppSettings["ChangePassword"].ToString();
            lnkqst.HRef = ConfigurationManager.AppSettings["SecurityQst"].ToString();
        }
    }
    protected void lnkadmin_Click(object sender, EventArgs e)
    {
        MembershipUser currentUser = Membership.GetUser(Page.User.Identity.Name.ToString().Trim());
        if (currentUser != null)
        {
            Response.Redirect(ReturnUrl("sitepathadmin") + "login.aspx?unamehigh=" + Page.User.Identity.Name.ToString().Trim());
        }
    }

    public void login()
    {
        MembershipUser user = Membership.GetUser(Page.User.Identity.Name.ToString().Trim());
        if (user != null)
        {
            online = user.IsOnline.ToString();
        }
    }
    //Add 04-02-2021 - employeeFIRST Request Count
    public void GetServiceRequestCount(string emp_code)
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetSerivesRequestSPOCInbox(emp_code);
            if (dtleaveInbox.Rows.Count > 0)
            {
                Service_Request_Count = dtleaveInbox.Rows.Count;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
     //Add 05-03-2021 Cuatomer First Count
    public void GetCustomerFIRSTCount(string emp_code)
    {
        try
        {
            // For PM/HOD
            DataTable dtleaveInbox1 = new DataTable();
            dtleaveInbox1 = spm.GetPMORHOD_INBOX(emp_code);
            if (dtleaveInbox1.Rows.Count > 0)
            {
                Customer_FIRST_Count = dtleaveInbox1.Rows.Count;
            }
            //// For HOD
            //DataTable dtleaveInbox = new DataTable();
            //dtleaveInbox = spm.GetHOD_INBOX(emp_code);
            ////lnk_leaverequest.Text = "Inbox :(0)";
            //if (dtleaveInbox.Rows.Count > 0)
            //{
            //    Customer_FIRST_Count=dtleaveInbox.Rows.Count;
            //}
            //For CEO
            DataTable dtCEOEMPCODE = new DataTable();
            // dtCEOEMPCODE = spm.GetCEOEmpCode();
            dtCEOEMPCODE = spm.Get_CSH_CEOEmpCode();
            if (dtCEOEMPCODE.Rows.Count > 0)
            {
                var loginCode = Convert.ToString(emp_code);
                var CeoEmpCode = Convert.ToString(dtCEOEMPCODE.Rows[0]["Emp_Code"]);
                if (loginCode == CeoEmpCode)
                {
                    var getCEOInbox = spm.GetCEO_INBOX();
                    if (getCEOInbox.Rows.Count > 0)
                    {
                        Customer_FIRST_Count = getCEOInbox.Rows.Count;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    //Attedance And Timesheet
    public void GetAttednceRegCount(string emp_code)
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetAttRegInbox(emp_code);

            if (dtleaveInbox.Rows.Count > 0)
            {
                Attedance_Reg_Count = dtleaveInbox.Rows.Count;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void GetTimesheetCount(string emp_code)
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetTimesheetRegInbox(emp_code);

            if (dtleaveInbox.Rows.Count > 0)
            {
                Timesheet_Count = dtleaveInbox.Rows.Count;
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void GetRecruitment_Requisition_APP_Count(string emp_code)
    {
        try
        {
            int RequisitionCount = 0;
            RequisitionCount = spm.getRequisitionPending_InboxList_Count(emp_code);
            if (RequisitionCount > 0)
            {
                Recruit_Req_Cnt = RequisitionCount;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void Get_Recruitment_Req_Recruiter()
    {
        try
        {
            DataTable dtRecruiter = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetRecruiterCnt";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            dtRecruiter = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dtRecruiter.Rows.Count > 0)
            {
                Recruiter_Cnt = (int)dtRecruiter.Rows[0]["Recruitcnt"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void Get_Recruitment_Req_Screener()
    {
        try
        {
            DataTable dtRecruiter = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetScreenerCnt";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            dtRecruiter = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dtRecruiter.Rows.Count > 0)
            {
                Screener_Cnt = (int)dtRecruiter.Rows[0]["Recruitcnt"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void Get_Recruitment_Req_ScheduleInt()
    {
        try
        {
            DataTable dtRecruiter = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetScheduleIntCnt";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            dtRecruiter = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dtRecruiter.Rows.Count > 0)
            {
                ScheduleInt_cnt = (int)dtRecruiter.Rows[0]["Recruitcnt"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void Get_Recruitment_Req_Reschedule_Int()
    {
        try
        {
            DataTable dtRecruiter = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetRescheduleIntCnt";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            dtRecruiter = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dtRecruiter.Rows.Count > 0)
            {
                RescheduleInt_cnt = (int)dtRecruiter.Rows[0]["Recruitcnt"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void Get_Recruitment_Req_Interviewr()
    {
        try
        {
            DataTable dtRecruiter = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "GetInterviewrCnt";
            spars[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(Session["Empcode"]).Trim();
            dtRecruiter = spm.getMobileRemDataList(spars, "SP_GET_REQ_REQUISTIONNO");
            if (dtRecruiter.Rows.Count > 0)
            {
                Interview_cnt = (int)dtRecruiter.Rows[0]["Recruitcnt"];
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    private void GetReq_Offer_Approval_PendingCount()
    {
        try
        {
            int OfferCount = 0;
            OfferCount = spm.getReq_Offer_Approval_Pending_InboxList_Count(Convert.ToString(Session["Empcode"]).Trim());
            if (OfferCount > 0)
            {
                OfferApproval_cnt = OfferCount;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    public void GetITAssetCount(string emp_code)
    {
        try
        {
            // For CUSTODIAN
            var loginCode = Convert.ToString(emp_code);
            DataTable dtCustodianCODE = new DataTable();
            DataTable dtleaveInbox1 = new DataTable();
            DataTable dtleaveInbox = new DataTable();
            dtCustodianCODE = spm.GetAllCustodianDetails();
            if (dtCustodianCODE.Rows.Count > 0)
            {
                var result = dtCustodianCODE.Select("EmpCode like '%" + loginCode + "%'");
                if (result != null)
                {
                    dtleaveInbox1 = spm.GetCustodianINBOX(emp_code);
                    if (dtleaveInbox1.Rows.Count > 0)
                    {
                        IT_Asset_Count = dtleaveInbox1.Rows.Count;
                    }
                    else
                    {
                        IT_Asset_Count = 0;
                    }
                }
            }

            DataTable dtITHODCODE = new DataTable();
            var ITHodCode = "";
            //GetITHOD
            dtITHODCODE = spm.GetITHod();
            if (dtITHODCODE.Rows.Count > 0)
            {
                ITHodCode = Convert.ToString(dtITHODCODE.Rows[0]["HOD"]);

            }
            if (loginCode == ITHodCode)
            {
                dtleaveInbox = spm.GetITHodINBOX(emp_code);
                if (dtleaveInbox.Rows.Count > 0)
                {
                    IT_Asset_Count = dtleaveInbox.Rows.Count;
                }
                else
                {
                    IT_Asset_Count = 0;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }

    //Referral Pending  Confirmation  26-11-21
    public void GetReferralPendingCount(string emp_code)
    {
        try
        {

            DataTable dtScreener = spm.GetRef_CandidatedList(emp_code, "GetReferralPendingCount");
            if (dtScreener.Rows.Count > 0)
            {
                EmpModerator_Count = dtScreener.Rows.Count;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    #region Exit Process

    //Pending Resignation/Resignation_Count

    private void InboxResignationscount(string empcode)

    {

        try

        {

            DataTable dtResigDetails = new DataTable();

            dtResigDetails = spm.InboxResignations(empcode);

            if (dtResigDetails.Rows.Count > 0)

            {

                Resignation_Count = dtResigDetails.Rows.Count;

            }

            else

            {

                Resignation_Count = 0;

            }

        }

        catch (Exception ex)

        {

            Response.Write(ex.Message.ToString());

        }

    }



    //Pending Team Exit/Team_Exit_Count

    public void LoadTeamExitCount(string empcode)

    {

        try

        {

            DataTable dtExitIntCnt = spm.GetTeamExitInterviewListFormDetails(empcode);

            if (dtExitIntCnt.Rows.Count > 0)

            {

                TeamExit_Count = dtExitIntCnt.Rows.Count;

            }

            else

            {

                TeamExit_Count = 0;

            }

        }

        catch (Exception)

        {



            throw;

        }

    }



    //Pending Clearance/Clearance_Count

    public void LoadClearanceCount(string empcode)

    {

        try

        {

            DataTable dtClearanceInbxCnt = spm.GetClearanceInbox(empcode);

            if (dtClearanceInbxCnt.Rows.Count > 0)

            {

                Clearance_Count = dtClearanceInbxCnt.Rows.Count;

            }

            else

            {

                Clearance_Count = 0;

            }

        }

        catch (Exception)

        {



            throw;

        }

    }

    #endregion

    public void GetTaskPendingRequestCount(string emp_code)
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.getTaskInboxCount("GetTaskExecuterInbox", "SP_TASK_M_EXECUTER", emp_code);
            if (dtleaveInbox.Rows.Count > 0)
            {
                TaskPending = dtleaveInbox.Rows.Count;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }
    }
    public void GetTaskCloseRequestCount(string emp_code)
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.getTaskInboxCount("getCloseChangeRequest", "SP_TASK_M_DETAILS", emp_code);
            if (dtleaveInbox.Rows.Count > 0)
            {
                TaskCloseRequest = dtleaveInbox.Rows.Count;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void GetTaskDueDateRequestCount(string emp_code)
    {
        try
        {
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.getTaskInboxCount("getDueDateChangeRequest", "SP_TASK_M_DETAILS", emp_code);
            if (dtleaveInbox.Rows.Count > 0)
            {
                TaskDueDateChange = dtleaveInbox.Rows.Count;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void InboxODApplicationCount(string Emp_code)
    {
        try
        {
            // DataSet dsList = new DataSet();
            DataTable dtleaveInbox = new DataTable();
            dtleaveInbox = spm.GetODApplicationInbox(Emp_code, "WFHApprovalInbox");

            if (dtleaveInbox.Rows.Count > 0)
            {
                PendingODApplication = dtleaveInbox.Rows.Count;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void Inbox_Mode_PendingRecord(string emp_code)
    {
        try
        {
            DataTable dtEmployee = new DataTable();
            dtEmployee = spm.Get_APP_Employee_Details("Moderation_Pending_Count", "SP_APP_Employee_Details", emp_code);
            if (dtEmployee.Rows.Count > 0)
            {
                Acceptance_APP_Cnt = dtEmployee.Rows.Count;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void Inbox_APP_Acceptance(string emp_code)
    {
        try
        {
            DataTable dtEmployee = new DataTable();
            dtEmployee = spm.Get_APP_Employee_Details("Employee_Pending_Count", "SP_APP_Employee_Details", emp_code);
            if (dtEmployee.Rows.Count > 0)
            {
                Acceptance_Cnt = dtEmployee.Rows.Count;
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void Inbox_CTC_ExceptionAPP(string emp_code)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = spm.CTC_Exception_Approval("Select_CTC_Pending_Search", 0, 0, Convert.ToString(emp_code), 0, 0, 0);
            if (dt.Rows.Count > 0)
            {
                ExceptionAPP = dt.Rows.Count;
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void GetUpdatSalaryStatusCount()
    {
        try
        {
            DataTable dtSalStatusCount = new DataTable();
            dtSalStatusCount = spm.GetPendingSalaryStatusUpdateForRM(Convert.ToString(Session["Empcode"]).Trim());
            if (dtSalStatusCount.Rows.Count > 0)
            {
                Salary_Status_Count = dtSalStatusCount.Rows.Count;
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    //Vendor Billing  24-01-22
    protected void GetPaymentRequestPendingCount(string emp_code)
    {
        try
        {
            int PaymentRequestCount = 0;
            PaymentRequestCount = spm.GetPaymentRequest_Pending_InboxList_Count("Get_PaymentRequest_Pending_Approver_cnt", emp_code);
            if (PaymentRequestCount > 0)
            {
                Payment_App_Count = PaymentRequestCount;
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void GetPartialPaymentPendingCount(string emp_code)
    {
        try
        {
            int PaymentRequestCount = 0;
            PaymentRequestCount = spm.GetPaymentRequest_Pending_InboxList_Count("Get_PartialPayment_Pending_Approver_cnt", emp_code);
            if (PaymentRequestCount > 0)
            {
                Payment_Partial_Count = PaymentRequestCount;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    protected void GetPaymentCorrectionCount(string emp_code)
    {
        try
        {
            int PaymentRequestCount = 0;
            PaymentRequestCount = spm.GetPaymentRequest_Pending_InboxList_Count("Get_Payment_Pending_Correction_cnt", emp_code);
            if (PaymentRequestCount > 0)
            {
                Payment_Corr_Count = PaymentRequestCount;
            }
        }
        catch (Exception)
        {

            throw;
        }

    }
    private void getMngInvoiceReqstCount(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPendingInvoiceCount(emp_code);
            if (dtTravelRequest.Rows.Count > 0)
            {
                Invoice_App_Count = Convert.ToInt32(dtTravelRequest.Rows[0]["NoofInvoices"]);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getMng_PendingBatchCount(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPendingBatchReqList(emp_code);
            if (dtTravelRequest.Rows.Count > 0)
            {
                Batch_App_Count = dtTravelRequest.Rows.Count;
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getMng_PendingPOWOCount(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getPendingPOWOCount(emp_code);
            if (dtTravelRequest.Rows.Count > 0)
            {
                if(Convert.ToInt32(dtTravelRequest.Rows[0]["NoofPOWOs"]) >0)
                POWO_APP_Count = Convert.ToInt32(dtTravelRequest.Rows[0]["NoofPOWOs"]);
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void check_LoginEmployee_InvoiceCreateInboxpaymentReq(string emp_code)
    {
        try
        {
            DataSet dsList = new DataSet();
            SqlParameter[] spars = new SqlParameter[2];
            spars[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            spars[0].Value = "Check_InvoicePR_forLoginEmployee";
            spars[1] = new SqlParameter("@empcode", SqlDbType.VarChar);
            spars[1].Value = emp_code.Trim();
            dsList = spm.getDatasetList(spars, "SP_VSCB_GETALL_DETAILS");
            if (dsList != null)
            {
                if (dsList.Tables[1].Rows.Count > 0)
                {
                    Payreq_APP_Count = dsList.Tables[1].Rows.Count;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void getCountIsCandidateApprover(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getRecruitmentInboxByCandiate("getRecruitmentInbox", Convert.ToString(emp_code));
            if (dtTravelRequest.Rows.Count > 0)
            {
                CandidateRequestSubmited_Count = dtTravelRequest.Rows.Count;
            }
        }
        catch (Exception ex)
        {

        }
    }
	//Add New 13_01_2023 Vidhyadhar
	private void EmployeeCVUpdateStatus(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getEmployeeCVReviewInbox("IsCheckEmployeeCVPending", Convert.ToString(emp_code));
            if (dtTravelRequest.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(dtTravelRequest.Rows[0]["Status"]);
                var getReviewedBy = Convert.ToString(dtTravelRequest.Rows[0]["ReviewedBy"]);
                if (getStatus=="Pending")
                {
                    PendingCVUpdate = 1;
                }
                if(getReviewedBy=="")
                {
                    PendingCVUpdate = 1;
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void EmployeeCVReviewInbox(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            dtTravelRequest = spm.getEmployeeCVReviewInbox("getEmployeeCVReviewInbox", Convert.ToString(emp_code));
            if (dtTravelRequest.Rows.Count > 0)
            {
                PendingCVReviewInboxCount = Convert.ToInt32(dtTravelRequest.Rows.Count);
            }
        }
        catch (Exception ex)
        {

        }
    }
    private void ETRInbox(string emp_code)
    {
        try
        {
            DataTable dtTravelRequest = new DataTable();
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GETListOfRequestAppr";

            spars[1] = new SqlParameter("@CREATEDBY", SqlDbType.VarChar);
            spars[1].Value = emp_code;

            dtTravelRequest = spm.getEmployeeTransferDDLData(spars, "SP_EmployeeTransferRequest");
            if (dtTravelRequest.Rows.Count > 0)
            {
                ETR_Count = Convert.ToInt32(dtTravelRequest.Rows.Count);
            }
        }
        catch (Exception ex)
        {

        }
    }

    public void Check_Advance_Pay_Count(string emp_code)
    {
        try
        {
            int Adv_App_Count = 0;
            Adv_App_Count = spm.GetPaymentRequest_Pending_InboxList_Count("Get_ADV_Pay_Pending_cnt", emp_code);
            if (Adv_App_Count > 0)
            {
                Adv_Pay_Cnt = Adv_App_Count;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

 //Appraisal Notifications
    public void Check_Performance_Review_List(string emp_code)
    {
        try
        {
            int PerCount = 0;
            PerCount = spm.GetInboxPendingCount(Convert.ToString(Session["Empcode"]).Trim(), 2);
            if (PerCount > 0)
            {
                Appr_Performance_ReviewCnt = PerCount;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }
    public void Check_Performance_Recommendation_List(string emp_code)
    {
        try
        {
            int PerCount = 0;
            PerCount = spm.GetInboxPendingCount(Convert.ToString(Session["Empcode"]).Trim(), 4);
            if (PerCount > 0)
            {
                Appr_Performance_RecommendationCnt= PerCount;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    //ABAP Completion 
    private void getABAPObjectCompletionCount(string emp_code)
    {
        try
        {
            SqlParameter[] sparsd = new SqlParameter[2];
            sparsd[0] = new SqlParameter("@stype", SqlDbType.VarChar);
            sparsd[0].Value = "GetApproverPageButton";
            sparsd[1] = new SqlParameter("@EmpCode", SqlDbType.VarChar);
            sparsd[1].Value = Convert.ToString(emp_code);
            DataSet DSApprover = spm.getDatasetList(sparsd, "SP_ABAP_Productivity_CompletionSheet");
            if (DSApprover.Tables[2].Rows.Count > 0)
            {
                if (Convert.ToInt32(DSApprover.Tables[2].Rows[0]["Countt"]) > 0)
                {
                    ABAPObjectCompletion_Count = Convert.ToInt32(DSApprover.Tables[2].Rows[0]["Countt"]);
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

//Employee Mediclaim Data
    private void getEmployeeMediclaimDataCount(string emp_code)
    {
        try
        {
            SqlParameter[] sparsd = new SqlParameter[2];
            sparsd[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            sparsd[0].Value = "get_employee_Mediclaim_Data_Count";

            sparsd[1] = new SqlParameter("@Emp_Code", SqlDbType.VarChar);
            sparsd[1].Value = Convert.ToString(emp_code);
            EmployeeMediclaim_Data_Count = 0;
            DataSet DSApprover = spm.getDatasetList(sparsd, "SP_Employee_Mediclaim_Data");
            if (DSApprover != null)
            {
                if (DSApprover.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(DSApprover.Tables[0].Rows[0]["Countt"]) > 0)
                    {
                        EmployeeMediclaim_Data_Count = Convert.ToInt32(DSApprover.Tables[0].Rows[0]["Countt"]);
                    }
                }
            }
        }
        catch (Exception)
        {

            throw;
        }
    }


    //Customer Service Request
    private void getCustomerServiceRequestCount(string emp_code)
    {
        try
        {
            

            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_CustomerSERVICELIST_forAction";

            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(emp_code);

            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");

            CustomerServicePendingCnt = 0;
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    CustomerServicePendingCnt = Convert.ToInt32(dslist.Tables[0].Rows.Count);
                }

            }

        }
        catch (Exception)
        {

            throw;
        }
    }

      // Cuatomer ServiceRequest Count
    public void GetCustsServiceRequestCount(string emp_code)
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_CustomerSERVICELIST_forAction";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    Custs_ServiceRequest_Count = dslist.Tables[0].Rows.Count;
                }
            }
            if (dslist.Tables[1].Rows.Count > 0)
            {
                Custs_ServiceRequest_Count = dslist.Tables[2].Rows.Count;
                HDCusts_HOD.Value = Convert.ToString(dslist.Tables[1].Rows.Count);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getCustomerServiceRequestCount_CSHead(string emp_code)
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];
            
            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_CustomerSERVICELIST_forAction";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            Custs_ServiceRequest_Count_CS = 0;
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    Custs_ServiceRequest_Count_CS = dslist.Tables[0].Rows.Count;
                }
            }
             
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    public void getCustomerServiceRequestPendingCount_CSHead(string emp_code)
    {
        try
        {
            SqlParameter[] spars = new SqlParameter[2];

            spars[0] = new SqlParameter("@qtype", SqlDbType.VarChar);
            spars[0].Value = "GET_CustomerSERVICELIST_forAction_CS";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = emp_code;
            DataSet dslist = spm.getDatasetList(spars, "SP_INSERTSERVICE_REQUEST");
            Custs_ServiceRequest_PendingCount_CS = 0;
            if (dslist != null)
            {
                if (dslist.Tables[0].Rows.Count > 0)
                {
                    Custs_ServiceRequest_PendingCount_CS = dslist.Tables[0].Rows.Count;
                }
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

    }

    private void employeephotoupdate(string emp_code)
    {
        var getdtDetails = new DataTable();
        try
        {
            liphoto.Visible = false;
            employeephoto.Visible = false;
            SqlParameter[] spars = new SqlParameter[3];
            spars[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
            spars[0].Value = "CheckIsShow_UpdatePhoto";
            spars[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[1].Value = Convert.ToString(emp_code).Trim();
            spars[2] = new SqlParameter("@ReportName", SqlDbType.NVarChar);
            spars[2].Value = "Update_Photo";
            getdtDetails = spm.getTeamReportAllDDL(spars, "SP_Update_Photo");

            if (getdtDetails.Rows.Count > 0)
            {
                var getStatus = Convert.ToString(getdtDetails.Rows[0]["IsAccess"]);
                if (getStatus == "SHOW")
                {
                    liphoto.Visible = true;
                    employeephoto.Visible = true;

                    SqlParameter[] sparsd = new SqlParameter[2];
                    sparsd[0] = new SqlParameter("@QUERYMODE", SqlDbType.VarChar);
                    sparsd[0].Value = "getPending_Count";
                    sparsd[1] = new SqlParameter("@emp_code", SqlDbType.VarChar);
                    sparsd[1].Value = Convert.ToString(emp_code);
                    DataSet DSApprover = spm.getDatasetList(sparsd, "SP_Update_Photo");
                    if (DSApprover.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(DSApprover.Tables[0].Rows[0]["pendingCount"]) > 0)
                        {
                            iUpdatePhotoCnt = Convert.ToInt32(DSApprover.Tables[0].Rows[0]["pendingCount"]);
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



    public int getEmployeeNotification(string emp_code)
    {
        int iTotalPendingCnt = 0;
        try
        {
            lilblMsg.Visible = false;
            lilblMsg_Mob.Visible = false;
            lilblMsg_Fuel.Visible = false;
            lilblMsg_Pay.Visible = false;
            lilblMsg_Travel.Visible = false;
            lilblServiceRequest.Visible = false;
            lilblTimesheet.Visible = false;
            liRecruit_Req_APP.Visible = false;
            liRecruiter.Visible = false;
            liScreener.Visible = false;
            liScheduleInt.Visible = false;
            liRescheduleInt.Visible = false;
            liOfferAPP.Visible = false;
            liInterviewr.Visible = false;
            liCustEscala.Visible = false;
            liEmpModerator.Visible = false;
            liResignation.Visible = false;
            liTeamExit.Visible = false;
            liClearance.Visible = false;
            liTaskPending.Visible = false;
            liTaskCloseRequest.Visible = false;
            LiReviewDelayedTaskPending.Visible = false;
            liKRAReviewer.Visible = false;
            liODAPP.Visible = false;
            liAPPApproval.Visible = false;
            liAPPAccept.Visible = false;
            liSalStatusUpdate.Visible = false;
            liVendorPayApp.Visible = false;
            liVendorPayPartial.Visible = false;
            liVendorPayCorr.Visible = false;
            liVendorInvoiceApp.Visible = false;
            liVendorBatch.Visible = false;
            liVendorPOWOApp.Visible = false;
            liRetentionAcc.Visible = false;
            LiPendingCandidateDetailApprove.Visible = false;
            LiCVReviewInbox.Visible = false;
            LiETRInbox.Visible = false;
            liAdvPayAPP.Visible = false;
            LiABAPObjectCompletion.Visible = false;
            liEmployeeMediclaimData.Visible = false;
            liKRANotAccepted.Visible = false;
            liCustConfir.Visible = false;
            liTaskDueDueDateRequest.Visible = false;
            liExceptionAPP.Visible = false;
            LiVendorPayRequestCre.Visible = false;
            liphoto.Visible = false;
            employeephoto.Visible = false;
            LiPendingCVUpdate.Visible = false;

            string popup = "";
            SqlParameter[] spars = new SqlParameter[1];            
            spars[0] = new SqlParameter("@empCode", SqlDbType.VarChar);
            spars[0].Value = emp_code;

            DataSet dsNotificationCnt = spm.getDatasetList(spars, "SP_Notification");
            if (dsNotificationCnt != null)
            {
                #region  -- Table 0 -- Pending Leave --getLeaveCount
                if (dsNotificationCnt.Tables[0].Rows.Count > 0)
                {
                    Leave_Cnt = (int) dsNotificationCnt.Tables[0].Rows[0]["TotalCount"];                  
                }
                if (Leave_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Leave_Cnt) + ") Leave approvals pending in Inbox" + "\n";
                    aL.InnerText = "Pending Leave Approvals (" + Convert.ToString(Leave_Cnt) + ")";
                    aL.Title = "Pending Leave Approvals (" + Convert.ToString(Leave_Cnt) + ")";
                    lilblMsg.Visible = true;
                }
                #endregion

                #region -- Table 1 -- Mobile Approver --getMobileClaimCount
                if (dsNotificationCnt.Tables[1].Rows.Count > 0)
                {
                    Mobile_Cnt =  Convert.ToInt32(dsNotificationCnt.Tables[1].Rows[0]["trvl_reqst_pending"]);                   
                }                 

                if (Mobile_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Mobile_Cnt) + ") Mobile approvals pending in Inbox" + "\n";
                    aM.InnerText = "Pending Mobile Approvals (" + Convert.ToString(Mobile_Cnt) + ")";
                    aM.Title = "Pending Mobile Approvals (" + Convert.ToString(Mobile_Cnt) + ")";
                    lilblMsg_Mob.Visible = true;
                }
                #endregion

                #region -- Table 2 -- Fuel Approver --getFuelClaimCount
                if (dsNotificationCnt.Tables[2].Rows.Count > 0)
                {
                    Fuel_Cnt =   Convert.ToInt32(dsNotificationCnt.Tables[2].Rows[0]["trvl_reqst_pending"]);
                }

                if (Fuel_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Fuel_Cnt) + ") Fuel approvals pending in Inbox" + "\n";
                    aF.InnerText = "Pending Fuel Approvals (" + Convert.ToString(Fuel_Cnt) + ")";
                    aF.Title = "Pending Fuel Approvals (" + Convert.ToString(Fuel_Cnt) + ")";
                    lilblMsg_Fuel.Visible = true;
                }


                #endregion

                #region -- Table 3 -- Payment Voucher Approver --getPaymentVoucherCount
                if (dsNotificationCnt.Tables[3].Rows.Count > 0)
                {
                    Payment_Cnt =   Convert.ToInt32(dsNotificationCnt.Tables[3].Rows[0]["trvl_reqst_pending"]);
                }

                if (Payment_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Cnt) + ") Payment approvals pending in Inbox";
                    aP.InnerText = "Pending Voucher Approvals (" + Convert.ToString(Payment_Cnt) + ")";
                    aP.Title = "Pending Voucher Approvals (" + Convert.ToString(Payment_Cnt) + ")";
                    lilblMsg_Pay.Visible = true;                   
                }


                #endregion

                #region -- Table 4 -- Travel Expenses Approver --getTravelExpensesCount
                if (dsNotificationCnt.Tables[4].Rows.Count > 0)
                {
                    Trvl_Exp_Cnt =  Convert.ToInt32(dsNotificationCnt.Tables[4].Rows[0]["expenses_reqst_pending"]);
                }

                if (Trvl_Exp_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Trvl_Exp_Cnt) + ") Travel Expense approvals pending in Inbox";
                    aE.InnerText = "Travel expense Approvals (" + Convert.ToString(Trvl_Exp_Cnt) + ")";
                    aE.Title = "Travel expense Approvals (" + Convert.ToString(Trvl_Exp_Cnt) + ")";
                    lilblMsg_Travel.Visible = true;
                }



                #endregion

                #region -- Table 5 -- Service Request Assigned --getServiceRequestCount
                if (dsNotificationCnt.Tables[5].Rows.Count > 0)
                {
                    Service_Request_Count = dsNotificationCnt.Tables[5].Rows.Count;
                }

                if (Service_Request_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Service_Request_Count) + ") EmployeeFIRST Request pending in Inbox" + "\n";
                    aEF.InnerText = "Pending EmployeeFIRST Request (" + Convert.ToString(Service_Request_Count) + ")";
                    aEF.Title = "Pending EmployeeFIRST Request (" + Convert.ToString(Service_Request_Count) + ")";
                    lilblServiceRequest.Visible = true;
                }



                #endregion

                #region -- Table 6 -- Pending for Timesheet Approval --getTimehseetPendingCount

                if (dsNotificationCnt.Tables[6].Rows.Count > 0)
                {
                    Timesheet_Count = dsNotificationCnt.Tables[6].Rows.Count;
                }
                if (Timesheet_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Timesheet_Count) + ") Timesheet In Inbox" + "\n";
                    aEF3.InnerText = "Pending Timesheet (" + Convert.ToString(Timesheet_Count) + ")";
                    aEF3.Title = "Pending Timesheet (" + Convert.ToString(Timesheet_Count) + ")";
                    lilblTimesheet.Visible = true;
                }
                #endregion

                #region -- Table 7 -- Requisition Pending Approval --getRequisitionPendingCount
                if (dsNotificationCnt.Tables[7].Rows.Count > 0)
                {
                    Recruit_Req_Cnt =Convert.ToInt32(dsNotificationCnt.Tables[7].Rows[0]["Recruitcnt"]);
                }
                if (Recruit_Req_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Recruit_Req_Cnt) + ") Requisition Approval in Inbox" + "\n";
                    ARECRQ.InnerText = "Requisition Approval (" + Convert.ToString(Recruit_Req_Cnt) + ")";
                    ARECRQ.Title = "Requisition Approval (" + Convert.ToString(Recruit_Req_Cnt) + ")";
                    liRecruit_Req_APP.Visible = true;
                }
                #endregion

                #region -- Table 8 -- Recruiter Pending --getRecruiterPendingCount
                if (dsNotificationCnt.Tables[8].Rows.Count > 0)
                {
                    Recruiter_Cnt = Convert.ToInt32(dsNotificationCnt.Tables[8].Rows[0]["Recruitcnt"]);
                }
                if (Recruiter_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Recruiter_Cnt) + ") Recruiter Inbox" + "\n";
                    ARecruiter.InnerText = "Recruiter Inbox (" + Convert.ToString(Recruiter_Cnt) + ")";
                    ARecruiter.Title = "Recruiter Inbox (" + Convert.ToString(Recruiter_Cnt) + ")";
                    liRecruiter.Visible = true;
                }
                #endregion

                #region -- Table 9 -- Screening Pending --getScreeningPendingCount
                if (dsNotificationCnt.Tables[9].Rows.Count > 0)
                {
                    Screener_Cnt = Convert.ToInt32(dsNotificationCnt.Tables[9].Rows[0]["Recruitcnt"]);
                }
                if (Screener_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Screener_Cnt) + ") Screener Inbox" + "\n";
                    AScreener.InnerText = "Screener Inbox (" + Convert.ToString(Screener_Cnt) + ")";
                    AScreener.Title = "Screener Inbox (" + Convert.ToString(Screener_Cnt) + ")";
                    liScreener.Visible = true;
                }
                #endregion

                #region -- Table 10 -- Schedule Interview Pending --getSchedule InterviewCount
                if (dsNotificationCnt.Tables[10].Rows.Count > 0)
                {
                    ScheduleInt_cnt = Convert.ToInt32(dsNotificationCnt.Tables[10].Rows[0]["Recruitcnt"]);
                }
                if (ScheduleInt_cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(ScheduleInt_cnt) + ") Schedule Interviews" + "\n";
                    AScheduleInt.InnerText = "Schedule Interviews (" + Convert.ToString(ScheduleInt_cnt) + ")";
                    AScheduleInt.Title = "Schedule Interviews(" + Convert.ToString(ScheduleInt_cnt) + ")";
                    liScheduleInt.Visible = true;
                }
                #endregion

                #region -- Table 11 -- Reschedule Interviews --getReschedule InterviewsCount
                if (dsNotificationCnt.Tables[11].Rows.Count > 0)
                {
                    RescheduleInt_cnt = Convert.ToInt32(dsNotificationCnt.Tables[11].Rows[0]["Recruitcnt"]);
                }
                if (RescheduleInt_cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(RescheduleInt_cnt) + ") Reschedule Interviews" + "\n";
                    ARescheduleInt.InnerText = "Reschedule Interviews (" + Convert.ToString(RescheduleInt_cnt) + ")";
                    ARescheduleInt.Title = "Reschedule Interviews(" + Convert.ToString(RescheduleInt_cnt) + ")";
                    liRescheduleInt.Visible = true;
                }
                #endregion

                #region -- Table 12 -- Offer Pending for Approval  --getOfferApprovalCount
                if (dsNotificationCnt.Tables[12].Rows.Count > 0)
                {
                    OfferApproval_cnt = Convert.ToInt32(dsNotificationCnt.Tables[12].Rows[0]["Recruitcnt"]);
                
                }
                if (OfferApproval_cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(OfferApproval_cnt) + ") Offer Approval " + "\n";
                    AOffer.InnerText = "Offer Approval (" + Convert.ToString(OfferApproval_cnt) + ")";
                    AOffer.Title = "Offer Approval (" + Convert.ToString(OfferApproval_cnt) + ")";
                    liOfferAPP.Visible = true;
                }
                #endregion

                #region -- Table 13 -- Interviewer Inbox  --Get_Recruitment_Req_Interviewr  --harshad
                if (dsNotificationCnt.Tables[13].Rows.Count > 0)
                {
                    Interview_cnt = Convert.ToInt32(dsNotificationCnt.Tables[13].Rows[0]["Recruitcnt"]);
                }
                if (Interview_cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Interview_cnt) + ") Interviewer Inbox " + "\n";
                    AInterviews.InnerText = "Interviewer Inbox (" + Convert.ToString(Interview_cnt) + ")";
                    AInterviews.Title = "Interviewer Inbox (" + Convert.ToString(Interview_cnt) + ")";
                    liInterviewr.Visible = true;
                }
                #endregion

                #region  -- Table 14 -- CustomerFirst Request pending  --GetCustEscalationCount  --harshad
                if (dsNotificationCnt.Tables[14].Rows.Count > 0)
                {
                    Cust_Escalation_Count = dsNotificationCnt.Tables[14].Rows.Count;
                }
                if (Cust_Escalation_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Cust_Escalation_Count) + ") CustomerFirst Request pending in Inbox" + "\n";
                    ACustEcla.InnerText = "Pending CustomerFirst Request (" + Convert.ToString(Cust_Escalation_Count) + ")";
                    ACustEcla.Title = "Pending CustomerFirst Request (" + Convert.ToString(Cust_Escalation_Count) + ")";
                    liCustEscala.Visible = true;
                }
                #endregion

                #region -- Table 15 -- Moderator Pending   --GetReferralPendingCount  --harshad
                if (dsNotificationCnt.Tables[15].Rows.Count > 0)
                {
                    EmpModerator_Count = dsNotificationCnt.Tables[15].Rows.Count;
                }
                if (EmpModerator_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(EmpModerator_Count) + ") Moderator Pending in Inbox" + "\n";
                    AModerator.InnerText = " Moderator Pending (" + Convert.ToString(EmpModerator_Count) + ")";
                    AModerator.Title = "Moderator Pending (" + Convert.ToString(EmpModerator_Count) + ")";
                    liEmpModerator.Visible = true;
                }
                #endregion

                #region -- Table 16 -- Resignation Request Pending   --InboxResignationscount  --harshad

                if (dsNotificationCnt.Tables[16].Rows.Count > 0)
                {
                    Resignation_Count = dsNotificationCnt.Tables[16].Rows.Count;
                }
                if (Resignation_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Resignation_Count) + ") Resignation Request in Inbox" + "\n";
                    AResig.InnerText = "Pending Resignation Request (" + Convert.ToString(Resignation_Count) + ")";
                    AResig.Title = "Pending Resignation Request (" + Convert.ToString(Resignation_Count) + ")";
                    liResignation.Visible = true;
                }
                #endregion

                #region  -- Table 17 -- Team Exit Interview Request Pending   --LoadTeamExitCount  --harshad

                if (dsNotificationCnt.Tables[17].Rows.Count > 0)
                {
                    TeamExit_Count = dsNotificationCnt.Tables[17].Rows.Count;
                }
                if (TeamExit_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(TeamExit_Count) + ") Team Exit Interview Request in Inbox" + "\n";
                    ATeamExit.InnerText = "Pending Team Exit Interview Request (" + Convert.ToString(TeamExit_Count) + ")";
                    ATeamExit.Title = "Pending Team Exit Interview Request (" + Convert.ToString(TeamExit_Count) + ")";
                    liTeamExit.Visible = true;
                }
                #endregion

                #region  -- Table 18 -- Clearance Request Pending   -- LoadClearanceCount  --harshad

                if (dsNotificationCnt.Tables[18].Rows.Count > 0)
                {
                    Clearance_Count = dsNotificationCnt.Tables[18].Rows.Count;
                }
                if (Clearance_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Clearance_Count) + ") Clearance Request in Inbox" + "\n";
                    AClearance.InnerText = "Pending Clearance Request (" + Convert.ToString(Clearance_Count) + ")";
                    AClearance.Title = "Pending Clearance Request (" + Convert.ToString(Clearance_Count) + ")";
                    liClearance.Visible = true;
                }
                #endregion

                #region  -- Table 19 -- Task Pending   --GetTaskPendingRequestCount  --harshad

                if (dsNotificationCnt.Tables[19].Rows.Count > 0)
                {
                    TaskPending = dsNotificationCnt.Tables[19].Rows.Count;
                }
                if (TaskPending > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(TaskPending) + ") Task Inbox " + "\n";
                    Task1.InnerText = " Pending Task (" + Convert.ToString(TaskPending) + ")";
                    Task1.Title = "Pending Task (" + Convert.ToString(TaskPending) + ")";
                    liTaskPending.Visible = true;
                }
                #endregion

                #region  -- Table 20 -- Task Closure Pending   --GetTaskCloseRequestCount  --harshad

                if (dsNotificationCnt.Tables[20].Rows.Count > 0)
                {
                    TaskCloseRequest = dsNotificationCnt.Tables[20].Rows.Count;
                }
                if (TaskCloseRequest > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(TaskCloseRequest) + ") Task Closure Inbox " + "\n";
                    Task2.InnerText = "Task Closure Request (" + Convert.ToString(TaskCloseRequest) + ")";
                    Task2.Title = "Task Closure Request (" + Convert.ToString(TaskCloseRequest) + ")";
                    liTaskCloseRequest.Visible = true;
                }

                #endregion

                #region  -- Table 21 Review Delayed Tasks Pending   --getDelayedTaskPendingCount --harshad

                if (dsNotificationCnt.Tables[21].Rows.Count > 0)
                {
                    ReviewDelayedTasks_Count = Convert.ToInt32(dsNotificationCnt.Tables[21].Rows[0]["DelayedTask"]);
                }
                if (ReviewDelayedTasks_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(ReviewDelayedTasks_Count) + ") Review Delayed Tasks in Inbox" + "\n";
                    AReviewDelayedTaskPending.InnerText = "Review Delayed Tasks (" + Convert.ToString(ReviewDelayedTasks_Count) + ")";
                    AReviewDelayedTaskPending.Title = "Review Delayed Tasks (" + Convert.ToString(ReviewDelayedTasks_Count) + ")";
                    LiReviewDelayedTaskPending.Visible = true;
                }

                #endregion

                #region  -- Table 22 KRA Request Pending   --GetKRAPendingCount --harshad

                if (dsNotificationCnt.Tables[22].Rows.Count > 0)
                {
                    Pending_KRA_Cnt = Convert.ToInt32(dsNotificationCnt.Tables[22].Rows[0]["KRA_Pending_Cnt"]);
                }
                if (Pending_KRA_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Pending_KRA_Cnt) + ") KRA Pending in Inbox" + "\n";
                    A_KRAPending.InnerText = " Pending KRA Request (" + Convert.ToString(Pending_KRA_Cnt) + ")";
                    A_KRAPending.Title = "Pending KRA Request (" + Convert.ToString(Pending_KRA_Cnt) + ")";
                    liKRAReviewer.Visible = true;
                }

                #endregion

                #region  -- Table 23 -- OD Application Pending   --InboxODApplicationCount --Harshad

                if (dsNotificationCnt.Tables[23].Rows.Count > 0)
                {
                    PendingODApplication = dsNotificationCnt.Tables[23].Rows.Count;
                }
                if (PendingODApplication > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(PendingODApplication) + ") Pending OD Application Request " + "\n";
                    A_ODAPP.InnerText = "Pending OD Application Request (" + Convert.ToString(PendingODApplication) + ")";
                    A_ODAPP.Title = "Pending OD Application Request (" + Convert.ToString(PendingODApplication) + ")";
                    liODAPP.Visible = true;
                }

                #endregion

                #region  -- Table 24 -- Appointment letter Acceptance Approval   --Inbox_Mode_PendingRecord --Harshad

                if (dsNotificationCnt.Tables[24].Rows.Count > 0)
                {
                    Acceptance_APP_Cnt = dsNotificationCnt.Tables[24].Rows.Count;
                }
                if (Acceptance_APP_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Acceptance_APP_Cnt) + ") Appointment letter Acceptance Approval in Inbox" + "\n";
                    AAppLatterM.InnerText = "Appointment letter Acceptance Approval (" + Convert.ToString(Acceptance_APP_Cnt) + ")";
                    AAppLatterM.Title = "Appointment letter Acceptance Approval (" + Convert.ToString(Acceptance_APP_Cnt) + ")";
                    liAPPApproval.Visible = true;
                }

                #endregion

                #region  -- Table 25 -- Appointment Letter Acceptance   --Inbox_APP_Acceptance --Harshad

                if (dsNotificationCnt.Tables[25].Rows.Count > 0)
                {
                    Acceptance_Cnt = dsNotificationCnt.Tables[25].Rows.Count;
                }
                if (Acceptance_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Acceptance_Cnt) + ") Appointment Letter Acceptance in Inbox" + "\n";
                    AAcceptance.InnerText = "Appointment Letter Acceptance (" + Convert.ToString(Acceptance_Cnt) + ")";
                    AAcceptance.Title = "Appointment Letter Acceptance (" + Convert.ToString(Acceptance_Cnt) + ")";
                    liAPPAccept.Visible = true;
                }

                #endregion


                #region  -- Table 26 -- Salary Status Update   --GetUpdatSalaryStatusCount --Harshad

                if (dsNotificationCnt.Tables[26].Rows.Count > 0)
                {
                    Salary_Status_Count = dsNotificationCnt.Tables[26].Rows.Count;
                }
                if (Salary_Status_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Salary_Status_Count) + ") Salary Status Update" + "\n";
                    ASalaryStatusUpdate.InnerText = "Salary Status Update (" + Convert.ToString(Salary_Status_Count) + ")";
                    ASalaryStatusUpdate.Title = "Salary Status Update (" + Convert.ToString(Salary_Status_Count) + ")";
                    liSalStatusUpdate.Visible = true;
                }

                #endregion

                #region  -- Table 27 -- Payment Request Approvals   --GetPaymentRequestPendingCount --Harshad

                if (dsNotificationCnt.Tables[27].Rows.Count > 0)
                {
                    Payment_App_Count = Convert.ToInt32(dsNotificationCnt.Tables[27].Rows[0]["Paymentcnt"]);
                }
                if (Payment_App_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_App_Count) + ") Payment Request Approvals in Inbox" + "\n";
                    APayApp.InnerText = "Payment Request Approvals (" + Convert.ToString(Payment_App_Count) + ")";
                    APayApp.Title = "Payment Request Approvals (" + Convert.ToString(Payment_App_Count) + ")";
                    liVendorPayApp.Visible = true;
                }

                #endregion

                #region  -- Table 28 -- Payment Partial Request Approvals   --GetPartialPaymentPendingCount --Harshad

                if (dsNotificationCnt.Tables[28].Rows.Count > 0)
                {
                    Payment_Partial_Count = Convert.ToInt32(dsNotificationCnt.Tables[28].Rows[0]["Paymentcnt"]);
                }
                if (Payment_Partial_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Partial_Count) + ") Partial Payment Requests in Inbox" + "\n";
                    APayPartial.InnerText = "Partial Payment Requests (" + Convert.ToString(Payment_Partial_Count) + ")";
                    APayPartial.Title = "Partial Payment Requests (" + Convert.ToString(Payment_Partial_Count) + ")";
                    liVendorPayPartial.Visible = true;
                }

                #endregion

                #region  -- Table 29 -- Payment Request Correction   --GetPaymentCorrectionCount --Harshad

                if (dsNotificationCnt.Tables[29].Rows.Count > 0)
                {
                    Payment_Corr_Count = Convert.ToInt32(dsNotificationCnt.Tables[29].Rows[0]["Paymentcnt"]);
                }
                if (Payment_Corr_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Payment_Corr_Count) + ") Payment Request Correction in Inbox" + "\n";
                    APayCorr.InnerText = "Payment Request Correction (" + Convert.ToString(Payment_Corr_Count) + ")";
                    APayCorr.Title = "Payment Request Correction (" + Convert.ToString(Payment_Corr_Count) + ")";
                    liVendorPayCorr.Visible = true;
                }

                #endregion

                #region  -- Table 30 -- Invoice Approvals   -- getMngInvoiceReqstCount --Harshad

                if (dsNotificationCnt.Tables[30].Rows.Count > 0)
                {
                    Invoice_App_Count = Convert.ToInt32(dsNotificationCnt.Tables[30].Rows[0]["NoofInvoices"]);
                }
                if (Invoice_App_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Invoice_App_Count) + ") Invoice Approvals in Inbox" + "\n";
                    AInvoiceApp.InnerText = "Invoice Approvals (" + Convert.ToString(Invoice_App_Count) + ")";
                    AInvoiceApp.Title = "Invoice Approvals (" + Convert.ToString(Invoice_App_Count) + ")";
                    liVendorInvoiceApp.Visible = true;
                }

                #endregion


                #region  -- Table 31 -- Batch Approvals   --getMng_PendingBatchCount --Harshad

                if (dsNotificationCnt.Tables[31].Rows.Count > 0)
                {
                    Batch_App_Count = dsNotificationCnt.Tables[31].Rows.Count;
                }
                if (Batch_App_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Batch_App_Count) + ") Batch Approvals in Inbox" + "\n";
                    ABatchApp.InnerText = "Batch Approvals (" + Convert.ToString(Batch_App_Count) + ")";
                    ABatchApp.Title = "Batch Approvals (" + Convert.ToString(Batch_App_Count) + ")";
                    liVendorBatch.Visible = true;
                }

                #endregion

                #region  -- Table 32 -- Pending PO/ WO Approvals   --NoofPOWOs --Harshad

                if (dsNotificationCnt.Tables[32].Rows.Count > 0)
                {
                    POWO_APP_Count = Convert.ToInt32(dsNotificationCnt.Tables[32].Rows[0]["NoofPOWOs"]);
                }
                if (POWO_APP_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(POWO_APP_Count) + ") Pending PO/ WO Approvals in Inbox" + "\n";
                    APOWOApp.InnerText = "Pending PO/ WO Approvals (" + Convert.ToString(POWO_APP_Count) + ")";
                    APOWOApp.Title = "Pending PO/ WO Approvals (" + Convert.ToString(POWO_APP_Count) + ")";
                    liVendorPOWOApp.Visible = true;
                }

                #endregion

                #region -- Table 33 -- Employee Retention   --CheckAccountDept --Harshad

                if (dsNotificationCnt.Tables[33].Rows.Count > 0)
                {
                    Retention_M_Cnt = dsNotificationCnt.Tables[33].Rows.Count;
                }
                if (Retention_M_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Retention_M_Cnt) + ") Employee Retention in Inbox" + "\n";
                    ARetentionAcc.InnerText = "Employee Retention (" + Convert.ToString(Retention_M_Cnt) + ")";
                    ARetentionAcc.Title = "Employee Retention (" + Convert.ToString(Retention_M_Cnt) + ")";
                    liRetentionAcc.Visible = true;
                }
                #endregion

                #region -- Table 34 -- Verify Candidate Data   --getCountIsCandidateApprover --Harshad

                if (dsNotificationCnt.Tables[34].Rows.Count > 0)
                {
                    CandidateRequestSubmited_Count = dsNotificationCnt.Tables[34].Rows.Count;
                }
                if (CandidateRequestSubmited_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(CandidateRequestSubmited_Count) + ") Verify Candidate Data" + "\n";
                    APendingCandidateDetailApprove.InnerText = "Verify Candidate Data (" + Convert.ToString(CandidateRequestSubmited_Count) + ")";
                    APendingCandidateDetailApprove.Title = "Verify Candidate Data (" + Convert.ToString(CandidateRequestSubmited_Count) + ")";
                    LiPendingCandidateDetailApprove.Visible = true;
                }
                #endregion

                #region -- Table 35 -- Pending Employee CV Review   --EmployeeCVReviewInbox --Harshad

                if (dsNotificationCnt.Tables[35].Rows.Count > 0)
                {
                    PendingCVReviewInboxCount = dsNotificationCnt.Tables[35].Rows.Count;
                }
                if (PendingCVReviewInboxCount > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(PendingCVReviewInboxCount) + ") Pending Employee CV Review" + "\n";
                    ACVReviewInbox.InnerText = "Pending Employee CV Review (" + Convert.ToString(PendingCVReviewInboxCount) + ")";
                    ACVReviewInbox.Title = "Pending Employee CV Review (" + Convert.ToString(PendingCVReviewInboxCount) + ")";
                    LiCVReviewInbox.Visible = true;
                }

                #endregion

                #region -- Table 36 -- Pending Employee Transfer Request   -- ETRInbox --Harshad

                if (dsNotificationCnt.Tables[36].Rows.Count > 0)
                {
                    ETR_Count = dsNotificationCnt.Tables[36].Rows.Count;
                }
                if (ETR_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(ETR_Count) + ") Pending Employee Transfer Request" + "\n";
                    AETRInbox.InnerText = "Pending Employee Transfer Request (" + Convert.ToString(ETR_Count) + ")";
                    AETRInbox.Title = "Pending Employee Transfer Request (" + Convert.ToString(ETR_Count) + ")";
                    LiETRInbox.Visible = true;
                }
                #endregion

                #region -- Table 37 -- Advance Payment Approval   --Check_Advance_Pay_Count --Harshad

                if (dsNotificationCnt.Tables[37].Rows.Count > 0)
                {
                    Adv_Pay_Cnt = Convert.ToInt32(dsNotificationCnt.Tables[37].Rows[0]["Paymentcnt"]);
                }
                if (Adv_Pay_Cnt > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Adv_Pay_Cnt) + ") Advance Payment Approval in Inbox" + "\n";
                    AAdvPay.InnerText = "Advance Payment Approval (" + Convert.ToString(Adv_Pay_Cnt) + ")";
                    AAdvPay.Title = "Advance Payment Approval (" + Convert.ToString(Adv_Pay_Cnt) + ")";
                    liAdvPayAPP.Visible = true;
                }
                #endregion

                #region -- Table 38 -- ABAP Object Completion   -- getABAPObjectCompletionCount --Harshad

                if (dsNotificationCnt.Tables[38].Rows.Count > 0)
                {
                    ABAPObjectCompletion_Count = Convert.ToInt32(dsNotificationCnt.Tables[38].Rows[0]["Countt"]);
                }
                if (ABAPObjectCompletion_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(ABAPObjectCompletion_Count) + ") ABAP Object Completion in Inbox" + "\n";
                    AABAPObjectCompletion.InnerText = "ABAP Object Completion (" + Convert.ToString(ABAPObjectCompletion_Count) + ")";
                    AABAPObjectCompletion.Title = "ABAP Object Completion (" + Convert.ToString(ABAPObjectCompletion_Count) + ")";
                    LiABAPObjectCompletion.Visible = true;
                }
                #endregion


                #region -- Table 39 -- Employee Mediclaim Data   --getEmployeeMediclaimDataCount --Harshad

                if (dsNotificationCnt.Tables[39].Rows.Count > 0)
                {
                    EmployeeMediclaim_Data_Count = Convert.ToInt32(dsNotificationCnt.Tables[39].Rows[0]["Countt"]);
                }
                if (EmployeeMediclaim_Data_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(EmployeeMediclaim_Data_Count) + ") Employee Mediclaim Data in Inbox" + "\n";
                    AEmployeeMediclaimData.InnerText = "Employee Mediclaim Data (" + Convert.ToString(EmployeeMediclaim_Data_Count) + ")";
                    AEmployeeMediclaimData.Title = "Employee Mediclaim Data (" + Convert.ToString(EmployeeMediclaim_Data_Count) + ")";
                    liEmployeeMediclaimData.Visible = true;
                }
                #endregion


                #region -- Table 40 -- Pending KRA Acceptance   --get_KRA_NotAccepted_Count --Harshad

                if (dsNotificationCnt.Tables[40].Rows.Count > 0)
                {
                    iKRANotAccept_Cnt = Convert.ToInt32(dsNotificationCnt.Tables[40].Rows[0]["KRANotAccept_Cnt"]);
                }
                if (iKRANotAccept_Cnt > 0)
                {
                    AKRANotAccepted.InnerText = "Pending KRA Acceptance (" + Convert.ToString(iKRANotAccept_Cnt) + ")";
                    AKRANotAccepted.Title = "Pending KRA Acceptance (" + Convert.ToString(iKRANotAccept_Cnt) + ")";
                    liKRANotAccepted.Visible = true;
                }

                #endregion 


                #region -- Table 41 -- Task Due Date Change Request   --GetTaskDueDateRequestCount --harshad

                if (dsNotificationCnt.Tables[41].Rows.Count > 0)
                {
                    TaskDueDateChange = dsNotificationCnt.Tables[41].Rows.Count;
                }
                if (TaskDueDateChange > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(TaskDueDateChange) + ") Due Date Change Request Inbox " + "\n";
                    Task3.InnerText = "Task Due Date Change Request (" + Convert.ToString(TaskDueDateChange) + ")";
                    Task3.Title = "Task Due Date Change Request (" + Convert.ToString(TaskDueDateChange) + ")";
                    liTaskDueDueDateRequest.Visible = true;
                }

                #endregion

                #region -- Table 42 -- CTC Exception Approval   --Inbox_CTC_ExceptionAPP --harshad

                if (dsNotificationCnt.Tables[42].Rows.Count > 0)
                {
                    ExceptionAPP = dsNotificationCnt.Tables[42].Rows.Count;
                }
                if (ExceptionAPP > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(ExceptionAPP) + ") CTC Exception Approval in Inbox" + "\n";
                    ACTCException.InnerText = "CTC Exception Approval (" + Convert.ToString(ExceptionAPP) + ")";
                    ACTCException.Title = "CTC Exception Approval (" + Convert.ToString(ExceptionAPP) + ")";
                    liExceptionAPP.Visible = true;
                }

                #endregion


                #region -- Table 43 -- Due Payment Request   --check_LoginEmployee_InvoiceCreateInboxpaymentReq --harshad

                if (dsNotificationCnt.Tables[43].Rows.Count > 0)
                {
                    Payreq_APP_Count = dsNotificationCnt.Tables[43].Rows.Count;
                }
                if (Payreq_APP_Count > 0)
                {
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(Payreq_APP_Count) + ") Due Payment Request in Inbox" + "\n";
                    ADuePaymentRequest.InnerText = "Due Payment Request (" + Convert.ToString(Payreq_APP_Count) + ")";
                    ADuePaymentRequest.Title = "Due Payment Request (" + Convert.ToString(Payreq_APP_Count) + ")";
                    LiVendorPayRequestCre.Visible = true;
                }

                #endregion


                #region  -- Table 44 -- Employee Update Photo   -- employeephotoupdate --harshad

                if (dsNotificationCnt.Tables[44].Rows.Count > 0)
                {
                    iUpdatePhotoCnt = Convert.ToInt32(dsNotificationCnt.Tables[44].Rows[0]["pendingCount"]);

                }
                if (iUpdatePhotoCnt > 0)
                {
                    employeephoto.InnerText = "Employee Update Photo (" + Convert.ToString(iUpdatePhotoCnt) + ")";
                    employeephoto.Title = "Employee Update Photo (" + Convert.ToString(iUpdatePhotoCnt) + ")";
                    liphoto.Visible = true;
                    employeephoto.Visible = true;
                }

                #endregion 
                


            }

            iTotalPendingCnt = Leave_Cnt + Mobile_Cnt + Fuel_Cnt + Payment_Cnt + Trvl_Exp_Cnt + Service_Request_Count + Timesheet_Count + Recruit_Req_Cnt + Recruiter_Cnt + Screener_Cnt + ScheduleInt_cnt + RescheduleInt_cnt + OfferApproval_cnt + Interview_cnt + Cust_Escalation_Count + EmpModerator_Count + Resignation_Count + TeamExit_Count + Clearance_Count + TaskPending + TaskCloseRequest + ReviewDelayedTasks_Count + Pending_KRA_Cnt + PendingODApplication + Acceptance_APP_Cnt + Acceptance_Cnt + Salary_Status_Count + Payment_App_Count + Payment_Partial_Count + Payment_Corr_Count + Invoice_App_Count + Batch_App_Count + POWO_APP_Count + Retention_M_Cnt + CandidateRequestSubmited_Count + PendingCVReviewInboxCount + ETR_Count + Adv_Pay_Cnt + ABAPObjectCompletion_Count + EmployeeMediclaim_Data_Count + iKRANotAccept_Cnt + TaskDueDateChange + ExceptionAPP + Payreq_APP_Count + iUpdatePhotoCnt;



        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
        }

        return iTotalPendingCnt;
    }

    // Added by Ajinkya's Code
    private int getABAPObjectPlanStageWiseCount(string emp_code)
    {
        int icnt = 0;
        try
        {

            Int32 objABAPPlanSubmitCnt = 0, objABAPPlanApprovalCnt = 0, objABAPPlanRGSPendingCnt = 0, objABAPPlanFSPendingCnt = 0, objABAPPlanFSAcceptanceCnt = 0, objABAPPlanABAPDevPendingCnt = 0,
                    objABAPPlanHBTPendingCnt = 0, objABAPPlanCTMPendingCnt = 0, objABAPPlanCTMApprovalCnt = 0, objABAPPlanUATCnt = 0, objABAPPlanGoLivePendingCnt = 0, objABAPPlanRGSApprovalCnt = 0;

            DataSet dtstagewise = new DataSet();
            string popup = "";
            SqlParameter[] spars = new SqlParameter[1];
            spars[0] = new SqlParameter("@emp_code", SqlDbType.VarChar);
            spars[0].Value = emp_code;
            dtstagewise = spm.getDatasetList(spars, "SP_Notification_ABAP");
            if (dtstagewise != null)
            {
                #region  -- Table 0 -- ABAP Plan Submit Count
                if (dtstagewise.Tables[0].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[0].Rows[0]["ABAPPlanSubmitCount"]) > 0)
                {
                    objABAPPlanSubmitCnt = Convert.ToInt32(dtstagewise.Tables[0].Rows[0]["ABAPPlanSubmitCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanSubmitCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanSubmitCnt.InnerText = "Submit ABAP Plan (" + Convert.ToString(objABAPPlanSubmitCnt) + ")";
                    A_ABAPPlanSubmitCnt.Title = "Submit ABAP Plan (" + Convert.ToString(objABAPPlanSubmitCnt) + ")";
                    liABAPPlanSubmitCnt.Visible = true;
                }
                #endregion

                #region  -- Table 1 -- ABAP Plan Approval Count
                if (dtstagewise.Tables[1].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[1].Rows[0]["ABAPPlanApprovalCount"]) > 0)
                {
                    objABAPPlanApprovalCnt = Convert.ToInt32(dtstagewise.Tables[1].Rows[0]["ABAPPlanApprovalCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanApprovalCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanApprovalCnt.InnerText = "ABAP Plan Approval (" + Convert.ToString(objABAPPlanApprovalCnt) + ")";
                    A_ABAPPlanApprovalCnt.Title = "ABAP Plan Approval (" + Convert.ToString(objABAPPlanApprovalCnt) + ")";
                    liABAPPlanApprovalCnt.Visible = true;
                }
                #endregion


                #region  -- Table 2 -- RGS Pending Count
                if (dtstagewise.Tables[2].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[2].Rows[0]["RGSPendingCount"]) > 0)
                {
                    objABAPPlanRGSPendingCnt = Convert.ToInt32(dtstagewise.Tables[2].Rows[0]["RGSPendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanRGSPendingCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanRGSPendingCnt.InnerText = "RGS Pending (" + Convert.ToString(objABAPPlanRGSPendingCnt) + ")";
                    A_ABAPPlanRGSPendingCnt.Title = "RGS Pending (" + Convert.ToString(objABAPPlanRGSPendingCnt) + ")";
                    liABAPPlanRGSPendingCnt.Visible = true;
                }
                #endregion


                #region  -- Table 3 -- FS Pending Count
                if (dtstagewise.Tables[3].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[3].Rows[0]["FSPendingCount"]) > 0)
                {
                    objABAPPlanFSPendingCnt = Convert.ToInt32(dtstagewise.Tables[3].Rows[0]["FSPendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanFSPendingCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanFSPendingCnt.InnerText = "FS Pending (" + Convert.ToString(objABAPPlanFSPendingCnt) + ")";
                    A_ABAPPlanFSPendingCnt.Title = "FS Pending (" + Convert.ToString(objABAPPlanFSPendingCnt) + ")";
                    liABAPPlanFSPendingCnt.Visible = true;
                }
                #endregion


                #region  -- Table 4 -- Functional Acceptance Pending Count
                if (dtstagewise.Tables[4].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[4].Rows[0]["FunctionalAcceptancePendingCount"]) > 0)
                {
                    objABAPPlanFSAcceptanceCnt = Convert.ToInt32(dtstagewise.Tables[4].Rows[0]["FunctionalAcceptancePendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanFSAcceptanceCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanFSAcceptanceCnt.InnerText = "FS Acceptance Pending (" + Convert.ToString(objABAPPlanFSAcceptanceCnt) + ")";
                    A_ABAPPlanFSAcceptanceCnt.Title = "FS Acceptance Pending (" + Convert.ToString(objABAPPlanFSAcceptanceCnt) + ")";
                    liABAPPlanFSAcceptanceCnt.Visible = true;
                }
                #endregion


                #region  -- Table 5 -- ABAP Dev Pending Count
                if (dtstagewise.Tables[5].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[5].Rows[0]["ABAPDevPendingCount"]) > 0)
                {
                    objABAPPlanABAPDevPendingCnt = Convert.ToInt32(dtstagewise.Tables[5].Rows[0]["ABAPDevPendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanABAPDevPendingCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanABAPDevPendingCnt.InnerText = "ABAP Development Pending (" + Convert.ToString(objABAPPlanABAPDevPendingCnt) + ")";
                    A_ABAPPlanABAPDevPendingCnt.Title = "ABAP Development Pending (" + Convert.ToString(objABAPPlanABAPDevPendingCnt) + ")";
                    liABAPPlanABAPDevPendingCnt.Visible = true;
                }
                #endregion


                #region  -- Table 6 -- HBTP ending Count
                if (dtstagewise.Tables[6].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[6].Rows[0]["HBTPendingCount"]) > 0)
                {
                    objABAPPlanHBTPendingCnt = Convert.ToInt32(dtstagewise.Tables[6].Rows[0]["HBTPendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanHBTPendingCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanHBTPendingCnt.InnerText = "HBT Testing Pending (" + Convert.ToString(objABAPPlanHBTPendingCnt) + ")";
                    A_ABAPPlanHBTPendingCnt.Title = "HBT Testing Pending (" + Convert.ToString(objABAPPlanHBTPendingCnt) + ")";
                    liABAPPlanHBTPendingCnt.Visible = true;
                }
                #endregion


                #region  -- Table 7 -- CTM Pending Count
                if (dtstagewise.Tables[7].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[7].Rows[0]["CTMPendingCount"]) > 0)
                {
                    objABAPPlanCTMPendingCnt = Convert.ToInt32(dtstagewise.Tables[7].Rows[0]["CTMPendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanCTMPendingCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanCTMPendingCnt.InnerText = "CTM Testing Pending (" + Convert.ToString(objABAPPlanCTMPendingCnt) + ")";
                    A_ABAPPlanCTMPendingCnt.Title = "CTM Testing Pending (" + Convert.ToString(objABAPPlanCTMPendingCnt) + ")";
                    liABAPPlanCTMPendingCnt.Visible = true;
                }
                #endregion


                #region  -- Table 8 -- CTM Approval Pending Count
                if (dtstagewise.Tables[8].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[8].Rows[0]["CTMApprovalCount"]) > 0)
                {
                    objABAPPlanCTMApprovalCnt = Convert.ToInt32(dtstagewise.Tables[8].Rows[0]["CTMApprovalCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanCTMApprovalCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanCTMApprovalCnt.InnerText = "CTM Testing Approval (" + Convert.ToString(objABAPPlanCTMApprovalCnt) + ")";
                    A_ABAPPlanCTMApprovalCnt.Title = "CTM Testing Approval (" + Convert.ToString(objABAPPlanCTMApprovalCnt) + ")";
                    liABAPPlanCTMApprovalCnt.Visible = true;
                }
                #endregion

                #region  -- Table 9 -- UAT Pending Count
                if (dtstagewise.Tables[9].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[9].Rows[0]["UATPendingCount"]) > 0)
                {
                    objABAPPlanUATCnt = Convert.ToInt32(dtstagewise.Tables[9].Rows[0]["UATPendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanUATCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanUATPendingCnt.InnerText = "UAT Pending (" + Convert.ToString(objABAPPlanUATCnt) + ")";
                    A_ABAPPlanUATPendingCnt.Title = "UAT Pending (" + Convert.ToString(objABAPPlanUATCnt) + ")";
                    liABAPPlanUATPendingCnt.Visible = true;
                }
                #endregion

                #region  -- Table 10 -- ABAP Plan Submit Count
                if (dtstagewise.Tables[10].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[10].Rows[0]["GoLivePendingCount"]) > 0)
                {
                    objABAPPlanGoLivePendingCnt = Convert.ToInt32(dtstagewise.Tables[10].Rows[0]["GoLivePendingCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanGoLivePendingCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanGoLivePendingCnt.InnerText = "Go Live Pending (" + Convert.ToString(objABAPPlanGoLivePendingCnt) + ")";
                    A_ABAPPlanGoLivePendingCnt.Title = "Go Live Pending (" + Convert.ToString(objABAPPlanGoLivePendingCnt) + ")";
                    liABAPPlanGoLivePendingCnt.Visible = true;
                }
                #endregion
                #region  -- Table 11 -- RGS Approval Count
                if (dtstagewise.Tables[11].Rows.Count > 0 && Convert.ToInt32(dtstagewise.Tables[11].Rows[0]["RGSApprovalCount"]) > 0)
                {
                    objABAPPlanRGSApprovalCnt = Convert.ToInt32(dtstagewise.Tables[11].Rows[0]["RGSApprovalCount"]);
                    popup = popup + Environment.NewLine + "(" + Convert.ToString(objABAPPlanRGSApprovalCnt) + ") Review Delayed Tasks in Inbox" + "\n";
                    A_ABAPPlanRGSApprovalCnt.InnerText = "RGS Approval (" + Convert.ToString(objABAPPlanRGSApprovalCnt) + ")";
                    A_ABAPPlanRGSApprovalCnt.Title = "RGS Approval (" + Convert.ToString(objABAPPlanRGSApprovalCnt) + ")";
                    liABAPPlanRGSApprovalCnt.Visible = true;
                }
                #endregion
            }



            icnt = objABAPPlanSubmitCnt + objABAPPlanApprovalCnt + objABAPPlanRGSPendingCnt + objABAPPlanFSPendingCnt + objABAPPlanFSAcceptanceCnt + objABAPPlanABAPDevPendingCnt +
                objABAPPlanHBTPendingCnt + objABAPPlanCTMPendingCnt + objABAPPlanCTMApprovalCnt + objABAPPlanUATCnt + objABAPPlanGoLivePendingCnt + objABAPPlanRGSApprovalCnt;

          //  btnShowMsg.Text = "Notifications (" + Tot_Cnt + ")";

        }
        catch (Exception ex)
        {

        }

        return icnt;
    }
}