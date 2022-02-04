var idError = true;                                 //__________Error =true means id input has an error,the buttons won't work
var nameError = true;
var birthdayError = true;
var genderError = true;
var weightError = true;
var heightError = true;
var asaError = true;
var surgeryError = true;
var surgeonError = true;
var anastheasistError = true;
var anasthesia_typeError = true;
var theatreError = true;
var importanceError = true;

$('#patient_id').change(function () {
    validateId();
});

$('#patient_name').change(function () {
    validateName();
});

$('#birthday').change(function () {
    validateBirthday();
});

$('#weight').change(function () {
    validateWeight();
});

$('#height').change(function () {
    validateHeight();
});   

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
    if (idInput == '') {
        $("#idCheck").show();
        isSuccess = false;
    }
    else if (!(idInput.match(/^[0-9]+$/))) {                          //only digits(0-9) will be allowed as the id input
        $("#idCheck").text($("#idCheck").html("**ID should contain only digits"));          //if code gives wrong input ,return false
        $("#idCheck").show();
        isSuccess = false;
    }
    else {
        idError = false;
    }
    return isSuccess;
}

function validateName() {                                //validate patient name
    var nameInput = $('#patient_name').val();
    var isSuccess = true;
    if (nameInput == '')) {                                //you matched an invalid character-(nameInput.match(/[^a-z ]/gi)
        $("#nameCheck").show();
        isSuccess = false;
    }
    else {
        nameError = false;
    }
    return isSuccess;
}

function validateWeight() {                                     //validate patient weight       
    let weightInput = $('#weight').val();
    var isSuccess = true;
    if (weightInput.length == '') {
        $('#weightCheck').show();                                                  //$("#weight").css("display", "");
        isSuccess = false;                                                                           //document.getElementById("weight").style.display = null;
    }
    else if (!(/^\d+\.\d+$|^\d+$/.test(weightInput))) {                       //allow integers & floats      
        $('#weightCheck').html('**Invalid weight-should be numeric');
        $('#weightCheck').show();
        isSuccess = false;
    }
    else {
        weightError = false;
    }
    return isSuccess;
}

function validateHeight() {                                      //validate patient height
    let heightInput = $('#height').val();
    var isSuccess = true;
    if (heightInput.length == '') {
        $('#heightCheck').show();
        isSuccess = false;
    }
    else if (!(/^\d+\.\d+$|^\d+$/.test(heightInput))) {                            //allow only integers & floats
        $('#heightCheck').html('**Invalid height-should be numeric');
        $('#heightCheck').show();
        isSuccess = false;
    }
    else {
        heightError = false;
    }
    return isSuccess;
}

function validateBirthday() {
    let birthdayInput = $('#birthday').val();
    var isSuccess = true;
    if (birthdayInput.length == '') {
        $('#birthdaytCheck').show();
        isSuccess = false;
    }
    else {
        birthdayError = false;
    }
    return isSuccess;
}

function validateGender() {                                         //validate gender radio is checked
    var isSuccess = true;    
    if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
        $('#genderCheck').show();
        isSuccess = false;
    }
    else {
        var genderSelected = $('input[name=gender]:checked').val();
        genderError = false;
    }
    return isSuccess;
}

function validateAnastheasist() {
    var isSuccess = true;
    if ((!($('#not_required').prop('checked'))) && (!($('#required').prop('checked')))) {
        $('#anastheasistCheck').show();
        isSuccess = false;
    }
    else if ($('#not_required').prop('checked')) {
        $("#ddlList option[value= '-1']").prop("disabled", true);            //--------------------disable Anasthesia_type combo box
        anastheasistError = false;
    }
    else {
        validateAnasthesia_type();
        anastheasistError = false;
    }
    return isSuccess;
}

//disable combo box item by using it's value : $("#ddlList option[value='jquery']").attr("disabled", "disabled");
//disable combo box item by using it's text : $('#ddlList option:contains("HTML")').attr("disabled", "disabled");
//$('#surgeon select').prop('disabled', true);

function validateAnasthesia_type() {                               //validate anasthesia type combo is choosed-this function is called inside validateAnastheasist()
    var isSuccess = true;
    if ($('#anasthesia_type').text() == '') {
        $('#anasthesia_typeCheck').show();
        isSuccess = false;
    }
    else {
        anasthesia_typeError = false;
    }
    return isSuccess;
}

function validateSurgery() {                                    //validate surgery combo is choosed
    var isSuccess = true;
    if ($('#surgery').text() == '') {
        $('#surgeryCheck').show();
        isSuccess = false;
    }
    else {
        surgeryError = false;
    }
    return isSuccess;
}

function validateSurgeon() {                                    //validate surgeon combo is choosed
    var isSuccess = true;
    if ($('#surgeon').text() == '') {
        $('#surgeonCheck').show();
        isSuccess = false;
    }
    else {
        surgeonError = false;
    }
    return isSuccess;
}

function validateTheatre() {                                    //validat or theatre combo is choosed
    var isSuccess = true;
    if ($('#theatreType').text() == '') {
        $('#theatreCheck').show();
        isSuccess = false;
    }
    else {
        theatreError = false;
    }
    return isSuccess;
}

function validateImportance() {                                     //check weather importance radio button is selected or not
    var isSuccess = true;
    if ((!($('#importance_high').prop('checked'))) && (!($('#importance_medium').prop('checked'))) && (!($('#importance_low').prop('checked')))) {
        $('#importanceCheck').show();
        isSuccess = false;
    }
    else {
        var importanceSelected = $('input[name=importance]:checked').val();
        importanceError = false;
    }
    return isSuccess;
}

function validateAsaStatus() {                                     //check weather ASA status radio button is selected or not
    var isSuccess = true;
    if ((!($('#status1').prop('checked'))) && (!($('#status2').prop('checked'))) && (!($('#status3').prop('checked')))) {
        $('#AsaCheck').show();
        //asaError = true;
        isSuccess = false;
    }
    else {
        var statusSelected = $('input[name=asa]:checked').val();
        asaError = false;
    }
    return isSuccess;
}
