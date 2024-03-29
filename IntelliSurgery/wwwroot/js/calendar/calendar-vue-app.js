﻿const PENDING = 0;
const SCHEDULED = 1;
const CONFIRMED = 2;
const CANCELLED = 3;
const COMPLETED = 4;
const ONGOING = 5;
const POSTPONED = 6;

calendarApp = Vue.createApp({
    data() {
        return {
            calendar: null,
            calendars: {},
            filters: [],
            fitlerValues: {},
            selectedFilter: "",
            selectedFilterValue: -1,
            selectedEvent: null,
            selectedSurgeonId: -1,
            surgeons: [],
            tableData: [],
            selectedCalendarEventId: -1
        };
    },
    watch: {
        selectedFilter: function () {
            this.selectedSurgeonId = -1;
        },
        selectedFilterValue: function () {
            this.selectedSurgeonId = -1;
        },
        selectedSurgeonId: function () { hideAppointmentDetails(); }
    },
    computed: {
        getSelectedFilter() {
            return this.selectedFilter;
        },
        getSelectedFilterText() {
            var text = "Select filter first";
            if (this.selectedFilter == "") {
                $("#filterValuesSection").hide();
                return text;
            }
            $("#filterValuesSection").show();
            var selectedF = this.filters.find((filter) => {
                return filter.value == this.selectedFilter;
            });

            text = selectedF.text;
            return text;
        },
        getFilterValueOptions() {
            var selectedFilter = this.selectedFilter;
            if (selectedFilter != "") {
                return this.filterValues[selectedFilter];
            }
            return [];
        },
        getSelectedEvent() {
            return this.selectedEvent;
        },
        getSurgeons() {
            var surgeonsByType = {};

            this.surgeons.forEach((surgeon) => {
                var specialityName = surgeon.speciality.name;
                if (!(surgeonsByType.hasOwnProperty(specialityName))) {
                    surgeonsByType[specialityName] = [];
                }
                surgeonsByType[specialityName].push(surgeon);

            });
            return surgeonsByType;
            //return this.surgeons;
        },
        getTableData() {
            return this.tableData;
        },
        

    },
    methods: {
        canComplete(status) {
            return status == CONFIRMED || status == ONGOING;
        },
        canPostpone(status) {
            return status == SCHEDULED || status == CONFIRMED;
        },
        setSelectedEvent(appointment, calendarEventId) {

            this.selectedCalendarEventId = calendarEventId;
            this.selectedEvent = selectedEventCtor(appointment);
            $("#appointment-box").show();

        },
        getName(object) {
            if (object != null) {
                return object.name;
            }
            return "NA";
        },
        async renderCalendar() {
            var selectedFilter = this.selectedFilter;
            var selectedFilterValue = this.selectedFilterValue;
            if (selectedFilter != "" && selectedFilterValue != -1) {
                var events = null; //getCalendarEvents(selectedFilter, selectedFilterValue)
                await this.showCalendar(events,selectedFilter, selectedFilterValue);
            } else {
                displaySweetAlert("Choose filters");
            }
        },
        async renderAppoinmentsTable() {
            var selectedSurgeonId = this.selectedSurgeonId;
            if (selectedSurgeonId != -1) {
                $("#loading").show();
                var appointments = await getSurgeonAppointments(selectedSurgeonId);
                $("#loading").hide();
                this.tableData = appointments;
                this.calendar = null;
                $("#calendar").hide();
                $("#appointments-table").show();
                $("#appointment-box").hide();
            }
        },
        async createSchedule() {
            var selectedSurgeonId = this.selectedSurgeonId;
            if (selectedSurgeonId != -1) {
                $("#loading").show();
                var isComplete = await CreateScheduleRequest(selectedSurgeonId);
                $("#loading").hide();
                if (isComplete == true) {
                    Swal.fire(
                        'Successful!',
                        "Schedule created",
                        'success'
                    );
                    var events = null;
                    await this.showCalendar(events,"surgeons", selectedSurgeonId);
                }
            }
        },
        async showCalendar(events, selectedFilter, selectedFilterValue) {
            if (events == null) {
                $("#loading").show();
                events = await getScheduledSurgeriesRequest(selectedFilter, selectedFilterValue);
                $("#loading").hide();
                this.updateCalendarEvents(events, selectedFilter, selectedFilterValue);
            }
            $("#appointments-table").hide();
            $("#calendar").show();
            $("#appointment-box").hide();
            this.calendar = initCalendar(events);
        },
        updateCalendarEvents(events, searchFilter, searchFilterValue) {
            var calendars = this.calendars;
            var entities = calendars[searchFilter];
            for(var i = 0; i<entities.length; i++) {
            if (entities[i].id == searchFilterValue) {
                    entities[i].events = events;
                    break;
                }
            }
            calendars[searchFilter] = entities;
            this.calendars = calendars;
        },
        async setStatusTo(newStatus, event) {
            var newStatusName = $(event.target).text();

            var isProceed = await isProceedToSetStatus(newStatusName);

            if (isProceed) {
                var selectedEvent = this.selectedEvent;
                var calendarEventId = this.selectedCalendarEventId;

                //update backend
                var appointmentId = selectedEvent.appointmentId;
                var tableRecord = await updateAppointmentStatusRequest(appointmentId, newStatus); //write this function

                if (tableRecord != null) {
                    //update selectedEvent
                    this.selectedEvent = selectedEventCtor(tableRecord);

                    //update tableData
                    this.tableData = updateTableData(this.tableData, tableRecord);

                    //update calendars obj if
                    if (calendarEventId != -1) {

                        var calendarEvent = await getCalendarEventRequest(appointmentId);
                        calendarEvent.id = calendarEventId;

                        //update all calendars in different filters
                        await updateEventInAllCalendars(calendarEvent);

                        //get filters of the current calendar that has been loaded
                        var selectedFilter = "surgeons";
                        var selectedFilterValue = this.selectedSurgeonId;
                        if (this.selectedSurgeonId == -1) {
                            selectedFilter = this.selectedFilter;
                            selectedFilterValue = this.selectedFilterValue;
                        }
                        //get the events from the current calendar;
                        var updatedEvents = getCalendarEvents(selectedFilter, selectedFilterValue);

                        //re init fullcalendar by passing the necesary events;
                        this.calendar = initCalendar(updatedEvents);

                        //
                        Swal.fire(
                            'Appointment status updated',
                            'to ' + newStatusName,
                            'success'
                        )
                    }
                }
            }
            
            
        }
    }

});

