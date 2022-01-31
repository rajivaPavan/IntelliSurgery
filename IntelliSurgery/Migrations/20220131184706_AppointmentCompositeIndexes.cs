using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class AppointmentCompositeIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduledSurgeryId_SurgeonId",
                table: "Appointments",
                columns: new[] { "ScheduledSurgeryId", "SurgeonId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduledSurgeryId_SurgeryTypeId",
                table: "Appointments",
                columns: new[] { "ScheduledSurgeryId", "SurgeryTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduledSurgeryId_TheatreId",
                table: "Appointments",
                columns: new[] { "ScheduledSurgeryId", "TheatreId" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduledSurgeryId_TheatreTypeId",
                table: "Appointments",
                columns: new[] { "ScheduledSurgeryId", "TheatreTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_ScheduledSurgeryId_SurgeonId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ScheduledSurgeryId_SurgeryTypeId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ScheduledSurgeryId_TheatreId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ScheduledSurgeryId_TheatreTypeId",
                table: "Appointments");
        }
    }
}
