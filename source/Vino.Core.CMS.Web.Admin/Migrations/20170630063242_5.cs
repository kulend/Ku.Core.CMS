using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicTimedJob");

            migrationBuilder.CreateTable(
                name: "TimedTasks",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AutoReset = table.Column<bool>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 256, nullable: true),
                    Interval = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedTasks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimedTasks");

            migrationBuilder.CreateTable(
                name: "DynamicTimedJob",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 512, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Begin = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Interval = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicTimedJob", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicTimedJob_IsEnabled",
                table: "DynamicTimedJob",
                column: "IsEnabled");
        }
    }
}
