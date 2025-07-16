<%@ Page Title="" Language="C#" MasterPageFile="~/pdnew.master" AutoEventWireup="true" CodeFile="photogallery.aspx.cs" Inherits="pdnew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>gallery/jquery_popup.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>gallery/CSS.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>gallery/Chat.css" rel="stylesheet" type="text/css" />

    <style>
        .mainContainer.clearfix {
            min-height: 500px;
        }
    </style>
    <asp:HiddenField ID="hdvalue" runat="server" Value="0" ViewStateMode="Inherit" />
            <div id="postbackground">
                <div id="postbox">
                    <div id="imagebox">
                        <asp:LinkButton ID="lnkprevious" runat="server" CssClass="post-arrow-left" OnClick="lnkprevious_Click"><i class="fa fa-angle-left"></i></asp:LinkButton>
                        <asp:Image ID="imgbanner" runat="server" Style="max-width: 100%; max-height: 100%;" ImageAlign="Middle" />
                        <asp:LinkButton ID="lnknext" runat="server" CssClass="post-arrow-right" OnClick="lnknext_Click"><i class="fa fa-angle-right"></i></asp:LinkButton>
                    </div>
                    
                    <div id="commentbox" >
                        <div class="close">
                            <asp:LinkButton ID="HyperLink1" runat="server" OnClick="HyperLink1_Click" CssClass="cancel"><i aria-hidden="true" class="fa fa-times"></i></asp:LinkButton>
                        </div>
                        <div class="userprofile" style="display:none;">
                            <div style="width: 15%; height: 50px; float: left;">
                                <a id="lnkuser" runat="server">
                                    <img id="profileimg" runat="server" /></a>
                            </div>
                            <div class="wrap">
                                <asp:LinkButton ID="lnkname" runat="server" CssClass="link"></asp:LinkButton>
                            </div>
                            <div class="date">
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="wrap1">
                            <a href='<%#galleryUrlrewriting(Eval("productid")) %>' class="link">
                                <asp:Label ID="lblproductname" runat="server" Text="" CssClass="post-title"></asp:Label></a>
                            <br />
                            <asp:Label ID="lbldescription" Visible="true" runat="server" Text="" CssClass="post-desc"></asp:Label>
                        </div>
                        <asp:UpdatePanel ID="updgallerylike" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <%--SONY COMMENTED THIS TO HIDE DISPLAY OF SOCIAL MEDIA BUTTONS--%>
                           <!--
                            <div class="wrap2">
                            <asp:LinkButton ID="lnklike" runat="server" OnClick="lnklike_Click" CssClass="link1">
                                <asp:Label ID="lbllike" runat="server"></asp:Label>
                                <asp:Label ID="lbllikecount" runat="server"></asp:Label>
                                <asp:Label ID="lblcnt" runat="server" Visible="false
                               "></asp:Label>
                            </asp:LinkButton>&nbsp;
                             <asp:LinkButton ID="lnkdislike" runat="server" OnClick="lnkdislike_Click" CssClass="link1">
                                 <asp:Label ID="lbldislike" runat="server"></asp:Label>
                                 <asp:Label ID="lbldislikecount" runat="server"></asp:Label>
                                 <asp:Label ID="lbldcnt" runat="server" Visible="false"></asp:Label>
                             </asp:LinkButton>&nbsp;
                     <asp:Label ID="lnkcomm" runat="server" CssClass="link1"> </asp:Label>&nbsp;
                            <asp:LinkButton ID="lnkfav" runat="server" OnClick="lnkfav_Click" CssClass="link1">
                                <asp:Label ID="lblfav" runat="server"></asp:Label>
                            </asp:LinkButton>
                        </div>
                            -->

                        </ContentTemplate>
                        <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />
                           <asp:AsyncPostBackTrigger ControlID="lnkdislike" EventName="Click" />
                           <asp:AsyncPostBackTrigger ControlID="lnkfav" EventName="Click" />
                       </Triggers>
                     </asp:UpdatePanel>
                         <asp:UpdatePanel ID="updcomment" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <%--SONY COMMENTED THIS TO HIDE DISPLAY of COMMENT BOX--%>
                    <!--
                        <div class="commentbox">
                          <div class="enterreply wow slideInLeft animated" id="divmsg" runat="server" visible="false" style="visibility: visible; animation-name: slideInLeft;">
                                Please <a id="lnklogin" runat="server" style="color: #318CE8;">Login</a> to write Comment !!!
                            </div>
                            <ul class="border-none" id="divcomment" runat="server">
                                <li>
                                    <a id="lnkprofile2" runat="server" class="post-comm-img">
                                        <img id="imgProfile2" runat="server" class="comm-img-profile" />  &nbsp <asp:Label ID="lblname" runat="server"></asp:Label></a>
                                    <div id='jqxRating' style="margin: 5px 10px !important; float: left;" runat="server" visible="false"></div>
                                </li>
                                <li>
                                    <asp:TextBox ID="CommentText" runat="server" CssClass="post-comm-box inputpost" TextMode="MultiLine" placeholder="Write a comment... "></asp:TextBox>
                                    <asp:LinkButton ID="btnsendmail" runat="server" CssClass="message-box-searchbtn1" OnClick="btnsendmail_Click" ValidationGroup="validate"><i class="fa fa-send m-r-xs"></i></asp:LinkButton><br />
                                     <asp:RequiredFieldValidator ID="reqcomment" runat="server" ForeColor="Red" ControlToValidate="CommentText" ErrorMessage="Please enter comment." Display="Dynamic" SetFocusOnError="True" ValidationGroup="validate"></asp:RequiredFieldValidator>
                                </li>
                            </ul>


                        </div>
                        <div class="commentbox" id="commentlist" runat="server">
                            <asp:Repeater ID="rptmoviereview" runat="server" OnItemCommand="rptmoviereview_ItemCommand">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="updreview" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <ul>
                                                <li>
                                                    <div class="userprofile">
                                                        <div style="width: 40px; height: 40px; float: left;">
                                                            <asp:LinkButton ID="lnkuser2" runat="server">
                                                                <asp:Image ID="imgprofile" runat="Server" class="comm-img-profile" />
                                                            </asp:LinkButton>
                                                        </div>
                                                        <div class="wrap">
                                                            <asp:LinkButton ID="lnkusername" runat="server" CommandName="username" OnClientClick="window.document.forms[0].target='_blank';" CommandArgument='<%# Eval("username") %>'> <%# Eval("fullname")%> </asp:LinkButton>
                                                        </div>
                                                        <div class="date">
                                                            <asp:Label ID="lbldate" runat="server" Text='<%# Eval("reviewdate") %>'></asp:Label>
                                                            <ajaxToolkit:Rating ID="Rating2" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                                                StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                                                EmptyStarCssClass="emptyRatingStar" ReadOnly="true" CssClass="user-rate">
                                                            </ajaxToolkit:Rating>
                                                        </div>
                                                    </div>
                                                    <asp:HiddenField ID="hdvalue2" runat="server" Value='<%# Eval("ratingvalue") %>' ViewStateMode="Inherit" />
                                                    <asp:Label ID="lblratval" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ratingvalue") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblrevid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewid") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbluseremail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.username") %>' Visible="false"></asp:Label>
                                                </li>
                                                <li>
                                                    <asp:Label ID="lblreviews" runat="server" CssClass="post-review" Text='<%# DataBinder.Eval(Container, "DataItem.reviewtext") %>'></asp:Label>
                                                    <asp:LinkButton ID="read" runat="server" ForeColor="#88B7E6" Text="Read More" Visible="false"> </asp:LinkButton>
                                                </li>
                                                <li>
                                                    <div class="div-social">
                                                        <asp:LinkButton ID="lnkfollow" CommandName="follow" runat="server" CommandArgument='<%# Eval("username") %>'></asp:LinkButton>
                                                        <asp:LinkButton ID="lnklike2" runat="server" CommandName="cmdlike" CommandArgument='<%# Eval("reviewid") %>'>
                                                            <asp:Label ID="lbllike2" runat="server"></asp:Label>
                                                            <asp:Label ID="lbllikecount2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>'></asp:Label>
                                                        </asp:LinkButton>
                                                        <asp:Label ID="lblcnt2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>' Visible="false"></asp:Label>
                                                        <asp:LinkButton ID="lnkcomm2" runat="server" CommandArgument='<%# Eval("reviewid") %>'> </asp:LinkButton>
                                                        <asp:Label ID="lblcommentcount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.commentcount") %>' Visible="false"></asp:Label>
                                                    </div>
                                                </li>
                                            </ul>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnklike2" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkfollow" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        -->
                     </ContentTemplate>
                              <Triggers>
                                         <asp:AsyncPostBackTrigger ControlID="btnsendmail" EventName="Click" />
                              </Triggers>
         </asp:UpdatePanel>
                    </div>
                </div>
            </div>
       
</asp:Content>

