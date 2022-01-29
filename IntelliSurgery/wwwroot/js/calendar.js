document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridMonth',
        themeSystem: "bootstrap",
        views: {
            dayGridMonth: { // name of view
                titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
                // other view-specific options here
            }
        },
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'// will normally be on the right. if RTL, will be on the left
        },
        selectable: true,
        events: [
            { // this object will be "parsed" into an Event Object
                title: 'The Title', // a property!
                start: '2022-02-10T10:40:00',
                end: '2022-02-10T15:23:00', // a property! ** see important note below about 'end' **
            },
            { // this object will be "parsed" into an Event Object
                title: '2', // a property!
                start: '2022-02-10T13:40:00',
                end: '2022-02-10T15:21:00', // a property! ** see important note below about 'end' **
            }, {
                start: '2022-02-10T10:23:00',
                end: '2022-02-10T16:00:00',
                display: 'background'
            }   
            
        ]
    });

    calendar.render();
});