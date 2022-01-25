using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class keychanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Appointments_AppointmentId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_UnScheduledSurgeries_Appointments_AppointmentId",
                table: "UnScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_UnScheduledSurgeries_AppointmentId",
                table: "UnScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_AppointmentId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "UnScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "ScheduledSurgeries");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Appointments",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ScheduledSurgeryId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduledSurgeryId",
                table: "Appointments",
                column: "ScheduledSurgeryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId",
                table: "Appointments",
                column: "ScheduledSurgeryId",
                principalTable: "ScheduledSurgeries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ScheduledSurgeryId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ScheduledSurgeryId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "UnScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnScheduledSurgeries_AppointmentId",
                table: "UnScheduledSurgeries",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_AppointmentId",
                table: "ScheduledSurgeries",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Appointments_AppointmentId",
                table: "ScheduledSurgeries",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnScheduledSurgeries_Appointments_AppointmentId",
                table: "UnScheduledSurgeries",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
