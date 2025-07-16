<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bottombanner.ascx.cs" Inherits="themes_creative1_LayoutControls_bottombanner" %>
<asp:Panel ID="pnlbanner" runat="server" Visible="false">
<div class="bottombanner-main">
<div class="bottombannerimage">
<asp:Repeater ID="rptbottombanner" runat="server">
<ItemTemplate>
<img src='<%=ConfigurationManager.AppSettings["adminsitepath"]%>images/banner/<%# Eval("imagename") %>' alt='<%# Eval("linkname") %>' title='<%# Eval("linkname") %>' />
</ItemTemplate>
</asp:Repeater>
</div>
</div>
</asp:Panel>