using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wechat_qrcode",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<long>(nullable: true),
                    EventKey = table.Column<string>(maxLength: 64, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PeriodType = table.Column<short>(nullable: false),
                    Purpose = table.Column<string>(maxLength: 128, nullable: true),
                    ScanCount = table.Column<int>(nullable: false),
                    SceneId = table.Column<int>(nullable: false),
                    SceneType = table.Column<short>(nullable: false),
                    Ticket = table.Column<string>(maxLength: 256, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wechat_qrcode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wechat_qrcode_wechat_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "wechat_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wechat_qrcode_wechat_user_CreateUserId",
                        column: x => x.CreateUserId,
                        principalTable: "wechat_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wechat_qrcode_AccountId",
                table: "wechat_qrcode",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_wechat_qrcode_CreateUserId",
                table: "wechat_qrcode",
                column: "CreateUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wechat_qrcode");
        }
    }
}
