

calendarApp = Vue.createApp({
    data() {
        return {
            calendars: {},
            filters: initFilters(),
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

        }
    }

});

calendarVueApp = calendarApp.mount("#calendar-main");

$(document).ready(async () => {
    //init empty calendar
    var noEvents = [];
    initCalendar(noEvents);

    //init dropdowns
    var filterValues = await getFilterValuesRequest();
    calendarVueApp.filterValues = filterValues;
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