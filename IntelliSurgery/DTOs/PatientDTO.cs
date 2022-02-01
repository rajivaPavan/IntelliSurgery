using IntelliSurgery.Enums;
using System;

namespace IntelliSurgery.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public ASA_Status ASA_Status { get; set; }
    }
}
