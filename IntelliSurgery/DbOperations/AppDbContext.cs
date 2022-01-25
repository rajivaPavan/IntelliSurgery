
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.DateAdded);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.PriorityLevel);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.Priority);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.Status);
        }


    }
}
