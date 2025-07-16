<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="following.aspx.cs" Inherits="following" EnableViewState="false" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/zommer.ascx" TagName="uxzommer"
    TagPrefix="profilehead" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/following.ascx" TagName="uxfollowing"
    TagPrefix="userfollowing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css"  />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <userfollowing:uxfollowing ID="ucfollowing" runat="server" />
</asp:Content>

