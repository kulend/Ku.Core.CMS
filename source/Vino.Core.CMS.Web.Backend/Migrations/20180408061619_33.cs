using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "system_sms");

            migrationBuilder.AddColumn<int>(
                name: "ExpireMinute",
                table: "system_sms_templet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "system_sms_queue",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SmsTempletId",
                table: "system_sms",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_system_sms_SmsTempletId",
                table: "system_sms",
                column: "SmsTempletId");

            migrationBuilder.AddForeignKey(
                name: "FK_system_sms_system_sms_templet_SmsTempletId",
                table: "system_sms",
                column: "SmsTempletId",
                principalTable: "system_sms_templet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_system_sms_system_sms_templet_SmsTempletId",
                table: "system_sms");

            migrationBuilder.DropIndex(
                name: "IX_system_sms_SmsTempletId",
                table: "system_sms");

            migrationBuilder.DropColumn(
                name: "ExpireMinute",
                table: "system_sms_templet");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "system_sms_queue");

            migrationBuilder.DropColumn(
                name: "SmsTempletId",
                table: "system_sms");

            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "system_sms",
                maxLength: 40,
                nullable: true);
        }
    }
}
