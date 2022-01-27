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

namespace IntelliSurgery.Global
{
    public class SurgeryScheduler : ISurgeryScheduler
    {
        private readonly ISurgeryRepository surgeryRepository;
        private readonly IAppointmentRepository appointmentRepository;

        public SurgeryScheduler(ISurgeryRepository surgeryRepository, IAppointmentRepository appointmentRepository)
        {
            this.surgeryRepository = surgeryRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public async Task CreateSchedule(TheatreType theatreType)
        {
            ///////////  implement algorithm ///////////
            
            //filter appointments that can be done in the theatertype


            //get appointments of the following week
            var appointments = await appointmentRepository.GetAppointments(
                AppointmentQueryLogic.StatusNotEqual(Status.Completed));

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

                //appointments = appointments.Where(a => a.Status != Status.Completed).ToList();
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
