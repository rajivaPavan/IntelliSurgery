const emptyHospitalData = {
    specialities: [],
    surgeons: [],
    surgeryTypes: [],
    theatreTypes: [],
    theatres: [],
    surgeryTypeTheatres: [],
    surgeonSchedules: [],
};

calendarApp = Vue.createApp({
    data() {
        return {
            prevData: emptyHospitalData,
            hospitalData: emptyHospitalData,
            deleteData: emptyHospitalData,
            selectedWorkingBlock: null,
            selectedSurgeonId: -1,
            selectedTheatreId: -1,
            calendars: {},
            fullCalendar: {},
            startTime: "",
            endTime: "",
            tryDeleteSelectedBlock : 0
        };
    },
    watch: {
        tryDeleteSelectedBlock: async function () {
            var selectedBlock = this.selectedWorkingBlock;
            if (selectedBlock != null) {

                var isSuccess = await deleteWorkingBlockRequest(selectedBlock.workingBlockId);
                if (isSuccess == true) {
                    var calendarEvents = this.calendars[this.selectedSurgeonId].events;

                    //find event from calendar events array in data
                    var blockIndex = -1;
                    for (var i = 0; i < calendarEvents.length; i++) {
                        if (calendarEvents[i].id == selectedBlock.eventId) {
                            blockIndex = i;
                            break;
                        }
                    }

                    if (blockIndex != -1) {
                        calendarEvents = removeElementAtIndex(blockIndex, calendarEvents);
                    }

                    this.calendars[this.selectedSurgeonId].events = calendarEvents;

                    //remove event from full calendar
                    var event = this.fullCalendar.getEventById(selectedBlock.eventId);
                    event.remove();
                }
                else {
                    Swal.fire(
                        'Deleted!',
                        "Cannot remove the block, as surgeries have been asigned already.",
                        'error'
                    )
                }


            }
        }
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

        },

        //getWorkBlock
        getSelectedWorkBlock() {
            return this.selectedWorkBlock;
        },

        
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
        async renderCalendar() {
            var selectedSurgeonId = this.selectedSurgeonId;
            if (selectedSurgeonId != -1) {
                //if surgeons calendar has been retrieved before
                var calendarEvents = null;
                if (this.calendars.hasOwnProperty(selectedSurgeonId)) {
                    calendarEvents = this.calendars[selectedSurgeonId].events;
                }
                else {
                    var surgeonCalendarDT0 = await getWorkingBlocksRequest(selectedSurgeonId);
                    if (surgeonCalendarDT0 != null) {
                        this.calendars[selectedSurgeonId] = surgeonCalendarDT0;
                        calendarEvents = surgeonCalendarDT0.events;
                    }
                }
                if (calendarEvents != null) {
                    this.fullCalendar = initSurgeonCalendar(calendarEvents);
                }
            }
        },
        async addWorkingHoursClick() {
            var selectedSurgeonId = this.selectedSurgeonId;
            var selectedTheatreId = this.selectedTheatreId;
            var startTime = $("#start-time").val();
            var endTime = $("#end-time").val();
            if (selectedSurgeonId != -1 && selectedTheatreId !=- 1 && startTime!="" && endTime != "") {
                var workBlock = {
                    start: startTime,
                    end: endTime,
                    surgeonId: selectedSurgeonId,
                    theatreId: selectedTheatreId
                }

                var fullcalendarevent = await saveWorkingBlockRequest(workBlock);
                if (fullcalendarevent != null) {
                    this.calendars[selectedSurgeonId].events.push(fullcalendarevent);
                    this.fullCalendar.addEvent(fullcalendarevent);

                    //reset fields
                    this.selectedTheatreId = -1;
                    this.startTime = "";
                    this.endTime = "";

                } else {
                    Swal.fire(
                        'Unsuccessful!',
                        "Time is overlapping with another block",
                        'warning'
                    )
                }


                
            }
        },
        async removeWorkingHoursClick() {
            
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

$(document).ready(async () => {

    var prevData = await getHospitalDataRequest();
    if (prevData != null) {
        calendarVueApp.prevData = prevData;
    }

});