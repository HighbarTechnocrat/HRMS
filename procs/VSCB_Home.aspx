<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="VSCB_Home.aspx.cs" Inherits="VSCB_Home" %>

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

#MainContent_lnk_trvlrequest:link, #MainContent_lnk_trvlrequest:visited,
#MainContent_lnk_mng_trvlrequest:link, #MainContent_lnk_mng_trvlrequest:visited,
#MainContent_lnk_mng_trvlexpenseswpay:link, #MainContent_lnk_mng_trvlexpenseswpay:visited,
#MainContent_lnk_mng_trvlexpenses:link, #MainContent_lnk_mng_trvlexpenses:visited,
#MainContent_lnk_trvlinbox:link, #MainContent_lnk_trvlinbox:visited,
#MainContent_lnk_trvlParametersmst:link, #MainContent_lnk_trvlParametersmst:visited,
#MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
#MainContent_lnk_trvl_TDInbox:link, #MainContent_lnk_trvl_TDInbox:visited,
#MainContent_lnk_trvl_COSInbox:link, #MainContent_lnk_trvl_COSInbox:visited,
#MainContent_lnk_trvl_AccInbox:link, #MainContent_lnk_trvl_AccInbox:visited,
#MainContent_lnk_expens_AccInbox:link, #MainContent_lnk_expens_AccInbox:visited,
#MainContent_lnk_trvlAccInbox:link, #MainContent_lnk_trvlAccInbox:visited,  
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

#MainContent_lnk_trvlrequest:hover, #MainContent_lnk_trvlrequest:active,
#MainContent_lnk_mng_trvlrequest:hover, #MainContent_lnk_mng_trvlrequest:active,
#MainContent_lnk_mng_trvlexpenseswpay:hover, #MainContent_lnk_mng_trvlexpenseswpay:active,
#MainContent_lnk_mng_trvlexpenses:hover, #MainContent_lnk_mng_trvlexpenses:active,
#MainContent_lnk_trvlinbox:hover, #MainContent_lnk_trvlinbox:active,
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

.flex-container {
  display: flex;
  justify-content: space-between;
  background-color: DodgerBlue;
}

