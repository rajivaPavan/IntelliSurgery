$(document).ready(function () {
    $("#idCheck").hide();                           //first all the h6 tags are hidden
    $("#patientNameCheck").hide();   //<span class="error_name" id="patientNameCheck"></span>
    $("#weightCheck").hide();
    $("#heightCheck").hide();
    $("#birthdayCheck").hide();
    $('#surgeryCheck').hide();
    $('#surgeonCheck').hide();
    $('#anasthesia_typeCheck').hide();
    //$('#or_theatreCheck').hide();
    $('#genderCheck').hide();
    $('#importanceCheck').hide();
    $('#anastheasistCheck').hide();
    $("#asaCheck").hide();

    var idError = false;                                    //first all errors are set to false
    var patientNameError = false;
    var weightError = false;
    var heightError = false;
    var birthdayError = false;
    var surgeryError = false;
    var surgeonError = false;
    var anasthesia_typeError = false;
    //var or_theatreError = false;
    var genderError = true;
    var importanceError = true;
    var anastheasistError = true;
    var asaError = false;

    $('#patient_id').keyup(function () {
        validateId();
    });

    $('#patient_name').blur(function (e) {                       //$('#patient_name').keyup(function () {
        validatePatientName();
    });

    $('#weight').keyup(function () {
        validateWeight();
    });

    $('#height').keyup(function () {
        validateHeight();
    });

    $('#birthday').keyup(function () {
        validateBirthday();
    });

    $('#surgery').keyup(function () {
        validateSurgery();
    });

    $('#surgeon').keyup(function () {
        validateSurgeon();
    });

    $('#anasthesia_type').keyupt(function () {
        validateAnasthesia_type();
    });

    /*$('#or_theatre').keyup(function () {
        validateOr_theatre();
    });*/

    function validateId() {                                       //validate patient ID
        var idInput = $('#patient_id').val();
        if (idInput == "") {
            $("#idCheck").show();
            patientNameError = true;
        }
        else if (idInput.match(/^[0-9]+$/)) {                      //only digits(0-9) will be allowed as the id input
            $("#idCheck").hide();
        }
        else {
            $("#idCheck").html("**ID should contain only digits");
            $("#idCheck").show();
            idError = true;
        }
    }

    function validatePatientName() {                                //validate patient name
        var nameInput = $("patient_name").val();
        if (nameInput == "") {
            $("#patientNameCheck").show();
            patientNameError = true;
        }
        else if (nameInput.match(/[^a-z ]/gi)) {                                           //you matched an invalid character
            $("#patientNameCheck").html("**Should contain only characters");
            $("#patientNameCheck").show();
            patientNameError = true;
        }
        else {
            $("#patientNameCheck").hide();
        }
    }

    function validateWeight() {                                      //validate patient weight
        let weightInput = $('#weight').val();

        if (weightInput.length == '') {
            $('#weightCheck').show();
            weightError = true;
        }
        else if (/^\d+\.\d+$|^\d+$/.test(weightInput)) {                       //allow integers & floats
            $('#weightCheck').hide();
        }
        else {
            $('#weightCheck').html('**Invalid weight-should be numeric');
            $('#weightCheck').show();
            weightError = true;
        }
    }
    function validateHeight() {                                      //validate patient height
        let heightInput = $('#height').val();

        if (heightInput.length == '') {
            $('#heightCheck').show();
            heightError = true;
        }
        else if (/^\d+\.\d+$|^\d+$/.test(heightInput)) {                            //allow integers & floats
            $('#heightCheck').hide();
        }
        else {
            $('#heightCheck').html('**Invalid height-should be numeric');
            $('#heightCheck').show();
            heightError = true;
        }
    }

    function validateBirthday() {
        $('.check').on('click', function (e) {
            e.preventDefault();
            $('.field input').each(function (index, value) {
                if ($(this).is('[type="date"]')) {
                    $('#birthdayCheck').hide();
                }
                else {
                    $('#birthdayCheck').show();
                    birthdayError = true;
                }
            })
        })
    }

    function validateGender() {                                         //validate patient gender
        if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
            $('#genderCheck').show();
            genderError = true;
        }
        else {
            $('#genderCheck').hide();
        }

    }
    function validateAnastheasist() {                                   //validate anastheasist is choosed
        if ((!($('#required').prop('checked'))) && (!($('#not_required').prop('checked')))) {
            $('#anastheasistCheck').show();
            anastheasistError = true;
        }
        else {
            $('#anastheasistCheck').hide();
        }
    }
    function validateSurgery() {                                    //validate surgery is choosed
        if ($('#surgery').val() == 'Choose...') {
            $('#surgeryCheck').show();
            surgeryError = true;
        }
        else {
            $('#surgeryCheck').hide();
        }
    }
    function validateSurgeon() {                                     //validate surgeon is choosed
        if ($('#surgeon').val() == 'Choose...') {
            $('#surgeonCheck').show();
            surgeryError = true;
        }
        else {
            $('#surgeonCheck').hide();
        }
    }
    function validateAnasthesia_type() {                               //validate anasthesia type is choosed
        if ($('#anasthesia_type').val() == 'Choose...') {
            $('#anasthesia_typeCheck').show();
            anasthesia_typeError = true;
        }
        else {
            $('#anasthesia_typeCheck').hide();
        }
    }
    /*function validateOr_theatre() {                                    //validat or theatre is choosed
        if ($('#or_theatre').val() == 'Choose...') {
            $('#or_theatreCheck').show();
            or_theatreError = true;
        }
        else {
            $('#or_theatreCheck').hide();
        }
    }*/
    function validateGender() {                                         //check weather gender radio button is selected or not
        var genderSelected = $('input[name=gender]:checked').val()
        if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
            $('#genderCheck').show();
            genderError = true;
        }
        else {
            $('#genderCheck').hide();
        }
    }

    function validateImportance() {                                     //check weather importance radio button is selected or not
        var importanceSelected = $('input[name=importance]:checked').val()
        if ((!($('#importance_high').prop('checked'))) && (!($('#importance_medium').prop('checked'))) && (!($('#importance_low').prop('checked')))) {
            $('#importanceCheck').show();
            importanceError = true;
            return false;
        }
        else {
            $('#importanceCheck').hide();
        }
    }

    function validateAnastheasist() {                                      //check weather anastheasists radio button is selected or not
        var anastheasistSelected = $('input[name=anastheasist]:checked').val()
        if ((!($('#required').prop('checked'))) && (!($('#not_required').prop('checked')))) {
            $('#anastheasistCheck').show();
            anastheasistError = true;
            return false;
        }
        else {
            $('#anastheasistCheck').hide();
        }
    }
    function validateAsaStatus() {                                     //check weather ASA status radio button is selected or not
        var statusSelected = $('input[name=asa]:checked').val()
        if ((!($('#status1').prop('checked'))) && (!($('#status2').prop('checked'))) && (!($('#status3').prop('checked')))) {
            $('#AsaCheck').show();
            asaError = true;
            return false;
        }
        else {
            $('#AsaCheck').hide();
        }
    }
})
    /*$("validate-patient-btn").click(function () {
        if (idError === false) {
            //alert("Patient validated successfully");
            return true;
        } else {
            alert("Please enter patient id correctly");
            return false;
        }
    }       
    })

    $("add-patient-btn").click(function () {
        if (patientNameError === false && weightError === false && heightError === false) {
            //alert("Patient details added successfully");
            return true;
        } else {
            alert("Please enter patient details correctly");
            return false;
        }

    })

    $("add-appointment-btn").click(function () {
        if (surgeonError === false && surgeryError === false && anasthesia_typeError === false && or_theatreError === false) {
            //alert("Surgery requirements added successfully");
            return true;
        } else {
            alert("Please fill the patient details correctly");
            return false;
        }

    })*/
