var specialityInput = [];

function getNewSpeciality() {
    var newSpeciality = $('#speciality').val();

    var speciality = {
        name: newSpeciality
    }

    specialityInput.push(speciality);
}

function clearNewSpecialityField(){
    $('#speciality').val("");
}

function showNewSpecialityDetails() {
    var message = $('#speciality').val();
    display_speciality.innerHTML = message;
}

//display_speciality should be the id in the html part where the msg is displayed