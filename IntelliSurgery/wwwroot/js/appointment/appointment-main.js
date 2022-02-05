//event listeners
$("#validate-patient-btn").click(async () => {
    var isValidationSuccess = validateId();
    if (isValidationSuccess) {
        global.patientId = await validatePatient();
        Swal.fire({
            icon: 'success',
            title: 'Patient Validated Successfully',
            showConfirmButton: false,                   //if false given,no need to press ok button
            timer: 1500
        });
    }
})

$("#add-patient-btn").click(async () => {    
    if (global.patientId == NULL_ENTITY_ID) {
        var isValidationSuccess = finalAddPatientValidation();
        if (isValidationSuccess) {
            global.patientId = await addPatient();
            Swal.fire({
                icon: 'success',
                title: 'Patient Added Successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
        }
    }else {
        displaySweetAlert("Validate Patient First !");
    }
})

$("#update-patient-btn").click(async () => {
    if (global.patientId != NULL_ENTITY_ID) {
        var isValidationSuccess = finalUpdatePatientValidation();
        if (isValidationSuccess) {
            global.patientId = await updatePatient(global.patientId);
            Swal.fire({
                icon: 'success',
                title: 'Patient Updated Successfully',
                showConfirmButton: false,                   //if false given,no need to press ok button
                timer: 1500
            });
        }
    }else {
        displaySweetAlert("Validate Patient First !");
    }
})

$("#add-appointment-btn").click(async () => {       //----------------both validate button & add patient buttons 2 nma true return karala kiyala check karanne?????  
    if (global.patientId != NULL_ENTITY_ID) {
        var isValidationSuccess = finalAppointmentValidation();
        if (isValidationSuccess) {
            await addAppointment(global.patientId);
            resetForms();
            return true;
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
