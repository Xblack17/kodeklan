using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class leaveupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocationVM_EmployeeVM_EmployeeId",
                table: "LeaveAllocationVM");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_ApprovedById",
                table: "LeaveRequestVM");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_RequestingEmployeeId",
                table: "LeaveRequestVM");

            migrationBuilder.DropTable(
                name: "EmployeeVM");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequestVM_ApprovedById",
                table: "LeaveRequestVM");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequestVM_RequestingEmployeeId",
                table: "LeaveRequestVM");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocationVM_EmployeeId",
                table: "LeaveAllocationVM");

            migrationBuilder.AlterColumn<string>(
                name: "IDNUMBER",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveRequestVM",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedById",
                table: "LeaveRequestVM",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedById1",
                table: "LeaveRequestVM",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestingEmployeeId1",
                table: "LeaveRequestVM",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocationVM",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId1",
                table: "LeaveAllocationVM",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_ApprovedById1",
                table: "LeaveRequestVM",
                column: "ApprovedById1");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_RequestingEmployeeId1",
                table: "LeaveRequestVM",
                column: "RequestingEmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocationVM_EmployeeId1",
                table: "LeaveAllocationVM",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocationVM_Employee_EmployeeId1",
                table: "LeaveAllocationVM",
                column: "EmployeeId1",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequestVM_Employee_ApprovedById1",
                table: "LeaveRequestVM",
                column: "ApprovedById1",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequestVM_Employee_RequestingEmployeeId1",
                table: "LeaveRequestVM",
                column: "RequestingEmployeeId1",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAllocationVM_Employee_EmployeeId1",
                table: "LeaveAllocationVM");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_Employee_ApprovedById1",
                table: "LeaveRequestVM");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_Employee_RequestingEmployeeId1",
                table: "LeaveRequestVM");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequestVM_ApprovedById1",
                table: "LeaveRequestVM");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequestVM_RequestingEmployeeId1",
                table: "LeaveRequestVM");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAllocationVM_EmployeeId1",
                table: "LeaveAllocationVM");

            migrationBuilder.DropColumn(
                name: "ApprovedById1",
                table: "LeaveRequestVM");

            migrationBuilder.DropColumn(
                name: "RequestingEmployeeId1",
                table: "LeaveRequestVM");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "LeaveAllocationVM");

            migrationBuilder.AlterColumn<string>(
                name: "IDNUMBER",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestingEmployeeId",
                table: "LeaveRequestVM",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ApprovedById",
                table: "LeaveRequestVM",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveAllocationVM",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_ApprovedById",
                table: "LeaveRequestVM",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_RequestingEmployeeId",
                table: "LeaveRequestVM",
                column: "RequestingEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocationVM_EmployeeId",
                table: "LeaveAllocationVM",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAllocationVM_EmployeeVM_EmployeeId",
                table: "LeaveAllocationVM",
                column: "EmployeeId",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_ApprovedById",
                table: "LeaveRequestVM",
                column: "ApprovedById",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_RequestingEmployeeId",
                table: "LeaveRequestVM",
                column: "RequestingEmployeeId",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
