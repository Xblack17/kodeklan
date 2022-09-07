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
    public class ClassFeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassFees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClassFee.Include(c => c.ClassName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassFees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classFee = await _context.ClassFee
                .Include(c => c.ClassName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classFee == null)
            {
                return NotFound();
            }

            return View(classFee);
        }

        // GET: ClassFees/Create
        public IActionResult Create()
        {
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name");
            return View();
        }

        // POST: ClassFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdmissionFee,ClassNameId")] ClassFee classFee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // GET: ClassFees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classFee = await _context.ClassFee.FindAsync(id);
            if (classFee == null)
            {
                return NotFound();
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // POST: ClassFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdmissionFee,ClassNameId")] ClassFee classFee)
        {
            if (id != classFee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassFeeExists(classFee.Id))
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
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // GET: ClassFees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classFee = await _context.ClassFee
                .Include(c => c.ClassName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classFee == null)
            {
                return NotFound();
            }

            return View(classFee);
        }

        // POST: ClassFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classFee = await _context.ClassFee.FindAsync(id);
            _context.ClassFee.Remove(classFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassFeeExists(int id)
        {
            return _context.ClassFee.Any(e => e.Id == id);
        }
    }
}
