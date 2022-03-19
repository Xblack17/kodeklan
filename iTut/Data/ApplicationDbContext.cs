using iTut.Models;
using iTut.Models.UserModels.EducatorUser;
using iTut.Models.UserModels.FacilitatorUser;
using iTut.Models.UserModels.HodUser;
using iTut.Models.UserModels.ParentUser;
using iTut.Models.UserModels.StudentUser;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace iTut.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Educator> Educators { get; set; }
        public DbSet<HOD> HODs { get; set; }
        public DbSet<Facilitator> Facilitators { get; set; }
    }
}
