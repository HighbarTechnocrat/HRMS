<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true"
    CodeFile="IndexEmployeeCV.aspx.cs" Inherits="IndexEmployeeCV" %>

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

        #MainContent_lnk_MY_CV:link, #MainContent_lnk_MY_CV:visited,
        #MainContent_lnk_Inbox_CV:link, #MainContent_lnk_Inbox_CV:visited,
        #MainContent_lnk_Board:link, #MainContent_lnk_Board:visited,
        #MainContent_lnk_Degree:link, #MainContent_lnk_Degree:visited,
        #MainContent_LinkButton3:link, #MainContent_LinkButton3:visited,
        #MainContent_LinkButton4:link, #MainContent_LinkButton4:visited,
        #MainContent_LinkButton5:link, #MainContent_LinkButton5:visited,
        #MainContent_LinkButton6:link, #MainContent_LinkButton6:visited,
        #MainContent_LinkButton7:link, #MainContent_LinkButton7:visited,
        #MainContent_LinkButton8:link, #MainContent_LinkButton8:visited,
        #MainContent_LinkButton9:link, #MainContent_LinkButton9:visited,
        #MainContent_LinkButton10:link, #MainContent_LinkButton10:visited,
        #MainContent_LinkButton11:link, #MainContent_LinkButton11:visited,
        #MainContent_LinkButton12:link, #MainContent_LinkButton12:visited,
        #MainContent_lnk_hrView:link, #MainContent_lnk_hrView:visited,
        #MainContent_Lnk_HRUpdateCV:link, #MainContent_Lnk_HRUpdateCV:visited,
		#MainContent_lnk_EmpCVReview:link, #MainContent_lnk_EmpCVReview:visited,
        #MainContent_BtnLink_DownLoadCVDump:link, #MainContent_BtnLink_DownLoadCVDump:visited
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
        #MainContent_lnk_Degree:hover, #MainContent_lnk_Degree:active,
        #MainContent_LinkButton3:hover, #MainContent_LinkButton3:active,
        #MainContent_LinkButton4:hover, #MainContent_LinkButton4:active,
        #MainContent_LinkButton5:hover, #MainContent_LinkButton5:active,
        #MainContent_LinkButton6:hover, #MainContent_LinkButton6:active,
        #MainContent_LinkButton7:hover, #MainContent_LinkButton7:active,
        #MainContent_LinkButton8:hover, #MainContent_LinkButton8:active,
        #MainContent_LinkButton9:hover, #MainContent_LinkButton9:active,
        #MainContent_LinkButton10:hover, #MainContent_LinkButton10:active,
        #MainContent_LinkButton11:hover, #MainContent_LinkButton11:active,
        #MainContent_LinkButton12:hover, #MainContent_LinkButton12:active,
         #MainContent_Lnk_HRUpdateCV:hover, #MainContent_Lnk_HRUpdateCV:active,
        #MainContent_lnk_hrView:hover, #MainContent_lnk_hrView:active,
		 #MainContent_lnk_EmpCVReview:hover, #MainContent_lnk_EmpCVReview:active,
        #MainContent_BtnLink_DownLoadCVDump:hover, #MainContent_BtnLink_DownLoadCVDump:active {
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

</script>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="CV Update"></asp:Label>
                        </span>
                    </div>

                    <div class="editprofile" id="editform1" runat="server" visible="true">
                        <div class="editprofileform">
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="                                    font-size: medium;
                                    font-weight: bold;"></asp:Label>
                            <table id="tbl_menu" runat="server">
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_MY_CV" runat="server" PostBackUrl="~/procs/EmployeeCV.aspx">Update CV</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Inbox_CV" Visible="true" runat="server" PostBackUrl="~/procs/InboxEmployeeCV.aspx">View Employee CV</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">                                    
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_hrView" Visible="false" runat="server" PostBackUrl="~/procs/InboxEmployeeCVHR.aspx">View All Employee CV</asp:LinkButton>
                                    </td>
                                     <td class="formtitle">
                                        <asp:LinkButton ID="Lnk_HRUpdateCV"  runat="server" Visible="false" PostBackUrl="~/procs/HREmployeeCV.aspx">HR - Update CV</asp:LinkButton>
                                   </td>
                                </tr>
                                <tr style="padding-top:1px;padding-bottom:2px;" id="isShowAdmin" runat="server" visible="false">
                                    <td class="formtitle">
                                        <br />
                                        <span runat="server" style="font-size: 12pt; font-weight: bold; color: #3D1956;">Admin: </span>
                                    </td>
                                     <td></td>
                                </tr>   
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Board" Visible="false" runat="server" PostBackUrl="~/procs/Board_CV.aspx">Add/Update Board</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_Degree" Visible="false" runat="server" PostBackUrl="~/procs/Degree_CV.aspx">Add/Update Degree</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton3" Visible="false" runat="server" PostBackUrl="~/procs/Certification_CV.aspx">Add/Update Certification</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton4" Visible="false" runat="server" PostBackUrl="~/procs/Designation_CV.aspx">Add/Update Designation</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton5" Visible="false" runat="server" PostBackUrl="~/procs/Domain_CV.aspx">Add/Update Domain</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton6" Visible="false" runat="server" PostBackUrl="~/procs/IndustryType_CV.aspx">Add/Update Industry Type</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton7" Visible="false" runat="server" PostBackUrl="~/procs/ProjectType_CV.aspx">Add/Update Project Type</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton8" Visible="false" runat="server" PostBackUrl="~/procs/DocumentType_CV.aspx">Add/Update Document Type</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton9" Visible="false" runat="server" PostBackUrl="~/procs/OrganisationName_CV.aspx">Add/Update Organisation Name</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton10" Visible="false" runat="server" PostBackUrl="~/procs/OrganisationType_CV.aspx">Add/Update Organisation Type</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton11" Visible="false" runat="server" PostBackUrl="~/procs/Stream_CV.aspx">Add/Update Stream</asp:LinkButton>
                                    </td>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="LinkButton12" Visible="false" runat="server" PostBackUrl="~/procs/ReportAccess_CV.aspx">Add/Update Report Access</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="padding-top: 1px; padding-bottom: 2px;">
                                    <td class="formtitle">
                                              <asp:LinkButton ID="BtnLink_DownLoadCVDump" Visible="false" OnClick="BtnLink_DownLoadCVDump_Click" runat="server" >DownLoad CV Dump</asp:LinkButton>
                                  

                                    </td>
                                    <td class="formtitle"></td>
                                </tr>
								 <tr>
                                    <td class="formtitle">
                                        <asp:LinkButton ID="lnk_EmpCVReview" Visible="false" runat="server" PostBackUrl="~/procs/EmployeeCVReviewInbox.aspx">Employee CV Review</asp:LinkButton>
                                    </td>
                                     <td></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="50" Visible="false"> </asp:TextBox>

    <br />

    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />

    <asp:HiddenField ID="hflEmpDepartment" runat="server" />

    <asp:HiddenField ID="hflEmailAddress" runat="server" />

    <asp:HiddenField ID="hflGrade" runat="server" />

    <asp:HiddenField ID="hflapprcode" runat="server" />
    <asp:HiddenField ID="hdnempwrkSchdule" runat="server" />
    <asp:HiddenField ID="hdnisViewAttendance" runat="server" />


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
