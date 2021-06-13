using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20210613_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClothingSeq",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "ClothingId",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClothingId",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "ClothingSeq",
                table: "Logs",
                type: "int",
                nullable: true);
        }
    }
}
