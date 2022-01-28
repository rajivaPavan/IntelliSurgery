using Itenso.TimePeriod;

namespace IntelliSurgery.Models
{
    public class Surgery
    {
        public int Id { get; set; }
    }
    public class UnScheduledSurgery : Surgery
    {

    }

    public class ScheduledSurgery : Surgery
    {
        public SurgeryEvent SurgeryEvent { get; set; }
    }

    public class SurgeryEvent: TimeRange
    {
        public int Id { get; set; }

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


}
