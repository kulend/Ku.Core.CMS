using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usercenter_user_point_record",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserPointId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    ChangeType = table.Column<short>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    BusType = table.Column<short>(nullable: false),
                    BusId = table.Column<long>(nullable: false),
                    BusDesc = table.Column<string>(maxLength: 500, nullable: true),
                    OperatorId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user_point_record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usercenter_user_point_record_usercenter_user_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_usercenter_user_point_record_usercenter_user_UserId",
                        column: x => x.UserId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usercenter_user_point_record_usercenter_user_point_UserPoint~",
                        column: x => x.UserPointId,
                        principalTable: "usercenter_user_point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_point_record_OperatorId",
                table: "usercenter_user_point_record",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_point_record_UserId",
                table: "usercenter_user_point_record",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_point_record_UserPointId",
                table: "usercenter_user_point_record",
                column: "UserPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usercenter_user_point_record");
        }
    }
}
