function getAppointmentDetails(patientId) {
    var appointment = {
        PatientId: patientId,
        SurgeryType: $("#surgery").val(),
        TheatreType:0,
        SurgeonId: $("#surgeon").val(),
        IsAnesthesiaRequired: true, //
        AnesthesiaType: 0 /* $("#anasthesia_type").val()*/,
        PriorityLevel: $("input:radio.priority:checked").val()
    };
    return appointment;
}

async function addAppointment(patientId) {
    var predictedTime = null;
    var appointment = getAppointmentDetails(patientId);
    predictedTime = await addAppointmentRequest(appointment);
    return predictedTime;
}

function clearAllAppointmentFields() {
    $("#surgery").val(-1);
    $("#surgeon").val(-1);
    $("#anasthesia_type").val(-1);
}

async function validatePatient() {
    


    return patientId;
}

async function addPatient() {
    


    return patientId;
}

async function updatePatient() {




    return patientId;
}

// Validate patient name
$('#patientNameCheck').hide();
let patientNameError = true;

$('#patient_name').keyup(function () {
    validatePatientName();
});

function validatePatientName() {
    let patientNameValue = $('#patient_name').val();

    if (patientNameValue.length == '') {
        $('#patientNameCheck').show();
        patientNameError = false;
        return false;
    }
    else {
        $('#patientNameCheck').hide();
    }
}

//Validate Weight
$('#weightCheck').hide();
let weightError = true;

$('#weight').keyup(function () {
    validateWeight();
});

function validateWeight() {
    let weight = $('#weight').val();

    if (weight.length == '') {
        $('#weightCheck').show();
        weightError = false;
        return false;
    }
    else {
        $('#weightCheck').hide();    //need to check whether isNumeric(),
    }
}


//Validate Height
$('#heightCheck').hide();
let heightError = true;

$('#height').keyup(function () {
    validateHeight();
});

function validateHeight() {
    let height = $('#height').val();

    if (height.length == '') {
        $('#heightCheck').show();
        heightError = false;
        return false;
    }
    else {
        $('#heightCheck').hide();             //need to check whether isNumeric(),
    }
}
