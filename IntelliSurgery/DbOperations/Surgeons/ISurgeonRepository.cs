using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeonRepository
    {
        Task<Surgeon> GetSurgeonById(int id);
        Task<List<Surgeon>> GetSurgeons();
        Task<Surgeon> AddSurgeon(Surgeon surgeon);
    }
}
