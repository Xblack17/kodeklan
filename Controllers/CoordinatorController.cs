using iTut.Constants;
using iTut.Data;
using iTut.Models.Users;
using iTut.Models.Coordinator;
using iTut.Models.ViewModels.Coordinator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace iTut.Controllers
{
    [Authorize(Roles = RoleConstants.SubjectCoordinator)]
    public class CoordinatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CoordinatorController> _logger;


        public CoordinatorController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<CoordinatorController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var SubjectCoordinator = _context.SubjectCoordinator.Where(c => c.UserId == _userManager.GetUserId(User)).FirstOrDefault();
            var viewModel = new CoordinatorIndexViewModel
            {
                SubjectCoordinator = SubjectCoordinator,
            };
            return View(viewModel);
        }

        public IActionResult Subject()
        {
            return View(_context.Subjects.ToList());
        }

        public IActionResult CreateASubject()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateASubject(Subject model)
        {
            if (ModelState.IsValid)
            {
                var SubjectCoordinator = _context.SubjectCoordinator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();

                var subject = new Subject
                {
                    Id = model.Id,
                    SubjectName = model.SubjectName,
                    SubjectDescr = model.SubjectDescr,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,

                };
                _context.Add(subject);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Subject was created!");
                return RedirectToAction("Subject");
            }
            return View(model);
        }
        //DELETE 
        public IActionResult Delete(string Id)
        {
            Subject subject = _context.Subjects.FirstOrDefault(s => s.Id == Id);
            if (subject != null)
            {
                _context.Remove(subject);
                _context.SaveChanges();
                return RedirectToAction("Subject");
            }
            return View();
        }

        //EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(string Id)
        {
            Subject subject = _context.Subjects.FirstOrDefault(s => s.Id == Id);
            if (subject != null)
            {
                _context.Remove(subject);
                _context.SaveChanges();
                return RedirectToAction("Subject");
            }
            return View();
        }

        public IActionResult Reports()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Reports(Report model)
        {
            if (ModelState.IsValid)
            {
                var SubjectCoordinator = _context.SubjectCoordinator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();
                var report = new Report
                {

                };
                _context.Add(report);

                _logger.LogInformation("Subject was created!");
                return RedirectToAction(nameof(Subject));
            }
            return View(model);
        }

        //GET FEEDBACK
        public IActionResult Feedback()
        {
            return View();
        }
        //feedback view
        public async Task<IActionResult> ViewFeedback()
        {
            return View(await _context.Feedbacks.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Feedback(Feedback model)
        {
            if (ModelState.IsValid)
            {
                var SubjectCoordinator = _context.SubjectCoordinator.Where(e => e.UserId == _userManager.GetUserId(User)).FirstOrDefault();
                var feedback = new Feedback
                {

                };
                _context.Add(feedback);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Feedback was created!");
                return RedirectToAction(nameof(Feedback));
            }
            return View(model);
        }

        //GETTING THE EDUCATOR
        public IActionResult Educator()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Educator(EducatorUser model)
        {
            _context.Educator.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Educator");
        }

    }
}