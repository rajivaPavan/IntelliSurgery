using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class surgeryevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Event_TimeId",
                table: "ScheduledSurgeries");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "ScheduledSurgeries",
                newName: "SurgeryEventId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledSurgeries_TimeId",
                table: "ScheduledSurgeries",
                newName: "IX_ScheduledSurgeries_SurgeryEventId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Event",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Event_SurgeryEventId",
                table: "ScheduledSurgeries",
                column: "SurgeryEventId",
                principalTable: "Event",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Event_SurgeryEventId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Event");

            migrationBuilder.RenameColumn(
                name: "SurgeryEventId",
                table: "ScheduledSurgeries",
                newName: "TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledSurgeries_SurgeryEventId",
                table: "ScheduledSurgeries",
                newName: "IX_ScheduledSurgeries_TimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Event_TimeId",
                table: "ScheduledSurgeries",
                column: "TimeId",
                principalTable: "Event",
                principalColumn: "Id");
        }
    }
}
