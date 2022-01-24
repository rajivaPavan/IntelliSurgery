using System.Collections.Generic;

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
        public List<OperationTheatre> SuitableOR{ get; set; }
    }
}
