//event listeners
$("#validate-patient-btn").click(async () => {
    global.validatedPatientId = await validatePatient();
})

$("#add-appointment-btn").click(async () => {
    var predictedTime = await addAppointment();
    Swal.fire({
        icon: "success",
        message: "Appointment added. Predicted Time duration for surgery is " + predictedTime
    });
})


function initGlobalVariable() {
    return {
        validatedPatientId: NULL_ENTITY_ID,
        addedPatientId: NULL_ENTITY_ID,
        patientId: NULL_ENTITY_ID,
        selectedSurgeonId: NULL_ENTITY_ID,
        selectedSurgeryType: NULL_ENTITY_ID,
        anesthesiaType: NULL_ENTITY_ID
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

    initSurgeons(surgeons);

    //initSurgeryTypes
    surgeryTypes.forEach((surgeryType) => {
        $('#surgery').append(new Option(surgeryType.name, surgeryType.id));
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
