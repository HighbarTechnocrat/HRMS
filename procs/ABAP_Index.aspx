<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Index.aspx.cs" Inherits="procs_ABAP_Index" %>

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
        #MainContent_Lnk_ReportABAP:link, #MainContent_Lnk_ReportABAP:visited,
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
        #MainContent_Lnk_ReportABAP:hover, #MainContent_Lnk_ReportABAP:active,
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
							<asp:Label ID="lblheading" runat="server" Text="ABAP Object Completion"></asp:Label>
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
										<asp:LinkButton ID="lnk_ResigForm" runat="server" Visible="false" PostBackUrl="~/procs/ABAP_Prd_TimeSheetAdd.aspx">ABAP Object Completion</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyResig" runat="server" Visible="false" PostBackUrl="~/procs/ABAP_Prd_TimeSheetList.aspx">My ABAP Object Completion</asp:LinkButton>
									</td>
								</tr>
								
									<%-- Approver Block--%>
								<tr style="padding-bottom: 2px;" id="trScreener" runat="server" visible="false">
									<td class="formtitle">
										<br />
                                        <asp:HiddenField ID="HDCount" runat="server" />
										<span id="span_App" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approver : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" id="trApprover" visible="false">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_InboxResignations" runat="server" OnClick="lnk_InboxResignations_Click">Inbox ABAP Object Completion(0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ProcessedResignations" runat="server" PostBackUrl="~/procs/ABAP_Prd_TimeSheetAppListView.aspx">Processed ABAP Object Completion</asp:LinkButton>
									</td>
								</tr>
								<%-- Reprt Block--%>
								<tr style="padding-bottom: 2px;" id="TRReport" runat="server" visible="false">
									<td class="formtitle">
										<br />
                                         <asp:HiddenField ID="HDRptCout" runat="server" />
										<span id="spanReport" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Report : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" id="tr1Report" visible="false">
									<td class="formtitle">
										<asp:LinkButton ID="Lnk_ReportABAP" runat="server"  OnClick="Lnk_ReportABAP_Click">ABAP Object Completion Report</asp:LinkButton>
									</td>
									<td class="formtitle">
								<%--		<asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/procs/ABAP_Prd_TimeSheetAppListView.aspx">Processed ABAP Completion</asp:LinkButton>
								--%>	</td>
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

