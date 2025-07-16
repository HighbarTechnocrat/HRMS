<%@ Control Language="C#" AutoEventWireup="true" CodeFile="promobannerm.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_promobannerm" %>

<div id="promodd" class="promodropdownm" tabindex="1" runat="server">
Promos
<ul class="dropdown">
<asp:Repeater ID="rptpromo" runat="server">
<ItemTemplate>
<li>
<a href='<%# DataBinder.Eval(Container.DataItem,"url")%>'  title='<%# Eval("altname") %>'>
<img src='<%=ReturnUrl("sitepathadmin") %>images/banner/<%# Eval("imagename") %>' width="140" height="35" alt="" />
</a>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>

