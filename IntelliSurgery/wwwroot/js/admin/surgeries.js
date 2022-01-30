var surgeryInput = [];

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
    display_surgery.innerHTML = message;
}

//display_surgery should be the id in the html part where the msg is displayed