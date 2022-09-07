using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTut.Data;
using iTut.Models.Users;

namespace iTut.Areas.HOD.Controllers
{
    [Area("HOD")]
    public class StudentUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: StudentUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentUser = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentUser == null)
            {
                return NotFound();
            }

            return View(studentUser);
        }

        // GET: StudentUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,EmailAddress,PhoneNumber,Grade,Gender,Race,PhysicalAddress,CreatedOn,Archived")] StudentUser studentUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentUser);
        }

        // GET: StudentUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentUser = await _context.Students.FindAsync(id);
            if (studentUser == null)
            {
                return NotFound();
            }
            return View(studentUser);
        }

        // POST: StudentUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,FirstName,LastName,EmailAddress,PhoneNumber,Grade,Gender,Race,PhysicalAddress,CreatedOn,Archived")] StudentUser studentUser)
        {
            if (id != studentUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentUserExists(studentUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(studentUser);
        }

        // GET: StudentUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentUser = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentUser == null)
            {
                return NotFound();
            }

            return View(studentUser);
        }

        // POST: StudentUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var studentUser = await _context.Students.FindAsync(id);
            _context.Students.Remove(studentUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentUserExists(string id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
