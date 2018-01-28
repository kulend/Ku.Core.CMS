using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wechat_menu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsIndividuation = table.Column<bool>(nullable: false),
                    MenuData = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    WxMenuId = table.Column<string>(maxLength: 32, nullable: true),
                    MatchRule_City = table.Column<string>(maxLength: 40, nullable: true),
                    MatchRule_Client = table.Column<string>(maxLength: 1, nullable: true),
                    MatchRule_Country = table.Column<string>(maxLength: 40, nullable: true),
                    MatchRule_Language = table.Column<string>(maxLength: 10, nullable: true),
                    MatchRule_Province = table.Column<string>(maxLength: 40, nullable: true),
                    MatchRule_Sex = table.Column<string>(maxLength: 1, nullable: true),
                    MatchRule_TagId = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wechat_menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wechat_menu_wechat_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "wechat_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wechat_menu_AccountId",
                table: "wechat_menu",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wechat_menu");
        }
    }
}
