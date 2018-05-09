using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SmsAccountId",
                table: "system_sms_templet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_system_sms_templet_SmsAccountId",
                table: "system_sms_templet",
                column: "SmsAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_system_sms_templet_system_sms_account_SmsAccountId",
                table: "system_sms_templet",
                column: "SmsAccountId",
                principalTable: "system_sms_account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_system_sms_templet_system_sms_account_SmsAccountId",
                table: "system_sms_templet");

            migrationBuilder.DropIndex(
                name: "IX_system_sms_templet_SmsAccountId",
                table: "system_sms_templet");

            migrationBuilder.DropColumn(
                name: "SmsAccountId",
                table: "system_sms_templet");
        }
    }
}
