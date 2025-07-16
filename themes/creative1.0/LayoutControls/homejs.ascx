<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homejs.ascx.cs" Inherits="themes_creative" %>
<%@ Register Src="~/themes/creative1.0/LayoutControls/reminder.ascx" TagName="reminder" TagPrefix="uc" %>
<script src="<%=ReturnUrl("sitepath")%>js/highbar/jquery.js"></script>
<script defer="defer">jQuery(document).ready(function () {
    jQuery("#scroll-top").on("click", function () {
        //SCROLL TO TOP
        jQuery("html, body").animate({ scrollTop: 0 }, 500);
        return false;
    });
});</script>
<script src="<%=ReturnUrl("sitepath")%>js/bxslider/jquery.bxslider.js"></script>
<script type="text/javascript">
    jQuery(document).ready(function () {
        if (top != self) {
            top.onbeforeunload = function () { };
            top.location.replace(self.location.href);
        }
        jQuery('.slider1').bxSlider({
            auto:true,
            slideWidth: innerWidth,
            minSlides: 1,
            maxSlides: 1,
            slideMargin: 10
        });
    });
</script>

<%--<script type="text/javascript">
    jQuery(document).ready(function () {
        var width;
        width = jQuery(window).width();
        if (width < 340) {
            jQuery('#bannerimg').attr('src',jQuery('#bannerimg').attr('src').replace("/banner/", "/bannermobile/"));
        }
    });
</script>--%>


<script>
    jQuery(document).ready(function () {
        jQuery(".lnkclose").click(function () {
            var el = jQuery(this);
            jQuery.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'manageservice.aspx/InsertUpdateSettings',
                data: "{'widget':'" + jQuery(this).attr("data") + "'}",
                dataType: 'text',
                async: false,
                success: function (response) {
                    if (response.indexOf("success") > -1) {
                        // e.preventDefault();    
                        el.parent().parent().css("display", "none");
                        return false;
                    }
                    else if (response.indexOf("error") > -1) {
                        //$('#m_uxloginpopup_lblstatus').show();
                        //    e.preventDefault();
                        return false;
                    }
                },
                error: function (xhr, status, error) {
                    //     e.preventDefault();
                    return false;
                    //alert(xhr.responseText);
                }
            });

            //alert(jQuery(this).attr("data"));
        });
    });
    //jQuery(document).ready(function () {
    //    var refreshId = setInterval(function () {
    //        jQuery.ajax({
    //            type: 'POST',
    //            contentType: "application/json; charset=utf-8",
    //            url: 'http://192.168.0.72/chatapp/notify.ashx?id=2',
    //            data: "{'userid':'2'}",
    //            dataType: 'text',
    //            async: false,
    //            success: function (response) {
    //                var json = JSON.parse(response);
    //                //alert(json["d"]);
    //                var xmlDoc = $.parseXML(json["d"]);
    //                var $xml = $(xmlDoc);
    //                var $table = $xml.find("Table1");
    //                $table.each(function () {
    //                    var msgcount = $(this).find('msgcount').text();
    //                    alert(msgcount);
    //                });

    //            },
    //            error: function (xhr, status, error) {
    //                return false;
    //            }
    //        });
    //    }, 2000);
    ////});
<%-- function getchatdata()
    {
        var refreshId2 = setInterval(function () {
        jQuery.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: "ReturnUrl("sitepathmain")manageservice.aspx/GetChatData",
            data: "{'content':'" + jQuery("#topseach_hduserchat").val() + "'}",
            dataType: 'text',
            async: false,
            success: function (response) {
                var json = JSON.parse(response);
                //alert(json["d"]);
                var xmlDoc = jQuery.parseXML(json["d"]);
                var $xml = jQuery(xmlDoc);
                var $table = $xml.find("Table2");
                jQuery(".chat-container").html('');
                $table.each(function () {
                    var URL = "<%=ConfigurationManager.AppSettings["chatURL"]%>default.aspx?id=" + jQuery("#topseach_hduserid").val() + '&to=' + jQuery(this).find('userid').text();
                    jQuery(".chat-container").append("<li><a href='"+URL+"' class='messagesContent' target='_blank'><div class='chat-wrap'> <div class='left-wrap'> <div class='chat-image'> <img alt='' src='" + jQuery(this).find('imageURL').text() + "' /> </div> </div> <div class='right-wrap'> <div class='right-content'> <div class='chat-author'> <strong><span>" + jQuery(this).find('username').text() + "</span></strong> </div> <div class='chat-msg-preview'> <span>" + jQuery(this).find('message').text() + "</span> </div> <div class='chat-time'> <span>" + jQuery(this).find('date').text() + "</span> </div> </div> </div> </div></a></li>");
                });

            },
            error: function (xhr, status, error) {
                return false;
            }
        });
        }, 2000);
    }
       
 var refreshId = setInterval(function () {
        jQuery.ajax({
            crossDomain: true,
            dataType: "jsonp",
            type: "GET",
            cache: false,
            jsonp: "callback",
            url: "<%=ConfigurationManager.AppSettings["chatURL"]%>notify.ashx?callback=?&id=" + jQuery("#topseach_hduserid").val(),
            data:"",
            contentType: "application/json; charset=utf-8",
            
            success: function(response) {
               // alert(response);
                var json = response;
                //alert(json);
                //alert(json["d"]);
                jQuery("#topseach_hduserchat").val(json);
                var xmlDoc = jQuery.parseXML(json);
                var $xml = jQuery(xmlDoc);
                var $table = $xml.find("Table1");
                var $table2 = $xml.find("Table2");
                $table.each(function () {
                    var msgcount = jQuery(this).find('msgcount').text();
                    jQuery("#lblchatcount").html(msgcount);
                   // alert(msgcount);
                });
            },
            error: function(xhr, textStatus, errorThrown) {
              //  alert(textStatus);
            }
        })
    },2000); --%>
