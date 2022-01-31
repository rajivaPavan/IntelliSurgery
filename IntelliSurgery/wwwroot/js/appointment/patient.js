function clearAllPatientFields() {
    $("#patient_name").val("");
    $("#birthday").val("");
    $("#weight").val("");
    $("#height").val("");
}

function showPatientAddBtn(ishow) {
    if (ishow) {
        $("#update-patient-btn").hide();
        $("#add-patient-btn").show();
    } else {
        $("#add-patient-btn").hide();
        $("#update-patient-btn").show();
    }
}

function disablePatientConstantFields(isDisable) {
    $("#patient_name").prop("disabled", isDisable);
    $("#birthday").prop("disabled", isDisable);
    $("input:radio.gender").prop("disabled", isDisable);
}

function enableAllPatientFields() {
    disablePatientConstantFields(false);
    $("#weight").prop("disabled", false);
    $("#height").prop("disabled", false);
}

function setPatientDetails(patientDetails) {
    $("#patient_name").val(patientDetails.name);
    $("#birthday").val(formatDateTime(patientDetails.dateOfBirth));
    $("input[name=gender][value=" + patientDetails.gender + "]").prop('checked', true);
    $("#weight").val(patientDetails.weight);
    $("#height").val(patientDetails.height);
}

function getPatientDetails() {
    var name = $("#patient_name").val();
    var dateOfBirth = $("#birthday").val();
    var gender = $("input:radio.gender:checked").val();
    var weight = $("#weight").val();
    var height = $("#height").val();

    var patient = {
        Name: name,
        DateOfBirth: dateOfBirth,
        Weight: weight,
        Height: height,
        Gender: gender
    };

    return patient;
}

async function validatePatient() {

    //validation
    var patientId = $("#patient_id").val();

    if (patientId != "") {

        var patientDetails = await validatePatientRequest(patientId);

        if (patientDetails != null) {
            //if patient exists
            patientId = parseInt(patientId);
            setPatientDetails(patientDetails);
            disablePatientConstantFields(true);

            showPatientAddBtn(false);

        } else {
            //patient doesnot exits
            patientId = NULL_ENTITY_ID;
            clearAllPatientFields();
            enableAllPatientFields();

            showPatientAddBtn(true);

        }
    } else {
        //displaySweetAlert("Patient Id is empty");
    }

    return patientId;
}

async function addPatient() {
    var patientId = NULL_ENTITY_ID;
    var patient = getPatientDetails();
    patientId = await addPatientRequest(patient);
    return patientId;
}

async function updatePatient() {
    var patient = getPatientDetails();
    var patientId = await updatePatientRequest(patient);
    return patientId;
}