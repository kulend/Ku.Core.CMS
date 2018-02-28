using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mall_product",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    ContentType = table.Column<short>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ImageData = table.Column<string>(maxLength: 3000, nullable: true),
                    Intro = table.Column<string>(maxLength: 512, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    PriceRange = table.Column<string>(maxLength: 32, nullable: true),
                    Properties = table.Column<string>(maxLength: 2000, nullable: true),
                    Sales = table.Column<int>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Visits = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall_product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mall_product_sku",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CoverImage = table.Column<string>(maxLength: 500, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MarketPrice = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    Sales = table.Column<int>(nullable: false),
                    Stock = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall_product_sku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mall_product_sku_mall_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "mall_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mall_product_sku_ProductId",
                table: "mall_product_sku",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mall_product_sku");

            migrationBuilder.DropTable(
                name: "mall_product");
        }
    }
}
