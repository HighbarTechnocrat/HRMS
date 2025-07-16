<%@ Control Language="C#" AutoEventWireup="true" CodeFile="eventjs.ascx.cs" Inherits="control_scriptl" %>

<script src="<%=ReturnUrl("sitepath")%>/js/jsframework.js"  type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () { $("#back-top").hide(); $(function () { $(window).scroll(function () { if ($(this).scrollTop() > 100) { $("#back-top").fadeIn() } else { $("#back-top").fadeOut() } }); $("#back-top a").click(function () { $("body,html").animate({ scrollTop: 0 }, 800); return false }) }) })</script>

<script src="<%=ReturnUrl("sitepathmain") %>js/script.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/moment-2.8.1.min.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/jquery-2.1.1.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/jquery-ui-1.11.1.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/jquery.qtip-2.2.0.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/fullcalendar-2.0.3.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/calendarscript.js" type="text/javascript"></script>
<script src="<%=ReturnUrl("sitepath")%>js/eventcalendar/jquery-ui-timepicker-addon-1.4.5.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".plus").click(function () {
            $(".plus_subdiv").toggle();
        });
    });
</script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".menu_square").click(function () {
            $(".topheadban1").fadeToggle(300);
        });
       
    });
</script>
<%--<script type="text/javascript" src="<%=ReturnUrl("sitepathmain") %>js/combinedefault.js"></script>--%>
