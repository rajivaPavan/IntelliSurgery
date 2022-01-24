using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class fixedPrevious : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTheatres_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres");

            migrationBuilder.DropIndex(
                name: "IX_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres");

            migrationBuilder.DropColumn(
                name: "OperationTheatreId",
                table: "OperationTheatres");

            migrationBuilder.AddColumn<int>(
                name: "OperationTheatreId",
                table: "SurgeryTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryTypes_OperationTheatreId",
                table: "SurgeryTypes",
                column: "OperationTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_SurgeryTypes_OperationTheatres_OperationTheatreId",
                table: "SurgeryTypes",
                column: "OperationTheatreId",
                principalTable: "OperationTheatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SurgeryTypes_OperationTheatres_OperationTheatreId",
                table: "SurgeryTypes");

            migrationBuilder.DropIndex(
                name: "IX_SurgeryTypes_OperationTheatreId",
                table: "SurgeryTypes");

            migrationBuilder.DropColumn(
                name: "OperationTheatreId",
                table: "SurgeryTypes");

            migrationBuilder.AddColumn<int>(
                name: "OperationTheatreId",
                table: "OperationTheatres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres",
                column: "OperationTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTheatres_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres",
                column: "OperationTheatreId",
                principalTable: "OperationTheatres",
                principalColumn: "Id");
        }
    }
}
