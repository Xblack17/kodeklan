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
    public class GuardianTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuardianTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GuardianTypes
        [Route("/HOD/GuardianTypes")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.GuardianType.ToListAsync());
        }

        // GET: GuardianTypes/Details/5
        [Route("/HOD/GuardianTypes/Details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardianType = await _context.GuardianType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardianType == null)
            {
                return NotFound();
            }

            return View(guardianType);
        }

        // GET: GuardianTypes/Create
        [Route("/HOD/GuardianTypes/Create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: GuardianTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/GuardianTypes/Create")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] GuardianType guardianType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardianType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guardianType);
        }

        // GET: GuardianTypes/Edit/5
        [Route("/HOD/GuardianTypes/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardianType = await _context.GuardianType.FindAsync(id);
            if (guardianType == null)
            {
                return NotFound();
            }
            return View(guardianType);
        }

        // POST: GuardianTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/GuardianTypes/Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] GuardianType guardianType)
        {
            if (id != guardianType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardianType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardianTypeExists(guardianType.Id))
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
            return View(guardianType);
        }

        // GET: GuardianTypes/Delete/5
        [Route("/HOD/GuardianTypes/Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardianType = await _context.GuardianType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardianType == null)
            {
                return NotFound();
            }

            return View(guardianType);
        }

        // POST: GuardianTypes/Delete/5
        [Route("/HOD/GuardianTypes/Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guardianType = await _context.GuardianType.FindAsync(id);
            _context.GuardianType.Remove(guardianType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuardianTypeExists(int id)
        {
            return _context.GuardianType.Any(e => e.Id == id);
        }
    }
}
