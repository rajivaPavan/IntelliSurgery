//event listeners
$("#validate-patient-btn").click(async () => {   
    if (validateId()) {                                         
        global.patientId = await validatePatient();      //-----------------------------------id eka return karanne ?????
        Swal.fire({
            icon: 'success',
            title: 'Patient validated successfully',
            showConfirmButton: false,                   //if false given,no need to press ok button
            timer: 1500
        });
        return true;
    } else {
        displaySweetAlert("Please try again !");
        return false;
    }
})

$("#add-patient-btn").click(async () => {    
    //global.patientId = await addPatient();

    if (!(nameError || weightError || heightError || birthdayError || genderError || asaError)) {
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
})

$("#update-patient-btn").click(async () => {
    //global.patientId = await updatePatient();

    if (!(nameError || weightError || heightError || birthdayError || genderError || asaError)) {
        Swal.fire({
            icon: 'success',
            title: 'Patient updated successfully',
            showConfirmButton: false,                   //if false given,no need to press ok button
            timer: 1500
        });
        return true;
    } else {
        displaySweetAlert("Please try again !");
        return false;
    }
})

$("#add-appointment-btn").click(async () => {       //----------------both validate button & add patient buttons 2 nma true return karala kiyala check karanne?????  
    if (!(surgeonError || surgeryError || anastheasistError || anasthesia_typeError || theatreError || importanceError)) {
        Swal.fire({
            icon: 'success',
            title: 'Surgery requirements added successfully',
            showConfirmButton: false,                   //if false given,no need to press ok button
            timer: 1500
        });
        await addAppointment(global.patientId);
        resetForms();

        return true;
    } else {
        displaySweetAlert("Validate and or Add a Patient first !");
        return false;
    }

    //if (global.patientId != NULL_ENTITY_ID) {
    //   await addAppointment(global.patientId);
    //  resetForms();
        
    // } else {
    //   displaySweetAlert("Validate and or Add a Patient first");
    //}
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
