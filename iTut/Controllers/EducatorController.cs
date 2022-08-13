using iTut.Constants;
using iTut.Data;
using iTut.Models.Edu;
using iTut.Models.Users;
using iTut.Models.ViewModels.Educator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace iTut.Controllers
{
    [Authorize(Roles = RoleConstants.Educator)]
    public class EducatorController: Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EducatorController> _logger;

        public EducatorController (ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<EducatorController> logger)
        {
            _context = context;
            _userManager=userManager;
            _logger = logger;
        }
        public ActionResult Index()
        {
            var educator = _context.Educator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();

            var viewModel = new EducatorIndexViewModel
            {
                Educator = educator,
              };

            return View(viewModel);
        }
        public IActionResult CreateTask()
        {
            return View();
        }
      
        public async Task<ActionResult> Categories()
        {
            //return View(await _context.Categories.Where(c => c.EducatorID == _userManager.GetUserId(User)).ToListAsync());
            return View(_context.Categories.ToList());
        }

        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>CreateCategory(Category model)
        {
            if(ModelState.IsValid)
            {
                var educator = _context.Educator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();

                var category = new Category
                {
                    EducatorID = educator.Id,
                    CategoryName = model.CategoryName,
                    categoryStatus = Category.CategoryStatus.Active,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,

                };
                _context.Add(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Categoy created!");
                return RedirectToAction(nameof(Categories));
            }
            return View(model);
        }

        public IActionResult CreateQuiz()
        {
            return View();
        }
    }
}
