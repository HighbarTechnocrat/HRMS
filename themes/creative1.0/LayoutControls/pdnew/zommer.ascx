<%@ Control Language="C#" AutoEventWireup="true" CodeFile="zommer.ascx.cs" Inherits="Themes_SecondTheme_LayoutControls_pdnew_zommer" %>

<div id="home" runat="server" class="maincatbannerimage backgroundimage" data-img-width="1920" data-img-height="1080">
  <div class="play-btn-div" id="divwatch" runat="server">
    <div class="play-btn-bg" id="divwatch2" runat="server">
      <asp:LinkButton ID="lnkmovie" runat="server" 
          onclick="lnkmovie_Click"><i class="fa fa-play"></i> </asp:LinkButton>
    </div>
  </div>
  <div class="play-btn-div" id="divsubscribe" runat="server" visible="false">
    <div class="play-btn-bg" id="divsubscribe2" runat="server">
      <asp:LinkButton ID="lnksubscribe" runat="server" 
          onclick="lnksubscribe_Click"><i class="fa fa-play"></i> </asp:LinkButton>
    </div>
  </div>
  <div class="banner-down-arrow"><a href="#productsummery"><i class="fa fa-angle-down"></i></a></div>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
<div class="sticky-right-panel">
     
  <ul>
   <%-- <li style="display:none;" id="trailor" runat="server" visible="false">
      <asp:LinkButton ID="lnktrailor" runat="server" onclick="lnktrailor_Click" ToolTip="View Trailer"><i class="fa fa-film"></i></asp:LinkButton>
    </li>
      <li>
          <asp:LinkButton ID="lnklike" runat="server" OnClick="lnklike_Click">
          <i id="lnkthumbup" runat="server" class="fa fa-thumbs-up"></i>
              </asp:LinkButton>
      </li>--%>
    <%--  <li>
          <asp:LinkButton ID="lnkdislike" runat="server" OnClick="lnkdislike_Click">
          <i id="lnkthumbdown" runat="server" class="fa fa-thumbs-down"></i>
              </asp:LinkButton>
      </li>--%>
    <li id="gotocritics" runat="server"><a href="#MainContent_pdnewglobal_m_uxPdLayout_ctl04_criticcornerdiv" title="View Comment"><i class="fa fa-comments"></i></a> </li>
    <%--<li><asp:LinkButton ID="lnkfav" runat="server" OnClick="lnkfav_Click">--%><%--<a id="lnkfav" runat="server">--%><%--<i id="lnkheart" runat="server" class="fa fa-heart"></i></asp:LinkButton> </li>--%>
 
         <%--<li><asp:LinkButton ID="lnkenquiry" runat="server" OnClick="lnkenquiry_Click" ToolTip="Enquiry"><i id="I1" runat="server" class="fa fa-question-circle"></i></asp:LinkButton> </li>--%>
 <%--   <li>
      <div id="popup" runat="server"><a id="popup_trigger" class="popup_trigger" href="#modal" title="Write Comment"><i class="fa fa-edit"></i></a></div>
    </li>--%>
    <li>
      <asp:HyperLink ID="lnkpopup_trigger" runat="server" ToolTip="Write Review" Visible="false"><i class="fa fa-edit"></i></asp:HyperLink>
    </li>
    <li style="display:none;">
      <div id="share">
        <div id="share-slidable" class="hide">
          <ul>
            <li><a target="_blank" href="http://www.facebook.com/share.php?u=<url>" onclick="return fbs_click()" title="Facebook"><i class="fa fa-facebook"></i></a></li>
            <li><a target="_blank" href="https://twitter.com/share" data-lang="en" data-count="none" title="Tweet"><i class="fa fa-twitter"></i></a></li>
            <li> <a id="googleshare" runat="server" onclick="javascript:window.open(this.href,
  '', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=600,width=600');return false;"><i class="fa fa-google-plus"></i> </a> </li>
              <li>
              <asp:Literal ID="ltrimage" runat="server"></asp:Literal>
            </li>
             
          </ul>
        </div>
        <div id="share-side"><i title="Share" class="fa fa-share-alt"></i></div>
      </div>
    </li>
  </ul>
            
</div>
             
        </ContentTemplate>
    <Triggers>
      <%--  <asp:AsyncPostBackTrigger ControlID="lnklike" EventName="Click" />--%>
       <%-- <asp:AsyncPostBackTrigger ControlID="lnkdislike" EventName="Click" />--%>
      <%--  <asp:AsyncPostBackTrigger ControlID="lnkfav" EventName="Click" />--%>
    </Triggers>
    </asp:UpdatePanel>

<div class="sticky-hidden-div" runat="server" id="strhdiv">
  <div class="sticky-right-panel-mobile">
    <ul>
      <li style="display:none;" id="flim" runat="server" visible="false">
        <asp:LinkButton ID="lnktrailormob" runat="server" onclick="lnktrailor_Click"><i class="fa fa-film"></i></asp:LinkButton>
      </li>
        <li>
          <a id="lnklikemob" runat="server"><i id="lnkthumbupmob" runat="server" class="fa fa-thumbs-up"></i></a>
      </li>
        <li>
          <a id="lnkdislikemob" runat="server"><i id="lnkthumbdownmob" runat="server" class="fa fa-thumbs-down"></i></a>
      </li>
      <li id="gotocriticsmob" runat="server"><a href="#MainContent_pdnewglobal_m_uxPdLayout_ctl04_criticcornerdiv"><i class="fa fa-comments"></i></a></li>
      <li><a id="lnkfavmob" runat="server"><i id="lnkheartmob" runat="server" class="fa fa-heart"></i></a></li>
      <li><a target="_blank" href="http://www.facebook.com/share.php?u=<url>" onclick="return fbs_click()" title="Facebook"><i class="fa fa-facebook"></i></a></li>
      <li><a target="_blank" href="https://twitter.com/share" data-lang="en" data-count="none" title="Tweet"><i class="fa fa-twitter"></i></a></li>
      <li><a id="googlesharemob" runat="server" onclick="javascript:window.open(this.href,
  '', 'menubar=no,toolbar=no,resizable=yes,scrollbars=yes,height=600,width=600');return false;"><i class="fa fa-google-plus"></i> </a></li>
      <li>
        <asp:Literal ID="ltrimage2" runat="server"></asp:Literal>
      </li>
      <li>
        <div id="popupmob" runat="server"><a id="popup_triggermob" class="popup_trigger" href="#modal" title="Write Review"><i class="fa fa-edit"></i></a></div>
      </li>
      <li>
        <asp:HyperLink ID="lnkpopup_triggermob" runat="server" ToolTip="Write Comment" Visible="false"><i class="fa fa-edit"></i></asp:HyperLink>
      </li>
    </ul>
  </div>
</div>
<script type="text/javascript">
     function fbs_click() {
         u = location.href; t = document.title; window.open('http://www.facebook.com/sharer.php?u=' + encodeURIComponent(u) +
                        '&t=' + encodeURIComponent(t), 'sharer', 'toolbar=0,status=0,width=626,height=436');
         return false;
     }
                </script> 
<script type="text/javascript">!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script> 
