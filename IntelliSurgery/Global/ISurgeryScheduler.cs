using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Global
{
    public interface ISurgeryScheduler
    {
        Task CreateSchedule(TheatreType theatreType);
        Task<List<Appointment>> PrioritizeAppointments(List<Appointment> appointments);
    }
}
