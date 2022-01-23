using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class ModelsTest1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SurgeryNumber",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeId",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Calendar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Specialities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SurgeryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CalendarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Event_Calendar_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "Calendar",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Surgeons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SpecialityId = table.Column<int>(type: "int", nullable: true),
                    WorkingHoursId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surgeons_Calendar_WorkingHoursId",
                        column: x => x.WorkingHoursId,
                        principalTable: "Calendar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Surgeons_Specialities_SpecialityId",
                        column: x => x.SpecialityId,
                        principalTable: "Specialities",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OperationTheatres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TheatreNumber = table.Column<int>(type: "int", nullable: false),
                    TheatreType = table.Column<int>(type: "int", nullable: false),
                    AvailableHoursId = table.Column<int>(type: "int", nullable: true),
                    SurgeryTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTheatres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationTheatres_Calendar_AvailableHoursId",
                        column: x => x.AvailableHoursId,
                        principalTable: "Calendar",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OperationTheatres_SurgeryTypes_SurgeryTypeId",
                        column: x => x.SurgeryTypeId,
                        principalTable: "SurgeryTypes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: true),
                    SurgeonId = table.Column<int>(type: "int", nullable: true),
                    SurgeryTypeId = table.Column<int>(type: "int", nullable: true),
                    PriorityLevel = table.Column<int>(type: "int", nullable: false),
                    AnesthesiaType = table.Column<int>(type: "int", nullable: false),
                    PredictedTimeDuration = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Surgeons_SurgeonId",
                        column: x => x.SurgeonId,
                        principalTable: "Surgeons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_SurgeryTypes_SurgeryTypeId",
                        column: x => x.SurgeryTypeId,
                        principalTable: "SurgeryTypes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UnScheduledSurgeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SurgeryNumber = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnScheduledSurgeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnScheduledSurgeries_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_AppointmentId",
                table: "ScheduledSurgeries",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_TimeId",
                table: "ScheduledSurgeries",
                column: "TimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SurgeonId",
                table: "Appointments",
                column: "SurgeonId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SurgeryTypeId",
                table: "Appointments",
                column: "SurgeryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CalendarId",
                table: "Event",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTheatres_AvailableHoursId",
                table: "OperationTheatres",
                column: "AvailableHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTheatres_SurgeryTypeId",
                table: "OperationTheatres",
                column: "SurgeryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeons_SpecialityId",
                table: "Surgeons",
                column: "SpecialityId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeons_WorkingHoursId",
                table: "Surgeons",
                column: "WorkingHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_UnScheduledSurgeries_AppointmentId",
                table: "UnScheduledSurgeries",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Appointments_AppointmentId",
                table: "ScheduledSurgeries",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Event_TimeId",
                table: "ScheduledSurgeries",
                column: "TimeId",
                principalTable: "Event",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Appointments_AppointmentId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Event_TimeId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "OperationTheatres");

            migrationBuilder.DropTable(
                name: "UnScheduledSurgeries");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Surgeons");

            migrationBuilder.DropTable(
                name: "SurgeryTypes");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropTable(
                name: "Specialities");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_AppointmentId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_TimeId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "SurgeryNumber",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "TimeId",
                table: "ScheduledSurgeries");
        }
    }
}
