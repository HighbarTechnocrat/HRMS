<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="comments.aspx.cs" Inherits="comments" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/reviewbanner.ascx" TagName="uxcomment"
    TagPrefix="review" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="<%=ReturnUrl("css") %>writecomments/writecomments.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>carouselnew/carousel.css" rel="stylesheet" type="text/css" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <review:uxcomment ID="reviewcomments" runat="server" />
</asp:Content>

