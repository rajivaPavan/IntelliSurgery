using IntelliSurgery.DTOs;
using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISurgeryTypeRepository
    {
        Task<SurgeryType> AddSurgeryType(SurgeryType surgeryType);
        Task<SurgeryType> GetSurgeryTypeById(int id);
        Task<List<SurgeryType>> GetAllSurgeryTypes();
        Task<List<SurgeryType>> AddSurgeryTypes(List<SurgeryType> surgeryTypes);
        Task<List<SurgeryType>> UpdateSurgeryTypes(List<SurgeryType> surgeryTypes);
    }
}
