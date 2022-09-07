using Microsoft.EntityFrameworkCore.Migrations;

namespace iTut.Data.Migrations
{
    public partial class tbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectEducators",
                columns: table => new
                {
                    SubjectEducatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EducatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectEducators", x => x.SubjectEducatorId);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGrades",
                columns: table => new
                {
                    SubjectGradeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGrades", x => x.SubjectGradeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "SubjectEducators");

            migrationBuilder.DropTable(
                name: "SubjectGrades");
        }
    }
}
