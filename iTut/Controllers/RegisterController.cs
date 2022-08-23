using iTut.Constants;
using iTut.Data;
using iTut.Models.Users;
using iTut.Models.ViewModels.Parent;
using iTut.Models.ViewModels.Educator;
using iTut.Models.ViewModels.Student;
using iTut.Models.ViewModels.Coordinator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using iTut.Models.ViewModels.HOD;
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
        public IActionResult HOD()
        {
            return View(new RegisterHODViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> HOD(RegisterHODViewModel model)
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
                    await _userManager.AddToRoleAsync(user, RoleConstants.HOD);
                    var hod = new HODUser
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
                    _context.Add(hod);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "HOD");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
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
                    return RedirectToAction("Index", "Parent");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Student()
        {
            return View(new RegisterStudentViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Student(RegisterStudentViewModel model)
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
                    await _userManager.AddToRoleAsync(user, RoleConstants.Student);
                    var student = new StudentUser
                    {
                        UserId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.EmailAddress,
                        PhoneNumber = model.PhoneNumber,
                        PhysicalAddress = model.PhysicalAddress,
                        Gender = model.Gender,
                        Grade = model.Grade,
                        Race = model.Race,
                        CreatedOn = DateTime.Now,
                        Archived = false
                    };
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Student");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Educator()
        {
            return View(new RegisterEducatorViewModel());
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Educator(RegisterEducatorViewModel model)
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
                    await _userManager.AddToRoleAsync(user, RoleConstants.Educator);
                    var educator = new EducatorUser
                    {
                        UserId = user.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        EmailAddress = model.EmailAddress,
                        PhoneNumber = model.PhoneNumber,
                        PhysicalAddress = model.PhysicalAddress,
                        PrimarySubject=model.PrimarySubject,
                        SecondarySubject=model.SecondarySubject,
                        Gender = model.Gender,
                        Race = model.Race,
                        CreatedOn = DateTime.Now,
                        Archived = false
                    };
                    _context.Add(educator);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Educator");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        
        [AllowAnonymous]
        [HttpGet]
        public IActionResult SubjectCoordinator()
        {
            return View(new RegisterSubjectCoordinator());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SubjectCoordinator(RegisterSubjectCoordinator model)
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
                    await _userManager.AddToRoleAsync(user, RoleConstants.SubjectCoordinator);
                    var coordinator = new CoordinatorUser
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
                    _context.Add(coordinator);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Coordinator");
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