</script>

<script type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#nav-sidebar-trigger").click(function () {
            //jQuery("#right-sidebar").css("right", "0");
            jQuery("#right-sidebar").toggleClass("sidebar-wrap");
            jQuery("#left-content").toggleClass("left-content-wrap");
        });

        // Close Notification tab :
        function CloseNotfications() {
            jQuery('#woffice-notifications-menu').fadeOut();
        }
        function CloseMessages() {
            jQuery('#woffice-message-menu').fadeOut();
        }
        function CloseLinks() {
            jQuery('#woffice-links-menu').fadeOut();
        }
        function CloseChat() {
            jQuery('#woffice-chat-menu').fadeOut();
        }
        // Mark as read :
        function MarkNotificationRead() {
            jQuery('a.mark-notification-read').on('click', function () {
                var readLink = jQuery(this);
                var component_action = readLink.data('component-action');
                var component_name = readLink.data('component-name');
                var item_id = readLink.data('item-id');
            });
        }
        // Notification AJAX :
        jQuery('#nav-notification-trigger').click(function () {
            if (!jQuery(this).hasClass('clicked')) {
                jQuery('#woffice-notifications-menu').slideDown();
                jQuery('#woffice-notfications-loader').fadeIn();
                jQuery('#woffice-notifications-content').empty();
                CloseMessages();
                CloseLinks();
                CloseChat();
                jQuery(this).addClass('clicked');
                jQuery('#topseach_navuser').removeClass
                    ('active');
                jQuery('#user-sidebar').removeClass('active');
                jQuery('#nav-chat-trigger').removeClass('clicked');
                jQuery('#nav-links-trigger').removeClass('clicked');
                jQuery('#nav-message-trigger').removeClass('clicked');
                jQuery("#right-sidebar").removeClass("sidebar-wrap");
                jQuery("#left-content").removeClass("left-content-wrap");
            } else {
                CloseNotfications();
                jQuery(this).removeClass('clicked');
            }
        });
        jQuery('#nav-message-trigger').click(function () {
            if (!jQuery(this).hasClass('clicked')) {
                jQuery('#woffice-message-menu').slideDown();
                jQuery('#woffice-notfications-loader').fadeIn();
                jQuery(this).addClass('clicked');
                CloseNotfications();
                CloseChat();
                CloseLinks();
                jQuery('#topseach_navuser').removeClass('active');
                jQuery('#user-sidebar').removeClass('active');
                jQuery('#nav-chat-trigger').removeClass('clicked');
                jQuery('#nav-links-trigger').removeClass('clicked');
                jQuery('#nav-notification-trigger').removeClass('clicked');
                jQuery("#right-sidebar").removeClass("sidebar-wrap");
                jQuery("#left-content").removeClass("left-content-wrap");
            }
            else {
                CloseMessages();
                jQuery(this).removeClass('clicked');
            }
        });
        jQuery('#nav-links-trigger').click(function () {
            if (!jQuery(this).hasClass('clicked')) {
                jQuery('#woffice-links-menu').slideDown();
                jQuery(this).addClass('clicked');
                CloseMessages();
                CloseChat();
                CloseNotfications();
                jQuery('#topseach_navuser').removeClass('active');
                jQuery('#user-sidebar').removeClass('active');
                jQuery('#nav-chat-trigger').removeClass('clicked');
                jQuery('#nav-message-trigger').removeClass('clicked');
                jQuery('#nav-notification-trigger').removeClass('clicked');
                jQuery("#right-sidebar").removeClass("sidebar-wrap");
                jQuery("#left-content").removeClass("left-content-wrap");
            }
            else {
                CloseLinks();
                jQuery(this).removeClass('clicked');
            }
        });
        jQuery('#nav-chat-trigger').click(function () {
            if (!jQuery(this).hasClass('clicked')) {
                jQuery('#topseach_navuser').removeClass('active');
                jQuery('#user-sidebar').removeClass('active');
                //jQuery('#woffice-chat-menu').slideDown();
                //jQuery(this).addClass('clicked');
                jQuery('#nav-links-trigger').removeClass('clicked');
                jQuery('#nav-message-trigger').removeClass('clicked');
                jQuery('#nav-notification-trigger').removeClass('clicked');
                jQuery("#right-sidebar").removeClass("sidebar-wrap");
                jQuery("#left-content").removeClass("left-content-wrap");
                getchatdata();
                CloseMessages();
                CloseLinks();
                CloseNotfications();
            }
            else {
                CloseChat();
                jQuery(this).removeClass('clicked');
            }
        });
        jQuery('#nav-sidebar-trigger').click(function () {
            jQuery('#topseach_navuser').removeClass('active');
            jQuery('#user-sidebar').removeClass('active');
            jQuery('#nav-links-trigger').removeClass('clicked');
            jQuery('#nav-message-trigger').removeClass('clicked');
            jQuery('#nav-notification-trigger').removeClass('clicked');
            jQuery('#nav-chat-trigger').removeClass('clicked');
            CloseLinks();
            CloseChat();
            CloseMessages();
            CloseNotfications();
        });
        jQuery('#user-thumb').click(function () {
            
            jQuery('#nav-links-trigger').removeClass('clicked');
            jQuery('#nav-message-trigger').removeClass('clicked');
            jQuery('#nav-notification-trigger').removeClass('clicked');
            jQuery('#nav-chat-trigger').removeClass('clicked');
            jQuery("#right-sidebar").removeClass("sidebar-wrap");
            jQuery("#left-content").removeClass("left-content-wrap");
            CloseLinks();
            CloseChat();
            CloseMessages();
            CloseNotfications();
        });

        if (document.getElementById("topseach_msghdcount").value == 0) {
            jQuery("#msg_count").hide();
        }
        else {
            jQuery("#msg_count").html(document.getElementById("topseach_msghdcount").value);
        }

        if (document.getElementById("topseach_hdcount").value == 0) {
            jQuery("#notification_count").hide();
        }
        else {
            jQuery("#notification_count").html(document.getElementById("topseach_hdcount").value);
        }
        //jQuery("#MainContent_uxhomecontent_uxhome_m_uxcategory_panfun").load("http://192.168.0.72/chatjs/default.aspx?id=1");
    });
    
