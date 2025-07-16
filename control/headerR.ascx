<%@ Control Language="C#" AutoEventWireup="true" CodeFile="headerR.ascx.cs" Inherits="control_header" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/megamenu.ascx" TagPrefix="ucmega" TagName="megamenu" %>

 <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
 <script src="<%=ReturnUrl("sitepath")%>js/dropdown/jsframework.js" type="text/javascript"></script>

 <script src="<%=ReturnUrl("sitepath")%>js/dropdown/combinedefault.js" type="text/javascript"></script>
 <script src="<%=ReturnUrl("sitepath")%>js/dropdown/script.js" type="text/javascript"></script>
 <script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>


<header id="main-header" class=" has-user ">
    <nav id="navbar" class="has_fixed_navbar">
        <%--JAYESH ADDED BELOW CODE TO DISPLAY CORNER IMAGE IN HEADER 3oct2017--%>
        <div id="nav-left">
 <div class="corner_image">
<a href="#">
<%--<img class="corner" src="http://localhost/hrms/images/profile/corner5.png" alt="" title="">--%>
</a></div>
         <%--JAYESH ADDED ABOVE CODE TO DISPLAY CORNER IMAGE IN HEADER 3oct2017--%>

            <!-- NAVIGATION TOGGLE -->
            <%--<a href="javascript:void(0)" id="nav-trigger"><i class="fa fa-long-arrow-left"></i></a>--%>
            <!-- START LOGO -->
            <div id="nav-logo">

           <%--SAGAR ADDED THIS CODE FOR ADDING IMAGE(LOGO) 19 SEPT 2017--%>
                <%--<a href='<%=ReturnUrl("sitepathmain") %>default.aspx'>--%>
                <a href='<%=ReturnUrl("sitepathmain") %>procs/RecruitmentsPositions.aspx'>
      <%--<span class="z"> <img src="<%=ReturnUrl("sitepathmain") %>images/profile/hcclogo.png" /></span>--%>
                    <span class="z"> <img src="<%=ReturnUrl("sitepathmain") %>images/profile/Technocrat_Logo.png" /></span>

               
                <%--SAGAR COMMENTED THIS FOR REMOVING PROJRCT LABEL 19SEPT2017 STARTS HERE--%>
<%--                <a href='<%=ReturnUrl("sitepathmain") %>default.aspx'>--%>  <%--<%--THIS LINE IS USED FOR GIVING LINK FOR LABEL--%>
                    <%--<span class="logoname"><span class="z"><%=ConfigurationManager.AppSettings["IntranetName"]%></span></span>
                </a>--%>
                <%--SAGAR COMMENTED THIS FOR REMOVING PROJRCT LABEL 19SEPT2017 ENDS HERE

                <%--<div id="elementtoScrollToID"></div>--%>
            </div>
           

            <!-- USER INFORMATIONS -->
<div id="navuser" class="clearfix navuser bp_is_active" runat="server" visible="false">

<%--    JAYESH COMMENTED BELOW CODE TO hide profile of user and comment strong tag 12oct2017--%>
            <%--<div id="nm" class="mm" runat="server" visible="false">--%>
<a id="user-thumb" href="javascript:void(0);"><%--<strong>--%>
        <%--    JAYESH COMMENTED ABOVE CODE TO hide profile of use 12oct2017--%>

  <%--JAYESH ADDED BELOW LINE a href="#" TEMPORARY 12oct2017--%>
   <%--  <a href="#">--%><asp:Label ID="lblname" CssClass="profilename" runat="server" Text="User"></asp:Label><%--</a>   --%>  <%--JAYESH ADDED ABOVE LINE a href="#" TEMPORARY 12oct2017--%>
