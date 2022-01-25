using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.Global
{
    public interface ISurgeryScheduler
    {
        Task CreateSchedule();
        Task<List<Appointment>> PrioritizeAppointments();
    }
}
