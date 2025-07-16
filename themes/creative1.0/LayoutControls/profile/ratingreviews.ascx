<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ratingreviews.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_ratingreviews" %>
<link rel="stylesheet" href="<%=ReturnUrl("css") %>contact/contact.css" type="text/css" media="all" />
<link rel="stylesheet" href="<%=ReturnUrl("css") %>messageboard/messagewall.css" type="text/css" media="all" />
<link rel="stylesheet" href="<%=ReturnUrl("css") %>highbar/hrms.css" type="text/css" media="all" />
<div class="profilediv" id="profilediv" runat="server">
    <div class="mainpostwallcat">
        <div class="msgpage">
            <div class="myaccountpagesheading msgboardheading">Message Board</div>
            <div class="userposts">
            </div>
            <div class="contact-container msg-container">
                <ul id="MainContent_userreviews_msgboard">
                    <%--                    <li class="msglable2"><span class="msgheading">WRITE ON WALL</span></li>--%>
                    <li class="msglable"><span class="titlewriteonwall">Title </span>&nbsp;
                           <span class="tiltewall">
                               <asp:TextBox ID="txttitle" runat="server" MaxLength="50" CssClass="msgtitle"> </asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="formerror" ControlToValidate="txttitle" SetFocusOnError="True" ErrorMessage="Please enter Title" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                           </span>
                    </li>
                    <li class="msglable1"><span>Message </span>&nbsp;
                           <span>
                               <asp:TextBox ID="txtmsg" runat="server" MaxLength="100" TextMode="MultiLine" ValidationGroup="validate" CssClass="msgdesc"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="formerror" ControlToValidate="txtmsg" SetFocusOnError="True" ErrorMessage="Please enter Message" Display="Dynamic" ValidationGroup="validate"></asp:RequiredFieldValidator>
                           </span>
                    </li>

                    <li class="proviewbtn">
                        <div class="cancelbtndiv">
                            <asp:LinkButton ID="btnmsg" runat="server" Text="Add Message" ToolTip="Update" ValidationGroup="validate" CssClass="submitbtnupdate1" OnClick="btnmsg_Click"><i class="fa fa-comment" aria-hidden="true"></i>Add Message</asp:LinkButton>
                        </div>
                        <div class="cancelbtndiv" id="homebtn" runat="server" visible="false">
                            <asp:LinkButton ID="btnhome" runat="server" ToolTip="Back To Home" CssClass="submitbtnupdate2" OnClick="btnhome_Click"><i class="fa fa-undo" aria-hidden="true"></i>Cancel</asp:LinkButton>
                        </div>
                    </li>
                    <li class="msglable"><span></span>&nbsp;
                        <asp:Label ID="lblmsgvalue" runat="server" Visible="false"></asp:Label><span></span></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="movierowdiv">


        <!-- Mywall -->
        <div class="userpostwall1">
            <!-- Message Board -->
            <div class="mainpostwallcat">

                <div class="profilemsg" id="div1" runat="server" visible="false">
                </div>


                <!-- Uesr Post Wall Start Here-->

                <div class="mypostrightpanel">
                    <div class="grouprightpanel1">

                        <div class="comments-summerytitle">
                            <div class="comments-summerytitle">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="userpost2">
                                            <div class="userposts">
                                                <span class="mypost">My Posts</span>

                                            </div>
                                             <asp:DropDownList ID="ddlposttype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlposttype_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            <asp:Panel ID="pnlmypst" runat="server" Visible="true">
                                               
                                                <div class="commentsdiv mywall1">

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
                                                                                <%# DataBinder.Eval(Container, "DataItem.productname") %></a>
                                                                             <%-- SAGAR COMMENTED BELOW CODE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017 STARTS HERE--%>
                                                                             <%--posted by <a id="LinkButton1" runat="server" class="imgwallpic" href='<%#getuser(Eval("indexid")) %>'><%# DataBinder.Eval(Container, "DataItem.fullname") %></a>--%>
                                                                             <%-- SAGAR COMMENTED BELOW CODE FOR REMOVING POSTED BY AND NAME OF THE USER WHO POSTED THE POST 9OCT2017 ENDS HERE--%>
                                                                            <br />
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
                      <asp:LinkButton ID="lnkcomm" runat="server" CssClass="btn-like" CommandName="cmdcomment" CommandArgument='<%# Eval("productid")%>' PostBackUrl='<%#productUrlrewriting2( Eval("cattype"),Eval("productname"), Eval("productid")) %>'> </asp:LinkButton>
                                                                            </div>
                                                                            <asp:LinkButton ID="lnknext" runat="server" OnClick="Page_Changed" Style="display: none;">Next</asp:LinkButton>
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

                                                </div>
                                                <div id="desc" runat="server" visible="false"></div>
                                                <div class="grid-pager">
                                                    <asp:LinkButton ID="lnkprev" runat="server" CssClass="searchpostbtn" OnClick="lnkprev_Click" ToolTip="Next"><<</asp:LinkButton>
                                                    <asp:Repeater ID="rptPager" runat="server">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                                OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <asp:LinkButton ID="lnknxt" runat="server" CssClass="searchpostbtn" OnClick="lnknxt_Click" ToolTip="Previous">>></asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                            <div class="userpostscats">
                                                <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>

                                            </div>
                                            </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <!-- Like and comment page div -->
                                <div class="movierow" id="movierow">
                                    <div class="userpostlike">
                                        <span>My Likes & Comments</span>
                                    </div>
                                    <asp:Panel ID="pnlmylike" runat="server" Visible="true">
                                        <div>
                                            <asp:Repeater ID="rptuserreview" runat="server"
                                                OnItemCommand="rptuserreview_ItemCommand">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div class="movieinfo">
                                                                <div class="movie-image">
                                                                    <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                                                        <span class="commenttitle">
                                                                            <%# Eval("productname").ToString().Length > 30 ? Eval("productname").ToString().Substring(0, 30)+"..." : Eval("productname") %></span>
                                                                    </a>
                                                                </div>
                                                                <asp:Panel ID="pnlimg1" runat="server" Visible="false">
                                                                    <div class="movie-image">
                                                                        <a id="lnkproduct" runat="server" href='<%#productUrlrewriting2( Eval("productname"), Eval("productid")) %>'><span class="wallimg"> <%#getproductimage1(Eval("bigimage")) %></span></a>
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pnlvideo1" runat="server" Visible="false">
                                                                    <div class="movie-image">
                                                                        <a href='<%#productUrlrewriting( Eval("productname"), Eval("productid")) %>'>
                                                                            <span class="wallvideo"><%#getpostvideo1(Eval("videoembed"), Eval("movietrailorcode")) %></span></a>
                                                                    </div>
                                                                </asp:Panel>
                                                                <div class="movie-info-div">
                                                                    <div class="movie-star">
                                                                        <asp:Panel ID="pnlstar" runat="server" Visible="false">
                                                                            <div class="ratingstar" style="padding: 0px; margin: 0px;">
                                                                                <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                                                                    StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                                                                    EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                                                                                </ajaxToolkit:Rating>
                                                                            </div>
                                                                        </asp:Panel>

                                                                    </div>
                                                                    <div class="movie-comments">
                                                                        <asp:Label ID="lblrevid" runat="server" Text='<%# Eval("reviewid") %>' Visible="false"></asp:Label>
                                                                        <asp:LinkButton ID="lnkcomm1" runat="server" CommandName="cmdcomment" CommandArgument='<%# Eval("reviewid") %>'> </asp:LinkButton>
                                                                    </div>
                                                                    <div class="movie-likes">
                                                                        <asp:LinkButton ID="lnklike1" runat="server" CommandName="cmdlike" CommandArgument='<%# Eval("reviewid") %>'>
                                                                            <asp:Label ID="lbllike1" runat="server"></asp:Label>
                                                                            <asp:Label ID="lbllikecount1" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblcnt1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>' Visible="false"></asp:Label>
                                                                        </asp:LinkButton>
                                                                    </div>
                                                                    <div class="movie-comment">
                                                                        <asp:Label ID="lblreviews" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewtext") %>'></asp:Label>
                                                                        <asp:LinkButton ID="read" runat="server" CommandName="cmdread" CommandArgument='<%# Eval("reviewid") %>' ForeColor="#88B7E6" Text="Read More" Visible="false"> </asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="lnklike1" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <div class="grid-pager">
                                            <asp:LinkButton ID="lnkprev1" runat="server" CssClass="searchpostbtn" OnClick="lnkprev1_Click"><<</asp:LinkButton>
                                            <asp:Repeater ID="rptPager1" runat="server">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkPage1" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                                        OnClick="lnkPage1_Click" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:LinkButton ID="lnknxt1" runat="server" CssClass="searchpostbtn" OnClick="lnknxt1_Click">>></asp:LinkButton>
                                        </div>
                                    </asp:Panel>
                                    <div class="userpostscats">
                                        <asp:Label ID="lblmsg1" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="profilemsg" id="divmsg" runat="server" visible="false"></div>

                    </div>
                </div>

            </div>
        </div>

    </div>
</div>


