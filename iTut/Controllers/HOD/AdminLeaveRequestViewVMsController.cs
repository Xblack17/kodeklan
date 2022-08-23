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
    public class AdminLeaveRequestViewVMsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminLeaveRequestViewVMsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminLeaveRequestViewVMs
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdminLeaveRequestViewVM.ToListAsync());
        }

        // GET: AdminLeaveRequestViewVMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminLeaveRequestViewVM == null)
            {
                return NotFound();
            }

            return View(adminLeaveRequestViewVM);
        }

        // GET: AdminLeaveRequestViewVMs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminLeaveRequestViewVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TotalRequests,ApprovedRequests,PendingRequests,RejectedRequests")] AdminLeaveRequestViewVM adminLeaveRequestViewVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminLeaveRequestViewVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("/View/HOD/AdminLeaveRequestViewVMs/Index");
        }

        // GET: AdminLeaveRequestViewVMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM.FindAsync(id);
            if (adminLeaveRequestViewVM == null)
            {
                return NotFound();
            }
            return View(adminLeaveRequestViewVM);
        }

        // POST: AdminLeaveRequestViewVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TotalRequests,ApprovedRequests,PendingRequests,RejectedRequests")] AdminLeaveRequestViewVM adminLeaveRequestViewVM)
        {
            if (id != adminLeaveRequestViewVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminLeaveRequestViewVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminLeaveRequestViewVMExists(adminLeaveRequestViewVM.Id))
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
            return View("/View/HOD/adminLeaveRequestViewVM/Edit");
        }

        // GET: AdminLeaveRequestViewVMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminLeaveRequestViewVM == null)
            {
                return NotFound();
            }

            return View("/View/HOD/adminLeaveRequestViewVM/Delete");
        }

        // POST: AdminLeaveRequestViewVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM.FindAsync(id);
            _context.AdminLeaveRequestViewVM.Remove(adminLeaveRequestViewVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminLeaveRequestViewVMExists(int id)
        {
            return _context.AdminLeaveRequestViewVM.Any(e => e.Id == id);
        }
    }
}
