using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTheatres_SurgeryTypes_SurgeryTypeId",
                table: "OperationTheatres");

            migrationBuilder.RenameColumn(
                name: "SurgeryTypeId",
                table: "OperationTheatres",
                newName: "OperationTheatreId");

            migrationBuilder.RenameIndex(
                name: "IX_OperationTheatres_SurgeryTypeId",
                table: "OperationTheatres",
                newName: "IX_OperationTheatres_OperationTheatreId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTheatres_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres",
                column: "OperationTheatreId",
                principalTable: "OperationTheatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTheatres_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres");

            migrationBuilder.RenameColumn(
                name: "OperationTheatreId",
                table: "OperationTheatres",
                newName: "SurgeryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_OperationTheatres_OperationTheatreId",
                table: "OperationTheatres",
                newName: "IX_OperationTheatres_SurgeryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTheatres_SurgeryTypes_SurgeryTypeId",
                table: "OperationTheatres",
                column: "SurgeryTypeId",
                principalTable: "SurgeryTypes",
                principalColumn: "Id");
        }
    }
}
