using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "system_sms",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Data = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    ExpireTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    Signature = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_sms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "system_sms_queue",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SmsId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false)
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
                name: "IX_system_sms_queue_SmsId",
                table: "system_sms_queue",
                column: "SmsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_sms_queue");

            migrationBuilder.DropTable(
                name: "system_sms");
        }
    }
}
