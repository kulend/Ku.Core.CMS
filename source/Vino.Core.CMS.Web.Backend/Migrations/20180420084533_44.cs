using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "FK_membership_member_point_record_membership_member_point_MemberPointId",
                        column: x => x.MemberPointId,
                        principalTable: "membership_member_point",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_membership_member_point_record_system_user_OperatorId",
                        column: x => x.OperatorId,
                        principalTable: "system_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membership_member_point_record");

            migrationBuilder.DropTable(
                name: "membership_member_point");
        }
    }
}
