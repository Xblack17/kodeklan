using Coravel.Queuing.Interfaces;
using iTut.Constants;
using iTut.Data;
using iTut.Models.Shared;
using iTut.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iTut.Controllers
{
    [Authorize]
    public class SharedController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SharedController> _logger;
        private readonly IQueue _queue;

        public SharedController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<SharedController> logger, IQueue queue)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _queue = queue;
        }

        [Route("/Parent/Timeline")]
        [Route("/Coordinator/Timeline")]
        public IActionResult CreateTimelinePost()
        {
            return View();
        }

        [Route("/Parent/Timeline")]
        [Route("/Coordinator/Timeline")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTimelinePost(TimelinePost post)
        {
            var user = await _userManager.GetUserAsync(User);
            if (await _userManager.IsInRoleAsync(user, RoleConstants.Parent.ToString()) || await _userManager.IsInRoleAsync(user, RoleConstants.SubjectCoordinator.ToString()))
            {
                if (ModelState.IsValid)
                {
                    var _post = new TimelinePost
                    {
                        PostTitle =  post.PostTitle,
                        PostContent = post.PostContent,
                        Likes = 0,
                        Comments = new List<PostComment>(),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };
                    _context.Add(_post);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Timeline Post, id: {_post.Id}, created!");
                    return Redirect("/");
                }
                return View("Error");
            }
            return View("Access Denied");
        }
    }
}
