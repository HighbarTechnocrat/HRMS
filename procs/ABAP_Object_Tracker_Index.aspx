<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="ABAP_Object_Tracker_Index.aspx.cs" Inherits="ABAP_Object_Tracker_Index" %>

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
        #MainContent_lnkuploadplan:link, #MainContent_lnkuploadplan:visited,
        #MainContent_lnkPendingList:link, #MainContent_lnkPendingList:visited,
        #MainContent_lnknotsubmitList:link, #MainContent_lnknotsubmitList:visited,
        #MainContent_lnkApprovedList:link, #MainContent_lnkApprovedList:visited,
        #MainContent_lnkAllAbapList:link, #MainContent_lnkAllAbapList:visited,
        #MainContent_lnkfschange:link, #MainContent_lnkfschange:visited,
        #MainContent_lnkfschanged:link, #MainContent_lnkfschanged:visited,
        #MainContent_lnkabapchange:link, #MainContent_lnkabapchange:visited,
        #MainContent_lnkabapchanged:link, #MainContent_lnkabapchanged:visited,
        #MainContent_lnkctmchange:link, #MainContent_lnkctmchange:visited,
        #MainContent_lnkctmchanged:link, #MainContent_lnkctmchanged:visited,
        #MainContent_lnkinboxapproval:link, #MainContent_lnkinboxapproval:visited,
        #MainContent_lnkApproved:link, #MainContent_lnkApproved:visited,
        #MainContent_lnkinboxsignoffappr:link, #MainContent_lnkinboxsignoffappr:visited,
        #MainContent_lnkhbtsingoffApproval:link, #MainContent_lnkhbtsingoffApproval:visited,
        #MainContent_lnkUpdateStatusRGSFSABAPTest:link, #MainContent_lnkUpdateStatusRGSFSABAPTest:visited,
        #MainContent_lnkUpdateStatusRGS:link, #MainContent_lnkUpdateStatusRGS:visited,
        #MainContent_lnkUpdateStatusFS:link, #MainContent_lnkUpdateStatusFS:visited,
        #MainContent_lnkUpdateStatusHBTTesting:link, #MainContent_lnkUpdateStatusHBTTesting:visited,
        #MainContent_lnkUpdateStatusCTMTesting:link, #MainContent_lnkUpdateStatusCTMTesting:visited,
        #MainContent_lnkUpdateStatusABAP:link, #MainContent_lnkUpdateStatusABAP:visited,
        #MainContent_lnkUploadSourceCode:link, #MainContent_lnkUploadSourceCode:visited,
        #MainContent_lnkUpdateAcceptNotAcceptABAP:link, #MainContent_lnkUpdateAcceptNotAcceptABAP:visited,
        #MainContent_lnkhbttestcaseuploadapproval:link, #MainContent_lnkhbttestcaseuploadapproval:visited,
        #MainContent_lnkUploadTestCasesCTMTesting:link, #MainContent_lnkUploadTestCasesCTMTesting:visited,
        #MainContent_lnkCTMTestCaseUploadApproval:link, #MainContent_lnkCTMTestCaseUploadApproval:visited,
        #MainContent_lnkRGSStageApproval:link, #MainContent_lnkRGSStageApproval:visited,
        #MainContent_Link_ChangeStatus_UATSignoff:link, #MainContent_Link_ChangeStatus_UATSignoff:visited,
        #MainContent_Link_ChangeStatus_GoLive:link, #MainContent_Link_ChangeStatus_GoLive:visited,
        #MainContent_lnkViewABAPObjectPlan:link, #MainContent_lnkViewABAPObjectPlan:visited,
        #MainContent_lnkRemoveABAPObject:link, #MainContent_lnkRemoveABAPObject:visited,
        #MainContent_lnk_AuditReport:link, #MainContent_lnk_AuditReport:visited,
        #MainContent_lnk_DelayReport:link, #MainContent_lnk_DelayReport:visited,
        #MainContent_lnk_DetailSummaryReport:link, #MainContent_lnk_DetailSummaryReport:visited,
        #MainContent_lnk_treeview:link, #MainContent_lnk_treeview:visited,
        #MainContent_lnkhbttestcaseupload:link, #MainContent_lnkhbttestcaseupload:visited {
            background-color: #C7D3D4;
            color: #603F83 !important;
            border-radius: 10px;
            padding: 25px 5px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            width: 90% !important;
        }


        #MainContent_lnk_ResigForm:hover, #MainContent_lnk_ResigForm:active,
        #MainContent_lnk_MyResig:hover, #MainContent_lnk_MyResig:active,
        #MainContent_lnkuploadplan:hover, #MainContent_lnkuploadplan:active,
        #MainContent_lnkPendingList:hover, #MainContent_lnkPendingList:active,
        #MainContent_lnkPendingList:hover, #MainContent_lnkPendingList:active,
        #MainContent_lnkApprovedList:hover, #MainContent_lnkApprovedList:active,
        #MainContent_lnkAllAbapList:hover, #MainContent_lnkAllAbapList:active,
        #MainContent_lnkfschange:hover, #MainContent_lnkfschange:active,
        #MainContent_lnkfschanged:hover, #MainContent_lnkfschanged:active,
        #MainContent_lnkabapchange:hover, #MainContent_lnkabapchange:active,
        #MainContent_lnkabapchanged:hover, #MainContent_lnkabapchanged:active,
        #MainContent_lnkctmchange:hover, #MainContent_lnkctmchange:active,
        #MainContent_lnkctmchanged:hover, #MainContent_lnkctmchanged:active,
        #MainContent_lnkinboxapproval:hover, #MainContent_lnkinboxapproval:active,
        #MainContent_lnkApproved:hover, #MainContent_lnkApproved:active,
        #MainContent_lnkinboxsignoffappr:hover, #MainContent_lnkinboxsignoffappr:active,
        #MainContent_lnkhbtsingoffApproval:hover, #MainContent_lnkhbtsingoffApproval:active
        #MainContent_lnkUpdateStatusRGSFSABAPTest:hover, #MainContent_lnkUpdateStatusRGSFSABAPTest:active,
        #MainContent_lnkUpdateStatusRGS:hover, #MainContent_lnkUpdateStatusRGS:active,
        #MainContent_lnkUpdateStatusFS:hover, #MainContent_lnkUpdateStatusFS:active,
        #MainContent_lnkUpdateStatusHBTTesting:hover, #MainContent_lnkUpdateStatusHBTTesting:active,
        #MainContent_lnkUpdateStatusCTMTesting:hover, #MainContent_lnkUpdateStatusCTMTesting:active,
        #MainContent_lnkUpdateStatusABAP:hover, #MainContent_lnkUpdateStatusABAP:active,
        #MainContent_lnkUploadSourceCode:hover, #MainContent_lnkUploadSourceCode:active,
        #MainContent_lnkUpdateAcceptNotAcceptABAP:hover, #MainContent_lnkUpdateAcceptNotAcceptABAP:active,
        #MainContent_lnkhbttestcaseuploadapproval:hover, #MainContent_lnkhbttestcaseuploadapproval:active,
        #MainContent_lnkUploadTestCasesCTMTesting:hover, #MainContent_lnkUploadTestCasesCTMTesting:active,
        #MainContent_lnkCTMTestCaseUploadApproval:hover, #MainContent_lnkCTMTestCaseUploadApproval:active,
        #MainContent_lnkRGSStageApproval:hover, #MainContent_lnkRGSStageApproval:active,
        #MainContent_Link_ChangeStatus_GoLive:hover, #MainContent_Link_ChangeStatus_GoLive:active,
        #MainContent_Link_ChangeStatus_UATSignoff:hover, #MainContent_Link_ChangeStatus_UATSignoff:active,
        #MainContent_lnk_AuditReport:hover, #MainContent_lnk_AuditReport:active,
        #MainContent_lnk_DelayReport:hover, #MainContent_lnk_DelayReport:active,
        #MainContent_lnk_DetailSummaryReport:hover, #MainContent_lnk_DetailSummaryReport:active,
        #MainContent_lnk_treeview:hover, #MainContent_lnk_treeview:active,
        #MainContent_lnkViewABAPObjectPlan:hover, #MainContent_lnkViewABAPObjectPlan:active,
        #MainContent_lnkRemoveABAPObject:hover, #MainContent_lnkRemoveABAPObject:active,
        #MainContent_lnkhbttestcaseupload:hover, #MainContent_lnkhbttestcaseupload:active {
            background-color: #3D1956;
            color: #C7D3D4 !important;
            border-color: #3D1956;
            border-width: 2pt;
            border-style: inset;
        }

        .blue-background {
            background-color: #ADD8E6;
            color: #000000;
        }

        .green-background {
            background-color: #90EE90;
            color: #000000;
        }

        #page-loader {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.9);
            z-index: 9999;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .loader {
            border: 10px solid #f3f3f3;
            border-top: 10px solid #3D1956;
            border-radius: 50%;
            width: 40px;
            height: 40px;
            animation: spin 1.5s linear infinite;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-loader">
        <div class="loader"></div>
    </div>
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
                            <asp:Label ID="lblheading" runat="server" Text="ABAP Object Tracker System"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
                            <table>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="trConsultantHead" runat="server" visible="false">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span1" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Consultant : </span>
                                    </td>
                                </tr>
                                <tr id="trConsultantHeadLine" runat="server" visible="false">
                                    <td colspan="2">
                                        <hr runat="server" id="hr1" visible="true" />
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="tdviewassingedabapoplan" visible="false">
                                        <asp:LinkButton ID="lnkApprovedList" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_ApprovedList.aspx">View Assigned Plan</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td1"></td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_changestatus_RGS" visible="false">
                                        <asp:LinkButton ID="lnkUpdateStatusRGS" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_RGS.aspx">RGS - Update Status (0)</asp:LinkButton>
                                    </td>

                                    <td class="formtitle" runat="server" id="td_changestatus_FS" visible="false">
                                        <asp:LinkButton ID="lnkUpdateStatusFS" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_FS.aspx">FS - Update Status (0)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_changestatus_HBTTesting" visible="false">
                                        <asp:LinkButton ID="lnkUpdateStatusHBTTesting" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_HBTTesting.aspx">HBT Testing - Update Status(0)</asp:LinkButton>
                                    </td>

                                    <td class="formtitle" runat="server" id="td_changestatus_CTMTesting" visible="false">
                                        <asp:LinkButton ID="lnkUpdateStatusCTMTesting" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_CTMTesting.aspx">CTM Testing - Update Status (0)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_changeAcceptNotAccept_ABAP" visible="false">
                                        <asp:LinkButton ID="lnkUpdateAcceptNotAcceptABAP" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_ABAP_Accept_NotAccept.aspx">Functional Specification Acceptance (0)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_changestatus_ABAP" visible="false">
                                        <asp:LinkButton ID="lnkUpdateStatusABAP" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_ABAP.aspx">ABAP Development - Update Status (0)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_uploadSourceCode" visible="false">
                                        <asp:LinkButton ID="lnkUploadSourceCode" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_ABAP_SourceCode.aspx">ABAP Development - Upload Soruce Code (0)</asp:LinkButton>
                                    </td>

                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="trPMHead" runat="server" visible="false">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span2" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">PM : </span>
                                    </td>
                                </tr>
                                <tr id="trPMHeadLine" runat="server" visible="false">
                                    <td colspan="2">
                                        <hr runat="server" id="hr2" visible="true" />
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="tduploadabapobjectplan" visible="false">
                                        <asp:LinkButton ID="lnkuploadplan" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Upload.aspx">Upload Plan</asp:LinkButton>
                                    </td>

                                    <td class="formtitle" runat="server" id="tdviewabapplanlist" visible="false">
                                        <asp:LinkButton ID="lnkAllAbapList" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_All_List.aspx"> Approved Plans </asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_change_RGSFSHBTCTMConsultant" visible="false">
                                        <asp:LinkButton ID="lnkfschange" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_RGSFSHBTABAPCTM_Consultant.aspx">Change Consultant</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_changed_RGSFSHBTCTMConsultant" visible="false">
                                        <asp:LinkButton ID="lnkfschanged" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Changed_FS_Consultants.aspx"> Consultant History</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" visible="false">
                                    <td class="formtitle" runat="server" id="td_change_ABAPConsultant" visible="false">
                                        <asp:LinkButton ID="lnkabapchange" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_ABAP_Consultant.aspx">ABAP - Change Consultant</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_changed_ABAPConsultant" visible="false">
                                        <asp:LinkButton ID="lnkabapchanged" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Changed_ABAP_Consultants.aspx">ABAP - Consultant History</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" runat="server" visible="false">
                                    <td class="formtitle" runat="server" id="td_change_CTMConsultant" visible="false">
                                        <asp:LinkButton ID="lnkctmchange" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_CTM_Consultant.aspx">CTM Testing - Change Conslutant</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_changed_CTMConsultant" visible="false">
                                        <asp:LinkButton ID="lnkctmchanged" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Changed_CTM_Consultants.aspx">CTM Testing - Consultant History</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_changestatus_uatsignoff" visible="false">
                                        <asp:LinkButton ID="Link_ChangeStatus_UATSignoff" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_UATSignoff.aspx">UAT Sign-off - Update Status (0)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_changesstatus_golive" visible="false">
                                        <asp:LinkButton ID="Link_ChangeStatus_GoLive" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_GoLive.aspx">Go-Live - Update Status (0)</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_AuditReport" visible="false">
                                        <asp:LinkButton ID="lnk_AuditReport" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_AuditReport.aspx">Audit Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_DelayReport" visible="false">
                                        <asp:LinkButton ID="lnk_DelayReport" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_DelayReport.aspx">Delay Report</asp:LinkButton>
                                    </td>

                                </tr>
                                 <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="td_detailsSummaryReport" visible="false">
                                        <asp:LinkButton ID="lnk_DetailSummaryReport" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_DetailSummary.aspx">Detail Summary Report</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="td_treeview" visible="false">
                                        <asp:LinkButton ID="lnk_treeview" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_TreeView.aspx">ABAP Object Tree View</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="idTRApproverHead" runat="server" visible="false">
                                    <td class="formtitle">
                                        <br /> 
                                        <span id="span_App_head" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Approvers : </span>
                                    </td>
                                </tr>
                                <tr id="idTRApproverHead_Line" runat="server" visible="false">
                                    <td colspan="2">
                                        <hr runat="server" id="hr_App_head" visible="true" />
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="tdctmtestcaseapproval" visible="false">
                                        <asp:LinkButton ID="lnkCTMTestCaseUploadApproval" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_CTM_Test_Cases_Index.aspx">CTM Approval (0)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle" runat="server" id="tdrgsstageapproval" visible="false">
                                        <asp:LinkButton ID="lnkRGSStageApproval" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Change_Status_RGS_Approval.aspx">RGS Approval (0)</asp:LinkButton>
                                    </td>

                                </tr>


                                <tr style="padding-top: 1px; padding-bottom: 2px;" id="trPRMHead" runat="server" visible="false">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span3" runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">PRM : </span>
                                    </td>
                                </tr>
                                <tr id="trPRMHeadLine" runat="server" visible="false">
                                    <td colspan="2">
                                        <hr runat="server" id="hr3" visible="true" />
                                    </td>
                                </tr>

                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="tdinbox_planapproval" visible="false">
                                        <asp:LinkButton ID="lnkinboxapproval" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_PendingList.aspx">Inbox Plan Approval (0)</asp:LinkButton>
                                    </td>

                                    <td class="formtitle" runat="server" id="tdViewPRMABAPObjectPlan" visible="false">
                                        <asp:LinkButton ID="lnkViewABAPObjectPlan" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_All_List.aspx">Approved Plans</asp:LinkButton>
                                    </td>

                                </tr>
                                  <tr id="trRow" runat="server" visible="false">
                                    <td colspan="2">
                                        <hr runat="server" id="hr4" visible="true" />
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle" runat="server" id="tdRemoveABAPObject" visible="false">
                                        <asp:LinkButton ID="lnkRemoveABAPObject" runat="server" PostBackUrl="~/procs/ABAP_Object_Tracker_Delete.aspx">Remove ABAP Object Tracker</asp:LinkButton>
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

        document.onreadystatechange = function () {
            if (document.readyState === "complete") {
                document.getElementById("page-loader").style.display = "none";
            }
        };

        // For async postbacks
        Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(function () {
            document.getElementById("page-loader").style.display = "flex";
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            document.getElementById("page-loader").style.display = "none";
        });
    </script>


</asp:Content>

