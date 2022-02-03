
function initCalendar(calendarEvents) {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'timeGridWeek',
        themeSystem: "bootstrap",
        views: {
            dayGridMonth: { // name of view
                titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
                // other view-specific options here
            }
        },
        height:"90vh",
        eventClick: function (info) {

            var event = info.event;
            var appointment = event.extendedProps;

            var selected = {
                surgeon: appointment.surgeon.name,
                patient: appointment.patient.name,
                surgery: appointment.surgeryType.name,
                priority: appointment.priorityLevel,
                theatre: appointment.theatre.name,
                startTime: event.start,
                endTime: event.end,
                duration: event.duration
            };

            calendarVueApp.selectedEvent = selected;
        },
        eventTextColor: "#000",
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'// will normally be on the right. if RTL, will be on the left
        },
        events: calendarEvents
        });
        calendar.render();


}


/* remove event code (need to create buttons)

var event = calendar.getEventById('1');  //Will return an Event Object if found, and null otherwise.
event.remove();
---------------------------------------------------
add event code (manually-if needed)

calendar.addEvent(event[, source]);
-------------------------------------------------------
get event code

calendar.getEvents()  //This method will return an array of Event Objects that FullCalendar has stored in client-side memory.*/