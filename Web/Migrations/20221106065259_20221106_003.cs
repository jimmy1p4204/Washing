using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20221106_003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineCashs",
                table: "MachineCashs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MachineCashs",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineCashs",
                table: "MachineCashs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MachineCashs",
                table: "MachineCashs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MachineCashs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MachineCashs",
                table: "MachineCashs",
                column: "Dt");
        }
    }
}
