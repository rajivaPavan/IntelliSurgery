using IntelliSurgery.Enums;

namespace IntelliSurgery.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Gender { get; set; }
        public float Weight { get; set; }
    }
}
