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
    [Migration("20220124181354_fixedPrevious")]
    partial class fixedPrevious
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

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("PredictedTimeDuration")
                        .HasColumnType("time(6)");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeonId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("SurgeonId");

                    b.HasIndex("SurgeryTypeId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Calendar");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CalendarId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("IntelliSurgery.Models.OperationTheatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AvailableHoursId")
                        .HasColumnType("int");

                    b.Property<int>("TheatreNumber")
                        .HasColumnType("int");

                    b.Property<int>("TheatreType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AvailableHoursId");

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

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryEventId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

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

                    b.Property<int?>("WorkingHoursId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("WorkingHoursId");

                    b.ToTable("Surgeons");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int?>("OperationTheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperationTheatreId");

                    b.ToTable("SurgeryTypes");
                });

            modelBuilder.Entity("IntelliSurgery.Models.UnScheduledSurgery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentId");

                    b.ToTable("UnScheduledSurgeries");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Appointment", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("IntelliSurgery.Models.Surgeon", "Surgeon")
                        .WithMany()
                        .HasForeignKey("SurgeonId");

                    b.HasOne("IntelliSurgery.Models.SurgeryType", "SurgeryType")
                        .WithMany()
                        .HasForeignKey("SurgeryTypeId");

                    b.Navigation("Patient");

                    b.Navigation("Surgeon");

                    b.Navigation("SurgeryType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Event", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Calendar", null)
                        .WithMany("Events")
                        .HasForeignKey("CalendarId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.OperationTheatre", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Calendar", "AvailableHours")
                        .WithMany()
                        .HasForeignKey("AvailableHoursId");

                    b.Navigation("AvailableHours");
                });

            modelBuilder.Entity("IntelliSurgery.Models.ScheduledSurgery", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId");

                    b.HasOne("IntelliSurgery.Models.Event", "SurgeryEvent")
                        .WithMany()
                        .HasForeignKey("SurgeryEventId");

                    b.Navigation("Appointment");

                    b.Navigation("SurgeryEvent");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId");

                    b.HasOne("IntelliSurgery.Models.Calendar", "WorkingHours")
                        .WithMany()
                        .HasForeignKey("WorkingHoursId");

                    b.Navigation("Speciality");

                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryType", b =>
                {
                    b.HasOne("IntelliSurgery.Models.OperationTheatre", null)
                        .WithMany("PossibleSurgeryTypess")
                        .HasForeignKey("OperationTheatreId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.UnScheduledSurgery", b =>
                {
                    b.HasOne("IntelliSurgery.Models.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Calendar", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("IntelliSurgery.Models.OperationTheatre", b =>
                {
                    b.Navigation("PossibleSurgeryTypess");
                });
#pragma warning restore 612, 618
        }
    }
}
