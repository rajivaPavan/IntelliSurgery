using IntelliSurgery.Models;
using Itenso.TimePeriod;
using System.Collections.Generic;

namespace IntelliSurgery.Logic
{
    public static class WorkingBlockLogic
    {
        public static bool CheckIfBlockOverlaps(TimeRange timeRange, List<WorkingBlock> checkBlocks)
        {
            bool isOverlaps = false;
            foreach (var block in checkBlocks)
            {
                if (timeRange.OverlapsWith(block.GetTimeRange()))
                {
                    isOverlaps = true;
                    break;
                }
            }
            return isOverlaps;
        }
    }
}
