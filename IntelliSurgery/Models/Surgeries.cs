namespace IntelliSurgery.Models
{
    public class Surgery
    {
        public int Id { get; set; }
        public Appointment Appointment { get; set; }
    }
    public class UnScheduledSurgery : Surgery
    {

    }

    public class ScheduledSurgery : Surgery
    {
        public Event SurgeryEvent { get; set; }
    }


}
