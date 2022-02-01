function getAppointmentDetails(patientId) {
    var appointment = {
        PatientId: patientId,
        SurgeryType: $("#surgery").val(),
        TheatreType:$("#theatreType").val(),
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
    $("#theatreType").val(-1);
}

//I have commented these 3 functions bevause their names have been used for other functions in another file

//async function validatePatient() {
    
//    return patientId;
//}

//async function addPatient() {
    

//    return patientId;
//}

//async function updatePatient() {

//    return patientId;
//}

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
    let weightInput = $('#weight').val();

    if (weightInput.length == '') {
        $('#weightCheck').show();
        weightError = false;
        return false;
    }
    else if (!$.isNumeric(weightInput)) {
        $('#weightCheck').show();
        $('#weightCheck').html('**Invalid weight-should be numeric');
        $('#weightCheck').css("color", "red");
        weightError = false;
        return false;
    }
    else{
        $('#weightCheck').hide();
}
}


//Validate Height
$('#heightCheck').hide();
let heightError = true;

$('#height').keyup(function () {
    validateHeight();
});

function validateHeight() {
    let heightInput = $('#height').val();

    if (heightInput.length == '') {
        $('#heightCheck').show();
        heightError = false;
        return false;
    }
    else if (!$.isNumeric(heightInput)) {                                     
        $('#heightCheck').show();
        $('#heightCheck').html('**Invalid height-should be numeric');
        $('#heightCheck').css("color", "red");
        heightError = false;
        return false;
    }
    else {
        $('#heightCheck').hide();             
    }
}

/*
//check weather gender radio button is selected or not
$('#genderCheck').hide();
let genderError = true;

$('#----').keyup(function () {
    validateGender();
});

function validateGender() {
    if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
        $('#genderCheck').show();
        genderError = false;
        return false;
    }
    else{
        $('#genderCheck').hide();
    }
    
}

//check weather anastheasists radio button is selected or not
$('#anastheasistCheck').hide();
let anastheasistError = true;

$('#----').keyup(function () {
    validateAnastheasist();
});

function validateAnastheasist() {
    if ((!($('#required').prop('checked'))) && (!($('#not_required').prop('checked')))) {
        $('#anastheasistCheck').show();
        anastheasistError = false;
        return false;
    }
    else {
        $('#anastheasistCheck').hide();
    }
}
*/

//check weather surgery is selected or not
$('#surgeryCheck').hide();
let surgeryError = true;

$('#surgery').keyup(function () {
    validateSurgery();
});

function validateSurgery() {
    if ($('#surgery').val() == 'Choose...') {
        $('#surgeryCheck').show();
        surgeryError = false;
        return false;
    }
    else {
        $('#surgeryCheck').hide();
    }
}

//check weather surgeon is selected or not
$('#surgeonCheck').hide();
let surgeonError = true;

$('#surgeon').keyup(function () {
    validateSurgeon();
});

function validateSurgeon() {
    if ($('#surgeon').val() == 'Choose...') {
        $('#surgeonCheck').show();
        surgeryError = false;
        return false;
    }
    else {
        $('#surgeonCheck').hide();
    }
}

//check weather anasthesia_type is selected or not
$('#anasthesia_typeCheck').hide();
let anasthesia_typeError = true;

$('#anasthesia_type').keyup(function () {
    validateAnasthesia_type();
});

function validateAnasthesia_type() {
    if ($('#anasthesia_type').val() == 'Choose...') {
        $('#anasthesia_typeCheck').show();
        anasthesia_typeError = false;
        return false;
    }
    else {
        $('#anasthesia_typeCheck').hide();
    }
}


//check weather or_theatre is selected or not
$('#or_theatreCheck').hide();
let or_theatreError = true;

$('#or_theatre').keyup(function () {
    validateOr_theatre();
});

function validateOr_theatre() {
    if ($('#or_theatre').val() == 'Choose...') {
        $('#or_theatreCheck').show();
        or_theatreError = false;
        return false;
    }
    else {
        $('#or_theatreCheck').hide();
    }
}