using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeryRepository
    {
        Task<ScheduledSurgery> AddSurgery(ScheduledSurgery surgery);
        Task<List<ScheduledSurgery>> GetAllSurgeries();
    }
}
