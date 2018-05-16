using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usercenter_role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    NameEn = table.Column<string>(maxLength: 40, nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usercenter_user",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Account = table.Column<string>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Factor = table.Column<int>(nullable: true),
                    HeadImage = table.Column<string>(maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    NickName = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    RealName = table.Column<string>(maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    Sex = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usercenter_user_role",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user_role", x => new { x.UserId, x.RoleId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usercenter_role");

            migrationBuilder.DropTable(
                name: "usercenter_user");

            migrationBuilder.DropTable(
                name: "usercenter_user_role");
        }
    }
}
