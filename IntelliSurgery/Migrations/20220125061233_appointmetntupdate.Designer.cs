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
    [Migration("20220125061233_appointmetntupdate")]
    partial class appointmetntupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IntelliSurgery.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnesthesiaType")
                        .HasColumnType("int");

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("PredictedTimeDuration")
                        .HasColumnType("time(6)");

                    b.Property<float>("Priority")
                        .HasColumnType("float");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduledSurgeryId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeonId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("ScheduledSurgeryId");

                    b.HasIndex("SurgeonId");

                    b.HasIndex("SurgeryTypeId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("IntelliSurgery.Models.OperationTheatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryTypeSurgeryTheatreId")
                        .HasColumnType("int");

                    b.Property<int>("TheatreNumber")
                        .HasColumnType("int");

                    b.Property<int>("TheatreType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeryTypeSurgeryTheatreId");

                    b.ToTable("OperationTheatres");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

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

                    b.Property<int?>("OperationTheatreId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryEventId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperationTheatreId");

                    b.HasIndex("SurgeryEventId");

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

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

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

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryTypeSurgeryTheatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeryTypeId");

                    b.ToTable("SurgeryType_Theatres");
                });

            modelBuilder.Entity("IntelliSurgery.Models.TheaterAvailablePeriod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("OperationTheatreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("OperationTheatreId");

                    b.ToTable("TheaterAvailablePeriod");
                });

            modelBuilder.Entity("IntelliSurgery.Models.UnScheduledSurgery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UnScheduledSurgeries");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingPeriod", b =>
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

                    b.HasKey("Id");

                    b.HasIndex("SurgeonId");

                    b.ToTable("WorkingPeriod");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Appointment", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("IntelliSurgery.Models.ScheduledSurgery", "ScheduledSurgery")
                        .WithMany()
                        .HasForeignKey("ScheduledSurgeryId");

                    b.HasOne("IntelliSurgery.Models.Surgeon", "Surgeon")
                        .WithMany()
                        .HasForeignKey("SurgeonId");

                    b.HasOne("IntelliSurgery.Models.SurgeryType", "SurgeryType")
                        .WithMany()
                        .HasForeignKey("SurgeryTypeId");

                    b.Navigation("Patient");

                    b.Navigation("ScheduledSurgery");

                    b.Navigation("Surgeon");

                    b.Navigation("SurgeryType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.OperationTheatre", b =>
                {
                    b.HasOne("IntelliSurgery.Models.SurgeryTypeSurgeryTheatre", null)
                        .WithMany("SuitableOR")
                        .HasForeignKey("SurgeryTypeSurgeryTheatreId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.ScheduledSurgery", b =>
                {
                    b.HasOne("IntelliSurgery.Models.OperationTheatre", null)
                        .WithMany("Surgeries")
                        .HasForeignKey("OperationTheatreId");

                    b.HasOne("IntelliSurgery.Models.SurgeryEvent", "SurgeryEvent")
                        .WithMany()
                        .HasForeignKey("SurgeryEventId");

                    b.Navigation("SurgeryEvent");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId");

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryTypeSurgeryTheatre", b =>
                {
                    b.HasOne("IntelliSurgery.Models.SurgeryType", "SurgeryType")
                        .WithMany()
                        .HasForeignKey("SurgeryTypeId");

                    b.Navigation("SurgeryType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.TheaterAvailablePeriod", b =>
                {
                    b.HasOne("IntelliSurgery.Models.OperationTheatre", null)
                        .WithMany("TheaterAvailablePeriods")
                        .HasForeignKey("OperationTheatreId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingPeriod", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Surgeon", null)
                        .WithMany("WorkingHours")
                        .HasForeignKey("SurgeonId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.OperationTheatre", b =>
                {
                    b.Navigation("Surgeries");

                    b.Navigation("TheaterAvailablePeriods");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryTypeSurgeryTheatre", b =>
                {
                    b.Navigation("SuitableOR");
                });
#pragma warning restore 612, 618
        }
    }
}
