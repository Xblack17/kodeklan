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
            // _userManager = userManager;
            _logger = logger;
        }
        // GET: CoordinatorController1
        public ActionResult Index()
        {
           
            return View();
        }

        // GET: CoordinatorController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CoordinatorController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoordinatorController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoordinatorController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CoordinatorController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CoordinatorController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CoordinatorController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
