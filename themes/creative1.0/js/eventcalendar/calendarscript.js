var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;

function updateEvent(event, element) {
    //alert(event.description);

    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;
    $("#MainContent_addtxtuser").val(event.taguser);
    $('#updatedialog').dialog('open');
    $("#eventName").val(event.title);
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    $("#addStartDate2").val(event.start.format("DD MMMM YYYY"));
    if (event.rtime != null) {
        $('#MainContent_addddlrtime').val(event.rtime)
    }
    else
    {

    }
    if (event.ddlrtime != null) {
        $('#MainContent_addddlrtimeval').val(event.ddlrtime)
    }
    else {

    }
   // $("#MainContent_addddlrtime").text(event.rtime).attr("selected","selected");
    //$("#MainContent_addddlrtimeval").text(event.ddlrtime).attr("selected","selected");
    $("#MainContent_addarray").val();
    $("#MainContent_addvalue").val(event.taguser);
    $("#MainContent_addhvalue").val(event.taguser);
    if (event.end === null) {
        $("#addEndDate2").val("");
    }
    else {
        $("#addEndDate2").val(event.end.format("DD MMMM YYYY"));
    }
    var users = $("#MainContent_addhvalue").val();
    if (users.trim().length > 0)
    {
        users = $("#MainContent_addhvalue").val().split(',');
        var text = "";
        for (i = 0; i < users.length; i++) {
            text += "<span class='tag label label-primary'>" + users[i] + "<span data-role='remove'></span></span>";
        }
        $(".bootstrap-tagsinput .label-primary").remove();
        $("#MainContent_ltmsg").html("<script type='text/javascript'>jQuery(\"" + text + "\").insertBefore(jQuery('.twitter-typeahead'));jQuery('.label-primary span').click(function(){$(this).parent().remove();});</script>");
    }
    //$("#eventStart").text("" + event.start.toLocaleString());


    return false;
}

function updateSuccess(updateResult) {
    //alert(updateResult);
}

function deleteSuccess(deleteResult) {
}

function addSuccess(addResult) {
// if addresult is -1, means event was not added
//    alert("added key: " + addResult);

    if (addResult != -1) {
        $('#calendar').fullCalendar('renderEvent',
						{
						    title: $("#addEventName").val(),
						    start: addStartDate,
						    end: addEndDate,
						    id: addResult,
						    description: $("#addEventDesc").val(),
						    allDay: globalAllDay
						},
						true // make the event "stick"
					);


        $('#calendar').fullCalendar('unselect');
    }
}
function UpdateTimeSuccess(updateResult) {
    //alert(updateResult);
}
function selectDate(start, end, allDay) {

    $('#addDialog').dialog('open');
    //$("#addEventStartDate").text("" + start.toLocaleString());
    //$("#addEventEndDate").text("" + end.toLocaleString());
    $("#addStartDate2").val(start.format("DD MMMM YYYY"));
    $("#addEndDate2").val(end.format("DD MMMM YYYY"));
    addStartDate = start;
    addEndDate = end;
    globalAllDay = allDay;
    //alert(allDay);
}
function updateEventOnDropResize(event, allDay) {

    //alert("allday: " + allDay);
    var eventToUpdate = {
        id: event.id,
        start: event.start
    };

    // FullCalendar 1.x
    //if (allDay) {
    //    eventToUpdate.start.setHours(0, 0, 0);
    //}

    if (event.end === null) {
        eventToUpdate.end = eventToUpdate.start;
    }
    else {
        eventToUpdate.end = event.end;

        // FullCalendar 1.x
        //if (allDay) {
        //    eventToUpdate.end.setHours(0, 0, 0);
        //}
    }

    // FullCalendar 1.x
    //eventToUpdate.start = eventToUpdate.start.format("dd-MM-yyyy hh:mm:ss tt");
    //eventToUpdate.end = eventToUpdate.end.format("dd-MM-yyyy hh:mm:ss tt");

    // FullCalendar 2.x
    var endDate;
    if (!event.allDay) {
        endDate = new Date(eventToUpdate.end + 60 * 60000);
        endDate = endDate.toJSON();
    }
    else {
        endDate = eventToUpdate.end.toJSON();
    }

    eventToUpdate.start = eventToUpdate.start.toJSON();
    eventToUpdate.end = eventToUpdate.end.toJSON(); //endDate;
    eventToUpdate.allDay = event.allDay;

    PageMethods.UpdateEventTime(eventToUpdate, UpdateTimeSuccess);
}
function eventDropped(event, dayDelta, minuteDelta, allDay, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    // FullCalendar 1.x
    //updateEventOnDropResize(event, allDay);

    // FullCalendar 2.x
    updateEventOnDropResize(event);
}
function eventResized(event, dayDelta, minuteDelta, revertFunc) {
    if ($(this).data("qtip")) $(this).qtip("destroy");

    updateEventOnDropResize(event);
}
function checkForSpecialChars(stringToCheck) {
    var pattern = /[^A-Za-z0-9 ]/;
    return pattern.test(stringToCheck); 
}

