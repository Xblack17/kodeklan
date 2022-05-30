using iTut.Constants;
using iTut.Data;
using iTut.Models;
using iTut.Models.Parent;
using iTut.Models.Users;
using iTut.Models.ViewModels.Parent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = RoleConstants.Parent)]
    public class ParentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ParentController> _logger;

        public ParentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ParentController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: ParentController
        public ActionResult Index()
        {
            var parent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();

            var viewModel = new ParentIndexViewModel
            {
                Parent = parent,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> Complaints()
        {
            return View(await _context.Complaints.Where(c => c.ParentId == _userManager.GetUserId(User)).ToListAsync());
        }

        public IActionResult CreateComplaint()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComplaint(Complaint model)
        {
            if (ModelState.IsValid)
            {
                var parent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();
                var complaint = new Complaint {
                    ParentId = parent.Id,
                    Title = model.Title,
                    ComplaintBody = model.ComplaintBody,
                    Status = ComplaintStatus.Pending,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                _context.Add(complaint);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Complaint created!");
                return RedirectToAction(nameof(Complaints));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InviteParent(ParentInvite invite)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(invite);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
