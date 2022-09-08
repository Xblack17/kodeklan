using iTut.Models.Parent;
using iTut.Models.Coordinator;
using iTut.Models.Users;
using iTut.Models.Quiz;
using iTut.Models.Educator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using iTut.Models.HOD;
using iTut.Models.ViewModels.Student;
using static iTut.Models.ViewModels.HOD.HODIndexViewModel;

namespace iTut.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
        public ApplicationDbContext()
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
            builder.Entity<RegisterStudentViewModel>().HasNoKey();
        }

        public DbSet<ParentUser> Parents { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<EducatorUser> Educator { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<StudentUser> Students { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<CoordinatorUser> SubjectCoordinator { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<MeetingRequest> MeetingRequest { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<SubjectGrade> SubjectGrades { get; set; }
        public DbSet<SubjectEducator> SubjectEducators { get; set; }

        internal Task FindAsync(int id)
        {
            throw new NotImplementedException();
        }
        public DbSet<HODUser> HOD { get; set; }




        public DbSet<GuardianType> GuardianType { get; set; }
        public DbSet<Guardian> Guardian { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Admission> Admission { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Shift> Shift { get; set; }
        
        public DbSet<Course> Course { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<ClassName> ClassName { get; set; }
        public DbSet<AssignRoll> AssignRoll { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<ExamTitle> ExamTitle { get; set; }
        public DbSet<EmployeeEducation> EmployeeEducation { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistory { get; set; }
        public DbSet<JobInfo> JobInfo { get; set; }
        public DbSet<ClassFee> ClassFee { get; set; }
        
        public DbSet<DefaultSetting> DefaultSetting { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<iTut.Models.HOD.AssignStuff> AssignStuff { get; set; }
        
        
        public DbSet<iTut.Models.HOD.Grade> Grade { get; set; }
        public DbSet<iTut.Models.HOD.FeeType> FeeType { get; set; }
        public DbSet<iTut.Models.HOD.Leave.LeaveAllocationVM> LeaveAllocationVM { get; set; }
        public DbSet<iTut.Models.HOD.Leave.LeaveRequestVM> LeaveRequestVM { get; set; }
        public DbSet<iTut.Models.HOD.Leave.LeaveTypeVM> LeaveTypeVM { get; set; }
        public DbSet<iTut.Models.HOD.Leave.AdminLeaveRequestViewVM> AdminLeaveRequestViewVM { get; set; }

        public DbSet<CalendarEvent> Events { get; set; }


        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }



    }
}
