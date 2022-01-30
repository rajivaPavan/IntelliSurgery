using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class SpecialityRepository : ISpecialityRepository
    {
        private readonly AppDbContext context;

        public SpecialityRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Speciality>> AddSpecialities(List<Speciality> specialities)
        {
            await context.Specialities.AddRangeAsync(specialities);
            await context.SaveChangesAsync();
            return specialities;
        }

        public async Task<List<Speciality>> GetAllSpecialities()
        {
            return await context.Specialities.ToListAsync();
        }

        public async Task<Speciality> GetSpecialityById(int specialityId)
        {
            return await context.Specialities.FirstOrDefaultAsync( s => s.Id == specialityId);
        }
    }
}
