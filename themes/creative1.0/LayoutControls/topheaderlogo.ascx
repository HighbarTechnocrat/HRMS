<%@ Control Language="C#" AutoEventWireup="true" CodeFile="topheaderlogo.ascx.cs"
    Inherits="Themes_FirstTheme_LayoutControls_topheaderlogo" %>
<style>
    .notificationdiv {
        color: #fff;
        float: right;
        margin: 0 5px 0 20px;
        padding: 11px 0 12px;
    }

    .user-profile-icon {
        color: #fff;
        float: right;
        margin: 0 5px 0 20px;
        padding: 11px 0 12px;
    }

    #notificationcontainer {
        overflow: visible;
        width: 250px;
        z-index: -1;
        display: none;
        margin: 50px 0 0 -250px;
        float: right;
    }
#bannermenucontainer {
  float: right;
  margin: 50px 0 0;
  overflow: visible;
  position: absolute;
  right: 190px;
  width: 250px;
  z-index: -1;
}
    .bannermenumaindiv {
        position: relative;
        right:0;
        background: #000;
    }
     #bannermenutitle {
        z-index: 1000;
        font-weight: bold;
        padding: 0px 0 8px;
        font-size: 14px;
        background-color: #000000;
        width: 100%;
        color: #fff;
        text-transform: uppercase;
        text-align: center;
        border-bottom: 1px solid #353535;
    }
    .notificationmaindiv {
        position: relative;
        right: -163px;
        background: #000;
    }

    #notificationtitle {
        z-index: 1000;
        font-weight: bold;
        padding: 0px 0 8px;
        font-size: 14px;
        background-color: #000000;
        width: 100%;
        color: #fff;
        text-transform: uppercase;
        text-align: center;
        border-bottom: 1px solid #353535;
    }

    #notificationsbody {
        min-height: 100px;
        height: 158px;
        overflow-y: auto;
    }

        #notificationsbody a {
            text-decoration: none;
        }

    .notification_count {
        padding: 1px 5px;
        background: #2E85DC;
        color: #fff;
        font-weight: bold;
        margin-left: 11px;
        border-radius: 50%;
        position: absolute;
        z-index: 999;
        font-size: 11px;
    }

    #imgprofile {
        width: 110;
        height: 110;
    }

    .fa.fa-user {
        margin-top: 3px !important;
    }

    .user-profile-icon > a > i {
        color: #fff;
        font-size: 19px;
        outline: none;
    }

    .user-profile-icon > a:hover > i {
        color: #287FE1;
    }

    #notificationlink {
        color: #fff;
        font-size: 19px;
        outline: none;
    }

    #bannermenu {
        color: #fff;
        padding: 0 0 0 20px;
        font-size: 19px;
        outline: none;
    }

    .notifications {
        max-height: 154px;
        min-height: 80px;
    }

    .notification_atag {
        text-decoration: none;
    }

    .notifications-detail {
        padding: 10px;
        float: left;
        width: 92%;
        border-bottom: 1px solid #353535;
        background: #f1f1f1;
    }

    .notifications-active {
        background: #222222;
    }

    .notificationsdiv {
        float: left;
        width: 68%;
        margin: 0 0 0 15px;
    }

    .notifications-heading {
        font-size: 13px;
        font-weight: 600;
        float: left;
        width: 100%;
        margin: 0 0 5px 0;
        color: #ffffff;
        text-decoration: none;
    }

        .notifications-heading:hover {
            color: #2E85DC;
        }

    .notifications-photo {
        float: left;
    }

        .notifications-photo img {
            width: 50px;
            height: 50px;
            border-radius: 50%;
            border: 3px solid #505050;
        }

    .notification-date {
        float: left;
        color: #808080;
        font-size: 12px;
        font-weight: 300;
        width: 100%;
    }

    .no-notifications {
        font-size: 15px;
        font-style: italic;
        color: #ccc;
        text-align: center;
        padding: 70px 0;
    }

    @media only screen and (max-width:1000px) and (min-width:240px) {
        .notificationdiv {
            margin: 0 62px 0 0;
        }

        .notificationmaindiv {
            right: -81px;
        }

        .user-profile-icon {
            margin: 0 25px 0 0;
        }
    }

    @media screen and (-webkit-min-device-pixel-ratio:0) {
        #notificationtitle, .notifications-heading {
            font-weight: 400;
        }
    }
</style>

