using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vino.Core.CMS.Web.Backend.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "content_article",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "ContentType",
                table: "content_article",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "content_article",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "content_article",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "content_article",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedTime",
                table: "content_article",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitle",
                table: "content_article",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Visits",
                table: "content_article",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "PublishedTime",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "SubTitle",
                table: "content_article");

            migrationBuilder.DropColumn(
                name: "Visits",
                table: "content_article");
        }
    }
}
