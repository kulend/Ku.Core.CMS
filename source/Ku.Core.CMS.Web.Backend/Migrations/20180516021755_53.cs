using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchRule_TagId",
                table: "wechat_menu",
                newName: "MatchRuleTagId");

            migrationBuilder.RenameColumn(
                name: "MatchRule_Sex",
                table: "wechat_menu",
                newName: "MatchRuleSex");

            migrationBuilder.RenameColumn(
                name: "MatchRule_Province",
                table: "wechat_menu",
                newName: "MatchRuleProvince");

            migrationBuilder.RenameColumn(
                name: "MatchRule_Language",
                table: "wechat_menu",
                newName: "MatchRuleLanguage");

            migrationBuilder.RenameColumn(
                name: "MatchRule_Country",
                table: "wechat_menu",
                newName: "MatchRuleCountry");

            migrationBuilder.RenameColumn(
                name: "MatchRule_Client",
                table: "wechat_menu",
                newName: "MatchRuleClient");

            migrationBuilder.RenameColumn(
                name: "MatchRule_City",
                table: "wechat_menu",
                newName: "MatchRuleCity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MatchRuleTagId",
                table: "wechat_menu",
                newName: "MatchRule_TagId");

            migrationBuilder.RenameColumn(
                name: "MatchRuleSex",
                table: "wechat_menu",
                newName: "MatchRule_Sex");

            migrationBuilder.RenameColumn(
                name: "MatchRuleProvince",
                table: "wechat_menu",
                newName: "MatchRule_Province");

            migrationBuilder.RenameColumn(
                name: "MatchRuleLanguage",
                table: "wechat_menu",
                newName: "MatchRule_Language");

            migrationBuilder.RenameColumn(
                name: "MatchRuleCountry",
                table: "wechat_menu",
                newName: "MatchRule_Country");

            migrationBuilder.RenameColumn(
                name: "MatchRuleClient",
                table: "wechat_menu",
                newName: "MatchRule_Client");

            migrationBuilder.RenameColumn(
                name: "MatchRuleCity",
                table: "wechat_menu",
                newName: "MatchRule_City");
        }
    }
}
