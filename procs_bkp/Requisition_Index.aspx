<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
	CodeFile="Requisition_Index.aspx.cs" Inherits="Requisition_Index" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
	Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

	<style>
		.myaccountpagesheading {
			text-align: center !important;
			text-transform: uppercase !important;
		}

		.aspNetDisabled {
			/*background: #dae1ed;*/
			background: #ebebe4;
		}

		.Calender {
			float: left;
			padding: 5% 5% 5% 5% !important;
		}

		/*--Bharat--*/
		#MainContent_lnk_Employee_Referral:link, #MainContent_lnk_Employee_Referral:visited,
		#MainContent_lnk_CreateQuestio:link, #MainContent_lnk_CreateQuestio:visited,
		#MainContent_lnk_Req_Questio:link, #MainContent_lnk_Req_Questio:visited,
		#MainContent_lnk_CreateJDBank:link, #MainContent_lnk_CreateJDBank:visited,
		#MainContent_lnk_MyJDBank:link, #MainContent_lnk_MyJDBank:visited,
		#MainContent_lnk_Req_Requisiti_Create:link, #MainContent_lnk_Req_Requisiti_Create:visited,
		#MainContent_lnk_Req_Requisiti_Details:link, #MainContent_lnk_Req_Requisiti_Details:visited,
		#MainContent_lnk_Req_RequisIndex:link, #MainContent_lnk_Req_RequisIndex:visited,
		#MainContent_lnk_Req_RequisApproval:link, #MainContent_lnk_Req_RequisApproval:visited,
		#MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
		#MainContent_lnk_leaveencashment:link, #MainContent_lnk_leaveencashment:visited,
		#MainContent_lnk_Vendor_Create:link, #MainContent_lnk_Vendor_Create:visited,
		#MainContent_lnk_Vendor_Details:link, #MainContent_lnk_Vendor_Details:visited,
		#MainContent_lnk_JobSite_Create:link, #MainContent_lnk_JobSite_Create:visited,
		#MainContent_lnk_JobSite_Details:link, #MainContent_lnk_JobSite_Details:visited,
		#MainContent_lnk_Req_Report_Dept:link, #MainContent_lnk_Req_Report_Dept:visited,
		#MainContent_lnk_Req_Report_Recruiter:link, #MainContent_lnk_Req_Report_Recruiter:visited,
		#MainContent_lnk_Rec_Offer_App_Index:link, #MainContent_lnk_Rec_Offer_App_Index:visited,
		#MainContent_lnk_Rec_Offer_Apprval_List:link, #MainContent_lnk_Rec_Offer_Apprval_List:visited,
		#MainContent_lnk_Screener_Inbox:link, #MainContent_lnk_Screener_Inbox:visited,
		#MainContent_lnk_Rec_Offer_Apprval_List:link, #MainContent_lnk_Rec_Offer_Apprval_List:visited,
		#MainContent_lnk_Prosepect_Cust_Details:link, #MainContent_lnk_Prosepect_Cust_Details:visited,
		#MainContent_lnk_Req_Report_SkillSet:link, #MainContent_lnk_Req_Report_SkillSet:visited,
        #MainContent_lnk_Req_Report_SkillSet:link, #MainContent_lnk_Req_Report_SkillSet:visited,
		
		/*Harsal*/
         #MainContent_Link_SkillSet:link, #MainContent_Link_SkillSet:visited,
         #MainContent_Link_Interviewermapping:link, #MainContent_Link_Interviewermapping:visited,
         #MainContent_LINK_ScreenerMapping:link, #MainContent_LINK_ScreenerMapping:visited,
        #MainContent_Lnk_Req_RequisitionStatus:link, #MainContent_Lnk_Req_RequisitionStatus:visited,
        #MainContent_Lnk_Req_StatusofInterview_L2:link, #MainContent_Lnk_Req_StatusofInterview_L2:visited,
        #MainContent_Lnk_Req_StatusofInterview_L1:link, #MainContent_Lnk_Req_StatusofInterview_L1:visited,
        #MainContent_lnk_Req_detailViewRequest:link, #MainContent_lnk_Req_detailViewRequest:visited,
        #MainContent_lnk_Req_detailapprovedRequest:link, #MainContent_lnk_Req_detailapprovedRequest:visited,
		#MainContent_Lnk_RecRescheduledInterviewList:link, #MainContent_Lnk_RecRescheduledInterviewList:visited,
        #MainContent_lnk_Detail_Report:link, #MainContent_lnk_Detail_Report:visited,
		#MainContent_lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter:link, #MainContent_lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter:visited,
		#MainContent_lnk_Req_RequisApprovalInterviewerSchedularChange:link, #MainContent_lnk_Req_RequisApprovalInterviewerSchedularChange:visited,
		#MainContent_lnk_Req_RequisApprovalRecruiterChange:link, #MainContent_lnk_Req_RequisApprovalRecruiterChange:visited,
		#MainContent_lnk_RecCreateEditCandidate:link, #MainContent_lnk_RecCreateEditCandidate:visited,
		#MainContent_lnk_mng_trvlrequest:link, #MainContent_lnk_mng_trvlrequest:visited,
		#MainContent_lnk_mng_recInbox:link, #MainContent_lnk_mng_recInbox:visited,
		#MainContent_lnk_mng_ViewRecRequest:link, #MainContent_lnk_mng_ViewRecRequest:visited,
		#MainContent_lnk_mng_InterviewrInbox:link, #MainContent_lnk_mng_InterviewrInbox:visited,
		#MainContent_lnk_mng_InterviewerShortlisting:link, #MainContent_lnk_mng_InterviewerShortlisting:visited,
		#MainContent_lnk_mng_ViewRecRequestInterviewer:link, #MainContent_lnk_mng_ViewRecRequestInterviewer:visited,
		#MainContent_Lnk_mng_recRescheduleInterview:link, #MainContent_Lnk_mng_recRescheduleInterview:visited,
		#MainContent_lnk_mng_recInterviewerShortlisted:link,#MainContent_lnk_mng_recInterviewerShortlisted:visited, 
		#MainContent_Lnk_RecSchedule_InterviewList:link,#MainContent_Lnk_RecSchedule_InterviewList:visited, 
		#MainContent_lnk_Screener_View_Recruitment:link, #MainContent_lnk_Screener_View_Recruitment:visited ,
        #MainContent_lnk_candidatedetails:link, #MainContent_lnk_candidatedetails:visited,
		#MainContent_lnk_Detail_Summary:link, #MainContent_lnk_Detail_Summary:visited,
		#MainContent_lnk_CTC_Inbox:link, #MainContent_lnk_CTC_Inbox:visited,
		#MainContent_lnk_CTC_View:link, #MainContent_lnk_CTC_View:visited,	
		#MainContent_Lnk_ReferCandidateHistory:link, #MainContent_Lnk_ReferCandidateHistory:visited, 
 		#MainContent_Lnk_DetailAbstractionReport:link, #MainContent_Lnk_DetailAbstractionReport:visited,
		#MainContent_lnk_Req_Candidate_Details_Approver_Inbox:link, #MainContent_lnk_Req_Candidate_Details_Approver_Inbox:visited	
		{
			background-color: #C7D3D4;
			color: #603F83 !important;
			border-radius: 10px;
			/*color: white;*/
			padding: 25px 5px;
			text-align: center;
			text-decoration: none;
			display: inline-block;
			width: 90% !important;
		}

		/*--Bharat--*/
	    #MainContent_lnk_Employee_Referral:hover, #MainContent_lnk_Employee_Referral:active,
	    #MainContent_lnk_CreateQuestio:hover, #MainContent_lnk_CreateQuestio:active,
	    #MainContent_lnk_Req_Questio:hover, #MainContent_lnk_Req_Questio:active,
	    #MainContent_lnk_CreateJDBank:hover, #MainContent_lnk_CreateJDBank:active,
	    #MainContent_lnk_MyJDBank:hover, #MainContent_lnk_MyJDBank:active,
	    #MainContent_lnk_Req_Requisiti_Create:hover, #MainContent_lnk_Req_Requisiti_Create:active,
	    #MainContent_lnk_Req_Requisiti_Details:hover, #MainContent_lnk_Req_Requisiti_Details:active,
	    #MainContent_lnk_Req_RequisIndex:hover, #MainContent_lnk_Req_RequisIndex:active,
	    #MainContent_lnk_Req_RequisApproval:hover, #MainContent_lnk_Req_RequisApproval:active,
	    #MainContent_lnk_TeamCalendar:hover, #MainContent_lnk_TeamCalendar:active,
	    #MainContent_lnk_leaveencashment:hover, #MainContent_lnk_leaveencashment:active,
	    #MainContent_lnk_Vendor_Create:hover, #MainContent_lnk_Vendor_Create:active,
	    #MainContent_lnk_Vendor_Details:hover, #MainContent_lnk_Vendor_Details:active,
	    #MainContent_lnk_JobSite_Create:hover, #MainContent_lnk_JobSite_Create:active,
	    #MainContent_lnk_JobSite_Details:hover, #MainContent_lnk_JobSite_Details:active,
	    #MainContent_lnk_Req_Report_Dept:hover, #MainContent_lnk_Req_Report_Dept:active,
	    #MainContent_lnk_Req_Report_Recruiter:hover, #MainContent_lnk_Req_Report_Recruiter:active,
	    #MainContent_lnk_Rec_Offer_App_Index:hover, #MainContent_lnk_Rec_Offer_App_Index:active,
	    #MainContent_lnk_Rec_Offer_Apprval_List:hover, #MainContent_lnk_Rec_Offer_Apprval_List:active,
	    #MainContent_lnk_Screener_Inbox:hover, #MainContent_lnk_Screener_Inbox:active,
	    #MainContent_lnk_Screener_View_Recruitment:hover, #MainContent_lnk_Screener_View_Recruitment:active,
	    #MainContent_lnk_Prosepect_Cust_Details:hover, #MainContent_lnk_Prosepect_Cust_Details:active,
	    #MainContent_lnk_Req_Report_SkillSet:hover, #MainContent_lnk_Req_Report_SkillSet:active,
	    /*Harsal*/
	    #MainContent_Link_SkillSet:hover, #MainContent_Link_SkillSet:active,
	    #MainContent_Link_Interviewermapping:hover, #MainContent_Link_Interviewermapping:active,
	    #MainContent_LINK_ScreenerMapping:hover, #MainContent_LINK_ScreenerMapping:active,
	    #MainContent_Lnk_Req_RequisitionStatus:hover, #MainContent_Lnk_Req_RequisitionStatus:active,
	    #MainContent_Lnk_Req_StatusofInterview_L2:hover, #MainContent_Lnk_Req_StatusofInterview_L2:active,
	    #MainContent_Lnk_Req_StatusofInterview_L1:hover, #MainContent_Lnk_Req_StatusofInterview_L1:active,
	    #MainContent_lnk_Req_detailViewRequest:hover, #MainContent_lnk_Req_detailViewRequest:active,
	    #MainContent_lnk_Req_detailapprovedRequest:hover, #MainContent_lnk_Req_detailapprovedRequest:active,
	    #MainContent_lnk_Detail_Report:hover, #MainContent_lnk_Detail_Report:active,
	    #MainContent_Lnk_RecRescheduledInterviewList:hover, #MainContent_Lnk_RecRescheduledInterviewList:active,
	    #MainContent_lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter:hover, #MainContent_lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter:active,
	    #MainContent_lnk_Req_RequisApprovalInterviewerSchedularChange:hover, #MainContent_lnk_Req_RequisApprovalInterviewerSchedularChange:active,
	    #MainContent_lnk_Req_RequisApprovalRecruiterChange:hover, #MainContent_lnk_Req_RequisApprovalRecruiterChange:active,
	    #MainContent_lnk_RecCreateEditCandidate:hover, #MainContent_lnk_RecCreateEditCandidate:active,
	    #MainContent_lnk_mng_recInbox:hover, #MainContent_lnk_mng_recInbox:active,
	    #MainContent_lnk_mng_ViewRecRequest:hover, #MainContent_lnk_mng_ViewRecRequest:active,
	    #MainContent_lnk_mng_InterviewrInbox:hover, #MainContent_lnk_mng_InterviewrInbox:active,
	    #MainContent_lnk_mng_InterviewerShortlisting:hover, #MainContent_lnk_mng_InterviewerShortlisting:active,
	    #MainContent_lnk_mng_ViewRecRequestInterviewer:hover, #MainContent_lnk_mng_ViewRecRequestInterviewer:active,
	    #MainContent_lnk_mng_recInterviewerShortlisted:hover, #MainContent_lnk_mng_recInterviewerShortlisted:active,
	    #MainContent_lnk_mng_recInterviewerShortlisted:hover, #MainContent_lnk_mng_recInterviewerShortlisted:active,
	    #MainContent_Lnk_RecSchedule_InterviewList:hover, #MainContent_Lnk_RecSchedule_InterviewList:active,
	    #MainContent_Lnk_mng_recRescheduleInterview:hover, #MainContent_Lnk_mng_recRescheduleInterview:active,
	    #MainContent_lnk_candidatedetails:hover, #MainContent_lnk_candidatedetails:active,
	    #MainContent_lnk_Detail_Summary:hover, #MainContent_lnk_Detail_Summary:active,
	    #MainContent_lnk_CTC_Inbox:hover, #MainContent_lnk_CTC_Inbox:active,
	    #MainContent_lnk_CTC_View:hover, #MainContent_lnk_CTC_View:active, 
	    #MainContent_Lnk_ReferCandidateHistory:hover, #MainContent_Lnk_ReferCandidateHistory:active,
	    #MainContent_Lnk_DetailAbstractionReport:hover, #MainContent_Lnk_DetailAbstractionReport:active,
	    #MainContent_lnk_Req_Candidate_Details_Approver_Inbox:hover, #MainContent_lnk_Req_Candidate_Details_Approver_Inbox:active 
	   {
	        /*background-color: #603F83;*/
	        background-color: #3D1956;
	        color: #C7D3D4 !important;
	        border-color: #3D1956;
	        border-width: 2pt;
	        border-style: inset;
	    }
		#MainContent_lnk_Employee_Referral:link, #MainContent_lnk_Employee_Referral:visited 
		{
			background-color: #3D1956;
			color: #C7D3D4!important;
			border-radius: 10px;
			/*color: white;*/
			padding: 25px 5px;
			text-align: center;
			text-decoration: none;
			display: inline-block;
			width: 90% !important;

		}
	    #MainContent_lnk_Employee_Referral:hover, #MainContent_lnk_Employee_Referral:active {
	        background-color: #C7D3D4;
	        color: #603F83 !important;
	        border-color: #C7D3D4;
	        border-width: 2pt;
	        /*border-style: inset;*/
	    }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
	<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
	<script type="text/javascript">
		var deprt;
		$(document).ready(function () {
			$("#MainContent_txtcity").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_City.ashx", {
				inputClass: "ac_input",
				resultsClass: "ac_results",
				loadingClass: "ac_loading",
				minChars: 1,
				delay: 1,
				matchCase: false,
				matchSubset: false,
				matchContains: false,
				cacheLength: 0,
				max: 12,
				mustMatch: false,
				extraParams: {},
				selectFirst: false,
				formatItem: function (row) { return row[0] },
				formatMatch: null,
				autoFill: false,
				width: 0,
				multiple: false,
				scroll: false,
				scrollHeight: 180,
				success: function (data) {
					response($.map(data, function (item) {
						return {

						}
					}));
				},
				context: this
			});
			$("#MainContent_txtloc").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_location.ashx", {
				inputClass: "ac_input",
				resultsClass: "ac_results",
				loadingClass: "ac_loading",
				minChars: 1,
				delay: 1,
				matchCase: false,
				matchSubset: false,
				matchContains: false,
				cacheLength: 0,
				max: 12,
				mustMatch: false,
				extraParams: {},
				selectFirst: false,
				formatItem: function (row) { return row[0] },
				formatMatch: null,
				autoFill: false,
				width: 0,
				multiple: false,
				scroll: false,
				scrollHeight: 180,
				success: function (data) {
					response($.map(data, function (item) {
						return {

						}
					}));
				},
				context: this
			});
			$("#MainContent_txtsubdept").autocomplete("<%=ReturnUrl("sitepathmain") %>Search_subdepartment.ashx?d=" + deprt, {
				inputClass: "ac_input",
				resultsClass: "ac_results",
				loadingClass: "ac_loading",
				minChars: 1,
				delay: 1,
				matchCase: false,
				matchSubset: false,
				matchContains: false,
				cacheLength: 0,
				max: 12,
				mustMatch: false,
				extraParams: { d: deprt },
				selectFirst: false,
				formatItem: function (row) { return row[0] },
				formatMatch: null,
				autoFill: false,
				width: 0,
				multiple: false,
				scroll: false,
				scrollHeight: 180,
				success: function (data) {
					response($.map(data, function (item) {
						return {
						}
					}));
				},

				context: this
			});
		});
	</script>
	<div class="commpagesdiv">
		<div class="commonpages">
			<div class="wishlistpagediv">
				<div class="wishlistpage">
					<div class="userposts">
						<span>
							<asp:Label ID="lblheading" runat="server" Text="Recruitment"></asp:Label>
						</span>
					</div>
					<div class="leavegrid">
						<%--<a href="http://192.168.21.193/hrms/Claims.aspx" class="aaa" >Recruitment Home </a>--%>
					</div>
					<div class="editprofile" id="editform1" runat="server" visible="true">
						<div class="editprofileform">
							<asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>

								<%--Bharat--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Employee_Referral" visible="true" runat="server" PostBackUrl="~/procs/Ref_Employee_Index.aspx">Employee Referral</asp:LinkButton>
									</td>
									<td class="formtitle">
										</td>
									</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_CreateQuestio" runat="server" Visible="false" OnClick="lnk_leaverequest_Click">Create Questionnaire</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_Questio" runat="server" Visible="false" PostBackUrl="~/procs/Req_Questionnaire_Index.aspx">My Questionnaire</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_CreateJDBank" Visible="false" runat="server" ToolTip="Create JD Bank" PostBackUrl="~/procs/Req_JD_Bank_create.aspx">Create JD Bank</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyJDBank" Visible="false" runat="server" PostBackUrl="~/procs/Req_JD_Bank_Index.aspx">My JD Bank </asp:LinkButton>
									</td>

								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_Vendor_Create" Visible="false" runat="server" ToolTip="Create Vender" PostBackUrl="~/procs/Req_Vendor_Create.aspx">Create Vendor</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Vendor_Details" Visible="false" runat="server" PostBackUrl="~/procs/Req_Vendor_Details.aspx">Vendor Details </asp:LinkButton>
									</td>

								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_JobSite_Create" Visible="false" runat="server" ToolTip="Create Job Sites" PostBackUrl="~/procs/Req_JobSites_Create.aspx">Create Job Sites</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_JobSite_Details" Visible="false" runat="server" PostBackUrl="~/procs/Req_JobSites_Details.aspx">Job Sites Details </asp:LinkButton>
									</td>

								</tr>
                                 <tr style="padding-top: 1px; padding-bottom: 4px">
									<td class="formtitle">
										<asp:LinkButton ID="LINK_ScreenerMapping" Visible="false" runat="server" ToolTip="Create Screener Mapping" PostBackUrl="~/procs/Screenersmapping.aspx">Create Screener Mapping</asp:LinkButton>
									</td>
                                     <td class="formtitle">
										<asp:LinkButton ID="Link_SkillSet" Visible="false" runat="server" ToolTip="Create SkillSet" PostBackUrl="~/procs/SkillSetAdd.aspx">Create SkillSet</asp:LinkButton>
									</td>
								</tr>


								<%--for  Prosepect_Cust_Details--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="spprose" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Prosepect Customer : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_Prosepect_Cust_Details" Visible="false" runat="server" ToolTip="Create Prosepect Customers" PostBackUrl="~/procs/Req_Prosepect_Cust.aspx">Create Prosepect Customers </asp:LinkButton>
									</td>									
								</tr>
								
								<%--Create  Requisition Block--%>
								<tr style="padding-top: 1px; padding-bottom: 4px">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_Requisiti_Create" Visible="false" runat="server" ToolTip="Create Requisition" PostBackUrl="~/procs/Req_Requisition_Create.aspx">Create Requisition </asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_Requisiti_Details" Visible="false" runat="server" PostBackUrl="~/procs/Req_Requisition_Details.aspx">My Recruitment Requests</asp:LinkButton>
									</td>

								</tr>
								<%-- Requisition Approval Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="span_App_head" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approver : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_RequisIndex" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_RequisitionIndex.aspx?itype=Pending">Inbox(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_RequisApproval" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_RequisitionIndex.aspx?itype=APP">Requisition Approval List</asp:LinkButton>
									</td>
								</tr>
								<%-- Recruiter  Change Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_RequisApprovalRecruiterChange" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_RequiterChangeIndex.aspx?itype=APP">Recruiter Change</asp:LinkButton>
									</td>
									<%-- Interviewer Schedular Change Block--%>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_RequisApprovalInterviewerSchedularChange" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_InterviewerSchedularChangeIndex.aspx?itype=APP">Interviewer Schedular Change</asp:LinkButton>
									</td>
								</tr>

                                <%-- CTC Approval Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="CTCspan" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">CTC Exception Approver : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_CTC_Inbox" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_CTCIndex.aspx?itype=Pending">Inbox(0)</asp:LinkButton>
									</td>
									<td class="formtitle"> 
										<asp:LinkButton ID="lnk_CTC_View" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_CTCIndex.aspx?itype=APP">CTC Exception Approval List</asp:LinkButton>
									</td>
								</tr>




								<%--Harsal--%>

								<%--  Recruiter Block--%>
								<tr style="padding-bottom: 2px;" id="SpanRecruiterBoxs" visible="false"  runat="server">
									<td class="formtitle">
										<br />
										<span style="font-size: 12pt; font-weight: bold; color: #3D1956;" runat="server" id="lblheadingRecruiter">Recruiter : </span>
										<%--<span style="font-size: 12pt; font-weight: bold; color: #3D1956;" visible="false" runat="server" id="lblheadingInterSchedular">Interviewer Schedular : </span>--%>
									</td>
								</tr>

								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_mng_recInbox" runat="server" OnClick="lnk_mng_recInbox_Click">Inbox(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_mng_ViewRecRequest" runat="server" PostBackUrl="~/procs/Rec_RecruiterInbox.aspx?type=VRR">View Recruitment Requests</asp:LinkButton>
									</td>
								</tr>								
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_RecCreateEditCandidate" runat="server" PostBackUrl="~/procs/SearchCandidate.aspx">Create/Edit Candidate</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_RequisApprovalInterviewerSchedularChangeonlyRequiter" runat="server" Text="" Visible="false" PostBackUrl="~/procs/Req_InterviewerSchedularChangeIndex.aspx?itype=APP">Interviewer Schedular Change</asp:LinkButton>
									</td>
								</tr>
								 <tr>
									 <td class="formtitle">
										<asp:LinkButton ID="lnk_Req_Candidate_Details_Approver_Inbox" runat="server" Text="" Visible="false" PostBackUrl="~/procs/Rec_RecruiterCandidateInbox.aspx">Verify Candidate Data</asp:LinkButton>
									</td>
									 <td></td>
								 </tr>

									<%-- Screener Block--%>
								<tr style="padding-bottom: 2px;" id="trScreener" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span style="font-size: 12pt; font-weight: bold; color: #3D1956;">Screener : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" runat="server">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Screener_Inbox" runat="server" Visible="false" OnClick="lnk_mng_InterviewerShortlisting_Click">Inbox Shortlisting(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Screener_View_Recruitment" Visible="false" runat="server" PostBackUrl="~/procs/Rec_InterviewerInbox.aspx?type=InScreeningList">View Screening Requests</asp:LinkButton>
									</td>
								</tr>

								<%-- Interviewer Schedular Block--%>
								<tr style="padding-bottom: 2px;" id="trInterSchedular" runat="server" visible="false">
									<td class="formtitle">
										<br />
										<span style="font-size: 12pt; font-weight: bold; color: #3D1956;"  runat="server" id="Span2">Interview Schedular : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									
									<td class="formtitle">
										<asp:LinkButton ID="lnk_mng_recInterviewerShortlisted" Visible="false" runat="server" OnClick="lnk_mng_recInterviewerShortlisted_Click">Schedule Interview(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="Lnk_mng_recRescheduleInterview" Visible="false" runat="server" OnClick="Lnk_mng_recRescheduleInterview_Click">Reschedule Interview(0)</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                      <asp:LinkButton ID="Lnk_RecRescheduledInterviewList" runat="server" PostBackUrl="~/procs/InterviewRescheduledList.aspx" Visible="false">Scheduled Interviews List</asp:LinkButton>
                                     </td>
                                    <td class="formtitle">
										<asp:LinkButton ID="Lnk_RecSchedule_InterviewList" Visible="false" runat="server" PostBackUrl="~/procs/Rec_RecruiterInbox.aspx?type=VRRIS">View Scheduling Requests</asp:LinkButton>
									</td>
                                </tr>

                                 <tr style="padding-top: 1px; padding-bottom: 4px">
									<td class="formtitle">
										<asp:LinkButton ID="Link_Interviewermapping" Visible="false" runat="server" ToolTip="Create Interviwer Mapping" PostBackUrl="~/procs/Interviewermapping.aspx">Create Interviwer Mapping</asp:LinkButton>
									</td>
								</tr>

								<%--  Interviewer Block--%>
								<tr style="padding-bottom: 2px;" id="SpanInterviewerBoxs" runat="server">
									<td class="formtitle">
										<br />
										<span style="font-size: 12pt; font-weight: bold; color: #3D1956;">Interviewer : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" id="TrInterviewer">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_mng_InterviewrInbox" runat="server" Visible="false" OnClick="lnk_mng_InterviewrInbox_Click">Inbox Interview(0)</asp:LinkButton>
									</td>	
									<td class="formtitle">
										<%--<asp:LinkButton ID="lnk_mng_InterviewerShortlisting" runat="server" Visible="false" OnClick="lnk_mng_InterviewerShortlisting_Click">Inbox Shortlisting(0)</asp:LinkButton>--%>									
									
										<asp:LinkButton ID="lnk_mng_ViewRecRequestInterviewer" Visible="false" runat="server" PostBackUrl="~/procs/Rec_InterviewerInbox.aspx?type=InVRInter">View Interviewer Requests</asp:LinkButton>
									</td>
								</tr>
								

								<%--  Offer Approver HOD Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="span_Offer_APP" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Offer Approver : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Rec_Offer_App_Index" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Offer_Index.aspx?itype=Pending">Inbox(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Rec_Offer_Apprval_List" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Offer_Index.aspx?itype=APP">Offer Approval List</asp:LinkButton>
									</td>
								</tr>

								<%--Report for  HOD Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="span_Report_head" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Report : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle" runat="server" id="DeptHOD">
										<asp:LinkButton ID="lnk_Req_Report_Dept" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Requis_Dept_Report.aspx"> Summary Report</asp:LinkButton>
									</td>
									<td class="formtitle">
                            	<asp:LinkButton ID="lnk_Detail_Report" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Detail_Report.aspx">Detail Report</asp:LinkButton>
									</td>
								</tr>
								<%--Report for  Recruiter Head Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_Req_Report_Recruiter" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Requis_Recruiter_Report.aspx">Recruiter wise Summary Report</asp:LinkButton>
									</td>
                                    <td class="formtitle">
										<asp:LinkButton ID="lnk_Req_detailapprovedRequest" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Rec_RequisitionViewListApproved.aspx">View Recruitment Status</asp:LinkButton>
									</td>
								</tr>

                                <%--Requisition View Status Block for HOD and Requisitioner--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle" id="HODViewShow" runat="server" >
										<asp:LinkButton ID="lnk_Req_detailViewRequest" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Rec_RequisitionViewListApproved.aspx">View Recruitment Status</asp:LinkButton>
									</td>
                                    <td class="formtitle">
										<asp:LinkButton ID="lnk_Req_Report_SkillSet" Visible="false" runat="server" Text="" ToolTip="Skillset wise Summary Report" PostBackUrl="~/procs/Req_SkillsetwiseReport.aspx">Skillset wise Summary Report</asp:LinkButton>
									</td>
								</tr>
                                <%--Report for  Recruiter All and Recruiter Head --%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="Lnk_Req_StatusofInterview_L1" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_StatusofInterview_L1_Report.aspx">L1 Report</asp:LinkButton>
									</td>
                                    <td class="formtitle">
                                             <asp:LinkButton ID="Lnk_Req_StatusofInterview_L2" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_StatusofInterview_L2_Report.aspx">L2 Report</asp:LinkButton>
							         </td>
								</tr>
                                 <%--Report for  Recruiter All and Recruiter Head --%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="Lnk_Req_RequisitionStatus" Visible="false" runat="server" Text="" PostBackUrl="~/procs/RequisitionStatusList.aspx">Requisition Status</asp:LinkButton>
									</td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Detail_Summary" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Detail_Summary_Report.aspx">Requisition Abstract Report </asp:LinkButton>
                                    </td>
								</tr>
                                 <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_candidatedetails" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Candidate_Report.aspx">Candidate Detail</asp:LinkButton>
                                    </td>
                                   <td class="formtitle">
  					<asp:LinkButton ID="Lnk_ReferCandidateHistory" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Ref_CandidateReport.aspx">Refer. Candidate History</asp:LinkButton>
  				  </td>
                                </tr>
				<tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                    <asp:LinkButton ID="Lnk_DetailAbstractionReport" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Req_Detail_AbstractCandidate_Report.aspx">Requisition Detail Abstract Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                    </td>
                                </tr>


							</table>
							<br />
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>


	<br />
	<asp:HiddenField ID="hflapprcode" runat="server" />
	<asp:HiddenField ID="hdnClaimDate" runat="server" />
	<asp:HiddenField ID="hflEmpCode" runat="server" />
	<asp:HiddenField ID="hdnBand" runat="server" />
	<asp:HiddenField ID="HiddenField2" runat="server" />
	<asp:HiddenField ID="HiddenField4" runat="server" />
	<asp:HiddenField ID="HiddenField3" runat="server" />

	<script type="text/javascript">
		function textboxMultilineMaxNumber(txt, maxLen) {
			try {
				if (txt.value.length > (maxLen - 1)) return false;
			} catch (e) {
			}
		}


		function maxLengthPaste(field, maxChars) {
			event.returnValue = false;
			if ((field.value.length + window.clipboardData.getData("Text").length) > maxChars) {
				return false;
			}
			event.returnValue = true;
		}

		function Count(text) {
			var maxlength = 250;
			var object = document.getElementById(text.id)
			if (object.value.length > maxlength) {
				object.focus();
				object.value = text.value.substring(0, maxlength);
				object.scrollTop = object.scrollHeight;
				return false;
			}
			return true;
		}

	</script>


</asp:Content>

