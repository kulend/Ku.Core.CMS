using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usercenter_user_point",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Type = table.Column<short>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    UsablePoints = table.Column<int>(nullable: false),
                    ExpiredPoints = table.Column<int>(nullable: false),
                    UsedPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user_point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usercenter_user_point_usercenter_user_UserId",
                        column: x => x.UserId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_point_UserId",
                table: "usercenter_user_point",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usercenter_user_point");
        }
    }
}
