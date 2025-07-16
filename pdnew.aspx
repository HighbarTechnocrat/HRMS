<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="pdnew.aspx.cs" Inherits="pdnew" %>
<%@ Register Src="~/Components/Common/pdnewglobal.ascx" TagName="pdnewglobal" TagPrefix="ucpdnewglobal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
        <link href="<%=ReturnUrl("css") %>movie-detail/movie-detail.css" rel="stylesheet" type="text/css"  />
    <link href="<%=ReturnUrl("css") %>navigation/mobile-navi-dd.css" rel="stylesheet" type="text/css"  />
    <link href="<%=ReturnUrl("css") %>scrollbar/scrollbar.css" rel="stylesheet" type="text/css"  />
        <link href="<%=ReturnUrl("css") %>comments/comments.css" rel="stylesheet" type="text/css"  />
    <link href="<%=ReturnUrl("css") %>animation/animate.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>carouselnew/carousel.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>RatingPopup/style.css" rel="stylesheet" type="text/css"  /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
         <ucpdnewglobal:pdnewglobal ID="pdnewglobal" runat="server" />
</asp:Content>

