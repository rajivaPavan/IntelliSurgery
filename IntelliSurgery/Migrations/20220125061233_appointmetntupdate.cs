using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class appointmetntupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Priority",
                table: "Appointments",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Appointments");
        }
    }
}
