function clearAllPatientFields() {
    $("#patient_name").val("");
    $("#birthday").val("");
    $("#weight").val("");
    $("#height").val("");
}

function showPatientAddBtn(ishow) {
    if (ishow) {
        $("#update-patient-btn").hide();
        $("#add-patient-btn").show();
    } else {
        $("#add-patient-btn").hide();
        $("#update-patient-btn").show();
    }
}

function disablePatientConstantFields(isDisable) {
    $("#patient_name").prop("disabled", isDisable);
    $("#birthday").prop("disabled", isDisable);
    $("input:radio.gender").prop("disabled", isDisable);
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
    $("input[name=asa][value=" + patientDetails.asaStatus + "]").prop('checked', true);
    $("#weight").val(patientDetails.weight);
    $("#height").val(patientDetails.height);
    if (patientDetails.diseasesValues.length != 0) {
        patientDetails.diseasesValues.forEach((val) => {
            $("input[name=disease][value=" + val + "]").prop('checked', true);
        });
    }
    
}

function getPatientDetails() {
    var name = $("#patient_name").val();
    var dateOfBirth = $("#birthday").val();
    var gender = $("input[name=gender]:checked").val();
    var weight = $("#weight").val();
    var height = $("#height").val();
    var asaStatus = $("input[name=asa]:checked").val();
    var diseases = [];
    $("input[name=disease]:checked").each(function () {
        diseases.push($(this).val());
    });
    var patient = {
        Name: name,
        DateOfBirth: dateOfBirth,
        Weight: weight,
        Height: height,
        Gender: gender,
        AsaStatus: asaStatus,
        DiseasesValues:diseases
    };

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

            showPatientAddBtn(false);
            Swal.fire({
                icon: 'success',
                title: 'Patient validated successfully',
                showConfirmButton: false,
                timer: 1500
            });

        } else {
            //patient doesnot exits
            Swal.fire({
                icon: 'info',
                title: "Patient with id "+patientId+" not found!",
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });

            patientId = NULL_ENTITY_ID;
            clearAllPatientFields();
            enableAllPatientFields();

            showPatientAddBtn(true);
            

        }
    } else {
        //displaySweetAlert("Patient Id is empty");
    }

    return patientId;
}

async function addPatient() {
    var patientId = NULL_ENTITY_ID;
    var patient = getPatientDetails();
    patientId = await addPatientRequest(patient);
    return patientId;
}

async function updatePatient(patientId) {
    var patient = getPatientDetails();
    patient["PatientId"] = patientId;
    var patientId = await updatePatientRequest(patient);
    return patientId;
}