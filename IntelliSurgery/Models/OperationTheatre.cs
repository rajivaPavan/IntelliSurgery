using Itenso.TimePeriod;
using System.Collections.Generic;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Models
{
    public class Theatre
    {
        public int Id { get; set; } 
        public TheatreType TheatreType { get; set; }
        public int TheatreNumber { get; set; }
        public List<TheaterAvailablePeriod> TheaterAvailablePeriods { get; set; }
        public List<ScheduledSurgery> Surgeries { get; set; }
    }

    public class TheatreType
    {
        public int Id { get; set; }
        public OperationTheatreType OperationTheatreType{ get; set; }
       
    }
    public class TheaterAvailablePeriod : TimeRange
    {
        public int Id { get; set; }
        public TimeRange GetTimeRange()
        {
            return new TimeRange()
            {
                Start = this.Start,
                End = this.End,
                Duration = this.Duration
            };
        }
    }
}

