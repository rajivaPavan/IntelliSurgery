using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations.WorkingBlocks
{
    public class WorkingBlockRepository : IWorkingBlockRepository
    {
        private readonly AppDbContext context;
        private readonly IIncludableQueryable<WorkingBlock, Theatre> readWorkingBlocks;

        public WorkingBlockRepository(AppDbContext context)
        {
            this.context = context;
            this.readWorkingBlocks = context.WorkingBlocks.Include(w => w.AllocatedSurgeries).Include(w => w.Surgeon).Include(w => w.Theatre);
        }

        public async Task<WorkingBlock> AddWorkingBlock(WorkingBlock workingBlock)
        {
            await context.WorkingBlocks.AddAsync(workingBlock);
            await context.SaveChangesAsync();
            return workingBlock;
        }

        public async Task<List<WorkingBlock>> AddWorkingBlocks(List<WorkingBlock> blocks)
        {
            await context.WorkingBlocks.AddRangeAsync(blocks);
            await context.SaveChangesAsync();
            return blocks;
        }

        public async Task<List<WorkingBlock>> GetAllWorkBlocks()
        {
            return await readWorkingBlocks.ToListAsync();
        }

        public async Task<List<WorkingBlock>> GetWorkBlocks(Expression<Func<WorkingBlock, bool>> expression)
        {
            return await readWorkingBlocks.Where(expression).ToListAsync();
        }

        public async Task<WorkingBlock> GetWorkBlock(Expression<Func<WorkingBlock, bool>> expression)
        {
            return await readWorkingBlocks.FirstOrDefaultAsync(expression);
        }

        public async Task DeleteWorkBlock(WorkingBlock workingBlock)
        {
            context.WorkingBlocks.Remove(workingBlock);
            await context.SaveChangesAsync();
        }

        public async Task<List<WorkingBlock>> UpdateWorkingBlocks(List<WorkingBlock> workingBlocks)
        {
            context.WorkingBlocks.UpdateRange(workingBlocks);
            await context.SaveChangesAsync();
            return workingBlocks;
        }
    }
}
