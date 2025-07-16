<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="productenquiry.aspx.cs" Inherits="productenquiry" %>

<%--<%@ Register Src="~/Themes/creative1.0/LayoutControls/basicbreadcum.ascx" TagName="basicbreadcrumb"
TagPrefix="ucbasicbreadcrumb" %>--%>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/prodenquiry.ascx" TagName="prodenquiry"
    TagPrefix="ucprodenquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">


<%--<ucbasicbreadcrumb:basicbreadcrumb ID="basicbreadcrumb" runat="server" />--%>

<ucprodenquiry:prodenquiry ID="prodenquiry1" runat="server" />

</asp:Content>

