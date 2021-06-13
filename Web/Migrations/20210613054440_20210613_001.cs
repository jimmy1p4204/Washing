using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20210613_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Act = table.Column<string>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    ClothingSeq = table.Column<int>(nullable: true),
                    Employee = table.Column<string>(nullable: true),
                    LogDt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
