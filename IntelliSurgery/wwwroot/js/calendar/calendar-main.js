

calendarApp = Vue.createApp({
    data() {
        return {
            calendars: {},
            filters: {
                
            },
            fitlerValues: {},
            selectedFilter: "",
            selectedFilterValue:-1
        };
    },
    computed: {
        getFilterValueOptions() {
            var selectedFilter = this.selectedFilter;
            if (selectedFilter != "") {
                return filterValues[selectedFilter];
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
    initCalendar([]);
    var filterValues = await getDropDownListsRequest();
    calendarVueApp.filterValues = filterValues;
});
