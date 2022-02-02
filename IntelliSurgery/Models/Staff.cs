using IntelliSurgery.DTOs;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Staff
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SurgeryStaff : Staff
    {
        public List<StaffWorkingPeriod> WorkingHours { get; set; }
    }

    public class Surgeon : SurgeryStaff
    {
        public Speciality Speciality { get; set; }

        public SurgeonDTO getDTO()
        {
            return new SurgeonDTO()
            {
                Id = this.Id,
                Name = this.Name
            };
        }
    }

    public class Nurse : SurgeryStaff
    {
        
    }

    public class Anesthetist: SurgeryStaff 
    {

    }

    

}
