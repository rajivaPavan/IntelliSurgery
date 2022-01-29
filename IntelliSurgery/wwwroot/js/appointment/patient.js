function clearAllPatientFields() {
    $("#patient_name").val("");
    $("#birthday").val("");
    $("input:radio.gender:checked").prop("checked", false);
    $("#weight").val("");
    $("#height").val("");
}

function disablePatientConstantFields(isDisable) {
    $("#patient_name").prop("disabled", isDisable);
    $("#birthday").prop("disabled", isDisable);
    $("input:radio.gender:checked").prop("checked", isDisable);
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
    var dateOfBirth = $("#birthday").val(formatDateTime());
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

            $("#add-patient-btn").hide();
            $("#update-patient-btn").show();

        } else {
            //patient doesnot exits
            patientId = NULL_ENTITY_ID;
            clearAllPatientFields();
            enableAllPatientFields();

            $("#add-patient-btn").show();
            $("#update-patient-btn").hide();

        }
    } else {
        displaySweetAlert("Patient Id is empty");
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