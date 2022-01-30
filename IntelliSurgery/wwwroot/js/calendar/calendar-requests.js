async function getScheduledSurgeriesRequest(filter, filterValue) {
    var res = await axios.get("/api/CalendarApi/GetScheduledSurgeries?filter="+filter+"&filterValue="+filterValue.toString());
    var dto = {};
    var events = [];
    if (res.data.success == true) {
        events = res.data.data;
    }
    return events;
}


async function getFilterValuesRequest() {
    var res = await axios.get("/api/AdminApi/GetHospitalData");
    var dto = {};
    var filterVals = {};
    if (res.data.success == true) {
        dto = res.data.data;
        filterVals.surgeons = dto.surgeons;
        filterVals.surgeryTypes = dto.surgeryTypes;
        filterVals.theatres = dto.theatres;
        filterVals.theatreTypes = dto.theatreTypes;
    }
    return filterVals;
}
