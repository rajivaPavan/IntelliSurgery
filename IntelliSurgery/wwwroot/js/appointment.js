function clearAllPatientFields() {
    $("#patient_name").val("");
    $("#birthday").val("");
    $("#gender_male").prop("checked",false);
    $("#gender_female").prop("checked", false);
    $("#weight").val("");
    $("#height").val("");
}

function disablePatientConstantFields(isDisable) {
    $("#patient_name").prop("disabled", isDisable);
    $("#birthday").prop("disabled", isDisable);
    $("#gender_male").prop("disabled", isDisable);
    $("#gender_female").prop("disabled", isDisable);
}

function enableAllPatientFields() {
    disablePatientConstantFields(false);
    $("#weight").prop("disabled", false);
    $("#height").prop("disabled", false);
}

function setPatientDetails(patientDetails) {
    $("#patient_name").val(patientDetails.name);
    $("#birthday").val(patientDetails.dateOfBirth);
    $("#gender").val(patientDetails.gender);
    $("#weight").val(patientDetails.weight);
    $("#height").val(patientDetails.height);
}

function getPatientDetails() {
    var gender = $("input:radio.someClass:checked").val();
}

async function validatePatient() {

    //validation
    var patientId = $("#patient_id").val();

    if (patientId != "") {

        var patientDetails = await validatePatientRequest(patientId);

        if (patientDetails != null) {
            //if patient exists
            setPatientDetails(patientDetails);
            disablePatientConstantFields(true);
        } else {
            //patient doesnot exits
            clearAllPatientFields();
            enableAllPatientFields();
        }
    } else {
        displaySweetAlert("Patient Id is empty");
    }   
}

//event listeners
$("#validate-patient-btn").click(async function () {
    await validatePatient();
})

//Runs after the DOM is ready
$(document).ready(async function () {
    
});
