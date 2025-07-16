<%@ Control Language="C#" AutoEventWireup="true" CodeFile="latestnews3.ascx.cs" Inherits="themes_creative_LayoutControls_latestnews3" %>
<div class="birth-main" id="">
    <asp:Panel ID="pnlnews" runat="server" CssClass="widget box WiseChatWidget">
        <div class="intern-padding">
            <div class="intern-box box-title"><h3>Latest News</h3><asp:LinkButton ID="btnmore" runat="server" OnClick="btnmore_Click">View All</asp:LinkButton></div>
            <asp:Repeater ID="rptrnews" runat="server" OnItemDataBound="rptrnews_ItemDataBound"
                OnItemCommand="rptrnews_ItemCommand">
                <ItemTemplate>
                   <div class="textwidget">
                                <a href='newsdetail.aspx?newsid=<%# DataBinder.Eval(Container, "DataItem.newsid") %>'>
                                    <i class="fa fa-square"></i>&nbsp;<%# Eval("newstitle") %>
                                </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
</div>
