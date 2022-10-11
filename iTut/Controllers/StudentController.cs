﻿using iTut.Constants;
using iTut.Data;
using iTut.Models;
using iTut.Models.Users;
using iTut.Models.ViewModels.Student;
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
    [Authorize(Roles = RoleConstants.Student)]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<StudentController> _logger;

        public StudentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<StudentController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        public ActionResult Index()
        {
            var student = _context.Students.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();

            var viewModel = new StudentIndexViewModel
            {
                Student = student,
            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

         public IActionResult Subjects()
        {
            return View();
        }
        public IActionResult Search()
        {
            var displaydata = _context.Subjects.ToList();

            return View(displaydata);
        }
        
        [HttpGet]
        public async Task<IActionResult> Search(string searchSubject)
        {
            ViewData["CreateSubject"] = searchSubject;

            var subjects = from s in _context.Subjects
                         select s;

            if (!String.IsNullOrEmpty(searchSubject))
            {
                subjects = subjects.Where(s => s.SubjectName.Contains(searchSubject) || s.SubjectId.Contains(searchSubject));
            }

            return View(await subjects.ToListAsync());
        }
        public IActionResult Calendar()
        {
            return View();
        }
    }
}
