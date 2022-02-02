using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelliSurgery.Migrations
{
    public partial class workblocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_Surgeons_SurgeonId",
                table: "WorkingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_Theatres_TheatreId",
                table: "WorkingBlocks");

            migrationBuilder.AlterColumn<int>(
                name: "TheatreId",
                table: "WorkingBlocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SurgeonId",
                table: "WorkingBlocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_Surgeons_SurgeonId",
                table: "WorkingBlocks",
                column: "SurgeonId",
                principalTable: "Surgeons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_Theatres_TheatreId",
                table: "WorkingBlocks",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_Surgeons_SurgeonId",
                table: "WorkingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingBlocks_Theatres_TheatreId",
                table: "WorkingBlocks");

            migrationBuilder.AlterColumn<int>(
                name: "TheatreId",
                table: "WorkingBlocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SurgeonId",
                table: "WorkingBlocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_Surgeons_SurgeonId",
                table: "WorkingBlocks",
                column: "SurgeonId",
                principalTable: "Surgeons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingBlocks_Theatres_TheatreId",
                table: "WorkingBlocks",
                column: "TheatreId",
                principalTable: "Theatres",
                principalColumn: "Id");
        }
    }
}
