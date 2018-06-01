using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usercenter_user_address",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Consignee = table.Column<string>(maxLength: 20, nullable: false),
                    Mobile = table.Column<string>(maxLength: 20, nullable: false),
                    ProvinceCode = table.Column<string>(maxLength: 20, nullable: true),
                    Province = table.Column<string>(maxLength: 40, nullable: true),
                    CityCode = table.Column<string>(maxLength: 20, nullable: true),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    RegionCode = table.Column<string>(maxLength: 20, nullable: true),
                    Region = table.Column<string>(maxLength: 40, nullable: true),
                    StreetCode = table.Column<string>(maxLength: 20, nullable: true),
                    Street = table.Column<string>(maxLength: 40, nullable: true),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user_address", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usercenter_user_address");
        }
    }
}
