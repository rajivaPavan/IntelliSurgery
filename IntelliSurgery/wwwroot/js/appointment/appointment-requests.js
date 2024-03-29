﻿async function validatePatientRequest(patientId) {
    // validate patient
    // api method returns if patient exists or not
    // this function returns a bool
    var patientDetails = null;
    var res = await axios.post("/api/AppointmentApi/ValidatePatient?patientId=" + patientId.toString());
    if (res.data.success == true) {
        var isPatientExists = res.data.data;
        if (isPatientExists == true) {
            patientDetails = res.data.patient;
        }
    }
    //keys in patientDetails => dateOfBirth,gender,height,weight,name
    return patientDetails;
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

async function updatePatientRequest(patient) {
    const NULL_PATIENT_ID = 0;
    var patientId = NULL_PATIENT_ID;

    var res = await axios.post("/api/AppointmentApi/UpdatePatient", patient);
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
    var appointmentId = -1;

    var res = await axios.post("/api/AppointmentApi/AddAppointment",appointment);
    if (res.data.success == true) {
        predicatedTime = res.data.data;
        appointmentId = res.data.appointmentId
        
    }
    return {appointmentId : appointmentId, predicatedTime: predicatedTime};
}

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

async function overrideTimeDurationRequest(aId, time) {
    var res = await axios.post("/api/AppointmentApi/OverrideTimeDuration?appointmentId=" + aId + "&timeDuration=" + time);
    var isSuccess = false;
    if (res.data.success == true) {
        isSuccess = true;
    }
    return isSuccess;
}
