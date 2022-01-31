using IntelliSurgery.Enums;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Disease
    {
        public int Id { get; set; }
        public DiseaseEnum DiseaseEnum { get; set; }
        public List<Patient> Patients { get; set; }
    }
}
