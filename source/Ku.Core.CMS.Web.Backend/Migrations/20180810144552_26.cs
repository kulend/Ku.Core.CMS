using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "content_advertisement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: true),
                    ImageData = table.Column<string>(maxLength: 512, nullable: true),
                    FlashUrl = table.Column<string>(maxLength: 256, nullable: true),
                    Link = table.Column<string>(maxLength: 512, nullable: true),
                    Provenance = table.Column<string>(maxLength: 64, nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    Clicks = table.Column<int>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_advertisement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "content_advertisement_board",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Tag = table.Column<string>(maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_advertisement_board", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "content_advertisement_board_ref",
                columns: table => new
                {
                    AdvertisementId = table.Column<long>(nullable: false),
                    AdvertisementBoardId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_advertisement_board_ref", x => new { x.AdvertisementId, x.AdvertisementBoardId });
                    table.ForeignKey(
                        name: "FK_content_advertisement_board_ref_content_advertisement_board_~",
                        column: x => x.AdvertisementBoardId,
                        principalTable: "content_advertisement_board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_content_advertisement_board_ref_content_advertisement_Advert~",
                        column: x => x.AdvertisementId,
                        principalTable: "content_advertisement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_content_advertisement_board_ref_AdvertisementBoardId",
                table: "content_advertisement_board_ref",
                column: "AdvertisementBoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content_advertisement_board_ref");

            migrationBuilder.DropTable(
                name: "content_advertisement_board");

            migrationBuilder.DropTable(
                name: "content_advertisement");
        }
    }
}
