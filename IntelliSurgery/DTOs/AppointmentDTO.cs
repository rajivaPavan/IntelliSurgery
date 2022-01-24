namespace IntelliSurgery.DTOs
{
    public class AppointmentDTO
    {
        public int PatientId { get; set; }
        public int SurgeryType { get; set; }
        public int DoctorId { get; set; }
        public bool IsAnesthesiaRequired { get; set; }
        public string AnesthesiaType { get; set; }
        public string PriorityLevel { get; set; }
    }

}
