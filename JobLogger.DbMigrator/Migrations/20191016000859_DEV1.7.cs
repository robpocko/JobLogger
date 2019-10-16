using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobLogger.DbMigrator.Migrations
{
    public partial class DEV17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TimeLineID",
                table: "TaskLog",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimeLine",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLine", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_TimeLineID",
                table: "TaskLog",
                column: "TimeLineID");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskLog_TimeLine_TimeLineID",
                table: "TaskLog",
                column: "TimeLineID",
                principalTable: "TimeLine",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskLog_TimeLine_TimeLineID",
                table: "TaskLog");

            migrationBuilder.DropTable(
                name: "TimeLine");

            migrationBuilder.DropIndex(
                name: "IX_TaskLog_TimeLineID",
                table: "TaskLog");

            migrationBuilder.DropColumn(
                name: "TimeLineID",
                table: "TaskLog");
        }
    }
}
