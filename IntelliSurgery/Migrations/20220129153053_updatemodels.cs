using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class updatemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TheatreNumber",
                table: "Theatres");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Theatres",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Theatres");

            migrationBuilder.AddColumn<int>(
                name: "TheatreNumber",
                table: "Theatres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
