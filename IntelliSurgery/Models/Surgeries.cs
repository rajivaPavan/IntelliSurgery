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

    


}
