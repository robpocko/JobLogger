using Microsoft.EntityFrameworkCore.Migrations;

namespace JobLogger.DbMigrator.Migrations
{
    public partial class DEV14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FeatureID",
                table: "Requirement",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirement_FeatureID",
                table: "Requirement",
                column: "FeatureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirement_Feature_FeatureID",
                table: "Requirement",
                column: "FeatureID",
                principalTable: "Feature",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirement_Feature_FeatureID",
                table: "Requirement");

            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropIndex(
                name: "IX_Requirement_FeatureID",
                table: "Requirement");

            migrationBuilder.DropColumn(
                name: "FeatureID",
                table: "Requirement");
        }
    }
}
