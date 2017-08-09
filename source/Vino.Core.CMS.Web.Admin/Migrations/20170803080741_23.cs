using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "membership_member",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Factor = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LastLoginIp = table.Column<string>(maxLength: 40, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    Sex = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_system_action_log_UserId",
                table: "system_action_log",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_system_action_log_system_user_UserId",
                table: "system_action_log",
                column: "UserId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_system_action_log_system_user_UserId",
                table: "system_action_log");

            migrationBuilder.DropIndex(
                name: "IX_system_action_log_UserId",
                table: "system_action_log");

            migrationBuilder.DropTable(
                name: "membership_member");
        }
    }
}
