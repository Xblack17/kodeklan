using iTut.Models.Parent;
using iTut.Models.Coordinator;
using iTut.Models.Users;
using iTut.Models.Quiz;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using iTut.Models.ViewModels.Student;
using iTut.Models.Edu;

namespace iTut.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }

        public DbSet<ParentUser> Parents { get; set; }
        public DbSet<HODUser> HODs { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<EducatorUser> Educator { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<StudentUser> Students { get; set; }
        public DbSet<CoordinatorUser> SubjectCoordinator { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<MeetingRequest> MeetingRequest { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SubjectGrade> SubjectGrades { get; set; }
        public DbSet<SubjectEducator> SubjectEducators { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
    }
}
