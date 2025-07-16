<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="LocalIPAddress.aspx.cs" Inherits="procs_LocalIPAddress" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register TagName="calender" TagPrefix="ucical" Src="~/themes/creative1.0/LayoutControls/leaveCalender.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

   <%-- <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>--%>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label runat="server"  ID="LBlIPv4"></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
</asp:Content>


