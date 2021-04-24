using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackathonApp.Migrations
{
    public partial class Addquantitytopurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Purchase",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ArticlePurchase",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ArticlePurchase");
        }
    }
}
