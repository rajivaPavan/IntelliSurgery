using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeryTypeRepository
    {
        Task<SurgeryType> CreateSurgery(SurgeryType surgeryType);
        Task<SurgeryType> GetSurgeryTypeById(int id);
        Task<List<SurgeryType>> GetSurgeryTypes();
    }
}
