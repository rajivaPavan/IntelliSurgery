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
    [Migration("20220129080419_null")]
    partial class @null
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

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("PredictedTimeDuration")
                        .HasColumnType("time(6)");

                    b.Property<float?>("Priority")
                        .HasColumnType("float");

                    b.Property<int>("PriorityLevel")
                        .HasColumnType("int");

                    b.Property<int?>("ScheduledSurgeryId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeonId")
                        .HasColumnType("int");

                    b.Property<int?>("SurgeryTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("TheatreTypeId")
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

                    b.HasIndex("TheatreTypeId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

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

                    b.Property<int?>("TheatreId")
                        .HasColumnType("int");

                    b.Property<int?>("WorkingBlockId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeryEventId");

                    b.HasIndex("TheatreId");

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

                    b.HasKey("Id");

                    b.HasIndex("SurgeonId");

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

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("TheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TheatreId");

                    b.ToTable("TheaterAvailablePeriod");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Theatre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("TheatreNumber")
                        .HasColumnType("int");

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

                    b.Property<int?>("SurgeryTypeSurgeryTheatreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeryTypeSurgeryTheatreId");

                    b.ToTable("TheatreTypes");
                });

            modelBuilder.Entity("IntelliSurgery.Models.UnScheduledSurgery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UnScheduledSurgeries");
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

                    b.Property<int?>("SurgeonWorkingPeriodId")
                        .HasColumnType("int");

                    b.Property<int?>("TheaterAvailablePeriodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurgeonWorkingPeriodId");

                    b.HasIndex("TheaterAvailablePeriodId");

                    b.ToTable("WorkingBlocks");
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

                    b.HasOne("IntelliSurgery.Models.TheatreType", "TheatreType")
                        .WithMany()
                        .HasForeignKey("TheatreTypeId");

                    b.Navigation("Patient");

                    b.Navigation("ScheduledSurgery");

                    b.Navigation("Surgeon");

                    b.Navigation("SurgeryType");

                    b.Navigation("TheatreType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.ScheduledSurgery", b =>
                {
                    b.HasOne("IntelliSurgery.Models.SurgeryEvent", "SurgeryEvent")
                        .WithMany()
                        .HasForeignKey("SurgeryEventId");

                    b.HasOne("IntelliSurgery.Models.Theatre", null)
                        .WithMany("Surgeries")
                        .HasForeignKey("TheatreId");

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
                    b.HasOne("IntelliSurgery.Models.Theatre", null)
                        .WithMany("TheaterAvailablePeriods")
                        .HasForeignKey("TheatreId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Theatre", b =>
                {
                    b.HasOne("IntelliSurgery.Models.TheatreType", "TheatreType")
                        .WithMany()
                        .HasForeignKey("TheatreTypeId");

                    b.Navigation("TheatreType");
                });

            modelBuilder.Entity("IntelliSurgery.Models.TheatreType", b =>
                {
                    b.HasOne("IntelliSurgery.Models.SurgeryTypeSurgeryTheatre", null)
                        .WithMany("SuitableTheatreTypes")
                        .HasForeignKey("SurgeryTypeSurgeryTheatreId");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingBlock", b =>
                {
                    b.HasOne("IntelliSurgery.Models.StaffWorkingPeriod", "SurgeonWorkingPeriod")
                        .WithMany()
                        .HasForeignKey("SurgeonWorkingPeriodId");

                    b.HasOne("IntelliSurgery.Models.TheaterAvailablePeriod", "TheaterAvailablePeriod")
                        .WithMany()
                        .HasForeignKey("TheaterAvailablePeriodId");

                    b.Navigation("SurgeonWorkingPeriod");

                    b.Navigation("TheaterAvailablePeriod");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Surgeon", b =>
                {
                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("IntelliSurgery.Models.SurgeryTypeSurgeryTheatre", b =>
                {
                    b.Navigation("SuitableTheatreTypes");
                });

            modelBuilder.Entity("IntelliSurgery.Models.Theatre", b =>
                {
                    b.Navigation("Surgeries");

                    b.Navigation("TheaterAvailablePeriods");
                });

            modelBuilder.Entity("IntelliSurgery.Models.WorkingBlock", b =>
                {
                    b.Navigation("AllocatedSurgeries");
                });
#pragma warning restore 612, 618
        }
    }
}