calendarVueApp = calendarApp.mount("#calendar-main");

const LEFT_KEY = 37;
const RIGHT_KEY = 39;
$("html").keydown((e) => {
    if (calendarVueApp.calendar != null) {
        if (e.keyCode == LEFT_KEY) {
            calendarVueApp.calendar.prev();
        } else if (e.keyCode == RIGHT_KEY) {
            calendarVueApp.calendar.next();
        }
    }
});

async function isProceedToSetStatus(newStatusName) {
    var isProceed = false;
    await Swal.fire({
        icon: 'warning',
        title: 'Change appointment status',
        text: 'to ' + newStatusName,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Proceed'
    }).then((result) => {
        if (result.isConfirmed) {
            isProceed = true;
        }
    })
    return isProceed;
}

function initFilters() {
    var filters = [
        { text: "Surgeon", value: "surgeons" },
        { text: "Theatre", value: "theatres" },
        { text: "Theatre Type", value: "theatreTypes" },
        { text: "Surgery Type", value: "surgeryTypes" }
    ];
    return filters;
}

function initCalendarsObj(filters, filterValues) {
    var calendars = {};
    filters.forEach((filter) => {
        calendars[filter.value] = [];
        filterValues[filter.value].forEach((val) => {
            calendars[filter.value].push({
                id: val["id"],
                events: null
            });
        })

    })
    return calendars;
}

function getCalendarEvents(searchFilter, searchFilterValue) {
    var events = null;
    var calendars = calendarVueApp.calendars;
    var entities = calendars[searchFilter];
    var entity = entities.find((entity) => {
        return entity.id == searchFilterValue;
    });
    events = entity.events;
    return events;
}

function hideAppointmentDetails() {
    $("#appointment-box").hide();
    this.selectedEvent = null;
}

async function updateEventInAllCalendars(eventDTO) {

    var selectedFilter = "";
    var selectedFilterValue = 0;
    var events;

    //surgeons
    selectedFilter = "surgeons";
    selectedFilterValue = eventDTO.extendedProps.surgeon.id;
    events = await updateEventInRelevantCalendarAsync(eventDTO, selectedFilter, selectedFilterValue);
    calendarVueApp.updateCalendarEvents(events, selectedFilter, selectedFilterValue);

    //surgeryTypes
    selectedFilter = "surgeryTypes"
    selectedFilterValue = eventDTO.extendedProps.surgeryType.id;
    events = await updateEventInRelevantCalendarAsync(eventDTO, selectedFilter, selectedFilterValue);
    calendarVueApp.updateCalendarEvents(events, selectedFilter, selectedFilterValue);


    //theatreTypes
    selectedFilter = "theatreTypes";
    selectedFilterValue = eventDTO.extendedProps.theatreType.id;
    events = await updateEventInRelevantCalendarAsync(eventDTO, selectedFilter, selectedFilterValue);
    calendarVueApp.updateCalendarEvents(events, selectedFilter, selectedFilterValue);


    //theatres
    selectedFilter = "theatres";
    selectedFilterValue = eventDTO.extendedProps.theatre.id;
    events = await updateEventInRelevantCalendarAsync(eventDTO, selectedFilter, selectedFilterValue);
    calendarVueApp.updateCalendarEvents(events, selectedFilter, selectedFilterValue);

    return;
}

async function updateEventInRelevantCalendarAsync(event, selectedFilter, selectedFilterValue) {
    var events = []
    events = getCalendarEvents(selectedFilter, selectedFilterValue);
    if (events == null) {
        events = await getScheduledSurgeriesRequest(selectedFilter, selectedFilterValue);
    } else {
        events = updateEventInEventsArray(events, event);
    }
    return events;
}

function updateTableData(tableData, record) {
    tableData = updateArrayByElementId(tableData, record);
    return tableData;
}

function updateArrayByElementId(arr, element) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].id == element.id) {
            arr[i] = element;
            break;
        }
    }
    return arr;
}

function updateEventInEventsArray(arr, event) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].id == event.id) {
            arr[i].extendedProps = event.extendedProps;
            var stval = event.extendedProps.statusValue;
            if ( stval == CANCELLED || stval == POSTPONED) {
                arr[i].display = 'background';
                arr[i].backgroundColor = arr[i].color;
            }
            break;
        }
    }
    return arr;
}

function selectedEventCtor(appointment) {
    const NA = "Not assigned";
    var selected = {
        appointmentId: appointment.id,
        surgeon: appointment.surgeon.name,
        patient: appointment.patient.name,
        surgery: appointment.surgeryType.name,
        priority: appointment.priorityLevel,
        status: appointment.status,
        statusValue: appointment.statusValue,
        theatre: appointment.theatre != null ? appointment.theatre.name : NA,
        startTime: appointment.startTime ? appointment.startTime : NA,
        endTime: appointment.endTime ? appointment.endTime : NA,
        duration: appointment.duration ? appointment.duration : NA
    };
    return selected;
}