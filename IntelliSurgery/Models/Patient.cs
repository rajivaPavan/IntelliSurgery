using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;

namespace IntelliSurgery.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
         
    }


}
