using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTut.Data;
using iTut.Models.HOD.Leave;

namespace iTut.Areas.HOD.Controllers
{
    [Area("HOD")]
    public class LeaveTypeVMsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveTypeVMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveTypeVMs
        [Route("/HOD/LeaveTypeVMs")]

        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveTypeVM.ToListAsync());
        }

        // GET: LeaveTypeVMs/Details/5
        [Route("/HOD/LeaveTypeVMs/Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTypeVM = await _context.LeaveTypeVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveTypeVM == null)
            {
                return NotFound();
            }

            return View(leaveTypeVM);
        }

        // GET: LeaveTypeVMs/Create
        [Route("/HOD/LeaveTypeVMs/Create")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/LeaveTypeVMs/Create")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DefaultDays,DateCreated")] LeaveTypeVM leaveTypeVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leaveTypeVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        // GET: LeaveTypeVMs/Edit/5
        [Route("/HOD/LeaveTypeVMs/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTypeVM = await _context.LeaveTypeVM.FindAsync(id);
            if (leaveTypeVM == null)
            {
                return NotFound();
            }
            return View(leaveTypeVM);
        }

        // POST: LeaveTypeVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/LeaveTypeVMs/Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DefaultDays,DateCreated")] LeaveTypeVM leaveTypeVM)
        {
            if (id != leaveTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveTypeVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeVMExists(leaveTypeVM.Id))
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
            return View(leaveTypeVM);
        }

        // GET: LeaveTypeVMs/Delete/5
        [Route("/HOD/LeaveTypeVMs/Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveTypeVM = await _context.LeaveTypeVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveTypeVM == null)
            {
                return NotFound();
            }

            return View(leaveTypeVM);
        }

        // POST: LeaveTypeVMs/Delete/5
        [Route("/HOD/LeaveTypeVMs/Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveTypeVM = await _context.LeaveTypeVM.FindAsync(id);
            _context.LeaveTypeVM.Remove(leaveTypeVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeVMExists(int id)
        {
            return _context.LeaveTypeVM.Any(e => e.Id == id);
        }
    }
}
