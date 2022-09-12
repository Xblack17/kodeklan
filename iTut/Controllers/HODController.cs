using Coravel.Queuing.Interfaces;
using iTut.Constants;
using iTut.Data;
using iTut.Models.Users;
using iTut.Models.ViewModels.HOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace iTut.Controllers
{
    [Authorize(Roles = RoleConstants.HOD)]
    public class HODController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HODController> _logger;
        private readonly IQueue _queue;

        public HODController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<HODController> logger, IQueue queue)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _queue = queue;
        }

        public IActionResult Index()
        {
            var hod = _context.HODs.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();

            var viewModel = new HODIndexViewModel
            {
                HodUser = hod,
            };
            return View(viewModel);
        }
    }
}
