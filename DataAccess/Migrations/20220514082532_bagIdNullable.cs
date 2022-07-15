using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class bagIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Bag_BagId",
                schema: "Package",
                table: "Package");

            migrationBuilder.AlterColumn<Guid>(
                name: "BagId",
                schema: "Package",
                table: "Package",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Bag_BagId",
                schema: "Package",
                table: "Package",
                column: "BagId",
                principalSchema: "Bag",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Package_Bag_BagId",
                schema: "Package",
                table: "Package");

            migrationBuilder.AlterColumn<Guid>(
                name: "BagId",
                schema: "Package",
                table: "Package",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Bag_BagId",
                schema: "Package",
                table: "Package",
                column: "BagId",
                principalSchema: "Bag",
                principalTable: "Bag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
