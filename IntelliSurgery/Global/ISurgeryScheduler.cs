using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Global
{
    public interface ISurgeryScheduler
    {
        Task CreateSchedule(Surgeon surgeon);
        Task<List<WorkingBlock>> AllocateSurgeriesToBlocks(List<WorkingBlock> workingBlocks,
            List<Appointment> appointments);
        List<Appointment> PrioritizeAppointments(List<Appointment> appointments);
        TimeSpan GetFinalSurgeryDuration(Appointment appointment);
        Task<List<WorkingBlock>> SortSurgeriesWithinWorkingBlocks(List<WorkingBlock> workingBlocks);
    }
}
