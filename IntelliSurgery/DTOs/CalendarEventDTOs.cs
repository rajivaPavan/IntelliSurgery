using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{
    public class FullCalendarEvent
    {
        public FullCalendarEvent()
        {

        }

        public FullCalendarEvent(WorkingBlock workingBlock)
        {
            Id = workingBlock.Id.ToString();
            Title = workingBlock.Surgeon.Name +" : "+workingBlock.Theatre.Name;
            Start = workingBlock.Start;
            End = workingBlock.End;
            Display = "auto";
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        public string Display { get; set; }

        
    }
    public class AppointmentCalendarEvent : FullCalendarEvent
    {
        public AppointmentExtendedProp ExtendedProps { get; set; }

        public AppointmentCalendarEvent(Appointment appointment)
        {
            Title = appointment.Patient.Name + " : " + appointment.SurgeryType.Name;
            if(appointment.ScheduledSurgery != null)
            {
                Start = appointment.ScheduledSurgery.SurgeryEvent.Start;
                End = appointment.ScheduledSurgery.SurgeryEvent.End;
            }
            ExtendedProps = new AppointmentExtendedProp(appointment);
            Color = GetPriorityColor(appointment.PriorityLevel);
            Display = "auto";
        }

        public AppointmentCalendarEvent(WorkingBlock workingBlock) : base(workingBlock)
        {
            Display = "background";
            Color = "#5bd2e5"; //lightblue
        }

        private static string GetPriorityColor(PriorityLevel priorityLevel)
        {
            string highColor = "#ff8f8f"; //lightred
            string lowColor = "#55c860"; //lightyellow
            string mediumColor = "#ffe18f"; //lightgreen
            string color;
            if (priorityLevel == PriorityLevel.Low)
            {
                color = lowColor;
            }
            else if (priorityLevel == PriorityLevel.Medium)
            {
                color = mediumColor;
            }
            else
            {
                color = highColor;
            }
            return color;
        }
    }

    public class SurgeonCalendarEvent : FullCalendarEvent
    {
        public SurgeonCalendarEvent(WorkingBlock workingBlock) : base(workingBlock)
        {
            ExtendedProps = workingBlock;
        }

        public WorkingBlock ExtendedProps { get; set; }
    }

    public class SurgeonCalendarDTO
    {
        public Surgeon Surgeon { get; set; }
        public Theatre Theatre { get; set; }
        public List<SurgeonCalendarEvent> Events { get; set; }
    }
}