<div class="middleheader">
    <div class="middleheaderContainer">
    <div class="sitelogodiv"> <a href='ReturnUrl("sitepathmain")default' title="Intranet Portal"> <span id="spanlogo"  runat="server"></span></a> </div>
    <div class="upperheaderright">
      <ul class="loginaccount" id="ulogin" runat="server">
        <%if (!Page.User.Identity.IsAuthenticated)
          { %>
        <li><a href='ReturnUrl("sitepathmain")login' title="Sign Up/Login"> Sign Up / Login</a> </li>
        <%}%>
      </ul>
      <ul class="account">
        <%if (Page.User.Identity.IsAuthenticated)
          { %>
        <div runat="server" id="sign_out">
          <li>
            <div class="accountdropdown"> <a class="accountlogout" ><span>
              <div class="loginname"> Hi
                <asp:Label ID="lblfirstname" runat="server" Text=""></asp:Label>
              </div>
              </span></a>
              <div class="submenu">
                <ul class="dropdownmenu">
                    <li><a title="View Profile" id="viewprofile" runat="server">View Profile</a></li>
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
                  <li id="lihistory" runat="server"><a href="ReturnUrl("sitepathmain")procs/subscriptionhistory"
                                        title="Subscription History">Subscription History</a></li>
                  <li><a href="ReturnUrl("sitepathmain")procs/pthistory"
                                        title="Reward Points">Reward Points</a></li>
                  <li><a href="http://blog.muvizz.com/"
                                        title="Blog" target="_blank">Blog</a></li>
                  <li><a href="http://forum.muvizz.com/"
                                        title="Forum" target="_blank">Forum</a></li>
                  <li>
                    <asp:LinkButton ID="btnSingOut" runat="server" CommandName="Logout" ToolTip="Logout"
                                            Text="Logout" OnClick="btnSingOut_Click"> </asp:LinkButton>
                  </li>
                </ul>
              </div>
            </div>
          </li>
          <li class="productID_1" id="divs" runat="server" visible="false">
            <asp:LinkButton ID="LinkButton1" runat="server" class="cartimg" title="Shopping Cart"
                            OnClick="LinkButton1_Click"></asp:LinkButton>
            <asp:Label ID="lblcarttotal1" runat="server"></asp:Label>
          </li>
        </div>
        <%}%>
      </ul>
    </div>

    <div class="notificationdiv" id="divnotify" runat="server">
      <a href="#" id="notificationlink" Style="text-decoration:none;">
 		<asp:Label ID="notification_count" runat="server" CssClass="notification_count"></asp:Label><i class="fa fa-bell"></i> </a>
        <a href="#" id="bannermenu" Style="text-decoration:none;"><i class="fa fa-list"></i> </a>
    </div>
    <div id="notificationcontainer">
    	<div class="notificationmaindiv">
        	<div id="notificationtitle">Notifications</div>
        	<div id="divmsg" runat="server" visible="false" style="text-align: center; padding: 15px;">No New Notification</div>
        	<div id="notificationsbody" class="notifications mCustomScrollbar" runat="server">
          <asp:Repeater ID="rptnotification" runat="server" OnItemCommand="rptnotification_ItemCommand">
            <ItemTemplate>
                <asp:Label runat="server" ID="follow" Text='<%# Eval("id")%>' Visible="false"></asp:Label>
              <asp:LinkButton ID="lnkuser" runat="server" CommandName="cmdnotify" CssClass="notification_atag" OnClick="lnkuser_Click">
                <div class="notifications-detail notifications-active">
                  <div class="notifications-photo">
                    <asp:Image ID="imgprofile" runat="server" />
                  </div>
                  <div class="notificationsdiv">
                    <div class="notifications-heading"> <%# Eval("fullname")%> <%# Eval("notifytext")%> . </div>
                    <div class="notification-date">
                      <asp:Label ID="lblflag" runat="server" Text='<%# Eval("readflag")%>' Visible="false"></asp:Label>
                      <asp:Label ID="lbldate" runat="server"></asp:Label>
                    </div>
                  </div>
                </div>
              </asp:LinkButton>
            </ItemTemplate>
          </asp:Repeater>
        </div>
        </div>
      </div>
    <div id="bannermenucontainer">
    	<div class="bannermenumaindiv">
        	<div id="bannermenutitle"></div>
        	
            </div>
              </div>
    <div class="user-profile-icon">
<a id="lnkprofile" runat="server" title="View Profile"><i class="fa fa-user"></i></a>

</div>
    <div class="sitesearch" id="divtopsearch" runat="server" visible="true">
      <asp:TextBox ID="txtsearch" runat="server" placeholder="Search" MaxLength="35"
                TabIndex="100" onkeydown="javascript:if ((event.which &amp;&amp; event.which == 13) || (event.keyCode &amp;&amp; event.keyCode == 13)) {document.forms[0].elements['topseach_m_uxlogopanel_btnsearch'].click();return false;} else return true;"></asp:TextBox>
      <asp:Button ID="btnsearch" runat="server" CssClass="searchbtn" Text="Search" ToolTip="Search"
                OnClick="btnsearch_Click" TabIndex="101" />
      <br />
    </div>
    <div class="navigationdesktop">
      <div id="desktop" runat="server"> </div>
    </div>
</div>
</div>
<div class="overlay"></div>
<script src="ReturnUrl("sitepath")/js/jsframework.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#notificationlink").click(function () {
            $("#notificationcontainer").fadeToggle(300);
            return false;
        });
        $(document).click(function () {
            $("#notificationcontainer").hide();
        });

    });
</script>
