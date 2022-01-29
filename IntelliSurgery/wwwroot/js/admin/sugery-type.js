function getNewSurgeryType{
    var newSurgeryType = $('#surgery_type').val();

    var surgeryType = {
        name: newSurgeryType;
    }

    return surgeryType;
}

async function addSurgeryTypeRequest(surgeryType) {

    const NULL_SURGERY_TYPE_ID = 0;
    var surgeryTypeId = NULL_SURGERY_TYPE_ID;

    var res = await axios.post("/api/AppointmentApi/AddPatient", surgeryType);
    if (res.data.success == true) {
        surgeryTypeId = res.data.data;
    }
    return surgeryTypeId;
}

async function addSurgeryType() {

    var patient = getPatientDetails();
    var patientId = await addPatientRequest(patient);
    global.addedPatientId = patientId;
}

function disableNewSurgeryTypeField{

}

