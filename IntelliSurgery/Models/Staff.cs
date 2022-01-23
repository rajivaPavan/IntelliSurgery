namespace IntelliSurgery.Models
{
    public class Staff
    {
        public int Id { get; set; }
    }
    public class SurgeryStaff : Staff
    {
        public Calendar WorkingHours { get; set; }
    }

    public class Surgeon : SurgeryStaff
    {
        public Speciality Speciality { get; set; }
        
    }

    public class Nurse : SurgeryStaff
    {
        
    }

    public class Anesthetist: SurgeryStaff 
    {

    }

}
