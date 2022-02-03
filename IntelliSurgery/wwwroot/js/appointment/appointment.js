function getAppointmentDetails(patientId) {
    var appointment = {
        PatientId: patientId,
        SurgeryType: $("#surgery").val(),
        TheatreType:$("#theatreType").val(),
        SurgeonId: $("#surgeon").val(),
        IsAnesthesiaRequired: true, //
        AnesthesiaType: 0 /* $("#anasthesia_type").val()*/,
        PriorityLevel: $("input:radio.priority:checked").val()
    };
    return appointment;
}

async function addAppointment(patientId) {
    var predictedTime = null;
    var appointment = getAppointmentDetails(patientId);
    predictedTime = await addAppointmentRequest(appointment);

    var durationInputValue = ""

    Swal.fire({
        title: 'Predicted Time Duration',
        text: predictedTime,
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Accept',
        cancelButtonText: 'Override'
    }).then(async (result) => {
        if (!result.isConfirmed) {

            const { value: newTimeDuration } = await Swal.fire({
                title: 'Enter new time duration',
                input: 'text',
                inputPlaceholder: "hours : minutes",
                showCancelButton: true,
                inputValidator: (value) => {
                    if (!value) {
                        return 'Time duration should be in the format \"hours:minutes\"'
                    }
                }
            })
        }
    })

    if (newTimeDuration != "") {

    }

    
    return;
}

function clearAllAppointmentFields() {
    $("#surgery").val(-1);
    $("#surgeon").val(-1);
    $("#anasthesia_type").val(-1);
    $("#theatreType").val(-1);
}