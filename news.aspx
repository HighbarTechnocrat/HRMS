<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="newsdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>profile/mprofile.css" rel="stylesheet" type="text/css" />
    <div class="mainpostwallcat">
        <div class="comments-summery1">
            <div class="userposts"><span>News</span> </div>
            <asp:Panel ID="pnlnews" runat="server" Visible="false">
                <asp:Repeater ID="rptrnews" runat="server">
                    <ItemTemplate>
                        <div class="commentsbyuser slideInLeft animated innerbd_wall">
                            <div class="userinfo">
                                <div class="user-name">
                                    <a id="lnkusername" href='<%#getnewsURL(Eval("newsid")) %>'><%# Eval("newstitle") %>
                                    </a>
                                    <br />
                                    News Date :
                                    <asp:Label ID="lbldate" runat="server" Text='<%# Eval("newsdate")%>'></asp:Label>
                                </div>
                                <div class="user-rating">
                                </div>

                                <div class="user-comment">
                                    <%# Eval("NewsDescSmall").ToString().Length > 150 ? Eval("NewsDescSmall").ToString().Substring(0, 147)+"..." : Eval("NewsDescSmall")%>
                                </div>
                            </div>

                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="grid-pager">
                    <asp:Repeater ID="rptPager" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

