using iTut.Constants;
using iTut.Data;
using iTut.Models;
using iTut.Models.ViewModels;
using iTut.Models.Users;
using iTut.Models.ViewModels.HOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static iTut.Models.ViewModels.HOD.HODIndexViewModel;
using System.Collections.Generic;
using System;

namespace iTut.Controllers
{
    [Authorize(Roles = RoleConstants.HOD)]
    public class HODController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HODController> _logger;

        public HODController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<HODController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: HODController
        public ActionResult Index()
        {
            var hod = _context.HOD.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();

            var viewModel = new HODIndexViewModel
            {
                HodUser = hod,
            };

            return View(viewModel);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}

