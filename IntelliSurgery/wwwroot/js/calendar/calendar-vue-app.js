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
            surgeons:[]
        };
    },
    computed: {
        getSelectedFilterText() {
            var text = "a filter first";
            if (this.selectedFilter == "") {
                $("#filterValuesDropDown").prop("disabled", true);
                return text;
            }
            $("#filterValuesDropDown").prop("disabled", false);
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
        }

    },
    methods: {
        async renderCalendar() {
            var selectedFilter = this.selectedFilter;
            var selectedFilterValue = this.selectedFilterValue;
            if (selectedFilter != "" && selectedFilterValue != -1) {
                var events = getCalendarEvents(selectedFilter, selectedFilterValue);
                if (events == null) {
                    events = await getScheduledSurgeriesRequest(selectedFilter, selectedFilterValue);
                }
                initCalendar(events);
                $("#appointments-table").hide();
                $("#calendar").show();
                this.selectedEvent = null;

            } else {
                displaySweetAlert("Choose filters");
            }
        },
        async renderAppoinmentsTable() {
            var selectedSurgeonId = this.selectedSurgeonId;
            if (selectedSurgeonId != -1) {

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
    console.log(calendars);
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