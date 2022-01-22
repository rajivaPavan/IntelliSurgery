namespace IntelliSurgery.Models
{
    public class Staff
    {
        public int Id { get; set; }
    }
    public class SurgeryStaff : Staff
    {
    }
    public class Doctor : SurgeryStaff
    {
    }
    public class Nurse : SurgeryStaff
    {

    }
    public class Anesthetist: SurgeryStaff 
    {
    }

}
