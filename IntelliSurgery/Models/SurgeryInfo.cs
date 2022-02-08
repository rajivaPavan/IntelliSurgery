using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntelliSurgery.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SurgeryType> SurgeryTypesPerformed { get; set; }
    }

    public class SurgeryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TheatreType> SuitableTheatreTypes { get; set; }
        public List<Speciality> SuitableSpecialists { get; set; }

    }
}
