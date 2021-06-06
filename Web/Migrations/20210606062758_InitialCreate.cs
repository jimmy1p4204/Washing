using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_Member_MemberID",
                table: "Clothing");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Member",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "MemberID",
                table: "Clothing",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Clothing_MemberID",
                table: "Clothing",
                newName: "IX_Clothing_MemberId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Member",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineId",
                table: "Member",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Member",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UniformNo",
                table: "Member",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cst",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UniformNo = table.Column<string>(nullable: true),
                    C1 = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    C2 = table.Column<string>(nullable: true),
                    C3 = table.Column<string>(nullable: true),
                    C4 = table.Column<string>(nullable: true),
                    Date1 = table.Column<string>(nullable: true),
                    DateTime1 = table.Column<string>(nullable: true),
                    C5 = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    C6 = table.Column<string>(nullable: true),
                    ShopName = table.Column<string>(nullable: true),
                    C7 = table.Column<string>(nullable: true),
                    PhoneName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    C8 = table.Column<string>(nullable: true),
                    C9 = table.Column<string>(nullable: true),
                    C10 = table.Column<string>(nullable: true),
                    C11 = table.Column<string>(nullable: true),
                    C12 = table.Column<string>(nullable: true),
                    C13 = table.Column<string>(nullable: true),
                    C14 = table.Column<string>(nullable: true),
                    C15 = table.Column<string>(nullable: true),
                    C16 = table.Column<string>(nullable: true),
                    C17 = table.Column<string>(nullable: true),
                    C18 = table.Column<string>(nullable: true),
                    DateTime2 = table.Column<string>(nullable: true),
                    Date2 = table.Column<string>(nullable: true),
                    C19 = table.Column<string>(nullable: true),
                    Date3 = table.Column<string>(nullable: true),
                    C20 = table.Column<string>(nullable: true),
                    C21 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cst", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wid",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    C1 = table.Column<string>(nullable: true),
                    C2 = table.Column<string>(nullable: true),
                    MemberNo = table.Column<string>(nullable: true),
                    MemberName = table.Column<string>(nullable: true),
                    CommodityNo = table.Column<string>(nullable: true),
                    CommodityName = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    MethodNo = table.Column<string>(nullable: true),
                    MethodName = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    C3 = table.Column<string>(nullable: true),
                    C4 = table.Column<string>(nullable: true),
                    DateTime = table.Column<string>(nullable: true),
                    DateTime2 = table.Column<string>(nullable: true),
                    DateTime3 = table.Column<string>(nullable: true),
                    DateTime4 = table.Column<string>(nullable: true),
                    C5 = table.Column<string>(nullable: true),
                    StaffName = table.Column<string>(nullable: true),
                    C6 = table.Column<string>(nullable: true),
                    StaffName2 = table.Column<string>(nullable: true),
                    C7 = table.Column<string>(nullable: true),
                    StaffName3 = table.Column<string>(nullable: true),
                    C8 = table.Column<string>(nullable: true),
                    C9 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wid", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_Member_MemberId",
                table: "Clothing",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_Member_MemberId",
                table: "Clothing");

            migrationBuilder.DropTable(
                name: "Cst");

            migrationBuilder.DropTable(
                name: "Wid");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "LineId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "UniformNo",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Member",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Clothing",
                newName: "MemberID");

            migrationBuilder.RenameIndex(
                name: "IX_Clothing_MemberId",
                table: "Clothing",
                newName: "IX_Clothing_MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_Member_MemberID",
                table: "Clothing",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
