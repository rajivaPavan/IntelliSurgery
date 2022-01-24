using IntelliSurgery.Models;
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
        
    }
}
