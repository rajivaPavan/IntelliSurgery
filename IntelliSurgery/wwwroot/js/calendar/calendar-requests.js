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

async function getSurgeonAppointments(surgeonId) {
    var res = await axios.post("/api/SurgeriesApi/GetTableData?surgeonId=" + surgeonId);
    
    var appointments = [];
    if (res.data.success == true) {
        appointments = res.data.data;
    }
    return appointments;
}


async function CreateScheduleRequest(surgeonId) {
    var res = await axios.post("/api/SurgeriesApi/CreateSchedule?surgeonId=" + surgeonId);
    var IsCompleted = false;
    if (res.data.success == true) {
        IsCompleted = true;
    }
    return IsCompleted;
}