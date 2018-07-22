using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content_column_article_ref");

            migrationBuilder.AddColumn<long>(
                name: "ColumnId",
                table: "content_article",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_content_article_ColumnId",
                table: "content_article",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_content_article_content_column_ColumnId",
                table: "content_article",
                column: "ColumnId",
                principalTable: "content_column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_content_article_content_column_ColumnId",
                table: "content_article");

            migrationBuilder.DropIndex(
                name: "IX_content_article_ColumnId",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "ColumnId",
                table: "content_article");

            migrationBuilder.CreateTable(
                name: "content_column_article_ref",
                columns: table => new
                {
                    ColumnId = table.Column<long>(nullable: false),
                    ArticleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_column_article_ref", x => new { x.ColumnId, x.ArticleId });
                    table.ForeignKey(
                        name: "FK_content_column_article_ref_content_article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "content_article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_content_column_article_ref_content_column_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "content_column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_content_column_article_ref_ArticleId",
                table: "content_column_article_ref",
                column: "ArticleId");
        }
    }
}
