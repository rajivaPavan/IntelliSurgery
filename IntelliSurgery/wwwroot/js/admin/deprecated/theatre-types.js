var theatreTypeInput = [];
var theatreTypeDisplay = [];

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
    var res = '  ' + message;
    stheatreTypeDisplay.push(res);
    $('#theatretypelist').text(theatreTypeDisplay);
}

