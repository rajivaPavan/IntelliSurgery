function getNewSurgeryType(){
    var newSurgeryType = $('#surgery_type').val();

    var surgeryType = {
        name: newSurgeryType
    }

    return surgeryType;
}

async function addSurgeryTypeRequest(SurgeryType) {

    const NULL_SURGERY_TYPE_ID = 0;
    var surgeryTypeId = NULL_SURGERY_TYPE_ID;

    var res = await axios.post("/api/Admin/AddSurgeryType", surgeryType);
    if (res.data.success == true) {
        surgeryTypeId = res.data.data;
    }
    return surgeryTypeId;
}

async function addSurgeryType() {

    var surgeryType = getNewSurgeryType();
    var surgeryTypeId = await addSurgeryTypeRequest(surgeryType);
    global.addedSurgeryTypeId = surgeryTypeId;
}

function clearNewSurgeryTypeField(){
    $('#surgery_type').val("");
}

function showSurgeryTypeDetails() {
    var message = $('#surgery_type').val();
    display_message.innerHTML = message;
}

//display_message should be the id in the html part where the msg is displayed