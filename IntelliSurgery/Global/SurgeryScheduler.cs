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

        public static double WaitDays { get; set; } = 1; //4
        public static double SchedulingDays { get; set; } = 3;



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
            DateTime lowerBound = todayDate.AddDays(WaitDays);
            DateTime upperBound = lowerBound.AddDays(SchedulingDays-1);
            //get work blocks of the surgeon that start after WaitDays from today and within ScheduleingDays after the lower bound
            List<WorkingBlock> workingBlocks = await workingBlockRepository.GetWorkBlocks(w => w.SurgeonId == surgeon.Id
                                                                                               && w.Start.Date >= lowerBound
                                                                                               && w.End.Date <= upperBound);
            //sort working blocks in chronological order
            workingBlocks = workingBlocks.OrderBy(w => w.Start).ToList();
            int numOfBlocks = workingBlocks.Count;


            List<Appointment> unconfirmedAppointments = await appointmentRepository.GetAppointments(a => a.SurgeonId == surgeon.Id
                                                                                  && a.Status == Status.Scheduled);
            foreach (Appointment appointment in unconfirmedAppointments)
            {
                workBlockLogic.RestoreWorkBlockTime(appointment).Wait();
                await appointmentLogic.DeleteScheduledSurgeryAsync(appointment);
                //reset the status to pending
                appointment.Status = Status.Pending;
            }

            await appointmentRepository.UpdateAppointments(unconfirmedAppointments);

            //get postponed then pending, appointments of the surgeon 
            Status[] statuses = new Status[] { Status.Postponed, Status.Pending };
            foreach(Status status in statuses)
            {

                List<Appointment> appointments = await appointmentRepository.GetAppointments(a => a.SurgeonId == surgeon.Id
                                                                                  && a.Status == status);
                int numOfAppointments = appointments.Count;
                if (numOfAppointments != 0)
                {
                    appointments = PrioritizeAppointments(appointments);

                    //allocate time for surgeries within the time blocks
                    workingBlocks = AllocateSurgeriesToBlocks(workingBlocks,numOfBlocks, appointments, numOfAppointments);

                    //sort the surgeries among the above working blocks
                    workingBlocks = SortSurgeriesAmongBlocks(workingBlocks, numOfBlocks);
                    
                    //sort the surgeries within each workingblock
                    workingBlocks = await SortSurgeriesWithinWorkingBlocks(workingBlocks);

                    //update blocks in the database
                    await workingBlockRepository.UpdateWorkingBlocks(workingBlocks);
                }
                
            }
        }
        public List<WorkingBlock> AllocateSurgeriesToBlocks(List<WorkingBlock> workingBlocks, int numOfBlocks,
           List<Appointment> appointments, int numOfAppointments)
        {
            ///////best fit algorithm in memory management//////

            //initially every appointment is unscheduled

            TimeSpan finalSurgeryDuration;
            Appointment currentAppointment;

            for (int i = 0; i < numOfAppointments; i++)
            {

                currentAppointment = appointments[i];

                finalSurgeryDuration = GetFinalSurgeryDuration(currentAppointment);
                if (finalSurgeryDuration == TimeSpan.Zero)
                {
                    //if neither the system nor surgeon has suggested a time duration, skip the appointment
                    continue;
                }

                //run the best fit algorithm
                int bestBlockIndex = BestFit(workingBlocks, numOfBlocks, currentAppointment, finalSurgeryDuration);

                //if a block was found for the current appointment
                if (bestBlockIndex != -1)
                {
                    WorkingBlock bestBlock = workingBlocks[bestBlockIndex];

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
                    //currentAppointment.ScheduledSurgeryId = scheduledSurgery.Id;

                    //set theatre for the appointment
                    currentAppointment.Theatre = bestBlock.Theatre;
                    currentAppointment.TheatreId = bestBlock.TheatreId;

                    //update appointmentRepo
                    //currentAppointment = await appointmentRepository.UpdateAppointment(currentAppointment);

                    //add scheduled surgery to working block
                    bestBlock.AllocatedSurgeries.Add(currentAppointment);
                    appointments[i] = currentAppointment;

                    //update best working block
                    workingBlocks[bestBlockIndex] = bestBlock;
                }
            }
            return workingBlocks;
        }

        private int BestFit(List<WorkingBlock> workingBlocks, int numOfBlocks, 
                Appointment currentAppointment, TimeSpan finalSurgeryDuration)
        {
            int bestBlockIndex = -1;
            WorkingBlock bestBlock = null;
            for (int j = 0; j < numOfBlocks; j++)
            {
                WorkingBlock currentBlock = workingBlocks[j];
                TheatreType currentAppointmentTheatreType = currentAppointment.TheatreType;
                //current block theatre type does not match appointment theatre type skip the current block,
                if (currentBlock.Theatre.TheatreType != currentAppointmentTheatreType)
                {
                    continue;
                }

                //block duration is the remaining time of the current block
                TimeSpan blockDuration = currentBlock.RemainingTime;

                //find the best block index for the current appointment
                if (blockDuration >= finalSurgeryDuration)
                {
                    if (bestBlock == null || blockDuration < bestBlock.RemainingTime)
                    {
                        bestBlockIndex = j;
                        bestBlock = workingBlocks[bestBlockIndex];
                    }
                }
            }
            return bestBlockIndex;
        }
        private List<WorkingBlock> SortSurgeriesAmongBlocks(List<WorkingBlock> workingBlocks, int numOfBlocks)
        {
            for (int currentBlockIndex = 0; currentBlockIndex < numOfBlocks; currentBlockIndex++)
            {
                WorkingBlock currentBlock = workingBlocks[currentBlockIndex];
                List<Appointment> allocatedSurgeries = currentBlock.AllocatedSurgeries;
                int appointmentCount = allocatedSurgeries.Count;
                TimeSpan blockRemainingTime = currentBlock.RemainingTime;

                TimeSpan swapMaxTime = blockRemainingTime;  
                workingBlocks = TryFill(workingBlocks, currentBlockIndex, numOfBlocks, swapMaxTime);

                for (int currentAppointmentIndex = 0; currentAppointmentIndex < appointmentCount; currentAppointmentIndex++)
                {
                    Appointment appointment = allocatedSurgeries[currentAppointmentIndex];
                    //do the check if priority level is not high
                    if (appointment.PriorityLevel != PriorityLevel.High)
                    {
                        TimeSpan surgeryDuration = appointment.ScheduledSurgery.SurgeryEvent.Duration;
                        swapMaxTime = surgeryDuration.Add(blockRemainingTime);

                        workingBlocks = TrySwap(workingBlocks, numOfBlocks, currentBlockIndex, currentAppointmentIndex, surgeryDuration, swapMaxTime);

                    }
                }

                //do a refill
                currentBlock = workingBlocks[currentBlockIndex];
                swapMaxTime = currentBlock.RemainingTime;
                workingBlocks = TryFill(workingBlocks, currentBlockIndex, numOfBlocks, swapMaxTime); 

            }

            return workingBlocks;
        }

        private List<WorkingBlock> TryFill(List<WorkingBlock> workingBlocks, int currentBlockIndex, int numOfBlocks, TimeSpan swapMaxTime)
        {
            TimeSpan minAppointmentDuration = TimeSpan.MaxValue;
            WorkingBlock currentBlock = workingBlocks[currentBlockIndex];
            while (minAppointmentDuration > currentBlock.RemainingTime)
            {
                TimeSpan minDifference = TimeSpan.MaxValue;

                int bestOtherBlockIndex = -1;
                Appointment bestOtherAppointment = null;

                
                //go throught the next blocks to find a suitable appointment to swap
                for (int otherBlockIndex = currentBlockIndex + 1; otherBlockIndex < numOfBlocks; otherBlockIndex++)
                {
                    WorkingBlock otherBlock = workingBlocks[otherBlockIndex];
                    List<Appointment> otherAllocatedSurgeries = otherBlock.AllocatedSurgeries;
                    int otherAppointmentsCount = otherAllocatedSurgeries.Count;
                    TimeSpan otherRemainingTime = otherBlock.RemainingTime;

                    //go throught appointments in the blocks until a suitable one to swap is found
                    for (int otherAppointmentIndex = 0; otherAppointmentIndex < otherAppointmentsCount; otherAppointmentIndex++)
                    {
                        Appointment otherAppointment = otherAllocatedSurgeries[otherAppointmentIndex];
                        TimeSpan otherSurgeryDuration = otherAppointment.ScheduledSurgery.SurgeryEvent.Duration;
                        TimeSpan otherSwapMaxTime = otherSurgeryDuration.Add(otherRemainingTime);

                        TimeSpan surgeryDuration = TimeSpan.Zero;

                    //is swappable
                        if (workBlockLogic.AreAppointmentsSwappable(surgeryDuration, swapMaxTime, otherSurgeryDuration, otherSwapMaxTime))
                        {
                            //correct the remaining times in the two blocks
                            TimeSpan difference = otherSurgeryDuration.Subtract(surgeryDuration);

                            if (difference < minDifference)
                            {
                                minDifference = difference;
                                bestOtherBlockIndex = otherBlockIndex;
                                bestOtherAppointment = otherAppointment;
                            }
                        }
                        if (otherSurgeryDuration < minAppointmentDuration)
                        {
                            minAppointmentDuration = otherSurgeryDuration;
                        }
                    }
                }
                //end looping through the next blocks

                if (bestOtherAppointment != null && bestOtherBlockIndex != -1)
                {
                    WorkingBlock bestOtherBlock = workingBlocks[bestOtherBlockIndex];

                    bestOtherBlock.RemainingTime = bestOtherBlock.RemainingTime.Add(minDifference);
                    currentBlock.RemainingTime = currentBlock.RemainingTime.Subtract(minDifference);

                    currentBlock.AllocatedSurgeries.Add(bestOtherAppointment);
                    bestOtherBlock.AllocatedSurgeries.Remove(bestOtherAppointment);

                    workingBlocks[currentBlockIndex] = currentBlock;
                    workingBlocks[bestOtherBlockIndex] = bestOtherBlock;

                    swapMaxTime = currentBlock.RemainingTime;
                }
                else
                {
                    break;
                }

            }

            return workingBlocks;
        }

        //work in progress
        private List<WorkingBlock> TrySwap2(List<WorkingBlock> workingBlocks, int numOfBlocks, int currentBlockIndex, 
            int currentAppointmentIndex, TimeSpan surgeryDuration, TimeSpan swapMaxTime)
        {
            WorkingBlock currentBlock = workingBlocks[currentBlockIndex];
            List<Appointment> allocatedSurgeries = currentBlock.AllocatedSurgeries;
            Appointment appointment = allocatedSurgeries[currentAppointmentIndex];

            TimeSpan minDifference = TimeSpan.MaxValue;
            int bestOtherBlockIndex = -1;
            int bestOtherAppointmentIndex = -1;
            Appointment bestOtherAppointment = null;
            List<Appointment> bestOtherAllocatedSurgeries = null;
            int swapCase = -1;
            //go throught the next blocks to find a suitable appointment to swap
            for (int otherBlockIndex = currentBlockIndex + 1; otherBlockIndex < numOfBlocks; otherBlockIndex++)
            {
                WorkingBlock otherBlock = workingBlocks[otherBlockIndex];
                List<Appointment> otherAllocatedSurgeries = otherBlock.AllocatedSurgeries;
                int otherAppointmentsCount = otherAllocatedSurgeries.Count;
                TimeSpan otherRemainingTime = otherBlock.RemainingTime;

                //go throught appointments in the blocks until a suitable one to swap is found
                for (int otherAppointmentIndex = 0; otherAppointmentIndex < otherAppointmentsCount; otherAppointmentIndex++)
                {
                    Appointment otherAppointment = otherAllocatedSurgeries[otherAppointmentIndex];
                    if (otherAppointment.PriorityLevel > appointment.PriorityLevel)
                    {
                        TimeSpan otherSurgeryDuration = otherAppointment.ScheduledSurgery.SurgeryEvent.Duration;
                        TimeSpan otherSwapMaxTime = otherSurgeryDuration.Add(otherRemainingTime);

                        //is swappable
                        if (workBlockLogic.AreAppointmentsSwappable(surgeryDuration, swapMaxTime, otherSurgeryDuration, otherSwapMaxTime))
                        {
                            TimeSpan difference;
                            
                            if (surgeryDuration >= otherSurgeryDuration)
                            {
                                difference = surgeryDuration.Subtract(otherSurgeryDuration);
                            }
                            else
                            {
                                difference = otherSurgeryDuration.Subtract(surgeryDuration);
                            }

                            if(difference < minDifference)
                            {
                                minDifference = difference;
                                bestOtherAppointment = otherAllocatedSurgeries[otherAppointmentIndex];
                                bestOtherBlockIndex = otherBlockIndex;

                                bestOtherAllocatedSurgeries = otherAllocatedSurgeries;

                                if(surgeryDuration >= otherSurgeryDuration){swapCase = 1;}
                                else { swapCase = 2;}
                            }

                            currentBlock.AllocatedSurgeries = allocatedSurgeries;
                            otherBlock.AllocatedSurgeries = otherAllocatedSurgeries;

                            workingBlocks[currentBlockIndex] = currentBlock;
                            workingBlocks[otherBlockIndex] = otherBlock;

                            //current appointment swapped
                            //now back to the top for loop 
                            //isBreak = true;
                            //break;
                        }
                    }
                }
                //end looping through other appointments
                //if (isBreak) { break; }

            }
            //end looping through the next blocks

            if (bestOtherAppointmentIndex != -1 && bestOtherAllocatedSurgeries != null)
            {
                WorkingBlock bestOtherBlock = workingBlocks[bestOtherBlockIndex];

                //swap

                allocatedSurgeries[currentAppointmentIndex] = bestOtherAppointment;

                bestOtherAllocatedSurgeries[bestOtherAppointmentIndex] = appointment;

                if(swapCase == 1)
                {
                    bestOtherBlock.RemainingTime = bestOtherBlock.RemainingTime.Subtract(minDifference);
                    currentBlock.RemainingTime = currentBlock.RemainingTime.Add(minDifference);
                }
                else
                {
                    bestOtherBlock.RemainingTime = bestOtherBlock.RemainingTime.Add(minDifference);
                    currentBlock.RemainingTime = currentBlock.RemainingTime.Subtract(minDifference);
                }
                

                currentBlock.AllocatedSurgeries = allocatedSurgeries;
                bestOtherBlock.AllocatedSurgeries = bestOtherAllocatedSurgeries;

                workingBlocks[currentBlockIndex] = currentBlock;
                workingBlocks[bestOtherBlockIndex] = bestOtherBlock;
            }

            return workingBlocks;
        }

        private List<WorkingBlock> TrySwap(List<WorkingBlock> workingBlocks, int numOfBlocks, int currentBlockIndex,
    int currentAppointmentIndex, TimeSpan surgeryDuration, TimeSpan swapMaxTime)
        {
            WorkingBlock currentBlock = workingBlocks[currentBlockIndex];
            List<Appointment> allocatedSurgeries = currentBlock.AllocatedSurgeries;
            Appointment appointment = allocatedSurgeries[currentAppointmentIndex];

            bool isBreak = false;
            //go throught the next blocks to find a suitable appointment to swap
            for (int otherBlockIndex = currentBlockIndex + 1; otherBlockIndex < numOfBlocks; otherBlockIndex++)
            {
                WorkingBlock otherBlock = workingBlocks[otherBlockIndex];
                List<Appointment> otherAllocatedSurgeries = otherBlock.AllocatedSurgeries;
                int otherAppointmentsCount = otherAllocatedSurgeries.Count;
                TimeSpan otherRemainingTime = otherBlock.RemainingTime;

                //go throught appointments in the blocks until a suitable one to swap is found
                for (int otherAppointmentIndex = 0; otherAppointmentIndex < otherAppointmentsCount; otherAppointmentIndex++)
                {
                    Appointment otherAppointment = otherAllocatedSurgeries[otherAppointmentIndex];
                    if (otherAppointment.PriorityLevel > appointment.PriorityLevel)
                    {
                        TimeSpan otherSurgeryDuration = otherAppointment.ScheduledSurgery.SurgeryEvent.Duration;
                        TimeSpan otherSwapMaxTime = otherSurgeryDuration.Add(otherRemainingTime);

                        //is swappable
                        if (workBlockLogic.AreAppointmentsSwappable(surgeryDuration, swapMaxTime, otherSurgeryDuration, otherSwapMaxTime))
                        {
                            TimeSpan difference;
                            //swap

                            allocatedSurgeries[currentAppointmentIndex] = otherAppointment;
                            otherAllocatedSurgeries[otherAppointmentIndex] = appointment;

                            //correct the remaining times in the two blocks
                            if (surgeryDuration >= otherSurgeryDuration)
                            {

                                difference = surgeryDuration.Subtract(otherSurgeryDuration);
                                //subrtact the difference from the remaining time of other block
                                otherBlock.RemainingTime = otherBlock.RemainingTime.Subtract(difference);
                                //add the difference to the remaining time of the current block
                                currentBlock.RemainingTime = currentBlock.RemainingTime.Add(difference);
                            }
                            else
                            {
                                difference = otherSurgeryDuration.Subtract(surgeryDuration);
                                //do the opposite of that of in the if case
                                otherBlock.RemainingTime = otherBlock.RemainingTime.Add(difference);
                                currentBlock.RemainingTime = currentBlock.RemainingTime.Subtract(difference);
                            }

                            workingBlocks[currentBlockIndex].AllocatedSurgeries = allocatedSurgeries;
                            workingBlocks[otherBlockIndex].AllocatedSurgeries = otherAllocatedSurgeries;

                            //current appointment swapped
                            //now back to the top for loop 
                            isBreak = true;
                            break;
                        }
                    }
                }
                //end looping through other appointments
                if (isBreak) { break; }

            }
            //end looping through the next blocks

            return workingBlocks;
        }

        private TimeSpan CalculateAverage(IEnumerable<TimeSpan> timeSpans)
        {
            IEnumerable<long> ticksPerTimeSpan = timeSpans.Select(t => t.Ticks);
            double averageTicks = ticksPerTimeSpan.Average();
            return TimeSpan.FromTicks((long)averageTicks);
        }

        public async Task<List<WorkingBlock>> SortSurgeriesWithinWorkingBlocks(List<WorkingBlock> workingBlocks)
        {
            foreach(WorkingBlock workingBlock in workingBlocks)
            {
                List<Appointment> appointments = workingBlock.AllocatedSurgeries;

                DateTime blockStart = workingBlock.Start;
                DateTime blockEnd = workingBlock.End;
                TimeSpan remainingTime = workingBlock.RemainingTime;

                int appointmentCount = appointments.Count;
                int numberOfGaps = appointmentCount - 1;
                TimeSpan gapTime = TimeSpan.Zero;
                if (numberOfGaps > 0) { gapTime = remainingTime.Divide(numberOfGaps); }

                appointments = DistributeInPriorityOrder(appointments, appointmentCount,
                                    blockStart, gapTime);

                await appointmentRepository.UpdateAppointments(appointments);
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
            List<Appointment> prioritized = new List<Appointment>();
            //sort the appointments in the order of priority, high first
            appointments = appointments.OrderByDescending(a => a.PriorityLevel).ThenBy(a => a.DateAdded).ToList();
            
            return appointments;
        }

        private List<Appointment> DistributeInPriorityOrder(List<Appointment> appointments, int appointmentCount,
            DateTime blockStart, TimeSpan gapTime)
        {
            for (int i = 0; i < appointmentCount; i++)
            {
                Appointment currentAppointment = appointments[i];
                TimeSpan duration = currentAppointment.ScheduledSurgery.SurgeryEvent.Duration;

                TimeRange timeRange = new TimeRange(blockStart, duration);
                DateTime end = timeRange.End;
                blockStart = end.Add(gapTime);
                SurgeryEvent surgeryEvent = new SurgeryEvent();
                surgeryEvent.SetTimeRange(timeRange);
                currentAppointment.ScheduledSurgery.SurgeryEvent = surgeryEvent;

                appointments[i] = currentAppointment;
            }

            return appointments;
        }

        private List<Appointment> NormallyDistrubute(List<Appointment> appointments, int appointmentCount, 
            DateTime blockStart,DateTime blockEnd, TimeSpan gapTime)
        {
            appointments = appointments.OrderBy(a => a.ScheduledSurgery.SurgeryEvent.Duration).ToList();

            for (int i = 0; i < appointmentCount; i++)
            {
                Appointment currentAppointment = appointments[i];
                TimeSpan duration = currentAppointment.ScheduledSurgery.SurgeryEvent.Duration;

                if ((i + 1) % 2 != 0)
                {

                    TimeRange timeRange = new TimeRange(blockStart, duration);
                    DateTime end = timeRange.End;
                    blockStart = end.Add(gapTime);
                    SurgeryEvent surgeryEvent = new SurgeryEvent();
                    surgeryEvent.SetTimeRange(timeRange);
                    currentAppointment.ScheduledSurgery.SurgeryEvent = surgeryEvent;


                }
                else
                {
                    DateTime start = blockEnd.Subtract(duration);
                    blockEnd = start.Subtract(gapTime);
                    TimeRange timeRange = new TimeRange(start, duration);
                    SurgeryEvent surgeryEvent = new SurgeryEvent();
                    surgeryEvent.SetTimeRange(timeRange);
                    currentAppointment.ScheduledSurgery.SurgeryEvent = surgeryEvent;

                }

                appointments[i] = currentAppointment;
            }

            return appointments;
        }

    }
}