using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class moreM2Mrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialitySurgeryType",
                columns: table => new
                {
                    SuitableSpecialistsId = table.Column<int>(type: "int", nullable: false),
                    SurgeryTypesPerformedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialitySurgeryType", x => new { x.SuitableSpecialistsId, x.SurgeryTypesPerformedId });
                    table.ForeignKey(
                        name: "FK_SpecialitySurgeryType_Specialities_SuitableSpecialistsId",
                        column: x => x.SuitableSpecialistsId,
                        principalTable: "Specialities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialitySurgeryType_SurgeryTypes_SurgeryTypesPerformedId",
                        column: x => x.SurgeryTypesPerformedId,
                        principalTable: "SurgeryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialitySurgeryType_SurgeryTypesPerformedId",
                table: "SpecialitySurgeryType",
                column: "SurgeryTypesPerformedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialitySurgeryType");
        }
    }
}
