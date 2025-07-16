<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footercat.ascx.cs" Inherits="themes_creative" %>
    <div id="bday" runat="server" class="widget col-md-4 animate-me fadeIn">
        <h3>Browse Category</h3>
        <div class="menu-menu-footer-container">
            <ul id="menu-menu-footer" class="menu">
                <asp:Repeater ID="rptcat" runat="server">
                    <ItemTemplate>
                        <li id="menu-item-439" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-439"><a href='<%#getcategoryURL( Eval("categoryname"),Eval("categoryid")) %>'><%#(DataBinder.Eval(Container, "DataItem.categoryname")) %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
