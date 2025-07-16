<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="favorites.aspx.cs" Inherits="favorites" EnableViewState="true" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/zommer.ascx" TagName="uxzommer"
    TagPrefix="profilehead" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/favorites.ascx" TagName="uxfav"
    TagPrefix="favorite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" /> 
        <link href="<%=ReturnUrl("css") %>highbar/hrms.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="~/themes/creative1.0/js/carouselnew/carousel.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--        <profilehead:uxzommer ID="imgprofile" runat="server" />--%>
        <favorite:uxfav ID="userfav" runat="server" />
       
</asp:Content>

