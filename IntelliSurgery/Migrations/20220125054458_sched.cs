using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class sched : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationTheatreId",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_OperationTheatreId",
                table: "ScheduledSurgeries",
                column: "OperationTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_OperationTheatres_OperationTheatreId",
                table: "ScheduledSurgeries",
                column: "OperationTheatreId",
                principalTable: "OperationTheatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_OperationTheatres_OperationTheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_OperationTheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "OperationTheatreId",
                table: "ScheduledSurgeries");
        }
    }
}
