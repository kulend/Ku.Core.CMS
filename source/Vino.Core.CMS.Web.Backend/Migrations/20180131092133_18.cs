using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wechat_user",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AccountId = table.Column<long>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    HeadImgUrl = table.Column<string>(maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Language = table.Column<string>(maxLength: 20, nullable: true),
                    NickName = table.Column<string>(maxLength: 100, nullable: true),
                    OpenId = table.Column<string>(maxLength: 64, nullable: false),
                    Province = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 30, nullable: true),
                    SubscribeTime = table.Column<DateTime>(nullable: true),
                    Sxe = table.Column<string>(nullable: true),
                    UnionId = table.Column<string>(maxLength: 64, nullable: true),
                    UserTags = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wechat_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wechat_user_wechat_account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "wechat_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wechat_user_AccountId",
                table: "wechat_user",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wechat_user");
        }
    }
}
