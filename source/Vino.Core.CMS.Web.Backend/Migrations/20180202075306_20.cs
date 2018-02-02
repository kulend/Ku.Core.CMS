using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sxe",
                table: "wechat_user");

            migrationBuilder.AddColumn<short>(
                name: "Sex",
                table: "wechat_user",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sex",
                table: "wechat_user");

            migrationBuilder.AddColumn<string>(
                name: "Sxe",
                table: "wechat_user",
                nullable: true);
        }
    }
}
