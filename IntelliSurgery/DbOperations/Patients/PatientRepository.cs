using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext context;

        public PatientRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Patient> CreatePatient(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
            return patient;
        }

        public async Task DeletePatientAsync(Patient patientToBeDeleted)
        {            
            context.Patients.Remove(patientToBeDeleted);
            await context.SaveChangesAsync();
        }

        

        public async Task<Patient> GetPatientById(int patientId)
        {
            return await context.Patients.Include(p => p.Diseases).FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<Patient> UpdatePatient(Patient update)
        {
            context.Patients.Update(update);
            await context.SaveChangesAsync();
            return update;
        }



        public async Task<Disease> GetDiseaseByEnumValue(DiseaseEnum d)
        {
            return await context.Diseases.FirstOrDefaultAsync(dis => dis.DiseaseEnum == d);
        }
    }

}
