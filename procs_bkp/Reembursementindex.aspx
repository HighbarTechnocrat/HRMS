<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Reembursementindex.aspx.cs" Inherits="Reembursementindex" %>



<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
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
                    <!--<div class="myaccount" style="display: none;">
                        <div class="myaccountheading">My Account</div>
                        <div class="myaccountlist">
                            <ul>
                                <li class="listselected"><a href="<%=ReturnUrl("sitepathmain") %>procs/leaveindex" title="Edit Profile"><b>Edit Profile</b></a></li>
                                <li><a href="<%=ReturnUrl("sitepathmain") %>procs/wishlist" title="Favorites">Favorites</a></li>
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/preference" title="Preference">Preference</a></li>

                                    <li id="lihistory" runat="server"><a href="<%=ReturnUrl("sitepathmain") %>procs/subscriptionhistory" title="Subscription History">Subscription History</a></li>
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>procs/pthistory" title=" Reward Points">Reward Points</a></li>

                                    <li><a href="<%=ReturnUrl("sitepathmain") %>recommend" title="Recommendation">Recommendation</a></li>
                                </asp:Panel>
                                <li>
                                    <asp:LinkButton ID="btnSingOut" runat="server" ToolTip="Logout"
                                        Text="Logout"> </asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                        <div class="myaccountlist-mobiletab">
                            <asp:DropDownList ID="ddlprofile" runat="server" AutoPostBack="true">
                                <asp:ListItem Selected="True" Value="edit">Edit Profile</asp:ListItem>
                                <asp:ListItem Value="pwd">Change Password</asp:ListItem>
                                <asp:ListItem Value="wishlist">Favorites</asp:ListItem>
                                <asp:ListItem Value="preference">preference</asp:ListItem>
                                <asp:ListItem Value="subscription">Subscription History</asp:ListItem>
                                <asp:ListItem Value="pthistory">Reward Points</asp:ListItem>
                                <asp:ListItem Value="logout">Logout</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>-->
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Claims"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server"  OnClick="lnk_leaverequest_Click">Claim Mobile Bill</asp:LinkButton>                                         
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" PostBackUrl="~/procs/MyMobile_Req.aspx">My Mobile Claims</asp:LinkButton>
                                        
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Attendancereg" runat="server"   OnClick="lnk_Attendancereg_Click">Fuel Reimbursement</asp:LinkButton>
                                        
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_Attendancereg" runat="server"  PostBackUrl="~/procs/MyFuel_Req.aspx">Manage Fuel Reimbursement</asp:LinkButton>
                                    </td>
                                </tr>
<%--                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle"></td>
                                </tr>--%>
                               <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveParametersmst" runat="server" 
										onClick="lnk_pvrequest_Click" Visible="True">Payment Voucher</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveParametersmst1" runat="server"  Visible="True" Text="Manage Payment Voucher" PostBackUrl="~/procs/MyPayments_Req.aspx">Manage Payment Voucher</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/ClaimsReport.aspx">Reimbursement Report</asp:LinkButton>
                                    </td>
                                </tr>

                                  <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport_1" runat="server"  Visible="True" Text="Mobile Reimbursement Report" PostBackUrl="~/procs/Mobile_Report.aspx">Mobile Reimbursement Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle"> <%--Fuel_Report--%>
                                        <asp:LinkButton ID="lnk_reimbursmentReport_2" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/Fuel_Report_Monthly.aspx">Fuel Reimbursement Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport_3" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/PVreimbursmentReport.aspx">Payment Voucher Reimbursement Report</asp:LinkButton>
                                    </td>
                                </tr>


                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="Inbox (leave requests)" PostBackUrl="~/procs/InboxMobile.aspx?app=APP">Inbox (Mobile Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                  <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_approverFuleInbox" runat="server"  Visible="True" Text="Leave Parameters Master" PostBackUrl="~/procs/InboxFuel.aspx?app=APP">Inbox (Fuel Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                  <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_approverPaymentInbox" runat="server"  Visible="True" Text="Inbox (Payment Voucher)" PostBackUrl="~/procs/InboxPayments.aspx?app=APP">Inbox (Payment Voucher)</asp:LinkButton>
                                    </td>
                                </tr>
<%--                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkIbox_mobCOS" runat="server" Text="Inbox (leave requests)" PostBackUrl="~/procs/InboxMobile.aspx?app=RCOS">Inbox COS (Mobile Claims)</asp:LinkButton>
                                    </td>
                                </tr>--%>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_COSFuel" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxFuel.aspx?app=RCOS">Inbox COS (Fuel Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MobACC" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxMobile.aspx?app=RACC">Inbox ACC (Mobile Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ACCFuel" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxFuel.aspx?app=RACC">Inbox ACC (Fuel Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_PayACC" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxPayments.aspx?app=RACC">Inbox ACC (Payment Voucher)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MobCFO" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxMobile.aspx?app=RCFO">Inbox CFO (Mobile Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_PayCFO" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxPayments.aspx?app=RCFO">Inbox CFO (Payment Voucher)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MobPastApproved_ACC" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxMobile_Arch.aspx?app=RACC">Inbox ACC (Mobile Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_FuelPastApproved_ACC" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxFuel_Arch.aspx?app=RACC">Inbox ACC (Fuel Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_FuelPastApproved_COS" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxFuel_Arch.aspx?app=RCOS">Inbox ACC (Fuel Claims)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_PayPastApproved_ACC" runat="server" Text="Inbox (leave requests)"  PostBackUrl="~/procs/InboxPayments_Arch.aspx?app=RACC">Inbox ACC (Payment Voucher)</asp:LinkButton>
                                    </td>
                                </tr>
                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <a  href="http://localhost/hrms/procs/How to Submit Fuel Reimbursement.pdf" title="hrms- How to Apply Fuel Reimbursement" class="LeaveManualLnik" target="_blank">hrms- How to Apply Fuel Reimbursement</a>
                                        
                                    </td>
                                </tr>
                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Recruitment" runat="server" Text="Leave Report " Visible="false" PostBackUrl="~/procs/RecruitmentsPositions.aspx">Recruitment</asp:LinkButton>
                                    </td>
                                </tr>

                                 

                           <%--     <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leavereport" runat="server" Text="Leave Report " Visible="false" PostBackUrl="~/procs/TravelRequest.aspx">Leave Report</asp:LinkButton>
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
                            <asp:LinkButton ID="lnk_leaverequest1" runat="server" PostBackUrl="~/procs/Leave_Req.aspx">Leave Requests</asp:LinkButton>
                        </li>

                         <li>
                            <asp:LinkButton ID="lnk_mng_leaverequest1" runat="server" PostBackUrl="~/procs/MyLeave_Req.aspx">Manage Leave Requests</asp:LinkButton>
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
                            <asp:LinkButton ID="lnk_leaveinbox1" runat="server" Text="Inbox (leave requests)" PostBackUrl="~/procs/InboxLeave_Req.aspx">Inbox (leave requests)</asp:LinkButton>
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

    <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />
    

    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnClaimDate" runat="server" />


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
