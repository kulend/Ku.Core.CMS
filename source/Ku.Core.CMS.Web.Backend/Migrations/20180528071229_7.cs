using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "material_picture");

            migrationBuilder.DropTable(
                name: "material_group");

            migrationBuilder.CreateTable(
                name: "materialcenter_picture",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(maxLength: 128, nullable: true),
                    FilePath = table.Column<string>(maxLength: 256, nullable: true),
                    FileSize = table.Column<long>(nullable: false),
                    FileType = table.Column<string>(maxLength: 20, nullable: true),
                    Folder = table.Column<string>(maxLength: 256, nullable: true),
                    IsCompressed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Md5Code = table.Column<string>(maxLength: 32, nullable: true),
                    ThumbPath = table.Column<string>(maxLength: 256, nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    UploadUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materialcenter_picture", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materialcenter_picture");

            migrationBuilder.CreateTable(
                name: "material_group",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Type = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "material_picture",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(maxLength: 128, nullable: true),
                    FilePath = table.Column<string>(maxLength: 256, nullable: true),
                    FileSize = table.Column<long>(nullable: false),
                    FileType = table.Column<string>(maxLength: 20, nullable: true),
                    Folder = table.Column<string>(maxLength: 256, nullable: true),
                    GroupId = table.Column<long>(nullable: true),
                    IsCompressed = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsPublic = table.Column<bool>(nullable: false),
                    Md5Code = table.Column<string>(maxLength: 32, nullable: true),
                    ThumbPath = table.Column<string>(maxLength: 256, nullable: true),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    UploadUserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material_picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_material_picture_material_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "material_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_material_picture_GroupId",
                table: "material_picture",
                column: "GroupId");
        }
    }
}
