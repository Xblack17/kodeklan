using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class updates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_Employee_ApprovedById1",
                table: "LeaveRequestVM");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_Employee_RequestingEmployeeId1",
                table: "LeaveRequestVM");

            

            migrationBuilder.DropTable(
                name: "LeaveAllocationVM");

            

           

            migrationBuilder.AddColumn<int>(
                name: "EmployeeVMId",
                table: "JobInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeVMId",
                table: "EmploymentHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeVMId",
                table: "EmployeeEducation",
                type: "int",
                nullable: true);

           

            migrationBuilder.CreateTable(
                name: "EmployeeVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EducationLevelId = table.Column<int>(type: "int", nullable: false),
                    ExamTitleId = table.Column<int>(type: "int", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstituteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResultType = table.Column<int>(type: "int", nullable: false),
                    CGPA = table.Column<float>(type: "real", nullable: false),
                    Scale = table.Column<int>(type: "int", nullable: false),
                    Marks = table.Column<float>(type: "real", nullable: false),
                    PassingYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Achievement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    To = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false),
                    DOJ = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalLeave = table.Column<int>(type: "int", nullable: false),
                    Appointment = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeVM_Designation_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "Designation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeVM_EducationLevel_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeVM_ExamTitle_ExamTitleId",
                        column: x => x.ExamTitleId,
                        principalTable: "ExamTitle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobInfo_EmployeeVMId",
                table: "JobInfo",
                column: "EmployeeVMId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentHistory_EmployeeVMId",
                table: "EmploymentHistory",
                column: "EmployeeVMId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducation_EmployeeVMId",
                table: "EmployeeEducation",
                column: "EmployeeVMId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVM_DesignationId",
                table: "EmployeeVM",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVM_EducationLevelId",
                table: "EmployeeVM",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVM_ExamTitleId",
                table: "EmployeeVM",
                column: "ExamTitleId");

           

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducation_EmployeeVM_EmployeeVMId",
                table: "EmployeeEducation",
                column: "EmployeeVMId",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmploymentHistory_EmployeeVM_EmployeeVMId",
                table: "EmploymentHistory",
                column: "EmployeeVMId",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobInfo_EmployeeVM_EmployeeVMId",
                table: "JobInfo",
                column: "EmployeeVMId",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_ApprovedById1",
                table: "LeaveRequestVM",
                column: "ApprovedById1",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_RequestingEmployeeId1",
                table: "LeaveRequestVM",
                column: "RequestingEmployeeId1",
                principalTable: "EmployeeVM",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducation_EmployeeVM_EmployeeVMId",
                table: "EmployeeEducation");

            migrationBuilder.DropForeignKey(
                name: "FK_EmploymentHistory_EmployeeVM_EmployeeVMId",
                table: "EmploymentHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_JobInfo_EmployeeVM_EmployeeVMId",
                table: "JobInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_ApprovedById1",
                table: "LeaveRequestVM");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequestVM_EmployeeVM_RequestingEmployeeId1",
                table: "LeaveRequestVM");

            migrationBuilder.DropTable(
                name: "EmployeeVM");

            migrationBuilder.DropTable(
                name: "RegisterStudentViewModel");

            migrationBuilder.DropTable(
                name: "StudentParents");

            migrationBuilder.DropIndex(
                name: "IX_JobInfo_EmployeeVMId",
                table: "JobInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmploymentHistory_EmployeeVMId",
                table: "EmploymentHistory");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducation_EmployeeVMId",
                table: "EmployeeEducation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectCoordinators",
                table: "SubjectCoordinators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educators",
                table: "Educators");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "EmployeeVMId",
                table: "JobInfo");

            migrationBuilder.DropColumn(
                name: "EmployeeVMId",
                table: "EmploymentHistory");

            migrationBuilder.DropColumn(
                name: "EmployeeVMId",
                table: "EmployeeEducation");

            migrationBuilder.RenameTable(
                name: "SubjectCoordinators",
                newName: "SubjectCoordinator");

            migrationBuilder.RenameTable(
                name: "Educators",
                newName: "Educator");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_at",
                table: "Subjects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectCoordinator",
                table: "SubjectCoordinator",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educator",
                table: "Educator",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeedbackContent = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAllocationVM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId1 = table.Column<int>(type: "int", nullable: true),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    NumberOfDays = table.Column<int>(type: "int", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocationVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAllocationVM_Employee_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveAllocationVM_LeaveTypeVM_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingRequest",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParentUserStudentUser",
                columns: table => new
                {
                    ChildrenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentUserStudentUser", x => new { x.ChildrenId, x.ParentsId });
                    table.ForeignKey(
                        name: "FK_ParentUserStudentUser_Parents_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "Parents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentUserStudentUser_Students_ChildrenId",
                        column: x => x.ChildrenId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EducatorID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocationVM_EmployeeId1",
                table: "LeaveAllocationVM",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocationVM_LeaveTypeId",
                table: "LeaveAllocationVM",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentUserStudentUser_ParentsId",
                table: "ParentUserStudentUser",
                column: "ParentsId");

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
    }
}
