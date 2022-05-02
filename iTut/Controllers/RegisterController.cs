using iTut.Constants;
using iTut.Data;
using iTut.Models.Users;
using iTut.Models.ViewModels.Parent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace iTut.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegisterController> _logger;
        private readonly IConfiguration _configuration;

        public RegisterController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context, ILogger<RegisterController> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Parent()
        {
            return View(new RegisterParentViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Parent(RegisterParentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.EmailAddress,
                    Email = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.Parent);
                    var parent = new ParentUser
                    {
                        UserId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.EmailAddress,
                        PhoneNumber = model.PhoneNumber,
                        PhysicalAddress = model.PhysicalAddress,
                        Gender = model.Gender,
                        Race = model.Race,
                        CreatedOn = DateTime.Now,
                        Archived = false
                    };
                    _context.Add(parent);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
    }
}
