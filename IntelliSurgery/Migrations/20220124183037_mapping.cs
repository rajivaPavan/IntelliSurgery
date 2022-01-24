using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class mapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SurgeryTypeSurgeryTheatreId",
                table: "OperationTheatres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SurgeryType_Theatres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SurgeryTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryType_Theatres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SurgeryType_Theatres_SurgeryTypes_SurgeryTypeId",
                        column: x => x.SurgeryTypeId,
                        principalTable: "SurgeryTypes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OperationTheatres_SurgeryTypeSurgeryTheatreId",
                table: "OperationTheatres",
                column: "SurgeryTypeSurgeryTheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryType_Theatres_SurgeryTypeId",
                table: "SurgeryType_Theatres",
                column: "SurgeryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OperationTheatres_SurgeryType_Theatres_SurgeryTypeSurgeryThe~",
                table: "OperationTheatres",
                column: "SurgeryTypeSurgeryTheatreId",
                principalTable: "SurgeryType_Theatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OperationTheatres_SurgeryType_Theatres_SurgeryTypeSurgeryThe~",
                table: "OperationTheatres");

            migrationBuilder.DropTable(
                name: "SurgeryType_Theatres");

            migrationBuilder.DropIndex(
                name: "IX_OperationTheatres_SurgeryTypeSurgeryTheatreId",
                table: "OperationTheatres");

            migrationBuilder.DropColumn(
                name: "SurgeryTypeSurgeryTheatreId",
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
    }
}
