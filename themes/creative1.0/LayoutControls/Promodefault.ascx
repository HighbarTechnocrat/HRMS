<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Promodefault.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_Promodefault" %>

<div class="topheadpromo" id="topheadpromo" runat="server" ><%--class="promo-main"   class="promo-outline-out"  --%>
  <div class="topheadban1">
    <ul class="promo-container" style="left:0">
      <asp:Repeater ID="rptpromo" runat="server">
        <ItemTemplate>
          <li> <a href="<%# Eval("url")%>">"  title='<%# Eval("altname") %>' target='<%# Eval("target") %>' id="promolink" runat="server" > <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/banner/<%# Eval("imagename") %>' alt="" /><span> <%# Eval("altname") %></span> </a>
             
          </li>
        </ItemTemplate>
      </asp:Repeater>
    </ul>
  </div>
</div>
<%-- href='<%# DataBinder.Eval(Container.DataItem,"url")%>'  --%>