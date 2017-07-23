using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_system_function_module_system_function_module_ParentId",
                table: "system_function_module");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "system_function_module",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_system_function_module_system_function_module_ParentId",
                table: "system_function_module",
                column: "ParentId",
                principalTable: "system_function_module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_system_function_module_system_function_module_ParentId",
                table: "system_function_module");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "system_function_module",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_system_function_module_system_function_module_ParentId",
                table: "system_function_module",
                column: "ParentId",
                principalTable: "system_function_module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
