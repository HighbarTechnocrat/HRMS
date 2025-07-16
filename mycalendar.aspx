<%@ Page Title="" Language="C#" MasterPageFile="~/cms.master" AutoEventWireup="true" CodeFile="mycalendar.aspx.cs" Inherits="eventcalendar" %>

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
    <div id="updatedialog" style="margin: 50px; display: none;"
        title="Update or Delete Event">
        <div class="box-background"></div>
        <table cellpadding="0" class="style1 updateevent">
            <tr>
                <%--sagar change name to subject in below line 4jan2018--%>
                <td class="alignRight">Subject:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="30" rows="8"></textarea></td>
            </tr>
            <tr class="start">
                <td class="alignRight">Start Date:</td>
                <td class="alignLeft">
                    <span id="eventStart" style="display: none;"></span>
                    <input id="addStartDate2" type="text" readonly="readonly" />
                </td>
            </tr>
            <tr class="start">
                <td class="alignRight">End Date: </td>
                <td class="alignLeft">
                    <span id="eventEnd" style="display: none;"></span>
                    <input id="addEndDate2" type="text" readonly="readonly" />
                    <input type="hidden" id="eventId" /></td>
            </tr>
            <asp:Panel ID="pnlupdtrem" runat="server">
                <tr class="startrtime">
                    <td class="alignRight">
                        <span class="reminder">Meeting Time:</span>
                    </td>
                    <td class="alignLeft">
                        <%--<input id="txtrtime" type="text"/>--%>
                        <asp:DropDownList ID="addddlrtime" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                            <asp:ListItem Value="1:00">1:00</asp:ListItem>
                            <asp:ListItem Value="1:30">1:30</asp:ListItem>
                            <asp:ListItem Value="2:00">2:00</asp:ListItem>
                            <asp:ListItem Value="2:30">2:30</asp:ListItem>
                            <asp:ListItem Value="3:00">3:00</asp:ListItem>
                            <asp:ListItem Value="3:30">3:30</asp:ListItem>
                            <asp:ListItem Value="4:00">4:00</asp:ListItem>
                            <asp:ListItem Value="4:30">4:30</asp:ListItem>
                            <asp:ListItem Value="5:00">5:00</asp:ListItem>
                            <asp:ListItem Value="5:30">5:30</asp:ListItem>
                            <asp:ListItem Value="6:00">6:00</asp:ListItem>
                            <asp:ListItem Value="6:30">6:30</asp:ListItem>
                            <asp:ListItem Value="7:00">7:00</asp:ListItem>
                            <asp:ListItem Value="7:30">7:30</asp:ListItem>
                            <asp:ListItem Value="8:00">8:00</asp:ListItem>
                            <asp:ListItem Value="8:30">8:30</asp:ListItem>
                            <asp:ListItem Value="9:00">9:00</asp:ListItem>
                            <asp:ListItem Value="9:30">9:30</asp:ListItem>
                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="addddlrtimeval" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                
<%--sagar added style="display:none" in below div for hiding select user display 4jan2018 when adding new event--%>
                <tr class="select1" style="display:none">

                    <td class="alignRight">Select User:
                    </td>
                    <td class="alignLeft">
                                            <div class="example example_tagclass">
                        <div class="bs-example">
                            <asp:HiddenField ID="addvalue" runat="server" />
                            <asp:HiddenField ID="addarray" runat="server" />
                        </div>
                    </div>
                        <input id="addtxtuser" class="txtcomposetitle txtsearch" runat="server" type="text" />
                        <asp:HiddenField ID="addhvalue" runat="server" />
                    </td>
                </tr>

            </asp:Panel>
        </table>
    </div>
    <div id="addDialog" style="margin: 50px;" title="Add Event">
        <table cellpadding="0" class="style1 addevent">
            <tr>
                <td class="alignRight">
                    <%--sagar change name to subject in below line 4jan2018--%>
                    <font color="red">*</font>Subject: </td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">
                    <font color="red">*</font>Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="30" rows="3"></textarea></td>
            </tr>
            <tr class="start">
                <td class="alignRight">
                    <font color="red">*</font>Start Date:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate" style="display: none;"></span>
                    <input id="addStartDate" type="text" readonly="readonly" />
                </td>
            </tr>
            <tr class="start">
                <td class="alignRight">
                    <font color="red">*</font>End Date:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate" style="display: none;"></span>
                    <input id="addEndDate" type="text" readonly="readonly" />
                </td>
            </tr>

            <asp:Panel ID="pnlreminder" runat="server">
                <tr class="startrtime">
                    <td class="alignRight">
                        <span class="reminder">Meeting Time:</span>
                    </td>
                    <td class="alignLeft">
                        <%--<input id="txtrtime" type="text"/>--%>
                        <asp:DropDownList ID="txtrtime" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                            <asp:ListItem Value="1:00">1:00</asp:ListItem>
                            <asp:ListItem Value="1:30">1:30</asp:ListItem>
                            <asp:ListItem Value="2:00">2:00</asp:ListItem>
                            <asp:ListItem Value="2:30">2:30</asp:ListItem>
                            <asp:ListItem Value="3:00">3:00</asp:ListItem>
                            <asp:ListItem Value="3:30">3:30</asp:ListItem>
                            <asp:ListItem Value="4:00">4:00</asp:ListItem>
                            <asp:ListItem Value="4:30">4:30</asp:ListItem>
                            <asp:ListItem Value="5:00">5:00</asp:ListItem>
                            <asp:ListItem Value="5:30">5:30</asp:ListItem>
                            <asp:ListItem Value="6:00">6:00</asp:ListItem>
                            <asp:ListItem Value="6:30">6:30</asp:ListItem>
                            <asp:ListItem Value="7:00">7:00</asp:ListItem>
                            <asp:ListItem Value="7:30">7:30</asp:ListItem>
                            <asp:ListItem Value="8:00">8:00</asp:ListItem>
                            <asp:ListItem Value="8:30">8:30</asp:ListItem>
                            <asp:ListItem Value="9:00">9:00</asp:ListItem>
                            <asp:ListItem Value="9:30">9:30</asp:ListItem>
                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlrtime" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
