using iTut.Constants;
using iTut.Data;
using iTut.Models;
using iTut.Models.UserModels.ParentUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace iTut
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            InitializeDbContextAsync(app).GetAwaiter().GetResult();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private async Task InitializeDbContextAsync(IApplicationBuilder builder)
        {
            var serviceScope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                Console.WriteLine(">>>>>>>>>>>>>>>>>> I am being called <<<<<<<<<<<<<<<<<<<<<<");
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                var studentRole = await roleManager.FindByNameAsync(RoleConstants.Student);
                if (studentRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Student));
                }

                var educatorRole = await roleManager.FindByNameAsync(RoleConstants.Educator);
                if (educatorRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Educator));
                }

                var parentRole = await roleManager.FindByNameAsync(RoleConstants.Parent);
                if (parentRole == null)
                {
                    var role = new IdentityRole(RoleConstants.Parent);
                    await roleManager.CreateAsync(role);

                    var user = new ApplicationUser
                    {
                        FirstName = "Johannes",
                        LastName = "Mogashoa",
                        PhoneNumber = "0618021411",
                        Email = "jm.segodi@gmail.com",
                        UserName = "jm.segodi@gmail.com",
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, "Pass@123456");

                    if (result.Succeeded)
                    {
                        Console.WriteLine(">>>>>>>>>>>>>>>>>> I Created the user <<<<<<<<<<<<<<<<<<<<<<");
                        await userManager.AddToRoleAsync(user, RoleConstants.Parent);
                        var parent = new Parent
                        {
                            UserId = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Gender = Gender.Female,
                            Race = Race.African,
                            PhysicalAddress = "3 On Mill, Port Elizabeth Central, Eastern Cape, 6001",
                            CreatedOn = DateTime.Now
                        };
                        context.Add(parent);
                    }
                    await context.SaveChangesAsync();
                    Console.WriteLine(">>>>>>>>>>>>>>>>>> Everything should be saved to the database by now <<<<<<<<<<<<<<<<<<<<<<");
                }

                var hodRole = await roleManager.FindByNameAsync(RoleConstants.HOD);
                if (hodRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleConstants.HOD));
                }

                var facilitatorRole = await roleManager.FindByNameAsync(RoleConstants.Facilitator);
                if (facilitatorRole == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(RoleConstants.Facilitator));
                }
            }
        }
    }
}
