using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class SurgeryTypeRepository : ISurgeryTypeRepository
    {
        private readonly AppDbContext context;
        private readonly IIncludableQueryable<SurgeryType, List<Speciality>> readSurgeryTypes;

        public SurgeryTypeRepository(AppDbContext context)
        {
            this.context = context;
            this.readSurgeryTypes = context.SurgeryTypes.Include(s => s.SuitableTheatreTypes).
                Include(s => s.SuitableSpecialists);
        }

        public async Task<SurgeryType> AddSurgeryType(SurgeryType surgery)
        {
            await context.SurgeryTypes.AddAsync(surgery);
            await context.SaveChangesAsync();
            return surgery;
        }

        public async Task<SurgeryType> GetSurgeryTypeById(int id)
        {
            return await readSurgeryTypes.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<SurgeryType>> GetAllSurgeryTypes()
        {
            return await readSurgeryTypes.ToListAsync();
        }

        public async Task<List<SurgeryType>> AddSurgeryTypes(List<SurgeryType> surgeryTypes)
        {
            await context.SurgeryTypes.AddRangeAsync(surgeryTypes);
            await context.SaveChangesAsync();
            return surgeryTypes;
        }

        public async Task<List<SurgeryType>> UpdateSurgeryTypes(List<SurgeryType> surgeryTypes) 
        {
            context.SurgeryTypes.UpdateRange(surgeryTypes);
            await context.SaveChangesAsync();
            return surgeryTypes;
        }

        public Task<List<SurgeryType>> DeleteSurgeryTypes(List<SurgeryType> surgeryTypes)
        {
            throw new System.NotImplementedException();
        }
    }
}
