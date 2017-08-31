using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "material_picture",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FileName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    FilePath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    FileType = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Md5Code = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    ThumbPath = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Title = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    UploadUserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_material_picture_system_user_UploadUserId",
                        column: x => x.UploadUserId,
                        principalTable: "system_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_system_menu_ParentId",
                table: "system_menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_material_picture_UploadUserId",
                table: "material_picture",
                column: "UploadUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_system_menu_system_menu_ParentId",
                table: "system_menu",
                column: "ParentId",
                principalTable: "system_menu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_system_menu_system_menu_ParentId",
                table: "system_menu");

            migrationBuilder.DropTable(
                name: "material_picture");

            migrationBuilder.DropIndex(
                name: "IX_system_menu_ParentId",
                table: "system_menu");
        }
    }
}
