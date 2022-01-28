using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations.WorkingBlocks
{
    public class WorkingBLockRepository : IWorkingBlockRepository
    {
        private readonly AppDbContext context;

        public WorkingBLockRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<WorkingBlock> AddWorkingBlock(WorkingBlock workingBlock)
        {
            await context.WorkingBlocks.AddAsync(workingBlock);
            await context.SaveChangesAsync();
            return workingBlock;
        }

        public async Task<List<WorkingBlock>> GetWorkBlocks()
        {
            return await context.WorkingBlocks.ToListAsync();
        }
    }
}
