//event listeners
$("#validate-patient-btn").click(async () => {   
    //if (global.patientId != NULL_ENTITY_ID) {
    if ($('#patient_id').val() != '') {
        validateId();
        if (!(idError)) {
           
            Swal.fire({
                icon: 'success',
                title: 'Patient Validated Successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
            return true;
        } else {
            displaySweetAlert("Enter Correct Patient ID !");
            validateId();
            return false;
        }
    } else {
        displaySweetAlert("Enter Patient ID First !");
        validateId();
        return false;
    }
})

$("#add-patient-btn").click(async () => {    
    if (global.patientId != NULL_ENTITY_ID) {
        if (!(nameError || weightError || heightError || birthdayError || genderError || asaError)) {
            global.patientId = await addPatient();
            Swal.fire({
                icon: 'success',
                title: 'Patient Added Successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
            return true;
        }
        else {
            displaySweetAlert("Enter correct details !");
            validateName();
            validateWeight();
            validateHeight();
            validateBirthday();
            validateGender();
            validateAsaStatus();
        }
    }
    else {
        displaySweetAlert("Validate Patient First !");
        return false;
    }
})

$("#update-patient-btn").click(async () => {
    if (global.patientId != NULL_ENTITY_ID) {
        if (!(nameError || weightError || heightError || birthdayError || genderError || asaError)) {
            global.patientId = await updatePatient();
            Swal.fire({
                icon: 'success',
                title: 'Patient Updated Successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
            return true;
        }
        else {
            displaySweetAlert("Enter correct details !");
            validateName();
            validateWeight();
            validateHeight();
            validateBirthday();
            validateGender();
            validateAsaStatus();
        }
    }
    else {
        displaySweetAlert("Validate Patient First !");
        return false;
    }
})

$("#add-appointment-btn").click(async () => {       //----------------both validate button & add patient buttons 2 nma true return karala kiyala check karanne?????  
    if (global.patientId != NULL_ENTITY_ID) {
        //if (!(surgeonError || surgeryError || anastheasistError || anasthesia_typeError || theatreError || importanceError)) {
            await addAppointment(global.patientId);
            resetForms();
            return true;
        }
        else {
            displaySweetAlert("Enter correct details !");
            validateSurgery();
            validateSurgeon();
            validateTheatre();
            validateAnastheasist();
            validatevalidateAnasthesia_type();
            validateImportance();
        }
    }
     else {
        displaySweetAlert("Validate or Add a Patient first !");
        return false;
    }
})

function resetForms() {
    global = initGlobalVariable()
    clearAllFields();
    enableAllPatientFields();
    showPatientAddBtn(true);
}

function clearAllFields() {
    $("#patient_id").val("");
    clearAllPatientFields();
    clearAllAppointmentFields();
}


function initGlobalVariable() {
    return {
        patientId: NULL_ENTITY_ID
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
    var anesthesiaTypes = dropDowns.anesthesiaTypes;
    var theatreTypes = dropDowns.theatreTypes;

    initSurgeons(surgeons);

    surgeryTypes.forEach((s) => {
        $('#surgery').append(new Option(s.name, s.id));
    });
    anesthesiaTypes.forEach((a) => {
        $('#anasthesia_type').append(new Option(a.name, a.id));
    });
    theatreTypes.forEach((t) => {
        $('#theatreType').append(new Option(t.name, t.id));
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
