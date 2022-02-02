using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{
    public class FullCalendarEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        public string Display { get; set; }

        
    }
    public class AppointmentCalendarEvent : FullCalendarEvent
    {
        public Appointment ExtendedProps { get; set; }
        public static string GetPriorityColor(PriorityLevel priorityLevel)
        {
            string highColor = "#ff8f8f";
            string lowColor = "#94ff8f";
            string mediumColor = "#ffe18f";
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
        public SurgeonCalendarEvent(WorkingBlock workingBlock)
        {
            Id = workingBlock.Id.ToString();
            Title = workingBlock.Theatre.Name;
            Start = workingBlock.Start;
            End = workingBlock.End;
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
