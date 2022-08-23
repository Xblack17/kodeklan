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
    public class LeaveRequestVMsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestVMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveRequestVMs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LeaveRequestVM.Include(l => l.LeaveType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveRequestVMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequestVM = await _context.LeaveRequestVM
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequestVM == null)
            {
                return NotFound();
            }

            return View(leaveRequestVM);
        }

        // GET: LeaveRequestVMs/Create
        public IActionResult Create()
        {
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name");
            return View();
        }

        // POST: LeaveRequestVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestingEmployeeId,StartDate,EndDate,LeaveTypeId,DateRequested,DateActioned,Approved,ApprovedById,Cancelled,RequestComments")] LeaveRequestVM leaveRequestVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveRequestVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name", leaveRequestVM.LeaveTypeId);
            return View(leaveRequestVM);
        }

        // GET: LeaveRequestVMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequestVM = await _context.LeaveRequestVM.FindAsync(id);
            if (leaveRequestVM == null)
            {
                return NotFound();
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name", leaveRequestVM.LeaveTypeId);
            return View(leaveRequestVM);
        }

        // POST: LeaveRequestVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestingEmployeeId,StartDate,EndDate,LeaveTypeId,DateRequested,DateActioned,Approved,ApprovedById,Cancelled,RequestComments")] LeaveRequestVM leaveRequestVM)
        {
            if (id != leaveRequestVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequestVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestVMExists(leaveRequestVM.Id))
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
            ViewData["LeaveTypeId"] = new SelectList(_context.Set<LeaveTypeVM>(), "Id", "Name", leaveRequestVM.LeaveTypeId);
            return View(leaveRequestVM);
        }

        // GET: LeaveRequestVMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveRequestVM = await _context.LeaveRequestVM
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequestVM == null)
            {
                return NotFound();
            }

            return View(leaveRequestVM);
        }

        // POST: LeaveRequestVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequestVM = await _context.LeaveRequestVM.FindAsync(id);
            _context.LeaveRequestVM.Remove(leaveRequestVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestVMExists(int id)
        {
            return _context.LeaveRequestVM.Any(e => e.Id == id);
        }
    }
}
