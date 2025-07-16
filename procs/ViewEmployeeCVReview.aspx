<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="ViewEmployeeCVReview.aspx.cs" ValidateRequest="false" Inherits="ViewEmployeeCVReview" %>

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
        #MainContent_lnk_Status_Submit,
        #MainContent_lnk_FileCancel {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
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
                        <asp:Label ID="lblheading" runat="server" Text="CV Review"></asp:Label>
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
                    <a href="EmployeeCVReviewInbox.aspx" style="margin-right: 18px;" class="aaaa">Back</a>&nbsp;&nbsp; 
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
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txtOtherModule" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Personal Email ID</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_P_EmailAddress" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Mother Name  </span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_MotherName" runat="server"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Father Name</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_FatherName" Enabled="false" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Permanent Address</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_P_Address" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Current Address</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_C_Address" CssClass="noresize" TextMode="MultiLine" Rows="4" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li>
                            <span>Do you have passport?</span>
                            <br />
                            <asp:DropDownList runat="server" Enabled="false" ID="ddl_IsPassport" AutoPostBack="false" OnSelectedIndexChanged="ddl_IsPassport_SelectedIndexChanged1">
                                <asp:ListItem Value="0">Select Do you have passport Yes/No?</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Passport No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_passportNo" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Place of Passport Issue</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Passport_Place_Issue" runat="server" AutoPostBack="false" MaxLength="50"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As Passport</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Name_As_Passport" MaxLength="100" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Passport Date of Issue.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_P_Date_Issue" runat="server" AutoPostBack="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender8" Format="dd/MM/yyyy" TargetControlID="txt_P_Date_Issue"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Passport Date of Expiry</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_P_Date_Expiry" runat="server" AutoPostBack="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender9" Format="dd/MM/yyyy" TargetControlID="txt_P_Date_Expiry"
                                runat="server">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Blood Group</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_BloodGroup" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Primary Skill </span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_Module1" Enabled="false" runat="server" AutoPostBack="true"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Emergency Contact Person Name</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_ECP_Name" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Emergency Contact No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_ECP_Number" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As PAN.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Name_As_PAN" MaxLength="100" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Pan Card No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_PAN" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Name As Aadhar.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Name_As_Aadhar" runat="server" MaxLength="100" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Aadhar No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_AdharNo" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>Bank Name</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_BankName" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>Bank A/c No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_BankACCNo" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>IFSC Code</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_IFSCCode" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>

                        <li class="mobile_inboxEmpCode">
                            <span>EPF No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_EPF" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li class="mobile_inboxEmpCode">
                            <span>UAN No.</span>
                            <br />
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_UAN" runat="server" AutoPostBack="false"></asp:TextBox>
                        </li>
                        <li>
                            <span>Sabbatical / Educational Break / Job Break (eg. 1.0 - 1 year and 0 month)</span>
                            <asp:TextBox AutoComplete="off" Enabled="false" ID="txt_Sabbatical_Educational_Break" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <br />
                            <span>Have you already completed Certification?</span>&nbsp;&nbsp;
                            <br />
                            <asp:DropDownList runat="server" Enabled="false" ID="ddl_IsAnyCertificateDone" AutoPostBack="false" OnSelectedIndexChanged="ddl_IsAnyCertificateDone_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Have you already completed Certification?">Select Have you already completed Certification?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>
                            <asp:CheckBox AutoComplete="off" AutoPostBack="false" Visible="false" ID="chk_Completed_Certification" OnCheckedChanged="chk_Completed_Certification_CheckedChanged1" runat="server"></asp:CheckBox>
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode" style="width: 100% !important">
                            <span><b>Family Details</b> </span><span></span>
                            <br />
                            <br />
                        </li>
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
                                        <asp:BoundField HeaderText="Name"
                                            DataField="Name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="25%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Relation"
                                            DataField="RelationName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Date Of Birth"
                                            DataField="DOB"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Contact No."
                                            DataField="ContactNo"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_FD_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_FD_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_FD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_FD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Education Details</b></span><br />
                            <br />
                        </li>
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
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Marks obtained"
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
                                            ItemStyle-Width="7%" ItemStyle-BorderColor="Navy" />

                                        <%--  <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_ED_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_ED_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_ED_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_ED_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode">
                            <span><b>Certification Details</b></span><br />
                        </li>
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
                                        <asp:BoundField HeaderText="Name of Certification"
                                            DataField="Certification"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Certification No"
                                            DataField="CertificationNo"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Institute Name"
                                            DataField="InstituteName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Module"
                                            DataField="Module"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="15%" ItemStyle-BorderColor="Navy" />

                                        <asp:BoundField HeaderText="Valid From"
                                            DataField="ValidFromDate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />
                                        <asp:BoundField HeaderText="Valid To"
                                            DataField="ValidTODate"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="10%" ItemStyle-BorderColor="Navy" />

                                        <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_Certi_D_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_Certi_D_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_CD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_CD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li class="mobile_inboxEmpCode" style="width: 100% !important">

                            <span><b>Professional Experience </b></span><span></span>
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
                        <li>
                            <span>Overall Work Experience(years)</span>
                            <asp:TextBox AutoComplete="off" ID="txt_TotalOverallExp" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <li>
                            <span>Sabbatical / Educational Break </span>
                            <asp:TextBox AutoComplete="off" ID="txt_Educational_Break" Enabled="false" MaxLength="5" runat="server"></asp:TextBox>

                        </li>
                        <hr />
                        <li>
                            <span><b>Project Details </b></span>
                        </li>
                        <li></li>
                        <li>
                            <br />
                            <span>Do you have project experience?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_IsAnyProject" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddl_IsAnyProject_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Do you have project experience?">Do you have project experience?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>

                        </li>
                        <li></li>
                        <li style="width: 100%">
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
                                        <%--  <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_PD_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_PD_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_PD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_PD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <span><b>Domain Details </b></span>
                        </li>
                        <li></li>
                         <li>
                            <br />
                            <span>Do you have domain experience?</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_IsDomain" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddl_IsAnyProject_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Do you have domain experience?">Do you have domain experience?</asp:ListItem>
                                <asp:ListItem Value="Yes" Text="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No" Text="No">No</asp:ListItem>
                            </asp:DropDownList>

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
                                        <%--   <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_DD_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_DD_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_DD_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_DD_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <span><b>Upload your documents </b></span>
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
                                        <%--   <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_File_Edit" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_File_Edit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_File_Delete" runat="server" Width="15px" Height="15px" ImageUrl="~/images/delete.png" OnClientClick="return confirm('Are you sure delete?');" OnClick="lnk_File_Delete_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>
                        <hr />
                        <li>
                            <span><b>Profile Summary </b></span>
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label8" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                        <li style="width: 100% !important">
                            <br />
                            <span>Description (Describe in 500 Char about your profile )</span>&nbsp;&nbsp;<br />
                            <br />
                            <asp:TextBox Enabled="false" AutoComplete="off" ID="txtJobDescription" runat="server" Rows="20" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                        </li>
                        <hr />
                        <li>
                            <br />
                            <span>Select Status</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Aprover_Status" AutoPostBack="false" OnSelectedIndexChanged="ddl_IsAnyProject_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Select Status">Please Select Status</asp:ListItem>
                                <asp:ListItem Value="Approved" Text="Approved">Approved</asp:ListItem>
                                <asp:ListItem Value="Correction" Text="Correction">Correction</asp:ListItem>
                            </asp:DropDownList>

                        </li>
                        <li>
                            <br />
                            <span>Reamrk</span>&nbsp;&nbsp;<span style="color: red">*</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_ApproverRemark" MaxLength="250" runat="server"></asp:TextBox>

                        </li>
                        <li style="margin-top: 10px;">
                            <asp:LinkButton ID="lnk_Status_Submit" Visible="true" runat="server" Text="Submit" ToolTip="Submit" CssClass="Savebtnsve" OnClientClick="return SaveFinalProfileClick_2();" OnClick="lnk_Status_Submit_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lnk_FinalSubmit" Visible="true" runat="server" Text="Back" ToolTip="Back" CssClass="Savebtnsve" PostBackUrl="~/procs/EmployeeCVReviewInbox.aspx"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label1" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label></li>
                        <hr />
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
    <asp:HiddenField ID="hdnA_Emp_Code" runat="server" />
    <asp:HiddenField ID="hdnA_Emp_Name" runat="server" />
    <asp:HiddenField ID="hdnA_Emp_Email" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            //$("#MainContent_ddl_Relation").select2();
            //$("#MainContent_ddl_Qualification").select2();
            //$("#MainContent_ddl_Board").select2();
            //$("#MainContent_ddl_Degree").select2();
            //$("#MainContent_ddl_CD_Certification").select2();
            //$("#MainContent_ddl_CD_Module").select2();
            //$("#MainContent_ddl_PD_ProjectType").select2();
            //$("#MainContent_lst_PD_IndustryType").select2();
            //$("#MainContent_ddl_PD_Role").select2();
            //$("#MainContent_ddl_PD_Module").select2();
            //$("#MainContent_lst_DD_IndustryType").select2();
            //$("#MainContent_ddl_DD_Domain").select2();
            //$("#MainContent_ddl_DD_Role").select2();
            //$("#MainContent_ddl_Stream").select2();
            //$("#MainContent_ddl_PD_OrgType").select2();
            //$("#MainContent_ddl_PD_OrgName").select2();
            //$("#MainContent_ddl_DD_OrgType").select2();
            //$("#MainContent_ddl_DD_OrgName").select2();
            $("#MainContent_ddl_IsPassport").select2();
            $("#MainContent_ddl_IsAnyCertificateDone").select2();
            $("#MainContent_ddl_Aprover_Status").select2();
            $("#MainContent_ddl_IsDomain").select2();
            $("#MainContent_ddl_IsAnyProject").select2();
            $("#MainContent_txtJobDescription").htmlarea();
            document.getElementById('<%=txtJobDescription.ClientID%>').disabled = false;
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
            //  window.open("http://localhost/APIAARP/api/ExcelDownload/Download?path1=" + localFilePath + "" + FileName);
        }
        function SaveFinalProfileClick_2() {
            try {
                var msg = "Do you want to submit?";
                var retunboolean = true;
                var ele = document.getElementById('<%=lnk_Status_Submit.ClientID%>');

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
    </script>
</asp:Content>
