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
        public List<SurgeryTypeTheatresDTO> SurgeryTypeTheatres { get; set; }

        //theatre available time
        //surgeon working hours
    }

    public class SurgeryTypeTheatresDTO
    {
        public int SurgeryTypeId { get; set; }
        public List<int> TheatreIds { get; set; }
    }

    public class TheatreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TheatreTypeId { get; set; }
    }

    public class SurgeonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SpecialityId { get; set; }
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
