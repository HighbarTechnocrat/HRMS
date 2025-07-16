<%@ Control Language="C#" AutoEventWireup="true" CodeFile="reviewbanner.ascx.cs" Inherits="themes_creative1_LayoutControls_profile_reviewbanner" %>
<script type="text/javascript">
    function jScript() {

        counter = function () {
            var value = $('#MainContent_reviewcomments_txtImagename1').val();

            if (value.length == 0) {
                jQuery('#MainContent_reviewcomments_lblcount').html('400 characters remaining');
                return;
            }
            if (value.length > 400) {
                jQuery('#MainContent_reviewcomments_lblcount').html('0 characters remaining');
                return;
            }
            if (value.length > 0) {
                jQuery('#MainContent_reviewcomments_lblerror').html('');
            }
            var regex = /\s+/gi;
            var wordCount = value.trim().replace(regex, ' ').split(' ').length;
            var totalChars = value.length;
            var charCount = value.trim().length;
            var charCountNoSpace = value.replace(regex, '').length;
            if (totalChars < 0) {
                jQuery('#MainContent_reviewcomments_lblcount').html('0 characters remaining');
            }
            else {
                jQuery('#MainContent_reviewcomments_lblcount').html(400 - totalChars + ' characters remaining');
            }
        };

        jQuery(document).ready(function () {
            jQuery('#MainContent_reviewcomments_txtImagename1').change(counter);
            jQuery('#MainContent_reviewcomments_txtImagename1').keydown(counter);
            jQuery('#MainContent_reviewcomments_txtImagename1').keypress(counter);
            jQuery('#MainContent_reviewcomments_txtImagename1').keyup(counter);
            jQuery('#MainContent_reviewcomments_txtImagename1').blur(counter);
            jQuery('#MainContent_reviewcomments_txtImagename1').focus(counter);
            var value2 = jQuery('#MainContent_reviewcomments_txtImagename1').val();
            jQuery('#MainContent_reviewcomments_lblcount').html((400 - value2.length) + ' characters remaining');
            var comment = jQuery('#MainContent_reviewcomments_txtImagename1').val();
            if (comment.length > 0)
                jQuery('#MainContent_reviewcomments_lblerror').html('');

        });
    }
</script>

