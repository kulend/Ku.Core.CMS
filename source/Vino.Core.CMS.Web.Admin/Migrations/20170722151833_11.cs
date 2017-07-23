using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "system_menu");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "system_menu");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "system_menu");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "system_menu");

            migrationBuilder.AddColumn<string>(
                name: "AuthCode",
                table: "system_menu",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "system_menu",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "system_menu",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthCode",
                table: "system_menu");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "system_menu",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "system_menu",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "system_menu",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Type",
                table: "system_menu",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "system_menu",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "system_menu",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);
        }
    }
}
