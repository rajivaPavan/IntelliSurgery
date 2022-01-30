calendarApp = Vue.createApp({
    data() {
        return {
            calendars: {},
            filters: [],
            fitlerValues: {},
            selectedFilter: "",
            selectedFilterValue:-1
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
                
            } else {
                displaySweetAlert("Choose filters");
            }
        }
    }

});

calendarVueApp = calendarApp.mount("#calendar-main");

$(document).ready(async () => {
    //init empty calendar
    var noEvents = [];
    initCalendar(noEvents);


    //init dropdowns
    var filters = initFilters();
    calendarVueApp.filters = filters;

    var filterValues = await getFilterValuesRequest();
    calendarVueApp.filterValues = filterValues;

    //init calendars array
    calendarVueApp.calendars = initCalendarsObj(filters, filterValues);

});


function initFilters() {
    var filters = [
        { text:"Surgeon", value:"surgeons" },
        { text:"Theatre", value:"theatres" },
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
                events:null
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