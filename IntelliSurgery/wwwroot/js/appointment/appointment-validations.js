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
        idError = true;
        $("#idCheck").html("**ID is missing");
        $("#idCheck").show();
        isSuccess = false;
    }
    else if (!(idInput.match(/^[0-9]+$/))) {                          //only digits(0-9) will be allowed as the id input
        idError = true;
        $("#idCheck").html("**ID should contain only digits");          //if code gives wrong input ,return false
        $("#idCheck").show();
        isSuccess = false;
    }
    else {
        idError = false;
        $("#idCheck").hide();
    }
    return isSuccess;
}

function validateName() {                                //validate patient name
    var nameInput = $('#patient_name').val();
    var isSuccess = true;
    if (nameInput == '') {                                //you matched an invalid character-(nameInput.match(/[^a-z ]/gi)
        nameError = true;
        $("#nameCheck").html("**Name is missing");
        $("#nameCheck").show();
        isSuccess = false;
    }
    else {
        nameError = false;
        $("#nameCheck").hide();
    }
    return isSuccess;
}

function validateWeight() {                                     //validate patient weight       
    let weightInput = $('#weight').val();
    var isSuccess = true;
    if (weightInput == '') {
        weightError = true;
        $('#weightCheck').html("**Weight is missing");                                                  //$("#weight").css("display", "");
        $('#weightCheck').show();
        isSuccess = false;                                                          //document.getElementById("weight").style.display = null;
    }
    else if (!(/^\d+\.\d+$|^\d+$/.test(weightInput))) {                       //allow integers & floats
        weightError = true;
        $('#weightCheck').html('**Invalid weight-should be numeric');
        $('#weightCheck').show();
        isSuccess = false;
    }
    else {
        weightError = false;
        $('#weightCheck').hide();
    }
    return isSuccess;
}

function validateHeight() {                                      //validate patient height
    let heightInput = $('#height').val();
    var isSuccess = true;
    if (heightInput == '') {
        heightError = true;
        $('#heightCheck').html("**Height is missing");
        $('#heightCheck').show();
        isSuccess = false;
    }
    else if (!(/^\d+\.\d+$|^\d+$/.test(heightInput))) {                            //allow only integers & floats
        heightError = true;
        $('#heightCheck').html('**Invalid height-should be numeric');
        $('#heightCheck').show();
        isSuccess = false;
    }
    else {
        $('#heightCheck').hide();
        heightError = false;
    }
    return isSuccess;
}

function validateBirthday() {
    let birthdayInput = $('#birthday').val();
    var isSuccess = true;
    if (birthdayInput.length == '') {
        birthdayError = true;
        $('#birthdayCheck').html("**Bithday is missing");
        $('#birthdayCheck').show();
        isSuccess = false;
    }
    else {
        $('#birthdaytCheck').hide();
        birthdayError = false;
    }
    return isSuccess;
}

function validateGender() {                                         //validate gender radio is checked
    var isSuccess = true;    
    if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
        genderError = true;
        $('#genderCheck').html("**Gender is missing");
        $('#genderCheck').show();
        isSuccess = false;
    }
    else {
        $('#genderCheck').hide();
        var genderSelected = $('input[name=gender]:checked').val();
        genderError = false;
    }
    return isSuccess;
}

function validateAnastheasist() {
    var isSuccess = true;
    if ((!($('#not_required').prop('checked'))) && (!($('#required').prop('checked')))) {
        anastheasistError = true;
        $('#anastheasistCheck').html("**Anastheasist requirement is missing");
        $('#anastheasistCheck').show();
        isSuccess = false;
    }
    else if ($('#not_required').prop('checked')) {
        $('#anastheasistCheck').hide();
        //$("#ddlList option[value= '-1']").prop("disabled", true);            //--------------------disable Anasthesia_type combo box
        anastheasistError = false;
    }
    else {
        $('#anastheasistCheck').hide();
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

    //I have set the combo box defalt value to -1
    if ($('#anasthesia_type').val() == -1) {                   //if ($('#anasthesia_type').children("option:selected").text() == '')
        anasthesia_typeError = true;
        $('#anasthesia_typeCheck').html("**Anasthesia type is missing");
        $('#anasthesia_typeCheck').show();
        isSuccess = false;
    }
    else {
        $('#anasthesia_typeCheck').hide();
        anasthesia_typeError = false;
    }
    return isSuccess;
}

function validateSurgery() {                                    //validate surgery combo is choosed
    var isSuccess = true;
    //I have set the combo box defalt value to -1
    if ($('#surgery').val() == -1) {
        surgeryError = true;
        $('#surgeryCheck').html("**Surgery is missing");
        $('#surgeryCheck').show();
        isSuccess = false;
    }
    else {
        $('#surgeryCheck').hide();
        surgeryError = false;
    }
    return isSuccess;
}

function validateSurgeon() {                                    //validate surgeon combo is choosed
    var isSuccess = true;
    //I have set the combo box defalt value to -1

    if ($('#surgeon').val() == -1) {
        surgeonError = true;
        $('#surgeonCheck').html("**Surgeon is missing");
        $('#surgeonCheck').show();
        isSuccess = false;
    }
    else {
        $('#surgeonCheck').hide();
        surgeonError = false;
    }
    return isSuccess;
}

function validateTheatre() {                                    //validat or theatre combo is choosed
    var isSuccess = true;
    //I have set the combo box defalt value to -1
    if ($('#theatreType').val() === -1) {
        surgeonError = true;
        $('#theatreCheck').html("**Theatre type is missing");
        $('#theatreCheck').show();
        isSuccess = false;
    }
    else {
        $('#theatreCheck').hide();
        surgeonError = false;
    }
    return isSuccess;
}

function validateImportance() {                                     //check weather importance radio button is selected or not
    var isSuccess = true;
    if ((!($('#importance_high').prop('checked'))) && (!($('#importance_medium').prop('checked'))) && (!($('#importance_low').prop('checked')))) {
        importanceError = true;
        $('#importanceCheck').html("**Importance is missing");
        $('#importanceCheck').show();
        isSuccess = false;
    }
    else {
        $('#importanceCheck').hide();
        var importanceSelected = $('input[name=importance]:checked').val();
        importanceError = false;
    }
    return isSuccess;
}

function validateAsaStatus() {                                     //check weather ASA status radio button is selected or not
    var isSuccess = true;
    if ((!($('#status1').prop('checked'))) && (!($('#status2').prop('checked'))) && (!($('#status3').prop('checked')))) {
        asaError = true;
        $('#AsaCheck').html("**ASA status is missing");
        $('#AsaCheck').show();
        isSuccess = false;
    }
    else {
        $('#AsaCheck').hide();
        var statusSelected = $('input[name=asa]:checked').val();
        asaError = false;
    }
    return isSuccess;
}
