using IntelliSurgery.DTOs;
using IntelliSurgery.Enums;
using System;

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
         
    }


}
