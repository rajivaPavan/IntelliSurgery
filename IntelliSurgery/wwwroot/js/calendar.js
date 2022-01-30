document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        themeSystem: "bootstrap",

        /*views: {                                                                      //no need to write this code,cuz headerToolbar is given
            dayGridMonth: { // name of view
                titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
                // other view-specific options here
            }
        },*/

        //if needed can use a footerToolbar also

        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },

        selectable: true,
        editable: true,
        navlinks: true,  // can click day/week names to navigate views
        events: [
            { // this object will be "parsed" into an Event Object
                id: '1',
                title: 'The Title', // a property!
                start: '2022-02-10T10:40:00',
                end: '2022-02-10T15:23:00', // a property! ** see important note below about 'end' **
            },
            { 
                id: '2',
                title: '2', // a property!
                start: '2022-02-10T13:40:00',
                end: '2022-02-10T15:21:00', // a property! ** see important note below about 'end' **
            },
            {
                id: '3',
                title: 'title',
                start: '2022-02-10T10:23:00',
                end: '2022-02-10T16:00:00',
                display: 'background'
            }   
            
        ]
    });
    
    calendar.render();
});

/* remove event code (need to create buttons)

var event = calendar.getEventById('1');  //Will return an Event Object if found, and null otherwise.
event.remove();
---------------------------------------------------
add event code (manually-if needed)

calendar.addEvent(event[, source]);
-------------------------------------------------------
get event code

calendar.getEvents()  //This method will return an array of Event Objects that FullCalendar has stored in client-side memory.*/