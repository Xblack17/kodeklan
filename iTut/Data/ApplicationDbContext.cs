using iTut.Models.Parent;
using iTut.Models.Relationships;
using iTut.Models.Coordinator;
using iTut.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using iTut.Models.HOD;
using iTut.Models.ViewModels.Student;
using static iTut.Models.ViewModels.HOD.HODIndexViewModel;
using iTut.Models.HOD.Leave;

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
        public DbSet<EducatorUser> Educators { get; set; }
        public DbSet<StudentUser> Students { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<CoordinatorUser> SubjectCoordinators { get; set; }
        public DbSet<Subject> Subjects { get; set; }
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
        public DbSet<AssignStuff> AssignStuff { get; set; }
        
        
        public DbSet<Grade> Grade { get; set; }
        public DbSet<FeeType> FeeType { get; set; }
        public DbSet<LeaveRequestVM> LeaveRequestVM { get; set; }
        public DbSet<LeaveTypeVM> LeaveTypeVM { get; set; }
        public DbSet<AdminLeaveRequestViewVM> AdminLeaveRequestViewVM { get; set; }
        public DbSet<CalendarEvent> Events { get; set; }


    }
}
