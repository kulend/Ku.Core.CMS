using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataCenter_AppVersion",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AppId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(maxLength: 2000, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    DownLoadUrl = table.Column<string>(maxLength: 256, nullable: false),
                    Force = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Version = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataCenter_AppVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataCenter_AppVersion_datacenter_app_AppId",
                        column: x => x.AppId,
                        principalTable: "datacenter_app",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataCenter_AppVersion_AppId",
                table: "DataCenter_AppVersion",
                column: "AppId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataCenter_AppVersion");
        }
    }
}
