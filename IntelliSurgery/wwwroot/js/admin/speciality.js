var specialityInput = [];
var specialityDisplay = [];

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
    var res = '  ' + message;
    specialityDisplay.push(res);
    $('#specialitylist').text(specialityDisplay);
    //document.getElementById("#speciality_list").innerHTML = message;
    // $('#speciality_list').append(new Option(message))
    // $('#display_speciality').append(new Option())
}

//speciality should be the id in the 'list' button