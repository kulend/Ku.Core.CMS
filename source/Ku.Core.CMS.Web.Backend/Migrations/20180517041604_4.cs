using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_usercenter_role_function_system_function_FunctionId",
                table: "usercenter_role_function",
                column: "FunctionId",
                principalTable: "system_function",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_usercenter_user_role_usercenter_role_RoleId",
                table: "usercenter_user_role",
                column: "RoleId",
                principalTable: "usercenter_role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_membership_member_point_record_usercenter_user_OperatorId",
                table: "membership_member_point_record");

            migrationBuilder.DropForeignKey(
                name: "FK_system_action_log_usercenter_user_UserId",
                table: "system_action_log");

            migrationBuilder.DropForeignKey(
                name: "FK_usercenter_role_function_system_function_FunctionId",
                table: "usercenter_role_function");

            migrationBuilder.DropForeignKey(
                name: "FK_usercenter_user_role_usercenter_role_RoleId",
                table: "usercenter_user_role");

            migrationBuilder.DropIndex(
                name: "IX_usercenter_user_role_RoleId",
                table: "usercenter_user_role");

            migrationBuilder.DropIndex(
                name: "IX_usercenter_role_function_FunctionId",
                table: "usercenter_role_function");

            migrationBuilder.CreateTable(
                name: "system_role",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    NameEn = table.Column<string>(maxLength: 40, nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_user",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Account = table.Column<string>(maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    Factor = table.Column<int>(nullable: true),
                    HeadImage = table.Column<string>(maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    LastLoginIp = table.Column<string>(maxLength: 40, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Password = table.Column<string>(maxLength: 64, nullable: false),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_role_function",
                columns: table => new
                {
                    RoleId = table.Column<long>(nullable: false),
                    FunctionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_role_function", x => new { x.RoleId, x.FunctionId });
                    table.ForeignKey(
                        name: "FK_system_role_function_system_function_FunctionId",
                        column: x => x.FunctionId,
                        principalTable: "system_function",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_system_role_function_system_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "system_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "system_user_role",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_user_role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_system_user_role_system_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "system_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_system_user_role_system_user_UserId",
                        column: x => x.UserId,
                        principalTable: "system_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_material_picture_UploadUserId",
                table: "material_picture",
                column: "UploadUserId");

            migrationBuilder.CreateIndex(
                name: "IX_material_group_CreateUserId",
                table: "material_group",
                column: "CreateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_system_role_function_FunctionId",
                table: "system_role_function",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_system_user_role_RoleId",
                table: "system_user_role",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_material_group_system_user_CreateUserId",
                table: "material_group",
                column: "CreateUserId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_material_picture_system_user_UploadUserId",
                table: "material_picture",
                column: "UploadUserId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_membership_member_point_record_system_user_OperatorId",
                table: "membership_member_point_record",
                column: "OperatorId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_system_action_log_system_user_UserId",
                table: "system_action_log",
                column: "UserId",
                principalTable: "system_user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
