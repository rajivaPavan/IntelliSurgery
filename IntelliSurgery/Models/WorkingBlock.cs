
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
        public int SurgeonId { get; set; }
        public Surgeon Surgeon { get; set; }
        public int TheatreId { get; set; }
        public Theatre Theatre { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public List<ScheduledSurgery> AllocatedSurgeries { get; set; }

    }

    public class WorkBlockDTO
    {
        public int SurgeonId { get; set; }
        public int TheatreId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
