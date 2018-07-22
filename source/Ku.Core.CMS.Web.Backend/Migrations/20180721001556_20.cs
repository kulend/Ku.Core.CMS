using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content_column_article_ref");
        }
    }
}
