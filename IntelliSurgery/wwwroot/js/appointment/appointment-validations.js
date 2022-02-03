$(document).ready(function () {
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
        if (nameInput.match(/[^a-z ]/gi)) {                                           //you matched an invalid character
            $("#patientNameCheck").html("**Should contain only letters");
            $("#patientNameCheck").show();
            patientNameError = true;
        }
    }

    function validateWeight() {                                     //validate patient weight
        
        let weightInput = $('#weight').val();
        $("#weightCheck").css("display", "");
        if (weightInput.length == '') {
            //$('#weightCheck').show();                                                  //$("#weight").css("display", "");
            //document.getElementById("weight").style.display = "block";                //document.getElementById("weight").style.display = null;
                                                                                                        //$('weight').show();   //$('.myerror').css('display','none');
            weightError = true;
        }
        else if (!(/^\d+\.\d+$|^\d+$/.test(weightInput))) {                       //allow integers & floats      
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
        else if (!(/^\d+\.\d+$|^\d+$/.test(heightInput))) {                            //allow only integers & floats
            $('#heightCheck').html('**Invalid height-should be numeric');
            $('#heightCheck').show();
            heightError = true;
        }
    }

    function validateBirthday() {
        let birthdayInput = $('#birthday').val();

        if (birthdayInput.length == '') {
            $('#birthdaytCheck').show();
            birthdaytError = true;
        }
    }

    function validateGender() {                                         //validate patient gender radio is checked
        if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
            $('#genderCheck').show();
            genderError = true;
        }
    }

    function validateAnastheasist() {                                   
        if ($('#not_required').prop('checked')) {
            var anastheasistSelected = $('input[name=anastheasist]:checked').val();
            //disable anasthesia type combo:$("#ddlList option[value='jquery']").attr("disabled","disabled");
        }
        else {
            validateAnasthesia_type()
        }
    }

    function validateSurgery() {                                    //validate surgery combo is choosed
        if ($('#surgery').text() == '') {
            $('#surgeryCheck').show();
            surgeryError = true;
        }
    }

    function validateSurgeon() {                                    //validate surgeon combo is choosed
        if ($('#surgeon').text() == '') {
            $('#surgeonCheck').show();
            surgeryError = true;
        }
    }

    function validateAnasthesia_type() {                               //validate anasthesia type combo is choosed-this function has called inside validateAnastheasist()
        if ($('#anasthesia_type').text() == '') {
            $('#anasthesia_typeCheck').show();
            anasthesia_typeError = true;
        }
    }

    function validateOr_theatre() {                                    //validat or theatre combo is choosed
        if ($('#or_theatre').text() == '') {
            $('#or_theatreCheck').show();
            or_theatreError = true;
        }
    }

    function validateGender() {                                       
        var genderSelected = $('input[name=gender]:checked').val()                  //check weather gender radio button is selected or not
        if ((!($('#gender_male').prop('checked'))) && (!($('#gender_female').prop('checked')))) {
            $('#genderCheck').show();
            genderError = true;
        }
    }

    function validateImportance() {                                     //check weather importance radio button is selected or not
        var importanceSelected = $('input[name=importance]:checked').val()
        if ((!($('#importance_high').prop('checked'))) && (!($('#importance_medium').prop('checked'))) && (!($('#importance_low').prop('checked')))) {
            $('#importanceCheck').show();
            importanceError = true;
        }
    }

    
    //disable combo box item by using it's value : $("#ddlList option[value='jquery']").attr("disabled", "disabled");
    //disable combo box item by using it's text : $('#ddlList option:contains("HTML")').attr("disabled", "disabled");
    //$('#surgeon select').prop('disabled', true);

    function validateAsaStatus() {                                     //check weather ASA status radio button is selected or not
        var statusSelected = $('input[name=asa]:checked').val();
        if ((!($('#status1').prop('checked'))) && (!($('#status2').prop('checked'))) && (!($('#status3').prop('checked')))) {
            $('#AsaCheck').show();
            asaError = true;
            return false;
        }
    }

    $("add-patient-btn").click(function () {
        if (patientNameError === false && weightError === false && heightError === false && birthdayError == false && genderError == false && asaError == false && diseasesError == false) {
            Swal.fire({
                icon: 'success',
                title: 'Patient added successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
            return true;
        } else {
            displaySweetAlert("Please try again !");
            return false;
        }

    });

    $("add-appointment-btn").click(function () {
        if (surgeonError === false && surgeryError === false && anasthesia_typeError === false && or_theatreError === false && importanceError === false) {
            Swal.fire({
                icon: 'success',
                title: 'Surgery requirements added successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
            return true;
        } else {
            displaySweetAlert("Please try again !");
            return false;
        }
    });
});
