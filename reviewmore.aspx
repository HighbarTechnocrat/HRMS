<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="reviewmore.aspx.cs" Inherits="reviewmore" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/pdnew/reviewmore.ascx" TagName="uxreviewmore"
    TagPrefix="ucuxreviewmore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="<%=ReturnUrl("css") %>comments/more-comments.css" rel="stylesheet" type="text/css"  />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <ucuxreviewmore:uxreviewmore ID="menu1" runat="server" />
</asp:Content>

