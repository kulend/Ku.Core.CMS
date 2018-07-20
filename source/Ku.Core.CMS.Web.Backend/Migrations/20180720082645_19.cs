using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "content_column",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tag",
                table: "content_column");
        }
    }
}
