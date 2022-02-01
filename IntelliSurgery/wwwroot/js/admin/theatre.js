var theatreInput = [];
var theatreDisplay = [];

function getNewTheatre() {
    var newTheatre = $('#theatre').val();
    var selectedId = $("#theatrelist :selected").val();

    var theatre = {
        name: newTheatre,
        theatreTypeId: selectedId
    }

    theatreInput.push(theatre);
}

function clearNewTheatreField() {
    $('#theatre').val("");
}

function showNewTheatreDetails() {
    var message = $('#theatre').val();
    var res = '  ' + message;
    theatreDisplay.push(res);
    $('#theatrelist').text(theatreDisplay);
}
