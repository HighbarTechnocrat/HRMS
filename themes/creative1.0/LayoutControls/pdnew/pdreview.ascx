<%@ Control Language="C#" AutoEventWireup="true" CodeFile="pdreview.ascx.cs" Inherits="themes_creative1_LayoutControls_pdnew_pdreview" %>
<script type="text/javascript">
    function pageLoad(sender, args) {
        jQuery("#jqxRating").jqxRating({ height: 35, value: document.getElementById("MainContent_pdnewglobal_m_uxPdLayout_ctl04_hdvalue2").value });
        jQuery("#jqxRating").bind('change', function (event) {
            document.getElementById("MainContent_pdnewglobal_m_uxPdLayout_ctl04_hdvalue2").value = event.value;
        });

        counter = function () {
            var value = jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').val();

            if (value.length == 0) {
                jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lblcount').html('400 characters remaining');
                return;
            }
            if (value.length > 400) {
                jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lblcount').html('0 characters remaining');
                return;
            }

            var regex = /\s+/gi;
            var wordCount = value.trim().replace(regex, ' ').split(' ').length;
            var totalChars = value.length;
            var charCount = value.trim().length;
            var charCountNoSpace = value.replace(regex, '').length;
            if (totalChars < 0) {
                jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lblcount').html('0 characters remaining');
            }
            else {
                jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lblcount').html(400 - totalChars + ' characters remaining');
            }
        };

        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').change(counter);
        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').keydown(counter);
        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').keypress(counter);
        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').keyup(counter);
        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').blur(counter);
        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').focus(counter);
        var value2 = jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').val();
        jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lblcount').html((400 - value2.length) + ' characters remaining');
        jQuery("#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lnksubmit").click(function () {

            if (document.getElementById("MainContent_pdnewglobal_m_uxPdLayout_ctl04_hdvalue2").value < 1) {
                if (jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_txtreview').val().length == 0) {
                    jQuery('#MainContent_pdnewglobal_m_uxPdLayout_ctl04_lblcount').html('Please enter review !!!');
                    return false;
                }
            }
        });
    }
    function jScript2() {
        new AnimOnScroll(document.getElementById('comment-grid'), {
            minDuration: 0.4,
            maxDuration: 0.7,
            viewportFactor: 0.2
        });
        jQuery(".popup_trigger").leanModal({ top: 0, overlay: 0.8, closeButton: ".modal_close" });
    }
</script>
<script type="text/javascript">
    window.onload = function () {
        pageLoad();
    };
</script>
  <%--  Sony commented thi/s to STOP display of comments trail as on 2 oct 2017 --%>
