<%@ Control Language="C#" AutoEventWireup="true" CodeFile="topheadermenu.ascx.cs" Inherits="Themes_FirstTheme_LayoutControls_topheadermenu" %>

<nav id="mobile-lateral-nav" class="mCustomScrollbar">
<div class="mobile-nav">
	<div class="mobile-search" >
    <asp:TextBox ID="txtsearch" runat="server" placeholder="Movie, Genre, Actor" MaxLength="35" CssClass="mobile-search-box"
                TabIndex="100" onkeydown="javascript:if ((event.which &amp;&amp; event.which == 13) || (event.keyCode &amp;&amp; event.keyCode == 13)) {document.forms[0].elements['menu1_btnsearch'].click();return false;} else return true;" ></asp:TextBox>
    <asp:Button ID="btnsearch" runat="server" CssClass="mobile-search-btn" Text="Search" 
                ToolTip="Search" OnClick="btnsearch_Click"  TabIndex="101" />
    <br />
    <div class="searcherror"> </div>
  </div>
	<div class="mobile-signin">
    <ul>
      <%if (!Page.User.Identity.IsAuthenticated) { %>
      <li><a href='ReturnUrl("sitepathmain")login' title="SIGN IN">Welcome Guest! | Sign In</a> </li>
      <%}%>
      <%if (Page.User.Identity.IsAuthenticated) { %>
      <div runat="server" id="sign_out">
        <li>
          <div class="mobile-accountdropdown"> <a class="mobile-accountlogout"><span>
            <div class="mobile-loginname"> Hi
              <asp:Label ID="lblfirstname" runat="server" Text=""></asp:Label>
            </div>
            </span></a>
            <div class="mobile-submenu" style="display:none">
              <ul class="dropdownmenu">
                   <li><a title="View Profile" id="lnkprofile" runat="server">View Profile</a></li>
              <li><a href="ReturnUrl("sitepathmain")procs/editprofile"
                                        title="Edit Profile">Edit Profile</a></li>
                                    <li><a href="ReturnUrl("sitepathmain")procs/changepassword"
                                        title="Change Password">Change Password</a></li>
                 <li><a href="ReturnUrl("sitepathmain")procs/wishlist"
                                        title="Favorites">Favorites</a></li>
                                         <li><a href="ReturnUrl("sitepathmain")procs/preference"
                                        title="Preference">Preference</a></li>
                                    <li><a href="ReturnUrl("sitepathmain")recommend"
                                        title="Recommendation">Recommendation</a></li>
                                                                           
                                    <li id="lihistory" runat="server"><a href="ReturnUrl("sitepathmain")subscriptionhistory"
                                        title="Subscription History">Subscription History</a></li>
                                          <li><a href="ReturnUrl("sitepathmain")procs/pthistory"
                                        title="Reward Points">Reward Points</a></li>

                                         <li><a href="http://blog.Intranet.com/"
                                        title="Blog" target="_blank">Blog</a></li>

                                         <li><a href="http://forum.Intranet.com/"
                                        title="Forum" target="_blank">Forum</a></li>
                <li>
                  <asp:LinkButton ID="btnSingOut" runat="server" CommandName="Logout" ToolTip="Logout" Text="Logout" OnClick="btnSingOut_Click"> </asp:LinkButton>
                </li>
              </ul>
            </div>
          </div>
        </li>
        <li class="productID_1" id="divs" runat="server" visible="false" >
          <asp:Label ID="lblcarttotal1" runat="server"></asp:Label>
        </li>
      </div>
      <%}%>
    </ul>


  </div>
  <div id="mobile" runat="server"></div>
  </div>
</nav>
