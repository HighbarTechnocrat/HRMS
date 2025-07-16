<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="Recruiter_Index.aspx.cs" 
    Inherits="procs_Recruiter_Index" %>


<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
    <%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/TravelCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/traindex.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            background: #dae1ed;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }

#MainContent_lnk_RecCreateEditCandidate:link, #MainContent_lnk_RecCreateEditCandidate:visited,
#MainContent_lnk_mng_trvlrequest:link, #MainContent_lnk_mng_trvlrequest:visited,
#MainContent_lnk_mng_recInbox:link, #MainContent_lnk_mng_recInbox:visited,
#MainContent_lnk_mng_ViewRecRequest:link, #MainContent_lnk_mng_ViewRecRequest:visited,
#MainContent_lnk_mng_InterviewrInbox:link, #MainContent_lnk_mng_InterviewrInbox:visited,
#MainContent_lnk_mng_InterviewerShortlisting:link, #MainContent_lnk_mng_InterviewerShortlisting:visited,
#MainContent_lnk_mng_ViewRecRequestInterviewer:link, #MainContent_lnk_mng_ViewRecRequestInterviewer:visited,
#MainContent_Lnk_mng_recRescheduleInterview:link, #MainContent_Lnk_mng_recRescheduleInterview:visited,

#MainContent_lnk_trvlinbox:link, #MainContent_lnk_trvlinbox:visited,
#MainContent_lnk_trvlParametersmst:link, #MainContent_lnk_trvlParametersmst:visited,
#MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
#MainContent_lnk_trvl_TDInbox:link, #MainContent_lnk_trvl_TDInbox:visited,
#MainContent_lnk_trvl_COSInbox:link, #MainContent_lnk_trvl_COSInbox:visited,
#MainContent_lnk_trvl_AccInbox:link, #MainContent_lnk_trvl_AccInbox:visited,
#MainContent_lnk_expens_AccInbox:link, #MainContent_lnk_expens_AccInbox:visited,
#MainContent_lnk_trvlAccInbox:link, #MainContent_lnk_trvlAccInbox:visited,  
#MainContent_lnk_mng_recInterviewerShortlisted:link, #MainContent_lnk_mng_recInterviewerShortlisted:visited, 
#MainContent_lnk_reimbursmentReport_3:link,#MainContent_lnk_reimbursmentReport_3:visited {
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

#MainContent_lnk_RecCreateEditCandidate:hover, #MainContent_lnk_RecCreateEditCandidate:active,
#MainContent_lnk_mng_recInbox:hover, #MainContent_lnk_mng_recInbox:active,
#MainContent_lnk_mng_ViewRecRequest:hover, #MainContent_lnk_mng_ViewRecRequest:active,
#MainContent_lnk_mng_InterviewrInbox:hover, #MainContent_lnk_mng_InterviewrInbox:active,
#MainContent_lnk_mng_InterviewerShortlisting:hover, #MainContent_lnk_mng_InterviewerShortlisting:active,
#MainContent_lnk_mng_ViewRecRequestInterviewer:hover, #MainContent_lnk_mng_ViewRecRequestInterviewer:active,
#MainContent_lnk_mng_recInterviewerShortlisted:hover, #MainContent_lnk_mng_recInterviewerShortlisted:active,
#MainContent_Lnk_mng_recRescheduleInterview:hover, #MainContent_Lnk_mng_recRescheduleInterview:active,

