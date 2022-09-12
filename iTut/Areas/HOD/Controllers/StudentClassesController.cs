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
    public class StudentClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentClasses
        [Route("/HOD/StudentClasses")]

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StudentClass.Include(s => s.ClassName).Include(s => s.Section).Include(s => s.Shift);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentClasses/Details/5
        [Route("/HOD/StudentClasses/Details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass
                .Include(s => s.ClassName)
                .Include(s => s.Section)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            return View(studentClass);
        }

        // GET: StudentClasses/Create
        [Route("/HOD/StudentClasses/Create")]

        public IActionResult Create()
        {
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name");
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name");
            return View();
        }

        // POST: StudentClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/StudentClasses/Create")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassNameId,SectionId,ShiftId")] StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", studentClass.ClassNameId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name", studentClass.SectionId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", studentClass.ShiftId);
            return View(studentClass);
        }

        // GET: StudentClasses/Edit/5
        [Route("/HOD/StudentClasses/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass.FindAsync(id);
            if (studentClass == null)
            {
                return NotFound();
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", studentClass.ClassNameId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name", studentClass.SectionId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", studentClass.ShiftId);
            return View(studentClass);
        }

        // POST: StudentClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/StudentClasses/Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassNameId,SectionId,ShiftId")] StudentClass studentClass)
        {
            if (id != studentClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentClassExists(studentClass.Id))
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
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", studentClass.ClassNameId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name", studentClass.SectionId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", studentClass.ShiftId);
            return View(studentClass);
        }

        // GET: StudentClasses/Delete/5
        [Route("/HOD/StudentClasses/Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass
                .Include(s => s.ClassName)
                .Include(s => s.Section)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            return View(studentClass);
        }

        // POST: StudentClasses/Delete/5
        [Route("/HOD/StudentClasses/Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentClass = await _context.StudentClass.FindAsync(id);
            _context.StudentClass.Remove(studentClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentClassExists(int id)
        {
            return _context.StudentClass.Any(e => e.Id == id);
        }
    }
}
