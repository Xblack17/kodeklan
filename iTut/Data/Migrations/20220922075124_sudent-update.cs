using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class sudentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "IDNUMBER",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "IDNUMBER",
                table: "Student");

            

            
        }
    }
}
