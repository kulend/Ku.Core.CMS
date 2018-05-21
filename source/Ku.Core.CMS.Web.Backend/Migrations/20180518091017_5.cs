using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataCenter_AppVersion_datacenter_app_AppId",
                table: "DataCenter_AppVersion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DataCenter_AppVersion",
                table: "DataCenter_AppVersion");

            migrationBuilder.RenameTable(
                name: "DataCenter_AppVersion",
                newName: "datacenter_app_version");

            migrationBuilder.RenameIndex(
                name: "IX_DataCenter_AppVersion_AppId",
                table: "datacenter_app_version",
                newName: "IX_datacenter_app_version_AppId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_datacenter_app_version",
                table: "datacenter_app_version",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_datacenter_app_version_datacenter_app_AppId",
                table: "datacenter_app_version",
                column: "AppId",
                principalTable: "datacenter_app",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_datacenter_app_version_datacenter_app_AppId",
                table: "datacenter_app_version");

            migrationBuilder.DropPrimaryKey(
                name: "PK_datacenter_app_version",
                table: "datacenter_app_version");

            migrationBuilder.RenameTable(
                name: "datacenter_app_version",
                newName: "DataCenter_AppVersion");

            migrationBuilder.RenameIndex(
                name: "IX_datacenter_app_version_AppId",
                table: "DataCenter_AppVersion",
                newName: "IX_DataCenter_AppVersion_AppId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DataCenter_AppVersion",
                table: "DataCenter_AppVersion",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DataCenter_AppVersion_datacenter_app_AppId",
                table: "DataCenter_AppVersion",
                column: "AppId",
                principalTable: "datacenter_app",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
