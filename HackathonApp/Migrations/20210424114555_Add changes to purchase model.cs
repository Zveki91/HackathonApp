using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HackathonApp.Migrations
{
    public partial class Addchangestopurchasemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyService");

            migrationBuilder.AddColumn<int>(
                name: "TokenAmount",
                table: "Purchase",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                table: "Discount",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BranchArticle",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: true),
                    ArticleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchArticle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BranchArticle_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchArticle_Branch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BranchId",
                table: "Discount",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchArticle_ArticleId",
                table: "BranchArticle",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchArticle_BranchId",
                table: "BranchArticle",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Branch_BranchId",
                table: "Discount",
                column: "BranchId",
                principalTable: "Branch",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Branch_BranchId",
                table: "Discount");

            migrationBuilder.DropTable(
                name: "BranchArticle");

            migrationBuilder.DropIndex(
                name: "IX_Discount_BranchId",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "TokenAmount",
                table: "Purchase");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Discount");

            migrationBuilder.CreateTable(
                name: "CompanyService",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyService_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyService_CompanyId",
                table: "CompanyService",
                column: "CompanyId");
        }
    }
}
