
using IntelliSurgery.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class WorkingBlock : TimeRange
    {
        public int Id { get; set; }
        public TheaterAvailablePeriod TheaterAvailablePeriod { get; set; }
        public StaffWorkingPeriod SurgeonWorkingPeriod { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public List<ScheduledSurgery> AllocatedSurgeries { get; set; }

    }
}
