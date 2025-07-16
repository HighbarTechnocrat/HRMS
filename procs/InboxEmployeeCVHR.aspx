<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeFile="InboxEmployeeCVHR.aspx.cs" Inherits="InboxEmployeeCVHR" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>togglebuttoncss/on-off-switch.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>singleautocomplete/jquery.autocomplete.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%--    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <%--   <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/travel_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/manageleave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style>
        .myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }
        /*.select2-search__field{
            width:0.0em !important;
        }*/
        .aspNetDisabled {
            /*background: #dae1ed;*/
            background: #ebebe4;
        }

        #MainContent_lstApprover {
            overflow: hidden !important;
        }

        .edit-contact input {
            padding-left: 0px !important;
        }

        #MainContent_lnk_ed_Search,
        #MainContent_lnk_ed_Clear,
        #MainContent_lnk_LinkButton1,
        #MainContent_lnk_LinkButton2,
        #MainContent_lnk_LinkButton3,
        #MainContent_lnk_ed_Download {
            background: #3D1956;
            color: #febf39 !important;
            padding: 0.5% 1.4%;
            margin: 0% 0% 0 0;
        }

        .noresize {
            resize: none;
        }

        /*.select2-container .select2-selection--multiple {
            box-sizing: border-box;
            cursor: pointer;
            display: block;
            min-height: 32px;
            user-select: none;
            -webkit-user-select: none;
            min-height: 10px;
        }*/
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--   <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>--%>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/dist/jquery-3.2.1.min.js"></script>
    <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>

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
                            <span><b>Find Employee Details</b></span><br />
                            <br />
                        </li>
                        <li>
                            <asp:Label runat="server" ID="Label2" Visible="True" Style="color: red; font-size: 16px; font-weight: 500; text-align: center;"></asp:Label>
                        </li>
                         <li style="width: 100%; text-align: center;">
                            <br />
                            <br />
                            <asp:LinkButton ID="lnk_LinkButton1" Visible="true" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="lnk_ed_Search_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_LinkButton2" Visible="true" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="lnk_ed_Clear_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_LinkButton3" Visible="true" runat="server" Text="Download Excel" ToolTip="Download Excel" CssClass="Savebtnsve" OnClick="lnk_ed_Download_Click"></asp:LinkButton>
                            <br />
                        </li>
                        <li>
                            <br />
                            <%-- <span>Wild Search</span>--%>
                            <br />
                            <asp:RadioButton AutoPostBack="true" ID="rbWildSearch" runat="server" GroupName="Search"  Text="Wild Search" OnCheckedChanged="rbWildSearch_CheckedChanged"></asp:RadioButton>

                        </li>
                        <li>
                            <span>Search Text</span>
                            <asp:TextBox AutoComplete="off" ID="txt_Wild_Search" Enabled="false" MaxLength="100" runat="server"></asp:TextBox>

                        </li>
                        <hr />
                        <li>
                          
                            <%-- <span>Normal Search</span>--%>
                            <br />
                            <asp:RadioButton AutoPostBack="true" ID="rbNormalSearch" Checked="true" runat="server" GroupName="Search" Text="Normal Search" OnCheckedChanged="rbWildSearch_CheckedChanged"></asp:RadioButton>
                        </li>
                        <li></li>
                        <li></li>

                        <li runat="server" id="show1">
                            <span>Department</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Department" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>

                        <li runat="server" id="show2">
                            <br />
                            <span>Band</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Band" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show3">
                            <span>Designation</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Designation" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>

                        <li runat="server" id="show4">
                            <br />
                            <span>Project</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Project" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show5">
                            <br />
                            <span>Employee Name</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Employee" AutoPostBack="false">
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="show6">
                            <br />
                            <span>Primary Module</span>
                            <br />
                            <asp:ListBox runat="server" ID="ddl_Module" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="showCHK1">
                            <br />
                            <asp:CheckBox AutoComplete="off" AutoPostBack="false" ID="chk_Completed_Certification" Text="Is Completed Certification?"  runat="server"></asp:CheckBox>
                            <br />
                        </li>
                        <li runat="server" id="show7"></li>
                        <hr  runat="server" id="show8"/>

                        <li runat="server" id="show9">
                            <br />
                            <span>Type of Qualification</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstQualification" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show10">
                            <br />
                            <span>University/Institute</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstUniversity" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show11">
                            <br />
                            <span>Degree</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstDegree" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>


                        <li runat="server" visible="false" id="show12">
                            <br />
                            <span>Status</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Status" AutoPostBack="false">
                                <asp:ListItem Value="All" Selected="True" Enabled="true">Select Status</asp:ListItem>
                                <asp:ListItem Value="All">All</asp:ListItem>
                                <asp:ListItem Value="Complete">Complete</asp:ListItem>
                                <asp:ListItem Value="InComplete">InComplete</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="show13">
                            <br />
                            <span>Certification Name</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstCertification" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show13_1">
                            <br />
                            <span>Certification Code</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstCertification_Code" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show14">
                            <br />
                            <span>Stream</span>
                            <br />
                            <asp:ListBox runat="server" ID="lst_Stream" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>

                        </li>
                        <li runat="server" id="show15">
                            <br />
                            <span>Industry Type</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstIndustry" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show16">
                            <br />
                            <span>Role/Designation</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstDesignation" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show17">
                            <br />
                            <span>Domain</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstDomain" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show18">
                            <br />
                            <span>Type of Organisation</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstOrganisationType" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>

                        </li>
                        <li runat="server" id="show19">
                            <br />
                            <span>Organisation Name</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstOrganisationName" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>

                        </li>
                        <li runat="server" id="show20">
                            <br />
                            <span>Region</span>
                            <br />
                            <asp:ListBox runat="server" ID="lst_Region" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>

                        </li>
                        <li runat="server" id="show21">
                            <span>Year of Passing</span>
                            <br />
                            <asp:TextBox AutoComplete="off" ID="txt_YearOfPassing" MaxLength="10" runat="server"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="TextBox1_CalendarExtender" runat="server" OnClientHidden="onCalendarHidden" OnClientDateSelectionChanged="checkProjectEndDate"
                                OnClientShown="onCalendarShown" Format="yyyy" BehaviorID="calendar1" TargetControlID="txt_YearOfPassing">
                            </ajaxToolkit:CalendarExtender>
                        </li>

                        <li runat="server" id="show22">
                            <br />
                            <span>Type of Project</span>
                            <br />
                            <asp:ListBox runat="server" ID="lstProject" CssClass="DropdownListSearch" SelectionMode="multiple"></asp:ListBox>
                        </li>
                        <li runat="server" id="show23">
                            <br />
                            <span>Select Conditions</span>
                            <br />
                            <asp:DropDownList runat="server" ID="ddl_Condition" AutoPostBack="false">
                                <asp:ListItem Value="0" Selected="True" Enabled="true">Select Condition</asp:ListItem>
                                <asp:ListItem Value="GT">Greater Than</asp:ListItem>
                                <asp:ListItem Value="LT">Less Than</asp:ListItem>
                                <asp:ListItem Value="GTE">Greater Than Or Equal To</asp:ListItem>
                                <asp:ListItem Value="LTE">Less Than Or Equal To</asp:ListItem>
                                <asp:ListItem Value="EQ">Equal To</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li runat="server" id="show24">
                            <br />
                            <span>No Of Project Worked On</span>
                            <asp:TextBox AutoComplete="off" ID="txt_ProjectNoType" Text="0" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li></li>
                        <li></li>
                        <li runat="server" id="show25">
                            <br />
                            <span>Overall Experience(years)</span>
                            <br />
                            <span>From</span>
                            <asp:TextBox AutoComplete="off" ID="txt_From" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li runat="server" id="show26">
                            <br />
                            <span>To</span>
                            <asp:TextBox AutoComplete="off" ID="txt_To" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li runat="server" id="show27"></li>
                        <li runat="server" id="show28">
                            <br />
                            <span>Total Domain Experience(years)</span>
                            <br />
                            <span>From</span>
                            <asp:TextBox AutoComplete="off" ID="txt_From_DE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li runat="server" id="show29">
                            <br />
                            <span>To</span>
                            <asp:TextBox AutoComplete="off" ID="txt_To_DE" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li runat="server" id="show30"></li>
                        <li runat="server" id="show31">
                            <br />
                            <span>Total SAP Experience(years)</span>
                            <br />
                            <span>From</span>
                            <asp:TextBox AutoComplete="off" ID="txt_From_SAP" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li runat="server" id="show32">
                            <span>To</span>
                            <asp:TextBox AutoComplete="off" ID="txt_To_SAP" MaxLength="5" runat="server"></asp:TextBox>
                        </li>
                        <li runat="server" id="show33"></li>

                        <li style="width: 100%; text-align: center;">
                            <br />
                            <br />
                            <asp:LinkButton ID="lnk_ed_Search" Visible="true" runat="server" Text="Search" ToolTip="Search" CssClass="Savebtnsve" OnClick="lnk_ed_Search_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_ed_Clear" Visible="true" runat="server" Text="Clear Search" ToolTip="Clear Search" CssClass="Savebtnsve" OnClick="lnk_ed_Clear_Click"></asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="lnk_ed_Download" Visible="true" runat="server" Text="Download Excel" ToolTip="Download Excel" CssClass="Savebtnsve" OnClick="lnk_ed_Download_Click"></asp:LinkButton>
                            <br />
                        </li>
                        <hr />
                        <li style="width: 100% !important">
                             <span Style="color: red;"><b id="spanNoRe" runat="server" visible="false" >No Of Records</b></span>
                            <br />
                            <br />
                            <br />
                            <span><b>Employee Details</b></span>
                            <br />
                            <br />
                            <div style="max-width: 100%; overflow-y: scroll;">
                                <asp:GridView ID="gv_EmployeeDetails" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" AllowPaging="true" PageSize="10" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="250%" EditRowStyle-Wrap="false"
                                    DataKeyNames="Emp_id,Emp_Code" OnPageIndexChanging="gv_EmployeeDetails_PageIndexChanging">
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
                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_ED_View" runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png" OnClick="lnk_ED_View_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WORD" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_Download" runat="server" Width="15px" Height="15px" ImageUrl="~/images/Download.png" OnClick="lnk_Download_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PDF" HeaderStyle-Width="0%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnk_Download_PDF" runat="server" Width="15px" Height="15px" ImageUrl="~/images/Download.png" OnClick="lnk_Download_PDF_Click" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0%" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Employee Code"
                                            DataField="Emp_Code"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />

                                        <asp:BoundField HeaderText="Employee Name"
                                            DataField="Emp_Name"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="15%" />
                                        <asp:BoundField HeaderText="Designation"
                                            DataField="Designation"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="13%" />
                                        <asp:BoundField HeaderText="Band"
                                            DataField="grade"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="4%" />
                                        <asp:BoundField HeaderText="SAP Experience"
                                            DataField="TotalSAPExperience"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="Total Experience"
                                            DataField="OverallWorkExperience"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="5%" />
                                        <asp:BoundField HeaderText="Highest Qualification"
                                            DataField="HighestQualification"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Project"
                                            DataField="emp_projectName"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                                        <asp:BoundField HeaderText="Primary Module"
                                            DataField="Module1"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />
                                        <%--  <asp:BoundField HeaderText="Module2"
                                            DataField="Module2"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />--%>
                                        <asp:BoundField HeaderText="Date Of Joining"
                                            DataField="DOJ"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Full Life Cycle Implementation"
                                            DataField="FLCI"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Rollout"
                                            DataField="Rollout"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Migration"
                                            DataField="Migration"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Support"
                                            DataField="Support"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Enhancements"
                                            DataField="Enhancements"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Others"
                                            DataField="Others"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="7%" />
                                        <asp:BoundField HeaderText="Criteria"
                                            DataField="Criteria"
                                            ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-BorderColor="Navy" ItemStyle-Width="10%" />



                                    </Columns>
                                </asp:GridView>
                            </div>
                            <br />
                        </li>

                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="mobile_Savebtndiv">
    </div>

    <br />
    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" SetFocusOnError="True" ErrorMessage="Please enter Employee code" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="regName" runat="server" CssClass="formerror" ControlToValidate="txtEmpCode" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter a valid name" ValidationGroup="validate" SetFocusOnError="true" />--%>

    <asp:HiddenField ID="hdnvouno" runat="server" />
    <asp:HiddenField ID="hdnIsMarried" runat="server" />
    <asp:HiddenField ID="hdnFamilyDetailID" runat="server" />
    <asp:HiddenField ID="hdnEduactonDetailID" runat="server" />
    <asp:HiddenField ID="hdnCertificationDetailID" runat="server" />
    <asp:HiddenField ID="hdnProjectDetailID" runat="server" />
    <asp:HiddenField ID="hdnDomainDetailID" runat="server" />
    <asp:HiddenField ID="hdnYesNo" runat="server" />
    <asp:HiddenField ID="hdnempcode" runat="server" />


    <asp:HiddenField ID="FilePath" runat="server" />

    <%--<script src="../js/dist/jquery-3.2.1.min.js"></script>--%>
    <script src="../js/dist/select2.min.js"></script>
    <link href="../js/dist/select2.min.css" rel="stylesheet" />

    <script type="text/javascript">      
        $(document).ready(function () {
            $("#MainContent_ddl_Department").select2();
            $("#MainContent_ddl_Designation").select2();
            $("#MainContent_ddl_Project").select2();
            $("#MainContent_ddl_Module").select2();
            $("#MainContent_ddl_Status").select2();
            $("#MainContent_ddl_Employee").select2();
            $("#MainContent_lstQualification").select2();
            $("#MainContent_lstDegree").select2();
            $("#MainContent_lstCertification").select2();
            $("#MainContent_lstProject").select2();
            $("#MainContent_lstIndustry").select2();
            $("#MainContent_lstDesignation").select2();
            $("#MainContent_lstDomain").select2();
            $("#MainContent_ddl_Band").select2();
            $("#MainContent_lstUniversity").select2();
            $("#MainContent_lstOrganisationType").select2();
            $("#MainContent_lstOrganisationName").select2();
            $("#MainContent_lst_Region").select2();
            $("#MainContent_lst_Stream").select2();
            $("#MainContent_ddl_Condition").select2();
            $("#MainContent_lstCertification_Code").select2();
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
        //
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
            var expFrom = document.getElementById("<%=txt_From.ClientID%>").value;
            var expTo = document.getElementById("<%=txt_To.ClientID%>").value;
            if (expFrom != "") {
                expFrom = parseFloat(expFrom);
                expTo = parseFloat(expTo);
                if (expFrom >= expTo) {
                    alert("False");
                }
            }
            else {
                alert("Please enter first overall experieance");
                document.getElementById("<%=txt_To.ClientID%>").value = "";
                return;
            }
        }
    </script>
</asp:Content>
