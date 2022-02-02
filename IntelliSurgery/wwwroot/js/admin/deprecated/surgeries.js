var surgeryInput = [];
var surgeryDisplay = [];

function getNewSurgery() {
    var newSurgery = $('#surgery').val();

    var surgery = {
        name: newSurgery
    }

    surgeryInput.push(surgery);
}

function clearNewSurgeryField() {
    $('#surgery').val("");
}

function showNewSurgeryDetails() {
    var message = $('#surgery').val();
    var res = '  ' + message;
    surgeryDisplay.push(res);
    $('#surgerylist').text(surgeryDisplay);
}

//display_surgery should be the id in the html part where the msg is displayed