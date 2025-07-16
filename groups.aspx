<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="groups.aspx.cs" EnableViewState="false" Inherits="follower" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/zommer.ascx" TagName="uxzommer"
    TagPrefix="profilehead" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/groups.ascx" TagName="uxgroups"
    TagPrefix="uxgrp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css"  />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<%--    <profilehead:uxzommer ID="imgprofile" runat="server" />--%>
    <uxgrp:uxgroups ID="groups" runat="server" />
</asp:Content>

