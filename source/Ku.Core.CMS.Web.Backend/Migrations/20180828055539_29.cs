using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datacenter_pageview_record",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    SessionId = table.Column<string>(maxLength: 32, nullable: false),
                    PageName = table.Column<string>(maxLength: 32, nullable: true),
                    Url = table.Column<string>(maxLength: 256, nullable: false),
                    Referer = table.Column<string>(maxLength: 256, nullable: true),
                    UserAgent = table.Column<string>(maxLength: 128, nullable: true),
                    ClientIp = table.Column<string>(maxLength: 64, nullable: true),
                    Location = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datacenter_pageview_record", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datacenter_pageview_record");
        }
    }
}
