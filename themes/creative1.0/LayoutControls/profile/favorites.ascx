<%@ Control Language="C#" AutoEventWireup="true" CodeFile="favorites.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_favorites" %>

  <!-- New Module -->
  <div class="mainpostwallcat">
  <div class="comments-summery1">
      <div class="userposts">
           <span> <asp:Label ID="lblcatname" runat="server"></asp:Label>
                        </span>
             </div>
      <asp:Panel ID="pnlpst" runat="server" Visible="true">

    <div class="comments-summerytitle">
        <div class="commentsdiv innerwall">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Repeater ID="rptwall" runat="server" OnItemCommand="rptwall_ItemCommand" >
                        <ItemTemplate>
                            <asp:UpdatePanel ID="updreview" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="commentsbyuser wow slideInLeft animated innerbd_wall">
                                            <a id="lnkimage" runat="server" target="_blank" href='<%#getuser(Eval("indexid")) %>'>
                                                <img id="imgprofile" runat="server" alt="" class="img_bd_wall"
                                                    src='<%#getuserimage(Eval("profilephoto")) %>' />
                                            </a>
                                        <div class="innerbd_wallright">
                                            <a id="lnkusername" runat="server" href='<%#productUrlrewriting2(Eval("productname"), Eval("productid")) %>'>
                                                    <%# DataBinder.Eval(Container, "DataItem.productname") %></a>
                                           <%-- SAGAR COMMENTED BELOW CODE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017 STARTS HERE--%>
                                          <%--  posted by <a id="LinkButton1" runat="server" href='<%#getuser(Eval("indexid")) %>'><%# DataBinder.Eval(Container, "DataItem.fullname") %></a>--%>
                                        <%-- SAGAR COMMENTED BELOW CODE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017 ENDS HERE--%>
                                                <asp:LinkButton ID="lnkremove" runat="server" CssClass="removefav" ToolTip="Remove Post" CommandName="cmdremove" 
                                                    CommandArgument='<%# Eval("productid") %>'><i class="fa fa-remove"></i></asp:LinkButton>
                                            <br />
                                                <asp:Label ID="lbldate" runat="server" Text='<%#getdate(Eval("createdon")) %>'></asp:Label>
                                            <br />
                                        <asp:Panel id="img1" runat="server">
                                        <div class="user-comment">
                                            <a href='<%#productUrlrewriting2(Eval("productname"), Eval("productid")) %>'>
                                               <span class="wallimg"><%#getproductimage(Eval("bigimage")) %> </span></a>
                                        </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlvideo" runat="server">
                                        <div class="user-comment">
                                               <span class="wallvideo"> <%#getpostvideo(Eval("videoembed"), Eval("movietrailorcode")) %> </span>
                                        </div>
                                        </asp:Panel>
                                        <asp:Panel id="pstdoc" runat="server">
                                        <div class="user-comment">
                                            <span class="doclink"><a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                                <%#getdocext(Eval("filename")) %>&nbsp;<span class="docfilename"><%#getpostdoc(Eval("filename")) %></span>
                                            </a></span>
                                        </div>
                                       </asp:Panel>
                                        <div class="user-comment">
                                            <%#getshortdesc(Eval("shortdescription")) %>
                                        </div>
                                        <div class="writereview" id="MainContent_reviewcomments_writereview">
                                            <asp:Label ID="lblpid" runat="server" Text='<%# Eval("productid") %>' Visible="false"></asp:Label>

                                            <asp:LinkButton ID="lnklike" runat="server" CommandName="cmdlike" CommandArgument='<%# Eval("productid") %>'>
                                                <asp:Label ID="lbllike" runat="server"></asp:Label>
                                                <asp:Label ID="lbllikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lblcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                                             <asp:LinkButton ID="lnkdislike" runat="server" CommandName="cmddislike" CommandArgument='<%#Eval("productid")%>'>
                                                <asp:Label ID="lbldislike" runat="server"></asp:Label>
                                                <asp:Label ID="lbldislikecount" runat="server"></asp:Label>
                                                <asp:Label ID="lbldcnt" runat="server" Visible="false"></asp:Label>
                                            </asp:LinkButton>&nbsp;&nbsp;&nbsp;
                      <asp:LinkButton ID="lnkcomm" runat="server" CommandArgument='<%# Eval("productid")%>' PostBackUrl='<%#productUrlrewriting2(Eval("productname"), Eval("productid")) %>'> </asp:LinkButton>
                                        </div>
                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkdislike" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div id="lastPostsLoader" style="text-align: center; margin: 20px 0px 0px;"></div>
        <div id="desc" runat="server" visible="false"></div>
        <div class="grid-pager">
          <asp:LinkButton ID="lnkprev" runat="server" CssClass="page_enabled" OnClick="lnkprev_Click" Visible="false" ToolTip="Previous"><<</asp:LinkButton> 
          <asp:Repeater ID="rptPager" runat="server">
                <ItemTemplate>
                       <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                </ItemTemplate>
          </asp:Repeater>
            <asp:LinkButton ID="lnknxt" runat="server" CssClass="page_enabled" OnClick="lnknxt_Click" Visible="false" ToolTip="Next">>></asp:LinkButton>
    </div>

    </div>
          </asp:Panel>
       <div class="userpostscats">
           <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
       </div>
 </div>
 </div>