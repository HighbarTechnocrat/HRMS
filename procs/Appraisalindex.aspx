<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="Appraisalindex.aspx.cs" Inherits="Appraisalindex" %>



<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server"> 
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>hcc/Appraisal.css" type="text/css" media="all" /> 
    <style>
        .myaccountpagesheading {
           background:#ebebe4;
        }

        .Calender {
            float: left;
            padding: 5% 5% 5% 5% !important;
        }


        #MainContent_lnk_CreateSelfAssessment:link, #MainContent_lnk_CreateSelfAssessment:visited, 
        #MainContent_lnk_mng_SelfAssessment:link, #MainContent_lnk_mng_SelfAssessment:visited,
         #MainContent_lnk_mng_RecommendationList:link, #MainContent_lnk_mng_RecommendationList:visited,
         #MainContent_lnk_mng_PerformanceRevList:link, #MainContent_lnk_mng_PerformanceRevList:visited,
         #MainContent_lnk_viewAppraisalForm:link, #MainContent_lnk_viewAppraisalForm:visited,
        #MainContent_lnk_TeamCalendar:link, #MainContent_lnk_TeamCalendar:visited,
         #MainContent_lnk_Appraisal_Manual:link, #MainContent_lnk_Appraisal_Manual:visited,
         #MainContent_lnk_AppraisalProcessStatus:link, #MainContent_lnk_AppraisalProcessStatus:visited ,
         #MainContent_lnk_AppraisalProcess:link, #MainContent_lnk_AppraisalProcess:visited,
        #MainContent_lnkUploadAppraisalData:link, #MainContent_lnkUploadAppraisalData:visited,
        #MainContent_lnk_HODTEAM:link, #MainContent_lnk_HODTEAM:visited ,
        #MainContent_lnkChangesPMSAcces_Key:link, #MainContent_lnkChangesPMSAcces_Key:visited  
        
        
		{
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

        #MainContent_lnk_CreateSelfAssessment:hover, #MainContent_lnk_CreateSelfAssessment:active,
        #MainContent_lnk_mng_SelfAssessment:hover, #MainContent_lnk_mng_SelfAssessment:active,
        #MainContent_lnk_mng_RecommendationList:hover, #MainContent_lnk_mng_RecommendationList:active,
        #MainContent_lnk_mng_PerformanceRevList:hover, #MainContent_lnk_mng_PerformanceRevList:active,
        #MainContent_lnk_viewAppraisalForm:hover, #MainContent_lnk_viewAppraisalForm:active,
        #MainContent_lnk_TeamCalendar:hover, #MainContent_lnk_TeamCalendar:active,
        #MainContent_lnk_Appraisal_Manual:hover, #MainContent_lnk_Appraisal_Manual:active,
        #MainContent_lnk_AppraisalProcessStatus:hover, #MainContent_lnk_AppraisalProcessStatus:active,
        #MainContent_lnk_AppraisalProcess:hover, #MainContent_lnk_AppraisalProcess:active ,
        #MainContent_lnkUploadAppraisalData:hover, #MainContent_lnkUploadAppraisalData:active,
        #MainContent_lnk_HODTEAM:hover, #MainContent_lnk_HODTEAM:active,
         #MainContent_lnkChangesPMSAcces_Key:hover, #MainContent_lnkChangesPMSAcces_Key:active
        
         {
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
                    <div class="myaccount" style="display: none;">
                        <div class="myaccountheading">My Account</div>
                        <div class="myaccountlist">
                            <ul>
                                <li class="listselected"><a href="<%=ReturnUrl("sitepathmain") %>myaccount/leaveindex" title="Edit Profile"><b>Edit Profile</b></a></li>
                                <li><a href="<%=ReturnUrl("sitepathmain") %>myaccount/wishlist" title="Favorites">Favorites</a></li>
                                <asp:Panel ID="Panel1" runat="server" Visible="false">
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>myaccount/preference" title="Preference">Preference</a></li>

                                    <li id="lihistory" runat="server"><a href="<%=ReturnUrl("sitepathmain") %>myaccount/subscriptionhistory" title="Subscription History">Subscription History</a></li>
                                    <li><a href="<%=ReturnUrl("sitepathmain") %>myaccount/pthistory" title=" Reward Points">Reward Points</a></li>

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
                    </div>
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Appraisal Module"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>
                                <tr style="padding-top:1px;padding-bottom:2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_CreateSelfAssessment" runat="server" PostBackUrl="~/procs/SelfAssessmentCreate.aspx"  >Self Assessment</asp:LinkButton>     <%-- OnClick="lnk_leaverequest_Click"  --%>                                  
                                    </td>
                                     <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_SelfAssessment" runat="server" PostBackUrl="~/procs/ManageSelfAssessment.aspx">Manage Self Assessment</asp:LinkButton>                                        
                                    </td>
                                </tr>
                              
                                <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_PerformanceRevList" runat="server" PostBackUrl="~/procs/PerformanceReviewList.aspx" Text="Performance Review List"></asp:LinkButton>                                        
                                    </td>
                                     <td class="formtitle">
                                        </td>
                                </tr>
                                 <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_RecommendationList" runat="server" PostBackUrl="~/procs/ManageRecommendationList.aspx">Recommendation List</asp:LinkButton>                                        
                                    </td>
                                      <td class="formtitle">
                                     </td>
                                </tr>
                               
                                 <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_viewAppraisalForm" runat="server" PostBackUrl="~/procs/Appraisal_FormCreate.aspx">View My Performance Appraisal Form</asp:LinkButton>                                        
                                    </td>
                                </tr>

                                  <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_TeamCalendar" runat="server" PostBackUrl="~/procs/Appraisal_TeamCalendar.aspx">My Team Appraisals</asp:LinkButton>                                        
                                    </td>
                                      <td class="formtitle">
                                        <asp:LinkButton ID="lnk_HODTEAM" runat="server" PostBackUrl="~/procs/Appraisal_HODCalendar.aspx" >All Team Appraisals</asp:LinkButton>                                        
                                    </td>
                                   
                                </tr> 
                               <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle" id="tdAppraisalProcessStatus" runat="server" visible="false">
                                          <asp:LinkButton ID="lnk_AppraisalProcessStatus" runat="server" PostBackUrl="~/procs/AppraisalProcess_Status.aspx" >Appraisal Process Status</asp:LinkButton>  
                                    </td>
                                </tr>
                                   <tr style="padding-top:1px;padding-bottom:4px;">
                                     <td class="formtitle">
                                        <asp:LinkButton ID="lnk_AppraisalProcess" PostBackUrl="~/procs/Appraisalprocessdata.aspx"  runat="server" Visible="false" >Appraisal Process Report</asp:LinkButton>                                        
                                    </td>
                                   <td class="formtitle">
                                        <asp:LinkButton ID="lnkUploadAppraisalData" PostBackUrl="~/procs/Appraisalprocessdata_Upload.aspx"  Visible="false" runat="server">Upload Appraisal Details </asp:LinkButton>                                        
                                    </td>
                                </tr>

                                  <tr style="padding-top:1px;padding-bottom:4px;">
                                    <td class="formtitle">
                                        <a  visible="false" href="https://www.ess.highbartech.com/hrms/procs/AppraisalUserManual.pdf" title="Appraisal User Manual" class="LeaveManualLnik"  id="lnk_Appraisal_Manual1" runat="server" target="_blank">Appraisal User Manual</a>
                                        <asp:LinkButton ID="lnk_Appraisal_Manual" runat="server" OnClientClick="Download_POSignCopyFile()" CssClass="BtnShow">Appraisal User Manual</asp:LinkButton>
                                        
                                    </td>
                                       <td class="formtitle">
                                          <asp:LinkButton ID="lnkChangesPMSAcces_Key" PostBackUrl="~/procs/Appraisal_login.aspx?p=c&i=1"    runat="server">Change PMS Access Key </asp:LinkButton> 
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
    <asp:HiddenField ID="hdnSingPOCopyFilePath" runat="server" />
	<asp:HiddenField ID="hdnSingPOCopyFileName" runat="server" />

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

        function Download_POSignCopyFile() {
			// alert(file);
			var localFilePath = document.getElementById("<%=hdnSingPOCopyFilePath.ClientID%>").value;
			var localFileName = document.getElementById("<%=hdnSingPOCopyFileName.ClientID%>").value;

		 
			window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

			//window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + localFileName);

        }

    </script>
</asp:Content>
