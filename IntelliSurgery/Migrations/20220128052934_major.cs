using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class major : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_OperationTheatres_OperationTheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_TheaterAvailablePeriod_OperationTheatres_OperationTheatreId",
                table: "TheaterAvailablePeriod");

            migrationBuilder.DropTable(
                name: "OperationTheatres");

            migrationBuilder.RenameColumn(
                name: "OperationTheatreId",
                table: "TheaterAvailablePeriod",
                newName: "TheatreId");

            migrationBuilder.RenameIndex(
                name: "IX_TheaterAvailablePeriod_OperationTheatreId",
                table: "TheaterAvailablePeriod",
                newName: "IX_TheaterAvailablePeriod_TheatreId");

            migrationBuilder.RenameColumn(
                name: "OperationTheatreId",
                table: "ScheduledSurgeries",
                newName: "TheatreId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledSurgeries_OperationTheatreId",
                table: "ScheduledSurgeries",
                newName: "IX_ScheduledSurgeries_TheatreId");

            migrationBuilder.RenameColumn(
                name: "AppointmentStatus",
                table: "Appointments",
                newName: "Status");

            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Patients",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "TheatreTypeId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TheatreTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OperationTheatreType = table.Column<int>(type: "int", nullable: false),
                    SurgeryTypeSurgeryTheatreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheatreTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TheatreTypes_SurgeryType_Theatres_SurgeryTypeSurgeryTheatreId",
                        column: x => x.SurgeryTypeSurgeryTheatreId,
                        principalTable: "SurgeryType_Theatres",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Theatres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TheatreTypeId = table.Column<int>(type: "int", nullable: true),
                    TheatreNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theatres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Theatres_TheatreTypes_TheatreTypeId",
                        column: x => x.TheatreTypeId,
                        principalTable: "TheatreTypes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DateAdded",
                table: "Appointments",
                column: "DateAdded");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Priority",
                table: "Appointments",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PriorityLevel",
                table: "Appointments",
                column: "PriorityLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Status",
                table: "Appointments",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_TheatreTypeId",
                table: "Appointments",
                column: "TheatreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Theatres_TheatreTypeId",
                table: "Theatres",
                column: "TheatreTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TheatreTypes_SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes",
                column: "SurgeryTypeSurgeryTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_TheatreTypes_TheatreTypeId",
                table: "Appointments",
                column: "TheatreTypeId",
                principalTable: "TheatreTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_Theatres_TheatreId",
                table: "ScheduledSurgeries",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TheaterAvailablePeriod_Theatres_TheatreId",
                table: "TheaterAvailablePeriod",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_TheatreTypes_TheatreTypeId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledSurgeries_Theatres_TheatreId",
                table: "ScheduledSurgeries");

            migrationBuilder.DropForeignKey(
                name: "FK_TheaterAvailablePeriod_Theatres_TheatreId",
                table: "TheaterAvailablePeriod");

            migrationBuilder.DropTable(
                name: "Theatres");

            migrationBuilder.DropTable(
                name: "TheatreTypes");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DateAdded",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Priority",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PriorityLevel",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Status",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_TheatreTypeId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "TheatreTypeId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "TheatreId",
                table: "TheaterAvailablePeriod",
                newName: "OperationTheatreId");

            migrationBuilder.RenameIndex(
                name: "IX_TheaterAvailablePeriod_TheatreId",
                table: "TheaterAvailablePeriod",
                newName: "IX_TheaterAvailablePeriod_OperationTheatreId");

            migrationBuilder.RenameColumn(
                name: "TheatreId",
                table: "ScheduledSurgeries",
                newName: "OperationTheatreId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledSurgeries_TheatreId",
                table: "ScheduledSurgeries",
                newName: "IX_ScheduledSurgeries_OperationTheatreId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Appointments",
                newName: "AppointmentStatus");

            migrationBuilder.CreateTable(
                name: "OperationTheatres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SurgeryTypeSurgeryTheatreId = table.Column<int>(type: "int", nullable: true),
                    TheatreNumber = table.Column<int>(type: "int", nullable: false),
                    TheatreType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationTheatres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationTheatres_SurgeryType_Theatres_SurgeryTypeSurgeryThe~",
                        column: x => x.SurgeryTypeSurgeryTheatreId,
                        principalTable: "SurgeryType_Theatres",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTheatres_SurgeryTypeSurgeryTheatreId",
                table: "OperationTheatres",
                column: "SurgeryTypeSurgeryTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledSurgeries_OperationTheatres_OperationTheatreId",
                table: "ScheduledSurgeries",
                column: "OperationTheatreId",
                principalTable: "OperationTheatres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TheaterAvailablePeriod_OperationTheatres_OperationTheatreId",
                table: "TheaterAvailablePeriod",
                column: "OperationTheatreId",
                principalTable: "OperationTheatres",
                principalColumn: "Id");
        }
    }
}
