var currentUpdateEvent;
var addStartDate;
var addEndDate;
var globalAllDay;

function updateEvent(event, element) {
    //alert(event.description);

    if ($(this).data("qtip")) $(this).qtip("destroy");

    currentUpdateEvent = event;

    $('#updatedialog').dialog('open');
    $("#eventName").val(event.title);
    $("#eventDesc").val(event.description);
    $("#eventId").val(event.id);
    $("#addStartDate2").val(event.start.format("DD MMMM YYYY"));

    //$("#eventStart").text("" + event.start.toLocaleString());

    if (event.end === null) {
        $("#addEndDate2").val("");
    }
    else {
        $("#addEndDate2").val(event.end.format("DD MMMM YYYY"));
    }

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
                    end: $("#addEndDate2").val()
                };

                if ($("#eventName").val() == "" || $("#eventDesc").val() == "" || $("#addStartDate2").val() == "" || $("#addEndDate2").val() == "") {
                    alert("All fields are mandatory");
                }
                else {
                    PageMethods.UpdateEvent(eventToUpdate, updateSuccess);
                    $(this).dialog("close");
                    //currentUpdateEvent.title = $("#eventName").val();
                    //currentUpdateEvent.description = $("#eventDesc").val();
                    //currentUpdateEvent.start = $("#addStartDate2").val().format("DD MMMM YYYY");
                    ////currentUpdateEvent.start=moment(currentUpdateEvent.start, 'DD MMMM YYYY').format("DD MMMM YYYY");
                    ////currentUpdateEvent.start = currentUpdateEvent.start.format("DD MMMM YYYY");
                    //currentUpdateEvent.end = $("#addEndDate2").val().format("DD MMMM YYYY");
                    ////currentUpdateEvent.end = currentUpdateEvent.end.format("DD MMMM YYYY")
                    ////currentUpdateEvent.end=moment(currentUpdateEvent.end, 'DD MMMM YYYY').format("DD MMMM YYYY");
                    //$('#calendar').fullCalendar('updateEvent', currentUpdateEvent);
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
                    allDay: isAllDay(addStartDate, addEndDate)
                };
                
                //if (checkForSpecialChars(eventToAdd.title) || checkForSpecialChars(eventToAdd.description)) {
                //    alert("please enter characters: A to Z, a to z, 0 to 9, spaces");
                //}
                //else {
                //alert("sending " + eventToAdd.title);

                if ($("#addEventName").val() == "" || $("#addEventDesc").val() == "" || $("#addStartDate").val() == "" || $("#addEndDate").val() == "") {
                    alert("All fields are mandatory");
                }
                else {
                    PageMethods.addEvent(eventToAdd, addSuccess(1));
                    $(".fc-event-hori").each(function () {
                        if ($(this).attr("style").includes("left: 1px;") == true) {
                            $(this).hide();
                        }
                    });
                    $("#addEventName").val("");
                    $("#addEventDesc").val("");
                    $("#addStartDate").val("");
                    $("#addEndDate").val("");
                    $(this).dialog("close");
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

