using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class SubjectModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "Educator",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educator_SubjectId",
                table: "Educator",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educator_Subjects_SubjectId",
                table: "Educator",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educator_Subjects_SubjectId",
                table: "Educator");

            migrationBuilder.DropIndex(
                name: "IX_Educator_SubjectId",
                table: "Educator");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Educator");
        }
    }
}
