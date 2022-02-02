﻿// <auto-generated />
using System;
using IntelliSurgery.DbOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IntelliSurgery.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220202044534_surgerystaffchange")]
    partial class surgerystaffchange
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DiseasePatient", b =>
                {
                    b.Property<int>("DiseasesId")
                        .HasColumnType("int");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("DiseasesId", "PatientsId");

                    b.HasIndex("PatientsId");

                    b.ToTable("DiseasePatient");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnesthesiaType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ApproximateProcedureDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("ComplicationPossibility")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<float?>("Priority")
                        .HasColumnType("float");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduledSurgeryId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SurgeonId")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("SurgeonsPredictedDuration")
                        .HasColumnType("time(6)");

                    b.Property<int>("SurgeryTypeId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("SystemPredictedDuration")
                        .HasColumnType("time(6)");

                    b.Property<int?>("TheatreId")
                        .HasColumnType("int");

                    b.Property<int>("TheatreTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DateAdded");

                    b.HasIndex("PatientId");

                    b.HasIndex("Priority");

                    b.HasIndex("PriorityLevel");

                    b.HasIndex("ScheduledSurgeryId");

                    b.HasIndex("Status");

                    b.HasIndex("SurgeonId");

                    b.HasIndex("SurgeryTypeId");

                    b.HasIndex("TheatreId");

                    b.HasIndex("TheatreTypeId");

                    b.HasIndex("ScheduledSurgeryId", "SurgeonId");

                    b.HasIndex("ScheduledSurgeryId", "SurgeryTypeId");

                    b.HasIndex("ScheduledSurgeryId", "TheatreId");

                    b.HasIndex("ScheduledSurgeryId", "TheatreTypeId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DiseaseEnum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Disease");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ASA_Status")
                        .HasColumnType("int");

                    b.Property<float>("BMI")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<float>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("IntelliSurgery.Models.ScheduledSurgery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryEventId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkingBlockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeryEventId");

                    b.HasIndex("WorkingBlockId");

                    b.ToTable("ScheduledSurgeries");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Speciality", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("IntelliSurgery.Models.StaffWorkingPeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SurgeonId")
                        .HasColumnType("int");

                    b.Property<int?>("TheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeonId");

                    b.HasIndex("TheatreId");

                    b.ToTable("StaffWorkingPeriod");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("SpecialityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.ToTable("Surgeons");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("SurgeryEvent");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("SurgeryTypes");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Theatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("TheatreTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheatreTypeId");

                    b.ToTable("Theatres");
                });

            modelBuilder.Entity("IntelliSurgery.Models.TheatreType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TheatreTypes");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("RemainingTime")
                        .HasColumnType("time(6)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("SurgeonId")
                        .HasColumnType("int");

                    b.Property<int?>("TheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeonId");

                    b.HasIndex("TheatreId");

                    b.ToTable("WorkingBlocks");
                });

            modelBuilder.Entity("SurgeryTypeTheatreType", b =>
                {
                    b.Property<int>("SuitableTheatreTypesId")
                        .HasColumnType("int");

                    b.Property<int>("SurgeryTypesConductedId")
                        .HasColumnType("int");

                    b.HasKey("SuitableTheatreTypesId", "SurgeryTypesConductedId");

                    b.HasIndex("SurgeryTypesConductedId");

                    b.ToTable("SurgeryTypeTheatreType");
                });

            modelBuilder.Entity("DiseasePatient", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Disease", null)
                        .WithMany()
                        .HasForeignKey("DiseasesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelliSurgery.Models.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntelliSurgery.Models.Appointment", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelliSurgery.Models.ScheduledSurgery", "ScheduledSurgery")
                        .WithMany()
                        .HasForeignKey("ScheduledSurgeryId");

                    b.HasOne("IntelliSurgery.Models.Surgeon", "Surgeon")
                        .WithMany()
                        .HasForeignKey("SurgeonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelliSurgery.Models.SurgeryType", "SurgeryType")
                        .WithMany()
                        .HasForeignKey("SurgeryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelliSurgery.Models.Theatre", "Theatre")
                        .WithMany("ScheduledAppointments")
                        .HasForeignKey("TheatreId");

                    b.HasOne("IntelliSurgery.Models.TheatreType", "TheatreType")
                        .WithMany()
                        .HasForeignKey("TheatreTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("ScheduledSurgery");

                    b.Navigation("Surgeon");

                    b.Navigation("SurgeryType");

                    b.Navigation("Theatre");

                    b.Navigation("TheatreType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.ScheduledSurgery", b =>
                {
                    b.HasOne("IntelliSurgery.Models.SurgeryEvent", "SurgeryEvent")
                        .WithMany()
                        .HasForeignKey("SurgeryEventId");

                    b.HasOne("IntelliSurgery.Models.WorkingBlock", null)
                        .WithMany("AllocatedSurgeries")
                        .HasForeignKey("WorkingBlockId");

                    b.Navigation("SurgeryEvent");
                });

            modelBuilder.Entity("IntelliSurgery.Models.StaffWorkingPeriod", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Surgeon", null)
                        .WithMany("WorkingHours")
                        .HasForeignKey("SurgeonId");

                    b.HasOne("IntelliSurgery.Models.Theatre", "Theatre")
                        .WithMany()
                        .HasForeignKey("TheatreId");

                    b.Navigation("Theatre");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Theatre", b =>
                {
                    b.HasOne("IntelliSurgery.Models.TheatreType", "TheatreType")
                        .WithMany()
                        .HasForeignKey("TheatreTypeId");

                    b.Navigation("TheatreType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingBlock", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Surgeon", "Surgeon")
                        .WithMany()
                        .HasForeignKey("SurgeonId");

                    b.HasOne("IntelliSurgery.Models.Theatre", "Theatre")
                        .WithMany()
                        .HasForeignKey("TheatreId");

                    b.Navigation("Surgeon");

                    b.Navigation("Theatre");
                });

            modelBuilder.Entity("SurgeryTypeTheatreType", b =>
                {
                    b.HasOne("IntelliSurgery.Models.TheatreType", null)
                        .WithMany()
                        .HasForeignKey("SuitableTheatreTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IntelliSurgery.Models.SurgeryType", null)
                        .WithMany()
                        .HasForeignKey("SurgeryTypesConductedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Theatre", b =>
                {
                    b.Navigation("ScheduledAppointments");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingBlock", b =>
                {
                    b.Navigation("AllocatedSurgeries");
                });
#pragma warning restore 612, 618
        }
    }
}
