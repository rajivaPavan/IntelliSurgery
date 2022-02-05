using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public interface IPatientRepository
    {
        Task<Patient> CreatePatient(Patient patient);
        Task<Patient> GetPatientById(int patientId);
        Task<Patient> UpdatePatient(Patient update);
        Task DeletePatientAsync(Patient patientToBeDeleted);
        Task<Disease> GetDiseaseByEnumValue(DiseaseEnum d);
    }
}
