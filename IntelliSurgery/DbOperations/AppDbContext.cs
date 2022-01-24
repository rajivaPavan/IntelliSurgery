
using IntelliSurgery.Models;
using Microsoft.EntityFrameworkCore;

namespace IntelliSurgery.DbOperations
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Surgeon> Surgeons { get; set;}
        public DbSet<SurgeryType> SurgeryTypes { get; set;}
        public DbSet<ScheduledSurgery> ScheduledSurgeries{ get; set; }
        public DbSet<UnScheduledSurgery> UnScheduledSurgeries { get; set; }
        public DbSet<OperationTheatre> OperationTheatres { get; set;}
        public DbSet<SurgeryTypeSurgeryTheatre> SurgeryType_Theatres { get; set; }
    }
}
