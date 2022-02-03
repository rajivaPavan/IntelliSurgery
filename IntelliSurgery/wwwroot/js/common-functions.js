
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

async function axiosPostWithData(url,data) {
    var res = await axios.post(url, data);
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}