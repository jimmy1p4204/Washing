using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20221106_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineCashs",
                columns: table => new
                {
                    Dt = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    CreateDt = table.Column<DateTime>(nullable: false),
                    UpdateDt = table.Column<DateTime>(nullable: false),
                    UpdateBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineCashs", x => x.Dt);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineCashs");
        }
    }
}