<%--</strong>--%>

                   <%-- <asp:Image ID="profileimg" runat="server" CssClass="avatar user-3-avatar avatar-96 photo" />--%>
                </a>
                <a href="javascript:void(0)" id="user-close">
                    <i class="fa fa-arrow-circle-o-right"></i>
                </a>
            </div>
        </div>

        <!-- EXTRA BUTTONS ABOVE THE SIDBAR -->


        <div>
        <%--<div id="nav-buttons">--%>
      

           <%-- sagar commnted this for removing sidebar 18aept2017 starts here--%>
            <!-- SIDEBAR TOGGLE -->
          <%--  <a href="javascript:void(0)" id="nav-sidebar-trigger"><i class="fa fa-long-arrow-right"></i></a>--%>
              <%--sagar commnted this for removing sidebar 18aept2017 starts here--%>

            <!-- SEACRH FORM -->
          

            <%if (Page.User.Identity.IsAuthenticated)
              { %>
            <a id="navchattrigger" runat="server" title="Go To Chat" class="" target="_blank">
               
               <%--  sagar commented this starts here 18sept2017 for removing chat --%>
                <%--<i class="fa fa-commenting-o"></i>--%>
               <%-- <span id="lblchatcount">0</span>           --%> </a>
           <%-- <a href="javascript:void(0)" id="nav-notification-trigger" title="View your notifications" class="">--%>
               <%-- <i class="fa fa-bell-o"></i>--%>
        <%--    sagar commented this starts here 18sept2017 for removing chat --%>

                <asp:Label ID="notification_count" runat="server" CssClass="notification_count" ></asp:Label>
                <asp:HiddenField ID="hdcount" runat="server" Value="0" />
            </a>

            <%--  sagar commented this starts here 18sept2017  --%>
           <%-- <a href="javascript:void(0)" id="nav-message-trigger" title="View Messages" class="">--%>
                <%--<i class="fa fa-envelope-o"></i>--%>
            <%--    sagar commented this starts here 18sept2017 --%>

                <asp:Label ID="msg_count" runat="server" CssClass="notification_count"></asp:Label>
                <asp:HiddenField ID="msghdcount" runat="server" Value="0" />
            </a>

          <%--  sagar commented this line 18sept2017--%>
           <%-- <a href="javascript:void(0)" id="nav-links-trigger" title="External Links">--%><%--<i class="fa fa-external-link"></i>--%></a>
            <%}%>

            <%if (!Page.User.Identity.IsAuthenticated)
              { %>
            <a href='<%=ReturnUrl("sitepathmain") %>loginR' title="Login"><%--<i class="fa fa-login"></i>--%></a>
            <%}%>
        </div>

    </nav>
    <!-- HIDDEN PARTS TRIGGERED BY JAVASCRIPT -->
    <!-- START USER LINKS - WAITING FOR FIRING -->

<!-- START menu - sagar and prajyot   <a href='<%=ReturnUrl("sitepathmain") %>default.aspx'>-->
<div class="megamenu">
        
    <a href="http://localhost/hrms/procs/RecruitmentsPositions.aspx">
	<img src="http://localhost/hrms/images/homepage_imgs/home.png"   class="tttt"/>
    </a>
    
