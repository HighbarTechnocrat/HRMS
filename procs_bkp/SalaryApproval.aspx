<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="SalaryApproval.aspx.cs" Inherits="procs_SalaryApproval" %>

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

    <div class="commpagesdiv">
        <div class="commonpages">
            <div class="wishlistpagediv">
                <div class="wishlistpage">
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblheading" runat="server" Text="Salary Status Update"></asp:Label>
                        </span>
                    </div>
                    <div>
                        <span>
                            <a href="SalaryApprovalHome.aspx" class="aaaa" id="aSalApp" runat="server">Home</a>
                        </span>
                    </div>
                    <br />
                    <br />
                    <div class="userposts">
                        <span>
                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                        </span>
                    </div>
                    <div id="divPara" runat="server">
                        <span style="font-size: 14px;">Please check the below list of employees, let us know if there is any abscond/Separation case. HR team will examine their case and make a decision to hold salary if required and put remark for required cases. Please update such cases details latest by tomorrow/21st EOD, also note that release of salary for absconded employees, due to lack of communication from Managers, will lead to financial loss for the organization
                        </span>
                        <ul>
                            <li><b>• Active</b> - Working currently and eligible for salary.</li>
                            <li><b>• Hold</b> - Need to hold salary because of certain reason or already left.</li>
                            <li><b>• Remark</b> is editable for all cases (Active and Hold) please mention the reason in remark for hold cases.</li>
                        </ul>
                    </div>

                    <div id="divexport" runat="server" visible="false">
                        <asp:LinkButton ID="btnExport" runat="server" Text="Export To Excel" CssClass="aaaa" OnClick="btnExport_Click" />
                    </div>
                    <br />
                    <br />
                    <br />
                    <div class="leavegridMain">
                        <asp:GridView ID="dgSalApp" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" Width="100%"
                            EmptyDataText="No records Found.">
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
                                <asp:TemplateField HeaderText="Employee Code" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_Code" runat="server" Text='<%# Eval("Emp_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Desgination" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesgination" runat="server" Text='<%# Eval("DesginationName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resignation Date" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResignationDate" runat="server" Text='<%# Eval("ResigDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Status For Salary" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSalStatus" runat="server">
                                            <asp:ListItem Text="Active" Value="1" Selected="True" />
                                            <asp:ListItem Text="Hold" Value="0" />
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" MaxLength="500"></asp:TextBox>
                                        <asp:Label ID="lblGDmsg" ForeColor="Red" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:GridView ID="dgSalApproved" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" Width="100%"
                            EmptyDataText="No records Found.">
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
                                <asp:BoundField DataField="Emp_Code" HeaderText="Employee Code">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DesginationName" HeaderText="Desgination">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ResigDate" HeaderText="Resignation Date">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MonthYear" HeaderText="MonthYear">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SalaryStatus" HeaderText="Current Status For Salary">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>
                        <div class="mobile_Savebtndiv">
                            <asp:LinkButton ID="mobile_btnSave" runat="server" Text="Submit" ToolTip="Save" CausesValidation="true" CssClass="Savebtnsve" OnClick="mobile_btnSave_Click" Style="float: right; padding-right: 20px;">Submit</asp:LinkButton>
                            <%--<asp:LinkButton ID="mobile_cancel" runat="server" Visible="false" Text="Cancel" ToolTip="Cancel" CssClass="Savebtnsve" OnClick="mobile_cancel_Click" style="float:right;padding-left:10px;">Cancel</asp:LinkButton>--%>
                            <br />
                        </div>
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

