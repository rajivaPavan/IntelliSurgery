async function getDropDownListsRequest() {
    var res = await axios.get("/api/AppointmentApi/GetFormDropDownLists");
    var dto = {};
    var dropDownLists = {};
    if (res.data.success == true) {
        dto = res.data.data;
        dropDownLists.surgeons = dto.surgeons;
        dropDownLists.surgeryTypes = dto.surgeryTypes;
        dropDownLists.anesthesiaTypes = dto.anesthesias;
        dropDownLists.theatreTypes = dto.theatreTypes;
    }
    return dropDownLists;
}