<%--sagar added style="display:none" in below div for hiding select user display 4jan2018 when updating new event--%>

                <tr class="select1" style="display:none ">
                    <div class="example example_tagclass">
                        <div class="bs-example">
                            <asp:HiddenField ID="hdvalue" runat="server" />
                            <asp:HiddenField ID="hdarray" runat="server" />
                        </div>
                    </div>
                    <td class="alignRight">Select User:
                    </td>
                    <td class="alignLeft">
                        <input id="txtTo1" class="txtcomposetitle txtsearch" runat="server" type="text" />
                        <asp:HiddenField ID="hfCustomerId" runat="server" />
                    </td>
                </tr>

                <tr id="repeat" runat="server" visible="false">
                    <td class="alignRight">Repeat:
                    </td>
                    <td class="alignLeft">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="repeat">
                    <td class="alignRight">
                        <font color="red">*</font>Reminder From:
                    </td>
                    <td class="alignLeft">
                        <%--<input id="txtrfrom" type="text"/>--%>
                        <asp:DropDownList ID="txtrfrom" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                            <asp:ListItem Value="1:00">1:00</asp:ListItem>
                            <asp:ListItem Value="1:30">1:30</asp:ListItem>
                            <asp:ListItem Value="2:00">2:00</asp:ListItem>
                            <asp:ListItem Value="2:30">2:30</asp:ListItem>
                            <asp:ListItem Value="3:00">3:00</asp:ListItem>
                            <asp:ListItem Value="3:30">3:30</asp:ListItem>
                            <asp:ListItem Value="4:00">4:00</asp:ListItem>
                            <asp:ListItem Value="4:30">4:30</asp:ListItem>
                            <asp:ListItem Value="5:00">5:00</asp:ListItem>
                            <asp:ListItem Value="5:30">5:30</asp:ListItem>
                            <asp:ListItem Value="6:00">6:00</asp:ListItem>
                            <asp:ListItem Value="6:30">6:30</asp:ListItem>
                            <asp:ListItem Value="7:00">7:00</asp:ListItem>
                            <asp:ListItem Value="7:30">7:30</asp:ListItem>
                            <asp:ListItem Value="8:00">8:00</asp:ListItem>
                            <asp:ListItem Value="8:30">8:30</asp:ListItem>
                            <asp:ListItem Value="9:00">9:00</asp:ListItem>
                            <asp:ListItem Value="9:30">9:30</asp:ListItem>
                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlrfrom" runat="server">
                            <asp:ListItem Value="AM" Selected="True">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="repeat">
                    <td class="alignRight">
                        <font color="red">*</font>Reminder To:
                    </td>
                    <td class="alignLeft">
                        <%--<input id="txtrto" type="text" />--%>
                        <asp:DropDownList ID="txtrto" runat="server" CssClass="msgselect">
                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                            <asp:ListItem Value="1:00">1:00</asp:ListItem>
                            <asp:ListItem Value="1:30">1:30</asp:ListItem>
                            <asp:ListItem Value="2:00">2:00</asp:ListItem>
                            <asp:ListItem Value="2:30">2:30</asp:ListItem>
                            <asp:ListItem Value="3:00">3:00</asp:ListItem>
                            <asp:ListItem Value="3:30">3:30</asp:ListItem>
                            <asp:ListItem Value="4:00">4:00</asp:ListItem>
                            <asp:ListItem Value="4:30">4:30</asp:ListItem>
                            <asp:ListItem Value="5:00">5:00</asp:ListItem>
                            <asp:ListItem Value="5:30">5:30</asp:ListItem>
                            <asp:ListItem Value="6:00">6:00</asp:ListItem>
                            <asp:ListItem Value="6:30">6:30</asp:ListItem>
                            <asp:ListItem Value="7:00">7:00</asp:ListItem>
                            <asp:ListItem Value="7:30">7:30</asp:ListItem>
                            <asp:ListItem Value="8:00">8:00</asp:ListItem>
                            <asp:ListItem Value="8:30">8:30</asp:ListItem>
                            <asp:ListItem Value="9:00">9:00</asp:ListItem>
                            <asp:ListItem Value="9:30">9:30</asp:ListItem>
                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlrto" runat="server">
                            <asp:ListItem Value="AM" Selected="True">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="repeat">
                    <td class="alignRight">Interval Type:
                    </td>
                    <td class="alignLeft">
                        <asp:DropDownList ID="ddltyp" runat="server">
                            <asp:ListItem Selected="True" Text="Select Type" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Hours" Value="H"></asp:ListItem>
                            <asp:ListItem Text="Minutes" Value="M"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="repeat">
                    <td class="alignRight">
                        <font color="red">*</font>Interval Time:
                    </td>
                    <td class="alignLeft">
                        <input id="txtinterval" type="text" disabled="disabled" />
                        <span id="lblint" runat="server" style="display: none;"></span>
                    </td>
                </tr>
            </asp:Panel>
        </table>

    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    <asp:HiddenField ID="hduser" runat="server" />
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>procs/myaccountpanel.css" />
    <link rel="stylesheet" type="text/css" href="<%=ReturnUrl("css") %>procs/editprofile.css" />
    <ucjs:js ID="cntrljs" runat="server" />
    <script type="text/javascript" defer="defer">
        $(document).ready(function () {
            $("#addStartDate").datepicker({
                dateFormat: 'dd MM yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "1950:2050",
                //minDate: 0,
                //maxDate: "+2Y",
                onSelect: function (selected) {
                    $("#addEndDate").datepicker("option", "minDate", selected);
                }
            });
            $("#addEndDate").datepicker({
                dateFormat: 'dd MM yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "1950:2050",
                //minDate: 0,
                //maxDate: "+2Y",
                onSelect: function (selected) {
                    $("#addStartDate").datepicker("option", "maxDate", selected);
                }
            });
            $("#addStartDate2").datepicker({
                dateFormat: 'dd MM yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "1950:2050",
                //minDate: 0,
                //maxDate: "+2Y",
                onSelect: function (selected) {
                    $("#addEndDate2").datepicker("option", "minDate", selected);
                }
            });
            $("#addEndDate2").datepicker({
                dateFormat: 'dd MM yy',
                changeMonth: true,
                changeYear: true,
                yearRange: "1950:2050",
                //minDate: 0,
                //maxDate: "+2Y",
                onSelect: function (selected) {
                    $("#addStartDate2").datepicker("option", "maxDate", selected);
                }
            });
        });
        $('#calendar').fullCalendar({

            theme: false,
            header: {
                left: 'prev,next,prevYear,nextYear today',
                center: 'title',
                //right: 'month,agendaWeek,agendaDay,'
                right: ''
            },
            defaultView: 'month',
            eventClick: updateEvent,
            aspectRatio: 1,
            //displayEventEnd: false,
            selectable: true,
            allDay: true,
            //overlap: false,
            select: selectDate,
            //editable: false,
            //windowResize: true,
            events: "<%=ReturnUrl("sitepathmain")%>MyJsonResponse.ashx?id=" + $("#MainContent_hduser").val(),
            //eventDrop: eventDropped,
            //eventOverlap: false,
            //eventResize: eventResized,
            //eventRepeat: false,
            globalAllDay: true,
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
            }

        });
    </script>
    <script type="text/javascript" defer="defer">
        var typ;
        $(document).ready(function () {
            $(".repeat").hide();

            $('#MainContent_ddltyp').change(function () {
                typ = $("#MainContent_ddltyp option:selected").val();

                if (typ == "H" || typ == "M") {
                    $("#txtinterval").removeAttr("disabled");
                }
                if (typ == "0") {
                    $("#txtinterval").attr("disabled", "disabled");
                }
            });
        });

        $(window).load(function () {
            $(".fc-event-hori").each(function () {
                if ($(this).attr("style").indexOf("left: 1px;")>=0) {
                    $(this).hide();
                }
            });
        })
    </script>
    <script type="text/javascript">
        function Check(textBox, maxLength) {
            if (textBox.value.length > maxLength) {
                alert("Max characters allowed are " + maxLength);
                textBox.value = textBox.value.substr(0, maxLength);
            }
        }
    </script>
    <link rel="stylesheet" href="<%=ReturnUrl("css") %>autocomplete/app.css" />
    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/typeahead.bundle.min.js"></script>
    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/bootstrap-tagsinput.min.js"></script>
<%--    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/app.js"></script>--%>
    <script src="<%=ReturnUrl("sitepath") %>js/autocomplete/app2.js"></script>
    <asp:Label ID="ltmsg" runat="server" Visible="true"></asp:Label>
</asp:Content>

