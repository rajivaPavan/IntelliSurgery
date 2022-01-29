using IntelliSurgery.Enums;

namespace IntelliSurgery.DTOs
{
    public class SurgeonDTO
    {
        public int SurgeonId { get; set; }
        public string SurgeonName { get; set; }
    }

    public class SurgeryTypeDTO
    {
        public int SurgeryTypeId { get;set; }
        public string SurgeryTypeName { get; set; }
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