</script>
<script>
    jQuery(document).ready(function () {
        jQuery("#btnclose").click(function () {
            jQuery('.divpopup').toggle(500);
        });
    });
</script>
<script src="<%=ReturnUrl("sitepath")%>js/highbar/plugins.js"></script>
<asp:Literal ID="ltjs" runat="server"></asp:Literal>
<img id="imgSessionAlive" src="" style="display:none;" />
<asp:Literal ID="ltalive" runat="server"></asp:Literal>
<script type='text/javascript' src='<%=ReturnUrl("sitepath")%>js/datepicker/jquery-ui.js'></script>
<script>
        jQuery("#MainContent_txtdob").datepicker({
            dateFormat: "dd/M/yy",
            changeMonth: true,
            changeYear: true,
            yearRange: "1950:2050",
        });
</script>   
      <script>
                      jQuery("#MainContent_txtsdate").datepicker({
        dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1950:2050",
    });
                      jQuery("#MainContent_txtedate").datepicker({
        dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true,
        yearRange: "1950:2050",
    });


    </script>
<script src="<%=ReturnUrl("sitepath")%>js/highbar/scripts.js"></script>
<uc:reminder ID="ucxreminder" runat="server" />
<script>
    //SAGAR COMMENTED THIS FOR REMOVING EVENT REMINDER NOTIFICATION 6OCT2017 STRATS HERE
 var refreshId = setInterval(function () {
        var dt = new Date();
        var time = dt.getMinutes();
        var sec = dt.getSeconds();
        if ((time == 30 && sec < 5) || (time == 0 && sec < 5))
        //if ((time != 30))
        {
            jQuery.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: "<%=ReturnUrl("sitepathmain")%>manageservice.aspx/GetReminders",
                data: "{'rtime':'" + dt.toLocaleTimeString().replace(/:\d+ /, ' ') + "'}",
            dataType: 'text',
            async: false,
            success: function (response) {
                var json = JSON.parse(response);
                var xmlDoc = jQuery.parseXML(json["d"]);
                var $xml = jQuery(xmlDoc);
                var $table = $xml.find("Table1");
                jQuery(".reminder-content .mCSB_container").html("");
                var intCount = 0;
                if ($table.length > 0)
                {
                    $table.each(function () {
                        
                        intCount = intCount + 1;
                        jQuery(".reminder-content .mCSB_container").append("<li id='rmlist" + jQuery(this).find('Id').text() + "'><a href='#'><span><i class='fa fa-calendar'></i></span><span class='reminder-guest'>" + jQuery(this).find('productname').text() + " at " + jQuery(this).find('reminder_time').text() + "<br />GUEST : " + jQuery(this).find('Tagged').text() + "</span></a><span class='rmclose' data-id='" + jQuery(this).find('Id').text() + "'><i class='fa fa-close'></i></span></li>");
                    });
                    //alert(intCount);
                    if (intCount > 0) {
                        jQuery('.rmclose').on('click', null, function () {
                            var rid = jQuery(this).attr("data-id");
                            jQuery.ajax({
                                type: 'POST',
                                contentType: "application/json; charset=utf-8",
                                url: "<%=ReturnUrl("sitepathmain")%>manageservice.aspx/UpdateReminderStatus",
                            data: "{'id':'" + jQuery(this).attr("data-id") + "'}",
                            dataType: 'text',
                            async: false,
                            success: function (response) {
                                var rmid = '#rmlist' + rid;
                                //alert(rmid);
                                jQuery(rmid).remove();
                            },
                            error: function (xhr, status, error) {
                                return false;
                            }
                        });
                    });
                    jQuery("#lblrmcount").html(intCount);
                    jQuery(".reminder-overlay").fadeIn();
                    jQuery(".reminder-container").fadeIn(500);
                    jQuery("html").css("overflow-y", "hidden");
                    }
                    else {
                        jQuery("html").css("overflow-y", "auto");
                        jQuery('.reminder-overlay').fadeOut(800);
                        jQuery('.reminder-container').fadeOut(500);
                    }
              }
                else {
                    jQuery("html").css("overflow-y", "auto");
                    jQuery('.reminder-overlay').fadeOut(800);
                    jQuery('.reminder-container').fadeOut(500);
                }
            },
                error: function (xhr, status, error) {
                   // alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                    //alert("responseText: " + xhr.responseText);
                    return false;
                }
            });

            //jQuery(".reminder-overlay").fadeIn();
            //jQuery(".reminder-container").fadeIn(500);
            //jQuery("html").css("overflow-y","hidden");
        }
        //alert(dt.toLocaleTimeString().replace(/:\d+ /, ' '));
    }, 2000);
  
    jQuery(document).ready(function () {
        jQuery(".rmdclose").click(function () {
            jQuery("html").css("overflow-y", "auto");
            jQuery('.reminder-overlay').fadeOut(800);
            jQuery('.reminder-container').fadeOut(500);
        });
    });
    //SAGAR COMMENTED THIS FOR REMOVING EVENT REMINDER NOTIFICATION 6OCT2017 ENDS HERE
