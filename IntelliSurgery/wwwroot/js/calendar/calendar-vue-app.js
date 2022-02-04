calendarApp = Vue.createApp({
    data() {
        return {
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
            return this.surgeons;
        },
        getTableData() {
            return this.tableData;
        },
        

    },
    methods: {
        
        setSelectedEvent(appointment, calendarEventId) {

            this.selectedCalendarEventId = calendarEventId;
            

            const NA = "Not assigned";
            var selected = {
                appointmentId : appointment.id,
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

            this.selectedEvent = selected;
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
                await this.showCalendar(selectedFilter, selectedFilterValue);
            } else {
                displaySweetAlert("Choose filters");
            }
        },
        async renderAppoinmentsTable() {
            var selectedSurgeonId = this.selectedSurgeonId;
            if (selectedSurgeonId != -1) {
                var appointments = await getSurgeonAppointments(selectedSurgeonId);
                this.tableData = appointments;
                $("#calendar").hide();
                $("#appointments-table").show();
                $("#appointment-box").hide();
            }
        },
        async createSchedule() {
            var selectedSurgeonId = this.selectedSurgeonId;
            if (selectedSurgeonId != -1) {
                var isComplete = await CreateScheduleRequest(selectedSurgeonId);
                if (isComplete == true) {
                    Swal.fire(
                        'Successful!',
                        "Schedule created",
                        'success'
                    );
                    await this.showCalendar("surgeons", selectedSurgeonId);
                }
            }
        },
        async showCalendar(selectedFilter, selectedFilterValue) {
            var events = getCalendarEvents(selectedFilter, selectedFilterValue);
            if (events == null) {
                events = await getScheduledSurgeriesRequest(selectedFilter, selectedFilterValue);
            }
            $("#appointments-table").hide();
            $("#calendar").show();
            $("#appointment-box").hide();
            initCalendar(events);
        },
        async setStatusTo(newStatus) {

            var selectedEvent = this.selectedEvent;
            var calendarEventId = this.selectedCalendarEventId;

            //update backend
            var appointmentId = selectedEvent.appointmentId;
            var tableRecord = await updateAppointmentStatusRequest(appointmentId, newStatus); //write this function

            //update tableData
            this.tableData = updateTableData(this.tableData, tableRecord);

            //update calendars obj if
            if (calendarEventId != -1) {

                var calendarEvent = await getCalendarEventRequest(appointmentId);
                this.setSelectedEvent(calendarEvent);

                //update all calendars in different filters
                this.calendars = updateEventInAllCalendars(this.calendars, calendarEvent);

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
                initCalendar(updatedEvents);
            }
        }
    }

});

calendarVueApp = calendarApp.mount("#calendar-main");

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

function updateEventInAllCalendars(calendars, eventDTO) {
    var events = [];
    //surgeons
    const SURGEONS = "surgeons";
    events = getCalendarEvents(SURGEONS, eventDTO.surgeon.id);
    events = updateCalendarEventsArray(events, eventDTO);
    calendars[SURGEONS] = surgeonEvents;

    //surgeryTypes
    const SURGERY_TYPES = "surgeryTypes";
    events = getCalendarEvents(SURGERY_TYPES, eventDTO.surgeryType.id);
    calendars[SURGERY_TYPES] = updateCalendarEventsArray(events, eventDTO);

    //theatreTypes
    const THEATRE_TYPES = "theatreTypes";
    events = getCalendarEvents(THEATRE_TYPES, eventDTO.theatreType.id);
    calendars[THEATRE_TYPES] = updateCalendarEventsArray(events, eventDTO);

    //theatres
    const THEATRES = "theatres";
    events = getCalendarEvents(THEATRES, eventDTO.theatre.id);
    calendars[THEATRES] = updateCalendarEventsArray(events, eventDTO);

    return calendars;
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

function updateCalendarEventsArray(arr, event) {
    for (var i = 0; i < arr.length; i++) {
        if (arr[i].id == event.id) {
            arr[i].extendedProps = event.extendedProps;
            break;
        }
    }
    return arr;
}