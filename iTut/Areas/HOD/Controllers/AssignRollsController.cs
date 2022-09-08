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
    public class AssignRollsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignRollsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignRolls
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssignRoll.Include(a => a.Session).Include(a => a.Student).Include(a => a.StudentClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssignRolls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignRoll = await _context.AssignRoll
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignRoll == null)
            {
                return NotFound();
            }

            return View(assignRoll);
        }

        // GET: AssignRolls/Create
        public IActionResult Create()
        {
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id");
            return View();
        }

        // POST: AssignRolls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Roll,SessionId,StudentClassId,StudentId")] AssignRoll assignRoll)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignRoll);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", assignRoll.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", assignRoll.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", assignRoll.StudentClassId);
            return View(assignRoll);
        }

        // GET: AssignRolls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignRoll = await _context.AssignRoll.FindAsync(id);
            if (assignRoll == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", assignRoll.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", assignRoll.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", assignRoll.StudentClassId);
            return View(assignRoll);
        }

        // POST: AssignRolls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Roll,SessionId,StudentClassId,StudentId")] AssignRoll assignRoll)
        {
            if (id != assignRoll.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignRoll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignRollExists(assignRoll.Id))
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
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", assignRoll.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", assignRoll.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", assignRoll.StudentClassId);
            return View(assignRoll);
        }

        // GET: AssignRolls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignRoll = await _context.AssignRoll
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignRoll == null)
            {
                return NotFound();
            }

            return View(assignRoll);
        }

        // POST: AssignRolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignRoll = await _context.AssignRoll.FindAsync(id);
            _context.AssignRoll.Remove(assignRoll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignRollExists(int id)
        {
            return _context.AssignRoll.Any(e => e.Id == id);
        }
    }
}
