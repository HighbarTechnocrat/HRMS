<%@ Control Language="C#" AutoEventWireup="true" CodeFile="category.ascx.cs" Inherits="themes_creative" %>

<asp:Panel ID="bday" runat="server">
    <div id="nav_menu-2" class="widget widget_nav_menu">
        <div class="intern-padding">
            <div class="intern-box box-title">
                <h3>Browse Category</h3>
            </div>
            <div class="menu-demo-nav-container">
                <ul id="menu-demo-nav" class="menu">
                    <asp:Repeater ID="rptcat" runat="server">
                        <ItemTemplate>
                            <li class="menu-item menu-item-type-custom menu-item-object-custom menu-item-423"><a href='<%#getcategoryURL( Eval("categoryname"),Eval("categoryid")) %>'><%#(DataBinder.Eval(Container, "DataItem.categoryname")) %></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    
                </ul>
            </div>
        </div>
    </div>
</asp:Panel>
