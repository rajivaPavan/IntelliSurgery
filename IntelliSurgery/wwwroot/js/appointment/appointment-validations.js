//it is sufficient to validate only text type inputs onchange
$('#patient_id').change(function () {
    validateId();
});

$('#patient_name').change(function () {
    validateName();
});

$('#gender').change(function () {
    validateGender();
});

$('#weight').change(function () {
    validateWeight();
});

$('#height').change(function () {
    validateHeight();
});

//$('#birthday').change(function () {
//    validateBirthday();
//});

//$('#status1,#status2,#status3').change(function () {
//    validateAsaStatus();
//});

//$('#gender_male,#gender_female').change(function () {
//    validateGender();
//});

$('#surgery').change(function () {
    validateSurgery();
});

$('#surgeon').change(function () {
    validateSurgeon();
});

$('#theatreType').change(function () {
    validateTheatre();
});

function validateId() {                                       //validate patient ID
    var idInput = $('#patient_id').val();
    var isSuccess = true;
    //first check data type
    if (!(idInput.match(/^[0-9]+$/)) && idInput!='') {                          //only digits(0-9) will be allowed as the id input
        $("#idCheck").html("**ID should contain only digits");          //if code gives wrong input ,return false
        $("#idCheck").show();
        isSuccess = false;
        return isSuccess;
        //special case of return
        //return value here because otherwise if input is empty, missing error will be shown onchange
    }
    else if (idInput == '') {
        $("#idCheck").html("**ID is missing");
        $("#idCheck").show();
        isSuccess = false;
    }
    else {
        $("#idCheck").hide();
    }
    return isSuccess;
}

function validateName() {                                //validate patient name
    var nameInput = $('#patient_name').val();
    var isSuccess = true;
    if (nameInput == '') {                                //you matched an invalid character-(nameInput.match(/[^a-z ]/gi)
        $("#nameCheck").html("**Name is missing");
        $("#nameCheck").show();
        isSuccess = false;
    }
    else {
        $("#nameCheck").hide();
    }
    return isSuccess;
}

function validateWeight() {                                     //validate patient weight       
    var weightInput = $('#weight').val();
    var isSuccess = true;
    //first check input data type
    if (!(/^\d+\.\d+$|^\d+$/.test(weightInput)) && weightInput != '') {                       //allow integers & floats
        $('#weightCheck').html('**Invalid weight-should be numeric');
        $('#weightCheck').show();
        isSuccess = false;
        return isSuccess;
        //special case of return
        //return value here because otherwise if input is empty, missing error will be shown onchange
    }
    else if (weightInput == '') {
        $('#weightCheck').html("**Weight is missing");                                                  //$("#weight").css("display", "");
        $('#weightCheck').show();
        isSuccess = false;                                                          //document.getElementById("weight").style.display = null;
    }
    else {
        $('#weightCheck').hide();
    }
    return isSuccess;
}

function validateHeight() {                                      //validate patient height
    var heightInput = $('#height').val();
    var isSuccess = true;
    //first check input data type
    if (!(/^\d+\.\d+$|^\d+$/.test(heightInput)) && heightInput != '') {                            //allow only integers & floats
        $('#heightCheck').html('**Invalid height-should be numeric');
        $('#heightCheck').show();
        isSuccess = false;
        return isSuccess;
        //special case of return
        //return value here because otherwise if input is empty, missing error will be shown onchange
    }
    else if (heightInput == '') {
        heightError = true;
        $('#heightCheck').html("**Height is missing");
        $('#heightCheck').show();
        isSuccess = false;
    }
    else {
        $('#heightCheck').hide();
    }
    return isSuccess;
}

function validateBirthday() {
    var birthdayInput = $('#birthday').val();
    var isSuccess = true;
    if (birthdayInput == '' || birthdayInput == null) {
        $('#birthdayCheck').html("**Date of birth is missing");
        $('#birthdayCheck').show();
        isSuccess = false;
    }
    else {
        $('#birthdayCheck').hide();
    }
    return isSuccess;
}

function validateGender() {                                         //validate gender radio is checked
    var isSuccess = true;    
    if ($("input[name=gender]:checked").val() == null) {
        $('#genderCheck').html("**Gender is missing");
        $('#genderCheck').show();
        isSuccess = false;
    }
    else {
        $('#genderCheck').hide();
    }
    return isSuccess;
}

function validateAnasthesia_type() {                               //validate anasthesia type combo is choosed-this function is called inside validateAnastheasist()
    var isSuccess = true;
    //I have set the combo box defalt value to -1
    if ($('#anasthesia_type').val() == -1) {                  
        $('#anasthesia_typeCheck').html("**Anasthesia type is missing");
        $('#anasthesia_typeCheck').show();
        isSuccess = false;
    }
    else {
        $('#anasthesia_typeCheck').hide();
    }
    return isSuccess;
}

function validateSurgery() {                                    //validate surgery combo is choosed
    var isSuccess = true;
    //I have set the combo box defalt value to -1
    if ($('#surgery').val() == -1) {
        $('#surgeryCheck').html("**Surgery is missing");
        $('#surgeryCheck').show();
        isSuccess = false;
    }
    else {
        $('#surgeryCheck').hide();
    }
    return isSuccess;
}

function validateSurgeon() {                                    //validate surgeon combo is choosed
    var isSuccess = true;
    //I have set the combo box defalt value to -1

    if ($('#surgeon').val() == -1) {
        $('#surgeonCheck').html("**Surgeon is missing");
        $('#surgeonCheck').show();
        isSuccess = false;
    }
    else {
        $('#surgeonCheck').hide();
    }
    return isSuccess;
}

function validateTheatre() {                                    //validat or theatre combo is choosed
    var isSuccess = true;
    //I have set the combo box defalt value to -1
    if ($('#theatreType').val() == -1) {
        $('#theatreCheck').html("**Theatre type is missing");
        $('#theatreCheck').show();
        isSuccess = false;
    }
    else {
        $('#theatreCheck').hide();
    }
    return isSuccess;
}

function validateImportance() {                                     //check weather importance radio button is selected or not
    var isSuccess = true;
    if ($("input[name=importance]:checked").val() == null) {    //-1 is the value assigned to Choose...
        $('#importanceCheck').html("**Importance is missing");
        $('#importanceCheck').show();
        isSuccess = false;
    }
    else {
        $('#importanceCheck').hide();
    }
    return isSuccess;
}

function validateAsaStatus() {                                     //check weather ASA status radio button is selected or not    
    var isSuccess = true;
    if ($("input[name=asa]:checked").val() == null) {  //if no radio button is selected value of that radio button group is null
        $('#asaCheck').html("**ASA status is missing");
        $('#asaCheck').show();
        isSuccess = false;
    }
    else {
        $('#asaCheck').hide();
    }
    return isSuccess;
}

function finalAddPatientValidation() {
    var isSuccess = true;
    isSuccess = validateName() &&
                validateWeight() &&
                validateHeight() &&
                validateBirthday() &&
                validateGender() &&
                validateAsaStatus();
    return isSuccess;
}
function finalAppointmentValidation() {
    var isSuccess = true;
    isSuccess = validateSurgery() &&
                validateSurgeon() &&
                validateAnasthesia_type() &&
                validateTheatre() &&
                validateImportance();
    return isSuccess;
}

function finalUpdatePatientValidation() {
    var isSuccess = true;
    isSuccess = validateWeight() &&
                validateHeight() &&
                validateAsaStatus();
    return isSuccess;
}
