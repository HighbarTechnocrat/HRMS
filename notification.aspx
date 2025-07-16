<%@ Page Title="" Language="C#" MasterPageFile="~/inner.master" AutoEventWireup="true" CodeFile="notification.aspx.cs" Inherits="newsdetails" EnableSessionState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>news/latestnews2.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>includes/mywall.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>notification/notification.css" rel="stylesheet" type="text/css" />

    <div class="mainpostwallcat1">
        <div class="comments-summery2">
            <div class="userposts" style="padding-bottom: 0px !important;" id="allread" runat="server" visible="false">
                <span>Notifications</span>
                <div class="cancelbtndiv" style="float: right;">
                    <asp:LinkButton ID="lnkreadall" runat="server" CssClass="submitbtnupdate1" ToolTip="Mark All Read" OnClick="lnkreadall_Click">
                        <i class="fa fa-check-circle-o" aria-hidden="true"></i>Mark All Read</asp:LinkButton>
                </div>
            </div>
            <asp:Panel ID="pnlnews" runat="server" Visible="false">

                <asp:Repeater ID="rptnotification" runat="server" OnItemCommand="rptnotification_ItemCommand">
                    <ItemTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="commentsbyuser1 wow slideInLeft animated">
                                    <asp:Label runat="server" ID="indexid" Text='<%# Eval("indexid")%>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="follow" Text='<%# Eval("id")%>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblpid" Text='<%# Eval("productid")%>' Visible="false"></asp:Label>
                                    <asp:Label runat="server" ID="lblevent" Text='<%# Eval("eventflag")%>' Visible="false"></asp:Label>


                                    <div class="notifications-detail1 notifications-active">
                                        <div class="notifications-photo">
                                            <a id="lnkimage" runat="server" target="_blank" href='<%#getuser(Eval("userid")) %>'>
                                                <asp:Image ID="imgprofile" runat="server" />
                                            </a>
                                        </div>
                                        <div class="notificationsdiv">
                                            <div class="notifications-heading1"><a id="A1" runat="server" target="_blank" href='<%#getuser(Eval("userid")) %>'><span><%# Eval("fullname")%></span> </a><%# Eval("notifytext")%><asp:Label runat="server" ID="lbltxtmsg" Visible="false"></asp:Label>
                                                <asp:LinkButton ID="lnkuser" runat="server" CommandName="cmdnotify" CssClass="notification_atag" CommandArgument='<%# Eval("indexid")%>'><span> <%# Eval("ptitle")%></span></asp:LinkButton>. </div>
                                            <%#geticons(Eval("eventflag")) %>
                                            <div class="notification-date1">
                                                <asp:Label ID="lblflag" runat="server" Text='<%# Eval("readflag")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lbldate" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="notifications-read">
                                            <asp:LinkButton ID="lnkread" runat="server" CommandName="cmdread" CommandArgument='<%# Eval("indexid") %>'>
                        <i class="fa fa-check-circle-o" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <%--  OnClick="lnkuser_Click"--%>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkread" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:Repeater>


            </asp:Panel>
            <div class="grid-pager">
                <asp:LinkButton ID="lnkprev" runat="server" CssClass="searchpostbtn" OnClick="lnkprev_Click"><<</asp:LinkButton>
                <asp:Repeater ID="rptPager" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>' OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="lnknxt" runat="server" CssClass="searchpostbtn" OnClick="lnknxt_Click">>></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="userpostscats">
        <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>

    </div>
</asp:Content>

