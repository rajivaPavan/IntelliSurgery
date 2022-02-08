appointmentApp = Vue.createApp({
    data() {
        return {
            selectedSurgeryTypeId:-1,
            surgeons: [],
            theatreTypes: [],
            surgeryTypes:[]
        };
    },
    watch: {
        
    },
    computed: {
        getFilterSurgeons() {
            return this.filterSurgeons(this.selectedSurgeryTypeId);
        },
        getFilteredTheatreTypes() {
            return this.filterTheatreTypes(this.selectedSurgeryTypeId);
        }
    },
    methods: {
        filterSurgeons(surgeryTypeId) {
            return this.surgeons; ////
        },
        filterTheatreTypes(surgeryTypeId) {

        }
    }

});

appointmentVueApp = calendarApp.mount("#appointment-page-main");