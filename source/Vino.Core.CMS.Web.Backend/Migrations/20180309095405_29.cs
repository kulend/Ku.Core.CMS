using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveTime",
                table: "mall_product",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireTime",
                table: "mall_product",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSnapshot",
                table: "mall_product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OriginId",
                table: "mall_product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SnapshotCount",
                table: "mall_product",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EffectiveTime",
                table: "mall_product");

            migrationBuilder.DropColumn(
                name: "ExpireTime",
                table: "mall_product");

            migrationBuilder.DropColumn(
                name: "IsSnapshot",
                table: "mall_product");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "mall_product");

            migrationBuilder.DropColumn(
                name: "SnapshotCount",
                table: "mall_product");
        }
    }
}
