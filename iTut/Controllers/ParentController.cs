using Coravel.Queuing.Interfaces;
using iTut.Constants;
using iTut.Coravel.Events;
using iTut.Data;
using iTut.Models;
using iTut.Models.Parent;
using iTut.Models.Relationships;
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
        private readonly IQueue _queue;

        public ParentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<ParentController> logger, IQueue queue)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _queue = queue;
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

        #region Complaints

        public async Task<ActionResult> Complaints()
        {
            var parent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();
            var complaints = await _context.Complaints.Where(c => c.ParentId == parent.Id && c.Archived == false).ToListAsync();
            return View(complaints);
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
                //_queue.QueueBroadcast(new ComplaintCreated(complaint, parent));
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Complaint, id: {complaint.Id}, created!");
                return RedirectToAction(nameof(Complaints));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditComplaint(Complaint model)
        {

            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>> Complaint Model ID: {model.Id}");

            if (ModelState.IsValid)
            {
                var dbComplaint = _context.Complaints.Where(c => c.Id == model.Id).FirstOrDefault();
                if(dbComplaint != null)
                {
                    dbComplaint.Title = model.Title;
                    dbComplaint.ComplaintBody = model.ComplaintBody;
                    dbComplaint.Status = model.Status;
                    dbComplaint.UpdateAt = DateTime.Now;
                    _context.Update(dbComplaint);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Complaint, id: {dbComplaint.Id}, updated");
                    return RedirectToAction(nameof(Complaints));
                }
            }
            return View(nameof(Complaints));
        }

        #endregion

        #region Meeting Requests
        [HttpGet]
        public ActionResult MeetingRequest()
        {
            ViewBag.Parent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault().Id;
            ViewBag.Teachers = _context.Educator.Where(e => e.Archived == false).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MeetingRequest(MeetingRequest model)
        {
            if (ModelState.IsValid)
            {
                var meetingRequest = new MeetingRequest
                {
                    ParentId = model.ParentId,
                    EducatorId = model.EducatorId,
                    Reason = model.Reason,
                    MeetingDate = model.MeetingDate,
                    Status = MeetingStatus.Pending,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now
                };
                _context.Add(meetingRequest);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Meeting Request, id: {meetingRequest.Id}, created!");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        #endregion

        #region Invite Parent

        [HttpGet]
        public ActionResult InviteParent()
        {
            ViewBag.Parent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault().Id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InviteParent(InviteParent invite)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation($"Invitation Sent to {invite.ParentEmail}");
                return RedirectToAction(nameof(Index));
            }
            return View(invite);
        }

        #endregion

        [HttpGet]
        public ActionResult Children()
        {
            List<StudentUser> children = new List<StudentUser>();
            var dbParent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();
            var students = _context.Students.Where(s => s.Archived == false).Include(s => s.Parents).ToList();
            foreach (var student in students)
            {
                foreach (var parent in student.Parents)
                {
                    if(parent.ParentId == dbParent.Id)
                    {
                        children.Add(student);
                    }
                }
            }

            return View(children);
        }

        [HttpGet]
        public ActionResult AddChild()
        {
            ViewBag.Parent = _context.Parents.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault().Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddChild(AddStudentModel model)
        {
            if (ModelState.IsValid)
            {
                var studentId = _context.Students.Where(s => s.EmailAddress == model.StudentEmail).FirstOrDefault().Id;
                var studentParent = new StudentParent
                {
                    ParentId = model.ParentId,
                    StudentId = studentId,
                    
                };
                _context.Add(studentParent);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Relationship created between parent: {model.ParentId} and student: {studentId}");
                return RedirectToAction(nameof(Children));
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
