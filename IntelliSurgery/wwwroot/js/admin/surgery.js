function getNewSurgery() {
    var newSurgery = $('#surgery').val();

    var surgery = {
        name: newSurgery
    }

    return surgery;
}

async function addSurgeryRequest(Surgery) {

    const NULL_SURGERY_ID = 0;
    var surgeryId = NULL_SURGERY_ID;

    var res = await axios.post("/api/Admin/AddSurgery", surgery);
    if (res.data.success == true) {
        surgeryId = res.data.data;
    }
    return surgeryId;
}

async function addSurgery() {

    var surgery= getNewSurgery();
    var surgeryId = await addSurgeryRequest(surgery);
    global.addedSurgeryId = surgeryId;
}

function clearNewSurgeryField() {
    $('#surgery').val("");
}

function showSurgeryDetails() {
    var message = $('#surgery').val();
    display_message.innerHTML = message;
}

