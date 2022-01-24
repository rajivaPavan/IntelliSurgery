async function GetScheduledSurgeries() {
    var res = await axios.get("/api/CalendarApi/GetScheduledSurgeries");
    var dto = {};
    var surgeries = [];
    if (res.data.success == true) {
        dto = res.data.data;
        surgeries = dto.scheduledSurgeries;
    }
    return surgeries;
}

$(document).ready(async () => {
    var scheduledSurgeries = await GetScheduledSurgeries();
    scheduledSurgeries.forEach(s => {
        console.log(s);
    });
});
