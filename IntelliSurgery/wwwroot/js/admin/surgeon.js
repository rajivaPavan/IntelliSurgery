var surgeonInput = [];
var surgeonDisplay = [];

function getNewSurgeon() {
    var newSurgeon = $('#surgeon').val();
    var selectedId = $("#surgeonlist :selected").val();

    var surgeon = {
        name: newSurgeon,
        surgeonTypeId: selectedId
    }

    surgeonInput.push(theatre);
}

function clearNewTSurgeonField() {
    $('#surgeon').val("");
}

function showNewSurgeonDetails() {
    var message = $('#surgeon').val();
    var res = '  ' + message;
    surgeonDisplay.push(res);
    $('#surgeonlist').text(surgeonDisplay);
}

