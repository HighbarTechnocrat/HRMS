<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
	CodeFile="ExitProcess_Index.aspx.cs" Inherits="ExitProcess_Index" %>

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

		/*--Manisha--*/
		#MainContent_lnk_ResigForm:link, #MainContent_lnk_ResigForm:visited,
		#MainContent_lnk_MyResig:link, #MainContent_lnk_MyResig:visited,
		#MainContent_lnk_ExitSurveyForm:link, #MainContent_lnk_ExitSurveyForm:visited,
		#MainContent_lnk_MyExitSurveyForm:link, #MainContent_lnk_MyExitSurveyForm:visited,
		#MainContent_lnk_ClearanceForm:link, #MainContent_lnk_ClearanceForm:visited,
		#MainContent_lnk_MyClearanceForm:link, #MainContent_lnk_MyClearanceForm:visited,
	    #MainContent_lnk_MyExitInterviewForm:link, #MainContent_lnk_MyExitInterviewForm:visited,
		#MainContent_lnk_InboxResignations:link, #MainContent_lnk_InboxResignations:visited,
		#MainContent_lnk_ProcessedResignations:link, #MainContent_lnk_ProcessedResignations:visited ,
		#MainContent_lnk_SubmitExitInteriewForm:link, #MainContent_lnk_SubmitExitInteriewForm:visited ,
		#MainContent_lnk_TeamExitInterviewForm:link, #MainContent_lnk_TeamExitInterviewForm:visited ,
		#MainContent_lnk_InboxClearanceForm:link, #MainContent_lnk_lnk_InboxClearanceForm:visited ,
		#MainContent_lnk_ApprovedClearanceForm:link, #MainContent_lnk_ApprovedClearanceForm:visited,
		#MainContent_lnk_SummaryReport:link, #MainContent_lnk_SummaryReport:visited,
		#MainContent_lnk_Detail_Report:link, #MainContent_lnk_Detail_Report:visited,
		#MainContent_lnk_Retention_Create:link, #MainContent_lnk_Retention_Create:visited,
		#MainContent_lnk_Retention_MyList:link, #MainContent_lnk_Retention_MyList:visited,
		#MainContent_lnk_Moderation_Inbox:link, #MainContent_lnk_Moderation_Inbox:visited,
		#MainContent_lnk_Moderation_APP:link, #MainContent_lnk_Moderation_APP:visited
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
		    
		/*--Manisha--*/
		#MainContent_lnk_ResigForm:hover, #MainContent_lnk_ResigForm:active,
		#MainContent_lnk_MyResig:hover, #MainContent_lnk_MyResig:active,
		#MainContent_lnk_ExitSurveyForm:hover, #MainContent_lnk_ExitSurveyForm:active,
		#MainContent_lnk_MyExitSurveyForm:hover, #MainContent_lnk_MyExitSurveyForm:active,
		#MainContent_lnk_ClearanceForm:hover, #MainContent_lnk_ClearanceForm:active,
		#MainContent_lnk_MyClearanceForm:hover, #MainContent_lnk_MyClearanceForm:active,
	    #MainContent_lnk_MyExitInterviewForm:hover, #MainContent_lnk_MyExitInterviewForm:active,
		#MainContent_lnk_InboxResignations:hover, #MainContent_lnk_InboxResignations:active,
		#MainContent_lnk_ProcessedResignations:hover, #MainContent_lnk_ProcessedResignations:active, 
		#MainContent_lnk_SubmitExitInteriewForm:hover, #MainContent_lnk_SubmitExitInteriewForm:active ,
		#MainContent_lnk_TeamExitInterviewForm:hover, #MainContent_lnk_TeamExitInterviewForm:active,
		#MainContent_lnk_InboxClearanceForm:hover, #MainContent_lnk_lnk_InboxClearanceForm:active,
		#MainContent_lnk_ApprovedClearanceForm:hover, #MainContent_lnk_ApprovedClearanceForm:active,
		#MainContent_lnk_SummaryReport:hover, #MainContent_lnk_SummaryReport:active,
		#MainContent_lnk_Detail_Report:hover, #MainContent_lnk_Detail_Report:active,
		#MainContent_lnk_Retention_Create:hover, #MainContent_lnk_Retention_Create:active,
		#MainContent_lnk_Retention_MyList:hover, #MainContent_lnk_Retention_MyList:active,
		#MainContent_lnk_Moderation_Inbox:hover, #MainContent_lnk_Moderation_Inbox:active,
		#MainContent_lnk_Moderation_APP:hover, #MainContent_lnk_Moderation_APP:active
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
							<asp:Label ID="lblheading" runat="server" Text="Exit Processes"></asp:Label>
						</span>
					</div>
					<div class="leavegrid">
						<%--<a href="https://ess.highbartech.com/hrms/Claims.aspx" class="aaa" >Recruitment Home </a>--%>
					</div>
					<div class="editprofile" id="editform1" runat="server" visible="true">
						<div class="editprofileform">
							<asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
								<%--Manisha--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ResigForm" runat="server" Visible="true" PostBackUrl="~/procs/ResignationForm.aspx">Resignation Form</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyResig" runat="server" Visible="true" PostBackUrl="~/procs/MyResignations.aspx">My Resignations</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ExitSurveyForm" Visible="false" runat="server" ToolTip="Submit Exit Survey Form" PostBackUrl="~/procs/ExitProcess_SurveyForm.aspx">Submit Exit Survey Form</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyExitSurveyForm" Visible="false" runat="server" PostBackUrl="~/procs/MyExitSurveyForm.aspx">My Exit Survey Form </asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">

									<td class="formtitle">
										<asp:LinkButton ID="lnk_ClearanceForm" Visible="false" runat="server" ToolTip="Submit Clearance Form" PostBackUrl="~/Procs/ExitProcClearanceForm.aspx">Submit Clearance Form</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyClearanceForm" Visible="false" runat="server" PostBackUrl="~/procs/ExitProcess_MyClearanceForm.aspx">My Clearance Form </asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_JobSite_Create" Visible="false" runat="server" ToolTip="Create Job Sites" PostBackUrl="#">Create Job Sites</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyExitInterviewForm" Visible="false" runat="server" PostBackUrl="~/Procs/ExitProcess_MyExitInterviewForm.aspx">My Exit Interview Forms</asp:LinkButton>
									</td>
								</tr>
									<%-- Approver Block--%>
								<tr style="padding-bottom: 2px;" id="trScreener" runat="server" visible="true">
									<td class="formtitle">
										<br />
										<span id="span_App" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approver : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" runat="server">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_InboxResignations" runat="server" Visible="true" PostBackUrl="~/procs/InboxResignations.aspx">Inbox Resignation(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ProcessedResignations" Visible="true" runat="server" PostBackUrl="~/procs/ProcessedResignations.aspx">Processed Resignation</asp:LinkButton>
									</td>
								</tr>
								<tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                      <asp:LinkButton ID="lnk_SubmitExitInteriewForm" runat="server" PostBackUrl="~/Procs/ExitProcess_ExitInterviewList.aspx">Submit Exit Interview Form</asp:LinkButton>
                                     </td>
                                    <td class="formtitle">
										<asp:LinkButton ID="lnk_TeamExitInterviewForm" runat="server" PostBackUrl="~/procs/ExitProc_TeamExitInterviewForm.aspx">Team Exit Interview Forms</asp:LinkButton>
									</td>
                                </tr>
								<%--  Clearance Approvers Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="span_clr" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Clearance Approvers : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" runat="server">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_InboxClearanceForm" runat="server" PostBackUrl="~/Procs/ExitProcess_ClearanceInbox.aspx">Inbox Clearance Forms(0)</asp:LinkButton>
									</td>	
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ApprovedClearanceForm" runat="server" PostBackUrl="~/Procs/ExitProcess_ApprovedClearance.aspx">Approved Clearance Forms</asp:LinkButton>
									</td>
								</tr>
								<%--Retaination for HOD Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="SPRT" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Employee Retention : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle" runat="server" id="Td1">
										<asp:LinkButton ID="lnk_Retention_Create" Visible="false" runat="server" Text="" PostBackUrl="~/Procs/EmployeeRetentionDetails.aspx">Submit Employee Retention</asp:LinkButton>
									</td>
									<td class="formtitle">
                                    	<asp:LinkButton ID="lnk_Retention_MyList" Visible="false" runat="server" Text="" PostBackUrl="~/Procs/MyRetentionInbox.aspx">Employee Retention List</asp:LinkButton>
									</td>
								</tr>
								<%--Retaination for Moderation Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="SPMD" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;"> Moderation : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle" runat="server" id="Td2">  
										<asp:LinkButton ID="lnk_Moderation_Inbox" Visible="false" runat="server" Text="" PostBackUrl="~/procs/ExitProcess_Mo_Index.aspx?Type=Pending" >Inbox Retention(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
                                    	<asp:LinkButton ID="lnk_Moderation_APP" Visible="false" runat="server" Text="" PostBackUrl="~/procs/ExitProcess_Mo_Index.aspx?Type=View" >Employee Retention List</asp:LinkButton>
									</td>
								</tr>
								<%--Report for RM Block--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<br />
										<span id="span_rpt" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Reports : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle" runat="server" id="DeptHOD">
										<asp:LinkButton ID="lnk_SummaryReport" Visible="false" runat="server" Text="" PostBackUrl="#"> Summary Report</asp:LinkButton>
									</td>
									<td class="formtitle">
                                    	<asp:LinkButton ID="lnk_Detail_Report" Visible="false" runat="server" Text="" PostBackUrl="#">Detail Report</asp:LinkButton>
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