</div>

     
   
    <div id="user-sidebar">  
        <div class="text-birth1 mCustomScrollbar" style="overflow-x:hidden !important;">
        <header id="user-cover">
            <a href="#" class="clearfix" id="A1" runat="server">
                
                <asp:Image ID="Image1" runat="server" CssClass="avatar user-3-avatar avatar-96 photo" />
                <%--<span id="labl">Welcome--%>
                    <asp:Label ID="Label1" runat="server" CssClass="woffice-welcome" Text="User"></asp:Label>
               <%-- </span>--%>

            </a>
            <div class="user-cover-layer"></div>

        </header>
        <nav>
            <ul id="menu-bp" class="menu">
                <li id="activity-personal-li" class="menu-parent">
                    <a href="#">Activity </a>
                    <ul class="sub-menu">
                        <li id="rating-me-personal-li" class="menu-child">
                            <a href="#" id="lnkrating" runat="server">Ratings & Reviews</a>
                        </li>
                        <li id="following-personal-li" class="menu-child">
                            <a href="#" id="lnkfollowing" runat="server">Following</a>
                        </li>
                        <li id="follower-personal-li" class="menu-child">
                            <a href="#" id="lnkfollower" runat="server">Follower</a>
                        </li>
                        <li id="activity-favs-personal-li" class="menu-child">
                            <a href="#" id="lnkfavorites" runat="server">Favorites</a>
                        </li>
                    </ul>
                </li>
                <div id="admin" runat="server" visible="false">
                 <li id="liadmin">
                      <asp:LinkButton ID="lnkadmin" runat="server" ToolTip="Admin Login" Text="Admin" OnClick="lnkadmin_Click" OnClientClick="document.forms[0].target = '_blank';"> </asp:LinkButton>
                 </li>
                </div>
                <li id="xprofile-personal-li" class="menu-parent">
                    <a href="#">Profile </a>
                    <ul class="sub-menu">
                        <li id="public-personal-li" class="menu-child">
                            <a href="#" id="lnkviewprofile" runat="server">View</a>
                        </li>
                        <li id="edit-personal-li" class="menu-child">
                            <a href="#" id="lnkeditprofile" runat="server">Edit</a>
                        </li>
                        <li id="edit-settings-li" class="menu-child">
                            <a href='<%=ReturnUrl("sitepathmain") %>procs/settings.aspx'>Settings</a>
                        </li>
                        <li id="edit-password-li" class="menu-child">
                            <a id="lnkpass" runat="server" target="_blank">Change Password</a>
                        </li>
                        <li id="edit-question-li" class="menu-child">
                            <a id="lnkqst" runat="server" target="_blank">Security Questions</a>
                        </li>
                    </ul>
                </li>

                <li id="messages-personal-li" class="menu-parent">
                    <a href="#">Messages </a>
                    <ul class="sub-menu">
                        <li id="compose-personal-li" class="menu-child">
                            <a href='<%=ReturnUrl("sitepathmain") %>composemail.aspx'>Compose</a>
                        </li>
                        <li id="inbox-personal-li" class="menu-child">
                            <a href='<%=ReturnUrl("sitepathmain") %>inbox.aspx'>Inbox</a>
                        </li>

                        <li id="sentbox-personal-li" class="menu-child">
                            <a href='<%=ReturnUrl("sitepathmain") %>outbox.aspx'>Sent</a>
                        </li>


                    </ul>
                </li>


                <li id="logout-li">
                    <asp:LinkButton ID="btnSingOut" runat="server" CommandName="Logout" ToolTip="Logout" Text="Logout" OnClick="btnSingOut_Click"> </asp:LinkButton></li>
            </ul>
        </nav>
        </div>
     </div>

    <div id="user-sidebar">  
        <div class="text-birth1 mCustomScrollbar" style="overflow-x:hidden !important;">
        <header id="user-cover">
            <a href="#" class="clearfix" id="lnkprof" runat="server">
                
                <asp:Image ID="imgbigpic" runat="server" CssClass="avatar user-3-avatar avatar-96 photo" />
                <%--<span id="labl">Welcome--%>
                    <asp:Label ID="lblfname" runat="server" CssClass="woffice-welcome" Text="User"></asp:Label>
               <%-- </span>--%>

            </a>
            <div class="user-cover-layer"></div>

        </header>
       
        </div>
     </div>
   
    <div id="woffice-notifications-menu">
        <asp:Panel ID="notificationsbody" runat="server" CssClass="mscrollbar">
            <asp:Repeater ID="rptnotification" runat="server" OnItemCommand="rptnotification_ItemCommand">
                <ItemTemplate>
                    <div class="textwidget bd_birth">
                        <asp:Label runat="server" ID="follow" Text='<%# Eval("id")%>' Visible="false"></asp:Label>
                         <asp:Label runat="server" ID="indexid" Text='<%# Eval("indexid")%>' Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblpid" Text='<%# Eval("productid")%>' Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblevent" Text='<%# Eval("eventflag")%>' Visible="false"></asp:Label>
                        <asp:LinkButton ID="lnkuser" runat="server" CommandName="cmdnotify" OnClick="lnkuser_Click">
                            <asp:Image ID="imgprofile" runat="server" /><span class="notify_title"><%# Eval("fullname")%> <%# Eval("notifytext")%>&nbsp;<%# Eval("ptitle")%>.</span>
                            <asp:Label ID="lblflag" runat="server" Text='<%# Eval("readflag")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lbldate" runat="server" CssClass="birth_date"></asp:Label>
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
        <div class="divnotify" id="divallnotify" runat="server">
            <a href="<%=ReturnUrl("sitepathmain") %>notification.aspx" title="See All Notifications" class="viewall" style="">See All Notifications</a>
        </div>
        <div class="textwidget bd_birth" id="divmsg" runat="server" visible="false">
            <p class="woffice-notification-empty">You have <b>0</b> unread notifications.</p>
        </div>

    </div>

    <div id="woffice-message-menu">
        <asp:Panel ID="pnkmsg" runat="server" CssClass="mscrollbar">
            <asp:Repeater ID="rptmsg" runat="server" OnItemCommand="rptmsg_ItemCommand">
                <ItemTemplate>
                    <div class="textwidget bd_birth">
                        <asp:Label runat="server" ID="lblmsgid" Text='<%# Eval("id")%>' Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblevent" Text='<%# Eval("eventflag")%>' Visible="false"></asp:Label>
                        <asp:LinkButton ID="lnkuser" runat="server" CommandName="cmdnotify" OnClick="lnkuser1_Click">
                            <asp:Image ID="imgprofile" runat="server" /><span class="notify_title"><%# Eval("fullname")%></br> <%# Eval("msgtext")%>.</span>
                            <asp:Label ID="lblflag" runat="server" Text='<%# Eval("readflag")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lbldate" runat="server" CssClass="birth_date"></asp:Label>
                        </asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
        <div class="divnotify" id="msgtitleall" runat="server">
            <a href="<%=ReturnUrl("sitepathmain") %>inbox.aspx" title="See All Notifications" class="viewall" style="">See All Messages</a>
        </div>
        <div class="textwidget bd_birth" id="divmsg1" runat="server" visible="false">
            <p class="woffice-notification-empty">You have <b>0</b> unread messages.</p>
        </div>
    </div>

    <div id="woffice-links-menu">
        <asp:Label ID="lbllnkmsg" runat="server" Text="No Records Found" Visible="false" Font-Size="18px"></asp:Label>
        <div class="mscrollbar" id="topheadpromo" runat="server">
            <ul class="promo-container" style="left: 0">
                <asp:Repeater ID="rptpromo" runat="server">
                    <ItemTemplate>
                        <li><a href='<%# Eval("url")%>' title='<%# Eval("altname") %>' target='<%# Eval("target") %>' id="promolink" runat="server">
                            <img src='<%=ConfigurationManager.AppSettings["sitepathadmin"]%>images/banner/<%# Eval("imagename") %>' alt="" class="mCS_img_loaded1" /></a>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>

    <div id="woffice-chat-menu">
        <asp:Panel ID="pnlchat" runat="server" CssClass="mscrollbar">
            <ul class="chat-container">
            </ul>
        </asp:Panel>
                <div class="divnotify" id="divallchat" runat="server">
            <a id="lnkchatlink" runat="server" title="Go To Chat" class="viewall" target="_blank">Go To Chat</a>
        </div>
        <asp:HiddenField ID="hduserid" runat="server" />
        <asp:HiddenField ID="hduserchat" runat="server" />
    </div>
    <%--Code Added for megamenu in top header by hameed--%>
     <%--   <ucmega:megamenu runat="server" id="megamenu" />--%>

    <!-- START SEARCH CONTAINER - WAITING FOR FIRING -->
    <div id="main-search">
        <div class="container">
            <div class="search-form">
                <input type="text" value="" name="s" id="s" placeholder="Search..." />
                <input type="hidden" name="searchsubmit" id="searchsubmit" value="true" />
                <button type="submit" name="searchsubmit"><i class="fa fa-search"></i></button>
                <div>
                </div>
            </div>
        </div>
    </div>
</header>
