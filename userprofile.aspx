<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="userprofile.aspx.cs" Inherits="userprofile" EnableViewState="true" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/zommer.ascx" TagName="uxzommer"
    TagPrefix="profilehead" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/ratingreviews.ascx" TagName="uxrating"
    TagPrefix="userrating" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>carouselnew/carousel.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--    <profilehead:uxzommer ID="imgprofile" runat="server" />--%>
    <userrating:uxrating ID="userreviews" runat="server" />
</asp:Content>

