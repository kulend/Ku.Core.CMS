using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wechat_account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AppId = table.Column<string>(maxLength: 32, nullable: true),
                    AppSecret = table.Column<string>(maxLength: 512, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    OriginalId = table.Column<string>(maxLength: 50, nullable: true),
                    Token = table.Column<string>(maxLength: 30, nullable: true),
                    Type = table.Column<short>(nullable: false),
                    WeixinId = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wechat_account", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wechat_account");
        }
    }
}
