using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class dob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_WorkingPeriod_SurgeonWorkingPeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropTable(
                name: "WorkingPeriod");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Surgeons",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "StaffWorkingPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SurgeonId = table.Column<int>(type: "int", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffWorkingPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffWorkingPeriod_Surgeons_SurgeonId",
                        column: x => x.SurgeonId,
                        principalTable: "Surgeons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StaffWorkingPeriod_SurgeonId",
                table: "StaffWorkingPeriod",
                column: "SurgeonId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_StaffWorkingPeriod_SurgeonWorkingPeriodId",
                table: "WorkingBlocks",
                column: "SurgeonWorkingPeriodId",
                principalTable: "StaffWorkingPeriod",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_StaffWorkingPeriod_SurgeonWorkingPeriodId",
                table: "WorkingBlocks");

            migrationBuilder.DropTable(
                name: "StaffWorkingPeriod");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Surgeons");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WorkingPeriod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SurgeonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingPeriod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingPeriod_Surgeons_SurgeonId",
                        column: x => x.SurgeonId,
                        principalTable: "Surgeons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingPeriod_SurgeonId",
                table: "WorkingPeriod",
                column: "SurgeonId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_WorkingPeriod_SurgeonWorkingPeriodId",
                table: "WorkingBlocks",
                column: "SurgeonWorkingPeriodId",
                principalTable: "WorkingPeriod",
                principalColumn: "Id");
        }
    }
}