<div id="coverphoto" runat="server" class="moviebannerposter backgroundimage" data-img-width="1750" data-img-height="1064" visible="false"></div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
  <ContentTemplate> 
    <script type="text/javascript">
            Sys.Application.add_load(jScript);
        </script>
    <div class="comments-summery">
      <div class="comments-summerytitle">
        <div class="moviename wow fadeInDown"> <a id="lnkproduct" runat="server" style="text-decoration: none; ">
          <asp:Label ID="lblproductname" runat="server" Text=""></asp:Label>
          </a> </div>

          <!-- Posted by and their name -->
        <div class="dateanddirector wow fadeInDown" >
          
          <span id="director" runat="server">
             <%-- SAGAR COMMENTED BELOW POSTED BY AND ADDED visible="false" IN lnkdirector AND lbldirectorname 9OCT2017--%>
              <%--Posted by--%> </span> <a id="lnkdirector" runat="server" visible="false">
          <asp:Label ID="lbldirectorname" runat="server" Text="" visible="false"></asp:Label>
          </a> 
           <%-- SAGAR CODE ENDS HERE 9OCT2017--%>
            <span id="Span1" runat="server">&nbsp;&nbsp;On&nbsp;&nbsp;</span>
            <asp:Label ID="lblyear" runat="server" CssClass="productyear"></asp:Label>
        </div>


        <div class="commentsdiv">
          <div class="commentsbyuser wow slideInLeft">
            <div class="comments-user-image"> <a id="lnkimage" runat="server" target="_blank"> <img id="imgprofile" runat="server" class="reviewimgs" alt=""> </a> </div>
            <div class="userinfo">
              <div class="user-name"> <a id="lnkusername" runat="server" target="_blank">
                <asp:Label ID="lblusername" runat="server" Text=""></asp:Label>
                </a> </div>
              <div class="user-rating">
                <div class="ratingstar" style="padding: 0px; margin: 0px;">
                  <ajaxToolkit:Rating ID="Rating1" runat="server" BehaviorID="Ratingproduct" MaxRating="5"
                                        StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                                        EmptyStarCssClass="emptyRatingStar" ReadOnly="true"> </ajaxToolkit:Rating>
                </div>
              </div>
            </div>
            <div class="writereview" id="writereview" runat="server"> <a href="#MainContent_reviewcomments_enterreply"><i class="fa fa-pencil"></i>&nbsp; Reply</a> </div>
            <div class="userinfo1">
              <div class="post-date">
                <asp:Label ID="lbldate" runat="server"></asp:Label>
              </div>
              <div class="post-comments">
                <asp:LinkButton ID="lnkcomm" runat="server" Enabled="false"> </asp:LinkButton>
              </div>
              <div class="post-likes">
                <asp:LinkButton ID="lnklike" runat="server" OnClick="lnklike_Click">
                  <asp:Label ID="lbllike" runat="server"></asp:Label>
                  <asp:Label ID="lbllikecount" runat="server"></asp:Label>
                </asp:LinkButton>
              </div>
            </div>
            <div class="user-comment">
              <asp:Label ID="lblreviews" runat="server"></asp:Label>
            </div>
          </div>
        </div>
        <div class="comments-scroll mCustomScrollbar" id="commentlist" runat="server" data-mcs-theme="minimal-dark">
          <asp:Repeater ID="rptmoviereview" runat="server">
            <ItemTemplate>
              <div class="replybyuser">
                <div class="reply-user-image">
                  <asp:LinkButton ID="lnkimage" runat="server" OnClientClick="window.document.forms[0].target='_blank';">
                    <asp:Image ID="imgprofile" runat="Server" class="reviewimgs1" />
                  </asp:LinkButton>
                </div>
                <div class="userinfo">
                  <div class="user-name">
                    <asp:LinkButton ID="lnkusername" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fullname") %>' OnClientClick="window.document.forms[0].target='_blank';"> </asp:LinkButton>
                  </div>
                  <div class="post-date">
                    <asp:Label ID="lbldate" runat="server"></asp:Label>
                  </div>
                  <div class="user-comment">
                    <asp:Label ID="lblreviews" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.commenttext") %>'></asp:Label>
                  </div>
                </div>
              </div>
            </ItemTemplate>
          </asp:Repeater>
        </div>
        <div id="enterreply" class="enterreply wow slideInLeft" runat="server">
          <div class="enterreply-user-image"> <img id="imgprofile2" runat="server" class="reviewimgs1" alt=""> </div>
          <div class="userinfo">
            <div class="user-name"> <a id="lnkusername2" runat="server">
              <asp:Label ID="lblusername2" runat="server" Text="" ></asp:Label>
              </a> </div>
          </div>
          <div class="user-comment-box">
            <textarea id="txtImagename1" name="txtImagename1" runat="server" maxlength="400"></textarea>
            <div class="userinfo" style="width: 99%; margin-left: 0px;">
              <div style="float: left;">
                <asp:Label ID="lblerror" runat="server" ForeColor="#ff0000"></asp:Label>
              </div>
              <div style="float: right;">
                <asp:Label ID="lblcount" runat="server"></asp:Label>
              </div>
            </div>
            <div class="post-button">
              <asp:LinkButton ID="lnksubmit" runat="server" OnClick="lnksubmit_Click">Post Comment</asp:LinkButton>
            </div>
          </div>
        </div>
        <div id="divmsg" class="enterreply wow slideInLeft" runat="server"> Please <a id="lnklogin" runat="server" style="color:#318CE8;">Login</a> to write Comment !!! </div>
      </div>
    </div>
  </ContentTemplate>
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="lnksubmit" EventName="Click" />
  </Triggers>
</asp:UpdatePanel>
