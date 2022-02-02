
var emptyHospitalData = {
    specialities: [],
    surgeons: [],
    surgeryTypes: [],
    theatreTypes: [],
    theatres: [],
    surgeryTypeTheatres: []
};

calendarApp = Vue.createApp({
    data() {
        return {
            prevData: emptyHospitalData,
            hospitalData: emptyHospitalData,
            deleteHospitalData: {}
        };
    },
    computed: {
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
        }
    },
    methods: {
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
        specialityRemoveClick(s) {
            
        },
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
        theatreTypesForSurgeryAddClick() {

        },
        workingHoursAddClick() {

        },
        async saveHospitalDataClick() {
            await saveHospitalDataRequest(this.hospitalData);
        }
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

$(document).ready(async () => {

    var prevData = await getHospitalDataRequest();
    if (prevData != null) {
        calendarVueApp.prevData = prevData;
    }

});