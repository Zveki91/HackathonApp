using Microsoft.EntityFrameworkCore.Migrations;

namespace HackathonApp.Migrations
{
    public partial class AddpricetopurchasedArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ArticlePurchase",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ArticlePurchase");
        }
    }
}
