using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class latestupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            

            

            migrationBuilder.DropColumn(
                name: "Image",
                table: "EmployeeVM");

            

            migrationBuilder.AlterColumn<string>(
                name: "IDNUMBER",
                table: "Student",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

           

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

           

            migrationBuilder.AlterColumn<string>(
                name: "IDNUMBER",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldNullable: true);

           

          
           

            
        }
    }
}
