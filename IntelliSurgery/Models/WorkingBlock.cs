
using IntelliSurgery.Models;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntelliSurgery.Models
{
    public class WorkingBlock : Event
    {
        public int Id { get; set; }
        public Surgeon Surgeon { get; set; }
        public Theatre Theatre { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public List<ScheduledSurgery> AllocatedSurgeries { get; set; }

    }
}
