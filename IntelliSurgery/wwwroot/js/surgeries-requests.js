async function GetTableDataRequest() {
    var res = await axios.get("/api/CalendarApi/GetTableData");
    var appointments = [];
    if (res.data.success == true) {
        appointments = res.data.data;
    }
    return appointments;
}

async function CreateScheduleRequest(theatreTypeId) {
    var res = await axios.post("/api/CalendarApi/CreateSchedule?typeId" + theatreTypeId);
    var IsCompleted = false;
    if (res.data.success == true) {
        IsCompleted = true;
    }
    return IsCompleted;
}