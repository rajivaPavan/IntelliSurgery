﻿using Itenso.TimePeriod;
using System.Collections.Generic;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Models
{
    public class Theatre
    {
        public int Id { get; set; } 
        public TheatreType TheatreType { get; set; }
        public string Name { get; set; }
        public List<TheaterAvailablePeriod> TheaterAvailablePeriods { get; set; }
        public List<Appointment> ScheduledAppointments { get; set; }
    }

    public class TheatreType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SurgeryType> SurgeryTypesConducted { get; set; }
    }
    public class TheaterAvailablePeriod : TimeRange
    {
        public int Id { get; set; }
        public TimeRange GetTimeRange()
        {
            return new TimeRange()
            {
                Start = this.Start,
                End = this.End,
                Duration = this.Duration
            };
        }
    }
}

