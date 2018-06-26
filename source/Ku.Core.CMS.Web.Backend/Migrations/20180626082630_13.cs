using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mall_brand_category_ref",
                columns: table => new
                {
                    BrandId = table.Column<long>(nullable: false),
                    ProductCategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall_brand_category_ref", x => new { x.BrandId, x.ProductCategoryId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mall_brand_category_ref");
        }
    }
}
