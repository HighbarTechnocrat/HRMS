<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="control_header" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/megamenu.ascx" TagPrefix="ucmega" TagName="megamenu" %>
<%-- <script src="//code.jquery.com/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
<%--<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/megamenu.js" type="text/javascript"></script>--%>
<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery-1.4.1.min.js" type="text/javascript"></script>

<%-- <link href="http://csia.in.iis3004.shared-servers.com/CSS/creative1.0/includes/menu.css" rel="stylesheet" type="text/css" />--%>

<%-- <link href="<%=ReturnUrl("sitepath")%>js/dropdown/menu.css" rel="stylesheet" type="text/css" />--%>
<script src="<%=ReturnUrl("sitepath")%>js/dropdown/jsframework.js" type="text/javascript"></script>
<%--<script src="http://csia.in.iis3004.shared-servers.com/themes/creative1.0//js/jsframework.js"></script>--%>
<script src="<%=ReturnUrl("sitepath")%>js/dropdown/combinedefault.js" type="text/javascript"></script>
<%-- <script type="text/javascript" src="http://csia.in.iis3004.shared-servers.com/js/combinedefault.js"></script>--%>
<%--<script src="http://csia.in.iis3004.shared-servers.com/js/script.js" type="text/javascript"></script>--%>
<script src="<%=ReturnUrl("sitepath")%>js/dropdown/script.js" type="text/javascript"></script>

<%--<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/megamenu.js" type="text/javascript"></script>--%>
<script src="<%=ReturnUrl("sitepath")%>js/singleautocomplete/jquery.autocomplete.js" type="text/javascript"></script>



<%--<nav id="navigation" class="mobile-hidden">
    <ul id="main-menu" class="main-menu">--%>


<%-- SAGAR COMMENTED THIS FOR REMOVING DASHBOARD SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%--<li id="menu-item-417" class="menu-item menu-item-type-custom menu-item-object-custom current-menu-item current_page_item menu-item-home menu-item-has-icon menu-item-417"><a href="<%=ReturnUrl("sitepathmain") %>default" class="fa fa-tachometer"><span class="menu-name">Dashboard</span></a></li>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING DASHBOARD SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>


<%-- SAGAR COMMENTED THIS FOR REMOVING ADD POST SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%--<li id="menu-item-428" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon menu1">
            <a href="#" class="fa fa-plus"><span class="menu-name">Add Post</span></a>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING ADD POST SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>

<%--   SAGAR COMMENTED THIS FOR REMOVING LOGIC OF ADD-POST 21SEPT2017 STARTS HERE--%>
<%--<ul class="sub-menu sub-menu-has-icons mscrollbar">
                <asp:Repeater ID="rptcat" runat="server">
                    <ItemTemplate>
                        <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                            <a href='<%#getAddPostURL( Eval("categhead ooryname"),Eval("categoryid")) %>'><%#(DataBinder.Eval(Container, "DataItem.categoryname")) %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </li>--%>
<%--   SAGAR COMMENTED THIS FOR REMOVING LOGIC OF ADD-POST 21SEPT2017 ENDS HERE--%>

<%-- SAGAR COMMENTED THIS FOR REMOVING BROWSE SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%-- <li id="menu-item-438" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon menu2">
            <a href="#" class="fa fa-globe"><span class="menu-name">Browse</span></a>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING BROWSE SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>

<%--SAGAR COMMENTED THIS FOR REMOVING LOGIC OF BROWSE SECTION 21SEPT2017 STARTS HERE--%>
<%--     <ul class="sub-menu sub-menu-has-icons mscrollbar">
                <asp:Repeater ID="rptcats" runat="server">
                    <ItemTemplate>
                        <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                            <a href='<%#getcategoryURL( Eval("categoryname"),Eval("categoryid")) %>'><%#(DataBinder.Eval(Container, "DataItem.displaycatname")) %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </li>--%>
<%--SAGAR COMMENTED THIS FOR REMOVING LOGIC OF BROWSE SECTION 21SEPT2017 ENDS HERE--%>

<%-- SAGAR COMMENTED THIS FOR REMOVING CALENDER SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%-- <li id="menu-item-441" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon menu3">--%>
<%--<a href="#" class="fa fa-calendar">--%><%--<span class="menu-name">Calendar</span></a>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING CALENDER SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>

<%--SAGAR COMMENTED THIS FOR REMOVING LOGIC OF CALENDER SECTION 21SEPT2017 STARTS HERE--%>
<%-- <ul class="sub-menu sub-menu-has-icons">
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                    <a href="<%=ReturnUrl("sitepathmain") %>mycalendar">My Calendar</a>
                </li>
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                    <a href="<%=ReturnUrl("sitepathmain") %>eventcalendar">Event Calendar</a>
                </li>
            </ul>
        </li>--%>
<%--SAGAR COMMENTED THIS FOR REMOVING LOGIC OF CALENDER SECTION 21SEPT2017 ENDS HERE--%>

<%-- SAGAR COMMENTED THIS FOR REMOVING GROUP SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%--   <li id="menu-item-431" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon menu4">
            <a id="lnkgroup" runat="server" href="#" class="fa fa-group"><span class="menu-name">Groups</span></a></li>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING GROUP SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>

<%-- SAGAR COMMENTED THIS FOR REMOVING MEMBERS SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%--  <li class="menu-item menu-item-type-post_type menu-item-has-icon menu5">
            <a href="<%=ReturnUrl("sitepathmain") %>userdirectory.aspx" class="fa fa-user"><span class="menu-name">Members</span></a>
        </li>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING MEMBERS SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>


<%-- SAGAR COMMENTED THIS FOR REMOVING MESSAEGE SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%--        <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon menu6">
            <a href="#" class="fa fa-commenting-o"><span class="menu-name">Messages</span></a>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING MESSAGE SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>

<%--SAGAR COMMENTED THIS FOR REMOVING LOGIC OF MESSAGE SECTION 21SEPT2017 STARTS HERE--%>
<%-- <ul class="sub-menu sub-menu-has-icons">
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                    <a href="<%=ReturnUrl("sitepathmain") %>composemail.aspx">Compose</a>
                </li>
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                    <a href="<%=ReturnUrl("sitepathmain") %>inbox.aspx">Inbox</a>
                </li>
                <li class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon">
                    <a href="<%=ReturnUrl("sitepathmain") %>outbox.aspx">Sent</a>
                </li>
            </ul>
        </li>--%>
