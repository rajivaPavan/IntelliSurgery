using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntelliSurgery.Models
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SurgeryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SurgeryTypeSurgeryTheatre
    {
        public int Id { get; set; }
        public SurgeryType SurgeryType { get; set; }
        public List<OperationTheatre> SuitableOR { get; set; }
    }
}