</script>
<link rel="stylesheet" type="text/css" href ="<%=ReturnUrl("css") %>tooltip/tooltipster.css" media="all"/>
<script type="text/javascript" src="<%=ReturnUrl("sitepath")%>js/jquery.tooltipster.min.js"></script>
<script>
    jQuery(document).ready(function () {
        jQuery('.tooltip').tooltipster({
            contentAsHTML: true,
            animation: 'fade',
            delay: 200,
            theme: 'tooltipster-default',
            touchDevices: false,
            trigger: 'hover'
        });
    });
</script>

<% if (!Page.User.IsInRole("Administrator") || !Page.User.IsInRole("Super Administrator"))
   { %>
<script>jQuery(document).ready(function () { jQuery("body").on("contextmenu", function (n) { return !1 }) }), document.onkeypress = function (n) { return n = n || window.event, 123 == n.keyCode ? !1 : void 0 }, document.onmousedown = function (n) { return n = n || window.event, 123 == n.keyCode ? !1 : void 0 }, document.onkeydown = function (n) { return n = n || window.event, 123 == n.keyCode ? !1 : void 0 }, jQuery(function () { jQuery("#fullscreen_slider").slider({ height: "auto", pagination: !0, thumbnails: !1 }) });</script><script>    jQuery(document).ready(function () { jQuery(document).keydown(function (e) { var o = String.fromCharCode(e.keyCode).toLowerCase(); return !e.ctrlKey || "c" != o && "u" != o ? void 0 : !1 }) });</script>
<%} %>