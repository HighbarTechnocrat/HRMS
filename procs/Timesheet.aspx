<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Timesheet.aspx.cs" Inherits="Timesheet" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading 
        {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
             /*background: #dae1ed;*/
           background:#ebebe4;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }

#MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited,
#MainContent_lnk_LeaveRequest_FrmHR:link, #MainContent_lnk_LeaveRequest_FrmHR:visited,
#MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
#MainContent_lnk_HRLeaveInbox:link, #MainContent_lnk_HRLeaveInbox:visited,
#MainContent_lnkempLeaveRpt:link, #MainContent_lnkempLeaveRpt:visited,
#MainContent_lnk_leavereport_Hr:link, #MainContent_lnk_leavereport_Hr:visited,
#MainContent_lnk_leavereport:link, #MainContent_lnk_leavereport:visited,
#MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
#MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
#MainContent_lnk_leaveinboxApprovedTimeInbox:link, #MainContent_lnk_leaveinboxApprovedTimeInbox:visited,
#MainContent_lnk_leaveencashment:link, #MainContent_lnk_leaveencashment:visited,
#MainContent_lnk_ResigForm:link, #MainContent_lnk_ResigForm:visited,
#MainContent_lnk_MyResig:link, #MainContent_lnk_MyResig:visited,
#MainContent_lnk_InboxResignations:link, #MainContent_lnk_InboxResignations:visited,
#MainContent_lnk_ProcessedResignations:link, #MainContent_lnk_ProcessedResignations:visited,
#MainContent_Lnk_ReportABAP:link, #MainContent_Lnk_ReportABAP:visited
 {
  background-color: #C7D3D4;
  color: #603F83 !important;
  border-radius: 10px;
  /*color: white;*/
  padding: 25px 5px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  width:90% !important;
}

#MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active, 
#MainContent_lnk_leaveinboxApprovedTimeInbox:hover, #MainContent_lnk_leaveinboxApprovedTimeInbox:active,
#MainContent_lnk_LeaveRequest_FrmHR:hover, #MainContent_lnk_LeaveRequest_FrmHR:active,
#MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
#MainContent_lnk_HRLeaveInbox:hover, #MainContent_lnk_HRLeaveInbox:active,
#MainContent_lnkempLeaveRpt:hover, #MainContent_lnkempLeaveRpt:active,
#MainContent_lnk_leavereport_Hr:hover, #MainContent_lnk_leavereport_Hr:active,
#MainContent_lnk_leavereport:hover, #MainContent_lnk_leavereport:active,
#MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
#MainContent_lnk_TeamCalendar:hover, #MainContent_lnk_TeamCalendar:active,
#MainContent_lnk_leaveencashment:hover, #MainContent_lnk_leaveencashment:active,
#MainContent_lnk_ResigForm:hover, #MainContent_lnk_ResigForm:active,
#MainContent_lnk_MyResig:hover, #MainContent_lnk_MyResig:active,
#MainContent_lnk_InboxResignations:hover, #MainContent_lnk_InboxResignations:active,
#MainContent_lnk_ProcessedResignations:hover, #MainContent_lnk_ProcessedResignations:active,
#MainContent_Lnk_ReportABAP:hover, #MainContent_Lnk_ReportABAP:active
 {
  /*background-color: #603F83;*/
    background-color: #3D1956;
  color: #C7D3D4 !important;
  border-color: #3D1956;
  border-width: 2pt;
  border-style:inset;
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
                            <asp:Label ID="lblheading" runat="server" Text="Timesheet"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" style="font-size:medium;font-weight:bold;"></asp:Label>
							<table id="tbl_menu" runat="server">
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server" OnClick="lnk_leaverequest_Click">Record Timesheet</asp:LinkButton>                                         
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" OnClick="lnk_mng_leaverequest_Click">My Timesheet</asp:LinkButton>
                                    </td>
                                </tr>
                                
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkempLeaveRpt" Visible="true" runat="server" ToolTip="Timesheet Report" PostBackUrl="~/procs/TimesheetRecordReport.aspx">My Timesheet Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_LeaveRequest_FrmHR" Visible="true" runat="server" PostBackUrl="~/procs/TimesheetRecordDetailReport.aspx">My Timesheet Details Report</asp:LinkButton>                                         
                                    </td>
                                </tr>

                                 <tr style="padding-top: 1px; padding-bottom: 2px;">
									<td class="formtitle">
										<asp:LinkButton ID="lnk_ResigForm" runat="server" Visible="false" PostBackUrl="~/procs/ABAP_Prd_TimeSheetAdd.aspx">ABAP Object Completion</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyResig" runat="server" Visible="false" PostBackUrl="~/procs/ABAP_Prd_TimeSheetList.aspx">My ABAP Object Completion</asp:LinkButton>
									</td>
								</tr>

                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_App_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Approver: </span>
                                    </td>
                                </tr>  
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server"  Text="Inbox " PostBackUrl="~/procs/InboxTimesheet_Req.aspx?itype=0">Inbox </asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                         <asp:LinkButton ID="lnk_leaveinboxApprovedTimeInbox" runat="server"  Text="Approved TimeSheet" PostBackUrl="~/procs/ApprovedTimesheet_Req.aspx?itype=0">Approved TimeSheet </asp:LinkButton>

                                    </td>
                                </tr>
                                  <tr style="padding-top:1px;padding-bottom:2px;">
                                      <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport" runat="server" Text="Team Regularization" Visible="false" PostBackUrl="~/procs/TimesheetRecordPMReport.aspx">Team Deployment Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport_Hr" runat="server" Visible="false"  Text="Inbox " PostBackUrl="~/procs/TimesheetDetailsTReport.aspx">Team Timesheet Detail Report </asp:LinkButton>
                                    </td>
                                   
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_Hr_head" runat="server" visible="false" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - HR: </span>
                                    </td>
                                </tr> 
                                <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton Visible="false" ID="lnk_HRLeaveInbox" runat="server" Text="HR Inbox" PostBackUrl="~/procs/InboxAttend_req.aspx?itype=1">HR Inbox</asp:LinkButton>
                                    </td>

                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport_Hr12" runat="server" Text="Regularization Report - HR" Visible="false" PostBackUrl="~/procs/AttendanceReport_HR.aspx">Regularization Report - HR</asp:LinkButton>
                                    </td>

                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">                                            
                                      <asp:LinkButton ID="lnk_TeamCalendar" Visible="false" runat="server" PostBackUrl="~/procs/TeamReport.aspx">Team Calendar</asp:LinkButton>                                                
                                    </td>
                                </tr> 
                               

                                <tr style="padding-bottom: 2px;" id="trScreener" runat="server" visible="false">
									<td class="formtitle">
										<br />
                                        <asp:HiddenField ID="HDCount" runat="server" />
										<span id="span_App" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;display:none">Approver : </span>
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
									</td>
								</tr>



<%--                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Attendancereg" runat="server" PostBackUrl="~/procs/Attendence.aspx">Regularize Attendance</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_attendanceinbox" runat="server" Text="Inbox (attendance regularization requests)" PostBackUrl="~/procs/InboxAttendance.aspx?apptype=0" >Inbox (attendance regularization)</asp:LinkButton>
                                    </td>
                                </tr>--%>
<%--                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_Attendancereg" runat="server" PostBackUrl="~/procs/MngAttendanceRequest.aspx">Manage Attendance regularization</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_HRAttendanceInbox" runat="server" Text="HR Inbox (Attendance requests)" PostBackUrl="~/procs/InboxAttendance.aspx?apptype=1">HR Inbox (Attendance requests)</asp:LinkButton>
                                    </td>

                                </tr> --%>                               
<%--                                <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>--%>

<%--                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                </tr>--%>

<%--                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <a  href="http://172.18.37.5/hrms/procs/hrms- How to Apply Leave.pdf" title="hrms- How to Apply Leave" class="LeaveManualLnik" target="_blank">hrms- How to Apply Leave</a>
                                        
                                    </td>
                                    <td class="formtitle">                                            
                                      <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" Text="Travel Index" class="homemanuallnk"></asp:LinkButton>
                                    </td>

                                </tr>
                               
                                   <tr style="padding-top:1px;padding-bottom:2px;">
                                </tr>

								<tr style="padding-top:1px;padding-bottom:2px;">
									<td class="formtitle">
										<ucical:calender ID="icalender" runat="server"></ucical:calender>
									</td>
								</tr>--%>	
                               						
                            </table>
                    <%--<ul>
                    <span>
                        <li class="Calender">

                            <div>
                                <ucical:calender ID="icalender1" runat="server"></ucical:calender>
                            </div>
                        </li>
                    </span>
                </ul>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>
    
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />
    
    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnempwrkSchdule" runat="server" />
    <asp:HiddenField ID="hdnisViewAttendance" runat="server" />


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
