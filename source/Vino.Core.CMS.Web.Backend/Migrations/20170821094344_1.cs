using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "membership_member_type",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_function",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AuthCode = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HasSub = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_function", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AuthCode = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HasSubMenu = table.Column<bool>(type: "bit", nullable: false),
                    Icon = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    IsShow = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Url = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    NameEn = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Remarks = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_user",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Account = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Factor = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginIp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Remarks = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedTaskLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BeginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Result = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    TaskId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedTaskLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedTasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    AutoReset = table.Column<bool>(type: "bit", nullable: false),
                    BeginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExpireTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Identifier = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true),
                    RunTimes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedTasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "wechat_account",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    AppId = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: true),
                    AppSecret = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Image = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    OriginalId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Token = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    WeixinId = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wechat_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "membership_member",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Factor = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    LastLoginIp = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    MemberTypeId = table.Column<long>(type: "bigint", nullable: true),
                    Mobile = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Password = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membership_member", x => x.Id);
                    table.ForeignKey(
                        name: "FK_membership_member_membership_member_type_MemberTypeId",
                        column: x => x.MemberTypeId,
                        principalTable: "membership_member_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "system_role_function",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    FunctionId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "system_action_log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ActionName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    ActionResult = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ControllerName = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Ip = table.Column<string>(type: "varchar(46)", maxLength: 46, nullable: true),
                    Operation = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Url = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    UrlReferrer = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_action_log", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_action_log_system_user_UserId",
                        column: x => x.UserId,
                        principalTable: "system_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "system_user_role",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
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
                name: "IX_membership_member_MemberTypeId",
                table: "membership_member",
                column: "MemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_system_action_log_UserId",
                table: "system_action_log",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_system_role_function_FunctionId",
                table: "system_role_function",
                column: "FunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_system_user_role_RoleId",
                table: "system_user_role",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TimedTaskLogs_TaskId",
                table: "TimedTaskLogs",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membership_member");

            migrationBuilder.DropTable(
                name: "system_action_log");

            migrationBuilder.DropTable(
                name: "system_menu");

            migrationBuilder.DropTable(
                name: "system_role_function");

            migrationBuilder.DropTable(
                name: "system_user_role");

            migrationBuilder.DropTable(
                name: "TimedTaskLogs");

            migrationBuilder.DropTable(
                name: "TimedTasks");

            migrationBuilder.DropTable(
                name: "wechat_account");

            migrationBuilder.DropTable(
                name: "membership_member_type");

            migrationBuilder.DropTable(
                name: "system_function");

            migrationBuilder.DropTable(
                name: "system_role");

            migrationBuilder.DropTable(
                name: "system_user");
        }
    }
}
