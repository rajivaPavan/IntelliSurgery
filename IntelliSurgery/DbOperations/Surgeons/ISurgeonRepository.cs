using IntelliSurgery.Models;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeonRepository
    {
        Task<Surgeon> GetSurgeonById(int id);
    }
}
