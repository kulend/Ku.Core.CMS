using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "mall_product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_mall_product_CategoryId",
                table: "mall_product",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_mall_product_mall_product_category_CategoryId",
                table: "mall_product",
                column: "CategoryId",
                principalTable: "mall_product_category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_mall_product_mall_product_category_CategoryId",
                table: "mall_product");

            migrationBuilder.DropIndex(
                name: "IX_mall_product_CategoryId",
                table: "mall_product");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "mall_product");
        }
    }
}
