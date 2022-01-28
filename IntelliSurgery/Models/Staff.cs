using Itenso.TimePeriod;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Staff
    {
        public int Id { get; set; }
    }
    public class SurgeryStaff : Staff
    {
        public List<WorkingPeriod> WorkingHours { get; set; }
    }

    public class Surgeon : SurgeryStaff
    {
        public Speciality Speciality { get; set; }
        
    }

    public class Nurse : SurgeryStaff
    {
        
    }

    public class Anesthetist: SurgeryStaff 
    {

    }

    public class WorkingPeriod : TimeRange
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
