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
    public class AdmissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdmissionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admissions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Admission.Include(a => a.Group).Include(a => a.Session).Include(a => a.Student).Include(a => a.StudentClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admission
                .Include(a => a.Group)
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // GET: Admissions/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id");
            return View();
        }

        // POST: Admissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AdmissionDate,PreviousSchool,PreviousSchoolAddrs,PreviousSchoolDocument,Extension,SessionId,StudentClassId,GroupId,StudentId")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", admission.GroupId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", admission.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", admission.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", admission.StudentClassId);
            return View(admission);
        }

        // GET: Admissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admission.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", admission.GroupId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", admission.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", admission.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", admission.StudentClassId);
            return View(admission);
        }

        // POST: Admissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AdmissionDate,PreviousSchool,PreviousSchoolAddrs,PreviousSchoolDocument,Extension,SessionId,StudentClassId,GroupId,StudentId")] Admission admission)
        {
            if (id != admission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", admission.GroupId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", admission.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", admission.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", admission.StudentClassId);
            return View(admission);
        }

        // GET: Admissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admission
                .Include(a => a.Group)
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // POST: Admissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admission = await _context.Admission.FindAsync(id);
            _context.Admission.Remove(admission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionExists(int id)
        {
            return _context.Admission.Any(e => e.Id == id);
        }
    }
}
