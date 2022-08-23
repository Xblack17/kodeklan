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
    public class EmploymentHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmploymentHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmploymentHistories
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmploymentHistory.Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmploymentHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistory
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentHistory == null)
            {
                return NotFound();
            }

            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email");
            return View();
        }

        // POST: EmploymentHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyName,CompanyLocation,Designation,From,To,EmployeeId")] EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employmentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistory.FindAsync(id);
            if (employmentHistory == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // POST: EmploymentHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,CompanyLocation,Designation,From,To,EmployeeId")] EmploymentHistory employmentHistory)
        {
            if (id != employmentHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employmentHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentHistoryExists(employmentHistory.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistory
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentHistory == null)
            {
                return NotFound();
            }

            return View(employmentHistory);
        }

        // POST: EmploymentHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employmentHistory = await _context.EmploymentHistory.FindAsync(id);
            _context.EmploymentHistory.Remove(employmentHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentHistoryExists(int id)
        {
            return _context.EmploymentHistory.Any(e => e.Id == id);
        }
    }
}
