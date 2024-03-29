﻿
function initCalendar(calendarEvents) {

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'timeGridWeek',
        themeSystem: "bootstrap",
        initialDate:"2022-02-13",
        views: {
            dayGridMonth: { 
                titleFormat: { year: 'numeric', month: '2-digit', day: '2-digit' }
            },
            timeGrid: {
                nowIndicator: true
            }
        },
        height: "89vh",
        eventClick: function (info) {

            var event = info.event;
            var appointment = event.extendedProps;
            
            if (event.display != "background") {
                calendarVueApp.setSelectedEvent(appointment, event.id);
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

    return calendar;

}

