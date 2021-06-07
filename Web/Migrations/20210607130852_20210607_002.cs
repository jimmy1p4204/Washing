using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20210607_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPickup",
                table: "Clothing",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPickup",
                table: "Clothing");
        }
    }
}
