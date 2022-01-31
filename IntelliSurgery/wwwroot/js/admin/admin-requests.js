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

async function initDropDownLists() {
    var dropDowns = await getDropDownListsRequest();
    var surgeons = dropDowns.surgeons;
    var surgeryTypes = dropDowns.surgeryTypes;
    var anesthesiaTypes = dropDowns.anesthesiaTypes;
    var theatreTypes = dropDowns.theatreTypes;
    //var surgeonSpecialityTypes = dropDowns.surgeonSpecialityTypes;

    initSurgeons(surgeons);
    //surgeonSpecialityTypes.forEach((s) => {
    //    $('#surgeon_speciality').append(new Option(s.name, s.id));
    //});

    surgeryTypes.forEach((s) => {
        $('#surgery_type_data').append(new Option(s.name, s.id));
    });
   
    theatreTypes.forEach((t) => {
        $('#theatre_type_data').append(new Option(t.name, t.id));
    });

}

async function saveHospitalDataRequest(hospitalData) {
    // hospitalData parameter show look like the follow object
    // {
    //    Surgeons: [ { Name: "Dr.ABC", SpecialityId: 1 }, similar_objects...], DONE
    //    Specialities: [ { Name: "Neurologist" }, { Name: "Cardiac Surgeon"}], DONE
    //    SurgeryTypes: [{ Name: "type asd" },......], DONE
    //    TheatreTypes: [{ Name: "type asd" },......], DONE
    //    Theatres: [{ Name: "OR 1", TheatreTypeId: 1 }, {}],DONE
    //    SurgeryTypeTheatres: [{ SurgeryTypeId: 1, TheatreIds: [1, 2, 3, 4] }, ....]
    // }

    //sending the data to the backend
    var res = axios.post("/api/AdminApi/SaveHospitalData", hospitalData);

    //nothing meaningful to do with variable res here so just end the function
    return;
}