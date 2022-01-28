using Itenso.TimePeriod;

namespace IntelliSurgery.Models
{
    public class Event : TimeRange
    {
        public void SetTimeRange(TimeRange timeRange)
        {
            this.Start = timeRange.Start;
            this.End = timeRange.End;
            this.Duration = timeRange.Duration;
        }

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
    public class StaffWorkingPeriod : Event
    {
        public int Id { get; set; }
    }
    public class SurgeryEvent : Event
    {
        public int Id { get; set; }

        
    }
}
