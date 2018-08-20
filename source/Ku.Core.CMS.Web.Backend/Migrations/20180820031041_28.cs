using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "materialcenter_user_material_group",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Type = table.Column<short>(nullable: false),
                    Name = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materialcenter_user_material_group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_materialcenter_user_material_group_usercenter_user_UserId",
                        column: x => x.UserId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "materialcenter_user_material_group_ref",
                columns: table => new
                {
                    GroupId = table.Column<long>(nullable: false),
                    MaterialId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materialcenter_user_material_group_ref", x => new { x.GroupId, x.MaterialId });
                    table.ForeignKey(
                        name: "FK_materialcenter_user_material_group_ref_materialcenter_user_m~",
                        column: x => x.GroupId,
                        principalTable: "materialcenter_user_material_group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_materialcenter_user_material_group_UserId",
                table: "materialcenter_user_material_group",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "materialcenter_user_material_group_ref");

            migrationBuilder.DropTable(
                name: "materialcenter_user_material_group");
        }
    }
}