function checkForNumeric(stringToCheck)
{
    var pattern=/[^0-9]/
    return pattern.test(stringToCheck);
}

function isAllDay(startDate, endDate) {
    var allDay;

    if (startDate.format("HH:mm:ss") == "00:00:00" && endDate.format("HH:mm:ss") == "00:00:00") {
        allDay = true;
        globalAllDay = true;
    }
    else {
        allDay = false;
        globalAllDay = false;
    }
    
    return allDay;
}
function qTipText(start, end, description) {
    var text;

    if (end !== null)
        text = "<strong>Start:</strong> " + start.format("DD MMMM YYYY") + "<br/><strong>End:</strong> " + end.format("DD MMMM YYYY") + "<br/><br/>" + description;
    else
        text = "<strong>Start:</strong> " + start.format("DD MMMM YYYY") + "<br/><strong>End:</strong><br/><br/>" + description;

    return text;
}
$(document).ready(function() {
    // update Dialog
    $('#updatedialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "update": function() {
                //alert(currentUpdateEvent.title);
                var eventToUpdate = {
                    id: currentUpdateEvent.id,
                    title: $("#eventName").val(),
                    description: $("#eventDesc").val(),
                    start: $("#addStartDate2").val(),
                    end: $("#addEndDate2").val(),
                    rtime: $("#MainContent_addddlrtime option:selected").val(),
                    ddlrtime: $("#MainContent_addddlrtimeval option:selected").val(),
                    taguser: $("#MainContent_addhvalue").val(),
                    hidden: $("#MainContent_addhvalue").val()
                };

                if ($("#eventName").val() == "" || $("#eventDesc").val() == "" || $("#addStartDate2").val() == "" || $("#addEndDate2").val() == "") {
                    alert("All fields are mandatory");
                }
                else {
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess(1));
                    $(this).dialog("close");
                    //currentUpdateEvent.title = $("#eventName").val();
                    //currentUpdateEvent.description = $("#eventDesc").val();
                    //currentUpdateEvent.start = $("#addStartDate2").val().format("DD MMMM YYYY");
                    //currentUpdateEvent.start=moment(currentUpdateEvent.start, 'DD MMMM YYYY').format("DD MMMM YYYY");
                    //currentUpdateEvent.start = currentUpdateEvent.start.format("DD MMMM YYYY");
                    //currentUpdateEvent.end = $("#addEndDate2").val().format("DD MMMM YYYY");
                    //currentUpdateEvent.end = currentUpdateEvent.end.format("DD MMMM YYYY")
                    //currentUpdateEvent.end=moment(currentUpdateEvent.end, 'DD MMMM YYYY').format("DD MMMM YYYY");
                    $('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
                }

            },
            "delete": function() {

                if (confirm("do you really want to delete this event?")) {

                    PageMethods.deleteEvent($("#eventId").val(), deleteSuccess);
                    $(this).dialog("close");
                    $('#calendar').fullCalendar('removeEvents', $("#eventId").val());
                }
            }
        }
    });

    //add dialog
    $('#addDialog').dialog({
        autoOpen: false,
        width: 470,
        buttons: {
            "Add": function() {

                //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                var eventToAdd = {
                    title: $("#addEventName").val(),
                    description: $("#addEventDesc").val(),

                    // FullCalendar 1.x
                    //start: addStartDate.format("dd-MM-yyyy hh:mm:ss tt"),
                    //end: addEndDate.format("dd-MM-yyyy hh:mm:ss tt")
                    // FullCalendar 2.x
                    start: $("#addStartDate").val(),
                    end: $("#addEndDate").val(),
                    allDay: isAllDay(addStartDate, addEndDate),

                    rtime: $("#MainContent_txtrtime option:selected").val(),
                    ddlrtime: $("#MainContent_ddlrtime option:selected").val(),
                    //repeat: $("#MainContent_ddlstatus option:selected").val(),
                    rfrom: $("#MainContent_txtrfrom option:selected").val(),
                    //ddlrfrom: $("#MainContent_ddlrfrom option:selected").val(),
                    //ddlrto: $("#MainContent_ddlrto option:selected").val(),
                    //rto: $("#MainContent_txtrto option:selected").val(),
                    //inttype: $("#MainContent_ddltyp option:selected").val(),
                    //inttime: $("#txtinterval").val(),
                    taguser: $("#MainContent_hdvalue").val(),
                    hidden: $("#MainContent_hdvalue").val()
                };
                
                //if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
                //    alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                //}
                //else {
                //alert("sending " + eventToAdd.title);

                //if (checkForNumeric(eventToAdd.inttime))
                //{
                //    alert("Please enter only numeric value. No space allowed.");
                //}

                if ($("#addEventName").val() == "" || $("#addEventDesc").val() == "" || $("#addStartDate").val() == "" || $("#addEndDate").val() == "") {
                    alert("All (*) fields are mandatory");
                }
                else {
                    PageMethods.addEvent(eventToAdd, addSuccess(1));
                    $(".fc-event-hori").each(function () {
                        if ($(this).attr("style").indexOf("left: 1px;")>=0) {
                            $(this).hide();
                        }
                    });
                    $(".label-danger").remove();
                    $(".label-warning").remove();
                    $("#addEventName").val("");
                    $("#addEventDesc").val("");
                    $("#addStartDate").val("");
                    $("#addEndDate").val("");
                    // reminder code end here
                    $(this).dialog("close");
                    $("#MainContent_txtTo1").val("");
                    //New code for reminder start
                    $('#MainContent_txtrtime').val($("#MainContent_txtrtime option:contains('12:00')").val());
                    $('#MainContent_ddlrtime').val($("#MainContent_ddlrtime option:contains('AM')").val());
                    //$('#MainContent_txtrfrom').val($("#MainContent_ddlrtime option:contains('12:00')").val());
                    //$('#MainContent_ddlrfrom').val($("#MainContent_ddlrfrom option:contains('AM')").val());
                    //$('#MainContent_txtrto').val($("#MainContent_ddlrtime option:contains('12:00')").val());
                    //$('#MainContent_ddlrto').val($("#MainContent_ddlrto option:contains('AM')").val());
                    //$('#MainContent_ddlstatus').val($("#MainContent_ddlstatus option:contains('No')").val());
                    //$('#MainContent_ddltyp').val($("#MainContent_ddltyp option:contains('Select Type')").val());
                     
                    $("#MainContent_hdarray").val("");
                    $("#MainContent_hdvalue").val("");

                  
                   
                }
                
                //}
            }
        }
    }); 

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    var options = {
        weekday: "long", year: "numeric", month: "short",
        day: "numeric", hour: "2-digit", minute: "2-digit"
    };

   
});

