
using IntelliSurgery.Global;
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
        public DbSet<Theatre> Theatres {get; set; }
        public DbSet<TheatreType> TheatreTypes { get; set;}
        public DbSet<WorkingBlock> WorkingBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region appointmentIndexes
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.DateAdded);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.PriorityLevel);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.Priority);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.Status);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.TheatreType);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgery, x.Theatre});
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgery, x.TheatreType });
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgery, x.SurgeryType });
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgery, x.Surgeon });
            #endregion

        }


    }
}
