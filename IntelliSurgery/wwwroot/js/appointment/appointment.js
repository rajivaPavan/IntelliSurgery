function getAppointmentDetails(patientID) {
    var appointment = {
        PatientId: patientId,
        SurgeryType: $("#surgery").val(),
        SurgeonId: $("#surgeon").val(),
        AnesthesiaType: $("#anesthesia").val(),
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

