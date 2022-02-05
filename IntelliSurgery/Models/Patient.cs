using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public ASA_Status AsaStatus { get; set; }
        public float BMI { get; set; }
        public List<Disease> Diseases { get; set; }
         
    }
}
