<%@ Control Language="C#" AutoEventWireup="true" CodeFile="mywall.ascx.cs" Inherits="themes_creative" EnableViewState="true" %>
<script src="<%=ReturnUrl("sitepath")%>js/highbar/jquery.js"></script>   
<%--<script type="text/javascript">
     //  jQuery = $;
       function jScript() {
            jQuery(window).scroll(function () {
                var wintop = jQuery(window).scrollTop(), docheight = jQuery(document).height(), winheight = jQuery(window).height();
                if (jQuery(window).scrollTop() + jQuery(window).height() == jQuery(document).height()) {
                    document.getElementById('<%= lnknext.ClientID %>').click();
                }
            });
        }
       jQuery(document).ready(function() { jScript(); });

</script>--%>
<%--<div class="postwallhome"><div id="evofc_widget-2" class="widget box evoFC_Widget mywall">
    <div class="intern-padding">
        <div class="widget_poll intern-box box-title">
            <h3>Post wall</h3>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(jScript);
                </script>
                <asp:Repeater ID="rptwall" runat="server" OnItemCommand="rptwall_ItemCommand">
                    <ItemTemplate>
                        <asp:UpdatePanel ID="updreview" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <script type="text/javascript">
                                    Sys.Application.add_load(jScript);
                                </script>
                                <div class="textwidget bd_wall">
                                     <a id="lnkimage" runat="server" class="imgwallpic" target="_blank" href='<%#getuser(Eval("indexid")) %>'>
                                                <img id="imgprofile" runat="server" alt=""
                                                    src='<%#getuserimage(Eval("profilephoto")) %>' />
                                            </a>
                                    <div class="bd_wallright">
                                        <a id="lnkusername" runat="server" class="imgwallpic" href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                                    <%# DataBinder.Eval(Container, "DataItem.productname") %></a> posted by <a id="LinkButton1" runat="server" class="imgwallpic" href='<%#getuser(Eval("indexid")) %>'><%# DataBinder.Eval(Container, "DataItem.fullname") %></a><br />
                                                <asp:Label ID="lbldate" runat="server" Text='<%#getdate(Eval("createdon")) %>'></asp:Label>
                                        <asp:Panel ID="img1" runat="server" Visible="false">
                                            <div class="user-comment">
                                                <a href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                                    <span class="wallimg"><%#getproductimage(Eval("bigimage")) %> </span></a>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlvideo" runat="server" Visible="false">
                                            <div class="user-comment">
                                                    <span class="wallvideo"><%#getpostvideo(Eval("videoembed"), Eval("movietrailorcode")) %> </span>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pstdoc" runat="server" Visible="false">
                                            <div class="user-comment">
                                                <span class="doclink"><a href='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'>
                                                    <%#getdocext(Eval("filename")) %> &nbsp;<span class="docfilename"><%#getpostdoc(Eval("filename")) %></span>
                                                </a></span>
                                            </div>
                                        </asp:Panel>
                                        <div class="user-comment">
                                            <%#getshortdesc(Eval("shortdescription")) %>
                                        </div>
                                        
                                        <div class="writereview" id="MainContent_reviewcomments_writereview">
                                            <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid") %>' Visible="false"></asp:Label>
                                            <asp:LinkButton ID="lnklike" runat="server" CssClass="btn-like" CommandName="cmdlike" CommandArgument='<%# Eval("productid") %>'>
                                                <asp:Label ID="lbllike" runat="server"></asp:Label>
                                                <asp:Label ID="lbllikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lblcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton ID="lnkdislike" CssClass="btn-like" runat="server" CommandName="cmddislike" CommandArgument='<%# Eval("productid") %>'>
                                                <asp:Label ID="lbldislike" runat="server"></asp:Label>
                                                <asp:Label ID="lbldislikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lbldcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="lnkcomm" runat="server"  CssClass="btn-like" CommandName="cmdcomment" CommandArgument='<%# Eval("productid")%>' PostBackUrl='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'> </asp:LinkButton>
                                        </div>

                                        </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkdislike" EventName="Click" />
                                     <asp:AsyncPostBackTrigger ControlID="lnknext" EventName="Click" />
                                </Triggers>
                        </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:LinkButton ID="lnknext" runat="server" OnClick="Page_Changed" style="display:none;">Next</asp:LinkButton>
            </ContentTemplate>
        </asp:UpdatePanel>
              <div id="desc" runat="server" visible="false"></div>
    </div>
</div>
</div>--%>