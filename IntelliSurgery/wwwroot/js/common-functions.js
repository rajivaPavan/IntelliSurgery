
function displaySweetAlert(message) {
    Swal.fire({
        icon: "error",
        text: message
    });
}

function formatDateTime(datetime) {
    var dateAndTime = datetime.split("T");
    var date = dateAndTime[0];
    return date;
}