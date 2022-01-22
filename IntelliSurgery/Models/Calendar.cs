using System;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Event
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }

    public class Calendar
    {
        public int Id { get; set; }
        public List<Event> Events{ get; set; }
    }
}
