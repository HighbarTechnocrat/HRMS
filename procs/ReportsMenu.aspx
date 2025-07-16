<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="ReportsMenu.aspx.cs" Inherits="ReportsMenu" %>



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
        #MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited,
        #MainContent_lnk_reimbursmentReport:link, #MainContent_lnk_reimbursmentReport:visited,
        #MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited,
        #MainContent_lnk_MobACC:link, #MainContent_lnk_MobACC:visited,
        #MainContent_Lnk_BenchList:link, #MainContent_Lnk_BenchList:visited,
        #MainContent_lnk_MobCFO:link, #MainContent_lnk_MobPastApproved_ACC:link, #MainContent_lnk_MobCFO:visited, #MainContent_lnk_MobPastApproved_ACC:visited,
        #MainContent_lnk_reimbursmentReport_1:link, #MainContent_lnk_reimbursmentReport_1:visited,
        #MainContent_lnk_ITAssetSummaryRpt:link, #MainContent_lnk_ITAssetSummaryRpt:visited,
        #MainContent_lnk_LOP:link, #MainContent_lnk_LOP:visited, #MainContent_lnk_Per_Diem:link, #MainContent_lnk_Per_Diem:visited,
        #MainContent_lnk_Insurance_Report:link, #MainContent_lnk_Insurance_Report:visited, #MainContent_EmpReport:link, #MainContent_EmpReport:visited,
        #MainContent_lnk_AppointLtrIssuedRpt:link, #MainContent_lnk_AppointLtrIssuedRpt:visited,
        #MainContent_lnk_ELC:link, #MainContent_lnk_ELC:visited,
        #MainContent_lnkSalaryUpdateReport:link, #MainContent_Link_lnkSalaryUpdateReport:visited,
        #MainContent_lnkCTCBreackupReport:link, #MainContent_lnkCTCBreackupReport:visited,
        #MainContent_lnk_ContractExpiryReport:link, #MainContent_lnk_ContractExpiryReport:visited,
        #MainContent_lnk_ExitIntrview:link, #MainContent_lnk_ExitIntrview:visited,
        #MainContent_lnk_ExitSurvey:link, #MainContent_lnk_ExitSurvey:visited,
        #MainContent_lnk_FuelSummary:link, #MainContent_lnk_FuelSummary:visited,
        #MainContent_lnk_OnboardingSeparation_MRMReport:link, #MainContent_lnk_OnboardingSeparation_MRMReport:visited
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

        #MainContent_lnk_leaverequest:hover, #MainContent_lnk_leaverequest:active,
        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active,
        #MainContent_Lnk_BenchList:hover, #MainContent_Lnk_BenchList:active,
        #MainContent_lnk_reimbursmentReport:hover, #MainContent_lnk_reimbursmentReport:active,
        #MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active,
        #MainContent_lnk_MobACC:hover, #MainContent_lnk_MobACC:active,
        #MainContent_lnk_MobCFO:hover, #MainContent_lnk_MobPastApproved_ACC:hover, #MainContent_lnk_MobCFO:active, #MainContent_lnk_MobPastApproved_ACC:active,
        #MainContent_lnk_reimbursmentReport_1:hover, #MainContent_lnk_reimbursmentReport_1:active, #MainContent_lnk_ITAssetSummaryRpt:hover, #MainContent_lnk_ITAssetSummaryRpt:active
        #MainContent_lnk_LOP:hover, #MainContent_lnk_LOP:active,
        #MainContent_lnk_Per_Diem:hover, #MainContent_lnk_Per_Diem:active,
        #MainContent_lnk_Insurance_Report:hover, #MainContent_lnk_Insurance_Report:active, #MainContent_EmpReport:hover, #MainContent_EmpReport:active,
        #MainContent_lnk_AppointLtrIssuedRpt:hover, #MainContent_lnk_AppointLtrIssuedRpt:active,
        #MainContent_lnk_ELC:hover, #MainContent_lnk_ELC:active,
        #MainContent_lnkSalaryUpdateReport:hover, #MainContent_lnkSalaryUpdateReport:active,
        #MainContent_lnkCTCBreackupReport:hover, #MainContent_lnkCTCBreackupReport:active,
        #MainContent_lnk_ContractExpiryReport:hover, #MainContent_lnk_ContractExpiryReport:active,
        #MainContent_lnk_ExitSurvey:hover, #MainContent_lnk_ExitSurvey:active,
        #MainContent_lnk_ExitIntrview:hover, #MainContent_lnk_ExitIntrview:active,
        #MainContent_lnk_FuelSummary:hover, #MainContent_lnk_FuelSummary:active,
        #MainContent_lnk_OnboardingSeparation_MRMReport:hover, #MainContent_lnk_OnboardingSeparation_MRMReport:active
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
                            <asp:Label ID="lblheading" runat="server" Text="Reports"></asp:Label>
                        </span>
                    </div>
                    <div class="leavegrid">
                        <a href="https://ess.highbartech.com/hrms/PersonalDocuments.aspx" class="aaa">My Corner</a>
                    </div>
                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
                            <table>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" Visible="false" runat="server" PostBackUrl="~/OrgStructure.aspx">Reporting Structure</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton3" Visible="false" runat="server" PostBackUrl="~/procs/OrgEmployee_Report.aspx">Employee Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" Visible="false" runat="server" PostBackUrl="~/procs/OrgEmployee_Report.aspx">Employee Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton1" Visible="false" runat="server" PostBackUrl="~/procs/OrgEmployee_Report.aspx">Employee Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <%--<td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/MIS_OnboardingSeparation.aspx">Onboarding & Separation Summary Report</asp:LinkButton>
                                    </td>--%>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/MIS_OnboardingSeparationNew.aspx">Onboarding & Separation Summary Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton2" Visible="false" runat="server" PostBackUrl="~/procs/OrgEmployee_Report.aspx">Employee Report</asp:LinkButton>
                                    </td>
                                </tr>
                                 <tr id="trOnboardingSeparation_MRMReport" runat="server" visible="false">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_OnboardingSeparation_MRMReport" runat="server" Text="Onboarding & Separation MRM Report" PostBackUrl="~/procs/MIS_OnboardingDynamic.aspx">Onboarding & Separation MRM Report</asp:LinkButton>
                                    </td>                                    
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Per_Diem" runat="server" Visible="false" Text="Per-diem & Travel Expenses Report" PostBackUrl="~/procs/PerDiemTravelExpReport.aspx">Per-diem & Travel Expenses Report</asp:LinkButton>
                                    </td>
                                    <%--<td class="formtitle">
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="" PostBackUrl="~/procs/InboxResignation_Arch.aspx?app=HRML">Archieved Resignations</asp:LinkButton>
                                    </td>--%>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_LOP" runat="server" Visible="false" Text="LOP Employee Report" PostBackUrl="~/procs/LOP_EmployeeReportSeparation.aspx">LOP Employee Report</asp:LinkButton>
                                    </td>
                                    <%--<td class="formtitle">
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text="" PostBackUrl="~/procs/InboxResignation_Arch.aspx?app=HRML">Archieved Resignations</asp:LinkButton>
                                    </td>--%>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ITAssetSummaryRpt" Visible="false" runat="server" Text="" PostBackUrl="~/procs/ITAssetSummary.aspx">IT Asset Summary Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Insurance_Report" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Insurance_Reminder_Report.aspx">Reminder Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="EmpReport" Visible="false" runat="server" PostBackUrl="~/procs/EmployeeDetailsReport.aspx">Employee Details Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_AppointLtrIssuedRpt" Visible="false" runat="server" Text="" PostBackUrl="~/procs/Appointment_Letter_Issued_Report.aspx">Appointment Letter Issued Status Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ELC" Visible="false" runat="server" Text="" PostBackUrl="~/procs/EmployeeList_ELC.aspx">Employee Lifecycle</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkSalaryUpdateReport" Visible="false" runat="server" Text="" PostBackUrl="~/procs/SalaryApprovalReport.aspx">Salary Update Status Report</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnkCTCBreackupReport" Visible="false" runat="server" Text="" PostBackUrl="~/procs/CTC_BreackupReport.aspx">CTC Breakup Report</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="Lnk_BenchList" runat="server" Text="" PostBackUrl="~/procs/BenchListReport.aspx">Bench List Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <%--                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <a  href="https://ess.highbartech.com/hrms/procs/How to Submit Fuel Reimbursement.pdf" title="hrms- How to Apply Fuel Reimbursement" class="LeaveManualLnik" target="_blank">hrms- How to Apply Fuel Reimbursement</a>
                                    </td>
                                </tr>--%>
                                <tr style="padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ContractExpiryReport" runat="server" Text="Contract Expiry Report" Visible="false" PostBackUrl="~/procs/ContractExpiryReport.aspx">Contract Expiry Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ExitSurvey" runat="server" Text="Employee Exit Survey Report" Visible="false" PostBackUrl="~/procs/ExitProcessReport.aspx">Employee Exit Survey Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_ExitIntrview" runat="server" Text="Employee Exit Intrview Report" Visible="false" PostBackUrl="~/procs/ExitInterviewProcessReport.aspx">Employee Exit Interview Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_FuelSummary" runat="server" Text="Fuel Claim Summary Report" Visible="false" PostBackUrl="~/procs/FuelSummaryReport.aspx">Fuel Claim Summary Report</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
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