<%--<asp:UpdatePanel ID="updreviews" runat="server">
    <ContentTemplate>
        <script type="text/javascript">
            Sys.Application.add_load(jScript2);
        </script>
        <div id="criticcornerdiv" runat="server" class="criticcornerdiv">
            <div class="criticcorner">
                <div class="criticcorner-heading wow fadeInDown" style="display: none">The Critic’s Corner</div>
                <div class="criticcorner-subheading fadeInDown">What people are talking about this</div>
                <div class="criticcorner-writereview fadeInDown" id="divpopup" runat="server">
                    <a id="popup_trigger" class="popup_trigger" href="#modal" title="Write Comment"><i class="fa fa-edit"></i> Write Comment</a>
                </div>
                <div class="criticcorner-writereview fadeInDown" id="divpopuplogin" runat="server">
                    <asp:HyperLink ID="lnkpopup_trigger" runat="server" ToolTip="Write Comment" Visible="false"><i class="fa fa-edit"></i> Write Comment</asp:HyperLink>
                </div>
                <div class="criticcorner-container">
                    <ul class="criticcorner-grid effect slideInLeft" id="comment-grid">
                        <asp:Repeater ID="rptmoviereview" runat="server" OnItemCommand="rptmoviereview_ItemCommand">
                            <ItemTemplate>

                                <li style="opacity: 1;">
                                    <asp:UpdatePanel ID="updreview" runat="server">
                                        <ContentTemplate>
                                            <script type="text/javascript">
                                                Sys.Application.add_load(jScript2);
                                            </script>
                                            <div class="user-image">
                                                <asp:Image ID="imgprofile" runat="Server" class="reviewimgs" />
                                            </div>
                                            <div class="userinfo">
                                                <div class="user-name">
                                                    <asp:LinkButton ID="lnkusername" runat="server" CommandName="username" OnClientClick="window.document.forms[0].target='_blank';" CommandArgument='<%# Eval("username") %>'> <%# Eval("fullname")%> </asp:LinkButton>
                                                </div>
                                                <div class="user-rating" style="display: none;">
                                                    <asp:HiddenField ID="hdvalue" runat="server" Value='<%# Eval("ratingvalue") %>' ViewStateMode="Inherit" />
                                                    <asp:Label ID="lblratval" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ratingvalue") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblrevid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewid") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lbluseremail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.username") %>' Visible="false"></asp:Label>
                                                    <div class="ratingstar" style="padding: 0px; margin: 0px;">
                                                        <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                                            StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                                            EmptyStarCssClass="emptyRatingStar" ReadOnly="true">
                                                        </ajaxToolkit:Rating>
                                                    </div>
                                                </div>
                                                <div class="user-follow">
                                                    <asp:LinkButton ID="lnkfollow" CommandName="follow" runat="server" CommandArgument='<%# Eval("username") %>'></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="userinfo1">
                                                <div class="post-date">
                                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("reviewdate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                </div>
                                                <div class="post-comments">
                                                    <asp:LinkButton ID="lnkcomm" runat="server" CommandArgument='<%# Eval("reviewid") %>'> </asp:LinkButton>
                                                    <asp:Label ID="lblcommentcount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.commentcount") %>' Visible="false"></asp:Label>
                                                </div>
                                                <div class="post-likes">
                                                    <asp:LinkButton ID="lnklike" runat="server" CommandName="cmdlike" CommandArgument='<%# Eval("reviewid") %>'>
                                                        <asp:Label ID="lbllike" runat="server"></asp:Label>
                                                        <asp:Label ID="lbllikecount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>'></asp:Label>
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblcnt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.likecount") %>' Visible="false"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="user-comment">
                                                <asp:Label ID="lblreviews" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.reviewtext") %>'></asp:Label>
                                                <asp:LinkButton ID="read" runat="server" ForeColor="#88B7E6" Text="Read More" Visible="false"> </asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="lnkfollow" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="criticcorner-container-overlap" id="readmore" runat="server" visible="false">
                        <div class="readmore wow zoomInUp"><a id="lnkreadmore" runat="server"><i class="fa fa-comments"></i>Read More</a></div>
                        <div class="down-arrow" id="gotoyoumaylike" runat="server"><a href="#MainContent_pdnewglobal_m_uxPdLayout_ctl03_filmsyoumaylikediv"><i class="fa fa-angle-down"></i></a></div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>--%>
<asp:HiddenField ID="hdvalue2" runat="server" Value="0" ViewStateMode="Inherit" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <script type="text/javascript">
            Sys.Application.add_load(pageLoad);
        </script>
        <div class="container">
            <div id="modal" class="popupcontainer" style="display: none;">
                <div class="popupbody">
                    <div class="movie-info">
                        <div class="movie-name">
                            <asp:Label ID="lblproductname" runat="server"></asp:Label>
                        </div>
                        <div class="movie-year" style="display: none;">
                            <asp:Label ID="lblyear" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="movie-rating-div">
                        <div class="movie-rating" style="display: none;">
                            <div id='jqxRating' style="margin-left: 76px; float: left;"></div>
                        </div>
                        <div class="movie-comment-div">
                            <div class="movie-comment">
                                <textarea id="txtreview" runat="server" rows="5" maxlength="400" autofocus="autofocus"></textarea>
                                <div class="movie-comment-character">
                                    <asp:Label ID="lblcount" runat="server"></asp:Label>
                                </div><br />
                                <asp:RequiredFieldValidator ID="rvfcomment" runat="server" Display="Dynamic" CssClass="formerror" SetFocusOnError="True" ControlToValidate="txtreview" ErrorMessage="Please enter comment" ValidationGroup="validate"></asp:RequiredFieldValidator>
                                <div class="movie-post-comment">
                                    <asp:LinkButton ID="lnksubmit" runat="server" CssClass="modal_close" OnClick="lnksubmit_Click" ValidationGroup="validate">Submit</asp:LinkButton>
                                    <span class="modal_close">Cancel</span>
                                </div>
                                <div class="popupheader"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
