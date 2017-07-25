using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "timed_task",
                newName: "TimedTasks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TimedTasks",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "TimedTasks",
                newName: "timed_task");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "timed_task");
        }
    }
}
