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

        public async Task<ScheduledSurgery> AddSurgery(ScheduledSurgery surgery)
        {
            await context.ScheduledSurgeries.AddAsync(surgery);
            await context.SaveChangesAsync();
            return surgery;
        }

        public async Task DeleteScheduleSurgery(ScheduledSurgery delScheduledSurgery)
        {
            context.ScheduledSurgeries.Remove(delScheduledSurgery);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSurgeryEvent(SurgeryEvent delSurgeryEvent)
        {
            context.SurgeryEvents.Remove(delSurgeryEvent);
            await context.SaveChangesAsync();
        }

        public async Task<List<ScheduledSurgery>> GetAllSurgeries()
        {
            return await context.ScheduledSurgeries.Include(s => s.SurgeryEvent).ToListAsync();
        }
    }
}
