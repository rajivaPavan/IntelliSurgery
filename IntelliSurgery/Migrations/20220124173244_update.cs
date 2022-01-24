using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurgeryNumber",
                table: "UnScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "SurgeryNumber",
                table: "ScheduledSurgeries");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "SurgeryNumber",
                table: "UnScheduledSurgeries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SurgeryNumber",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
