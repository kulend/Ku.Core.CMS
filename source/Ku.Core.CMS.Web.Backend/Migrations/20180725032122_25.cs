using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usercenter_user_dialogue",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    LastMessageSummary = table.Column<string>(maxLength: 256, nullable: true),
                    LastMessageTime = table.Column<DateTime>(nullable: false),
                    IsForbidden = table.Column<bool>(nullable: false),
                    IsSolved = table.Column<bool>(nullable: false),
                    SolveTime = table.Column<DateTime>(nullable: true),
                    SolveUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user_dialogue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usercenter_user_dialogue_usercenter_user_UserId",
                        column: x => x.UserId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "usercenter_user_dialogue_message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DialogueId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Message = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usercenter_user_dialogue_message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usercenter_user_dialogue_message_usercenter_user_dialogue_Di~",
                        column: x => x.DialogueId,
                        principalTable: "usercenter_user_dialogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usercenter_user_dialogue_message_usercenter_user_UserId",
                        column: x => x.UserId,
                        principalTable: "usercenter_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_dialogue_UserId",
                table: "usercenter_user_dialogue",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_dialogue_message_DialogueId",
                table: "usercenter_user_dialogue_message",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_usercenter_user_dialogue_message_UserId",
                table: "usercenter_user_dialogue_message",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usercenter_user_dialogue_message");

            migrationBuilder.DropTable(
                name: "usercenter_user_dialogue");
        }
    }
}
