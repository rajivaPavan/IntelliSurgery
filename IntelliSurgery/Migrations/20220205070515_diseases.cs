using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class diseases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseasePatient_Disease_DiseasesId",
                table: "DiseasePatient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Disease",
                table: "Disease");

            migrationBuilder.RenameTable(
                name: "Disease",
                newName: "Diseases");

            migrationBuilder.RenameColumn(
                name: "ASA_Status",
                table: "Patients",
                newName: "AsaStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diseases",
                table: "Diseases",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasePatient_Diseases_DiseasesId",
                table: "DiseasePatient",
                column: "DiseasesId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiseasePatient_Diseases_DiseasesId",
                table: "DiseasePatient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diseases",
                table: "Diseases");

            migrationBuilder.RenameTable(
                name: "Diseases",
                newName: "Disease");

            migrationBuilder.RenameColumn(
                name: "AsaStatus",
                table: "Patients",
                newName: "ASA_Status");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Disease",
                table: "Disease",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DiseasePatient_Disease_DiseasesId",
                table: "DiseasePatient",
                column: "DiseasesId",
                principalTable: "Disease",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
