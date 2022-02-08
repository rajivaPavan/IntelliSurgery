using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{

    public class HospitalDataDTO
    {
        public List<Speciality> Specialities { get; set; }
        public List<SurgeonDTO> Surgeons { get; set; }
        public List<SurgeryType> SurgeryTypes { get; set; }
        public List<TheatreType> TheatreTypes { get; set; } 
        public List<TheatreDTO> Theatres { get; set; }
        public List<Speciality> DeleteSpecialities { get; set; }
        public List<SurgeonDTO> DeleteSurgeons { get; set; }
        public List<SurgeryType> DeleteSurgeryTypes { get; set; }
        public List<TheatreType> DeleteTheatreTypes { get; set; }
        public List<TheatreDTO> DeleteTheatres { get; set; }
        public List<SurgeryTypeTheatresDTO> SurgeryTypeTheatres { get; set; }
        public List<SurgerySurgeonSpecialitiesDTO> SurgerySurgeonSpecialities { get; set; }
        public List<Surgeon> SurgeonSchedules { get; internal set; }

        //surgeon working hours
    }

    public class SurgeryTypeTheatresDTO
    {
        public int SurgeryTypeId { get; set; }
        public List<TheatreType> TheatreTypes { get; set; }
        public List<int> TheatreTypeIds { get; set; }
    }

    public class SurgerySurgeonSpecialitiesDTO
    {
        public int SurgeryTypeId { get; set; }
        public List<Speciality> Specialities { get; set; }
        public List<int> SpecialityIds { get; set; }
    }

    public class TheatreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TheatreType TheatreType { get; set; }
    }

    public class SurgeonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Speciality Speciality { get; set; }
    }

    public class AnesthesiaDTO
    {

        public AnesthesiaDTO(AnesthesiaType anesthesiaType)
        {
            this.Type = anesthesiaType;
            this.Name = System.Text.RegularExpressions.Regex.Replace(anesthesiaType.ToString(), "([A-Z])", " $1", 
                System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }

        public AnesthesiaType Type { get; set; }
        public string Name { get; set; }
    }

}
