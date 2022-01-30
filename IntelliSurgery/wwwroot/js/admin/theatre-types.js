var theatreTypeInput = [];

function getNewTheatreType() {
    var newTheatreType = $('#theatre-type').val();

    var theatreType = {
        name: newTheatreType
    }

    theatreInput.push(theatreType);
}

function clearNewTheatreTypeField() {
    $('#theatre-type').val("");
}

function showNewTheatreTypeDetails() {
    var message = $('#theatre-type').val();
    display_theatre_type.innerHTML = message;
}

//display_speciality should be the id in the html part where the msg is displayed