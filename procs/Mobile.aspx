<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Mobile.aspx.cs" Inherits="Mobile" %>



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

#MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited ,
#MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited ,
#MainContent_lnk_reimbursmentReport:link, #MainContent_lnk_reimbursmentReport:visited ,
#MainContent_lnk_leaveinbox:link, #MainContent_lnk_leaveinbox:visited ,
#MainContent_lnk_MobACC:link, #MainContent_lnk_MobACC:visited ,
#MainContent_lnk_MobCFO:link,#MainContent_lnk_MobPastApproved_ACC:link, #MainContent_lnk_MobCFO:visited,#MainContent_lnk_MobPastApproved_ACC:visited ,
#MainContent_lnk_reimbursmentReport_1:link, #MainContent_lnk_reimbursmentReport_1:visited,
#MainContent_Lnk_DownLoadReport:link,#MainContent_Lnk_DownLoadReport:visited
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
#MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active ,
#MainContent_lnk_reimbursmentReport:hover, #MainContent_lnk_reimbursmentReport:active ,
#MainContent_lnk_leaveinbox:hover, #MainContent_lnk_leaveinbox:active ,
#MainContent_lnk_MobACC:hover, #MainContent_lnk_MobACC:active ,
#MainContent_lnk_MobCFO:hover,#MainContent_lnk_MobPastApproved_ACC:hover, #MainContent_lnk_MobCFO:active,#MainContent_lnk_MobPastApproved_ACC:active ,
#MainContent_lnk_reimbursmentReport_1:hover, #MainContent_lnk_reimbursmentReport_1:active,
#MainContent_Lnk_DownLoadReport:hover,#MainContent_Lnk_DownLoadReport:active
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
                            <asp:Label ID="lblheading" runat="server" Text="Mobile Claims"></asp:Label>
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
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server"  OnClick="lnk_leaverequest_Click">Claim Mobile Bill</asp:LinkButton>                                         
                                    </td>
                                     <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" PostBackUrl="~/procs/MyMobile_Req.aspx">My Mobile Claims</asp:LinkButton>
                                    </td>                                    
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/ClaimsReport_Mobile.aspx">Report - Self</asp:LinkButton>
                                    </td>
                                      <td class="formtitle">
                                        <asp:LinkButton ID="Lnk_DownLoadReport" runat="server"  OnClick="Lnk_DownLoadReport_Click" Visible="True" Text="" >DownLoad Report Voucher</asp:LinkButton>
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
                                        <asp:LinkButton ID="lnk_leaveinbox" runat="server" Text="" PostBackUrl="~/procs/InboxMobile.aspx?app=APP">Inbox</asp:LinkButton>
                                    </td>

                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_cos_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - COS: </span>
                                        <span id="span_acc_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - Accounts: </span>
                                    </td>
                                </tr>   
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MobCFO" runat="server" Text=""  PostBackUrl="~/procs/InboxMobile.aspx?app=RCFO">COS-Inbox : </asp:LinkButton>
                                        <asp:LinkButton ID="lnk_MobACC" runat="server" Text=""  PostBackUrl="~/procs/InboxMobile.aspx?app=RACC">Accounts-Inbox :</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport_1" runat="server"  Visible="True" Text="" PostBackUrl="~/procs/MobileReport_Audit.aspx">Audit Report - (ACC)</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MobPastApproved_ACC" runat="server" Text=""  PostBackUrl="~/procs/InboxMobile_Arch.aspx?app=RACC">Approved Mobile Claims</asp:LinkButton>
                                    </td>
                                </tr>
                                <%--                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <a  href="https://ess.highbartech.com/hrms/procs/How to Submit Fuel Reimbursement.pdf" title="hrms- How to Apply Fuel Reimbursement" class="LeaveManualLnik" target="_blank">hrms- How to Apply Fuel Reimbursement</a>
                                    </td>
                                </tr>--%>
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
