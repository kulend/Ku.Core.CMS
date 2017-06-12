using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_menu",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Icon = table.Column<string>(maxLength: 20, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    ParentId = table.Column<long>(nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<short>(nullable: false),
                    Url = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_user",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Account = table.Column<string>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LastLoginIp = table.Column<string>(maxLength: 40, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_user", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_menu");

            migrationBuilder.DropTable(
                name: "system_role");

            migrationBuilder.DropTable(
                name: "system_user");
        }
    }
}
