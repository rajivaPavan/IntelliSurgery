
using IntelliSurgery.Global;
using IntelliSurgery.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IntelliSurgery.DbOperations
{
    public class AppDbContext : IdentityDbContext
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
        public DbSet<Theatre> Theatres {get; set; }
        public DbSet<TheatreType> TheatreTypes { get; set;}
        public DbSet<WorkingBlock> WorkingBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region appointmentIndexes
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.DateAdded);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.PriorityLevel);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.Status);
            modelBuilder.Entity<Appointment>()
               .HasIndex(x => x.SurgeonId);
            modelBuilder.Entity<Appointment>()
               .HasIndex(x => x.PatientId);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.TheatreTypeId);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => x.ScheduledSurgeryId);
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgeryId, x.TheatreId });
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgeryId, x.TheatreTypeId });
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgeryId, x.SurgeryTypeId });
            modelBuilder.Entity<Appointment>()
                .HasIndex(x => new { x.ScheduledSurgeryId, x.SurgeonId });
            #endregion

            #region workingBlockIndexes
            modelBuilder.Entity<WorkingBlock>()
                .HasIndex(x => new { x.SurgeonId , x.Start});
            #endregion

        }


    }
}
