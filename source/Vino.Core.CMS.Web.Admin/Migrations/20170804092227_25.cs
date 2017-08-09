using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vino.Core.CMS.Web.Admin.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "membership_member",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "MemberTypeId",
                table: "membership_member",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "membership_member",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_membership_member_MemberTypeId",
                table: "membership_member",
                column: "MemberTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_membership_member_membership_member_type_MemberTypeId",
                table: "membership_member",
                column: "MemberTypeId",
                principalTable: "membership_member_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_membership_member_membership_member_type_MemberTypeId",
                table: "membership_member");

            migrationBuilder.DropIndex(
                name: "IX_membership_member_MemberTypeId",
                table: "membership_member");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "membership_member");

            migrationBuilder.DropColumn(
                name: "MemberTypeId",
                table: "membership_member");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "membership_member");
        }
    }
}
