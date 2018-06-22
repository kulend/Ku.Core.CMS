using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaCode",
                table: "usercenter_user_address",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IntValue",
                table: "demo1",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "DoubleValue",
                table: "demo1",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaCode",
                table: "usercenter_user_address");

            migrationBuilder.AlterColumn<int>(
                name: "IntValue",
                table: "demo1",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DoubleValue",
                table: "demo1",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
