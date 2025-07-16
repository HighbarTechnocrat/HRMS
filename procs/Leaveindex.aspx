<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Leaveindex.aspx.cs" Inherits="Leaveindex" %>



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

#MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited {
  background-color: #3D1956;
  color: #F28820 !important;
  border-radius: 10px;
  /*color: white;*/
  padding: 25px 5px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  width:90% !important;
}

#MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active {
  background-color: #F28820;
  color: #3D1956 !important;
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
                            <asp:Label ID="lblheading" runat="server" Text="Leave Module"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server" OnClick="lnk_leaverequest_Click">Apply Leave</asp:LinkButton>                                         
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_LeaveRequest_FrmHR" Visible="false" runat="server" PostBackUrl="~/procs/Leave_Req_Hr.aspx">Leaves From HR</asp:LinkButton>                                         
                                    </td>
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" OnClick="lnk_mng_leaverequest_Click">My Leaves</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_HRLeaveInbox" runat="server" Text="HR Inbox" PostBackUrl="~/procs/InboxLeave_Req.aspx?itype=1">HR Inbox</asp:LinkButton>
                                    </td>
                                </tr>
                                
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkempLeaveRpt" Visible="true" runat="server" ToolTip="Leave Report"  PostBackUrl="~/procs/LeavesReport.aspx">My Leave Report</asp:LinkButton>                                                
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport" runat="server" Text="Leave Report (HR)" Visible="false" PostBackUrl="~/procs/LeavesReport_Hr.aspx">Leave Report(HR)</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="Inbox " PostBackUrl="~/procs/InboxLeave_Req.aspx?itype=0">Inbox </asp:LinkButton>
                                    </td>
                                    <td class="formtitle">                                            
                                      <asp:LinkButton ID="lnk_TeamCalendar" runat="server" PostBackUrl="~/procs/TeamReport.aspx">Team Calendar</asp:LinkButton>                                                
                                    </td>
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveencashment" Visible="false" runat="server" PostBackUrl="~/procs/Encash_leave.aspx">Leave Encashment</asp:LinkButton>                                         
                                    </td>
                                </tr>

                                  <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_EmpAccessReport"  Visible="false"  runat="server" PostBackUrl="~/procs/EmpAccessReport.aspx">Employee Portal Access Report</asp:LinkButton>                                         
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
                                <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>
<%--                                <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>--%>

                                 <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>
                                 <tr style="padding-top:1px;padding-bottom:2px;">

                                </tr>
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

                

                <%--<div class="index">
                    <ul>

                        <li>
                            <asp:LinkButton ID="lnk_leaverequest1" runat="server" PostBackUrl="~/procs/Leave_Req.aspx">Leaves</asp:LinkButton>
                        </li>

                         <li>
                            <asp:LinkButton ID="lnk_mng_leaverequest1" runat="server" PostBackUrl="~/procs/MyLeave_Req.aspx">My Leaves</asp:LinkButton>
                        </li>

                        <li>
                            <asp:LinkButton ID="lnk_Attendancereg1" runat="server" PostBackUrl="~/procs/Attendence.aspx">Regularize Attendence</asp:LinkButton>
                        </li>
                     
                        <li>
                            <asp:LinkButton ID="lnk_mng_Attendancereg1" runat="server" PostBackUrl="~/procs/MngAttendanceRequest.aspx">Manage Attendance regularization</asp:LinkButton>
                        </li>

                    </ul>
                </div>--%>


                <%--<div class="index">
                    <ul>
                        <li>
                            <asp:LinkButton ID="lnk_leaveinbox1" runat="server" Text="Inbox (Leaves)" PostBackUrl="~/procs/InboxLeave_Req.aspx">Inbox (Leaves)</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_leaveParametersmst1" runat="server" Text="Leave Parameters Master">Leave Parameters Master</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_leavereport1" runat="server" Text="Leave Report " PostBackUrl="~/procs/TravelRequest.aspx">Leave Report</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_attendanceinbox1" runat="server" Text="Inbox (attendance regularization requests)" PostBackUrl="~/procs/InboxAttendance.aspx" >Inbox (attendance regularization)</asp:LinkButton>
                        </li>
                    </ul>
                </div>--%>


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
