<%@ Control Language="C#" AutoEventWireup="true" CodeFile="reminder.ascx.cs" Inherits="themes_creative1_LayoutControls_reminder" %>
<link rel="stylesheet" href="<%=ReturnUrl("css") %>reminder/reminder.css" type="text/css" media="screen" />
<div class="reminder-container">
    <div class="reminder-wrapper">
        <div class="reminder-head">
            <span class="reminder-head-title"><i class="fa fa-bell"></i> Event Reminder (<span id="lblrmcount">0</span>)</span>
            <a class="rmdclose"><i class="fa fa-close"></i></a>
        </div>
        <div class="reminder-body">
            <div class="reminder-content">
                <ul class="reminder-content-list mscrollbar">
                    <%--<li><a href="#"><span><i class="fa fa-calendar"></i></span><span class="reminder-guest">Test Event at 4.30 PM<br />GUEST : Shaikh Hameed, Krishna Sawant, Mariya Sayyed</span></a></li><li>
                        <a href="#"><span><i class="fa fa-calendar"></i></span>
                            <span class="reminder-guest">Test Event at 4.30 PM
                                <br />
                                GUEST : Shaikh Hameed, Krishna Sawant, Mariya Sayyed</span></a>
                    </li>
                    <li>
                        <a href="#"><span><i class="fa fa-calendar"></i></span>
                            <span class="reminder-guest">Test Event at 4.30 PM
                                <br />
                                GUEST : Shaikh Hameed, Krishna Sawant, Mariya Sayyed</span></a>
                    </li>
                    <li>
                        <a href="#"><span><i class="fa fa-calendar"></i></span>
                            <span class="reminder-guest">Test Event at 4.30 PM
                                <br />
                                GUEST : Shaikh Hameed, Krishna Sawant, Mariya Sayyed</span></a>
                    </li>
                    <li>
                        <a href="#"><span><i class="fa fa-calendar"></i></span>
                            <span class="reminder-guest">Test Event at 4.30 PM
                                <br />
                                GUEST : Shaikh Hameed, Krishna Sawant, Mariya Sayyed</span></a>
                    </li>
                    <li>
                        <a href="#"><span><i class="fa fa-calendar"></i></span>
                            <span class="reminder-guest">Test Event at 4.30 PM
                                <br />
                                GUEST : Shaikh Hameed, Krishna Sawant, Mariya Sayyed</span></a>
                    </li>--%>
                </ul>
            </div>
        </div>
    </div>
</div>
<div class="reminder-overlay"></div>
