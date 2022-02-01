using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{
    public class PatientMedicalData
    {
        public int Age { get; set; }
        public int Gender { get; set; }
        public int ASA { get; set; }
        public float BMI { get; set; }
        public List<string> Diseases { get; set; }

        public PatientMedicalData(Patient patient)
        {
            Age = DateTime.Now.Subtract(patient.DateOfBirth).Days/365;
            Gender = (int)patient.Gender;
            BMI = patient.BMI;
            ASA = (int)patient.ASA_Status;
            if (patient.Diseases != null)
            {
                Diseases = patient.Diseases.ConvertAll<string>(DiseaseToString);
            }
            else
            {
                Diseases = new List<string>();
            }

        }

        private static string DiseaseToString(Disease disease)
        {
            return disease.DiseaseEnum.ToString();
        }

    }
}
