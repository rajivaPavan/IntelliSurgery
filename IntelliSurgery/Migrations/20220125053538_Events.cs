using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTheatres_Calendar_AvailableHoursId",
                table: "OperationTheatres");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Event_SurgeryEventId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_Surgeons_Calendar_WorkingHoursId",
                table: "Surgeons");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Calendar");

            migrationBuilder.DropIndex(
                name: "IX_Surgeons_WorkingHoursId",
                table: "Surgeons");

            migrationBuilder.DropIndex(
                name: "IX_OperationTheatres_AvailableHoursId",
                table: "OperationTheatres");

            migrationBuilder.DropColumn(
                name: "WorkingHoursId",
                table: "Surgeons");

            migrationBuilder.DropColumn(
                name: "AvailableHoursId",
                table: "OperationTheatres");

            migrationBuilder.CreateTable(
                name: "SurgeryEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryEvent", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TheaterAvailablePeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OperationTheatreId = table.Column<int>(type: "int", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheaterAvailablePeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheaterAvailablePeriod_OperationTheatres_OperationTheatreId",
                        column: x => x.OperationTheatreId,
                        principalTable: "OperationTheatres",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkingPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SurgeonId = table.Column<int>(type: "int", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriod_Surgeons_SurgeonId",
                        column: x => x.SurgeonId,
                        principalTable: "Surgeons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TheaterAvailablePeriod_OperationTheatreId",
                table: "TheaterAvailablePeriod",
                column: "OperationTheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriod_SurgeonId",
                table: "WorkingPeriod",
                column: "SurgeonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_SurgeryEvent_SurgeryEventId",
                table: "ScheduledSurgeries",
                column: "SurgeryEventId",
                principalTable: "SurgeryEvent",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_SurgeryEvent_SurgeryEventId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropTable(
                name: "SurgeryEvent");

            migrationBuilder.DropTable(
                name: "TheaterAvailablePeriod");

            migrationBuilder.DropTable(
                name: "WorkingPeriod");

            migrationBuilder.AddColumn<int>(
                name: "WorkingHoursId",
                table: "Surgeons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvailableHoursId",
                table: "OperationTheatres",
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
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CalendarId = table.Column<int>(type: "int", nullable: true),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Surgeons_WorkingHoursId",
                table: "Surgeons",
                column: "WorkingHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTheatres_AvailableHoursId",
                table: "OperationTheatres",
                column: "AvailableHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_CalendarId",
                table: "Event",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTheatres_Calendar_AvailableHoursId",
                table: "OperationTheatres",
                column: "AvailableHoursId",
                principalTable: "Calendar",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Event_SurgeryEventId",
                table: "ScheduledSurgeries",
                column: "SurgeryEventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surgeons_Calendar_WorkingHoursId",
                table: "Surgeons",
                column: "WorkingHoursId",
                principalTable: "Calendar",
                principalColumn: "Id");
        }
    }
}
