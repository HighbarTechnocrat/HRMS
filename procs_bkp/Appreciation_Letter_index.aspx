<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Appreciation_Letter_index.aspx.cs" Inherits="Appreciation_Letter_index" %>


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
        #MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
        #MainContent_lnk_reimbursmentReport:link, #MainContent_lnk_reimbursmentReport:visited,
        #MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
        #MainContent_lnk_MobACC:link, #MainContent_lnk_MobACC:visited,
        #MainContent_lnk_MobCFO:link, #MainContent_lnk_MobPastApproved_ACC:link, #MainContent_lnk_MobCFO:visited, #MainContent_lnk_MobPastApproved_ACC:visited,
        #MainContent_lnk_reimbursmentReport_1:link, #MainContent_lnk_reimbursmentReport_1:visited,
        #MainContent_lnk_summary_report:link, #MainContent_lnk_summary_report:visited,
        #MainContent_lnk_CustomerFirstReport:link, #MainContent_lnk_CustomerFirstReport:visited,
        #MainContent_lnk_CustomerFirstView:link, #MainContent_lnk_CustomerFirstView:visited,
        #MainContent_lnk_Inbox_Payment_Request:link, #MainContent_lnk_Inbox_Payment_Request:visited,
        #MainContent_lnk_Index_Acc_Invoices:link, #MainContent_lnk_Index_Acc_Invoices:visited,
        #MainContent_lnk_Index_Acc_Payment_Requests:link, #MainContent_lnk_Index_Acc_Payment_Requests:visited,
        #MainContent_lnk_Index_Acc_Batch_Approval:link, #MainContent_lnk_Index_Acc_Batch_Approval:visited,
        #MainContent_lnk_Index_Acc_Batch_Requests:link, #MainContent_lnk_Index_My_Batch_Requests:link, #MainContent_lnk_Index_Acc_Batch_Requests:visited,
        #MainContent_lnk_Index_My_Batch_Requests:visited,
        #MainContent_lnk_Inbox_Payment_View:link, #MainContent_lnk_Inbox_Payment_View:visited,
        #MainContent_lnk_Approved_Invoice:link, #MainContent_lnk_lnk_Approved_Invoice:visited,
        #MainContent_lnk_Deemed_Approval:link, #MainContent_lnk_lnk_Deemed_Approval:visited,
        #MainContent_lnk_Payment_Request_withOutPO:link, #MainContent_lnk_Payment_Request_withOutPO:visited,
        #MainContent_lnk_Index_myapprovedBatchList:link, #MainContent_lnk_Index_myapprovedBatchList:visited,
        #MainContent_lnk_Reports:link, #MainContent_lnk_Reports:visited {
            background-color: #C7D3D4;
            color: #603F83 !important;
            border-radius: 10px;
            /*color: white;  */
            padding: 25px 5px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            width: 90% !important;
        }

        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
        #MainContent_lnk_reimbursmentReport:hover, #MainContent_lnk_reimbursmentReport:active,
        #MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
        #MainContent_lnk_MobACC:hover, #MainContent_lnk_MobACC:active,
        #MainContent_lnk_MobCFO:hover, #MainContent_lnk_MobPastApproved_ACC:hover, #MainContent_lnk_MobCFO:active, #MainContent_lnk_MobPastApproved_ACC:active,
        #MainContent_lnk_reimbursmentReport_1:hover, #MainContent_lnk_reimbursmentReport_1:active,
        #MainContent_lnk_summary_report:hover, #MainContent_lnk_summary_report:active,
        #MainContent_lnk_CustomerFirstReport:hover, #MainContent_lnk_CustomerFirstReport:active,
        #MainContent_lnk_CustomerFirstView:hover, #MainContent_lnk_CustomerFirstView:active,
        #MainContent_lnk_Inbox_Payment_Request:hover, #MainContent_lnk_Inbox_Payment_Request:active,
        #MainContent_lnk_Index_Acc_Invoices:hover, #MainContent_lnk_Index_Acc_Invoices:active,
        #MainContent_lnk_Index_Acc_Payment_Requests:hover, #MainContent_lnk_Index_Acc_Payment_Requests:active,
        #MainContent_lnk_Index_Acc_Batch_Approval:hover, #MainContent_lnk_Index_Acc_Batch_Approval:active,
        #MainContent_lnk_Index_Acc_Batch_Requests:hover, #MainContent_lnk_Index_Acc_Batch_Requests:active,
        #MainContent_lnk_Index_My_Batch_Requests:hover, #MainContent_lnk_Index_My_Batch_Requests:active,
        #MainContent_lnk_Inbox_Payment_View:hover, #MainContent_lnk_Inbox_Payment_View:active,
        #MainContent_lnk_Approved_Invoice:hover, #MainContent_lnk_lnk_Approved_Invoice:active,
        #MainContent_lnk_Deemed_Approval:hover, #MainContent_lnk_lnk_Deemed_Approval:active,
        #MainContent_lnk_Payment_Request_withOutPO:hover, #MainContent_lnk_Payment_Request_withOutPO:active,
        #MainContent_lnk_Index_myapprovedBatchList:hover, #MainContent_lnk_Index_myapprovedBatchList:active,
        #MainContent_lnk_Reports:hover, #MainContent_lnk_Reports:active {
            /*background-color: #603F83; lnk_Approved_Invoice */
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
                        <br />
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Appreciation Letter"></asp:Label>
                        </span>
                    </div>
                    <div>
                        <span>
                            <a href="https://ess.highbartech.com/hrms/PersonalDocuments.aspx" class="aaaa">My Corner</a>
                        </span>
                    </div>
                    <%--  <div class="leavegrid">
                        <a href="http://localhost/hrms/Service.aspx" class="aaa" >Service Request Menu</a>
                     </div>--%>
                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="true" Style="margin-left: 135px"></asp:Label>
                            <table>
                                <%--Employee Update Photo--%>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" id="td1" runat="server">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Visible="false" ToolTip="Send Appreciation Letter" PostBackUrl="~/procs/Appreciation_Letter.aspx">Send Appreciation Letter</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MobACC" runat="server" Visible="false" ToolTip="View Appreciation Letter" PostBackUrl="~/procs/View_Appreciation_Letter.aspx">View Appreciation Letter</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" id="td2" runat="server">
                                        <asp:LinkButton ID="lnk_MobCFO" runat="server" Visible="true" ToolTip="Received Appreciation Letter" PostBackUrl="~/procs/Appreciation_to_employee.aspx">Received Appreciation Letter</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport_1" runat="server" Visible="false" ToolTip="Appreciation Letter Report" PostBackUrl="~/procs/Send_Appreciationletter_report.aspx">Send Appreciation Letter Report</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server" Visible="false" ToolTip="Appreciation Letter Report" PostBackUrl="~/procs/Appreciation_Letter_Report.aspx">Appreciation Letter Report</asp:LinkButton>

                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Index_Acc_Invoices" runat="server" Visible="false" ToolTip="Created Appreciation Letter" PostBackUrl="~/procs/Appreciation_Letter_Draft.aspx">Created Appreciation Letter</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Deemed_Approval" runat="server" Visible="false" ToolTip="All Appreciation Letter" PostBackUrl="~/procs/Appreciation_old_letter.aspx">All Appreciation Letter</asp:LinkButton>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
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

