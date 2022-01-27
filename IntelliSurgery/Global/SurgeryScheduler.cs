using IntelliSurgery.DbOperations.Appointments;
using IntelliSurgery.DbOperations;
using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itenso.TimePeriod;
using static IntelliSurgery.Enums.OperationTheatreEnums;
using IntelliSurgery.DbOperations.Theatres;

namespace IntelliSurgery.Global
{
    public class SurgeryScheduler : ISurgeryScheduler
    {
        private readonly ISurgeryRepository surgeryRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ITheatreRepository theatreRepository;

        public SurgeryScheduler(ISurgeryRepository surgeryRepository, IAppointmentRepository appointmentRepository, ITheatreRepository theatreRepository)
        {
            this.surgeryRepository = surgeryRepository;
            this.appointmentRepository = appointmentRepository;
            this.theatreRepository = theatreRepository;
        }

        public async Task CreateSchedule(TheatreType theatreType)
        {
            ///////////  implement algorithm ///////////
            ///
            //get list of theatres of theatreType
            List<Theatre> theatres = await theatreRepository.GetTheatres(TheatreQueryLogic.ByTheatreType(theatreType));

            //filter appointments that can be done in the theatertype
            List<Appointment> appointments = await appointmentRepository.GetAppointments(AppointmentQueryLogic.ByTheatreType(theatreType));

            //get appointments of the following week
            appointments = appointments.Where(a => a.Status != Status.Completed).ToList();


            //prioritize appointments for the following week
            appointments = await PrioritizeAppointments(appointments);
            
            //calculate time blocks


            //save time blocks in database
            

            //alocate time for surgeries
            //implement the best fit algorithm in memory management accordingly

        }

        public async Task<List<Appointment>> PrioritizeAppointments(List<Appointment> appointments)
        {
            foreach (int level in Enum.GetValues(typeof(PriorityLevel)))
            {
                appointments = await appointmentRepository.GetAppointments(
                    AppointmentQueryLogic.ByPriorityLevel((PriorityLevel)level));

                List<TimeSpan> timeSpans = appointments.Select(a => a.PredictedTimeDuration).ToList();
                float avgTime = CalculateAverage(timeSpans);

                foreach(Appointment appointment in appointments)
                {
                    appointment.Priority = (float)appointment.PriorityLevel + appointment.PredictedTimeDuration.Ticks/avgTime;
                }

                appointments = await appointmentRepository.UpdateAppointments(appointments);
            }

            return appointments;
        }

        private float CalculateAverage(IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            return (float)averageTicks;
        }

        //private List<TimeRange> CalculateTimeBlocks()
        //{

        //    return 
        //}
    }

}
