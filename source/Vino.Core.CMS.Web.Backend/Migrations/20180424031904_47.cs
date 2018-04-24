using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _47 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AppId",
                table: "datacenter_app_feedback",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_datacenter_app_feedback_AppId",
                table: "datacenter_app_feedback",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_datacenter_app_feedback_datacenter_app_AppId",
                table: "datacenter_app_feedback",
                column: "AppId",
                principalTable: "datacenter_app",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_datacenter_app_feedback_datacenter_app_AppId",
                table: "datacenter_app_feedback");

            migrationBuilder.DropIndex(
                name: "IX_datacenter_app_feedback_AppId",
                table: "datacenter_app_feedback");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "datacenter_app_feedback");
        }
    }
}
