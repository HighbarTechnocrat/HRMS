<%@ Control Language="C#" AutoEventWireup="true" CodeFile="footerpost.ascx.cs" Inherits="themes_creative1" %>
    <div id="woffice_wiki" runat="server" class="widget col-md-4 widget_woffice_wiki animate-me fadeIn" visible="false">
        <h3>Recent post</h3>
        <ul class="list-styled list-wiki">
            <asp:Repeater ID="rptwall" runat="server">
                <ItemTemplate>
                    <li><a href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'><%# DataBinder.Eval(Container, "DataItem.productname") %>
                        <asp:Label ID="lblcount" runat="server" CssClass="count label" Visible="false"></asp:Label>
                        <asp:Label ID="lblpid" runat="server" Visible="false" Text='<%#Eval("productid")%>'></asp:Label>
                        </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
