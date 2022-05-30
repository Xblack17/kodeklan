using iTut.Constants;
using iTut.Data;
using iTut.Models;
using iTut.Models.Parent;
using iTut.Models.Users;
using iTut.Models.ViewModels.Coordinator;
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
    [Authorize(Roles = RoleConstants.SubjectCoordinator)]
    public class CoordinatorController1 : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<CoordinatorController1> _logger;


        public CoordinatorController1(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<CoordinatorController1> logger)
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
    }
}
