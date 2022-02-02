const emptyHospitalData = {
    specialities: [],
    surgeons: [],
    surgeryTypes: [],
    theatreTypes: [],
    theatres: [],
    surgeryTypeTheatres: [],
    surgeonSchedules:[]
};

calendarApp = Vue.createApp({
    data() {
        return {
            prevData: emptyHospitalData,
            hospitalData: emptyHospitalData,
            deleteData: emptyHospitalData
        };
    },
    computed: {
        //getPrevData
        getPrevSpecialities() {
            return this.prevData.specialities;
        },
        getPrevSurgeons() {
            return this.prevData.surgeons;
        },
        getPrevSurgeryTypes() {
            return this.prevData.surgeryTypes;
        },
        getPrevTheatreTypes() {
            return this.prevData.theatreTypes;
        },
        getPrevTheatres() {
            return this.prevData.theatres;
        },
        getPrevSurgeryTypeTheatres() {
            return this.prevData.surgeryTypeTheatres;
        },
        getPrevSchedules() {

        },
        //getNewData
        getNewSpecialities() {
            return this.hospitalData.specialities;
        },
        getNewSurgeons() {
            return this.hospitalData.surgeons;
        },
        getNewSurgeryTypes() {
            return this.hospitalData.surgeryTypes;
        },
        getNewTheatreTypes() {
            return this.hospitalData.theatreTypes;
        },
        getNewTheatres() {
            return this.hospitalData.theatres;
        },
        getNewSurgeryTypeTheatres() {
            return this.hospitalData.surgeryTypeTheatres;
        },
        getNewSchedules() {

        }
    },
    methods: {
        //speciality
        specialityAddClick() {
            var newSpeciality = $('#speciality').val();
            if (newSpeciality != "") {
                var speciality = {
                    name: newSpeciality
                };
                this.hospitalData.specialities.push(speciality);
                clearNewSpecialityField();
            }
        },
        deleteSpecialityClick(s) {
            this.deleteData.specialities.push(s);
            removeElementFromArray(s, this.prevData.specialities);
        },
        specialityRemoveClick(s) {
            removeElementFromArray(s, this.hospitalData.specialities);
        },
        async saveSpecialities() {

        },

        //surgeon
        surgeonAddClick() {
            var newSurgeon = $('#surgeon').val();
            var specialityId = $("#specialityList :selected").val();
            if (newSurgeon != "" && specialityId != -1) {
                var surgeon = {
                    name: newSurgeon,
                    surgeonTypeId: specialityId
                };
                this.hospitalData.surgeons.push(surgeon);
                clearNewSurgeonField();
            }
        },
        deleteSurgeonClick(s) {
            this.deleteData.surgeons.push(s);
            removeElementFromArray(s, this.prevData.specialities);
        },
        surgeonRemoveClick(s) {
            removeElementFromArray(s, this.hospitalData.surgeons);
        },
        async saveSurgeons() {

        },

        // surgeryType
        surgeryTypeAddClick() {
            var newSurgery = $('#surgery').val();
            if (newSurgery != "") {
                var surgery = {
                    name: newSurgery
                };
                this.hospitalData.surgeryTypes.push(surgery);
                clearNewSurgeryField();
            }
        },
        deleteSurgeryTypeClick(s) {
            this.deleteData.surgeryTypes.push(s);
            removeElementFromArray(s, this.prevData.surgeryTypes);
        },
        surgeryTypeRemoveClick(s) {
            removeElementFromArray(s, this.hospitalData.surgeryTypes);
        },
        async saveSurgeryTypes() {

        },
        //theatreType
        theatreTypeAddClick() {
            var newTheatreType = $('#theatre-type').val();
            if (newTheatreType != "") {
                var theatreType = {
                    name: newTheatreType
                };
                this.hospitalData.theatreTypes.push(theatreType);
                clearNewTheatreTypeField();
            }
        },
        deleteTheatreTypeClick(s) {
            this.deleteData.theatreTypes.push(s);
            removeElementFromArray(s, this.prevData.theatreTypes);
        },
        theatreTypeRemoveClick(s) {
            removeElementFromArray(s, this.hospitalData.theatreTypes);
        },
        async saveTheatreTypes() {

        },
        //theatre
        theatreAddClick() {
            var newTheatre = $('#theatre').val();
            var selectedId = $("#theatreList :selected").val();
            if (newTheatre != "" && selectedId != -1) {
                var theatre = {
                    name: newTheatre,
                    theatreTypeId: selectedId
                };
                this.hospitalData.theatres.push(theatre);
                clearNewTheatreField();
            }

        },
        deleteTheatreClick(s) {
            this.deleteData.theatres.push(s);
            removeElementFromArray(s, this.prevData.theatres);
        },
        theatreRemoveClick(s) {
            removeElementFromArray(s, this.hospitalData.theatres);
        },
        async saveTheatres() {

        },
        //surgery theatreTypes
        theatreTypesForSurgeryAddClick() {

        },

        deleteSurgeryTypeTheatresClick(s) {
            this.deleteData.surgeryTypeTheatres.push(s);
            removeElementFromArray(s, this.prevData.surgeryTypeTheatres);
        },
        surgeryTypeTheatresRemoveClick(s) {
            removeElementFromArray(s, this.hospitalData.surgeryTypeTheatres);
        },

        //schedules
        workingHoursAddClick() {

        },
        deleteWorkingHoursClick(s) {
            
        },
        workingHoursRemoveClick(s) {
            
        },
    }

});

calendarVueApp = calendarApp.mount("#admin-main");

function clearNewTheatreTypeField() {
    $('#theatre-type').val("");
}
function clearNewTheatreField() {
    $('#theatre').val("");
    $("#theatreList").val(-1);
}
function clearNewSurgeryField() {
    $('#surgery').val("");
}
function clearNewSpecialityField() {
    $('#speciality').val("");
}

function removeElementFromArray(el, arr) {
    var index = arr.indexOf(el);
    if (index != -1) {
        arr.splice(index, 1);
    }
    return index;
}

$(document).ready(async () => {

    var prevData = await getHospitalDataRequest();
    if (prevData != null) {
        calendarVueApp.prevData = prevData;
    }

});