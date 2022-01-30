function initCalendar(calendarEvents) {

        var calendarEl = document.getElementById('calendar');

        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'timeGridWeek',
            themeSystem: "bootstrap",
            initialDate: "2022-02-10",
            views: {
                dayGridMonth: { // name of view
                    titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
                    // other view-specific options here
                }
            },
            selectable: true,
            editable: true,
            headerToolbar: {
                left: 'prev,next today',
                center: 'title',
                right: 'dayGridMonth,timeGridWeek,timeGridDay'// will normally be on the right. if RTL, will be on the left
            },
            events: calendarEvents
        });
        calendar.render();


}

var events = [
    { // this object will be "parsed" into an Event Object
        title: 'The Title', // a property!
        start: '2022-02-10T10:40:00',
        end: '2022-02-10T15:23:00',
        display: "auto"
    },
    { // this object will be "parsed" into an Event Object
        title: '2', // a property!
        start: '2022-02-10T13:40:00',
        end: '2022-02-10T15:21:00',
        display: "auto"
    }, {
        start: '2022-02-10T10:23:00',
        end: '2022-02-10T16:00:00',
        display: 'background'
    }

];

/* remove event code (need to create buttons)

var event = calendar.getEventById('1');  //Will return an Event Object if found, and null otherwise.
event.remove();
---------------------------------------------------
add event code (manually-if needed)

calendar.addEvent(event[, source]);
-------------------------------------------------------
get event code

calendar.getEvents()  //This method will return an array of Event Objects that FullCalendar has stored in client-side memory.*/