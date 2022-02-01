
using IntelliSurgery.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelliSurgery.Models
{
    public class WorkingBlock : TimeRange
    {
        public int Id { get; set; }
        public Theatre Theatre { get; set; }

        //start end and duration is calculated from the overlap of two below periods
        //public TheaterAvailablePeriod TheaterAvailablePeriod { get; set; }

        [NotMapped]
        public StaffWorkingPeriod SurgeonWorkingPeriod { get; set; }

        public TimeSpan RemainingTime { get; set; }
        public List<ScheduledSurgery> AllocatedSurgeries { get; set; }

    }
}
