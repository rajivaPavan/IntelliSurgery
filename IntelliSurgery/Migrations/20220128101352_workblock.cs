using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class workblock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkingBlockId",
                table: "ScheduledSurgeries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkingBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TheaterAvailablePeriodId = table.Column<int>(type: "int", nullable: true),
                    SurgeonWorkingPeriodId = table.Column<int>(type: "int", nullable: true),
                    RemainingTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingBlocks_TheaterAvailablePeriod_TheaterAvailablePeriodId",
                        column: x => x.TheaterAvailablePeriodId,
                        principalTable: "TheaterAvailablePeriod",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkingBlocks_WorkingPeriod_SurgeonWorkingPeriodId",
                        column: x => x.SurgeonWorkingPeriodId,
                        principalTable: "WorkingPeriod",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledSurgeries_WorkingBlockId",
                table: "ScheduledSurgeries",
                column: "WorkingBlockId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_SurgeonWorkingPeriodId",
                table: "WorkingBlocks",
                column: "SurgeonWorkingPeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_TheaterAvailablePeriodId",
                table: "WorkingBlocks",
                column: "TheaterAvailablePeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_WorkingBlocks_WorkingBlockId",
                table: "ScheduledSurgeries",
                column: "WorkingBlockId",
                principalTable: "WorkingBlocks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_WorkingBlocks_WorkingBlockId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropTable(
                name: "WorkingBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledSurgeries_WorkingBlockId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropColumn(
                name: "WorkingBlockId",
                table: "ScheduledSurgeries");
        }
    }
}
