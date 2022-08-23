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
    public class JobInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobInfoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobInfo.Include(j => j.Designation).Include(j => j.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInfo = await _context.JobInfo
                .Include(j => j.Designation)
                .Include(j => j.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobInfo == null)
            {
                return NotFound();
            }

            return View(jobInfo);
        }

        // GET: JobInfoes/Create
        public IActionResult Create()
        {
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email");
            return View();
        }

        // POST: JobInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DesignationId,DOJ,Salary,TotalLeave,Appointment,AppointmentExt,EmployeeId")] JobInfo jobInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // GET: JobInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInfo = await _context.JobInfo.FindAsync(id);
            if (jobInfo == null)
            {
                return NotFound();
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // POST: JobInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DesignationId,DOJ,Salary,TotalLeave,Appointment,AppointmentExt,EmployeeId")] JobInfo jobInfo)
        {
            if (id != jobInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobInfoExists(jobInfo.Id))
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
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // GET: JobInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInfo = await _context.JobInfo
                .Include(j => j.Designation)
                .Include(j => j.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobInfo == null)
            {
                return NotFound();
            }

            return View(jobInfo);
        }

        // POST: JobInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobInfo = await _context.JobInfo.FindAsync(id);
            _context.JobInfo.Remove(jobInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobInfoExists(int id)
        {
            return _context.JobInfo.Any(e => e.Id == id);
        }
    }
}
