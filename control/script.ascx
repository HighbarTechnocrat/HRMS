<%@ Control Language="C#" AutoEventWireup="true" CodeFile="script.ascx.cs" Inherits="control_scriptl" %>
     <script src="<%=ReturnUrl("sitepath")%>/js/jsframework.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
       
        //var count = 1;
        //function lastAddedLiveFunc() {
        //    if (count < 50) {
        //        $('#lastPostsLoader').html('<img src="http://intranet.com.iis1100.shared-servers.com/themes/creative1.0/images/loading.gif"/>');
        //        $.ajax({
        //            type: 'POST',
        //            contentType: "application/json; charset=utf-8",
        //            url: 'default.aspx/GetMyWall',
        //            data: "{'page':'" + count + "'}",
        //            dataType: 'text',
        //            async: false,
        //            success: function (response) {
        //                var json = JSON.parse(response);
        //                $('.mywall').append(json["d"]);
        //            },
        //            error: function (xhr, status, error) {
        //                // alert(xhr.responseText);
        //            }
        //        });
        //        $('#lastPostsLoader').empty();
        //        count = count + 1;
        //    }
        //};
        //$(window).scroll(function () {
        //    var wintop = $(window).scrollTop(), docheight = $(document).height(), winheight = $(window).height();
        //    if ($(window).scrollTop() + $(window).height() == $(document).height()) {
        //        alert("hii");
        //        //lastAddedLiveFunc();
        //        //$("#MainContent_homebanner_m_uxmywall_lnknext").click();
        //        $("#MainContent_homebanner_m_uxmywall_lnknext").trigger("click");
        //    }
        //});
    });
</script>

<script>
    $(document).ready(function () { $("#back-top").hide(); $(function () {$(window).scroll(function () { if ($(this).scrollTop() > 100) { $("#back-top").fadeIn() } else { $("#back-top").fadeOut() } }); $("#back-top a").click(function () { $("body,html").animate({ scrollTop: 0 }, 800); return false }) }) })</script>
<%--<script type="text/javascript" src="<%=ReturnUrl("sitepathmain") %>js/combinedefault.js"></script>--%>
<script src="<%=ReturnUrl("sitepathmain") %>js/script.js" type="text/javascript"></script>
<script>
    $(document).ready(function () {
        $(".plus").click(function () {
            $(".plus_subdiv").toggle();
        });
    });
</script>
<script>
    $(document).ready(function () {
        $(".menu_square").hover(function () {
            $(".topheadban1").fadeToggle(300);
        }, function () {
            $(".topheadban1").fadeToggle(300);
        });
    });
</script>
<%-- Flex-Slidder --%>
<script src="<%=ReturnUrl("sitepathmain") %>js/jquery.flexslider-min.js"></script>
<script type="text/javascript" defer="defer">
    //$(function () {
    //    SyntaxHighlighter.all();
    //});
    $(window).load(function () {
        $('.flexslider').flexslider({
            animation: "slide",
            start: function (slider) {
                $('body').removeClass('loading');
            }
        });
    });
</script>
         <script>
             if (window.screen.width > 1000)
             {
                 var target = $('#MainContent_homebanner_uxcategorypanel');
                 var partial = (target.height()) * 0.5;
                 var div_position = target.height() - partial;

                 $(window).scroll(function () {
                     var y_position = $(window).scrollTop();
                     if (y_position > div_position) {
                         target.addClass('fixed');
                     }
                     else {
                         target.removeClass('fixed');
                     }
                 });
             }
</script>
<script>
    if (window.screen.width > 1000) {

        var target2 = $('#MainContent_homebanner_m_uxuserbirth');
        var partial2 = (target2.height()) * 0.0;
        var div_position2 = target2.height() - partial2;

        $(window).scroll(function () {
            var x_position2 = $(window).scrollTop();
            if (x_position2 > div_position2) {
                target2.addClass('fixed-right');
            }
            else {
                target2.removeClass('fixed-right');
            }
        });
    }

</script>  
<%-- <link rel="stylesheet" type="text/css" href ="<%=ReturnUrl("css") %>tooltip/tooltipster.css" media="all"/>
<script type="text/javascript" src="<%=ReturnUrl("sitepath")%>js/jquery.tooltipster.min.js"></script>
<script>
    $(document).ready(function () {
        $('.tooltip').tooltipster({
            contentAsHTML: true,
            animation: 'fade',
            delay: 200,
            theme: 'tooltipster-default',
            touchDevices: false,
            trigger: 'hover'
        });

    });
</script>--%>
