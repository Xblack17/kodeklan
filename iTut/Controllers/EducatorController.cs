using iTut.Constants;
using iTut.Data;
using iTut.Models.Users;
using iTut.Models.UploadFiles;
using iTut.Models.ViewModels.Educator;
using iTut.Models.Edu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using iTut.Models.Quiz;

namespace iTut.Controllers
{
    [Authorize(Roles = RoleConstants.Educator)]
    public class EducatorController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EducatorController> _logger;

        public EducatorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<EducatorController> logger)
        {
            _context = context;
            _userManager = userManager;
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

        //Categories are renamed to topics 
        public IActionResult Categories()
        {
            //return View(await _context.Categories.Where(c => c.EducatorID == _userManager.GetUserId(User)).ToListAsync());
            return View(_context.Topics.ToList());

        }

        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(Topic model)
        {
            if (ModelState.IsValid)
            {
                var educator = _context.Educator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();

                var category = new Topic
                {
                    EducatorID = educator.Id,
                    TopicName = model.TopicName,
                    Status = Topic.TopicStatus.Active,
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
            ViewBag.Topics =_context.Topics.Where(t=>t.Status==Topic.TopicStatus.Active).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQuiz (Quiz model)
        {
           if(ModelState.IsValid)
           {
                var educator = _context.Educator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();

                var quiz = new Quiz
                {
                    EducatorID = educator.Id,
                    TopicId = model.TopicId,
                    QuizDescription = model.QuizDescription,
                    status = Quiz.quizStatus.Active,
                    CreatedAt = DateTime.Now,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                };
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Quiz id= {quiz.QuizId}, has beeen created!");
                return RedirectToAction(nameof(AddQuestion));

           }

            return View(model); 

        }

        public IActionResult AddQuestion()
        {
            ViewBag.Quizzes = _context.Quizzes.Where(q => q.status == Quiz.quizStatus.Active).ToList();
            return View();

        }
        public IActionResult UploadFile()
        {
            SingleFileModel model = new SingleFileModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Upload(SingleFileModel model)
        {
            if (ModelState.IsValid)
            {
                model.IsResponse = true;

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");

                //create folder if not exist
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                //get file extension
                FileInfo fileInfo = new FileInfo(model.File.FileName);
                string fileName = model.FileName + fileInfo.Extension;

                string fileNameWithPath = Path.Combine(path, fileName);

                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
                model.IsSuccess = true;
                model.Message = "File upload successfully";
            }
            return View("UploadFile", model);
        }


    }
}
