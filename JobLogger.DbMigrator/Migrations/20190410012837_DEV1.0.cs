using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobLogger.DbMigrator.Migrations
{
    public partial class DEV10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CodeBranch",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeBranch", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Requirement",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirement", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RequirementID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Task_Requirement_RequirementID",
                        column: x => x.RequirementID,
                        principalTable: "Requirement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskLog",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogDate = table.Column<DateTime>(type: "Date", nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: true),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    TaskID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TaskLog_Task_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Task",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckIn",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false),
                    Comment = table.Column<string>(maxLength: 1000, nullable: false),
                    CheckInTime = table.Column<DateTime>(nullable: false),
                    TaskLogID = table.Column<long>(nullable: true),
                    CodeBranchID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckIn_CodeBranch_CodeBranchID",
                        column: x => x.CodeBranchID,
                        principalTable: "CodeBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckIn_TaskLog_TaskLogID",
                        column: x => x.TaskLogID,
                        principalTable: "TaskLog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskCheckIn",
                columns: table => new
                {
                    TaskID = table.Column<long>(nullable: false),
                    CheckInID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskCheckIn", x => new { x.TaskID, x.CheckInID });
                    table.ForeignKey(
                        name: "FK_TaskCheckIn_CheckIn_CheckInID",
                        column: x => x.CheckInID,
                        principalTable: "CheckIn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskCheckIn_Task_TaskID",
                        column: x => x.TaskID,
                        principalTable: "Task",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_CodeBranchID",
                table: "CheckIn",
                column: "CodeBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_TaskLogID",
                table: "CheckIn",
                column: "TaskLogID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_RequirementID",
                table: "Task",
                column: "RequirementID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskCheckIn_CheckInID",
                table: "TaskCheckIn",
                column: "CheckInID");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLog_TaskID",
                table: "TaskLog",
                column: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskCheckIn");

            migrationBuilder.DropTable(
                name: "CheckIn");

            migrationBuilder.DropTable(
                name: "CodeBranch");

            migrationBuilder.DropTable(
                name: "TaskLog");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Requirement");
        }
    }
}
