<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="MIS_OnboardingDynamic.aspx.cs"
    Inherits="procs_MIS_OnboardingDynamic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <%-- <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />--%>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
    <style type="text/css">
        #MainContent_Lnk_Export {
            background: #3D1956;
            color: #febf39 !important;
            padding: 8px 14px 8px 14px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="userposts">
        <span>
            <asp:Label ID="lblheading" runat="server" Text="Onboarding Report"></asp:Label>
        </span>
    </div>

    <div class="leavegrid">
        <a href="https://ess.highbartech.com/hrms/procs/ReportsMenu.aspx" class="aaa">Reports</a>
    </div>

    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label runat="server" ID="lblmessage" Visible="True" Style="color: red; font-size: 14px; font-weight: 400; text-align: center;"></asp:Label>
        </div>
    </div>
    <div class="edit-contact">
        <ul>
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">From Date: <span style="color: red">*</span></span><br />
                <asp:TextBox ID="txtFromdate" CssClass="txtcls" AutoComplete="off" runat="server" AutoPostBack="false" MaxLength="10" AutoCompleteType="Disabled"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtFromdate" runat="server">
                </ajaxToolkit:CalendarExtender>
            </li>
            <li>
                <span style="font-weight: normal; font-size: 14px; color: hsl(0, 0%, 0%);">To Date:</span><br />
                <asp:TextBox ID="txtToDate" CssClass="txtcls" AutoComplete="off" runat="server" AutoPostBack="false"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtToDate"
                    runat="server">
                </ajaxToolkit:CalendarExtender>
            </li>
        </ul>
        <ul>
            <li>
                <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Generate Report" CssClass="leaverpt" OnClick="btnSave_Click">Generate Report</asp:LinkButton>
            </li>
            <li>
                        <asp:LinkButton ID="Lnk_Export" runat="server" Visible="false" OnClick="Lnk_Export_Click" Text="Export to Excel" ToolTip="Export to Excel"
            CssClass="Savebtnsve">Export to Excel</asp:LinkButton>
            </li>
        </ul>
    </div>

    <br /><br />
    

    <div style="width: 100%; overflow: auto">
        <asp:Label ID="lblGVOnboardingData_period" runat="server"></asp:Label>
        <asp:GridView ID="GVOnboardingData" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
            CellPadding="3" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="GVOnboatdingData_RowDataBound">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
            <PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>

     <br />
    <br />
    <br />

    <div style="width: 100%; overflow: auto">
        <asp:Label ID="lblGVOnboardingData2_period" runat="server"></asp:Label>
        <asp:GridView ID="GVOnboardingData2" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
            CellPadding="3" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="GVOnboatdingData_RowDataBound">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
            <PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>

     <br />
    <br />
    <br />

    <div style="width: 100%; overflow: auto">
        <asp:Label ID="lblGVOnboardingData3_period" runat="server"></asp:Label>
        <asp:GridView ID="GVOnboardingData3" runat="server" BackColor="White" BorderColor="Navy" BorderStyle="solid" BorderWidth="1px"
            CellPadding="3" Width="100%" EditRowStyle-Wrap="false" OnRowDataBound="GVOnboatdingData_RowDataBound">
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#C7D3D4" Font-Bold="True" ForeColor="#3D1956" HorizontalAlign="Center" />
            <PagerStyle CssClass="gridpager" HorizontalAlign="Right" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="hdnloginempcode" runat="server" />
    <asp:HiddenField ID="hflEmpCode" runat="server" />

    <asp:HiddenField ID="hflEmpDesignation" runat="server" />


    <br />
    <br />
    <br />


</asp:Content>

