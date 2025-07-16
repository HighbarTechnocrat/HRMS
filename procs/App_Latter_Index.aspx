<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" ValidateRequest="false"
	AutoEventWireup="true" CodeFile="App_Latter_Index.aspx.cs" Inherits="procs_App_Latter_Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
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

        #MainContent_lnk_MY_CV:link, #MainContent_lnk_MY_CV:visited,
        #MainContent_lnk_Inbox_CV:link, #MainContent_lnk_Inbox_CV:visited,
        #MainContent_lnk_Board:link, #MainContent_lnk_Board:visited,
        #MainContent_lnk_Degree:link, #MainContent_lnk_Degree:visited
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

        #MainContent_lnk_MY_CV:hover, #MainContent_lnk_MY_CV:active
        #MainContent_lnk_Inbox_CV:hover, #MainContent_lnk_Inbox_CV:active,
        #MainContent_lnk_Board:hover, #MainContent_lnk_Board:active,
        #MainContent_lnk_Degree:hover, #MainContent_lnk_Degree:active
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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
	 <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Appointment Letter"></asp:Label>
                        </span>
                    </div>
					<span>
					<a href="https://ess.highbartech.com/hrms/PolicyProcedure.aspx" style="margin-right: 18px;" class="aaaa">Policy & Procedure</a>&nbsp;&nbsp; 
				</span>
                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="                                    font-size: medium;
                                    font-weight: bold;"></asp:Label>
                            <table id="tbl_menu" runat="server">
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MY_CV" runat="server" PostBackUrl="~/procs/App_Latter_Acceptance.aspx">Acceptance Appointment Letter</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Inbox_CV" Visible="true" runat="server" PostBackUrl="~/procs/App_Latter_Acceptance.aspx?Type=View">View Appointment Letter</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;" id="isShowAdmin" runat="server" visible="false">
                                    <td class="formtitle" colspan="2" style="padding-right:55px;">
                                        <br />
                                        <span runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Moderation: </span>
										<hr />
                                    </td>
                                     <%--<td><hr /></td>--%>
                                </tr>   
								<tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Board" runat="server" visible="false" PostBackUrl="~/procs/App_Latter_M_Index.aspx?Type=Pending">Inbox(0)</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Degree" Visible="false" runat="server" PostBackUrl="~/procs/App_Latter_M_Index.aspx?Type=APP">View Approval Appointment Letter</asp:LinkButton>
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


    <br />

    <asp:HiddenField ID="hflEmpCode" runat="server" />
    <asp:HiddenField ID="hflEmpDesignation" runat="server" />
    <asp:HiddenField ID="hflEmpDepartment" runat="server" />
    <asp:HiddenField ID="hflEmailAddress" runat="server" />
    <asp:HiddenField ID="hflGrade" runat="server" />
    <asp:HiddenField ID="hflapprcode" runat="server" />
    
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

        

    </script>
</asp:Content>

