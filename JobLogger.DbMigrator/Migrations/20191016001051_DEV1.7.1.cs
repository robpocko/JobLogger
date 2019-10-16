using Microsoft.EntityFrameworkCore.Migrations;

namespace JobLogger.DbMigrator.Migrations
{
    public partial class DEV171 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TimeLine",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TimeLine");
        }
    }
}
