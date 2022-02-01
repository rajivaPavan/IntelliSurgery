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

function clearNewSurgeonField() {
    $('#surgeon').val("");
    $("#surgeonlist").val(-1);
}

function showNewSurgeonDetails() {
    var message = $('#surgeon').val();
    var res = '  ' + message;
    surgeonDisplay.push(res);
    $('#surgeonlist').text(surgeonDisplay);
}

function clearNewSurgeonField() {
    $('#surgeon').val("");
    $("#surgeonlist").val(-1);
}

