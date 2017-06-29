using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicTimedJob");
        }
    }
}
