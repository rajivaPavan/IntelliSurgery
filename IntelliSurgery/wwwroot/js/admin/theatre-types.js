var theatreTypeInput = [];

function getNewTheatreType() {
    var newTheatreType = $('#theatre-type').val();

    var theatreType = {
        name: newTheatreType
    }

    theatreInput.push(theatreType);
}

function clearNewTheatreTypeField() {
    $('#speciality').val("");
}

function showNewTheatreTypeDetails() {
    var message = $('#speciality').val();
    display_speciality.innerHTML = message;
}

//display_speciality should be the id in the html part where the msg is displayed