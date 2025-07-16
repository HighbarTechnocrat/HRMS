<%@ Control Language="C#" AutoEventWireup="true" CodeFile="topheaderbanner.ascx.cs" Inherits="Themes_FirstTheme_LayoutControls_topheaderbanner"%>
<%--<%@ Register Src="~/themes/creative1.0/LayoutControls/mywall.ascx" TagName="mywall" TagPrefix="ucmywall"%>--%>
<%--<div class="bannertoprollimg"></div>--%>
 
     <section class="slider">
            <div class="flexslider">
               
                
                <ul class="slides">
    <asp:Repeater ID="rptbanner" runat="server">
                <ItemTemplate>
                  <li> <a href='<%# DataBinder.Eval(Container.DataItem,"url")%>' title='<%# Eval("altname") %>'> <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"] %>images/banner/<%# Eval("imagename") %>' /> </a> </li>
      </ItemTemplate>
    </asp:Repeater>
               </ul>
          
                   
                 </div>
        </section>

<%--<ucmywall:mywall ID="uxmywall" runat="server" />--%>

<%--<div class="bannerbottomrollimg"></div>--%>