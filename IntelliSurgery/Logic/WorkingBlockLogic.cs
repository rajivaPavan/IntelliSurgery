using IntelliSurgery.DbOperations.WorkingBlocks;
using IntelliSurgery.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Logic
{
    public class WorkingBlockLogic : IWorkingBlockLogic
    {
        private readonly IWorkingBlockRepository workingBlockRepository;

        public WorkingBlockLogic(IWorkingBlockRepository workingBlockRepository)
        {
            this.workingBlockRepository = workingBlockRepository;
        }

        public bool AreAppointmentsSwappable(TimeSpan surgeryDuration, TimeSpan swapMaxTime,
                        TimeSpan otherSurgeryDuration, TimeSpan otherSwapMaxTime)
        {
            return (surgeryDuration <= otherSwapMaxTime) && (otherSurgeryDuration <= swapMaxTime);
        }

        public bool CheckIfBlockOverlaps(TimeRange timeRange, List<WorkingBlock> checkBlocks)
        {
            bool isOverlaps = false;
            foreach (var block in checkBlocks)
            {
                if (timeRange.OverlapsWith(block.GetTimeRange()))
                {
                    isOverlaps = true;
                    break;
                }
            }
            return isOverlaps;
        }

        public bool IsBlockDeletable(WorkingBlock workingBlock)
        {
            return workingBlock.AllocatedSurgeries != null || workingBlock.AllocatedSurgeries.Count != 0;
        }

        public async Task RestoreWorkBlockTime(Appointment appointment)
        {
            SurgeryEvent delSurgeryEvent = appointment.ScheduledSurgery.SurgeryEvent;
            ScheduledSurgery delScheduledSurgery = appointment.ScheduledSurgery;

            //update working block time
            int workingBlockId = (int)appointment.WorkingBlockId;
            WorkingBlock workingBlock = await workingBlockRepository.GetWorkBlock(w => w.Id == workingBlockId);
            workingBlock.RemainingTime = workingBlock.RemainingTime.Add(delSurgeryEvent.Duration);
            await workingBlockRepository.UpdateWorkingBlock(workingBlock);
        }
    }

    public interface IWorkingBlockLogic
    {
        bool IsBlockDeletable(WorkingBlock workingBlock);
        bool CheckIfBlockOverlaps(TimeRange timeRange, List<WorkingBlock> checkBlocks);
        Task RestoreWorkBlockTime(Appointment appointment);
        bool AreAppointmentsSwappable(TimeSpan surgeryDuration, TimeSpan swapMaxTime, TimeSpan otherSurgeryDuration, TimeSpan otherSwapMaxTime);
    }
}
