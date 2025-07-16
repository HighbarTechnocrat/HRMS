<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="groupspost.aspx.cs" Inherits="follower" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/zommergrp.ascx" TagName="uxzommer"
    TagPrefix="profilehead" %>
<%@ Register Src="~/Themes/creative1.0/LayoutControls/profile/groupspost.ascx" TagName="uxgroupspost"
    TagPrefix="uxgrp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css"  />
     <link href="<%=ReturnUrl("css") %>includes/groupwall.css" rel="stylesheet" type="text/css"  />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <uxgrp:uxgroupspost ID="grppst" runat="server" />
</asp:Content>

