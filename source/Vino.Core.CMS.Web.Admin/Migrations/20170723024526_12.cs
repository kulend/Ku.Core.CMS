using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_role_function",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    FunctionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_role_function", x => new { x.RoleId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_system_role_function_system_function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "system_function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_system_role_function_system_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "system_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_system_role_function_FunctionId",
                table: "system_role_function",
                column: "FunctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_role_function");
        }
    }
}
