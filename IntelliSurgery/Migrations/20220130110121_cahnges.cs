using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class cahnges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Theatres_TheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_TheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "TheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.AddColumn<int>(
                name: "TheatreId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TheatreId",
                table: "Appointments",
                column: "TheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Theatres_TheatreId",
                table: "Appointments",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Theatres_TheatreId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TheatreId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "TheatreId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "TheatreId",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_TheatreId",
                table: "ScheduledSurgeries",
                column: "TheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Theatres_TheatreId",
                table: "ScheduledSurgeries",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");
        }
    }
}
