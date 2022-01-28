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
using IntelliSurgery.DbOperations.WorkingBlocks;

namespace IntelliSurgery.Global
{
    public class SurgeryScheduler : ISurgeryScheduler
    {
        private readonly ISurgeryRepository surgeryRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly ITheatreRepository theatreRepository;
        private readonly IWorkingBlockRepository workingBlockRepository;
        private readonly TimeSpan prepTime = new(0,5,0);
        private readonly TimeSpan cleanTime = new(0, 5, 0);

        public SurgeryScheduler(ISurgeryRepository surgeryRepository, IAppointmentRepository appointmentRepository, 
            ITheatreRepository theatreRepository, IWorkingBlockRepository workingBlockRepository)
        {
            this.surgeryRepository = surgeryRepository;
            this.appointmentRepository = appointmentRepository;
            this.theatreRepository = theatreRepository;
            this.workingBlockRepository = workingBlockRepository;
        }

        public async Task CreateSchedule(TheatreType theatreType)
        {
           
            //get list of theatres of theatreType
            List<Theatre> theatres = await theatreRepository.GetTheatres(TheatreQueryLogic.ByTheatreType(theatreType));

            //filter appointments that can be done in the theatertype
            List<Appointment> appointments = await appointmentRepository.GetAppointments(AppointmentQueryLogic.ByTheatreType(theatreType));

            //get appointments of the following week
            appointments = appointments.Where(a => a.Status != Status.Completed).ToList();

            //get list of surgeons allocated to the above appointments
            List<Surgeon> surgeons = appointments.Select(a => a.Surgeon).Distinct().ToList();

            //sort surgeons in descending order of total time of surgeries

            //prioritize appointments for the following week
            appointments = await PrioritizeAppointments(appointments);

            //calculate time blocks
            List<WorkingBlock> workingBlocks = CalculateTimeBlocks();

            //allocate time for surgeries within the time blocks
            workingBlocks = await AllocateSurgeriesToBlocks(workingBlocks, appointments);
            
            //add blocks to the database
            await workingBlockRepository.AddBlocks(workingBlocks);

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

        private List<WorkingBlock> CalculateTimeBlocks()
        {
            ////
            //////
            ///// IMPLEMENT
            //////
            /////
            return new List<WorkingBlock>();
        }

        private async Task<List<WorkingBlock>> AllocateSurgeriesToBlocks(List<WorkingBlock> workingBlocks, 
            List<Appointment> appointments)
        {
            ///////best fit algorithm in memory management//////
            
            //initially every appointment is unscheduled

            int numOfBlocks = workingBlocks.Count;
            int numOfAppointments = appointments.Count;

            TimeSpan blockDuration;
            TimeSpan surgeryDuration;
            Appointment currentAppointment;
            WorkingBlock currentBlock;

            for(int i = 0; i < numOfAppointments; i++)
            {
                int bestBlockIndex = -1;
                WorkingBlock bestBlock = null;

                currentAppointment = appointments.ElementAt(i);
                surgeryDuration = GetFinalSurgeryDuration(currentAppointment.PredictedTimeDuration);

                for (int j = 0; j < numOfBlocks; j++)
                {
                    currentBlock = workingBlocks[j];
                    blockDuration = currentBlock.Duration;

                    //find the best block index for the current appointment
                    if (blockDuration >= surgeryDuration)
                    {
                        if (bestBlock == null || bestBlock.Duration > blockDuration)
                        {
                            bestBlockIndex = j;
                            bestBlock = workingBlocks[bestBlockIndex];   
                        }
                    }
                }
                //if a block was found for the current appointment
                if (bestBlockIndex != -1)
                {
                    //set appointment status to scheduled
                    currentAppointment.Status = Status.Scheduled;

                    //reduce remaining time in block
                    bestBlock.RemainingTime = bestBlock.RemainingTime.Subtract(surgeryDuration);

                    //set appointment surgery duration with preparation and cleanging time
                    TimeRange surgeryTimeRange = new TimeRange()
                    {
                        Start = bestBlock.End.Subtract(bestBlock.RemainingTime),
                        Duration = surgeryDuration
                    };
                    currentAppointment.ScheduledSurgery.SurgeryEvent.SetTimeRange(surgeryTimeRange);

                   

                    //update appointmentRepo
                    currentAppointment = await appointmentRepository.UpdateAppointment(currentAppointment);

                    //add scheduled surgery to working block
                    bestBlock.AllocatedSurgeries.Add(currentAppointment.ScheduledSurgery);
                }
                else
                {
                    //set unscheduled appointment status to InWaitingList
                    currentAppointment.Status = Status.InWaitingList;
                    currentAppointment = await appointmentRepository.UpdateAppointment(currentAppointment);
                }

                //update best working block
                workingBlocks[bestBlockIndex] = bestBlock;
            }
            return workingBlocks;
        }

        private TimeSpan GetFinalSurgeryDuration(TimeSpan predictedTime)
        {
            return predictedTime.Add(prepTime).Add(cleanTime);
        }
        

    }

}

