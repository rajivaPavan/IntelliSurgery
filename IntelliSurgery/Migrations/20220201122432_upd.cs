using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class upd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_StaffWorkingPeriod_SurgeonWorkingPeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_TheaterAvailablePeriod_TheaterAvailablePeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropTable(
                name: "TheaterAvailablePeriod");

            migrationBuilder.DropTable(
                name: "UnScheduledSurgeries");

            migrationBuilder.DropIndex(
                name: "IX_WorkingBlocks_SurgeonWorkingPeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropIndex(
                name: "IX_WorkingBlocks_TheaterAvailablePeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropColumn(
                name: "SurgeonWorkingPeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropColumn(
                name: "TheaterAvailablePeriodId",
                table: "WorkingBlocks");

            migrationBuilder.RenameColumn(
                name: "PredictedTimeDuration",
                table: "Appointments",
                newName: "SystemPredictedDuration");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApproximateProcedureDate",
                table: "Appointments",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SurgeonsPredictedDuration",
                table: "Appointments",
                type: "time(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproximateProcedureDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "SurgeonsPredictedDuration",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "SystemPredictedDuration",
                table: "Appointments",
                newName: "PredictedTimeDuration");

            migrationBuilder.AddColumn<int>(
                name: "SurgeonWorkingPeriodId",
                table: "WorkingBlocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TheaterAvailablePeriodId",
                table: "WorkingBlocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TheaterAvailablePeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TheatreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheaterAvailablePeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheaterAvailablePeriod_Theatres_TheatreId",
                        column: x => x.TheatreId,
                        principalTable: "Theatres",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UnScheduledSurgeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnScheduledSurgeries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_SurgeonWorkingPeriodId",
                table: "WorkingBlocks",
                column: "SurgeonWorkingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_TheaterAvailablePeriodId",
                table: "WorkingBlocks",
                column: "TheaterAvailablePeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_TheaterAvailablePeriod_TheatreId",
                table: "TheaterAvailablePeriod",
                column: "TheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_StaffWorkingPeriod_SurgeonWorkingPeriodId",
                table: "WorkingBlocks",
                column: "SurgeonWorkingPeriodId",
                principalTable: "StaffWorkingPeriod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_TheaterAvailablePeriod_TheaterAvailablePeriodId",
                table: "WorkingBlocks",
                column: "TheaterAvailablePeriodId",
                principalTable: "TheaterAvailablePeriod",
                principalColumn: "Id");
        }
    }
}
