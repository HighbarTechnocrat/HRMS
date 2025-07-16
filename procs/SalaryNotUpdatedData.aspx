<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="SalaryNotUpdatedData.aspx.cs" Inherits="procs_SalaryNotUpdatedData" %>

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
                            <asp:Label ID="lblheading" runat="server" Text="Update Salary Status"></asp:Label>
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
                    <div class="leavegridMain">

                        <asp:GridView ID="dgSalPending" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                            AutoGenerateColumns="False" Width="40%"
                            EmptyDataText="No records Found." OnRowCommand="dgSalPending_RowCommand">
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
                                <%--<asp:BoundField DataField="Emp_Code" HeaderText="RM Code">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Emp_Name" HeaderText="RM Name">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DesginationName" HeaderText="Desgination">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="MonthYear" HeaderText="MonthYear">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SalaryStatus" HeaderText="Salary Update Status">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Width="20%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
<%--                                        <asp:LinkButton ID="lnkViewFile" CommandName="ViewData" ImageUrl="~/Images/edit.png" CommandArgument='<%# Eval("ID")%>' Width="60%" runat="server" CssClass="linkcss">
                                        </asp:LinkButton>--%>
                                        <asp:ImageButton ID="lnkViewFile" CommandName="ViewData" CommandArgument='<%# Eval("ID")%>' runat="server" Width="15px" Height="15px" ImageUrl="~/Images/edit.png"  />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle Width="20%"/>        
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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

