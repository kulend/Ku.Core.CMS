using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_sms_queue");

            migrationBuilder.DropTable(
                name: "system_sms");

            migrationBuilder.DropTable(
                name: "system_sms_templet");

            migrationBuilder.DropTable(
                name: "system_sms_account");

            migrationBuilder.CreateTable(
                name: "communication_sms_account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    ParameterConfig = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_communication_sms_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "communication_sms_templet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Example = table.Column<string>(maxLength: 400, nullable: true),
                    ExpireMinute = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Signature = table.Column<string>(maxLength: 40, nullable: true),
                    SmsAccountId = table.Column<long>(nullable: true),
                    Tag = table.Column<string>(maxLength: 64, nullable: false),
                    Templet = table.Column<string>(maxLength: 400, nullable: true),
                    TempletKey = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_communication_sms_templet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_communication_sms_templet_communication_sms_account_SmsAccountId",
                        column: x => x.SmsAccountId,
                        principalTable: "communication_sms_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "communication_sms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Content = table.Column<string>(maxLength: 256, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 512, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    SmsTempletId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_communication_sms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_communication_sms_communication_sms_templet_SmsTempletId",
                        column: x => x.SmsTempletId,
                        principalTable: "communication_sms_templet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "communication_sms_queue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Response = table.Column<string>(maxLength: 2000, nullable: true),
                    SendTime = table.Column<DateTime>(nullable: true),
                    SmsId = table.Column<long>(nullable: false),
                    Status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_communication_sms_queue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_communication_sms_queue_communication_sms_SmsId",
                        column: x => x.SmsId,
                        principalTable: "communication_sms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_communication_sms_SmsTempletId",
                table: "communication_sms",
                column: "SmsTempletId");

            migrationBuilder.CreateIndex(
                name: "IX_communication_sms_queue_SmsId",
                table: "communication_sms_queue",
                column: "SmsId");

            migrationBuilder.CreateIndex(
                name: "IX_communication_sms_templet_SmsAccountId",
                table: "communication_sms_templet",
                column: "SmsAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "communication_sms_queue");

            migrationBuilder.DropTable(
                name: "communication_sms");

            migrationBuilder.DropTable(
                name: "communication_sms_templet");

            migrationBuilder.DropTable(
                name: "communication_sms_account");

            migrationBuilder.CreateTable(
                name: "system_sms_account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    ParameterConfig = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 200, nullable: true),
                    Type = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_sms_account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_sms_templet",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Example = table.Column<string>(maxLength: 400, nullable: true),
                    ExpireMinute = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsEnable = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Signature = table.Column<string>(maxLength: 40, nullable: true),
                    SmsAccountId = table.Column<long>(nullable: true),
                    Tag = table.Column<string>(maxLength: 64, nullable: false),
                    Templet = table.Column<string>(maxLength: 400, nullable: true),
                    TempletKey = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_sms_templet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_sms_templet_system_sms_account_SmsAccountId",
                        column: x => x.SmsAccountId,
                        principalTable: "system_sms_account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "system_sms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Content = table.Column<string>(maxLength: 256, nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Data = table.Column<string>(maxLength: 512, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 11, nullable: true),
                    SmsTempletId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_sms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_sms_system_sms_templet_SmsTempletId",
                        column: x => x.SmsTempletId,
                        principalTable: "system_sms_templet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "system_sms_queue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Response = table.Column<string>(maxLength: 2000, nullable: true),
                    SendTime = table.Column<DateTime>(nullable: true),
                    SmsId = table.Column<long>(nullable: false),
                    Status = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_sms_queue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_system_sms_queue_system_sms_SmsId",
                        column: x => x.SmsId,
                        principalTable: "system_sms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_system_sms_SmsTempletId",
                table: "system_sms",
                column: "SmsTempletId");

            migrationBuilder.CreateIndex(
                name: "IX_system_sms_queue_SmsId",
                table: "system_sms_queue",
                column: "SmsId");

            migrationBuilder.CreateIndex(
                name: "IX_system_sms_templet_SmsAccountId",
                table: "system_sms_templet",
                column: "SmsAccountId");
        }
    }
}
