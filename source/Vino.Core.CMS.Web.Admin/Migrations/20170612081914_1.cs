using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_role_menu",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    MenuId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_role_menu", x => new { x.RoleId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_system_role_menu_system_menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "system_menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_system_role_menu_system_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "system_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_system_role_menu_MenuId",
                table: "system_role_menu",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_role_menu");
        }
    }
}
