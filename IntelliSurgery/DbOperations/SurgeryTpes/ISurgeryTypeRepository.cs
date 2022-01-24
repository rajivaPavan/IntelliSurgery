using IntelliSurgery.Models;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeryTypeRepository
    {
        Task<SurgeryType> CreateSurgery(SurgeryType surgeryType);
        Task<SurgeryType> GetSurgeryTypeById(int id);
    }
}
