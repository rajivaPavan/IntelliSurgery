using Itenso.TimePeriod;
using System.Collections.Generic;
using static IntelliSurgery.Enums.OperationTheatreEnums;

namespace IntelliSurgery.Models
{
    public class OperationTheatre
    {
        public int Id { get; set; }
        public int TheatreNumber { get; set; }
        public TheatreType TheatreType{ get; set; }
        public Calendar AvailableHours { get; set; }
    }

    //public class TheaterAvailableHours : Itenso.TimePeriod
    //{

    //}
}

