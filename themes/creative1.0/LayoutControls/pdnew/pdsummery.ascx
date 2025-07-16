<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pdsummery.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_pdnew_pdsummery" %>
<div class="productsummery" id="productsummery">
    <asp:Panel ID="uxproductsummary" runat="server" CssClass="Default">
    </asp:Panel>
    <asp:Panel ID="uxpdattributes" runat="server" CssClass="Default">
    </asp:Panel>
    <%-- SONY changed to VISIBLE FALSE --%>
    <asp:Panel ID="uxpfproduct" runat="server" CssClass="item-counter" Visible="false">
    </asp:Panel>
    <asp:Panel ID="uxpshop" runat="server" CssClass="Default">
    </asp:Panel>  
   </div>

<asp:Panel ID="uxreview" runat="server" CssClass="Default">
</asp:Panel>
<%-- SONY changed to VISIBLE TRUE--%>
<asp:Panel ID="uxodrelateprod" runat="server" CssClass="Default" Visible="true">
</asp:Panel>
<asp:Panel ID="uxmoviebanner" runat="server" CssClass="Default">
</asp:Panel>

