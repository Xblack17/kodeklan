using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class SubjectCoordinator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_subjectcoordinator",
                table: "subjectcoordinator");

            migrationBuilder.RenameTable(
                name: "subjectcoordinator",
                newName: "SubjectCoordinator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectCoordinator",
                table: "SubjectCoordinator",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectCoordinator",
                table: "SubjectCoordinator");

            migrationBuilder.RenameTable(
                name: "SubjectCoordinator",
                newName: "subjectcoordinator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_subjectcoordinator",
                table: "subjectcoordinator",
                column: "Id");
        }
    }
}
