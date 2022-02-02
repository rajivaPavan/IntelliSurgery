using IntelliSurgery.DTOs;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Theatre
    {
        public int Id { get; set; } 
        public TheatreType TheatreType { get; set; }
        public string Name { get; set; }

        public TheatreDTO getDTO()
        {
            return new TheatreDTO()
            {
                Id = this.Id,
                Name = this.Name,
                TheatreType = this.TheatreType
            };
        }
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

