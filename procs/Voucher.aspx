<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Voucher.aspx.cs" Inherits="Voucher" %>



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
           background:#ebebe4;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }

#MainContent_lnk_leaveParametersmst:link, #MainContent_lnk_leaveParametersmst1:link,
#MainContent_lnk_reimbursmentReport:link, #MainContent_lnk_approverPaymentInbox:link,
#MainContent_lnk_PayACC:link, #MainContent_lnk_Approved_ACC:link, #MainContent_lnk_PayCFO:link,
#MainContent_lnk_reimbursmentReport_3:link,#MainContent_Lnk_DownLoadReport:link, 
#MainContent_LINK_TallyCodeDept:link,#MainContent_LINK_TallyCodeLocation:link,
#MainContent_lnk_leaveParametersmst:visited,
#MainContent_lnk_leaveParametersmst1:visited, #MainContent_lnk_reimbursmentReport:visited,
#MainContent_lnk_approverPaymentInbox:visited, #MainContent_lnk_PayACC:visited, #MainContent_lnk_Approved_ACC:visited,
#MainContent_lnk_PayCFO:visited, #MainContent_lnk_reimbursmentReport_3:visited,#MainContent_Lnk_DownLoadReport:visited,
#MainContent_LINK_TallyCodeDept:visited, #MainContent_LINK_TallyCodeLocation:visited,#MainContent_lnk_ExpencessMapping:visited,
#MainContent_lnk_ExpencessMapping:link
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
#MainContent_Lnk_ApproveredPayment:visited,
#MainContent_Lnk_ApproveredPayment:link
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

        #MainContent_lnk_leaveParametersmst:hover, #MainContent_lnk_leaveParametersmst1:hover,
        #MainContent_lnk_reimbursmentReport:hover, #MainContent_lnk_approverPaymentInbox:hover,
        #MainContent_lnk_PayACC:hover, #MainContent_lnk_Approved_ACC:hover, #MainContent_lnk_PayCFO:hover,
        #MainContent_lnk_reimbursmentReport_3:hover, #MainContent_Lnk_DownLoadReport:hover,
        #MainContent_LINK_TallyCodeDept:hover, #MainContent_LINK_TallyCodeLocation:hover,
        #MainContent_lnk_leaveParametersmst:active,
        #MainContent_lnk_leaveParametersmst1:active, #MainContent_lnk_reimbursmentReport:active,
        #MainContent_lnk_approverPaymentInbox:active, #MainContent_lnk_PayACC:active, #MainContent_lnk_Approved_ACC:active,
        #MainContent_lnk_PayCFO:active, #MainContent_lnk_reimbursmentReport_3:active, #MainContent_Lnk_DownLoadReport:active,
        #MainContent_LINK_TallyCodeDept:active, #MainContent_LINK_TallyCodeLocation:active, 
        #MainContent_lnk_ExpencessMapping:active,#MainContent_lnk_ExpencessMapping:hover {
            /*background-color: #603F83;*/
            background-color: #3D1956;
            color: #C7D3D4 !important;
            border-color: #3D1956;
            border-width: 2pt;
            border-style: inset;
        }
        #MainContent_Lnk_ApproveredPayment:active,#MainContent_Lnk_ApproveredPayment:hover {
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
                            <asp:Label ID="lblheading" runat="server" Text="Payment Voucher"></asp:Label>
                        </span>
                    </div>
                    <div class="leavegrid">
                        <a href="https://ess.highbartech.com/hrms/Claims.aspx" class="aaa" >Claims Menu</a>
                     </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
                               <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveParametersmst" runat="server" 
										onClick="lnk_pvrequest_Click" Visible="True">Claim Payment Voucher</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveParametersmst1" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/MyPayments_Req.aspx">My Payment Vouchers</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/ClaimsReport_Voucher.aspx">Report - Self</asp:LinkButton>
                                    </td>
                                     <td class="formtitle">
                                        <asp:LinkButton ID="Lnk_DownLoadReport" runat="server"  OnClick="Lnk_DownLoadReport_Click"  Visible="True" Text="" >DownLoad Report Voucher</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="                                        padding-top: 1px;
                                        padding-bottom: 2px;
                                ">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LINK_TallyCodeDept" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/TallyCodeDept.aspx">Add Tally Code for Departments</asp:LinkButton>
                                    </td>
                                     <td class="formtitle">
                                        <asp:LinkButton ID="LINK_TallyCodeLocation" runat="server"   Visible="True" Text="" PostBackUrl="~/procs/TallyCodeLocation.aspx" >Add Tally Code for Locations</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                         <asp:LinkButton ID="lnk_ExpencessMapping" Visible="false" runat="server" Text="Expense-Employee Mapping"  PostBackUrl="~/procs/EmployeeExpProjectMapping.aspx">Expense-Employee Mapping</asp:LinkButton>

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
                                        <asp:LinkButton ID="lnk_approverPaymentInbox" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/InboxPayments.aspx?app=APP">Inbox (Payment Voucher)</asp:LinkButton>
                                    </td>
                                       <td class="formtitle">
                                        <asp:LinkButton ID="Lnk_ApproveredPayment" runat="server"  Visible="false" PostBackUrl="~/procs/ApprovedInboxPayments.aspx?app=APP" Text="Approved Payment Voucher">Approved Payment Voucher</asp:LinkButton>
                                    </td>
                                </tr>
                                
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_acc_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - Accounts: </span>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_PayACC" runat="server" Text=""  PostBackUrl="~/procs/InboxPayments.aspx?app=RACC">Inbox ACC (Payment Voucher)</asp:LinkButton>
                                        <asp:LinkButton ID="lnk_PayCFO" runat="server" Text=""  PostBackUrl="~/procs/InboxPayments.aspx?app=RCFO">Inbox CFO (Payment Voucher)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport_3" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/PVreimbursmentReport_Audit.aspx">Payment Vouchers Report - Accounts</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Approved_ACC" runat="server" Text=""  PostBackUrl="~/procs/InboxPayments_Arch.aspx?app=RACC">Approved Vouchers</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
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
