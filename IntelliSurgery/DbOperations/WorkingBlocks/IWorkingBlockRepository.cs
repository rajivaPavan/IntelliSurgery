using IntelliSurgery.Global;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations.WorkingBlocks
{
    public interface IWorkingBlockRepository
    {
        Task<WorkingBlock> AddWorkingBlock(WorkingBlock workingBlock);
        Task<List<WorkingBlock>> AddWorkingBlocks(List<WorkingBlock> blocks);
        Task<List<WorkingBlock>> GetAllWorkBlocks();
        Task<List<WorkingBlock>> GetWorkBlocks(Expression<Func<WorkingBlock, bool>> expression);
        Task<WorkingBlock> GetWorkBlock(Expression<Func<WorkingBlock, bool>> expression);
        Task DeleteWorkBlock(WorkingBlock workingBlock);
        Task<List<WorkingBlock>> UpdateWorkingBlocks(List<WorkingBlock> workingBlocks);
    }
}