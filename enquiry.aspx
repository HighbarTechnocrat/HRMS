<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="enquiry.aspx.cs" Inherits="followers" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/zommer.ascx" TagName="uxzommer"
    TagPrefix="profilehead" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/profile/enquiry.ascx" TagName="viewpro" TagPrefix="ucheader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <ucheader:viewpro ID="viewprofile" runat="server" />
</asp:Content>

