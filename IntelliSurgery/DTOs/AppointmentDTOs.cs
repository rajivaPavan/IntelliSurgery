using IntelliSurgery.Enums;
using IntelliSurgery.Models;
using System.Collections.Generic;

namespace IntelliSurgery.DTOs
{
    public class AppointmentDTO
    {
        public int PatientId { get; set; }
        public int SurgeryType { get; set; }
        public int TheatreType { get; set; }    
        public int SurgeonId { get; set; }
        public bool IsAnesthesiaRequired { get; set; }
        public int AnesthesiaType { get; set; }
        public int PriorityLevel { get; set; }
    }

    public class DropDownListsDTO
    {
        public List<SurgeryType> SurgeryTypes{ get; set; }
        public List<Surgeon> Surgeons { get; set; }
        public List<TheatreType> TheatreTypes{ get; set; }
        public List<AnesthesiaDTO> Anesthesias { get; set; }
    }

}
