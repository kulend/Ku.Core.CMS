using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _52 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "membership_member_address",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Address = table.Column<string>(maxLength: 256, nullable: true),
                    City = table.Column<string>(maxLength: 40, nullable: true),
                    CityCode = table.Column<string>(maxLength: 20, nullable: true),
                    Consignee = table.Column<string>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemberID = table.Column<long>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 20, nullable: false),
                    Province = table.Column<string>(maxLength: 40, nullable: true),
                    ProvinceCode = table.Column<string>(maxLength: 20, nullable: true),
                    Region = table.Column<string>(maxLength: 40, nullable: true),
                    RegionCode = table.Column<string>(maxLength: 20, nullable: true),
                    Street = table.Column<string>(maxLength: 40, nullable: true),
                    StreetCode = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member_address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membership_member_address_membership_member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "membership_member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_address_MemberID",
                table: "membership_member_address",
                column: "MemberID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membership_member_address");
        }
    }
}
