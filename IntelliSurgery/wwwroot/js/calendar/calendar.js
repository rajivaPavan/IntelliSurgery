
function initCalendar(calendarEvents) {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'timeGridWeek',
        themeSystem: "bootstrap",
        views: {
            dayGridMonth: { 
                titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
            }
        },
        height:"90vh",
        eventClick: function (info) {

            var event = info.event;
            var appointment = event.extendedProps;

            if (event.display != "background") {
                calendarVueApp.setSelectedEvent(appointment);
            }

        },
        eventTextColor: "#000",
        headerToolbar: {
            left: 'prev,next today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay'
        },
        events: calendarEvents
    });

    calendar.render();

}
