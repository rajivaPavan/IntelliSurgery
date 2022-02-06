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
using IntelliSurgery.Logic;

namespace IntelliSurgery.Global
{
    public class SurgeryScheduler : ISurgeryScheduler
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IWorkingBlockRepository workingBlockRepository;
        private readonly IWorkingBlockLogic workBlockLogic;
        private readonly IAppointmentLogic appointmentLogic;
        private readonly TimeSpan prepTime = new(0,5,0);
        private readonly TimeSpan cleanTime = new(0, 5, 0);
        private readonly double waitDays = 4;
        private readonly double schedulingDays = 3;

        public SurgeryScheduler(IAppointmentRepository appointmentRepository, IWorkingBlockRepository workingBlockRepository,
            IWorkingBlockLogic workBlockLogic, IAppointmentLogic appointmentLogic)
        {
            this.appointmentRepository = appointmentRepository;
            this.workingBlockRepository = workingBlockRepository;
            this.workBlockLogic = workBlockLogic;
            this.appointmentLogic = appointmentLogic;
        }

        public async Task CreateSchedule(Surgeon surgeon)
        {
            DateTime todayDate = DateTime.Now.Date;
            DateTime lowerBound = todayDate.AddDays(waitDays);
            DateTime upperBound = lowerBound.AddDays(schedulingDays);
            //get work blocks of the surgeon that start after the current x days from today and within 3 days after the lower bound
            List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(w => w.SurgeonId == surgeon.Id
                                                                                               && w.Start.Date >= lowerBound
                                                                                               && w.End.Date <= upperBound);

            //get postponed then pending, appointments of the surgeon and unconfirmed appointments in blocks
            Status[] statuses = new Status[] { Status.Postponed, Status.Scheduled, Status.Pending };
            foreach(Status status in statuses)
            {

                List<Appointment> appointments = await appointmentRepository.GetAppointments(a => a.SurgeonId == surgeon.Id
                                                                                  && a.Status == status);

                if(status == Status.Scheduled)
                {
                    appointments.ForEach(async appointment => {
                        await workBlockLogic.RestoreWorkBlockTime(appointment);
                        await appointmentLogic.DeleteScheduledSurgeryAsync(appointment);
                        //reset the status to pending
                        appointment.Status = Status.Pending;
                    });
                    appointments = await appointmentRepository.UpdateAppointments(appointments);
                }

                //allocate time for surgeries within the time blocks
                workingBlocks = await AllocateSurgeriesToBlocks(workingBlocks, appointments);

                //update blocks in the database
                await workingBlockRepository.UpdateWorkingBlocks(workingBlocks);
            }
        }

        private float CalculateAverage(IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            return (float)averageTicks;
        }

        public async Task<List<WorkingBlock>> AllocateSurgeriesToBlocks(List<WorkingBlock> workingBlocks, 
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
            TheatreType currentAppointmentTheatreType;

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
                    currentAppointmentTheatreType = currentAppointment.TheatreType;
                    //current block theatre type does not match appointment theatre type skip the current block,
                    if (currentBlock.Theatre.TheatreType != currentAppointmentTheatreType)
                    {
                        continue;
                    }

                    //block duration is the remaining time of the current block
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

                    //set appointment surgery duration with preparation and cleanging time
                    TimeRange surgeryTimeRange = new TimeRange()
                    {
                        Start = bestBlock.End.Subtract(bestBlock.RemainingTime),
                        Duration = finalSurgeryDuration
                    };

                    //reduce remaining time in block
                    bestBlock.RemainingTime = bestBlock.RemainingTime.Subtract(finalSurgeryDuration);

                    //assign the appointment to the workblock
                    currentAppointment.WorkingBlock = bestBlock;
                    currentAppointment.WorkingBlockId = bestBlock.Id;

                    //add scheduled surgery to database and add to current appointment
                    SurgeryEvent surgeryEvent = new SurgeryEvent();
                    surgeryEvent.SetTimeRange(surgeryTimeRange);
                    ScheduledSurgery scheduledSurgery = new ScheduledSurgery(surgeryEvent);
                    currentAppointment.ScheduledSurgery = scheduledSurgery;
                    currentAppointment.ScheduledSurgeryId = scheduledSurgery.Id;

                    //set theatre for the appointment
                    currentAppointment.Theatre = bestBlock.Theatre;
                    currentAppointment.TheatreId = bestBlock.TheatreId;

                    //update appointmentRepo
                    currentAppointment = await appointmentRepository.UpdateAppointment(currentAppointment);

                    //add scheduled surgery to working block
                    bestBlock.AllocatedSurgeries.Add(currentAppointment);

                    //update best working block
                    workingBlocks[bestBlockIndex] = bestBlock;
                }
            }
            return workingBlocks;
        }

        public TimeSpan GetFinalSurgeryDuration(Appointment appointment)
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

        public List<Appointment> PrioritizeAppointments(List<Appointment> appointments)
        {
            //sort the appointments in the order of priority, high first
            appointments = appointments.OrderByDescending(a => a.PriorityLevel).ToList();
            return appointments;
        }

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

