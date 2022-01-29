function clearAllPatientFields() {
    $("#patient_name").val("");
    $("#birthday").val("");
    $("input:radio.gender:checked").prop("checked",false);
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
        Height:height,
        Gender: gender
    };
    console.log(patient);
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
        } else {
            //patient doesnot exits
            patientId = NULL_ENTITY_ID;
            clearAllPatientFields();
            enableAllPatientFields();

        }
    } else {
        displaySweetAlert("Patient Id is empty");
    }

    return patientId;
}

async function addAppointment() {

}

//event listeners
$("#validate-patient-btn").click(async function () {
    global.validatedPatientId = await validatePatient();
})


function initGlobalVariable() {
    return {
        validatedPatientId: NULL_ENTITY_ID,
        selectedSurgeonId: NULL_ENTITY_ID,
        selectedSurgeryType: NULL_ENTITY_ID,
        anesthesiaType: NULL_ENTITY_ID
    };
}

function initSurgeons(surgeons) {

    var surgeonsByType = {};

    surgeons.forEach((surgeon) => {
        var specialityName = surgeon.speciality.name;
        if (!(surgeonsByType.hasOwnProperty(specialityName))) {
            surgeonsByType[specialityName] = [];
        }
        surgeonsByType[specialityName].push(surgeon);

    });

    var specialities = Object.keys(surgeonsByType);
    specialities.forEach((specialityName) => {
        var $optgroup = $("<optgroup label='" + specialityName + "'>");
        var filteredSurgeons = surgeonsByType[specialityName];
        filteredSurgeons.forEach((surgeon) => {
            $optgroup.append(new Option(surgeon.name, surgeon.id));
        });
        $('#surgeon').append($optgroup);
    })
}

async function initDropDownLists() {
    var dropDowns = await getDropDownListsRequest();
    var surgeons = dropDowns.surgeons;
    var surgeryTypes = dropDowns.surgeryTypes;

    initSurgeons(surgeons);

    //initSurgeryTypes
    surgeryTypes.forEach((surgeryType) => {
        $('#surgery').append(new Option(surgeryType.name, surgeryType.id));
    });
}

//main
const NULL_ENTITY_ID = 0;

var global = {};

//Runs after the DOM is ready
$(document).ready(async function () {
    global = initGlobalVariable();
    await initDropDownLists();
});
