using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using System;

namespace IntelliSurgery.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Surgeon Surgeon { get; set; }
        public SurgeryType SurgeryType { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
        public AnesthesiaType AnesthesiaType { get; set; }
        public TimeSpan PredictedTimeDuration { get; set; }
        public Status AppointmentStatus { get; set; }
    }
}
