using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationTheatreType",
                table: "TheatreTypes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TheatreTypes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TheatreTypes");

            migrationBuilder.AddColumn<int>(
                name: "OperationTheatreType",
                table: "TheatreTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
