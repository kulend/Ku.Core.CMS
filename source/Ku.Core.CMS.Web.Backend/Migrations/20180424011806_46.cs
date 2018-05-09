using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "datacenter_app_feedback",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Content = table.Column<string>(maxLength: 2000, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProviderId = table.Column<long>(nullable: true),
                    ProviderName = table.Column<string>(maxLength: 64, nullable: true),
                    Resolved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datacenter_app_feedback", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datacenter_app_feedback");
        }
    }
}
