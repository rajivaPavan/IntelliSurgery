using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations
{
    public class SurgeryRepository : ISurgeryRepository
    {
        private readonly AppDbContext context;

        public SurgeryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ScheduledSurgery> CreateSurgery(ScheduledSurgery surgery)
        {
            await context.ScheduledSurgeries.AddAsync(surgery);
            await context.SaveChangesAsync();
            return surgery;
        }

        public async Task<List<ScheduledSurgery>> GetAllSurgeries()
        {
            return await context.ScheduledSurgeries.ToListAsync();
        }
    }
}
