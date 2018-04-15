using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datacenter_app",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AppKey = table.Column<string>(maxLength: 32, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DownloadUrl = table.Column<string>(maxLength: 256, nullable: true),
                    IconData = table.Column<string>(maxLength: 512, nullable: true),
                    Intro = table.Column<string>(maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Type = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datacenter_app", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datacenter_app");
        }
    }
}