<%--SAGAR COMMENTED THIS FOR REMOVING LOGIC OF MESSAGE SECTION 21SEPT2017 ENDS HERE--%>

<%-- SAGAR COMMENTED THIS FOR REMOVING NEWS SECTION FROM FRONT END 19SEPT2017 STARTS HERE--%>
<%--        <li id="menu-item-434" class="menu-item menu-item-type-post_type menu-item-object-page menu-item-has-icon menu7">
            <a href="<%=ReturnUrl("sitepathmain") %>news.aspx" class="fa fa-newspaper-o"><span class="menu-name">News</span></a>--%>
<%-- SAGAR COMMENTED THIS FOR REMOVING NEWS SECTION FROM FRONT END 19SEPT2017 ENDS HERE--%>

<%--        </li>
    </ul>
</nav>--%>
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
</style>

<header id="main-header" class=" has-user ">
    <nav id="navbar" class="has_fixed_navbar">
        <%--JAYESH ADDED BELOW CODE TO DISPLAY CORNER IMAGE IN HEADER 3oct2017--%>

        <div id="nav-left">
            <div class="corner_image">
                <a href="#">
                    
                    <%--<img class="cornerOnehr" src="<%=ReturnUrl("sitepathmain") %>images/profile/OneHRLogo3.jpg" alt="" title="">--%>                    
                    <a href="https://www.greatplacetowork.in/great/company/highbar-technocrat-limited" target="_blank">
                        <img class="cornerOnehr" src="<%=ReturnUrl("sitepathmain") %>images/profile/OneHR_GreatePlacetowork.jpg" alt="" title="">
                    </a> 
                </a>
            </div>
            <%--JAYESH ADDED ABOVE CODE TO DISPLAY CORNER IMAGE IN HEADER 3oct2017--%>

            <!-- NAVIGATION TOGGLE -->
            <%--<a href="javascript:void(0)" id="nav-trigger"><i class="fa fa-long-arrow-left"></i></a>--%>
            <!-- START LOGO -->
            <div id="nav-logo">

                <%--SAGAR ADDED THIS CODE FOR ADDING IMAGE(LOGO) 19 SEPT 2017--%>
                <a href='<%=ReturnUrl("sitepathmain") %>default.aspx'>
                    <%--<span class="z"> <img src="<%=ReturnUrl("sitepathmain") %>images/profile/hcclogo.png" /></span>--%>
                    <span class="z">
                        <img src="<%=ReturnUrl("sitepathmain") %>images/profile/Technocrat_Logo.png" /></span>


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

            <asp:Label ID="notification_count" runat="server" CssClass="notification_count"></asp:Label>
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
            <a href='<%=ReturnUrl("sitepathmain") %>login' title="Login"><%--<i class="fa fa-login"></i>--%></a>
            <%}%>
        </div>


    </nav>
    <!-- HIDDEN PARTS TRIGGERED BY JAVASCRIPT -->
    <!-- START USER LINKS - WAITING FOR FIRING -->

    <!-- START menu - sagar and prajyot   <a href='<%=ReturnUrl("sitepathmain") %>default.aspx'>-->

    <%--<div class="tcontainer"><div class="ticker-wrap"><div class="ticker-move">
  <div class="ticker-item">You can check your PF in My Corner</div>
  <div class="ticker-item">You can check your Salary Slip in My Corner.  To change Password go in My Corner.</div>
