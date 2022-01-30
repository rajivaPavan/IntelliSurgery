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

async function saveHospitalDataRequest(hospitalData) {
    // hospitalData parameter show look like the follow object
    // {
    //    Surgeons: [ { Name: "Dr.ABC", SpecialityId: 1 }, similar_objects...],
    //    Specialities: [ { Name: "Neurologist" }, { Name: "Cardiac Surgeon"}],
    //    SurgeryTypes: [{ Name: "type asd" },......],
    //    TheatreTypes: [{ Name: "type asd" },......],
    //    Theatres: [{ Name: "OR 1", TheatreTypeId: 1 }, {}],
    //    SurgeryTypeTheatres: [{ SurgeryTypeId: 1, TheatreIds: [1, 2, 3, 4] }, ....]
    // }

    //sending the data to the backend
    var res = axios.post("/api/AdminApi/SaveHospitalData", hospitalData);

    //nothing meaningful to do with variable res here so just end the function
    return;
}