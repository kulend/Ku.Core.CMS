using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usercenter_role_function",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    FunctionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_role_function", x => new { x.RoleId, x.FunctionId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usercenter_role_function");
        }
    }
}