</div></div></div>    --%>
    <div class="megamenu">

        <a href="http://localhost/hrms/default.aspx">
            <img src="http://localhost/hrms/images/homepage_imgs/home.png" class="tttt" />
        </a>
        <%--    <a href="http://localhost/hrms/logout.aspx">
	<img src="http://localhost/hrms/images/homepage_imgs/logout1.png" title="Notifications"   class="Notifications"/>
    </a>--%>
        <a href="http://localhost/hrms/logout.aspx">
            <img src="http://localhost/hrms/images/homepage_imgs/logout (1).png" title="Logout" class="logout" />
        </a>
        <%--<div id="cssmenu2" style="display:none">--%>
        <div id="cssmenu2">
            <!--SONY COMMENTED THIS TO MAKE MENU WORK ON MOBILE-->
            <!-- <div id="menu-button"></div> -->

            <ul>
                <%--<li class="has-sub"><span class="submenu-button"></span><a href="http://localhost/hrms/default.aspx" style="font-size: 14px !important;"  title="Home">Home</a>--%>
                <li class="has-sub" style="display: none"><span></span>
                    <!-- <ul class="mega-sub-menu"></ul>-->
                </li>
                <%--<li class="has-sub"><span class="submenu-button"></span><a href="http://localhost/hrms/userdirectory.aspx" style="font-size: 14px !important;"  title="People">People</a><!-- <ul class="mega-sub-menu"></ul>--></li>--%>
                <li class="has-sub"><span class="submenu-button"></span>
                    <asp:LinkButton ID="btnShowMsg" runat="server" Text="Notifications" Style="font-size: 14px !important; color: #F28820"
                        OnClick="btnShowMsg_Click" />
                    <%--<a href="http://localhost/hrms/contacts.aspx" onclick="btnShowMsg_Click" style="font-size: 14px !important;color:#F28820"  title="Notifications">Notifications</a>--%>
                    <ul class="mega-sub-menu">

                        <%-- add by Harshad --%>
                        <li id="LilblCusts_ServiceRequestHOD" runat="server" title="Pending Customer Service_Request" visible="false"><a href="http://localhost/hrms/procs/Custs_Service.aspx" runat="server" id="aEFCSRHOD" style="font-size: 12px !important;" title="Pending Customer Service_Request">Customer Service_Request</a></li>
                        <li id="liCustomerServiceRequest" runat="server" title="CustomerFIRST Service Request" visible="false"><a href="http://localhost/hrms/procs/Custs_InboxServiceRequest.aspx" runat="server" id="ACustomerServiceRequest" style="font-size: 12px !important;" title="CustomerFIRST Service Request">CustomerFIRST Service Request</a></li>
                        <li id="LilblCusts_ServiceRequest" runat="server" title="Pending Customer Service_Request" visible="false"><a href="http://localhost/hrms/procs/Custs_InboxServiceRequest.aspx" runat="server" id="aEFCSR" style="font-size: 12px !important;" title="Pending Customer Service_Request">Customer Service_Request</a></li>
                        <li id="liCustomerServiceRequest_CS" runat="server" title="CustomerFIRST Service Request" visible="false"><a href="http://localhost/hrms/procs/Custs_InboxPendingServiceRequest.aspx" runat="server" id="aCSPending" style="font-size: 12px !important;" title="CustomerFIRST Service Request">CustomerFIRST Service Request</a></li>
                        <%-- add by Harshad --%>

                        <li id="lilblMsg" runat="server" title="Leave" visible="false"><a href="http://localhost/hrms/procs/leaves.aspx" runat="server" id="aL" style="font-size: 12px !important;" title="Leave">Leave</a></li>
                        <li id="lilblMsg_Mob" runat="server" title="Mobile" visible="false"><a href="http://localhost/hrms/procs/Mobile.aspx" runat="server" id="aM" style="font-size: 12px !important;" title="Mobile">Mobile</a></li>
                        <li id="lilblMsg_Fuel" runat="server" title="Fuel" visible="false"><a href="http://localhost/hrms/procs/fuel.aspx" runat="server" id="aF" style="font-size: 12px !important;" title="Fuel">Fuel</a></li>
                        <li id="lilblMsg_Pay" runat="server" title="Payment" visible="false"><a href="http://localhost/hrms/procs/Voucher.aspx" runat="server" id="aP" style="font-size: 12px !important;" title="Payment">Payment</a></li>
                        <li id="lilblMsg_Travel" runat="server" title="Travel" visible="false"><a href="http://localhost/hrms/procs/travelindex.aspx" runat="server" id="aE" style="font-size: 12px !important;" title="Travel">Travel</a></li>
                        <li id="lilblServiceRequest" runat="server" title="EmployeeFIRST" visible="false"><a href="http://localhost/hrms/procs/InboxServiceRequest.aspx" runat="server" id="aEF" style="font-size: 12px !important;" title="EmployeeFIRST">EmployeeFIRST</a></li>
                        <li id="lilblCustomerFIRST" runat="server" title="Pending Customer Feedback Response" visible="false"><a href="http://localhost/hrms/procs/InboxCustomerFirst.aspx" runat="server" id="aEF1" style="font-size: 12px !important;" title="Pending Customer Feedback Response">Customer Feedback</a></li>
                        <li id="lilblAttedanceRegularization" runat="server" title="Pending Attedance Regularization" visible="false"><a href="http://localhost/hrms/procs/InboxAttend_Req.aspx" runat="server" id="aEF2" style="font-size: 12px !important;" title="Pending Attedance Regularization">Attedance Regularization</a></li>
                        <li id="lilblTimesheet" runat="server" title="Pending Timesheet" visible="false"><a href="http://localhost/hrms/procs/InboxTimesheet_Req.aspx" runat="server" id="aEF3" style="font-size: 12px !important;" title="Pending Timesheet">Timesheet</a></li>
                        <li id="lilblITAsset" runat="server" title="Pending ITInventory Response" visible="false"><a href="http://localhost/hrms/procs/InboxITAssetServiceRequest.aspx" runat="server" id="aIT1" style="font-size: 12px !important;" title="Pending IT Asset Request">IT-Inventory</a></li>
                        <li id="liRecruit_Req_APP" runat="server" title="Requisition Approval" visible="false"><a href="http://localhost/hrms/procs/Req_RequisitionIndex.aspx?itype=Pending" runat="server" id="ARECRQ" style="font-size: 12px !important;" title="Requisition Approval">Requisition Approval</a></li>
                        <li id="liRecruiter" runat="server" title="Recruiter Inbox" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterInbox.aspx?type=InRec" runat="server" id="ARecruiter" style="font-size: 12px !important;" title="Recruiter Inbox">Recruiter Inbox</a></li>
                        <li id="liScreener" runat="server" title="Screener Inbox" visible="false"><a href="http://localhost/hrms/procs/Rec_InterviewerInbox.aspx?type=InPInter" runat="server" id="AScreener" style="font-size: 12px !important;" title="Screener Inbox">Recruitment</a></li>
                        <li id="liScheduleInt" runat="server" title="Schedule Interviews" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterInbox.aspx?type=RECISL" runat="server" id="AScheduleInt" style="font-size: 12px !important;" title="Schedule Interviews">Schedule Interviews</a></li>
                        <li id="liRescheduleInt" runat="server" title="Reschedule Interviews" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterInbox.aspx?type=RECIRescedule" runat="server" id="ARescheduleInt" style="font-size: 12px !important;" title="Reschedule Interviews">Reschedule Interviews</a></li>
                        <li id="liInterviewr" runat="server" title="Interviewer Inbox" visible="false"><a href="http://localhost/hrms/procs/Rec_InterviewerInbox.aspx?type=InShPInter" runat="server" id="AInterviews" style="font-size: 12px !important;" title="Interviewer Inbox">Interviewer Inbox</a></li>
                        <li id="liOfferAPP" runat="server" title="Offer Approval" visible="false"><a href="http://localhost/hrms/procs/Req_Offer_Index.aspx?itype=Pending" runat="server" id="AOffer" style="font-size: 12px !important;" title="Offer Approval">Offer Approval</a></li>
                        <li id="liCustEscala" runat="server" title="CustomerFirst" visible="false"><a href="http://localhost/hrms/procs/InboxCustEscalation.aspx" runat="server" id="ACustEcla" style="font-size: 12px !important;" title="CustomerFirst">CustomerFirst</a></li>
                        <li id="liCustConfir" runat="server" title="CustomerFirst Pending Confirmation" visible="false"><a href="http://localhost/hrms/procs/InboxCustEscalationApp.aspx" runat="server" id="ACustconfir" style="font-size: 12px !important;" title="CustomerFirst Pending Confirmation">CustomerFirst Confirmation</a></li>
                        <li id="liEmpModerator" runat="server" title="Moderator Pending" visible="false"><a href="http://localhost/hrms/procs/Ref_Moderator_Index.aspx" runat="server" id="AModerator" style="font-size: 12px !important;" title="Moderator Pending">Moderator Pending</a></li>
                        <li id="liResignation" runat="server" title="Pending Resignation" visible="false"><a href="http://localhost/hrms/procs/InboxResignations.aspx" runat="server" id="AResig" style="font-size: 12px !important;" title="Pending Resignation">Pending Resignation</a></li>
                        <li id="liTeamExit" runat="server" title="Pending Team Exit Interview" visible="false"><a href="http://localhost/hrms/procs/ExitProcess_ExitInterviewList.aspx" runat="server" id="ATeamExit" style="font-size: 12px !important;" title="Pending Team Exit">Pending Team Exit</a></li>
                        <li id="liClearance" runat="server" title="Pending Clearance" visible="false"><a href="http://localhost/hrms/procs/ExitProcess_ClearanceInbox.aspx" runat="server" id="AClearance" style="font-size: 12px !important;" title="Pending Clearance">Pending Clearance</a></li>
                        <li id="liTaskPending" runat="server" title="Pending Task" visible="false"><a href="http://localhost/hrms/procs/InboxExecuter.aspx" runat="server" id="Task1" style="font-size: 12px !important;" title="Pending Task">Pending Task</a></li>
                        <li id="liTaskCloseRequest" runat="server" title="Pending Task Close Request" visible="false"><a href="http://localhost/hrms/procs/Task_Closure_Inbox.aspx" runat="server" id="Task2" style="font-size: 12px !important;" title="Pending Task Close Request">Pending Task Close Request</a></li>
                        <li id="liTaskDueDueDateRequest" runat="server" title="Pending Due Date Change Request" visible="false"><a href="http://localhost/hrms/procs/Task_DueDateChange_Inbox.aspx" runat="server" id="Task3" style="font-size: 12px !important;" title="Pending Due Date Change Request">Pending Due Date Change Request</a></li>
                        <li id="liKRAReviewer" runat="server" title="KRA Pending" visible="false"><a href="http://localhost/hrms/procs/KRA_Inbox.aspx" runat="server" id="A_KRAPending" style="font-size: 12px !important;" title="KRA Pending">KRA Pending</a></li>
                        <li id="liKRANotAccepted" runat="server" title="Pending KRA Acceptance" visible="false"><a href="http://localhost/hrms/procs/KRA_Create.aspx" runat="server" id="AKRANotAccepted" style="font-size: 12px !important;" title="Pending KRA Acceptance">Pending KRA Acceptance</a></li>
                        <li id="liODAPP" runat="server" title="Pending OD Application Request" visible="false"><a href="http://localhost/hrms/procs/InboxODApplication.aspx" runat="server" id="A_ODAPP" style="font-size: 12px !important;" title="Pending OD Application Request">Pending OD Application Request</a></li>
                        <li id="liAPPAccept" runat="server" title="Appointment Letter Acceptance" visible="false"><a href="http://localhost/hrms/procs/App_Latter_Acceptance.aspx" runat="server" id="AAcceptance" style="font-size: 12px !important;" title="Appointment Letter Acceptance">Appointment Letter Acceptance</a></li>
                        <li id="liAPPApproval" runat="server" title="Appointment letter Acceptance Approval" visible="false"><a href="http://localhost/hrms/procs/App_Latter_M_Index.aspx?Type=Pending" runat="server" id="AAppLatterM" style="font-size: 12px !important;" title="Appointment letter Acceptance Approval">Appointment letter Acceptance Approval</a></li>
                        <li id="liExceptionAPP" runat="server" title="CTC Exception Approval" visible="false"><a href="http://localhost/hrms/procs/Req_CTCIndex.aspx?itype=Pending" runat="server" id="ACTCException" style="font-size: 12px !important;" title="CTC Exception Approval">CTC Exception Approval</a></li>
                        <li id="liSalStatusUpdate" runat="server" title="Salary Status Update" visible="false"><a href="http://localhost/hrms/procs/SalaryNotUpdatedData.aspx" runat="server" id="ASalaryStatusUpdate" style="font-size: 12px !important;" title="Salary Status Update">Salary Status Update</a></li>
                        <li id="liVendorPOWOApp" runat="server" title="PO/ WO Request Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxPOWO.aspx" runat="server" id="APOWOApp" style="font-size: 12px !important;" title="PO/ WO Approvals">Pending PO/ WO Approvals</a></li>
                        <li id="liVendorInvoiceApp" runat="server" title="Payment Request Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_Inboxinvoice.aspx" runat="server" id="AInvoiceApp" style="font-size: 12px !important;" title="Invoice Approvals">Invoice Approvals</a></li>
                        <li id="liVendorPayApp" runat="server" title="Invoice Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxPaymentRequest.aspx" runat="server" id="APayApp" style="font-size: 12px !important;" title="Payment Request Approvals">Payment Request Approvals</a></li>
                        <li id="liVendorPayCorr" runat="server" title="Payment Request Correction" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxMyPaymentRequest.aspx" runat="server" id="APayCorr" style="font-size: 12px !important;" title="Payment Request Correction">Payment Request Correction</a></li>
                        <li id="liVendorPayPartial" runat="server" title="Partial Payment Requests" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxPartialPaymentRequest.aspx" runat="server" id="APayPartial" style="font-size: 12px !important;" title="Partial Payment Requests">Partial Payment Requests</a></li>
                        <li id="liVendorBatch" runat="server" title="Batch Approvals" visible="false"><a href="http://localhost/hrms/procs/VSCB_InboxBatchReq.aspx" runat="server" id="ABatchApp" style="font-size: 12px !important;" title="Batch Approvals">Batch Approvals</a></li>
                        <li id="LiVendorPayRequestCre" runat="server" title="Due Payment Request" visible="false"><a href="http://localhost/hrms/procs/VSCB_PaymentRequestAll.aspx" runat="server" id="ADuePaymentRequest" style="font-size: 12px !important;" title="Due Payment Request">Due Payment Request</a></li>
                        <li id="LiReviewDelayedTaskPending" runat="server" title="Review Delayed Tasks" visible="false"><a href="http://localhost/hrms/procs/ReviewDelayedTasks.aspx" runat="server" id="AReviewDelayedTaskPending" style="font-size: 12px !important;" title="Review Delayed Tasks">Review Delayed Tasks</a></li>
                        <li id="LiPendingCandidateDetailApprove" runat="server" title="Verify Candidate Data" visible="false"><a href="http://localhost/hrms/procs/Rec_RecruiterCandidateInbox.aspx" runat="server" id="APendingCandidateDetailApprove" style="font-size: 12px !important;" title="Verify Candidate Data">Verify Candidate Data</a></li>
                        <li id="liRetentionAcc" runat="server" title="Employee Retention" visible="false"><a href="http://localhost/hrms/procs/ExitProcess_Mo_Index.aspx?Type=Pending" runat="server" id="ARetentionAcc" style="font-size: 12px !important;" title="Employee Retention">Employee Retention</a></li>
                        <li id="LiPendingCVUpdate" runat="server" title="Pending CV Update" visible="false"><a href="http://localhost/hrms/procs/EmployeeCV.aspx" runat="server" id="APendingCVUpdate" style="font-size: 12px !important;" title="Pending CV Update">Pending CV Update(1)</a></li>
                        <li id="LiCVReviewInbox" runat="server" title="Pending CV Review Inbox" visible="false"><a href="http://localhost/hrms/procs/EmployeeCVReviewInbox.aspx" runat="server" id="ACVReviewInbox" style="font-size: 12px !important;" title="Pending CV Review Inbox">Pending CV Review Inbox(1)</a></li>
                        <li id="LiETRInbox" runat="server" title="Pending Employee Transfer Request Inbox" visible="false"><a href="http://localhost/hrms/procs/EmployeeTransferReqInbox.aspx" runat="server" id="AETRInbox" style="font-size: 12px !important;" title="Pending Employee Transfer Request Inbox">Pending Employee Transfer Request Inbox(1)</a></li>
                        <li id="liAdvPayAPP" runat="server" title="Advance Payment Approval" visible="false"><a href="http://localhost/hrms/procs/VSCB_Inbox_ADV_Payment.aspx?Type=Pending" runat="server" id="AAdvPay" style="font-size: 12px !important;" title="Advance Payment Approval">Advance Payment Approval</a></li>

                        <li id="liPerformanceReviewPendingCnt" runat="server" title="Appraisal Performance Review" visible="false"><a href="http://localhost/hrms/procs/PerformanceReviewList.aspx" runat="server" id="A_ApprPerformanceReviewPending" style="font-size: 12px !important;" title="Appraisal Performance Review">Appraisal Performance Review</a></li>
                        <li id="liPerformanceRecommendationPendingCnt" runat="server" title="Appraisal Performance Recommendation" visible="false"><a href="http://localhost/hrms/procs/ManageRecommendationList.aspx" runat="server" id="A_ApprPerformanceRecommendationPending" style="font-size: 12px !important;" title="Appraisal Performance Recommendation">Appraisal Performance Recommendation</a></li>
                        <li id="LiABAPObjectCompletion" runat="server" title="ABAP Object Completion" visible="false"><a href="http://localhost/hrms/procs/ABAP_Prd_TimeSheetAppList.aspx" runat="server" id="AABAPObjectCompletion" style="font-size: 12px !important;" title="ABAP Object Completion">ABAP Object Completion</a></li>

                        <li id="liEmployeeMediclaimData" runat="server" title="Employee Mediclaim Data" visible="false"><a href="http://localhost/hrms/procs/Mediclaimdata.aspx" runat="server" id="AEmployeeMediclaimData" style="font-size: 12px !important;" title="Employee Mediclaim Data">Employee Mediclaim Data</a></li>
                        <li id="liphoto" runat="server" title="Update Employee Photo" visible="false"><a href="http://localhost/hrms/procs/View_updatephoto_forhr.aspx" runat="server" id="employeephoto" style="font-size: 12px !important;" title="Update Employee Photo">Update Employee Photo</a></li>


                        <%--ABAP Tracker--%>
                        <li id="liABAPPlanSubmitCnt" runat="server" title="Submit ABAP Plan" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_All_List.aspx" runat="server" id="A_ABAPPlanSubmitCnt" style="font-size: 12px !important;" title="Submit ABAP Plan ">Submit ABAP Plan Pending </a></li>
                        <li id="liABAPPlanApprovalCnt" runat="server" title="ABAP Plan Approval" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_PendingList.aspx" runat="server" id="A_ABAPPlanApprovalCnt" style="font-size: 12px !important;" title="ABAP Plan Approval">ABAP Plan Approval Pending</a></li>
                        <li id="liABAPPlanRGSPendingCnt" runat="server" title="RGS Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_RGS.aspx" runat="server" id="A_ABAPPlanRGSPendingCnt" style="font-size: 12px !important;" title="RGS Pending">RGS Pending</a></li>
                        <li id="liABAPPlanFSPendingCnt" runat="server" title="FS Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_FS.aspx" runat="server" id="A_ABAPPlanFSPendingCnt" style="font-size: 12px !important;" title="FS Pending">FS Pending </a></li>
                        <li id="liABAPPlanFSAcceptanceCnt" runat="server" title="FS Acceptance Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_ABAP_Accept_NotAccept.aspx" runat="server" id="A_ABAPPlanFSAcceptanceCnt" style="font-size: 12px !important;" title="FS Acceptance Pending">FS Acceptance Pending </a></li>
                        <li id="liABAPPlanABAPDevPendingCnt" runat="server" title="ABAP Development Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_ABAP.aspx" runat="server" id="A_ABAPPlanABAPDevPendingCnt" style="font-size: 12px !important;" title="ABAP Development Pending">ABAP Development Pending </a></li>
                        <li id="liABAPPlanHBTPendingCnt" runat="server" title="HBT Testing Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_HBTTesting.aspx" runat="server" id="A_ABAPPlanHBTPendingCnt" style="font-size: 12px !important;" title="HBT Testing Pending">HBT Testing Pending </a></li>
                        <li id="liABAPPlanCTMPendingCnt" runat="server" title="CTM Testing Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_CTMTesting.aspx" runat="server" id="A_ABAPPlanCTMPendingCnt" style="font-size: 12px !important;" title="CTM Testing Pending">CTM Testing Pending </a></li>
                        <li id="liABAPPlanCTMApprovalCnt" runat="server" title="CTM Testing Approval" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_CTM_Test_Cases_Index.aspx" runat="server" id="A_ABAPPlanCTMApprovalCnt" style="font-size: 12px !important;" title="CTM Testing Approval">CTM Testing Approval </a></li>
                        <li id="liABAPPlanUATPendingCnt" runat="server" title="HBT Testing Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_UATSignoff.aspx" runat="server" id="A_ABAPPlanUATPendingCnt" style="font-size: 12px !important;" title="HBT Testing Pending">UAT Pending </a></li>
                        <li id="liABAPPlanGoLivePendingCnt" runat="server" title="Go Live Pending" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_GoLive.aspx" runat="server" id="A_ABAPPlanGoLivePendingCnt" style="font-size: 12px !important;" title="Go Live Pending">Go Live Pending </a></li>
                        <li id="liABAPPlanRGSApprovalCnt" runat="server" title="RGS Approval" visible="false"><a href="http://localhost/hrms/procs/ABAP_Object_Tracker_Change_Status_RGS_Approval.aspx" runat="server" id="A_ABAPPlanRGSApprovalCnt" style="font-size: 12px !important;" title="RGS Approval">RGS Approval</a></li>



                        <%--<li id="LiReviewDelayedTaskPending" runat="server" title="Review Delayed Tasks" visible="false"><a href="http://localhost/hrms/procs/ReviewDelayedTasks.aspx" runat="server" id="AReviewDelayedTaskPending" style="font-size: 12px !important;" title="Review Delayed Tasks">Review Delayed Tasks</a></li>--%>
                    </ul>

                </li>
                <li class="has-sub"><span class="submenu-button"></span>
                    <%-- <a href="http://localhost/hrms/hrmsgroup.aspx" style="font-size: 14px !important;" title="Highbar Group">Highbar Group</a>--%>
                    <%--                    <div id="dvMsg" class="notification" runat="server" visible="false">
                    <asp:Label ID="lblMsg" runat="server" visible="false"></asp:Label>
                    </div>--%>
                    <div class="tcontainer">
                        <div class="ticker-wrap">
                            <div class="ticker-move">
                                <div class="ticker-item">Please Punch-In & Punch-Out your attendance in Attendance module!          </div>
                                <%--<div class="ticker-item">          Please fill SPANDAN survey through the link provided here on homepage!!          </div>--%>
                                <%--<div class="ticker-item">PF data is available through link in My Corner</div>
  				<div class="ticker-item">You can find your UAN no. from Salary Slip in My Corner. </div>--%>
                            </div>
                        </div>
                    </div>
                </li>
                <li class="has-sub" style="display: none"><span class="submenu-button"></span><a href="http://localhost/hrms/ho.aspx" style="font-size: 14px !important;" title="HO">Head Office</a>
                    <ul class="mega-sub-menu"></ul>
                </li>
                <li class="has-sub" style="display: none"><span class="submenu-button"></span><%--<a href="http://localhost/hrms/ps/e0EuruqY6eXyYJ6MaTjsDw==" style="font-size: 14px !important;"  title="Projects">Projects</a>--%>
                    <%--<a href="http://localhost/hrms/projectlist.aspx" style="font-size: 14px !important;"  title="Projects">Projects</a>--%>
                    <a href="#" style="font-size: 14px !important;" title="Projects">Projects</a>
                    <ul class="mega-sub-menu">
                        <li><a href="http://localhost/hrms/ongoing.aspx" title="Ongoing">Ongoing</a></li>
                        <li><a href="http://localhost/hrms/completed.aspx" title="Completed">Completed</a></li>
                        <!--<li><a href="http://localhost/hrms/projectmanagernamewithphotograph" title="Project Manager Name with photograph">Project Manager Name with photograph</a></li>
  <li><a href="http://localhost/hrms/progressphotograph" title="Progress Photograph">Progress Photograph</a></li>
  <li><a href="http://localhost/hrms/currentstatus" title="Current Status">Current Status</a></li>-->
                    </ul>
                </li>
                <li class="has-sub" style="display: none"><span class="submenu-button"></span><a href="#" style="font-size: 14px !important;" title="Policy & Procedures">Policy & Procedures</a>
                    <ul class="mega-sub-menu">

                        <li><a href="#" style="font-size: 14px !important;" title="Integrated Management Systems">Integrated Management Systems</a>
                            <ul class="mega-sub-menu">
                                <!--    <li><a href="http://localhost/hrms/corporatemanual" style="font-size: 14px !important;" title="Corporate Manual">Corporate Manual</a></li>
    <li><a href="http://localhost/hrms/qms.aspx" style="font-size: 14px !important;" title="QMS">QMS</a></li>
    <li><a href="http://localhost/hrms/ems" style="font-size: 14px !important;" title="EMS">EMS</a></li>
    <li><a href="http://localhost/hrms/ohandsms" style="font-size: 14px !important;"  title="OH&SMS">OH&SMS</a></li>-->
                                <!--********************************************************************-->
                                <li><a href="http://localhost/hrms/strategicdocuments" style="font-size: 14px !important;" title="Strategic Documents">Strategic Documents</a>
                                    <!--<ul class="mega-sub-menu">
			<li><a href="http://localhost/hrms/accordian" style="font-size: 14px !important;" title="Accordian">Accordian</a></li>
		  </ul>		-->
                                </li>
                                <li><a href="http://localhost/hrms/corporatemanual" style="font-size: 14px !important;" title="Corporate Manual">Corporate Manual</a></li>
                                <li><a href="#" style="font-size: 14px !important;" title="Procedures">Procedures</a>
                                    <ul class="mega-sub-menu">

                                        <li><a href="http://localhost/hrms/informationsystemdepartmentprocedures" style="font-size: 14px !important;" title="Information System Department Procedures">Information Systems</a></li>
                                        <li><a href="http://localhost/hrms/engineeringmanagement " style="font-size: 14px !important;" title="Engineering Management">Engineering Management</a></li>
                                        <li><a href="#" style="font-size: 14px !important;" title="IMS Common Procedures">IMS Common Procedures</a>
                                            <ul class="mega-sub-menu">
                                                <li><a href="http://localhost/hrms/mr" style="font-size: 14px !important;" title="MR">MR</a></li>
                                                <li><a href="http://localhost/hrms/qaqc" style="font-size: 14px !important;" title="QA QC">QA QC</a></li>
                                                <li><a href="http://localhost/hrms/ems" style="font-size: 14px !important;" title="EMS">EMS</a></li>
                                                <li><a href="http://localhost/hrms/emsperformance" style="font-size: 14px !important;" title="EMS Performance">EMS Performance</a></li>
                                                <li><a href="http://localhost/hrms/ohsms" style="font-size: 14px !important;" title="OH&SMS">OH&SMS</a></li>
                                                <li><a href="http://localhost/hrms/ohsmsperformance" style="font-size: 14px !important;" title="OH&SMS performance">OH&SMS performance</a></li>
                                            </ul>
                                        </li>
                                        <li><a href="http://localhost/hrms/procurementandsubcontract " style="font-size: 14px !important;" title="Procurement & Sub-Contract">Procurement & Sub-Contract</a></li>
                                        <li><a href="http://localhost/hrms/humanresource" style="font-size: 14px !important;" title="HUMAN RESOURCE">Human Resouece</a></li>
                                        <li><a href="http://localhost/hrms/equipment" style="font-size: 14px !important;" title="EQUIPMENT">Equipment</a></li>
                                        <li><a href="http://localhost/hrms/contracts" style="font-size: 14px !important;" title="CONTRACTS">Contracts</a></li>
                                        <li><a href="http://localhost/hrms/accountsfinanceandtaxation" style="font-size: 14px !important;" title="FINANCE ACCOUNTS & TAXATION">Accounts Finance & Taxation</a></li>
                                        <!--<li><a href="http://localhost/hrms/projectmonitoringandcontrol" style="font-size: 14px !important;" title="PROJECT MONITORING & CONTROL">Project Monitoring & Control</a></li>-->
                                        <li><a href="http://localhost/hrms/centralprojectsplanningandmonitoring" style="font-size: 14px !important;" title="CENTRAL PROJECTS PLANNING & MONITORING">Central Projects Planning & Monitoring</a></li>
                                        <li><a href="http://localhost/hrms/cc" style="font-size: 14px !important;" title="CORPORATE COMMUNICATION">Corporate Communications</a></li>
                                        <li><a href="http://localhost/hrms/cos" style="font-size: 14px !important;" title="CORPORATE OFFICE SERVICES">Corporate Office Services</a></li>
                                        <li><a href="http://localhost/hrms/csr" style="font-size: 14px !important;" title="CSR">Corporate Social Responsibility</a></li>
                                        <li><a href="http://localhost/hrms/tara" style="font-size: 14px !important;" title="TARA">TARA</a></li>
                                        <li><a href="http://localhost/hrms/siteprocedure" style="font-size: 14px !important;" title="SITE PROCEDURE">Site Procedure</a></li>

                                    </ul>


                                </li>
                                <li><a href="http://localhost/hrms/contextoforganization" style="font-size: 14px !important;" title="Context of Organization">Context of Organization</a></li>
                                <li><a href="http://localhost/hrms/organizationalknowledge" style="font-size: 14px !important;" title="Organizational Knowledge">Organizational Knowledge</a>
                                    <ul class="mega-sub-menu">
                                        <li><a href="http://localhost/hrms/organizationalknowledgeenggmanagement" style="font-size: 14px !important;" title="Engg Management">Engg Management </a></li>
                                        <li><a href="http://localhost/hrms/organizationalknowledgefinanceandaccounts" style="font-size: 14px !important;" title="Finance And Accounts">Finance And Accounts </a></li>
                                        <li><a href="http://localhost/hrms/organizationalknowledgecorporateofficeservices" style="font-size: 14px !important;" title="Corporate Office Services">Corporate Office Services </a></li>
                                        <li><a href="http://localhost/hrms/organizationalknowledgecorporatesocialresponsibility" style="font-size: 14px !important;" title="Corporate Social Responsibility">Corporate Social Responsibility</a></li>
                                        <li><a href="http://localhost/hrms/organizationalknowledgecontracts" style="font-size: 14px !important;" title="Contracts">Contracts</a></li>
                                    </ul>
                                </li>
                                <li><a href="http://localhost/hrms/safetyawareness" style="font-size: 14px !important;" title="Safety Awareness (E – Learning)">Safety Awareness    (E – Learning)</a>
                                </li>
                                <li><a href="http://localhost/hrms/environmentknowledge" style="font-size: 14px !important;" title="Environment Knowledge">Environment Knowledge</a>
                                    <!--<ul class="mega-sub-menu">
			<li><a href="http://localhost/hrms/procedures" style="font-size: 14px !important;" title="Environment Messages">Environment Messages </a></li>
			<li><a href="http://localhost/hrms/procedures" style="font-size: 14px !important;" title="Environment Posters">Environment Posters</a></li>
		 </ul>	-->
                                </li>

                                <!--********************************************************************-->

                            </ul>
                        </li>
                        <li><a href="http://localhost/hrms/humanresource-policies.aspx" style="font-size: 14px !important;" title="Human Resource">Human Resource</a></li>
                        <li><a href="http://localhost/hrms/corporateofficeservices" style="font-size: 14px !important;" title="Corporate Office Services">Corporate Office Services</a>
                            <!--<ul class="mega-sub-menu">
    <li><a href="#" style="font-size: 14px !important;" title="A">A</a></li>
    <li><a href="#" style="font-size: 14px !important;" title="B">B</a></li>
    <li><a href="#" style="font-size: 14px !important;" title="C">C</a></li>
    <li><a href="#" style="font-size: 14px !important;" title="D">D</a></li>
 </ul> -->
                        </li>
                        <li><a href="#" style="font-size: 14px !important;" title="Corporate Communications">Corporate Communications</a>
                            <ul class="mega-sub-menu">
                                <li><a href="http://localhost/hrms/brandguidelines" style="font-size: 14px !important;" title="Brand Guidelines">Brand Guidelines</a></li>
                                <li><a href="http://localhost/hrms/grouppresentation" style="font-size: 14px !important;" title="Group Presentation">Group Presentation</a></li>
                            </ul>
                        </li>
                        <li><a href="http://localhost/hrms/corporatesocialresponsibility" style="font-size: 14px !important;" title="Corporate Social Responsibility">Corporate Social Responsibility</a></li>
                        <li><a href="http://localhost/hrms/informationsystems" style="font-size: 14px !important;" title="Information Systems">Information Systems</a></li>
                        <!--<li><a href="http://localhost/hrms/humanresource-policies.aspx" style="font-size: 14px !important;" title="Human Resource">Human Resource</a></li>-->
                        <!--<li><a href="http://localhost/hrms/alldepartmentprocedure" style="font-size: 14px !important;"  title="All Department Procedures">All Department Procedures</a>
		 <ul class="mega-sub-menu">
			<li><a href="http://localhost/hrms/newsaccordians" style="font-size: 14px !important;" title="Sub Menu">Sub Menu1</a></li>
		 </ul>	
	    </li>-->


                    </ul>
                </li>
                <%--<li class="has-sub"><span class="submenu-button"></span><a href="http://localhost/hrms/procedures" title="Procedures">Procedures</a>--%>
                <ul class="mega-sub-menu">
                    <%--<li><a href="http://localhost/hrms/alldepartmentprocedure" title="All Department Procedures">All Department Procedures</a></li>--%>
                </ul>
                </li>
                <li class="has-sub" style="display: none"><span class="submenu-button"></span><a href="#" style="font-size: 14px !important;" title="Gallery">Gallery</a>
                    <ul class="mega-sub-menu">
                        <li><a href="#" style="font-size: 14px !important;" title="Photographs">Photographs</a>
                            <ul class="mega-sub-menu">

                                <li><a href="http://localhost/hrms/ps/hqjrtcrY5IJTEd2cPsDBFw==" style="font-size: 14px !important;" title="Transport">Transportation</a></li>
                                <li><a href="http://localhost/hrms/ps/6b0kgTGNAapd3n28TD_vig==" style="font-size: 14px !important;" title="Hydro">Hydro Power</a></li>
                                <li><a href="http://localhost/hrms/ps/Ut7_RTkgp0vF_-ZEIiFlZQ==" style="font-size: 14px !important;" title="Nuclear Power">Nuclear Power & Special Projects</a></li>
                                <li><a href="http://localhost/hrms/ps/LDvv5iC7MC4UPbvLBTVjRQ==" style="font-size: 14px !important;" title="Water Solution">Water Solution</a></li>
                                <li><a href="http://localhost/hrms/ps/0JKuwKKfZXmayODjj6E9tQ==" style="font-size: 14px !important;" title="Buildings & Industrial">Buildings & Industrial Plants</a></li>
                                <li><a href="http://localhost/hrms/ps/IOYHQIKJUk0l00B5T3SwqA==" style="font-size: 14px !important;" title="Thermal Power">Thermal Power</a></li>
                                <li><a href="http://localhost/hrms/ps/VGGql6vR1crCkva-pVk5hQ==" style="font-size: 14px !important;" title="Ports & Marine">Ports & Marine</a></li>
                            </ul>
                        </li>
                        <li><a href="http://localhost/hrms/ps/ylAI0oJu7HJ7MQqa9fgC8w==" style="font-size: 14px !important;" title="Videos">Videos</a></li>
                    </ul>
                </li>
                <li class="has-sub" style="display: none"><span class="submenu-button"></span><a href="http://localhost/hrms/whitepapers.aspx" style="font-size: 14px !important;" title="Knowledge Centre">Knowledge Centre</a>
                    <ul class="mega-sub-menu">
                        <%--<li><a href="http://localhost/hrms/ps/qpWxk1i4vHHyVw9fmLUOeQ==" style="font-size: 14px !important;" title="News">News</a></li>
