<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="EmployeeCV.aspx.cs" ValidateRequest="false" Inherits="EmployeeCV" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <%--   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <%-- <link href="<%=ReturnUrl("css") %>calendar/calendar.css" rel="stylesheet" type="text/css" />
    <link href="sampleform_css.css" rel="stylesheet" type="text/css" />--%>
    <%--<ajaxToolkit:ToolkitScriptManager ID="srcp" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }

        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        #MainContent_btn_FD_Save,
        #MainContent_btn_FD_Update,
        #MainContent_btn_FD_Cancel,
        #MainContent_lnk_ed_Save,
        #MainContent_lnk_ed_Update,
        #MainContent_lnk_ed_Cancel,
        #MainContent_lnk_CD_Save,
        #MainContent_lnk_CD_Update,
        #MainContent_lnk_CD_Cancel,
        #MainContent_lnk_PD_Save,
        #MainContent_lnk_PD_Update,
        #MainContent_lnk_PD_Cancel,
        #MainContent_lnk_DD_Save,
        #MainContent_lnk_DD_Update,
        #MainContent_lnk_DD_Cancel,
        #MainContent_lnk_DE_Save,
        #MainContent_lnk_DE_Update,
        #MainContent_lnk_FinalSubmit,
        #MainContent_lnk_FileSave,
        #MainContent_lnk_FileUpdate,
        #MainContent_lnk_Update_Profile,
        #MainContent_lnk_Final_Profile,
        #MainContent_lnk_Final_Profile_2,
        #MainContent_lnk_Update_Profile_1,
        #MainContent_lnk_FileCancel {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }

        input.select2-search__field {
            padding-left: 0px !important;
            height:0px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--   <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>--%>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../js/HtmlControl/jHtmlArea-0.8.min.js"></script>
    <link href="../js/HtmlControl/jHtmlArea.css" rel="stylesheet" />
    <%--<script type="text/javascript">
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
    </script>--%>
    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="userposts">
                    <span>
                        <asp:Label ID="lblheading" runat="server" Text="CV Details"></asp:Label>
                    </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                </div>
                <span runat="server" id="backToSPOC" visible="false">
                    <a href="InboxServiceRequest.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToEmployee" visible="false">
                    <a href="MyService_Req.aspx" class="aaaa">Back</a>
                </span>
                <span runat="server" id="backToArr" visible="false">
                    <a href="InboxServiceRequest_Arch.aspx" class="aaaa">Back</a>
                </span>
                <span>
                    <a href="IndexEmployeeCV.aspx" style="margin-right: 18px;" class="aaaa">CV Update</a>&nbsp;&nbsp; 
                </span>
                <div class="edit-contact">
                    <div class="editprofile" style="text-align: center; border: none;" id="divmsg" runat="server" visible="false">
                    </div>
                    <div class="editprofile btndiv" id="divbtn" runat="server" visible="false">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkhome" Text="Go To Home" ToolTip="Home" CssClass="submitbtn" runat="server" OnClick="lnkhome_Click"></asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="lnkcont" Text="Continue" ToolTip="Continue" CssClass="submitbtn" runat="server"></asp:LinkButton>
                        </div>
                    </div>
                    <ul id="editform" runat="server" visible="false">
                        <li class="mobile_inboxEmpCode">
                            <span><b>Employee CV Details</b></span>
                            <br />
                           
                        </li>
                        <li> <span id="spanSuccess" style="color: green" runat="server" visible="false">Your CV is already Submitted for Review!</span>
                            <span id="spanError" style="color: red" runat="server" visible="false">Your CV is not yet Submitted for Review!</span>
                            <br /></li>
                        <li class="mobile_InboxEmpName">
                            <br />
                            <span>Employee Name</span><br />
                            <asp:TextBox AutoComplete="off" ID="txtEmpName" runat="server" Visible="True" Enabled="false"></asp:TextBox>
                        </li>
                        <li class="mobile_InboxEmpName">
                            <br />
                            <span>Project Code </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_ProjectCode" Enabled="false" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Employee Id</span><br />

                            <asp:TextBox AutoComplete="off" ID="txtEmpCode" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Band</span><br />

                            <asp:TextBox AutoComplete="off" ID="Txt_Band" runat="server" MaxLength="50" Visible="True" Enabled="false"> </asp:TextBox>

                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Department  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_Department" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>                       
                        <li class="mobile_inboxEmpCode">
                            <span>Designation  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Designation" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Module 2  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Module2" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Date of Birth</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="Txt_DOB" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Module 3  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Module3" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Project Manager/ Reporting Manager</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_RM" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>                      
                        <li class="mobile_inboxEmpCode">
                            <span>Program Manager</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PM" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Date of Joining  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_DOJ" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Delivery Manager</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_DM" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Email Address </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_EmailAddress" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>HOD</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_HOD" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Mobile Number </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_MobileNumber" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Marital Status</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_MaritalStatus" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Gender</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Gender" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Other Module  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtOtherModule" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Personal Email ID</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_P_EmailAddress" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                       <li class="mobile_inboxEmpCode">
                            <span>Mother Name  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_MotherName" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Father Name</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_FatherName" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                          <li class="mobile_inboxEmpCode">
                            <span>Permanent Address</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_P_Address" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Current Address</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_C_Address" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                         <li>
                            <span>Do you have passport?</span> <span style="color:red">(Note: Please upload your passport copy)</span>
                            <br />
                            <asp:CheckBox Visible="false" AutoComplete="off" AutoPostBack="false" ID="chk_Passport" OnCheckedChanged="chk_Passport_CheckedChanged" runat="server"></asp:CheckBox>
                               <asp:DropDownList runat="server" ID="ddl_IsPassport" AutoPostBack="false" OnSelectedIndexChanged="ddl_Relation_SelectedIndexChanged">
                                   <asp:ListItem Value="0">Select Do you have passport Yes/No?</asp:ListItem>
                                   <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                   <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        
                        <li class="mobile_inboxEmpCode">  
                                <span>Passport No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_passportNo" runat="server" AutoPostBack="false"></asp:TextBox>                            
                        </li>
                        <li class="mobile_inboxEmpCode">
                          <span>Place of Passport Issue</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Passport_Place_Issue" runat="server" AutoPostBack="false" MaxLength="50"></asp:TextBox>
                        </li>
                         <li class="mobile_inboxEmpCode">
                            <span>Name As Passport</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Name_As_Passport" MaxLength="100" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Passport Date of Issue.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_P_Date_Issue" runat="server" AutoPostBack="false"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender8" Format="dd/MM/yyyy" TargetControlID="txt_P_Date_Issue"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Passport Date of Expiry</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_P_Date_Expiry" runat="server" AutoPostBack="false"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender9" Format="dd/MM/yyyy" TargetControlID="txt_P_Date_Expiry"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li> 
                        
                         <li class="mobile_inboxEmpCode">
                            <span>Blood Group</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_BloodGroup" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                         <li class="mobile_inboxEmpCode">
                            <span>Primary Skill </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Module1" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                         </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Emergency Contact Person Name</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_ECP_Name" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Emergency Contact No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_ECP_Number" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                         <li class="mobile_inboxEmpCode">
                            <span>Name As PAN.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Name_As_PAN" MaxLength="100" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Pan Card No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PAN" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As Aadhar.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Name_As_Aadhar" runat="server" MaxLength="100" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Aadhar No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_AdharNo" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Bank Name</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_BankName" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Bank A/c No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_BankACCNo" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>IFSC Code</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_IFSCCode" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>EPF No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_EPF" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>UAN No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_UAN" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                       <li>
                            <span>Sabbatical / Educational Break / Job Break (eg. 1.0 - 1 year and 0 month)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_Sabbatical_Educational_Break" Enabled="true" MaxLength="5" runat="server"></asp:TextBox>

                        </li>  
                         <li ><br />
                            <span>Have you already completed Certification?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_IsAnyCertificateDone" AutoPostBack="false" OnSelectedIndexChanged="ddl_IsAnyCertificateDone_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Have you already completed Certification?">Select Have you already completed Certification?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                             <asp:CheckBox AutoComplete="off" AutoPostBack="false" Visible="false" ID="chk_Completed_Certification" OnCheckedChanged="chk_CD_Isompleted_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>
                        <li>
                            
                        </li>
                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_Update_Profile" Visible="true" runat="server" Text="Save As Draft" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveProfileClick();" OnClick="lnk_Update_Profile_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_Final_Profile" Visible="true" runat="server" Text="Submit For Review" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveFinalProfileClick();" OnClick="lnk_Final_Profile_Click"></asp:LinkButton>
                            
                        </li>
                        <li>
                            
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode" style="width: 100% !important">
                            <span><b>Family Details</b> </span><span>(Note: Please enter correct details of your family member - the data should not include deceased member details)</span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li></li>
                        <li>
                            <span>Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_FD_Name" MaxLength="250" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <span>Relation</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Relation" AppendDataBoundItems="false" AutoPostBack="false" OnSelectedIndexChanged="ddl_Relation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span>Date of Birth</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_FD_DOB" MaxLength="15" runat="server" OnTextChanged="txt_FD_DOB_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txt_FD_DOB"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li>
                            <span>Contact No.</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_FD_ContectNo" MaxLength="30" runat="server"></asp:TextBox>
                        </li>


                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="btn_FD_Save" Visible="true" runat="server" Text="Save Family Details" ToolTip="Save Family Details" OnClientClick="return SaveFDClick();" CssClass="Savebtnsve" OnClick="btn_FD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btn_FD_Update" Visible="false" runat="server" Text="Update Family Details" ToolTip="Update Family Details" OnClientClick="return SaveFD2Click();" CssClass="Savebtnsve" OnClick="btn_FD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="btn_FD_Cancel" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" OnClientClick="return SaveFD3Click();" CssClass="Savebtnsve" OnClick="btn_FD_Save_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <li style="width: 100%">
                            <br />
                            <div>

                                <asp:GridView ID="dg_FimalyDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,RelationId">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                          <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:ImageButton ID="lnk_FD_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_FD_Edit_Click" />
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                      </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name"
                                            DataField="Name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="40%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Relation"
                                            DataField="RelationName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Date Of Birth"
                                            DataField="DOB"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Contact No."
                                            DataField="ContactNo"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="30%" ItemStyle-BorderColor="Navy" />

                                       
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_FD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_FD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Education Details</b></span><span style="color:red"> (Note: Please upload the documents)</span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li>
                            <span>Qualification</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Qualification" AutoPostBack="false" OnSelectedIndexChanged="ddl_Qualification_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span>Degree</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Degree" AutoPostBack="true" OnSelectedIndexChanged="ddl_Degree_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_DegreeOther" Visible="false" MaxLength="250" runat="server"></asp:TextBox>
                        
                        </li>
                        <li>
                            <br />
                            <span>Board</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Board" AutoPostBack="false" OnSelectedIndexChanged="ddl_Board_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>University/Institute</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Institute" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                         <li>
                            <span>Have you already completed?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_ED_Iscompleted" AutoPostBack="true" OnSelectedIndexChanged="ddl_ED_Iscompleted_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Have you already completed Yes/No?">Select Have you already completed Yes/No?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox AutoComplete="off" Visible="false" AutoPostBack="true" ID="chk_ED_Iscompleted" OnCheckedChanged="chk_ED_Iscompleted_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>
                      <%--  <li>
                            <span>Have you already completed?</span>
                            <br />
                            <asp:CheckBox AutoComplete="off" AutoPostBack="true" ID="chk_ED_Iscompleted" OnCheckedChanged="chk_ED_Iscompleted_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>--%>
                        <li>
                            <span>Stream</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            
                            <asp:DropDownList runat="server" ID="ddl_Stream" AutoPostBack="true" OnSelectedIndexChanged="ddl_Stream_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                           <asp:TextBox AutoComplete="off" Visible="false" ID="txt_Stream" MaxLength="100" runat="server"></asp:TextBox>
                        
                        </li>
                        <li>
                            <span>Year of Passing</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_YearOfPassing" MaxLength="10" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" Format="dd/MM/yyyy" BehaviorID="calendar1" TargetControlID="txt_YearOfPassing">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li>
                            <br />
                            <span>Marks obtained</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_GradeMarks" MaxLength="7" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <span>Out of total marks</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_TotalMark" MaxLength="7" runat="server"></asp:TextBox>
                        </li>
                        <li></li>
                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_ed_Save" Visible="true" runat="server" Text="Save Education Details" ToolTip="Save Education Details" CssClass="Savebtnsve" OnClientClick="return SaveEDClick();" OnClick="lnk_ed_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_ed_Update" Visible="false" runat="server" Text="Update Education Details" ToolTip="Update Education Details" CssClass="Savebtnsve" OnClientClick="return SaveED2Click();" OnClick="lnk_ed_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_ed_Cancel" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return SaveED3Click();" OnClick="lnk_ed_Save_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <li style="width: 100%">
                            <br />
                            <div>

                                <asp:GridView ID="gv_EducationDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,QualificationId">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="lnk_ED_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_ED_Edit_Click" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                     </asp:TemplateField>
                                        <asp:BoundField HeaderText="Qualification"
                                            DataField="EducationType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="University/Institute"
                                            DataField="University_Institute"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Board"
                                            DataField="Board"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="13%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Degree"
                                            DataField="Degree"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Stream"
                                            DataField="Stream"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Date of Passing"
                                            DataField="YearOfPassing"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Marks Obtained"
                                            DataField="MarksObtained"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Total  Marks"
                                            DataField="TotalMarks"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="% Marks"
                                            DataField="Grade"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="5%" ItemStyle-BorderColor="Navy" />

                                        
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_ED_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_ED_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode" style="width:60% !important;">
                            <span><b>Certification Details</b></span> <span style="color:red"> (Note: Please upload your certificate copy)</span><br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label3" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li>
                            <br />
                            <span>Name of Certification</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_CD_Certification" AutoPostBack="false" OnSelectedIndexChanged="ddl_CD_Certification_SelectedIndexChanged1">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span>Module</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_CD_Module" AutoPostBack="false" OnSelectedIndexChanged="ddl_CD_Module_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Institute Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_CD_InstituteName" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <br />
                            <span>Certification Course Code</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_CertificationCode" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <span>Have you already completed?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_CD_Isompleted" AutoPostBack="true" OnSelectedIndexChanged="ddl_CD_Isompleted_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Have you already completed Yes/No?">Select Have you already completed Yes/No?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox AutoComplete="off" AutoPostBack="true" Visible="false" ID="chk_CD_Isompleted" OnCheckedChanged="chk_CD_Isompleted_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>
                       <%-- <li>
                            <span>Have you already completed?</span>
                            <br />
                            <asp:CheckBox AutoComplete="off" AutoPostBack="true" ID="chk_CD_Isompleted" OnCheckedChanged="chk_CD_Isompleted_CheckedChanged" runat="server"></asp:CheckBox>

                        </li>--%>
                        <li>
                            <br />
                            <span>Certification ID/NO.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_CD_CertificationNo" MaxLength="100" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <span>Valid From Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_CD_FromDate" MaxLength="15" runat="server" OnTextChanged="txt_FD_DOB_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txt_CD_FromDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li>
                            <span>Valid To Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_CD_ToDate" MaxLength="15" runat="server" OnTextChanged="txt_FD_DOB_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="txt_CD_ToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_CD_Save" Visible="true" runat="server" Text="Save Certification Details" ToolTip="Save Certification Details" CssClass="Savebtnsve" OnClientClick="return SaveCDClick();" OnClick="lnk_CD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_CD_Update" Visible="false" runat="server" Text="Update Certification Details" ToolTip="Update Certification Details" CssClass="Savebtnsve" OnClientClick="return SaveCD2Click();" OnClick="lnk_CD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_CD_Cancel" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return SaveCD3Click();" OnClick="lnk_CD_Save_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <li style="width: 100%">
                            <br />
                            <div>

                                <asp:GridView ID="gv_CertificationDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                             <ItemTemplate>
                                                 <asp:ImageButton ID="lnk_Certi_D_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_Certi_D_Edit_Click" />
                                             </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                         </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name of Certification"
                                            DataField="Certification"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        
                                        <asp:BoundField HeaderText="Course Code"
                                            DataField="CourseCode"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Certification No"
                                            DataField="CertificationNo"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Institute Name"
                                            DataField="InstituteName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="20%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Module"
                                            DataField="Module"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Valid From"
                                            DataField="ValidFromDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Valid To"
                                            DataField="ValidTODate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />

                                        
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_CD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_CD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode" style="width: 100% !important">

                            <span><b>Professional Experience </b></span><span style="color: red;">(Note: Please ensure you fill your experience in ascending order starting from current project at Highbar)</span>
                            <br />
                            <span>Summary of Experience</span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label6" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                        <li></li>
                        <li>
                            <span>Total Domain Experience(years)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_TotalDomainExp" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <span>Total SAP Experience(years)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_TotalSAPExp" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li style="display:none">
                            <span>Total Non SAP Experience(years)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_Non_SAPExp" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <span>Overall Work Experience(years)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_TotalOverallExp" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <span>Sabbatical / Educational Break / Job Break (eg. 1.0 - 1 year and 0 month)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_EducationalB" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_DE_Save" Visible="true" runat="server" Text="Save" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveDEClick();" OnClick="lnk_DE_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_DE_Update" Visible="false" runat="server" Text="Update" ToolTip="Update" CssClass="Savebtnsve" OnClientClick="return SaveDE2Click();" OnClick="lnk_DE_Save_Click"></asp:LinkButton>

                        </li>
                        <li></li>
                        <hr />
                        <li>
                            <span><b>Project Details </b></span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label4" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                          <li><br />
                              <span>Do you have project experience?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_IsAnyProject" AutoPostBack="true" OnSelectedIndexChanged="ddl_IsAnyProject_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Do you have project experience?">Do you have project experience?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox AutoComplete="off" Visible="false" AutoPostBack="true" ID="CheckBox1" OnCheckedChanged="chk_PD_IsCurrentProject_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>
                        <li></li>
                        <li style ="display:none"><br />
                            <span>Is this project SAP/Non SAP?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_PD_ProjectSAPORNON" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectSAPORNON_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select project SAP/Non SAP?">Select project SAP/Non SAP?</asp:ListItem>
                                <asp:ListItem Value="SAP" Text="SAP">SAP</asp:ListItem>
                                <asp:ListItem Value="NONSAP" Text="SAP">NON SAP</asp:ListItem>
                            </asp:DropDownList></li>
                        <li>
                            <br />
                            <span>Project Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_PD_ProjectType" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span>Industry Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Visible="false" AutoComplete="off" ID="txt_PD_IndustryType" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:ListBox runat="server" ID="lst_PD_IndustryType" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li>
                            <br />
                            <span>Role/Designation</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_PD_Role" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Modules Integration</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_PD_Module" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span>Organisation Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_PD_OrgType" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Organisation Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            
                            <asp:DropDownList runat="server" ID="ddl_PD_OrgName" AutoPostBack="true" OnSelectedIndexChanged="ddl_PD_OrgName_SelectedIndexChanged">
                            </asp:DropDownList>
                             <br />
                             <asp:TextBox Visible="false" AutoComplete="off" ID="txt_PD_Organisation" MaxLength="250" runat="server"></asp:TextBox>
                        </li>                       
                        <li>
                            <br />
                            <span>Product Implemented</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PD_ProductImplemented" CssClass="noresize" TextMode="MultiLine" Rows="4" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <span>Brief Summary of Role</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PD_Summary" CssClass="noresize" TextMode="MultiLine" Rows="4" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                         <li>                            
                            <span>Client Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PD_ClientName" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                         <li>                           
                            <span>Region</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Region" AutoPostBack="false" OnSelectedIndexChanged="ddl_Region_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                      <%--  <li>
                            <span>Is this your current project</span>
                            <br />
                            <asp:CheckBox AutoComplete="off" AutoPostBack="true" ID="chk_PD_IsCurrentProject" OnCheckedChanged="chk_PD_IsCurrentProject_CheckedChanged" runat="server"></asp:CheckBox>

                        </li>--%>
                        <li>
                            <span>Is this your current project?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddlPD_IsCurrentProject" AutoPostBack="true" OnSelectedIndexChanged="ddlPD_IsCurrentProject_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Is this your current project Yes/No">Select Have you already completed Yes/No</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox AutoComplete="off" Visible="false" AutoPostBack="true" ID="chk_PD_IsCurrentProject" OnCheckedChanged="chk_PD_IsCurrentProject_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>
                        <li></li>
                        <li>
                            <br />
                            <span>From Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PD_FromDate" MaxLength="15" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="txt_PD_FromDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li>
                            <span>To Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_PD_ToDate" MaxLength="15" runat="server" OnTextChanged="txt_PD_FromDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="txt_PD_ToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_PD_Save" Visible="true" runat="server" Text="Save Project Details" ToolTip="Save Project Details" CssClass="Savebtnsve" OnClientClick="return SavePDClick();" OnClick="lnk_PD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_PD_Update" Visible="false" runat="server" Text="Update Project Details" ToolTip="Update Project Details" CssClass="Savebtnsve" OnClientClick="return SavePD2Click();" OnClick="lnk_PD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_PD_Cancel" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return SavePD3Click();" OnClick="lnk_PD_Save_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <li style="width: 103%">
                            <br />
                            <div>

                                <asp:GridView ID="gv_ProjectDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="lnk_PD_Edit" runat="server" Width="13px" Height="13px" ImageUrl="~/Images/edit.png" OnClick="lnk_PD_Edit_Click" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                     </asp:TemplateField>
                                        <asp:BoundField HeaderText="Project Type"
                                            DataField="ProjectType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Client Name"
                                            DataField="ClientName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Organisation Name"
                                            DataField="OrganisationName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Organisation Type"
                                            DataField="OrganisationType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Industry Type"
                                            DataField="IndustryType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Modules Integration"
                                            DataField="ModuleDesc"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Role/Designation"
                                            DataField="Designation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="From Date"
                                            DataField="FromDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="To Date"
                                            DataField="ToDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="9%" ItemStyle-BorderColor="Navy" />
                                        <%--  <asp:BoundField HeaderText="Brief Summary of Role"
                                            DataField="BriefSummaryOfRole"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />--%>
                                        
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_PD_Delete" runat="server" Width="13px" Height="13px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_PD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <span><b>Domain Details </b></span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label5" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li><br />
                              <span>Do you have Domain experience?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_IsAnyDomain" AutoPostBack="true" OnSelectedIndexChanged="ddl_IsAnyDomain_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Do you have domain experience?">Do you have domain experience?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>

                        </li>
                        <li></li>
                        <li>
                            <br />
                            <span>Organisation Name</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            
                            <asp:DropDownList runat="server" ID="ddl_DD_OrgName" AutoPostBack="true" OnSelectedIndexChanged="ddl_DD_OrgName_SelectedIndexChanged">
                            </asp:DropDownList>
                            <br />
                            <asp:TextBox Visible="false" AutoComplete="off" ID="txt_DD_Organisation" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                        <li>
                            <br />
                            <span>Organisation Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_DD_OrgType" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <span>Domain</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Visible="false" AutoComplete="off" ID="txt_DD_Domain" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:DropDownList runat="server" ID="ddl_DD_Domain" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Industry Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox Visible="false" AutoComplete="off" ID="txt_DD_IndustryType" MaxLength="100" runat="server"></asp:TextBox>
                            <asp:ListBox runat="server" ID="lst_DD_IndustryType" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                       <li>
                            <br />
                            <span>Role</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_DD_Role" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                         <li>
                            <span>Is this your current experience?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                             <asp:DropDownList runat="server"  ID="ddl_DD_IsThisCurrent" AutoPostBack="true" OnSelectedIndexChanged="ddl_DD_IsThisCurrent_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Is this your current experience Yes/No?">Select Is this your current experience Yes/No?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox AutoComplete="off" AutoPostBack="true" Visible="false" ID="chk_DD_IsComplited" OnCheckedChanged="chk_DD_IsComplited_CheckedChanged" runat="server"></asp:CheckBox>
                        </li>
                       <%--<li>
                            <span>Is this your current experience?</span>
                            <br />
                            <asp:CheckBox AutoComplete="off" AutoPostBack="true" ID="chk_DD_IsComplited" OnCheckedChanged="chk_DD_IsComplited_CheckedChanged" runat="server"></asp:CheckBox>

                        </li>--%>
                        <li>
                            <br>
                            <span>Responsibilities Handled</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_DD_Responsibilities" CssClass="noresize" TextMode="MultiLine" Rows="4" MaxLength="250" runat="server"></asp:TextBox>
                        </li>
                       <li></li>
                        <li>
                            <br />
                            <span>From Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_DD_FromDate" MaxLength="15" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" Format="dd/MM/yyyy" TargetControlID="txt_DD_FromDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>
                        <li>
                            <span>To Date</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_DD_ToDate" MaxLength="15" runat="server" OnTextChanged="txt_DD_ToDate_TextChanged"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender7" Format="dd/MM/yyyy" TargetControlID="txt_DD_ToDate"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_DD_Save" Visible="true" runat="server" Text="Save Domain Details" ToolTip="Save Domain Details" CssClass="Savebtnsve" OnClientClick="return SaveDDClick();" OnClick="lnk_DD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_DD_Update" Visible="false" runat="server" Text="Update Domain Details" ToolTip="Update Domain Details" CssClass="Savebtnsve" OnClientClick="return SaveDD2Click();" OnClick="lnk_DD_Save_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_DD_Cancel" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return SaveDD3Click();" OnClick="lnk_DD_Save_Click"></asp:LinkButton>
                        </li>
                        <li></li>
                        <li style="width: 100%">
                            <br />
                            <div>

                                <asp:GridView ID="gv_DomainDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="lnk_DD_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_DD_Edit_Click" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                     </asp:TemplateField>
                                        <asp:BoundField HeaderText="Organisation Name"
                                            DataField="OrganisationName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Domain"
                                            DataField="Domain"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Organisation Type"
                                            DataField="OrganisationType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Roles/Designsation"
                                            DataField="Designation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Industry Type"
                                            DataField="IndustryType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="From date"
                                            DataField="FromDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="To Date"
                                            DataField="ToDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_DD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_DD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr style="display:none" />
                        <li style="display:none">
                            <span><b>Profile Summary </b></span>
                            <br />
                        </li>
                        <li style="display:none">
                            <asp:Label runat="server" ID="Label8" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li  style="width: 100% !important;display:none">
                            <br />
                            <span>Description (Describe in 500 Char about your profile )</span>&nbsp;&nbsp;<br />
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txtJobDescription" runat="server" Rows="20" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <li style="margin-top: 10px;display:none">
                            <asp:LinkButton ID="lnk_FinalSubmit" Visible="true" runat="server" Text="Update Summary" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveDDClick();" OnClick="lnk_FinalSubmit_Click"></asp:LinkButton>
                        </li>
                        <hr />
                        <li>
                            <span><b>Upload your documents </b></span>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label7" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li>
                            <br />
                            <span>Document Type</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_DocumentName" AutoPostBack="false" OnSelectedIndexChanged="ddl_PD_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </li>
                        <li>
                            <br />
                            <span>Upload File</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:FileUpload ID="uploadfile" runat="server" />
                        </li>
                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_FileSave" Visible="true" runat="server" Text="Save Document" ToolTip="Save Document" CssClass="Savebtnsve" OnClientClick="return SaveFileClick();" OnClick="lnk_FileSave_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_FileUpdate" Visible="false" runat="server" Text="Update Document" ToolTip="Update Document" CssClass="Savebtnsve" OnClientClick="return SaveFile1Click();" OnClick="lnk_FileSave_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_FileCancel" Visible="false" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClientClick="return SaveFile2Click();" OnClick="lnk_FileSave_Click"></asp:LinkButton>
                        </li>
                        <li style="width: 100%">
                            <br />
                            <div>
                                <asp:GridView ID="gv_Documents" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Id,FilePath">
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                    <PagerStyle ForeColor="#000066" HorizontalAlign="Center" BackColor="White" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                         <ItemTemplate>
                                             <asp:ImageButton ID="lnk_File_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_File_Edit_Click" />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                     </asp:TemplateField>
                                            <asp:BoundField HeaderText="Document Type"
                                            DataField="DocumentType"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="File Uploaded"
                                            DataField="FilePath"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />
                                        <asp:TemplateField HeaderText="File View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit1" runat="server" ToolTip=" File View" Width="15px" Height="15px" ImageUrl="~/Images/Download.png" OnClientClick=<%# "DownloadFile('" + Eval("FilePath") + "')" %> />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_File_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_File_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <asp:LinkButton ID="lnk_Update_Profile_1" Visible="true" runat="server" Text="Save As Draft" ToolTip="Save" CssClass="Savebtnsve" OnClientClick="return SaveProfileClick();" OnClick="lnk_Update_Profile_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_Final_Profile_2" Visible="true" runat="server" Text="Submit For Review" ToolTip="Submit For Review" CssClass="Savebtnsve" OnClientClick="return SaveFinalProfileClick_2();" OnClick="lnk_Final_Profile_Click"></asp:LinkButton>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnFamilyDetailID" runat="server" />
    <asp:HiddenField ID="hdnEduactonDetailID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileDetailID" runat="server" />
    <asp:HiddenField ID="hdnFileName" runat="server" />
    <asp:HiddenField ID="hdnFilePath" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_Relation").select2();
            $("#MainContent_ddl_Qualification").select2();
            $("#MainContent_ddl_Board").select2();
            $("#MainContent_ddl_Degree").select2();
            $("#MainContent_ddl_CD_Certification").select2();
            $("#MainContent_ddl_CD_Module").select2();
            $("#MainContent_ddl_PD_ProjectType").select2();
            $("#MainContent_lst_PD_IndustryType").select2();
            $("#MainContent_ddl_PD_Role").select2();
            $("#MainContent_ddl_PD_Module").select2();
            $("#MainContent_lst_DD_IndustryType").select2();
            $("#MainContent_ddl_DD_Domain").select2();
            $("#MainContent_ddl_DD_Role").select2();
            $("#MainContent_ddl_Stream").select2();
            $("#MainContent_ddl_PD_OrgType").select2();
            $("#MainContent_ddl_PD_OrgName").select2();
            $("#MainContent_ddl_DD_OrgType").select2();
            $("#MainContent_ddl_DD_OrgName").select2();
            $("#MainContent_ddl_DocumentName").select2();
            $("#MainContent_ddl_Region").select2();
            $("#MainContent_ddl_IsPassport").select2();
            $("#MainContent_ddl_IsAnyProject").select2();
            $("#MainContent_ddl_PD_ProjectSAPORNON").select2();
            $("#MainContent_ddlPD_IsCurrentProject").select2();
            $("#MainContent_ddl_IsAnyCertificateDone").select2();
            $("#MainContent_ddl_DD_IsThisCurrent").select2();
            $("#MainContent_ddl_CD_Isompleted").select2();
            $("#MainContent_ddl_ED_Iscompleted").select2();
            $("#MainContent_ddl_IsAnyDomain").select2();
            $("#MainContent_txtJobDescription").htmlarea();
        });
    </script>
    <script type="text/javascript">

        function onCalendarShown() {
            var cal = $find("calendar1");
            cal._switchMode("years", true);
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            if (cal._yearsBody) {
                for (var i = 0; i < cal._yearsBody.rows.length; i++) {
                    var row = cal._yearsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "year":
                    var cal = $find("calendar1");
                    cal.set_selectedDate(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged(); break;
            }
        }
        function checkProjectEndDate(sender, args) {
            if (sender._selectedDate >= new Date()) {
                alert("You can not select a future date than today!");
                sender._selectedDate = new Date();
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }

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



        function onCharOnlyNumber(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        //Cinfirmation Family Details
        function SaveFDClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_FD_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveFD2Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_FD_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveFD3Click() {
            try {
                var msg = "Do you want to cancel?";
                var retunboolean = true;
                var ele = document.getElementById('<%=btn_FD_Cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Family Details
        //Eduaction Details 
        function SaveEDClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_ed_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveED2Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_ed_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveED3Click() {
            try {
                var msg = "Do you want to cancel?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_ed_Cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Education Details

        //Certification Details 
        function SaveCDClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_CD_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveCD2Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_CD_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveCD3Click() {
            try {
                var msg = "Do you want to cancel?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_CD_Cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Certification Details
        //OverAll EXP Details 
        function SaveDEClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_DE_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveDE2Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_DE_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Over All Exp Details
        //Project Details 
        function SavePDClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_PD_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SavePD2Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_PD_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SavePD3Click() {
            try {
                var msg = "Do you want to cancel?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_PD_Cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Project Details
        //Domain Details 
        function SaveDDClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_DD_Save.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveDD2Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_DD_Update.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveDD3Click() {
            try {
                var msg = "Do you want to cancel?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_DD_Cancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Domain Details
        //Document Details

        function SaveFileClick() {
            try {
                var msg = "Do you want to Submit";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_FileSave.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveFile1Click() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_FileUpdate.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveFile2Click() {
            try {
                var msg = "Do you want to cancel?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_FileCancel.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true) {
                        var confirm_value = document.createElement("INPUT");
                        confirm_value.type = "hidden";
                        confirm_value.name = "confirm_value";
                        confirm_value.value = "Yes";
                        document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
                    }
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //SaveProfileClick  SaveFinalProfileClick
        function SaveProfileClick() {
            try {
                var msg = "Do you want to update?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Update_Profile.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }

        function SaveFinalProfileClick() {
            try {
                var msg = "Do you want to submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Final_Profile.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        function SaveFinalProfileClick_2() {
            try {
                var msg = "Do you want to submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Final_Profile_2.ClientID%>');

                if (ele != null && !ele.disabled)
                    retunboolean = true;
                else
                    retunboolean = false;
                if (ele != null) {
                    ele.disabled = true;
                    if (retunboolean == true)
                        Confirm(msg);
                }
            }
            catch (err) {
                alert(err.description);
            }
            return retunboolean;
        }
        //End Document Details

        function Confirm(msg) {
            //Testing();
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm(msg)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }

            document.getElementById("<%=hdnYesNo.ClientID%>").value = confirm_value.value;
            return;
        }
        // Delete

        //End Delete
        function onCharOnlyNumber_EXP(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789.]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function onCharOnlyNumber_Mobile(e) {
            var keynum;
            var keychar;
            var numcheck = /[0123456789]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function noanyCharecters(e) {
            var keynum;
            var keychar;
            var numcheck = /[]/;

            if (window.event) {
                keynum = e.keyCode;
            }
            else if (e.which) {
                keynum = e.which;
            }
            keychar = String.fromCharCode(keynum);
            return numcheck.test(keychar);
        }
        function sumOfExp(e) {
            var sum = 0.0;
            var totalDomainExp = document.getElementById("<%=txt_TotalDomainExp.ClientID%>").value;
            var totalSAPExp = document.getElementById("<%=txt_TotalSAPExp.ClientID%>").value;
            if (totalDomainExp != "") {
                sum = parseFloat(sum) + parseFloat(totalDomainExp);
            }
            if (totalSAPExp != "") {
                sum = parseFloat(sum) + parseFloat(totalSAPExp);
            }
            if (sum != 0.0) {
                sum = parseFloat(sum).toFixed(2);
                document.getElementById("<%=txt_TotalOverallExp.ClientID%>").value = sum;
            }
        }
        function DownloadFile(FileName) {

            var localFilePath = document.getElementById("<%=FilePath.ClientID%>").value;
            //alert(FileName);        
            window.open("https://ess.highbartech.com/oneHRAPI/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
           // window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }
    </script>
</asp:Content>
