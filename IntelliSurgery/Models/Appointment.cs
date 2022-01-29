using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using System;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Surgeon Surgeon { get; set; }
        public SurgeryType SurgeryType { get; set; }
        public TheatreType TheatreType{ get; set; }
        public PriorityLevel PriorityLevel { get; set; }
        public float? Priority { get; set; }
        public AnesthesiaType AnesthesiaType { get; set; }
        public TimeSpan PredictedTimeDuration { get; set; }
        public Status Status { get; set; }
        public DateTime DateAdded { get; set; }
        public ScheduledSurgery ScheduledSurgery { get; set; }
    }
}
