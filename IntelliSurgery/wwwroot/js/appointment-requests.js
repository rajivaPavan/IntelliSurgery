
async function validatePatientRequest(patientId) {
    // validate patient
    // api method returns if patient exists or not
    // this function returns a bool

    var isPatientExists = false;
    var res = await axios.post("/api/AppointmentApi/AddAppointment?patientId=" + patientId.toString());
    if (res.data.success == true) {
        isPatientExists  = res.data.data;
    }
    return isPatientExists;
}

async function addPatientRequest(patient) {
    //var patient = {
    //    Name: 0,
    //    DateOfBirth: 0,
    //    Weight: 0,
    //    Height:0,
    //    Gender: 0
    //};
    const NULL_PATIENT_ID = 0;
    var patientId = NULL_PATIENT_ID;

    var res = await axios.post("/api/AppointmentApi/AddPatient", patient);
    if (res.data.success == true) {
        patientId = res.data.data;
    }
    return patientId;
}

async function addAppointmentRequest(appointment) {
    //get data from form
    //add appointment
    //return predicted time for surgery

    //var appointment = {
    //    PatientId: 0,
    //    SurgeryType: 0,
    //    SurgeonId: 0,
    //    AnesthesiaType: 0,
    //    PriorityLevel:0
    //};

    var predicatedTime = null;

    var res = await axios.post("/api/AppointmentApi/AddAppointment",appointment);
    if (res.data.success == true) {
        predicatedTime = res.data.data;
    }
    return predicatedTime;
}

async function getDropDownListsRequest() {
    var res = await axios.get("/api/AppoinmentApi/GetFormDropDownLists");
    var dto = {};
    var dropDownLists = {
        surgeons: [],
        surgeryTypes:[]
    }
    if (res.data.success == true) {
        dto = res.data.data;
        dropDownList.surgeons = dto.surgeons;
        dropDownList.surgeryTypes = dto.surgeryTypes;
    }
    return dropDownLists;
}
