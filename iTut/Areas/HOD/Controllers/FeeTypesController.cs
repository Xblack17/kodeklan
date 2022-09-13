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
    public class FeeTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeeTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FeeTypes
        [Route("/HOD/FeeTypes")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeeType.ToListAsync());
        }

        // GET: FeeTypes/Details/5
        [Route("/HOD/FeeTypes/Details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeType = await _context.FeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feeType == null)
            {
                return NotFound();
            }

            return View(feeType);
        }

        // GET: FeeTypes/Create
        [Route("/HOD/FeeTypes/Create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: FeeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/FeeTypes/Create")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FeeType feeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feeType);
        }

        // GET: FeeTypes/Edit/5
        [Route("/HOD/FeeTypes/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeType = await _context.FeeType.FindAsync(id);
            if (feeType == null)
            {
                return NotFound();
            }
            return View(feeType);
        }

        // POST: FeeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/FeeTypes/Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FeeType feeType)
        {
            if (id != feeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeTypeExists(feeType.Id))
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
            return View(feeType);
        }

        // GET: FeeTypes/Delete/5
        [Route("/HOD/FeeTypes/Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeType = await _context.FeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feeType == null)
            {
                return NotFound();
            }

            return View(feeType);
        }

        // POST: FeeTypes/Delete/5
        [Route("/HOD/FeeTypes/Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feeType = await _context.FeeType.FindAsync(id);
            _context.FeeType.Remove(feeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeeTypeExists(int id)
        {
            return _context.FeeType.Any(e => e.Id == id);
        }
    }
}
