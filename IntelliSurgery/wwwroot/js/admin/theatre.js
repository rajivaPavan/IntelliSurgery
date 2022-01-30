var theatreInput = [];
var theatreDisplay = [];

function getNewTheatre() {
    var newTheatre = $('#theatre').val();
    var selectedId = $("#theatretypelist :selected").val();

    var theatre = {
        name: newTheatre,
        theatreTypeId: selectedId
    }

    theatreInput.push(theatre);
}

function clearNewTheatreField() {
    $('#theatre-type').val("");
}

function showNewTheatreDetails() {
    var message = $('#theatre-type').val();
    var res = '  ' + message;
    theatreDisplay.push(res);
    $('#theatretypelist').text(theatreDisplay);
}
