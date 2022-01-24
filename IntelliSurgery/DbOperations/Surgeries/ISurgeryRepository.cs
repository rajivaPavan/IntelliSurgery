using IntelliSurgery.Models;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeryRepository
    {
        Task<ScheduledSurgery> CreateSurgery(ScheduledSurgery surgery);
    }
}
