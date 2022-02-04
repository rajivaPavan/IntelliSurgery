var idError = false;
var patientNameError = false;
var birthdayError = false;
var genderError = false;
var weightError = false;
var heightError = false;
var asaError = false;
var diseasesError = false;
var surgeryError = false;
var surgeonError = false;
var anastheasistError = false;
var anasthesia_typeError = false;
var or_theatreError = false;
var importanceError = false;

$('#patient_id').change(function () {
    validateId();
});

$('#patient_name').change(function () {
    validatePatientName();
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

$('#anasthesia_type').change(function () {
    validateAnasthesia_type();
});

$('#or_theatre').change(function () {
    validateOr_theatre();
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
    return isSuccess;
}

    function validatePatientName() {                                //validate patient name
        var nameInput = $("patient_name").val();
        var isSuccess = true;
        if (nameInput.match(/[^a-z ]/gi)) {                                           //you matched an invalid character
            $("#patientNameCheck").html("**Should contain only letters");
            $("#patientNameCheck").show();
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateWeight() {                                     //validate patient weight       
        let weightInput = $('#weight').val();
        var isSuccess = true;
        $("#weightCheck").css("display", "");
        if (weightInput.length == '') {
            $('#weightCheck').show();                                                  //$("#weight").css("display", "");
            isSuccess = false;                                                                           //document.getElementById("weight").style.display = null;
                                                    //$('weight').show();   //$('.myerror').css('display','none');   
        }
        else if (!(/^\d+\.\d+$|^\d+$/.test(weightInput))) {                       //allow integers & floats      
            $('#weightCheck').html('**Invalid weight-should be numeric');
            $('#weightCheck').show();
            
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateHeight() {                                      //validate patient height
        let heightInput = $('#height').val();
        var isSuccess = true;
        if (heightInput.length == '') {
            $('#heightCheck').show();
            //heightError = true;
            isSuccess = false;
        }
        else if (!(/^\d+\.\d+$|^\d+$/.test(heightInput))) {                            //allow only integers & floats
            $('#heightCheck').html('**Invalid height-should be numeric');
            $('#heightCheck').show();
            //heightError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateBirthday() {
        let birthdayInput = $('#birthday').val();
        var isSuccess = true;
        if (birthdayInput.length == '') {
            $('#birthdaytCheck').show();
            //birthdaytError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

function validateGender() {                                         //validate patient gender radio is checked
    var isSuccess = true;
    if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
        $('#genderCheck').show();
        //genderError = true;
        isSuccess = false;
    }
    return isSuccess;
}

function validateAnastheasist() {
    var isSuccess = true;
    if ($('#not_required').prop('checked')) {
        var anastheasistSelected = $('input[name=anastheasist]:checked').val();
        $("#ddlList option[value= '-1']").prop("disabled",true);
    }
    else {
        validateAnasthesia_type()
    }
    return isSuccess;
}

    function validateSurgery() {                                    //validate surgery combo is choosed
        var isSuccess = true;
        if ($('#surgery').text() == '') {
            $('#surgeryCheck').show();
           //surgeryError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateSurgeon() {                                    //validate surgeon combo is choosed
        var isSuccess = true;
        if ($('#surgeon').text() == '') {
            $('#surgeonCheck').show();
            //surgeryError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateAnasthesia_type() {                               //validate anasthesia type combo is choosed-this function has called inside validateAnastheasist()
        var isSuccess = true;
        if ($('#anasthesia_type').text() == '') {
            $('#anasthesia_typeCheck').show();
            //anasthesia_typeError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateOr_theatre() {                                    //validat or theatre combo is choosed
        var isSuccess = true;
        if ($('#or_theatre').text() == '') {
            $('#or_theatreCheck').show();
            //or_theatreError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateGender() {                                       
        var genderSelected = $('input[name=gender]:checked').val()                  //check weather gender radio button is selected or not
        var isSuccess = true;
        if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
            $('#genderCheck').show();
            //genderError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    function validateImportance() {                                     //check weather importance radio button is selected or not
        var importanceSelected = $('input[name=importance]:checked').val()
        var isSuccess = true;
        if ((!($('#importance_high').prop('checked'))) && (!($('#importance_medium').prop('checked'))) && (!($('#importance_low').prop('checked')))) {
            $('#importanceCheck').show();
            //importanceError = true;
            isSuccess = false;
        }
        return isSuccess;
}

    //disable combo box item by using it's value : $("#ddlList option[value='jquery']").attr("disabled", "disabled");
    //disable combo box item by using it's text : $('#ddlList option:contains("HTML")').attr("disabled", "disabled");
    //$('#surgeon select').prop('disabled', true);

    function validateAsaStatus() {                                     //check weather ASA status radio button is selected or not
        var statusSelected = $('input[name=asa]:checked').val();
        var isSuccess = true;
        if ((!($('#status1').prop('checked'))) && (!($('#status2').prop('checked'))) && (!($('#status3').prop('checked')))) {
            $('#AsaCheck').show();
            //asaError = true;
            isSuccess = false;
        }
        return isSuccess;
    }

    //$("add-patient-btn").click(function () {
    //    if (patientNameError === false && weightError === false && heightError === false && birthdayError == false && genderError == false && asaError == false && diseasesError == false) {
    //        Swal.fire({
    //            icon: 'success',
    //            title: 'Patient added successfully',
    //            showConfirmButton: false,                   //if false given,no need to press ok button
    //            timer: 1500
    //        });
    //        return true;
    //    } else {
    //        displaySweetAlert("Please try again !");
    //        return false;
    //    }

    //});

    //$("add-appointment-btn").click(function () {
    //    if (surgeonError === false && surgeryError === false && anasthesia_typeError === false && or_theatreError === false && importanceError === false) {
    //        Swal.fire({
    //            icon: 'success',
    //            title: 'Surgery requirements added successfully',
    //            showConfirmButton: false,                   //if false given,no need to press ok button
    //            timer: 1500
    //        });
    //        return true;
    //    } else {
    //        displaySweetAlert("Please try again !");
    //        return false;
    //    }
    //});

