using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class workingblockid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_SurgeryEvent_SurgeryEventId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurgeryEvent",
                table: "SurgeryEvent");

            migrationBuilder.RenameTable(
                name: "SurgeryEvent",
                newName: "SurgeryEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurgeryEvents",
                table: "SurgeryEvents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_SurgeryEvents_SurgeryEventId",
                table: "ScheduledSurgeries",
                column: "SurgeryEventId",
                principalTable: "SurgeryEvents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_SurgeryEvents_SurgeryEventId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurgeryEvents",
                table: "SurgeryEvents");

            migrationBuilder.RenameTable(
                name: "SurgeryEvents",
                newName: "SurgeryEvent");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurgeryEvent",
                table: "SurgeryEvent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_SurgeryEvent_SurgeryEventId",
                table: "ScheduledSurgeries",
                column: "SurgeryEventId",
                principalTable: "SurgeryEvent",
                principalColumn: "Id");
        }
    }
}
