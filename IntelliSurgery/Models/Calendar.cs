using System;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }

    public class Calendar
    {
        public int Id { get; set; }
        public List<Event> Events{ get; set; }
    }
}
