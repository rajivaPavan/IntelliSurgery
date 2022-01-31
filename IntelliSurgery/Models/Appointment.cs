using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using System;

namespace IntelliSurgery.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int SurgeonId { get; set; }
        public Surgeon Surgeon { get; set; }
        public int SurgeryTypeId { get; set; }
        public SurgeryType SurgeryType { get; set; }
        public int TheatreTypeId { get; set; }
        public TheatreType TheatreType{ get; set; }
        public int? TheatreId { get; set; }
        public Theatre Theatre { get; set; }
        public PriorityLevel PriorityLevel { get; set; }
        public float? Priority { get; set; }
        public AnesthesiaType AnesthesiaType { get; set; }
        public TimeSpan PredictedTimeDuration { get; set; }
        public Status Status { get; set; }
        public DateTime DateAdded { get; set; }
        public int? ScheduledSurgeryId { get; set; }
        public ScheduledSurgery ScheduledSurgery { get; set; }
    }
}
