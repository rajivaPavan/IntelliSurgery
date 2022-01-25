
using IntelliSurgery.DbOperations;
using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task CreateSchedule()
        {
            await PrioritizeAppointments();

            //
            //implement algorithm
            //make necessary updates in the database
            //

        }

        public async Task<List<Appointment>> PrioritizeAppointments()
        {
            List<Appointment> appointments;
            foreach (int level in Enum.GetValues(typeof(PriorityLevel)))
            {
                appointments = await appointmentRepository.GetAppointmentsOfPriorityLevel((PriorityLevel)level);
                appointments = appointments.Where(a => a.Status != Status.Completed).ToList();
                List<TimeSpan> timeSpans = appointments.Select(a => a.PredictedTimeDuration).ToList();
                float avgTime = CalculateAverage(timeSpans);

                foreach(Appointment appointment in appointments)
                {
                    appointment.Priority = (float)appointment.PriorityLevel + appointment.PredictedTimeDuration.Ticks/avgTime;
                }

                await appointmentRepository.UpdateAppointments(appointments);
            }

            return await appointmentRepository.GetAppointments();
        }

        private float CalculateAverage(IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            return (float)averageTicks;
        }
    }

}
