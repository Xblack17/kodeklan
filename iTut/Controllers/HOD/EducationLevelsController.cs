using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTut.Data;
using iTut.Models.HOD;

namespace iTut.Controllers.HOD
{
    public class EducationLevelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationLevelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EducationLevels
        public async Task<IActionResult> Index()
        {
            return View(await _context.EducationLevel.ToListAsync());
        }

        // GET: EducationLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // GET: EducationLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EducationLevelNaame")] EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationLevel);
        }

        // GET: EducationLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevel.FindAsync(id);
            if (educationLevel == null)
            {
                return NotFound();
            }
            return View(educationLevel);
        }

        // POST: EducationLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EducationLevelNaame")] EducationLevel educationLevel)
        {
            if (id != educationLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationLevelExists(educationLevel.Id))
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
            return View(educationLevel);
        }

        // GET: EducationLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // POST: EducationLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationLevel = await _context.EducationLevel.FindAsync(id);
            _context.EducationLevel.Remove(educationLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationLevelExists(int id)
        {
            return _context.EducationLevel.Any(e => e.Id == id);
        }
    }
}
