<%@ Control Language="C#" AutoEventWireup="true" CodeFile="news.ascx.cs" Inherits="themes_creative1_LayoutControls_news" %>

<asp:Panel ID="pnlnews" runat="server" Visible="false">
<div class="newsmaindiv">
<div class="news-main">
<div class="news-heading">Recent News</div>
<div id="newsticker">
<ul>
<asp:Repeater ID="rptnews" runat="server">
<ItemTemplate>
<li>
	<div class="news">
    <div class="newstitle">
	<a href='news.aspx?id=<%# DataBinder.Eval(Container, "DataItem.newsid") %>' title='<%# DataBinder.Eval(Container, "DataItem.Newstitle") %>'>
	<%# Eval("Newstitle")%></a>
    </div>
    <div class="newsdescsmall">
	<%# Eval("NewsDesclong")%>
    </div>
    <div class="newsdate"> - 
 	<asp:Label ID="Label1" runat="server" Text='<%#(DataBinder.Eval(Container, "DataItem.daydt")) %>'></asp:Label>
 	<asp:Label ID="Label3" runat="server" Text='<%#(DataBinder.Eval(Container, "DataItem.mndt")) %>'></asp:Label>
 	<asp:Label ID="Label4" runat="server" Text='<%#(DataBinder.Eval(Container, "DataItem.yeardt")) %>'></asp:Label> 
    </div>
    </div>
    <div class="newsdivider">
    	<span></span>
	</div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>

</div>
<div class="completelist">
	<a href="news.aspx" class="completelistbtn" title="News">Complete list</a>
</div>
</div>
</asp:Panel>