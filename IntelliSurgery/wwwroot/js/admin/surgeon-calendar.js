function initSurgeonCalendar(calendarEvents) {

    var calendarEl = document.getElementById('surgeon-calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'timeGridWeek',
        themeSystem: "bootstrap",
        views: {
            dayGridMonth: { // name of view
                titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
                // other view-specific options here
            }
        },
        height: '80vh',
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

            sweetAlertBlockDetails(selected);
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

    return calendar;

}

function sweetAlertBlockDetails(block) {
    Swal.fire({
        title: '<strong>Working block</strong>',
        html: "<div>Surgeon: " + block.surgeon + "</div>" +
            "<div>Theatre: " + block.theatre + "</div>" +
            "<div>Start of shift:  " + block.startTime + "</div>" +
            "<div>End of shift: " + block.endTime + "</div>",
        showCloseButton: true,
        focusConfirm: false,
        confirmButtonText: 'Delete <i class="fas fa-trash"></i>',
        confirmButtonColor: '#d33'
    }).then((result) => {
        if (result.isConfirmed) {
            calendarVueApp.tryDeleteSelectedBlock+=1; //just to change the value, so that the watch function runs
        }
    })
    
}