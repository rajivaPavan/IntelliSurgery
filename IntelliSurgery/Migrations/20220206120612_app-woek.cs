using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class appwoek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_WorkingBlocks_WorkingBlockId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_WorkingBlockId",
                table: "ScheduledSurgeries");

            migrationBuilder.AddColumn<int>(
                name: "WorkingBlockId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_WorkingBlockId",
                table: "Appointments",
                column: "WorkingBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_WorkingBlocks_WorkingBlockId",
                table: "Appointments",
                column: "WorkingBlockId",
                principalTable: "WorkingBlocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_WorkingBlocks_WorkingBlockId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_WorkingBlockId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "WorkingBlockId",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_WorkingBlockId",
                table: "ScheduledSurgeries",
                column: "WorkingBlockId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_WorkingBlocks_WorkingBlockId",
                table: "ScheduledSurgeries",
                column: "WorkingBlockId",
                principalTable: "WorkingBlocks",
                principalColumn: "Id");
        }
    }
}
