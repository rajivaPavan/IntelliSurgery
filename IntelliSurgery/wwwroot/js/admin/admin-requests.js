async function getHospitalDataRequest() {
    var res = await axios.get("/api/AdminApi/GetHospitalData");
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}

async function saveHospitalDataRequest(hospitalData) {
    var res = await axios.post("/api/AdminApi/SaveHospitalData", hospitalData);
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}

async function getWorkingBlocksRequest(surgeonId) {
    var res = await axios.post("/api/StaffApi/GetWorkingBlocks?surgeonId=" + surgeonId);
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}

async function saveWorkingBlockRequest(block){
    var res = await axios.post("/api/StaffApi/SaveWorkingBlock", block);
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}
