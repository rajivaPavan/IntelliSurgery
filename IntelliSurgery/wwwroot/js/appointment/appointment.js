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
    return predictedTime;
}

function clearAllAppointmentFields() {
    $("#surgery").val(-1);
    $("#surgeon").val(-1);
    $("#anasthesia_type").val(-1);
    $("#theatreType").val(-1);
}
