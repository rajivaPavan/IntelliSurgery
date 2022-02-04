using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{
    public class AppointmentDTO
    {
        public int PatientId { get; set; }
        public int SurgeryType { get; set; }
        public int TheatreType { get; set; }    
        public int SurgeonId { get; set; }
        public bool IsAnesthesiaRequired { get; set; }
        public bool Complication { get; set; }
        public int AnesthesiaType { get; set; }
        public int PriorityLevel { get; set; }
    }

    public class DropDownListsDTO
    {
        public List<SurgeryType> SurgeryTypes{ get; set; }
        public List<SurgeonDTO> Surgeons { get; set; }
        public List<TheatreType> TheatreTypes{ get; set; }
        public List<AnesthesiaDTO> Anesthesias { get; set; }
        public List<Speciality> Specialities { get; set; }
    }

    public class AppointmentExtendedProp : Appointment
    {
        public new string PriorityLevel { get; set; }
        public new string ApproximateProcedureDate { get; set; }
        public new string ComplicationPossibility { get; set; }
        public new string AnesthesiaType { get; set; }
        public new string SystemPredictedDuration { get; set; }
        public new string SurgeonsPredictedDuration { get; set; }
        public new string Status { get; set; }
        public new string DateAdded { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }

        public AppointmentExtendedProp(Appointment appointment)
        {
            Id = appointment.Id;
            PatientId = appointment.PatientId;
            Patient = appointment.Patient;
            Surgeon = appointment.Surgeon;
            SurgeonId = appointment.SurgeonId;
            TheatreType = appointment.TheatreType;
            Theatre = appointment.Theatre;
            SurgeryType = appointment.SurgeryType;
            PriorityLevel = appointment.PriorityLevel.ToString();
            ApproximateProcedureDate = "";
            if(appointment.ApproximateProcedureDate != null)
            {
                DateTime approximateProcedureDate = (DateTime)appointment.ApproximateProcedureDate;
                ApproximateProcedureDate = approximateProcedureDate.ToString("yyyy/MM/dd");
            }
            AnesthesiaType = appointment.AnesthesiaType.ToString();
            SystemPredictedDuration = FormatTime(appointment.SystemPredictedDuration);
            SurgeonsPredictedDuration = "";
            if (appointment.SurgeonsPredictedDuration != null)
            {
                TimeSpan t = (TimeSpan)appointment.SurgeonsPredictedDuration;
                
                SurgeonsPredictedDuration = FormatTime(t);
            }
            Status = appointment.Status.ToString();
            DateAdded = appointment.DateAdded.ToString("yyyy/MM/dd");
            ComplicationPossibility = appointment.ComplicationPossibility ? "Yes" : "No";

            StartTime = appointment.ScheduledSurgery != null ? appointment.ScheduledSurgery.SurgeryEvent.Start.ToString("g") : string.Empty;
            EndTime = appointment.ScheduledSurgery != null ? appointment.ScheduledSurgery.SurgeryEvent.End.ToString("g") : string.Empty;
            Duration = appointment.ScheduledSurgery != null ? appointment.ScheduledSurgery.SurgeryEvent.DurationDescription : string.Empty;
            
        }

        private string FormatTime(TimeSpan t)
        {
            string td = "";
            td = t.Hours == 0 ? td : t.Hours.ToString() + " hours ";
            td = td + (t.Minutes == 0 ? "" : t.Minutes.ToString() + " minutes");
            return td;
        }
    }

}
