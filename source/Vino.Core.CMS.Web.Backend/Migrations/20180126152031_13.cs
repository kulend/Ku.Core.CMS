using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_material_picture_MaterialGroups_GroupId",
                table: "material_picture");

            migrationBuilder.DropForeignKey(
                name: "FK_MaterialGroups_system_user_CreateUserId",
                table: "MaterialGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaterialGroups",
                table: "MaterialGroups");

            migrationBuilder.RenameTable(
                name: "MaterialGroups",
                newName: "material_group");

            migrationBuilder.RenameIndex(
                name: "IX_MaterialGroups_CreateUserId",
                table: "material_group",
                newName: "IX_material_group_CreateUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_material_group",
                table: "material_group",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_material_group_system_user_CreateUserId",
                table: "material_group",
                column: "CreateUserId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_material_picture_material_group_GroupId",
                table: "material_picture",
                column: "GroupId",
                principalTable: "material_group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_material_group_system_user_CreateUserId",
                table: "material_group");

            migrationBuilder.DropForeignKey(
                name: "FK_material_picture_material_group_GroupId",
                table: "material_picture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_material_group",
                table: "material_group");

            migrationBuilder.RenameTable(
                name: "material_group",
                newName: "MaterialGroups");

            migrationBuilder.RenameIndex(
                name: "IX_material_group_CreateUserId",
                table: "MaterialGroups",
                newName: "IX_MaterialGroups_CreateUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaterialGroups",
                table: "MaterialGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_material_picture_MaterialGroups_GroupId",
                table: "material_picture",
                column: "GroupId",
                principalTable: "MaterialGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaterialGroups_system_user_CreateUserId",
                table: "MaterialGroups",
                column: "CreateUserId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
