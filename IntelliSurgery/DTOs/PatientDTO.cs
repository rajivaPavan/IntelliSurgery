using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{
    public class PatientDTO
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public int AsaStatus { get; set; }
        public List<int> DiseasesValues { get; set; }
    }
}
