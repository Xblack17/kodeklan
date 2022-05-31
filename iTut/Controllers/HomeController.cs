using iTut.Constants;
using iTut.Models;
using iTut.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace iTut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(RoleConstants.Parent))
                {
                    return RedirectToAction("Index", "Parent");
                }
                else if (User.IsInRole(RoleConstants.Educator))
                {
                    return RedirectToAction("Index", "Educator");
                }
                else if (User.IsInRole(RoleConstants.Student))
                {
                    return RedirectToAction("Index", "Student");
                }
                else if (User.IsInRole(RoleConstants.SubjectCoordinator))
                {
                    return RedirectToAction("Index", "Coordinator");
                }
                else
                {
                    return RedirectToAction("Index", "HOD");
                }
            }
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Disclaimer()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
