using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "material_picture",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "material_picture",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MaterialGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialGroups_system_user_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "system_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_material_picture_GroupId",
                table: "material_picture",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialGroups_CreateUserId",
                table: "MaterialGroups",
                column: "CreateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_material_picture_MaterialGroups_GroupId",
                table: "material_picture",
                column: "GroupId",
                principalTable: "MaterialGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_material_picture_MaterialGroups_GroupId",
                table: "material_picture");

            migrationBuilder.DropTable(
                name: "MaterialGroups");

            migrationBuilder.DropIndex(
                name: "IX_material_picture_GroupId",
                table: "material_picture");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "material_picture");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "material_picture");
        }
    }
}
