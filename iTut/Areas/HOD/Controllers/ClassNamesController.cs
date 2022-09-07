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
    public class ClassNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClassName.ToListAsync());
        }

        // GET: ClassNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var className = await _context.ClassName
                .FirstOrDefaultAsync(m => m.ID == id);
            if (className == null)
            {
                return NotFound();
            }

            return View(className);
        }

        // GET: ClassNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] ClassName className)
        {
            if (ModelState.IsValid)
            {
                _context.Add(className);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(className);
        }

        // GET: ClassNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var className = await _context.ClassName.FindAsync(id);
            if (className == null)
            {
                return NotFound();
            }
            return View(className);
        }

        // POST: ClassNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] ClassName className)
        {
            if (id != className.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(className);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassNameExists(className.ID))
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
            return View(className);
        }

        // GET: ClassNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var className = await _context.ClassName
                .FirstOrDefaultAsync(m => m.ID == id);
            if (className == null)
            {
                return NotFound();
            }

            return View(className);
        }

        // POST: ClassNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var className = await _context.ClassName.FindAsync(id);
            _context.ClassName.Remove(className);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassNameExists(int id)
        {
            return _context.ClassName.Any(e => e.ID == id);
        }
    }
}
