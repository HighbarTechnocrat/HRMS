<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" 
	CodeFile="Ref_Employee_Index.aspx.cs" Inherits="Ref_Employee_Index" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
   <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
	<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>

	<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />
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
        
		 
		#MainContent_lnk_leaverequest:link, #MainContent_lnk_leaverequest:visited ,
        #MainContent_lnk_CreateJDBank:link, #MainContent_lnk_CreateJDBankt:visited ,
        #MainContent_lnk_MyJDBank:link, #MainContent_lnk_MyJDBank:visited ,
        #MainContent_lnk_mng_leaverequest:link, #MainContent_lnk_mng_leaverequest:visited 
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
        #MainContent_lnk_CreateJDBank:hover, #MainContent_lnk_CreateJDBank:active,
        #MainContent_lnk_MyJDBank:hover, #MainContent_lnk_MyJDBank:active,
        #MainContent_lnk_mng_leaverequest:hover, #MainContent_lnk_mng_leaverequest:active
      
		{
            /*background-color: #603F83;*/
            background-color: #3D1956;
            color: #C7D3D4 !important;
            border-color: #3D1956;
            border-width: 2pt;
            border-style: inset;
        }
		.Position {
			color: #F28820;
			display: inline-block;
			font-size: 22px;
			font-weight: 300;
			//margin: 0 10px 15px 0;
			padding: 0 0 10px;
			text-align: left;
			width: 100%;
		}
		table {
        border-collapse: separate;
        border-spacing: 0;
       
        
    }
    #gvMain tr th,
    #gvMain tr td {
        border-right: 1px solid #000;
        border-bottom: 1px solid #000;
        padding: 5px;
    }
    #gvMain tr th:first-child, table tr th:last-child{
    border-top:solid 1px      #000;}
     #gvMain tr th:first-child,
     #gvMain tr td:first-child {
        border-left: 1px solid #000;
        
    }
     #gvMain tr th:first-child,
     #gvMain tr td:first-child {
        border-left: 1px solid #000;
    }
      
     #gvMain.Info tr th,
     #gvMain.Info tr:first-child td
    {
        border-top: 1px solid #000;
    }
    
    /* top-left border-radius */
     #gvMain tr:first-child th:first-child,
     #gvMain.Info tr:first-child td:first-child {
        border-top-left-radius: 10px;
    }
    
    /* top-right border-radius */
     #gvMain tr:first-child th:last-child,
     #gvMain.Info tr:first-child td:last-child {
        border-top-right-radius: 10px;
    }
    
    /* bottom-left border-radius */
     #gvMain tr:last-child td:first-child {
        border-bottom-left-radius: 10px;
    }
    
    /* bottom-right border-radius */
     #gvMain tr:last-child td:last-child {
        border-bottom-right-radius: 10px;
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
							<asp:Label ID="lblheading" runat="server" Text="Employee Referral"></asp:Label>
						</span>
					</div>
					<div class="leavegrid">
						<span>
						<a href="Requisition_Index.aspx" class="aaaa" style="margin-bottom:20px;">Recruitment Home</a>
					</span>

					</div>
					   <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
							<asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
							<table>

								<%--Bharat--%>
								<tr style="padding-top: 1px; padding-bottom: 2px;">
									
									<td class="formtitle">
                                        <asp:LinkButton ID="lnk_leaverequest" runat="server"  PostBackUrl="~/procs/Ref_CandidateInfo.aspx" >Refer Candidate</asp:LinkButton>                                         
                                    </td>
                                     <td class="formtitle">
                                        <asp:LinkButton ID="lnk_mng_leaverequest" runat="server" PostBackUrl="~/procs/MyRef_Candidate_Index.aspx">My Referrals</asp:LinkButton>
                                    </td>     
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 2px; display:none">
									<td class="formtitle">
                                       
										<br />
										<span id="span_App_head" runat="server" visible="false" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Moderator : </span>
									</td>
								</tr>
								<tr style="padding-top: 1px; padding-bottom: 4px; display:none">

									<td class="formtitle" style="padding-left:5px">
										<asp:LinkButton ID="lnk_CreateJDBank" Visible="false" runat="server" PostBackUrl="~/procs/Ref_Moderator_Index.aspx" >Inbox (0)</asp:LinkButton>
									</td>
									<td class="formtitle">
										<asp:LinkButton ID="lnk_MyJDBank" Visible="false" runat="server" PostBackUrl="~/procs/Ref_Moderator_View.aspx">All Referrals</asp:LinkButton>
									</td>

								</tr>

							</table>
							
							<span><br /><br />
							<asp:Label ID="Label1" CssClass="Position" runat="server" Text="Open Positions"></asp:Label>
						</span>
								
							<br />
							  <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
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
	<asp:HiddenField ID="HiddenField2" runat="server" />
	<asp:HiddenField ID="HiddenField4" runat="server" />
	<asp:HiddenField ID="HiddenField3" runat="server" />

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

