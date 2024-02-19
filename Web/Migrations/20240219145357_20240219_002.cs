using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20240219_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CashCheckout");

            migrationBuilder.AddColumn<decimal>(
                name: "MachineAmount",
                table: "CashCheckout",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SelfWashAmount",
                table: "CashCheckout",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachineAmount",
                table: "CashCheckout");

            migrationBuilder.DropColumn(
                name: "SelfWashAmount",
                table: "CashCheckout");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "CashCheckout",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
