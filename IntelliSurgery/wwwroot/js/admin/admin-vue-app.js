calendarApp = Vue.createApp({
    data() {
        return {
            prevData: {
                specialities: [],
                surgeons: [],
                surgeryTypes: [],
                theatreTypes: [],
                theatres: [],
                surgeryTypeTheatres: [],
                surgeonSchedules: [],
            },
            hospitalData: {
                specialities: [],
                surgeons: [],
                surgeryTypes: [],
                theatreTypes: [],
                theatres: [],
                surgeryTypeTheatres: [],
                surgeonSchedules: [],
            },
            deleteData: {
                specialities: [],
                surgeons: [],
                surgeryTypes: [],
                theatreTypes: [],
                theatres: [],
                surgeryTypeTheatres: [],
                surgeonSchedules: [],
            },
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
            await this.removeWorkingHours();
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

        removeClick(x, removeOrDelete, type) {
            if (removeOrDelete == 1) { //one means remove
                if (type == 1) {
                    this.specialityRemoveClick(x);
                }
                else if (type == 2) {
                    this.surgeonRemoveClick(x);
                }
                else if (type == 3) {
                    this.surgeryTypeRemoveClick(x);
                }
                else if (type == 4) {
                    this.theatreTypeRemoveClick(x);
                }
                else if (type == 5) {
                    this.theatreRemoveClick(x);
                }
                else if (type == 6) {

                }
            } else { //else delete
                if (type == 1) {
                    this.deleteSpecialityClick(x);
                }
                else if (type == 2) {
                    this.deleteSurgeonClick(x);
                }
                else if (type == 3) {
                    this.deleteSurgeryTypeClick(x);
                }
                else if (type == 4) {
                    this.deleteTheatreTypeClick(x);
                }
                else if (type == 5) {
                    this.deleteTheatreClick(x);
                }
                else if (type == 6) {

                }
            }
        },
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
        async specialitySaveClick() {
            var data = {
                specialities: this.hospitalData.specialities,
                deleteSpecialities: this.deleteData.specialities
            }
            var res = await saveSpecialitiesRequest(data);
            if (res != null) {
                this.hospitalData.specialities = [];
                this.prevData.specialities = this.prevData.specialities.concat(res);
            }
        },

        //surgeon
        surgeonAddClick() {
            var newSurgeon = $('#surgeon').val();
            var specialityId = $("#surgeon_speciality :selected").val();
            if (newSurgeon != "" && specialityId != -1) {
                var surgeon = {
                    name: newSurgeon,
                    speciality: {
                        id: specialityId,
                        name: $("#surgeon_speciality :selected").text()
                    }
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
        async surgeonSaveClick() {
            var data = {
                surgeons: this.hospitalData.surgeons,
                deleteSurgeons: this.deleteData.surgeons
            }
            var res = await saveSurgeonsRequest(data);
            if (res != null) {
                this.hospitalData.surgeons = [];
                this.prevData.surgeons = this.prevData.surgeons.concat(res);
            }
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
        async surgeryTypeSaveClick() {
            var data = {
                surgeryTypes: this.hospitalData.surgeryTypes,
                deleteSurgeryTypes: this.deleteData.surgeryTypes
            }
            var res = await saveSurgeryTypesRequest(data);
            if (res != null) {
                this.hospitalData.surgeryTypes = [];
                this.prevData.surgeryTypes = this.prevData.surgeryTypes.concat(res);
            }
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
        async theatreTypeSaveClick() {
            var data = {
                theatreTypes: this.hospitalData.theatreTypes,
                deleteTheatreTypes: this.deleteData.theatreTypes
            }
            var res = await saveTheatreTypesRequest(data);
            if (res != null) {
                this.hospitalData.theatreTypes = [];
                this.prevData.theatreTypes = this.prevData.theatreTypes.concat(res);
            }
        },

        //theatre
        theatreAddClick() {
            var newTheatre = $('#theatre').val();
            var selectedTheatreTypeId = $("#theatreList :selected").val();
            if (newTheatre != "" && selectedTheatreTypeId != -1) {
                var theatre = {
                    name: newTheatre,
                    theatreType: {
                        id: selectedTheatreTypeId,
                        name: $("#theatreList :selected").text()
                    }
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
        async theatreSaveClick() {
            var data = {
                theatres: this.hospitalData.theatres,
                deleteTheatres: this.deleteData.theatres
            }
            var res = await saveTheatresRequest(data);
            if (res != null) {
                this.hospitalData.theatres = [];
                this.prevData.theatres = this.prevData.theatres.concat(res);
            }
        },

        //surgery theatreTypes
        theatreTypesForSurgeryAddClick() {
            var selectedSurgeryTypeId = $("#surgery-type-data").val();
            var selectedSurgeryTypeName = $("#surgery-type-data").text();
        },
        deleteSurgeryTypeTheatresClick(s) {
            this.deleteData.surgeryTypeTheatres.push(s);
            removeElementFromArray(s, this.prevData.surgeryTypeTheatres);
        },
        theatreTypesForSurgerySaveClick(s) {
            removeElementFromArray(s, this.hospitalData.surgeryTypeTheatres);
        },

        //working hours
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
                    this.startTime = null;
                    this.endTime = null;

                } else {
                    Swal.fire(
                        'Unsuccessful!',
                        "Time is overlapping with another block",
                        'warning'
                    )
                }
            }
        },
        async removeWorkingHours() {
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
function clearNewSurgeonField() {
    $('#surgeon').val("");
    $("#surgeonlist").val(-1);
}



$(document).ready(async () => {

    var prevData = await getHospitalDataRequest();
    if (prevData != null) {
        calendarVueApp.prevData = prevData;
    }

});