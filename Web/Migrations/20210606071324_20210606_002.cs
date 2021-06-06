using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class _20210606_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_Member_MemberId",
                table: "Clothing");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Clothing",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Clothing",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Action",
                table: "Clothing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Clothing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Clothing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountAmount",
                table: "Clothing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDt",
                table: "Clothing",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Clothing",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Clothing",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_Member_MemberId",
                table: "Clothing",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothing_Member_MemberId",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "PickupDt",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Clothing");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Clothing");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clothing",
                newName: "ID");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Clothing",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Clothing_Member_MemberId",
                table: "Clothing",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
