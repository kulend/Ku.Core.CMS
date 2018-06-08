using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class demo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "demo1",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Type = table.Column<short>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    IntValue = table.Column<int>(nullable: false),
                    ShortValue = table.Column<short>(nullable: true),
                    DecimalValue = table.Column<decimal>(nullable: true),
                    FloatValue = table.Column<float>(nullable: false),
                    BoolValue = table.Column<bool>(nullable: false),
                    DoubleValue = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_demo1", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_communication_sms_templet_communication_sms_account_SmsAccou~",
                table: "communication_sms_templet",
                column: "SmsAccountId",
                principalTable: "communication_sms_account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_membership_member_point_record_membership_member_point_Membe~",
                table: "membership_member_point_record",
                column: "MemberPointId",
                principalTable: "membership_member_point",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_communication_sms_templet_communication_sms_account_SmsAccou~",
                table: "communication_sms_templet");

            migrationBuilder.DropForeignKey(
                name: "FK_membership_member_point_record_membership_member_point_Membe~",
                table: "membership_member_point_record");

            migrationBuilder.DropTable(
                name: "demo1");

            migrationBuilder.AddForeignKey(
                name: "FK_communication_sms_templet_communication_sms_account_SmsAccountId",
                table: "communication_sms_templet",
                column: "SmsAccountId",
                principalTable: "communication_sms_account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_membership_member_point_record_membership_member_point_MemberPointId",
                table: "membership_member_point_record",
                column: "MemberPointId",
                principalTable: "membership_member_point",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
