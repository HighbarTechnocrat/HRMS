<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="notifications.aspx.cs" Inherits="notifications" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/TravelCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/traindex.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }

       li 
       {
           padding-bottom:8px !important;
       }

        #MainContent_aEFCSRHOD:link, #MainContent_aEFCSRHOD:visited,
        #MainContent_ACustomerServiceRequest:link, #MainContent_ACustomerServiceRequest:visited,
        #MainContent_aCSPending:link, #MainContent_aCSPending:visited,
        #MainContent_aL:link, #MainContent_aL:visited,        
        #MainContent_aM:link, #MainContent_aM:visited, 
        #MainContent_aF:link, #MainContent_aF:visited, 
        #MainContent_aP:link, #MainContent_aP:visited, 
        #MainContent_aE:link, #MainContent_aE:visited, 
        #MainContent_aEF:link, #MainContent_aEF:visited, 
        #MainContent_aEF1:link, #MainContent_aEF1:visited,    
        #MainContent_aEF2:link, #MainContent_aEF2:visited, 
        #MainContent_aEF3:link, #MainContent_aEF3:visited, 
        #MainContent_aIT1:link, #MainContent_aIT1:visited, 
        #MainContent_ARECRQ:link, #MainContent_ARECRQ:visited, 
        #MainContent_ARecruiter:link, #MainContent_ARecruiter:visited, 
        #MainContent_AScreener:link, #MainContent_AScreener:visited, 
        #MainContent_AScheduleInt:link, #MainContent_AScheduleInt:visited,     
        #MainContent_ARescheduleInt:link, #MainContent_ARescheduleInt:visited,     
        #MainContent_AInterviews:link, #MainContent_AInterviews:visited,     
        #MainContent_AOffer:link, #MainContent_AOffer:visited,
        #MainContent_AAcceptance:link, #MainContent_AAcceptance:visited,
        #MainContent_ACustEcla:link, #MainContent_ACustEcla:visited,
        #MainContent_ACustconfir:link, #MainContent_ACustconfir:visited,
        #MainContent_AModerator:link, #MainContent_AModerator:visited,
        #MainContent_AResig:link, #MainContent_AResig:visited,
        #MainContent_ATeamExit:link, #MainContent_ATeamExit:visited,
        #MainContent_AClearance:link, #MainContent_AClearance:visited,
        #MainContent_Task1:link, #MainContent_Task1:visited,
        #MainContent_Task2:link, #MainContent_Task2:visited,
        #MainContent_Task3:link, #MainContent_Task3:visited,
        #MainContent_A_KRAPending:link, #MainContent_A_KRAPending:visited,
        #MainContent_A_ODAPP:link, #MainContent_A_ODAPP:visited,
        #MainContent_AAppLatterM:link, #MainContent_AAppLatterM:visited,
        #MainContent_ACTCException:link, #MainContent_ACTCException:visited,
        #MainContent_ASalaryStatusUpdate:link, #MainContent_ASalaryStatusUpdate:visited,
        #MainContent_APOWOApp:link, #MainContent_APOWOApp:visited,
        #MainContent_AInvoiceApp:link, #MainContent_AInvoiceApp:visited,
        #MainContent_AInvoiceApp:link, #MainContent_AInvoiceApp:visited,
        #MainContent_APayApp:link, #MainContent_APayApp:visited,
        #MainContent_APayCorr:link, #MainContent_APayCorr:visited,
        #MainContent_APayPartial:link, #MainContent_APayPartial:visited,
        #MainContent_ABatchApp:link, #MainContent_ABatchApp:visited,
        #MainContent_ADuePaymentRequest:link, #MainContent_ADuePaymentRequest:visited,
        #MainContent_AReviewDelayedTaskPending:link, #MainContent_AReviewDelayedTaskPending:visited,
        #MainContent_APendingCandidateDetailApprove:link, #MainContentAPendingCandidateDetailApprove:visited,
        #MainContent_ARetentionAcc:link, #MainContent_ARetentionAcc:visited,        
        #MainContent_APendingCVUpdate:link, #MainContent_APendingCVUpdate:visited,
        #MainContent_ACVReviewInbox:link, #MainContent_ACVReviewInbox:visited,
        #MainContent_AETRInbox:link, #MainContent_AETRInbox:visited,
        #MainContent_AAdvPay:link, #MainContent_AAdvPay:visited,
        #MainContent_A_ApprPerformanceReviewPending:link, #MainContent_A_ApprPerformanceReviewPending:visited,
        #MainContent_A_ApprPerformanceRecommendationPending:link, #MainContent_A_ApprPerformanceRecommendationPending:visited,
        #MainContent_AABAPObjectCompletion:link, #MainContent_AABAPObjectCompletion:visited,
        #MainContent_AEmployeeMediclaimData:link, #MainContent_AEmployeeMediclaimData:visited,        
        #MainContent_aEFCSR:link, #MainContent_aEFCSR:visited,
        #MainContent_AKRANotAccepted:link, #MainContent_AKRANotAccepted:visited        
        {
            background-color: #C7D3D4;
            color: #603F83 !important;
            border-radius: 10px;
            text-align: center;
            padding:15px !important;
            text-decoration: none;
            display: inline-block;
            width: 70% !important;
        }

        #MainContent_aEFCSRHOD:hover, #MainContent_aEFCSRHOD:active,
        #MainContent_aEFCSR:hover, #MainContent_aEFCSR:active,
        #MainContent_aCSPending:hover, #MainContent_aCSPending:active,
        #MainContent_aL:hover, #MainContent_aL:active,  
        #MainContent_aM:hover, #MainContent_aM:active,      
        #MainContent_aF:hover, #MainContent_aF:active, 
        #MainContent_aP:hover, #MainContent_aP:active, 
        #MainContent_aE:hover, #MainContent_aE:active, 
        #MainContent_aEF:hover, #MainContent_aEF:active, 
        #MainContent_aEF1:hover, #MainContent_aEF1:active, 
        #MainContent_aEF2:hover, #MainContent_aEF2:active, 
        #MainContent_aEF3:hover, #MainContent_aEF3:active, 
        #MainContent_aIT1:hover, #MainContent_aIT1:active, 
        #MainContent_ARECRQ:hover, #MainContent_ARECRQ:active, 
        #MainContent_ARecruiter:hover, #MainContent_ARecruiter:active, 
        #MainContent_AScreener:hover, #MainContent_AScreener:active, 
        #MainContent_AScheduleInt:hover, #MainContent_AScheduleInt:active,     
        #MainContent_ARescheduleInt:hover, #MainContent_ARescheduleInt:active,     
        #MainContent_AInterviews:hover, #MainContent_AInterviews:active,     
        #MainContent_AOffer:hover, #MainContent_AOffer:active, 
        #MainContent_ACustEcla:hover, #MainContent_ACustEcla:active,
        #MainContent_ACustconfir:hover, #MainContent_ACustconfir:active,
        #MainContent_AModerator:hover, #MainContent_AModerator:active,
        #MainContent_AResig:hover, #MainContent_AResig:active,
        #MainContent_ATeamExit:hover, #MainContent_ATeamExit:active,
        #MainContent_AClearance:hover, #MainContent_AClearance:active,
        #MainContent_Task1:hover, #MainContent_Task1:active,
        #MainContent_Task2:hover, #MainContent_Task2:active,
        #MainContent_Task3:hover, #MainContent_Task3:active,
        #MainContent_A_KRAPending:hover, #MainContent_A_KRAPending:active,
        #MainContent_A_ODAPP:hover, #MainContent_A_ODAPP:active,
        #MainContent_AAppLatterM:hover, #MainContent_AAppLatterM:active,
        #MainContent_ACTCException:hover, #MainContent_ACTCException:active,
        #MainContent_ASalaryStatusUpdate:hover, #MainContent_ASalaryStatusUpdate:active,
        #MainContent_APOWOApp:hover, #MainContent_APOWOApp:active,
        #MainContent_AInvoiceApp:hover, #MainContent_AInvoiceApp:active,
        #MainContent_AInvoiceApp:hover, #MainContent_AInvoiceApp:active,
        #MainContent_APayApp:hover, #MainContent_APayApp:active,
        #MainContent_APayCorr:hover, #MainContent_APayCorr:active,
        #MainContent_APayPartial:hover, #MainContent_APayPartial:active,
        #MainContent_ABatchApp:hover, #MainContent_ABatchApp:active,
        #MainContent_ADuePaymentRequest:hover, #MainContent_ADuePaymentRequest:active,
        #MainContent_AReviewDelayedTaskPending:hover, #MainContent_AReviewDelayedTaskPending:active,
        #MainContent_APendingCandidateDetailApprove:hover, #MainContentAPendingCandidateDetailApprove:active,
        #MainContent_ARetentionAcc:hover, #MainContent_ARetentionAcc:active,        
        #MainContent_APendingCVUpdate:hover, #MainContent_APendingCVUpdate:active,
        #MainContent_ACVReviewInbox:hover, #MainContent_ACVReviewInbox:active,
        #MainContent_AETRInbox:hover, #MainContent_AETRInbox:active,
        #MainContent_AAdvPay:hover, #MainContent_AAdvPay:active,
        #MainContent_A_ApprPerformanceReviewPending:hover, #MainContent_A_ApprPerformanceReviewPending:active,
        #MainContent_A_ApprPerformanceRecommendationPending:hover, #MainContent_A_ApprPerformanceRecommendationPending:active,
        #MainContent_AABAPObjectCompletion:hover, #MainContent_AABAPObjectCompletion:active,
        #MainContent_AEmployeeMediclaimData:hover, #MainContent_AEmployeeMediclaimData:active,
        #MainContent_ACustomerServiceRequest:hover, #MainContent_ACustomerServiceRequest:active,
        #MainContent_AAcceptance:hover, #MainContent_AAcceptance:active,
        #MainContent_AKRANotAccepted:hover, #MainContent_AKRANotAccepted:active
        {
            /*background-color: #603F83;*/
            background-color: #3D1956;
            color: #C7D3D4 !important;
            border-color: #3D1956;
            border-width: 2pt;
            border-style: inset;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Notifications"></asp:Label>
                        </span>
                    </div>
                    <div class="leavegrid">
                    </div>
                    <div class="editprofile" id="editform1" runat="server">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>

                            <ul>
                                <li id="LilblCusts_ServiceRequestHOD" runat="server" title="Pending Customer Service_Request" visible="false"><a href="http://localhost/hrms/procs/Custs_Service.aspx" runat="server" id="aEFCSRHOD" style="font-size: 12px !important;" title="Pending Customer Service_Request">Customer Service_Request</a></li>
                                <li id="liCustomerServiceRequest" runat="server" title="CustomerFIRST Service Request" visible="false"><a href="http://localhost/hrms/procs/Custs_InboxServiceRequest.aspx" runat="server" id="ACustomerServiceRequest" style="font-size: 12px !important;" title="CustomerFIRST Service Request">CustomerFIRST Service Request</a></li>
                                <li id="LilblCusts_ServiceRequest" runat="server" title="Pending Customer Service_Request" visible="false"><a href="http://localhost/hrms/procs/Custs_InboxServiceRequest.aspx" runat="server" id="aEFCSR" style="font-size: 12px !important;" title="Pending Customer Service_Request">Customer Service_Request</a></li>
                                <li id="liCustomerServiceRequest_CS" runat="server" title="CustomerFIRST Service Request" visible="false"><a href="http://localhost/hrms/procs/Custs_InboxPendingServiceRequest.aspx" runat="server" id="aCSPending" style="font-size: 12px !important;" title="CustomerFIRST Service Request">CustomerFIRST Service Request</a></li>


                                <li id="lilblMsg" runat="server" title="Leave" visible="false"><a href="http://localhost/hrms/procs/leaves.aspx" runat="server" id="aL" style="font-size: 12px !important;" title="Leave">Leave</a></li>
                                <li id="lilblMsg_Mob" runat="server" title="Mobile" visible="false"><a href="http://localhost/hrms/procs/Mobile.aspx" runat="server" id="aM" style="font-size: 12px !important;" title="Mobile">Mobile</a></li>
                                <li id="lilblMsg_Fuel" runat="server" title="Fuel" visible="false"><a href="http://localhost/hrms/procs/fuel.aspx" runat="server" id="aF" style="font-size: 12px !important;" title="Fuel">Fuel</a></li>
                                <li id="lilblMsg_Pay" runat="server" title="Payment" visible="false"><a href="http://localhost/hrms/procs/Voucher.aspx" runat="server" id="aP" style="font-size: 12px !important;" title="Payment">Payment</a></li>
                                <li id="lilblMsg_Travel" runat="server" title="Travel" visible="false"><a href="http://localhost/hrms/procs/travelindex.aspx" runat="server" id="aE" style="font-size: 12px !important;" title="Travel">Travel</a></li>
                                <li id="lilblServiceRequest" runat="server" title="EmployeeFIRST" visible="false"><a href="http://localhost/hrms/procs/InboxServiceRequest.aspx" runat="server" id="aEF" style="font-size: 12px !important;" title="EmployeeFIRST">EmployeeFIRST</a></li>
                                <li id="lilblCustomerFIRST" runat="server" title="Pending Customer Feedback Response" visible="false"><a href="http://localhost/hrms/procs/InboxCustomerFirst.aspx" runat="server" id="aEF1" style="font-size: 12px !important;" title="Pending Customer Feedback Response">Customer Feedback</a></li>
                                <li id="lilblAttedanceRegularization" runat="server" title="Pending Attedance Regularization" visible="false"><a href="http://localhost/hrms/procs/InboxAttend_Req.aspx" runat="server" id="aEF2" style="font-size: 12px !important;" title="Pending Attedance Regularization">Attedance Regularization</a></li>
                                <li id="lilblTimesheet" runat="server" title="Pending Timesheet" visible="false"><a href="http://localhost/hrms/procs/InboxTimesheet_Req.aspx" runat="server" id="aEF3" style="font-size: 12px !important;" title="Pending Timesheet">Timesheet</a></li>
                                <li id="lilblITAsset" runat="server" title="Pending ITInventory Response" visible="false"><a href="http://localhost/hrms/procs/InboxITAssetServiceRequest.aspx" runat="server" id="aIT1" style="font-size: 12px !important;" title="Pending IT Asset Request">IT-Inventory</a></li>
                                <li id="liRecruit_Req_APP" runat="server" title="Requisition Approval" visible="false"><a href="http://localhost/hrms/procs/Req_RequisitionIndex.aspx?itype=Pending" runat="server" id="ARECRQ" style="font-size: 12px !important;" title="Requisition Approval">Requisition Approval</a></li>
                                <li id="liRecruiter" runat="server" title="Recruiter Inbox" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterInbox.aspx?type=InRec" runat="server" id="ARecruiter" style="font-size: 12px !important;" title="Recruiter Inbox">Recruiter Inbox</a></li>
                                <li id="liScreener" runat="server" title="Screener Inbox" visible="false"><a href="http://localhost/hrms/procs/Rec_InterviewerInbox.aspx?type=InPInter" runat="server" id="AScreener" style="font-size: 12px !important;" title="Screener Inbox">Recruitment</a></li>
                                <li id="liScheduleInt" runat="server" title="Schedule Interviews" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterInbox.aspx?type=RECISL" runat="server" id="AScheduleInt" style="font-size: 12px !important;" title="Schedule Interviews">Schedule Interviews</a></li>
                                <li id="liRescheduleInt" runat="server" title="Reschedule Interviews" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterInbox.aspx?type=RECIRescedule" runat="server" id="ARescheduleInt" style="font-size: 12px !important;" title="Reschedule Interviews">Reschedule Interviews</a></li>
                                <li id="liInterviewr" runat="server" title="Interviewer Inbox" visible="false"><a href="http://localhost/hrms/procs/Rec_InterviewerInbox.aspx?type=InShPInter" runat="server" id="AInterviews" style="font-size: 12px !important;" title="Interviewer Inbox">Interviewer Inbox</a></li>
                                <li id="liOfferAPP" runat="server" title="Offer Approval" visible="false"><a href="http://localhost/hrms/procs/Req_Offer_Index.aspx?itype=Pending" runat="server" id="AOffer" style="font-size: 12px !important;" title="Offer Approval">Offer Approval</a></li>
                                <li id="liCustEscala" runat="server" title="CustomerFirst" visible="false"><a href="http://localhost/hrms/procs/InboxCustEscalation.aspx" runat="server" id="ACustEcla" style="font-size: 12px !important;" title="CustomerFirst">CustomerFirst</a></li>
                                <li id="liCustConfir" runat="server" title="CustomerFirst Pending Confirmation" visible="false"><a href="http://localhost/hrms/procs/InboxCustEscalationApp.aspx" runat="server" id="ACustconfir" style="font-size: 12px !important;" title="CustomerFirst Pending Confirmation">CustomerFirst Confirmation</a></li>
                                <li id="liEmpModerator" runat="server" title="Moderator Pending" visible="false"><a href="http://localhost/hrms/procs/Ref_Moderator_Index.aspx" runat="server" id="AModerator" style="font-size: 12px !important;" title="Moderator Pending">Moderator Pending</a></li>
                                <li id="liResignation" runat="server" title="Pending Resignation" visible="false"><a href="http://localhost/hrms/procs/InboxResignations.aspx" runat="server" id="AResig" style="font-size: 12px !important;" title="Pending Resignation">Pending Resignation</a></li>
                                <li id="liTeamExit" runat="server" title="Pending Team Exit Interview" visible="false"><a href="http://localhost/hrms/procs/ExitProcess_ExitInterviewList.aspx" runat="server" id="ATeamExit" style="font-size: 12px !important;" title="Pending Team Exit">Pending Team Exit</a></li>
                                <li id="liClearance" runat="server" title="Pending Clearance" visible="false"><a href="http://localhost/hrms/procs/ExitProcess_ClearanceInbox.aspx" runat="server" id="AClearance" style="font-size: 12px !important;" title="Pending Clearance">Pending Clearance</a></li>
                                <li id="liTaskPending" runat="server" title="Pending Task" visible="false"><a href="http://localhost/hrms/procs/InboxExecuter.aspx" runat="server" id="Task1" style="font-size: 12px !important;" title="Pending Task">Pending Task</a></li>
                                <li id="liTaskCloseRequest" runat="server" title="Pending Task Close Request" visible="false"><a href="http://localhost/hrms/procs/Task_Closure_Inbox.aspx" runat="server" id="Task2" style="font-size: 12px !important;" title="Pending Task Close Request">Pending Task Close Request</a></li>
                                <li id="liTaskDueDueDateRequest" runat="server" title="Pending Due Date Change Request" visible="false"><a href="http://localhost/hrms/procs/Task_DueDateChange_Inbox.aspx" runat="server" id="Task3" style="font-size: 12px !important;" title="Pending Due Date Change Request">Pending Due Date Change Request</a></li>
                                <li id="liKRAReviewer" runat="server" title="KRA Pending" visible="false"><a href="http://localhost/hrms/procs/KRA_Inbox.aspx" runat="server" id="A_KRAPending" style="font-size: 12px !important;" title="KRA Pending">KRA Pending</a></li>
                                <li id="liKRANotAccepted" runat="server" title="Pending KRA Acceptance" visible="false"><a href="http://localhost/hrms/procs/KRA_Create.aspx" runat="server" id="AKRANotAccepted" style="font-size: 12px !important;" title="Pending KRA Acceptance">Pending KRA Acceptance</a></li>
                                <li id="liODAPP" runat="server" title="Pending OD Application Request" visible="false"><a href="http://localhost/hrms/procs/InboxODApplication.aspx" runat="server" id="A_ODAPP" style="font-size: 12px !important;" title="Pending OD Application Request">Pending OD Application Request</a></li>
                                <li id="liAPPAccept" runat="server" title="Appointment Letter Acceptance" visible="false"><a href="http://localhost/hrms/procs/App_Latter_Acceptance.aspx" runat="server" id="AAcceptance" style="font-size: 12px !important;" title="Appointment Letter Acceptance">Appointment Letter Acceptance</a></li>
                                <li id="liAPPApproval" runat="server" title="Appointment letter Acceptance Approval" visible="false"><a href="http://localhost/hrms/procs/App_Latter_M_Index.aspx?Type=Pending" runat="server" id="AAppLatterM" style="font-size: 12px !important;" title="Appointment letter Acceptance Approval">Appointment letter Acceptance Approval</a></li>
                                <li id="liExceptionAPP" runat="server" title="CTC Exception Approval" visible="false"><a href="http://localhost/hrms/procs/Req_CTCIndex.aspx?itype=Pending" runat="server" id="ACTCException" style="font-size: 12px !important;" title="CTC Exception Approval">CTC Exception Approval</a></li>
                                <li id="liSalStatusUpdate" runat="server" title="Salary Status Update" visible="false"><a href="http://localhost/hrms/procs/SalaryNotUpdatedData.aspx" runat="server" id="ASalaryStatusUpdate" style="font-size: 12px !important;" title="Salary Status Update">Salary Status Update</a></li>
                                <li id="liVendorPOWOApp" runat="server" title="PO/ WO Request Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxPOWO.aspx" runat="server" id="APOWOApp" style="font-size: 12px !important;" title="PO/ WO Approvals">Pending PO/ WO Approvals</a></li>
                                <li id="liVendorInvoiceApp" runat="server" title="Payment Request Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_Inboxinvoice.aspx" runat="server" id="AInvoiceApp" style="font-size: 12px !important;" title="Invoice Approvals">Invoice Approvals</a></li>
                                <li id="liVendorPayApp" runat="server" title="Invoice Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxPaymentRequest.aspx" runat="server" id="APayApp" style="font-size: 12px !important;" title="Payment Request Approvals">Payment Request Approvals</a></li>
                                <li id="liVendorPayCorr" runat="server" title="Payment Request Correction" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxMyPaymentRequest.aspx" runat="server" id="APayCorr" style="font-size: 12px !important;" title="Payment Request Correction">Payment Request Correction</a></li>
                                <li id="liVendorPayPartial" runat="server" title="Partial Payment Requests" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxPartialPaymentRequest.aspx" runat="server" id="APayPartial" style="font-size: 12px !important;" title="Partial Payment Requests">Partial Payment Requests</a></li>
                                <li id="liVendorBatch" runat="server" title="Batch Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxBatchReq.aspx" runat="server" id="ABatchApp" style="font-size: 12px !important;" title="Batch Approvals">Batch Approvals</a></li>
                                <li id="LiVendorPayRequestCre" runat="server" title="Due Payment Request" visible="false"><a href="http://localhost/hrms/procs/VSCB_PaymentRequestAll.aspx" runat="server" id="ADuePaymentRequest" style="font-size: 12px !important;" title="Due Payment Request">Due Payment Request</a></li>
                                <li id="LiReviewDelayedTaskPending" runat="server" title="Review Delayed Tasks" visible="false"><a href="http://localhost/hrms/procs/ReviewDelayedTasks.aspx" runat="server" id="AReviewDelayedTaskPending" style="font-size: 12px !important;" title="Review Delayed Tasks">Review Delayed Tasks</a></li>
                                <li id="LiPendingCandidateDetailApprove" runat="server" title="Verify Candidate Data" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterCandidateInbox.aspx" runat="server" id="APendingCandidateDetailApprove" style="font-size: 12px !important;" title="Verify Candidate Data">Verify Candidate Data</a></li>
                                <li id="liRetentionAcc" runat="server" title="Employee Retention" visible="false"><a href="http://localhost/hrms/procs/ExitProcess_Mo_Index.aspx?Type=Pending" runat="server" id="ARetentionAcc" style="font-size: 12px !important;" title="Employee Retention">Employee Retention</a></li>
                                <li id="LiPendingCVUpdate" runat="server" title="Pending CV Update" visible="false"><a href="http://localhost/hrms/procs/EmployeeCV.aspx" runat="server" id="APendingCVUpdate" style="font-size: 12px !important;" title="Pending CV Update">Pending CV Update(1)</a></li>
                                <li id="LiCVReviewInbox" runat="server" title="Pending CV Review Inbox" visible="false"><a href="http://localhost/hrms/procs/EmployeeCVReviewInbox.aspx" runat="server" id="ACVReviewInbox" style="font-size: 12px !important;" title="Pending CV Review Inbox">Pending CV Review Inbox(1)</a></li>
                                <li id="LiETRInbox" runat="server" title="Pending Employee Transfer Request Inbox" visible="false"><a href="http://localhost/hrms/procs/EmployeeTransferReqInbox.aspx" runat="server" id="AETRInbox" style="font-size: 12px !important;" title="Pending Employee Transfer Request Inbox">Pending Employee Transfer Request Inbox(1)</a></li>
                                <li id="liAdvPayAPP" runat="server" title="Advance Payment Approval" visible="false"><a href="http://localhost/hrms/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending" runat="server" id="AAdvPay" style="font-size: 12px !important;" title="Advance Payment Approval">Advance Payment Approval</a></li>

                                <li id="liPerformanceReviewPendingCnt" runat="server" title="Appraisal Performance Review" visible="false"><a href="http://localhost/hrms/procs/PerformanceReviewList.aspx" runat="server" id="A_ApprPerformanceReviewPending" style="font-size: 12px !important;" title="Appraisal Performance Review">Appraisal Performance Review</a></li>
                                <li id="liPerformanceRecommendationPendingCnt" runat="server" title="Appraisal Performance Recommendation" visible="false"><a href="http://localhost/hrms/procs/ManageRecommendationList.aspx" runat="server" id="A_ApprPerformanceRecommendationPending" style="font-size: 12px !important;" title="Appraisal Performance Recommendation">Appraisal Performance Recommendation</a></li>
                                <li id="LiABAPObjectCompletion" runat="server" title="ABAP Object Completion" visible="false"><a href="http://localhost/hrms/procs/ABAP_Prd_TimeSheetAppList.aspx" runat="server" id="AABAPObjectCompletion" style="font-size: 12px !important;" title="ABAP Object Completion">ABAP Object Completion</a></li>

                                <li id="liEmployeeMediclaimData" runat="server" title="Employee Mediclaim Data" visible="false"><a href="http://localhost/hrms/procs/Mediclaimdata.aspx" runat="server" id="AEmployeeMediclaimData" style="font-size: 12px !important;" title="Employee Mediclaim Data">Employee Mediclaim Data</a></li>



                            </ul>

                        </div>
                    </div>

                    <br /><br /><br /><br />
                </div>

                <asp:HiddenField ID="HDCusts_HOD" runat="server" />


            </div>
        </div>
    </div>





</asp:Content>
