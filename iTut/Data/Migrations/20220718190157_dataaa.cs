using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class dataaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Educators",
                table: "Educators");

            migrationBuilder.RenameTable(
                name: "Educators",
                newName: "Educator");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educator",
                table: "Educator",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Educator",
                table: "Educator");

            migrationBuilder.RenameTable(
                name: "Educator",
                newName: "Educators");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educators",
                table: "Educators",
                column: "Id");
        }
    }
}
