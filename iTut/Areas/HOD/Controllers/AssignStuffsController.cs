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
    public class AssignStuffsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignStuffsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignStuffs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AssignStuff.ToListAsync());
        }

        // GET: AssignStuffs/Details/5
        [Route("/AssignStuffs/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignStuff = await _context.AssignStuff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignStuff == null)
            {
                return NotFound();
            }

            return View(assignStuff);
        }

        // GET: AssignStuffs/Create
        [Route("/AssignStuffs/Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AssignStuffs/Create
        [Route("/AssignStuffs/Create")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] AssignStuff assignStuff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignStuff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(assignStuff);
        }

        // GET: AssignStuffs/Edit/5
        [Route("/AssignStuffs/Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignStuff = await _context.AssignStuff.FindAsync(id);
            if (assignStuff == null)
            {
                return NotFound();
            }
            return View(assignStuff);
        }

        // POST: AssignStuffs/Edit/5
        [Route("/AssignStuffs/Edit")]
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] AssignStuff assignStuff)
        {
            if (id != assignStuff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignStuff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignStuffExists(assignStuff.Id))
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
            return View(assignStuff);
        }

        // GET: AssignStuffs/Delete/5
        [Route("/AssignStuffs/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignStuff = await _context.AssignStuff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignStuff == null)
            {
                return NotFound();
            }

            return View(assignStuff);
        }

        // POST: AssignStuffs/Delete/5
        [Route("/AssignStuffs/Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignStuff = await _context.AssignStuff.FindAsync(id);
            _context.AssignStuff.Remove(assignStuff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignStuffExists(int id)
        {
            return _context.AssignStuff.Any(e => e.Id == id);
        }
    }
}
