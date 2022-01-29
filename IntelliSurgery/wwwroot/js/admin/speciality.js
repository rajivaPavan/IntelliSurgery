function getNewSpeciality(){
    var newSpeciality = $('#speciality').val();

    var speciality = {
        name: newSpeciality
    }

    return speciality;
}

async function addSpecialityRequest(speciality) {

    const NULL_SPECIALITY_ID = 0;
    var specialityId = NULL_SPECIALITY_ID;

    var res = await axios.post("/api/AdminApi/SaveHospitalData", hospitalData.Specialities);
    if (res.data.success == true) {
        specialityId = res.data.data;
    }
    return specialityId;
}

async function addSpeciality() {

    var speciality = getNewSpeciality();
    var specialityId = await addSpecialityRequest(speciality);
    global.addedSpecialityId = speciality;
}

function clearNewSpecialityField(){
    $('#surgery_type').val("");
}

function showSpecialityDetails() {
    var message = $('#surgery_type').val();
    display_message.innerHTML = message;
}

//display_message should be the id in the html part where the msg is displayed