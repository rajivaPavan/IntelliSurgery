using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class AppointmentForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Surgeons_SurgeonId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SurgeryTypes_SurgeryTypeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Theatres_TheatreId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TheatreTypes_TheatreTypeId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "TheatreTypeId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TheatreId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SurgeryTypeId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SurgeonId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ScheduledSurgeryId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId",
                table: "Appointments",
                column: "ScheduledSurgeryId",
                principalTable: "ScheduledSurgeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Surgeons_SurgeonId",
                table: "Appointments",
                column: "SurgeonId",
                principalTable: "Surgeons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SurgeryTypes_SurgeryTypeId",
                table: "Appointments",
                column: "SurgeryTypeId",
                principalTable: "SurgeryTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Theatres_TheatreId",
                table: "Appointments",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TheatreTypes_TheatreTypeId",
                table: "Appointments",
                column: "TheatreTypeId",
                principalTable: "TheatreTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Surgeons_SurgeonId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SurgeryTypes_SurgeryTypeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Theatres_TheatreId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TheatreTypes_TheatreTypeId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "TheatreTypeId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TheatreId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SurgeryTypeId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SurgeonId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduledSurgeryId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId",
                table: "Appointments",
                column: "ScheduledSurgeryId",
                principalTable: "ScheduledSurgeries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Surgeons_SurgeonId",
                table: "Appointments",
                column: "SurgeonId",
                principalTable: "Surgeons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SurgeryTypes_SurgeryTypeId",
                table: "Appointments",
                column: "SurgeryTypeId",
                principalTable: "SurgeryTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Theatres_TheatreId",
                table: "Appointments",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TheatreTypes_TheatreTypeId",
                table: "Appointments",
                column: "TheatreTypeId",
                principalTable: "TheatreTypes",
                principalColumn: "Id");
        }
    }
}
