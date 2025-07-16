<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="KRA_TeamStatusReport.aspx.cs" Inherits="KRA_TeamStatusReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leave_css.css" type="text/css" media="all" />
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/leaveteamrpt.css" type="text/css" media="all" />
	 <link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/Report.css" type="text/css" media="all" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div class="userposts">
        <span>
            <asp:Label ID="lblheading" runat="server" Text="Team Status Report"></asp:Label>
        </span>
    </div>


    <div class="leavegrid">
        <a href="KRA_Index.aspx" class="aaa">KRA Home</a>
    </div>


    <div class="leavegrid">
    </div>
    <div style="width: 100%; overflow: auto; align-content: flex-start">
        <div class="editprofileform">
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Visible="false" Style="margin-left: 135px"></asp:Label>
        </div>
        <table style="width: 80%; display: none">
            <tr>
                <td style="width: 35%;">
                    <span>Select Leave Type</span>
                    <br />
                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="dlleavereport" TabIndex="0"></asp:DropDownList>
                </td>
                <td style="width: 30%;">
                    <span>Select Year</span>
                    <br />
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="dlleavereport" TabIndex="0"></asp:DropDownList>
                </td>
                <td style="width: 35%;">
                    <span>&nbsp;</span>
                    <asp:LinkButton ID="btnSave" runat="server" Text="View Report" ToolTip="Leave Report" CssClass="leaverpt">Generate Report</asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <br />



    <div style="width: 100%; overflow: auto">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="300px"
            Width="80%" ShowBackButton="False" SizeToReportContent="True"
            ShowCredentialPrompts="False" ShowDocumentMapButton="False" ShowExportControls="true" 
            ShowPageNavigationControls="true" ShowRefreshButton="False" PageCountMode="Actual"  ShowFindControls="false">
        </rsweb:ReportViewer>
    </div>
    <asp:HiddenField ID="hdnloginempcode" runat="server" />

</asp:Content>

