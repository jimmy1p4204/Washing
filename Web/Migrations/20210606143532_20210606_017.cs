using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20210606_017 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothingColor",
                table: "ClothingColor");

            migrationBuilder.RenameTable(
                name: "ClothingColor",
                newName: "ClothingColors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothingColors",
                table: "ClothingColors",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClothingColors",
                table: "ClothingColors");

            migrationBuilder.RenameTable(
                name: "ClothingColors",
                newName: "ClothingColor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClothingColor",
                table: "ClothingColor",
                column: "Id");
        }
    }
}
