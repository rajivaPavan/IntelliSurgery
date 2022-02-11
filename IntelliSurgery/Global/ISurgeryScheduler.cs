using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Global
{
    public interface ISurgeryScheduler
    {
        Task CreateSchedule(Surgeon surgeon);
        List<WorkingBlock> AllocateSurgeriesToBlocks(List<WorkingBlock> workingBlocks, int numOfBlocks,
            List<Appointment> appointments, int numOfAppointments);
        List<Appointment> PrioritizeAppointments(List<Appointment> appointments);
        TimeSpan GetFinalSurgeryDuration(Appointment appointment);
        Task<List<WorkingBlock>> SortSurgeriesWithinWorkingBlocks(List<WorkingBlock> workingBlocks);
    }
}
