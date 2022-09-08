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
    public class EducatorUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducatorUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EducatorUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Educators.ToListAsync());
        }

        // GET: EducatorUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educatorUser = await _context.Educators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educatorUser == null)
            {
                return NotFound();
            }

            return View(educatorUser);
        }

        // GET: EducatorUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducatorUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,EmailAddress,PhoneNumber,Gender,Race,PhysicalAddress,PrimarySubject,SecondarySubject,CreatedOn,Archived")] EducatorUser educatorUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educatorUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educatorUser);
        }

        // GET: EducatorUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educatorUser = await _context.Educators.FindAsync(id);
            if (educatorUser == null)
            {
                return NotFound();
            }
            return View(educatorUser);
        }

        // POST: EducatorUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,FirstName,LastName,EmailAddress,PhoneNumber,Gender,Race,PhysicalAddress,PrimarySubject,SecondarySubject,CreatedOn,Archived")] EducatorUser educatorUser)
        {
            if (id != educatorUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educatorUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducatorUserExists(educatorUser.Id))
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
            return View(educatorUser);
        }

        // GET: EducatorUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educatorUser = await _context.Educators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educatorUser == null)
            {
                return NotFound();
            }

            return View(educatorUser);
        }

        // POST: EducatorUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var educatorUser = await _context.Educators.FindAsync(id);
            _context.Educators.Remove(educatorUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducatorUserExists(string id)
        {
            return _context.Educators.Any(e => e.Id == id);
        }
    }
}
