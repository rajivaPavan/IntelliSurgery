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
        public ScheduledSurgery() { } //need empty constructor
        public ScheduledSurgery(SurgeryEvent surgeryEvent)
        {
            SurgeryEvent = surgeryEvent;
        }

        public SurgeryEvent SurgeryEvent { get; set; }
    }




}
