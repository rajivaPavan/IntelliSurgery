using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class nulldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_Priority",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Appointments");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SystemPredictedDuration",
                table: "Appointments",
                type: "time(6)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_SurgeonId_Start",
                table: "WorkingBlocks",
                columns: new[] { "SurgeonId", "Start" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkingBlocks_SurgeonId_Start",
                table: "WorkingBlocks");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "SystemPredictedDuration",
                table: "Appointments",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Priority",
                table: "Appointments",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Priority",
                table: "Appointments",
                column: "Priority");
        }
    }
}
