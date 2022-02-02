using IntelliSurgery.Global;
using IntelliSurgery.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliSurgery.DbOperations.WorkingBlocks
{
    public interface IWorkingBlockRepository
    {
        Task<WorkingBlock> AddWorkingBlock(WorkingBlock workingBlock);
        Task<List<WorkingBlock>> AddBlocks(List<WorkingBlock> blocks);
        Task<List<WorkingBlock>> GetAllWorkBlocks();
        Task<List<WorkingBlock>> GetWorkBlocks(Surgeon surgeon);
    }
}