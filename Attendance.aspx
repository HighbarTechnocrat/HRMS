<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Attendance.aspx.cs" Inherits="Attendance" %>

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

        #MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited,
        #MainContent_lnk_LeaveRequest_FrmHR:link, #MainContent_lnk_LeaveRequest_FrmHR:visited,
        #MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
        #MainContent_lnk_HRLeaveInbox:link, #MainContent_lnk_HRLeaveInbox:visited,
        #MainContent_lnkempLeaveRpt:link, #MainContent_lnkempLeaveRpt:visited,
        #MainContent_lnk_leavereport_Hr:link, #MainContent_lnk_leavereport_Hr:visited,
        #MainContent_lnk_leavereport:link, #MainContent_lnk_leavereport:visited,
        #MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
        #MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
        #MainContent_lnk_ODApplication:link, #MainContent_lnk_ODApplication:visited,
        #MainContent_lnk_MyODApplication:link, #MainContent_lnk_MyODApplication:visited,
        #MainContent_lnk_InboxODApplication:link, #MainContent_lnk_InboxODApplication:visited,
        #MainContent_lnk_ProcessODApplication:link, #MainContent_lnk_ProcessODApplication:visited,
        #MainContent_lnk_WFH_Report:link, #MainContent_lnk_WFH_Report:visited,
        #MainContent_lnk_WFH_IPAddress:link, #MainContent_lnk_WFH_IPAddress:visited,
        #MainContent_lnk_EmpReport:link, #MainContent_lnk_EmpReport:visited,
        #MainContent_lnk_MyAttReport:link, #MainContent_lnk_MyAttReport:visited,
        #MainContent_lnk_leaveencashment:link, #MainContent_lnk_leaveencashment:visited {
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

        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
        #MainContent_lnk_LeaveRequest_FrmHR:hover, #MainContent_lnk_LeaveRequest_FrmHR:active,
        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
        #MainContent_lnk_HRLeaveInbox:hover, #MainContent_lnk_HRLeaveInbox:active,
        #MainContent_lnkempLeaveRpt:hover, #MainContent_lnkempLeaveRpt:active,
        #MainContent_lnk_leavereport_Hr:hover, #MainContent_lnk_leavereport_Hr:active,
        #MainContent_lnk_leavereport:hover, #MainContent_lnk_leavereport:active,
        #MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
        #MainContent_lnk_TeamCalendar:hover, #MainContent_lnk_TeamCalendar:active,
        #MainContent_lnk_ODApplication:hover, #MainContent_lnk_ODApplication:active,
        #MainContent_lnk_MyODApplication:hover, #MainContent_lnk_MyODApplication:active,
        #MainContent_lnk_InboxODApplication:hover, #MainContent_lnk_InboxODApplication:active,
        #MainContent_lnk_ProcessODApplication:hover, #MainContent_lnk_ProcessODApplication:active,
        #MainContent_lnk_WFH_Report:hover, #MainContent_lnk_WFH_Report:active,
        #MainContent_lnk_WFH_IPAddress:hover, #MainContent_lnk_WFH_IPAddress:active,
        #MainContent_lnk_EmpReport:hover, #MainContent_lnk_EmpReport:active,
        #MainContent_lnk_MyAttReport:hover, #MainContent_lnk_MyAttReport:active,
        #MainContent_lnk_leaveencashment:hover, #MainContent_lnk_leaveencashment:active {
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
                            <asp:Label ID="lblheading" runat="server" Text="Attendance"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="font-size: medium; font-weight: bold;"></asp:Label>
                            <table id="tbl_menu" runat="server">
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server" OnClick="lnk_leaverequest_Click">Record Attendance</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" OnClick="lnk_mng_leaverequest_Click">Regularize Attendance</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="
        padding-top: 1px;
        padding-bottom: 2px;
">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkempLeaveRpt" Visible="true" runat="server" ToolTip="Regularization Report" PostBackUrl="~/procs/MyInboxAttend_Req.aspx">My Regularization</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_LeaveRequest_FrmHR" Visible="false" runat="server" PostBackUrl="~/procs/Site_Attendance.aspx">Upload Site Attendance</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                     <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MyAttReport" runat="server" Text="Attendance Report " PostBackUrl="~/procs/EmployeeAttendanceReport.aspx">Attendance Report </asp:LinkButton>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_App_head" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approver: </span>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="Inbox " PostBackUrl="~/procs/InboxAttend_Req.aspx?itype=0">Inbox </asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport" runat="server" Text="Team Regularization" Visible="false" PostBackUrl="~/procs/AttendanceReport_Mgr.aspx">Team Regularization</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" id="lnkReport">
                                     <td class="formtitle">
                                        <asp:LinkButton ID="lnk_EmpReport" Visible="false" runat="server" Text="Employee Attendance Report " PostBackUrl="~/procs/EmployeeAttendanceRMReport.aspx">Team Attendance Report </asp:LinkButton>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_Hr_head" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Admin - HR: </span>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_HRLeaveInbox" runat="server" Visible="false" Text="HR Inbox" PostBackUrl="~/procs/InboxAttend_req.aspx?itype=1">HR Inbox</asp:LinkButton>
                                    </td>

                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport_Hr" runat="server" Text="Regularization Report - HR" Visible="false" PostBackUrl="~/procs/AttendanceReport_HR.aspx">Regularization Report - HR</asp:LinkButton>
                                    </td>

                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_TeamCalendar" Visible="false" runat="server" PostBackUrl="~/procs/TeamReport.aspx">Team Calendar</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" id="IsShow" runat="server"> 
                                        <br />
                                        <span id="span1" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">OD Application: </span>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ODApplication" runat="server" Text="OD Application" Visible="false" PostBackUrl="~/procs/WFO_OD_Application.aspx">OD Application </asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MyODApplication" runat="server" Text="My OD Applcation" Visible="false"  PostBackUrl="~/procs/MyODApplications.aspx">My OD Applcation</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_InboxODApplication" Visible="false" runat="server" Text="Inbox " PostBackUrl="~/procs/InboxODApplication.aspx">Inbox(0)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ProcessODApplication" Visible="false" runat="server" Text="My Process OD Application"  PostBackUrl="~/procs/MyODProcessRequest.aspx">My Process OD Application</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_WFH_Report" Visible="false" runat="server" Text="Employee OD Report " PostBackUrl="~/procs/WFH_WFOPMReport.aspx">Employee OD Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_WFH_IPAddress" Visible="false" runat="server" Text="Attendance Trace Report"  PostBackUrl="~/procs/WFHWFOIPAddressReport.aspx">Attendance Trace Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                </tr>
                                <%--                                <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>--%>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                </tr>
                            </table>
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
    <asp:HiddenField ID="hdnTypeId" runat="server" />


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
