using Microsoft.EntityFrameworkCore.Migrations;

namespace JobLogger.DbMigrator.Migrations
{
    public partial class DEV18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CodeBranch",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CodeBranch");
        }
    }
}
