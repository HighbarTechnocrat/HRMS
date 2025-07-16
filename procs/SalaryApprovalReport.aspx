<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="SalaryApprovalReport.aspx.cs" Inherits="procs_SalaryApprovalReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Mobile_RemRequest_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hbtmodalpopupCss.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/ClaimMobile_css.css" type="text/css" media="all" />

    <style>
        form label, #buddypress .standard-form label, #buddypress .standard-form span.label {
            font-weight: 300 !important;
            text-transform: none !important;
        }
        /*.myaccountpagesheading {
            text-align: center !important;
            text-transform: uppercase !important;
        }*/


        /*.Dropdown {
            border-bottom: 2px solid #cccccc;*/
        /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
        /*background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            cursor: default;
        }

        .grayDropdown {
            border-bottom: 2px solid #cccccc;*/
        /*background: url("../themes/creative1.0/images/arrow.png") no-repeat right top;*/
        /*background: url('../themes/creative1.0/images/user_check_dropdown1.png') no-repeat right center;
            background-color: #ebebe4;
        }

        .grayDropdownTxt {
            background-color: #ebebe4;
        }

        .taskparentclass2 {
            width: 29.5%;
            height: 112px;
        }

        .taskparentclasskkk {
            width: 29.5%;
            height: 72px;*/
        /*overflow:initial;*/
        /*}

        #MainContent_ListBox1, #MainContent_ListBox2 {
            padding: 0 0 0% 0 !important;*/
        /*overflow: unset;*/
        /*}

        #MainContent_lstApprover, #MainContent_lstIntermediate {
            padding: 0 0 33px 0 !important;*/

        /*overflow: unset;*/
        /*}*/

        #cssTable td {
            text-align: center;
            vertical-align: middle;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <link href="../js/dist/select2.min.css" rel="stylesheet" />
    <script src="../js/dist/select2.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".DropdownListSearch").select2();
        });
        function jsFunction_ddl_MonthYear() {
            var ddl_MonthYear = document.getElementById('MainContent_ddl_MonthYear');
            var a = ddl_MonthYear.options[ddl_MonthYear.selectedIndex].value;
            var b = ddl_MonthYear.options[ddl_MonthYear.selectedIndex].text;

            var textboxMain = document.getElementById('MainContent_ddl_MonthYearId');
            var txtMain = document.getElementById('MainContent_ddl_MonthYeartexts');
            textboxMain.value = a;
            txtMain.value = b;
        }
        //function jsFunction_ddl_RM() {
        //    var ddl_RM = document.getElementById('MainContent_ddl_RM');
        //    var a = ddl_RM.options[ddl_RM.selectedIndex].value;
        //    var b = ddl_RM.options[ddl_RM.selectedIndex].text;

        //    var textboxMain = document.getElementById('MainContent_ddl_RMId');
        //    var txtMain = document.getElementById('MainContent_ddl_RMtexts');
        //    textboxMain.value = a;
        //    txtMain.value = b;
        //}
        function jsFunction_dlRMList() {
            var dlRMList = document.getElementById('MainContent_dlRMList');
            var a = dlRMList.options[dlRMList.selectedIndex].value;
            var b = dlRMList.options[dlRMList.selectedIndex].text;

            var textboxMain = document.getElementById('MainContent_dlRMList_ID');
            var txtMain = document.getElementById('MainContent_dlRMList_Text');
            textboxMain.value = a;
            txtMain.value = b;
        }
        
    </script>

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Salary Approval Report"></asp:Label>
                        </span>
                    </div>
                    <div>
                        <span>
                            <a href="ReportsMenu.aspx" class="aaaa">Home</a>
                        </span>
                    </div>
                    <%--                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </span>
                    </div>--%>
                    <div class="leavegridMain">
                        <table id="TABLE123" border="0" cellpadding="5px" cellspacing="5px" width="90%">
                            <tr>
                                <td colspan="2" align="left">
                                    <%--<font color="blue">Note: Fields marked with  </font>(<font color="red">*</font>)<font color="blue"> are necessary </font>--%>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label Font-Bold="true" ForeColor="Red" ID="msgsave" runat="server" Visible="false"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 223px" align="right">Month and Year:</td>
                                <td style="width: 350px">
                                    <asp:DropDownList runat="server" ID="ddl_MonthYear" CssClass="DropdownListSearch" onchange="jsFunction_ddl_MonthYear();">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ErrorMessage="Select Month and Year" InitialValue="0" Display="Dynamic" ForeColor="Red" ValidationGroup="AddData" ControlToValidate="ddl_MonthYear" runat="server" />
                                    <asp:TextBox ID="ddl_MonthYearId" runat="server" type="hidden"></asp:TextBox>
                                    <asp:TextBox ID="ddl_MonthYeartexts" runat="server" type="hidden"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 223px" align="right">RM:</td>
                                <td style="width: 450px">
                                    <%--                                    <asp:DropDownList ID="ddl_RM" runat="server" CssClass="DropdownListSearch" onchange="jsFunction_ddl_RM();"></asp:DropDownList>
                                    <asp:TextBox ID="ddl_RMId" runat="server" type="hidden"></asp:TextBox>
                                    <asp:TextBox ID="ddl_RMtexts" runat="server" type="hidden"></asp:TextBox>--%>
                                    <asp:DropDownList runat="server" ID="dlRMList" CssClass="DropdownListSearch" onchange="jsFunction_dlRMList();">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="dlRMList_ID" runat="server" type="hidden"></asp:TextBox>
                                    <asp:TextBox ID="dlRMList_Text" runat="server" type="hidden"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 223px" align="right">Salary Approval Status:</td>
                                <td style="width: 350px">
                                    <asp:DropDownList runat="server" ID="ddl_SalAppStatus">
                                        <asp:ListItem Text="Select Approval Status" Value="2" />
                                        <asp:ListItem Text="Approved" Value="1" />
                                        <asp:ListItem Text="Not Approved" Value="0" />
                                    </asp:DropDownList>
                                    <asp:TextBox ID="ddl_SalAppStatusId" runat="server" type="hidden"></asp:TextBox>
                                    <asp:TextBox ID="ddl_SalAppStatustexts" runat="server" type="hidden"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td class="input-medium" style="width: 223px"></td>
                                <td align="left">
                                    <div class="mobile_Savebtndiv">
                                        <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Search" ToolTip="Search" CausesValidation="true" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" ValidationGroup="AddData">Generate Report</asp:LinkButton>
                                        <asp:LinkButton ID="mobile_cancel" runat="server" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click">Clear</asp:LinkButton>
                                    </div>
                                </td>
                            </tr>
                            <%--                            <tr>
                                <td colspan="4" align="right">

                                    <asp:LinkButton ID="btnExport" runat="server" Text="Export To Excel" Visible="false" CssClass="aaaa" OnClick="btnExport_Click" />

                                </td>
                            </tr>--%>
                            <tr>
                                <td colspan="4">
                                    <div style="width: 100%; overflow: auto">
                                        <rsweb:ReportViewer ID="rptvwrSalaryStatus" runat="server" Height="700px"
                                            Width="100%" ShowBackButton="False" SizeToReportContent="false"
                                            ShowCredentialPrompts="False" ShowDocumentMapButton="False"
                                            ShowPageNavigationControls="true" ShowFindControls="false" ShowExportControls="true" ShowRefreshButton="False" PageCountMode="Actual">
                                        </rsweb:ReportViewer>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="productsearch-gridview" align="center" visible="false">

                                    <%--<asp:GridView ID="dgSalApproved" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                        AutoGenerateColumns="False" Width="100%"
                                        EmptyDataText="No records Found." OnRowCommand="dgSalApproved_RowCommand" DataKeyNames="RM_EMPCODE" >
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" />
                                        <PagerStyle ForeColor="#000066" HorizontalAlign="left" BackColor="White" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                        <Columns>
                                            <asp:BoundField DataField="RM_EMPCODE" HeaderText="RM EMP Code">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RMName" HeaderText="RM Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DesginationName" HeaderText="Desgination">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SalaryStatus" HeaderText="Salary Status">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="MonthYear" HeaderText="MonthYear">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CreatedOn" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkViewFile" CommandName="ViewData" CommandArgument='<%# Eval("MonthAndYear")%>' Width="80%" runat="server" Text="View" CssClass="linkcss">
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblempCode" Text='<%# Eval("RM_EMPCODE") %>' Visible="false" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>--%>
                                    <br />
                                </td>
                            </tr>
                        </table>

                        <br />
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