#MainContent_lnk_trvlinbox:hover, #MainContent_lnk_trvlinbox:active,
#MainContent_lnk_mng_trvlrequest:hover, #MainContent_lnk_mng_trvlrequest:active,
#MainContent_lnk_trvlParametersmst:hover, #MainContent_lnk_trvlParametersmst:active,
#MainContent_lnk_TeamCalendar:hover, #MainContent_lnk_TeamCalendar:active,
#MainContent_lnk_trvl_TDInbox:hover, #MainContent_lnk_trvl_TDInbox:active,
#MainContent_lnk_trvl_COSInbox:hover, #MainContent_lnk_trvl_COSInbox:active,
#MainContent_lnk_trvl_AccInbox:hover, #MainContent_lnk_trvl_AccInbox:active,
#MainContent_lnk_expens_AccInbox:hover, #MainContent_lnk_expens_AccInbox:active,
#MainContent_lnk_trvlAccInbox:hover, #MainContent_lnk_trvlAccInbox:active,
#MainContent_lnk_reimbursmentReport_3:hover,#MainContent_lnk_reimbursmentReport_3:active  {
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
                            <asp:Label ID="lblheading" runat="server"></asp:Label>
                        </span>
                    </div>
                    <div class="leavegrid">
                       <%-- <a href="http://192.168.21.193/hrms/Claims.aspx" class="aaa" >Claims Menu</a>--%>
                     </div>
                    <div class="editprofile" id="editform1" runat="server">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                      <asp:LinkButton ID="lnk_mng_recInbox" runat="server" Visible="false" PostBackUrl="~/procs/Rec_RecruiterInbox.aspx?type=InRec">Inbox(0)</asp:LinkButton>
                                 </td>
                                     <td class="formtitle">
                                      <asp:LinkButton ID="lnk_mng_recInterviewerShortlisted" runat="server" Visible="false" PostBackUrl="~/procs/Rec_RecruiterInbox.aspx?type=RECISL">Schedule Interview (0)</asp:LinkButton>
                                 </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                           <asp:LinkButton ID="lnk_RecCreateEditCandidate" runat="server" Visible="false" PostBackUrl="~/procs/SearchCandidate.aspx">Create/Edit Candidate</asp:LinkButton>
                                     </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_ViewRecRequest" runat="server" Visible="false" PostBackUrl="~/procs/Rec_RecruiterInbox.aspx?type=VRR">View Recruitment Requests</asp:LinkButton>
                                    </td>
                                </tr>

                                 <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                           <asp:LinkButton ID="Lnk_mng_recRescheduleInterview" runat="server"  PostBackUrl="~/procs/Rec_RecruiterInbox.aspx?type=RECIRescedule">Reschedule Interview(0)</asp:LinkButton>
                                     </td>
                                </tr>

                                 <tr style="padding-top:1px;padding-bottom:2px;" runat="server">
                                    <td class="formtitle">
                                       <asp:LinkButton ID="lnk_mng_InterviewrInbox" runat="server" Visible="false" PostBackUrl="~/procs/Rec_InterviewerInbox.aspx?type=InShPInter">Inbox Interview(0)</asp:LinkButton>
                                    </td>
                                      <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_ViewRecRequestInterviewer" runat="server" PostBackUrl="~/procs/Rec_InterviewerInbox.aspx?type=InVRInter">View Recruitment Requests</asp:LinkButton>
                                    </td>
                                      

                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;" runat="server">
                                   <td class="formtitle">
                                       <asp:LinkButton ID="lnk_mng_InterviewerShortlisting" runat="server" PostBackUrl="~/procs/Rec_InterviewerInbox.aspx?type=InPInter">Inbox Shortlisting(0)</asp:LinkButton>
                                     </td>
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                       <%-- <span id="span_App_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Approver: </span>
                                   --%> </td>
                                </tr>                                
                               <%-- <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle" style="display:none;">
                                        <asp:LinkButton ID="lnk_trvlinbox" runat="server" Text="Inbox (leave requests)" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=APP" >Inbox (Travel requests)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_trvlParametersmst" runat="server" Text ="Inbox (Travel Expenses)" PostBackUrl="~/procs/InboxTravelExpense.aspx?stype=APP"> Inbox (Travel Expenses)</asp:LinkButton>
                                    </td>
                                </tr>--%>

                               <%-- <tr style="padding-top:1px;padding-bottom:2px;display:none;">
                                    <td class="formtitle">                                            
                                      <asp:LinkButton ID="lnk_TeamCalendar" runat="server" PostBackUrl="~/procs/teamcalender_Travel.aspx">Team Calendar</asp:LinkButton>                                                
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_TD_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - Traveldesk: </span>
                                        <span id="span_cos_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - COS: </span>
                                        <span id="span_acc_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Admin - Accounts: </span>
                                    </td>
                                </tr>
                                  <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle" style="display:none;">  
                                        <asp:LinkButton ID="lnk_trvl_TDInbox" runat="server" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=TD" Text ="Travel Desk Inbox">Travel Desk Inbox</asp:LinkButton> 
                                        <asp:LinkButton ID="lnk_trvl_COSInbox" runat="server" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=COS" Text ="COS Inbox">COS Travel Desk Inbox</asp:LinkButton> 
                                        <asp:LinkButton ID="lnk_trvl_AccInbox" runat="server" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=ACC" Text ="ACC Inbox">ACC Inbox</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_expens_AccInbox" runat="server"  CssClass="aaaa" PostBackUrl ="~/procs/InboxTravelExpense.aspx?stype=ACC" Text ="Inbox Travel Expenses"> Inbox Travel Expenses</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_reimbursmentReport_3" runat="server"  CssClass="aaaa" Text="Travel Expense Report - Accounts" PostBackUrl="~/procs/TravelExpenseReport_Audit.aspx">Travel Expense Report - Accounts</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">      
                                        <asp:LinkButton ID="lnk_trvlAccInbox" runat="server"  Text ="Approved Expenses" PostBackUrl="~/procs/InboxTravel_ACC.aspx?stype=ACC">Approved Expenses</asp:LinkButton>           
                                    </td>
                                </tr>	--%>
                            </table>
                        </div>
                    </div>
                </div>

              <div class="index">
                    <ul>
                <li><asp:LinkButton ID="lnk_trvePara" runat="server" visible ="false" Text="Travel Parameters Master" PostBackUrl ="~/procs/InboxLeave_Req.aspx" >Travel Parameters Master</asp:LinkButton> </li>
                 <li><asp:LinkButton ID="lnk_expPara" runat="server" visible ="false" Text ="Expenses Parameters Master">Expenses Parameters Master</asp:LinkButton> </li>
         
                </ul>
                </div>

            </div>
        </div>
    </div>
    
    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hflEmpName" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />

     <asp:HiddenField ID="hdnempcode" runat="server" />



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


