using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20210614_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicNo",
                table: "Clothing");

            migrationBuilder.AddColumn<int>(
                name: "PackageTypeId",
                table: "Clothing",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClothingPackageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingPackageTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingPackageTypes");

            migrationBuilder.DropColumn(
                name: "PackageTypeId",
                table: "Clothing");

            migrationBuilder.AddColumn<int>(
                name: "PicNo",
                table: "Clothing",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
