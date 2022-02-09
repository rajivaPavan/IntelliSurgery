function getAppointmentDetails(patientId) {
    var appointment = {
        PatientId: patientId,
        SurgeryType: $("#surgery").val(),
        TheatreType:$("#theatreType").val(),
        SurgeonId: $("#surgeon").val(),
        Complication: $("input[name=complication]:checked").val() == "1" ? true : false,
        AnesthesiaType: $("input[name=anasthesia_type]:checked").val(),
        PriorityLevel: $("input[name=importance]:checked").val()
    };
    return appointment;
}

async function addAppointment(patientId) {

    var appointment = getAppointmentDetails(patientId);
    $("#loading").show();
    var res = await addAppointmentRequest(appointment);
    $("#loading").hide();
    var predictedTime = res.predicatedTime;
    var appointmentId = res.appointmentId;
    if (appointmentId != -1 && predictedTime != null) {

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

                const { value: inputVal } = await Swal.fire({
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

                var newTimeDuration = inputVal;
                if (newTimeDuration != "") {
                    var isSuccess = await overrideTimeDurationRequest(appointmentId, newTimeDuration);
                    if (isSuccess) {
                        Swal.fire({
                            icon: 'success',
                            text: 'Time overrided sucessfully'
                        });
                    }
                }
            }
        })
    } else {
        displaySweetAlert("Error occured. Appointment was not added.")
    }
}

function clearAllAppointmentFields() {
    $("#surgery").val(-1);
    $("#surgeon").val(-1);
    $("#anasthesia_type").val(-1);
    $("#theatreType").val(-1);
}