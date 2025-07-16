<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="eventcalendar.aspx.cs" Inherits="eventcalendar" %>

<%@ Register Src="~/control/eventjs.ascx" TagName="js" TagPrefix="ucjs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <link href="<%=ReturnUrl("css") %>cupertino/jquery-ui-1.10.3.min.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>cupertino/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="<%=ReturnUrl("css") %>jquery.qtip-2.2.0.css" rel="stylesheet" type="text/css" />
    <style>
    </style>
    <div id="calendar">
    </div>
    <%--    <div id="updatedialog" style="margin: 50px;display: none;"
        title="Update or Delete Event">
        <div class="box-background"></div>
        <table cellpadding="0" class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" /><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="10" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="eventStart" style="display:none;"></span>
                    <input id="addStartDate2" type="text" />
                </td>
            </tr>
            <tr>
                <td class="alignRight">
                    End: </td>
                <td class="alignLeft">
                    <span id="eventEnd" style="display:none;"></span>
                    <input id="addEndDate2" type="text" />
                    <input type="hidden" id="eventId" /></td>
            </tr>
        </table>
    </div>
    <div id="addDialog" style="margin: 50px;" title="Add Event">
    <table cellpadding="0" class="style1">
            <tr>
                <td class="alignRight">
                    Name:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text"/><br /></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3" ></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">
                    Start:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate" style="display:none;"></span>
                    <input id="addStartDate" type="text" />
                </td>
            </tr>
            <tr>
                <td class="alignRight">
                    End:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate" style="display:none;"></span>
                    <input id="addEndDate" type="text" />
                </td>
            </tr>
        </table>
        
    </div>--%>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    <ucjs:js ID="cntrljs" runat="server" />
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>procs/myaccountpanel.css" />
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>procs/editprofile.css" />
    <%-- <script type='text/javascript' src='<%=ReturnUrl("sitepathmain") %>js/datepicker/jquery-ui.js'></script>--%>
    <script>
        $("#addStartDate,#addStartDate2,#addEndDate,#addEndDate2").datepicker({
            dateFormat: "dd MM yy",
            changeMonth: true,
            changeYear: true,
            yearRange: "1950:2050",
            //minDate: new Date()
        });
        $('#calendar').fullCalendar({
            theme: false,
            header: {
                left: 'prev,next,prevYear,nextYear today',
                center: 'title',
                // right: 'month,agendaWeek,agendaDay,'
                right: ''
            },
            defaultView: 'month',
            eventClick: updateEvent,
            selectable: true,
            eventLimit: false,
            views: {
                agenda: {
                    eventLimit: 2 // adjust to 6 only for agendaWeek/agendaDay
                }
            },
            selectHelper: true,
            select: selectDate,
            editable: false,
            windowResize: true,
            events: "JsonResponse.ashx",
            eventDrop: eventDropped,
            eventOverlap: false,
            eventResize: eventResized,
            allDay: false,
            eventRender: function (event, element) {
                //alert(event.title);
                element.qtip({
                    content: {
                        text: qTipText(event.start, event.end, event.description),
                        title: '<strong>' + event.title + '</strong>'
                    },
                    position: {
                        my: 'bottom center',
                        at: 'top center'
                    },
                    style: { classes: 'qtip-shadow qtip-rounded' }
                });
            },

        });
    </script>
</asp:Content>

