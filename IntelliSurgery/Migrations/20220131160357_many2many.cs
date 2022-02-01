using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class many2many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TheatreTypes_SurgeryType_Theatres_SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes");

            migrationBuilder.DropTable(
                name: "SurgeryType_Theatres");

            migrationBuilder.DropIndex(
                name: "IX_TheatreTypes_SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes");

            migrationBuilder.DropColumn(
                name: "SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes");

            migrationBuilder.AddColumn<int>(
                name: "TheatreId",
                table: "WorkingBlocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SurgeryTypeTheatreType",
                columns: table => new
                {
                    SuitableTheatreTypesId = table.Column<int>(type: "int", nullable: false),
                    SurgeryTypesConductedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgeryTypeTheatreType", x => new { x.SuitableTheatreTypesId, x.SurgeryTypesConductedId });
                    table.ForeignKey(
                        name: "FK_SurgeryTypeTheatreType_SurgeryTypes_SurgeryTypesConductedId",
                        column: x => x.SurgeryTypesConductedId,
                        principalTable: "SurgeryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurgeryTypeTheatreType_TheatreTypes_SuitableTheatreTypesId",
                        column: x => x.SuitableTheatreTypesId,
                        principalTable: "TheatreTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingBlocks_TheatreId",
                table: "WorkingBlocks",
                column: "TheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryTypeTheatreType_SurgeryTypesConductedId",
                table: "SurgeryTypeTheatreType",
                column: "SurgeryTypesConductedId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_Theatres_TheatreId",
                table: "WorkingBlocks",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_Theatres_TheatreId",
                table: "WorkingBlocks");

            migrationBuilder.DropTable(
                name: "SurgeryTypeTheatreType");

            migrationBuilder.DropIndex(
                name: "IX_WorkingBlocks_TheatreId",
                table: "WorkingBlocks");

            migrationBuilder.DropColumn(
                name: "TheatreId",
                table: "WorkingBlocks");

            migrationBuilder.AddColumn<int>(
                name: "SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes",
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
                name: "IX_TheatreTypes_SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes",
                column: "SurgeryTypeSurgeryTheatreId");

            migrationBuilder.CreateIndex(
                name: "IX_SurgeryType_Theatres_SurgeryTypeId",
                table: "SurgeryType_Theatres",
                column: "SurgeryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TheatreTypes_SurgeryType_Theatres_SurgeryTypeSurgeryTheatreId",
                table: "TheatreTypes",
                column: "SurgeryTypeSurgeryTheatreId",
                principalTable: "SurgeryType_Theatres",
                principalColumn: "Id");
        }
    }
}
