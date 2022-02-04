namespace IntelliSurgery.Enums
{
    public enum PriorityLevel

    {
        Low,
        Medium,
        High
    }

    public enum AnesthesiaType
    {
        None,
        General
    }

    

    public enum Status
    {
        Pending, //status after adding the appointment
        Scheduled, //status after scheduling a pending appointment
        Confirmed, //status after confirming date and time with patient
        Cancelled, //status if appointment is cancelled
        Completed, // status when appointment is completed
        Ongoing, //status when surgery is ongoing
        Interrupted //status when surgery is interrupted

    }
}
