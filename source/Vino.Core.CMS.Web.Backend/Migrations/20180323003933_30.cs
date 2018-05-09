using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "mall_product_sku");

            migrationBuilder.AddColumn<int>(
                name: "GainPoints",
                table: "mall_product_sku",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "PointsGainRule",
                table: "mall_product_sku",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "mall_product_sku",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GainPoints",
                table: "mall_product_sku");

            migrationBuilder.DropColumn(
                name: "PointsGainRule",
                table: "mall_product_sku");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "mall_product_sku");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "mall_product_sku",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }
    }
}
