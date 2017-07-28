using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_action_log",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    ActionName = table.Column<string>(maxLength: 30, nullable: true),
                    ControllerName = table.Column<string>(maxLength: 60, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Ip = table.Column<string>(maxLength: 46, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: true),
                    UrlReferrer = table.Column<string>(maxLength: 256, nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_action_log", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_action_log");
        }
    }
}