<li><a href="http://localhost/hrms/ps/V4cbELyNCF6GlgD6lYZ_mQ==" style="font-size: 14px !important;" title="Speeches">Speeches</a></li>--%>
                        <%--   <li><a href="http://localhost/hrms/ps/nMHuMhwryph7lOo_PsklcA==" style="font-size: 14px !important;"  title="Whitepapers">Whitepapers</a></li> 
       <li><a href="http://localhost/hrms/whitepapers.aspx" style="font-size: 14px !important;"  title="Whitepapers">Whitepapers</a></li> --%>
                        <!--<li><a href="http://localhost/hrms/ps/4aBNc2DICzEVSii43gO4IA==" style="font-size: 14px !important;"  title="Test Whitepapers">Test Whitepapers</a></li> -->
                        <%--SAGAR COMMENTED THIS FOR REMOVING ANY OTHER FROM MENU 21SEPT2017--%>
                        <%--<li><a href="http://localhost/hrms/anyother" title="Speeches">Any other</a></li>--%>
                    </ul>
                </li>
                <li class="has-sub" style="display: none"><span class="submenu-button"></span><a href="#" style="font-size: 14px !important;" title="Media">Media</a>
                    <ul class="mega-sub-menu">
                        <li><a href="http://localhost/hrms/pressrelease.aspx" style="font-size: 14px !important;" title="Press Releases">Press Releases</a></li>
                        <li><a href="http://localhost/hrms/ps/L7S11lAQzL9NKuo_x0byJg==" style="font-size: 14px !important;" title=" Interviews">Interviews</a></li>
                        <li><a href="http://localhost/hrms/financialresults.aspx" style="font-size: 14px !important;" title="Financial Results">Financial Results</a></li>

                        <li><a href="http://localhost/hrms/annualreports.aspx" style="font-size: 14px !important;" title="Financial Results">Annual Reports</a></li>


                    </ul>
                </li>
            </ul>


        </div>
    </div>
    <!-- end menu - sagar and prajyot -->

    <!-- SONY added this -->

    <!-- SONY code ends here -->


    <div id="user-sidebar">
        <div class="text-birth1 mCustomScrollbar" style="overflow-x: hidden !important;">
            <header id="user-cover">
                <a href="#" class="clearfix" id="lnkprof" runat="server">

                    <asp:Image ID="imgbigpic" runat="server" CssClass="avatar user-3-avatar avatar-96 photo" />
                    <%--<span id="labl">Welcome--%>
                    <asp:Label ID="lblfname" runat="server" CssClass="woffice-welcome" Text="User"></asp:Label>
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
        <asp:HiddenField ID="HDCusts_HOD" runat="server" />
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
