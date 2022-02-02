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
        eventClick: function (info) {

            var event = info.event;
            var workingBlock = event.extendedProps;

            var selected = {
                workingBlockId: workingBlock.id,
                surgeon: workingBlock.surgeon.name,
                theatre: workingBlock.theatre.name,
                startTime: event.start,
                endTime: event.end,
                duration: event.duration,
                eventId: event.id,
            };

            calendarVueApp.selectedWorkingBlock = selected;
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