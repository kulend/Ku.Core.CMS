using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_function_module_action");

            migrationBuilder.DropTable(
                name: "system_function_module");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_function_module",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    HasCode = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(maxLength: 20, nullable: true),
                    IsLeaf = table.Column<bool>(nullable: false),
                    IsMenu = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_function_module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_function_module_system_function_module_ParentId",
                        column: x => x.ParentId,
                        principalTable: "system_function_module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "system_function_module_action",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModuleId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_function_module_action", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_function_module_action_system_function_module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "system_function_module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_system_function_module_ParentId",
                table: "system_function_module",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_system_function_module_action_ModuleId",
                table: "system_function_module_action",
                column: "ModuleId");
        }
    }
}
