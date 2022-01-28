using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class SurgeonRepository : ISurgeonRepository
    {
        private readonly AppDbContext context;

        public SurgeonRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Surgeon> AddSurgeon(Surgeon surgeon)
        {
            await context.Surgeons.AddAsync(surgeon);
            await context.SaveChangesAsync();
            return surgeon;
        }
        public async Task<Surgeon> GetSurgeonById(int id)
        {
            return await context.Surgeons.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Surgeon>> GetSurgeons()
        {
            return await context.Surgeons.ToListAsync();
        }
    }
}
