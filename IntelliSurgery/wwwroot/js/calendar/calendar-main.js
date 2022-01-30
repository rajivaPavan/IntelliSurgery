
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


