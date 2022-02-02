using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class surgerystaffchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurgeonId",
                table: "WorkingBlocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TheatreId",
                table: "StaffWorkingPeriod",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_SurgeonId",
                table: "WorkingBlocks",
                column: "SurgeonId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffWorkingPeriod_TheatreId",
                table: "StaffWorkingPeriod",
                column: "TheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffWorkingPeriod_Theatres_TheatreId",
                table: "StaffWorkingPeriod",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_Surgeons_SurgeonId",
                table: "WorkingBlocks",
                column: "SurgeonId",
                principalTable: "Surgeons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffWorkingPeriod_Theatres_TheatreId",
                table: "StaffWorkingPeriod");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_Surgeons_SurgeonId",
                table: "WorkingBlocks");

            migrationBuilder.DropIndex(
                name: "IX_WorkingBlocks_SurgeonId",
                table: "WorkingBlocks");

            migrationBuilder.DropIndex(
                name: "IX_StaffWorkingPeriod_TheatreId",
                table: "StaffWorkingPeriod");

            migrationBuilder.DropColumn(
                name: "SurgeonId",
                table: "WorkingBlocks");

            migrationBuilder.DropColumn(
                name: "TheatreId",
                table: "StaffWorkingPeriod");
        }
    }
}
