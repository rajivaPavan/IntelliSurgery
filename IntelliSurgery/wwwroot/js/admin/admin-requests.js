async function getHospitalDataRequest() {
    var res = await axios.get("/api/AdminApi/GetHospitalData");
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}

async function saveHospitalDataRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveHospitalData", hospitalData);
}

async function getWorkingBlocksRequest(surgeonId) {
    var res = await axios.post("/api/StaffApi/GetWorkingBlocks?surgeonId=" + surgeonId);
    if (res.data.success == true) {
        return res.data.data;
    }
    return null;
}

async function saveWorkingBlockRequest(block) {
    var res = await axios.post("/api/StaffApi/SaveWorkingBlock", block);
    if (res.data.success == false) {
        Swal.fire(
            'Unsuccessful!',
            res.data.message,
            'warning'
        )
        return null;
    }
    return res.data.data;
}

async function deleteWorkingBlockRequest(blockId) {
    var res = await axios.post("/api/StaffApi/DeleteWorkBlock?workingBlockId=" + blockId);
    return res.data.success;
}

async function saveSpecialitiesRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveSpecialities", hospitalData);
}
async function saveSurgeonsRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveSurgeons", hospitalData);
}
async function saveSurgeryTypesRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveSurgeryTypes", hospitalData);
}
async function saveTheatreTypesRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveTheatreTypes", hospitalData);
}
async function saveTheatresRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveTheatres", hospitalData);
}
async function saveSurgeryTheatreTypesRequest(hospitalData) {
    return await axiosPostWithData("/api/AdminApi/SaveTheatres", hospitalData);
}