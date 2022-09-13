using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTut.Data;
using iTut.Models.HOD;

namespace iTut.Areas.HOD.Controllers
{
    [Area("HOD")]
    public class GuardiansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuardiansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guardians
        [Route("/HOD/Guardians")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Guardian.Include(g => g.GuardianType).Include(g => g.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Guardians/Details/5
        [Route("/HOD/Guardians/Details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardian
                .Include(g => g.GuardianType)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // GET: Guardians/Create
        [Route("/HOD/Guardians/Create")]

        public IActionResult Create()
        {
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            return View();
        }

        // POST: Guardians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/Guardians/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Email,NID,GuardianTypeId,StudentId")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        // GET: Guardians/Edit/5
        [Route("/HOD/Guardians/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardian.FindAsync(id);
            if (guardian == null)
            {
                return NotFound();
            }
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        // POST: Guardians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/Guardians/Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Email,NID,GuardianTypeId,StudentId")] Guardian guardian)
        {
            if (id != guardian.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardianExists(guardian.Id))
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
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        // GET: Guardians/Delete/5
        [Route("/HOD/Guardians/Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardian
                .Include(g => g.GuardianType)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // POST: Guardians/Delete/5
        [Route("/HOD/Guardians/Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guardian = await _context.Guardian.FindAsync(id);
            _context.Guardian.Remove(guardian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuardianExists(int id)
        {
            return _context.Guardian.Any(e => e.Id == id);
        }
    }
}
