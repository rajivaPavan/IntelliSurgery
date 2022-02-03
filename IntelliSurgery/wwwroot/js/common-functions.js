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

function removeElementFromArray(el, arr) {
    var index = arr.indexOf(el);
    if (index != -1) {
        arr.splice(index, 1);
    }
    return index;
}

function removeElementAtIndex(elementIndex, arr) {
    if (elementIndex != -1) {
        arr.splice(elementIndex, 1);
    }
    return arr;
}