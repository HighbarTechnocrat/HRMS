<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustSheader.ascx.cs" Inherits="control_CustSheader" %>
<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/dropdown/jsframework.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/dropdown/combinedefault.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/dropdown/script.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>
<style>
    /* OUTER CONTAINER */
    .tcontainer {
        width: 100%;
        overflow: hidden; /* Hide scroll bar */
        margin: 2% 0 0 0 !important;
    }

    /* MIDDLE CONTAINER */
    .ticker-wrap {
        width: 100%;
        padding-left: 100%; /* Push contents to right side of screen */
        background-color: #3D1956;
        /*color:#F28820;*/
        color: white;
    }

    /* INNER CONTAINER */
    @keyframes ticker {
        0% {
            transform: translate3d(0, 0, 0);
        }

        100% {
            transform: translate3d(-100%, 0, 0);
        }
    }

    .ticker-move {
        /* Basically move items from right side of screen to left in infinite loop */
        display: inline-block;
        white-space: nowrap;
        padding-right: 100%;
        animation-iteration-count: infinite;
        animation-timing-function: linear;
        animation-name: ticker;
        animation-duration: 10s;
    }

        .ticker-move:hover {
            animation-play-state: paused; /* Pause scroll on mouse hover */
        }

    /* ITEMS */
    .ticker-item {
        display: inline-block; /* Lay items in a horizontal line */
        padding: 0 1rem;
        font-size: 10pt;
    }
    .cornerOnehr {
    /* height: 109px; */
    float: left;
    margin: 0px 75px 0 100px !important;
    height: 70px !important;
}
</style>

<header id="main-header" class=" has-user ">

    <nav id="navbar" class="has_fixed_navbar">
        <div id="nav-left">
            <div class="corner_image" style="display:inline">
                <a href="#">
                    <img class="cornerOnehr" src="<%=ReturnUrl("sitepathmain") %>images/profile/CustomerFirst_Logo.png" alt="" title="">
                </a>
            </div>

            <div id="nav-logo">             
                 
                    <span class="z">
                        <img src="<%=ReturnUrl("sitepathmain") %>images/profile/Technocrat_Logo.png" /></span>
            </div>


            <!-- USER INFORMATIONS -->
            <div id="navuser" class="clearfix navuser bp_is_active" runat="server" visible="false">
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
          

        <div>

            <%if (Page.User.Identity.IsAuthenticated)
                { %>
            <asp:Label ID="notification_count" runat="server" CssClass="notification_count"></asp:Label>
            <asp:HiddenField ID="hdcount" runat="server" Value="0" />
            <asp:Label ID="msg_count" runat="server" CssClass="notification_count"></asp:Label>
            <asp:HiddenField ID="msghdcount" runat="server" Value="0" />

            <%}%>

            <%if (!Page.User.Identity.IsAuthenticated)
                { %>
            <a href='<%=ReturnUrl("sitepathmain") %>login' title="Login"><%--<i class="fa fa-login"></i>--%></a>
            <%}%>
        </div>


    </nav>


    <div class="megamenu">
        <a href="http://localhost/hrms/procs/Custs_MyServiceReq.aspx">
            <img src="http://localhost/hrms/images/homepage_imgs/home.png" class="tttt" />
        </a>
        <a href="http://localhost/hrms/CustSlogout.aspx">
            <img src="http://localhost/hrms/images/homepage_imgs/logout (1).png" title="Logout" class="logout" />
        </a> 
    </div>   

    <asp:HiddenField ID="hduserid" runat="server" />
    <asp:HiddenField ID="hduserchat" runat="server" />

   
</header>
