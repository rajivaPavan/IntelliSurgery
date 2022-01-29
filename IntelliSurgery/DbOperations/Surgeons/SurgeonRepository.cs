using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class SurgeonRepository : ISurgeonRepository
    {
        private readonly AppDbContext context;
        private readonly IIncludableQueryable<Surgeon, Speciality> readSurgeons;

        public SurgeonRepository(AppDbContext context)
        {
            this.context = context;
            readSurgeons = context.Surgeons.Include(s => s.Speciality);
        }
        public async Task<Surgeon> AddSurgeon(Surgeon surgeon)
        {
            await context.Surgeons.AddAsync(surgeon);
            await context.SaveChangesAsync();
            return surgeon;
        }

        public async Task<List<Surgeon>> AddSurgeons(List<Surgeon> surgeons)
        {
            await context.Surgeons.AddRangeAsync(surgeons);
            await context.SaveChangesAsync();
            return surgeons;
        }

        public async Task<Surgeon> GetSurgeonById(int id)
        {
            return await readSurgeons.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Surgeon>> GetSurgeons()
        {
            return await readSurgeons.ToListAsync();
        }
    }
}
