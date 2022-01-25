//// This function is used to get the list of surgeries from the backend
//async function getSurgeryTypes() {
//    var res = await axios.get("/api/AppoinmentApi/getSurgeryTypes");
//    var dto = {};
//    var surgeryTypes = [];
//    if (res.data.success == true) {
//        dto = res.data.data;
//        surgeryTypes = dto.surgeryTypes;
//    }
//    return surgeryTypes;
//}

////This function is used to get the list of surgeons from the backend
//async function getSurgeons() {
//    var res = await axios.get("/api/AppoinmentApi/getSurgeons");
//    var dto = {};
//    var surgeons = [];
//    if (res.data.success == true) {
//        dto = res.data.data;
//        surgeons = dto.surgeons;
//    }
//    return surgeons;
//}

//
/////////////////////////////////////// Sulith Google whats document ready scene eka ////////////////////////////////////
//

//$(document).ready(async () => {
//    var surgeryTypes = await getSurgeryTypes();
//    surgeryTypes.forEach(s => {
//        console.log(s);
//    });
//});

async function getDropDownLists() {
    var res = await axios.get("/api/AppoinmentApi/GetFormDropDownLists");
    var dto = {};
    var dropDownLists = {
        surgeons: [],
        surgeryTypes:[]
    }
    if (res.data.success == true) {
        dto = res.data.data;
        dropDownList.surgeons = dto.surgeons;
        dropDownList.surgeryTypes = dto.surgeryTypes;
    }
    return dropDownLists;
}



$(document).ready(async () => {
    //this function is called automatically when the html is fully loaded
});



//var data = {
//    Name : 
//}