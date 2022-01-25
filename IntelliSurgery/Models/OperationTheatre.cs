using Itenso.TimePeriod;
using System.Collections.Generic;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Models
{
    public class OperationTheatre
    {
        public int Id { get; set; }
        public int TheatreNumber { get; set; }
        public TheatreType TheatreType{ get; set; }
        public List<TheaterAvailablePeriod> TheaterAvailablePeriods { get; set; }
        public List<ScheduledSurgery> Surgeries { get; set; }
    }
    public class TheaterAvailablePeriod : TimeRange
    {
        public int Id { get; set; }
    }
}

