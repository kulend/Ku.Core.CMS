using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionResult",
                table: "system_action_log",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Operation",
                table: "system_action_log",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "system_action_log",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionResult",
                table: "system_action_log");

            migrationBuilder.DropColumn(
                name: "Operation",
                table: "system_action_log");

            migrationBuilder.AlterColumn<string>(
                name: "ControllerName",
                table: "system_action_log",
                maxLength: 60,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}
