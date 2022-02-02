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
    public class WorkingBLockRepository : IWorkingBlockRepository
    {
        private readonly AppDbContext context;
        private readonly IIncludableQueryable<WorkingBlock, Theatre> readWorkingBlocks;

        public WorkingBLockRepository(AppDbContext context)
        {
            this.context = context;
            this.readWorkingBlocks = context.WorkingBlocks.Include(w => w.Surgeon).Include(w => w.Theatre);
        }

        public async Task<WorkingBlock> AddWorkingBlock(WorkingBlock workingBlock)
        {
            await context.WorkingBlocks.AddAsync(workingBlock);
            await context.SaveChangesAsync();
            return workingBlock;
        }

        public async Task<List<WorkingBlock>> AddBlocks(List<WorkingBlock> blocks)
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
    }
}
