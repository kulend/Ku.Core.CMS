using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membership_member_address");

            migrationBuilder.DropTable(
                name: "membership_member_point_record");

            migrationBuilder.DropTable(
                name: "membership_member_point");

            migrationBuilder.DropTable(
                name: "membership_member");

            migrationBuilder.DropTable(
                name: "membership_member_type");

            migrationBuilder.CreateTable(
                name: "mall_brand",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Logo = table.Column<string>(maxLength: 256, nullable: true),
                    Intro = table.Column<string>(maxLength: 1024, nullable: true),
                    IsEnable = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mall_brand", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_address_UserId",
                table: "usercenter_user_address",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_usercenter_user_address_usercenter_user_UserId",
                table: "usercenter_user_address",
                column: "UserId",
                principalTable: "usercenter_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usercenter_user_address_usercenter_user_UserId",
                table: "usercenter_user_address");

            migrationBuilder.DropTable(
                name: "mall_brand");

            migrationBuilder.DropIndex(
                name: "IX_usercenter_user_address_UserId",
                table: "usercenter_user_address");

            migrationBuilder.CreateTable(
                name: "membership_member_type",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "membership_member",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Factor = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LastLoginIp = table.Column<string>(maxLength: 40, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    MemberTypeId = table.Column<long>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Sex = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membership_member_membership_member_type_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalTable: "membership_member_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "membership_member_point",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ExpiredPoints = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemberId = table.Column<long>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Type = table.Column<short>(nullable: false),
                    UsablePoints = table.Column<int>(nullable: false),
                    UsedPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member_point", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membership_member_point_membership_member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "membership_member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "membership_member_point_record",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    BusDesc = table.Column<string>(maxLength: 500, nullable: true),
                    BusId = table.Column<long>(nullable: false),
                    BusType = table.Column<short>(nullable: false),
                    ChangeType = table.Column<short>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MemberId = table.Column<long>(nullable: false),
                    MemberPointId = table.Column<long>(nullable: false),
                    OperatorId = table.Column<long>(nullable: true),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member_point_record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membership_member_point_record_membership_member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "membership_member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_membership_member_point_record_membership_member_point_Membe~",
                        column: x => x.MemberPointId,
                        principalTable: "membership_member_point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_membership_member_point_record_usercenter_user_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_MemberTypeId",
                table: "membership_member",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_address_MemberID",
                table: "membership_member_address",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_point_MemberId",
                table: "membership_member_point",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_point_record_MemberId",
                table: "membership_member_point_record",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_point_record_MemberPointId",
                table: "membership_member_point_record",
                column: "MemberPointId");

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_point_record_OperatorId",
                table: "membership_member_point_record",
                column: "OperatorId");
        }
    }
}
