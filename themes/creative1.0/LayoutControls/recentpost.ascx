<%@ Control Language="C#" AutoEventWireup="true" CodeFile="recentpost.ascx.cs" Inherits="themes_creative1" %>
        <div id="recent-posts-2" class="widget widget_recent_entries">
            <div class="intern-padding">
                <div class="intern-box box-title">
                    <h3>Recent Posts</h3>
                </div>
                <ul>
                    <asp:Repeater ID="rptwall" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'> <%# DataBinder.Eval(Container, "DataItem.productname") %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
