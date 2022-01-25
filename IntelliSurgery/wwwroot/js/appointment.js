const { axios } = require("../lib/axios/axios");


// This function is used to get the list of surgeries from the backend
async function getSurgeryTypes() {
    var res = await axios.get("/api/AppoinmentApi/getSurgeryTypes");
    var dto = {};
    var surgeryTypes = [];
    if (res.data.success == true) {
        dto = res.data.data;
        surgeryTypes = dto.surgeryTypes;
    }
    return surgeryTypes;
}


$(document).ready(async () => {
    var surgeryTypes = await getSurgeryTypes();
    surgeryTypes.forEach(s => {
        console.log(s);
    });
});


//This function is used to get the list of surgeons from the backend
async function getSurgeons() {
    var res = await axios.get("/api/AppoinmentApi/getSurgeons");
    var dto = {};
    var surgeons = [];
    if (res.data.success == true) {
        dto = res.data.data;
        surgeons = dto.surgeons;
    }
    return surgeons;
}


$(document).ready(async () => {
    var surgeons = await getSurgeons();
    surgeons.forEach(s => {
        console.log(s);
    });
});



//var data = {
//    Name : 
//}