using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class QuizEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerID);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    OptionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.OptionID);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuizID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionID);
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducatorID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuizDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.QuizId);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ResultID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuestionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudentID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Results");
        }
    }
}
