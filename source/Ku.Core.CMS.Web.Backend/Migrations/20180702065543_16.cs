using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mall_order",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Status = table.Column<short>(nullable: false),
                    UserId = table.Column<long>(nullable: true),
                    ProductAmount = table.Column<decimal>(nullable: false),
                    PayAmount = table.Column<decimal>(nullable: false),
                    DiscountAmount = table.Column<decimal>(nullable: false),
                    FreightAmount = table.Column<decimal>(nullable: false),
                    Consignee = table.Column<string>(maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(maxLength: 20, nullable: true),
                    AreaCode = table.Column<string>(maxLength: 20, nullable: true),
                    DeliveryAddress = table.Column<string>(nullable: true),
                    DeliveryTime = table.Column<DateTime>(nullable: true),
                    IsPaid = table.Column<bool>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: true),
                    ReceivedTime = table.Column<DateTime>(nullable: true),
                    RefundStatus = table.Column<short>(nullable: false),
                    RefundAmount = table.Column<decimal>(nullable: false),
                    RefundTime = table.Column<DateTime>(nullable: true),
                    UserRemark = table.Column<string>(maxLength: 256, nullable: true),
                    Remark = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mall_order_usercenter_user_UserId",
                        column: x => x.UserId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mall_order_product",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    ProductId = table.Column<long>(nullable: false),
                    ProductSkuId = table.Column<long>(nullable: false),
                    ProductPrice = table.Column<decimal>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    Remark = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall_order_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mall_order_product_mall_order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "mall_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mall_order_product_mall_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "mall_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mall_order_product_mall_product_sku_ProductSkuId",
                        column: x => x.ProductSkuId,
                        principalTable: "mall_product_sku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mall_order_UserId",
                table: "mall_order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_mall_order_product_OrderId",
                table: "mall_order_product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_mall_order_product_ProductId",
                table: "mall_order_product",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_mall_order_product_ProductSkuId",
                table: "mall_order_product",
                column: "ProductSkuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mall_order_product");

            migrationBuilder.DropTable(
                name: "mall_order");
        }
    }
}
