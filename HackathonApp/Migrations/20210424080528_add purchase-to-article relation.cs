using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackathonApp.Migrations
{
    public partial class addpurchasetoarticlerelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_Purchase_PurchaseId",
                table: "Article");

            migrationBuilder.DropIndex(
                name: "IX_Article_PurchaseId",
                table: "Article");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Article");

            migrationBuilder.CreateTable(
                name: "ArticlePurchase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: true),
                    PurchaseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticlePurchase_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ArticlePurchase_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePurchase_ArticleId",
                table: "ArticlePurchase",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePurchase_PurchaseId",
                table: "ArticlePurchase",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlePurchase");

            migrationBuilder.AddColumn<Guid>(
                name: "PurchaseId",
                table: "Article",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Article_PurchaseId",
                table: "Article",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_Purchase_PurchaseId",
                table: "Article",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