.flex-container > div {
  background-color: #f1f1f1;
  width: 100px;
  margin: 10px;
  text-align: center;
  line-height: 75px;
  font-size: 30px;
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
                            <asp:Label ID="lblheading" runat="server" Text="Vendor Sub-Con Billing"></asp:Label>
                        </span>
                    </div>
                    <div class="leavegrid">
                        <a href="https://ess.highbartech.com/hrms/Claims.aspx" class="aaa" >Claims Menu</a>
                     </div>
                    <div class="editprofile" id="editform1" runat="server">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
                                <tr style="padding-top:1px;padding-bottom:2px;display:inline;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_trvlrequest" runat="server" PostBackUrl="~/procs/VSCB_CreateMilestone.aspx" >Create Milestone</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_trvlrequest" runat="server" PostBackUrl="~/procs/VSCB_CreateMilestone.aspx" >My Milestone</asp:LinkButton>
                                    </td>
                                </tr>
                              

                                <tr style="padding-top:1px;padding-bottom:2px;display:none">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_trvlexpenseswpay" runat="server" OnClick="lnk_mng_trvlexpenseswpay_Click">Submit Travel Expenses</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_trvlexpenses" runat="server" PostBackUrl="~/procs/ManageTravel_expense.aspx">My Travel Expenses</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <br />
                                        <span id="span_App_head" runat="server" style="font-size:12pt;font-weight:bold; color:#3D1956;">Approver: </span>
                                    </td>
                                </tr>                                
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle" style="display:none;">
                                        <asp:LinkButton ID="lnk_trvlinbox" runat="server" Text="Inbox (leave requests)" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=APP" >Inbox (Travel requests)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_trvlParametersmst" runat="server" Text ="Inbox (Travel Expenses)" PostBackUrl="~/procs/InboxTravelExpense.aspx?stype=APP"> Inbox (Travel Expenses)</asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="padding-top:1px;padding-bottom:2px;display:none;">
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
                                        <asp:LinkButton ID="lnk_trvlAccInbox" runat="server"  Text ="View Offer Letter" OnClick="lnk_trvlAccInbox_Click">View Offer Letter</asp:LinkButton>           
                                    </td>
                                </tr>
                              
                                

<%--								<tr style="padding-top:1px;padding-bottom:2px;">
									<td class="formtitle">
										<ucical:calender ID="icalender" runat="server"></ucical:calender>
                                        
									</td>
								</tr>--%>	
                               						
                            </table>
                        </div>
                    </div>
                </div>

                <div style="width:80%"><p id="poffer" runat="server" style="text-align: justify;">Please refer to your application and our offer letter no.<b>@offerletterno</b> dated <b>@offerdate</b> and subsequent discussions you had with us. We are pleased to appoint you in <b>@offerBand</b> in <b>HIGHBAR TECHNOCRAT LTD</b>. Your present functional designation will be <b>@offerPossition</b>.The detailed terms and conditions of your employment are as under:<br />1.0 Your date of appointment is effective from 01-09-2022 .<br />2.0 Presently, you are being posted at our Head Office, Mumbai. However, based on the needs of the company, you can be transferred to any branch, site (or project), group (or associate) companies or a joint venture entered into by the Company, located anywhere in India or abroad.</p></div>



                <asp:Label ID="lblid" runat="server"></asp:Label>
                <%--<div class="index">
                    <ul>
                    <li><asp:LinkButton ID="lnk_trvlrequest" runat="server" OnClick="lnk_trvlrequest_Click" >Travel Request</asp:LinkButton> </li>
                    <li><asp:LinkButton ID="lnk_mng_trvlrequest" runat="server" PostBackUrl ="~/procs/ManageTravelRequest.aspx" >Manage Travel Requests</asp:LinkButton> </li>
                   <li><asp:LinkButton ID="lnk_mng_trvlexpenseswpay" runat="server" OnClick="lnk_mng_trvlexpenseswpay_Click">Travel Expenses (without Request)</asp:LinkButton> </li>
                    <li><asp:LinkButton ID="lnk_mng_trvlexpenses" runat="server" PostBackUrl="~/procs/ManageTravel_expense.aspx">Manage Travel Expenses</asp:LinkButton> </li>
                
                </ul>
                    </div>
                <div class="index">
                    <ul>
                <li><asp:LinkButton ID="lnk_trvlinbox" runat="server" Text="Inbox (leave requests)" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=APP" >Inbox (Travel requests)</asp:LinkButton> </li>
                 <li><asp:LinkButton ID="lnk_trvlParametersmst" runat="server" Text ="Inbox (Travel Expenses)" PostBackUrl="~/procs/InboxTravelExpense.aspx"> Inbox (Travel Expenses)</asp:LinkButton> </li>
                <li><asp:LinkButton ID="lnk_trvlreport" runat="server" Text ="Leave Report " PostBackUrl ="~/procs/TravelRequest.aspx" >Leave Report</asp:LinkButton> </li>
                <li><asp:LinkButton ID="lnk_trvlattendanceinbox" runat="server" Text ="Inbox (attendance regularization requests)">Inbox (attendance regularization)</asp:LinkButton> </li>
                <li><asp:LinkButton ID="lnk_trvl_TDInbox" runat="server" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=TD" Text ="Travel Desk Inbox">Travel Desk Inbox</asp:LinkButton> </li>
                <li><asp:LinkButton ID="lnk_trvl_COSInbox" runat="server" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=COS" Text ="COS Inbox">COS Travel Desk Inbox</asp:LinkButton> </li>
                <li><asp:LinkButton ID="lnk_trvl_AccInbox" runat="server" PostBackUrl ="~/procs/InboxTravelRequest.aspx?stype=ACC" Text ="ACC Inbox">ACC Inbox</asp:LinkButton> </li>
                </ul>
                </div>--%>



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
