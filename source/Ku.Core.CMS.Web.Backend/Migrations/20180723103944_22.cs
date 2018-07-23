using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ku.Core.CMS.Web.Backend.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimedTaskLogs");

            migrationBuilder.DropTable(
                name: "TimedTasks");

            migrationBuilder.CreateTable(
                name: "system_timed_task",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Group = table.Column<string>(maxLength: 64, nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Status = table.Column<short>(nullable: false),
                    Cron = table.Column<string>(nullable: true),
                    AssemblyName = table.Column<string>(maxLength: 128, nullable: false),
                    TypeName = table.Column<string>(maxLength: 128, nullable: false),
                    StarRunTime = table.Column<DateTime>(nullable: true),
                    EndRunTime = table.Column<DateTime>(nullable: true),
                    NextRunTime = table.Column<DateTime>(nullable: true),
                    ValidStartTime = table.Column<DateTime>(nullable: false),
                    ValidEndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_system_timed_task", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "system_timed_task");

            migrationBuilder.CreateTable(
                name: "TimedTaskLogs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<long>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Result = table.Column<string>(maxLength: 20, nullable: true),
                    TaskId = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedTaskLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimedTasks",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    AutoReset = table.Column<bool>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    ExpireTime = table.Column<DateTime>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 256, nullable: true),
                    Interval = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: true),
                    RunTimes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimedTasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimedTaskLogs_TaskId",
                table: "TimedTaskLogs",
                column: "TaskId");
        }
    }
}
