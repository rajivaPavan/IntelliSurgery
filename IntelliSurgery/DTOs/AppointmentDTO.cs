namespace IntelliSurgery.DTOs
{
    public class AppointmentDTO
    {
        public int SurgeryType { get; set; }
        public int DoctorId { get; set; }
        public bool IsAnesthesiaRequired { get; set; }
        public int AnesthesiaType { get; set; }
    }
}
