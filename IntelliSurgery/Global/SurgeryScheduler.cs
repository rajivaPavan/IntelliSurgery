using IntelliSurgery.DbOperations.Appointments;
using IntelliSurgery.DbOperations;
using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Itenso.TimePeriod;
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

        public async Task CreateSchedule(Surgeon surgeon)
        {
            //get incomplete appointments of the surgeon
            List<Appointment> appointments = await appointmentRepository.GetAppointments(a => a.SurgeonId == surgeon.Id && a.Status != Status.Completed);

            //prioritize appointments for the following week
            //appointments = await PrioritizeAppointments(appointments);

            //get time blocks of the surgeon that start after the current time
            List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(w => w.SurgeonId == surgeon.Id && w.Start > DateTime.Now);

            //allocate time for surgeries within the time blocks
            workingBlocks = await AllocateSurgeriesToBlocks(workingBlocks, appointments);

            //update blocks in the database
            await workingBlockRepository.UpdateWorkingBlocks(workingBlocks);

        }
        private float CalculateAverage(IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            return (float)averageTicks;
        }

        private async Task<List<WorkingBlock>> AllocateSurgeriesToBlocks(List<WorkingBlock> workingBlocks, 
            List<Appointment> appointments)
        {
            ///////best fit algorithm in memory management//////
            
            //initially every appointment is unscheduled

            int numOfBlocks = workingBlocks.Count;
            int numOfAppointments = appointments.Count;

            TimeSpan blockDuration;
            TimeSpan finalSurgeryDuration;
            Appointment currentAppointment;
            WorkingBlock currentBlock;

            for(int i = 0; i < numOfAppointments; i++)
            {
                int bestBlockIndex = -1;
                WorkingBlock bestBlock = null;

                currentAppointment = appointments.ElementAt(i);

                finalSurgeryDuration = GetFinalSurgeryDuration(currentAppointment);
                if (finalSurgeryDuration == TimeSpan.Zero)
                {
                    //if neither the system nor surgeon has suggested a time duration, skip the appointment
                    continue;
                }

                for (int j = 0; j < numOfBlocks; j++)
                {
                    currentBlock = workingBlocks[j];
                    blockDuration = currentBlock.RemainingTime;

                    //find the best block index for the current appointment
                    if (blockDuration >= finalSurgeryDuration)
                    {
                        if (bestBlock == null || bestBlock.RemainingTime > blockDuration)
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
                    bestBlock.RemainingTime = bestBlock.RemainingTime.Subtract(finalSurgeryDuration);

                    //set appointment surgery duration with preparation and cleanging time
                    TimeRange surgeryTimeRange = new TimeRange()
                    {
                        Start = bestBlock.End.Subtract(bestBlock.RemainingTime),
                        Duration = finalSurgeryDuration
                    };

                    //add scheduled surgery to database and add to current appointment
                    SurgeryEvent surgeryEvent = new SurgeryEvent();
                    surgeryEvent.SetTimeRange(surgeryTimeRange);

                    ScheduledSurgery scheduledSurgery = await surgeryRepository.AddSurgery(
                            new ScheduledSurgery() { 
                                SurgeryEvent = surgeryEvent,
                                WorkingBlockId = bestBlock.Id,
                            }
                        );
                    currentAppointment.ScheduledSurgery = scheduledSurgery;
                    currentAppointment.ScheduledSurgeryId = scheduledSurgery.Id;

                    //set theatre for the appointment
                    currentAppointment.Theatre = bestBlock.Theatre;
                    currentAppointment.TheatreId = bestBlock.TheatreId;

                    //update appointmentRepo
                    currentAppointment = await appointmentRepository.UpdateAppointment(currentAppointment);

                    //add scheduled surgery to working block
                    bestBlock.AllocatedSurgeries.Add(currentAppointment.ScheduledSurgery);

                    //update best working block
                    workingBlocks[bestBlockIndex] = bestBlock;
                }

                
            }
            return workingBlocks;
        }

        private TimeSpan GetFinalSurgeryDuration(Appointment appointment)
        {
            TimeSpan intitialTimeSpan = TimeSpan.Zero;

            if (appointment.SurgeonsPredictedDuration != null)
            {
                intitialTimeSpan = (TimeSpan)appointment.SurgeonsPredictedDuration;

            }
            else if (appointment.SystemPredictedDuration != null)
            {
                intitialTimeSpan = (TimeSpan)appointment.SystemPredictedDuration;
            }
            if(intitialTimeSpan == TimeSpan.Zero)
            {
                return intitialTimeSpan;
            }

            return intitialTimeSpan.Add(prepTime).Add(cleanTime);
        }

        public Task<List<Appointment>> PrioritizeAppointments(List<Appointment> appointments)
        {
            throw new NotImplementedException();
        }


        //public async Task CreateSchedule(TheatreType theatreType)
        //{

        //    //get list of theatres of theatreType
        //    List<Theatre> theatres = await theatreRepository.GetTheatres(TheatreQueryLogic.ByTheatreType(theatreType));

        //    //filter appointments that can be done in the theatertype
        //    List<Appointment> appointments = await appointmentRepository.GetAppointments(AppointmentQueryLogic.ByTheatreType(theatreType));

        //    //get incomplete appointments
        //    appointments = appointments.Where(a => a.Status != Status.Completed).ToList();

        //    //get list of surgeons allocated to the above appointments
        //    List<Surgeon> surgeons = appointments.Select(a => a.Surgeon).Distinct().ToList();

        //    //sort surgeons in descending order of total time of surgeries

        //    //prioritize appointments for the following week
        //    appointments = await PrioritizeAppointments(appointments);

        //    //calculate time blocks
        //    List<WorkingBlock> workingBlocks = CalculateTimeBlocks(surgeons, theatres);

        //    //allocate time for surgeries within the time blocks
        //    workingBlocks = await AllocateSurgeriesToBlocks(workingBlocks, appointments);

        //    //add blocks to the database
        //    await workingBlockRepository.AddBlocks(workingBlocks);

        //}


        //private List<WorkingBlock> CreateWorkingBlocks(Surgeon surgeon)
        //{
        //    List<WorkingBlock> workingBlocks = new List<WorkingBlock>();
        //    foreach(var s in surgeon.WorkingHours)
        //    {
        //        WorkingBlock workingBlock = new WorkingBlock() { Surgeon = surgeon, Theatre = s.Theatre };
        //        TimeRange timeRange = new TimeRange() { Start = s.Start, End = s.End };
        //        workingBlock.SetTimeRange(timeRange);
        //        workingBlocks.Add(workingBlock);
        //    }

        //    return workingBlocks;
        //}

        //public async Task<List<Appointment>> PrioritizeAppointments(List<Appointment> appointments)
        //{
        //    //foreach (int level in Enum.GetValues(typeof(PriorityLevel)))
        //    //{
        //    //    appointments = await appointments.GetAppointments(
        //    //        AppointmentQueryLogic.ByPriorityLevel((PriorityLevel)level));

        //    //    List<TimeSpan> timeSpans = appointments.Select(a => a.SystemPredictedDuration).ToList();
        //    //    float avgTime = CalculateAverage(timeSpans);

        //    //    foreach(Appointment appointment in appointments)
        //    //    {
        //    //        appointment.Priority = (float)appointment.PriorityLevel + appointment.SystemPredictedDuration.Ticks/avgTime;
        //    //    }

        //    //    appointments = await appointmentRepository.UpdateAppointments(appointments);
        //    //}

        //    return appointments;
        //}



    }

}

