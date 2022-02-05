using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface ISpecialityRepository
    {
        Task<List<Speciality>> AddSpecialities(List<Speciality> specialities);
        Task<Speciality> GetSpecialityById(int specialityId);
        Task<List<Speciality>> GetAllSpecialities();
        Task<List<Speciality>> DeleteSpecialities(List<Speciality> specialities);
    }

}