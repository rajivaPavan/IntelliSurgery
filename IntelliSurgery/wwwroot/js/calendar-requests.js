async function GetScheduledSurgeriesRequest(theaterType) {
    var res = await axios.get("/api/CalendarApi/GetScheduledSurgeries?type="+theaterType.toString());
    var dto = {};
    var surgeries = [];
    if (res.data.success == true) {
        dto = res.data.data;
        surgeries = dto.scheduledSurgeries;
    }
    return surgeries;
}

