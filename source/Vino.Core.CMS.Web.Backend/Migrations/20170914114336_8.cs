using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Md5Code",
                table: "material_picture",
                type: "varchar(32)",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Md5Code",
                table: "material_picture",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)",
                oldMaxLength: 32,
                oldNullable: true);
        }
    }
}
