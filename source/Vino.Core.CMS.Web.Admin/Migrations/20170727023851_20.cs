using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "duration",
                table: "TimedTaskLogs",
                newName: "Duration");

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "TimedTaskLogs",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "TimedTaskLogs",
                newName: "duration");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "TimedTaskLogs");
        }
    }
}
