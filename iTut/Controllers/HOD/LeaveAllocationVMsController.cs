using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTut.Data;
using iTut.Models.HOD.Leave;

namespace iTut.Controllers.HOD
{
    public class LeaveAllocationVMsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveAllocationVMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveAllocationVMs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveAllocationVM.Include(l => l.LeaveType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveAllocationVMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveAllocationVM = await _context.LeaveAllocationVM
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveAllocationVM == null)
            {
                return NotFound();
            }

            return View(leaveAllocationVM);
        }

        // GET: LeaveAllocationVMs/Create
        public IActionResult Create()
        {
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name");
            return View();
        }

        // POST: LeaveAllocationVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumberOfDays,DateCreated,Period,EmployeeId,LeaveTypeId")] LeaveAllocationVM leaveAllocationVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveAllocationVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name", leaveAllocationVM.LeaveTypeId);
            return View(leaveAllocationVM);
        }

        // GET: LeaveAllocationVMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveAllocationVM = await _context.LeaveAllocationVM.FindAsync(id);
            if (leaveAllocationVM == null)
            {
                return NotFound();
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name", leaveAllocationVM.LeaveTypeId);
            return View(leaveAllocationVM);
        }

        // POST: LeaveAllocationVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumberOfDays,DateCreated,Period,EmployeeId,LeaveTypeId")] LeaveAllocationVM leaveAllocationVM)
        {
            if (id != leaveAllocationVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveAllocationVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveAllocationVMExists(leaveAllocationVM.Id))
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
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name", leaveAllocationVM.LeaveTypeId);
            return View(leaveAllocationVM);
        }

        // GET: LeaveAllocationVMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveAllocationVM = await _context.LeaveAllocationVM
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveAllocationVM == null)
            {
                return NotFound();
            }

            return View(leaveAllocationVM);
        }

        // POST: LeaveAllocationVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveAllocationVM = await _context.LeaveAllocationVM.FindAsync(id);
            _context.LeaveAllocationVM.Remove(leaveAllocationVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveAllocationVMExists(int id)
        {
            return _context.LeaveAllocationVM.Any(e => e.Id == id);
        }
    }
}
