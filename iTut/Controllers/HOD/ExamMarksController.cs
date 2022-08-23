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
    public class ExamMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExamMarks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExamMark.Include(e => e.AssignRoll).Include(e => e.Course).Include(e => e.Session).Include(e => e.StudentClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExamMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examMark = await _context.ExamMark
                .Include(e => e.AssignRoll)
                .Include(e => e.Course)
                .Include(e => e.Session)
                .Include(e => e.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examMark == null)
            {
                return NotFound();
            }

            return View(examMark);
        }

        // GET: ExamMarks/Create
        public IActionResult Create()
        {
            ViewData["AssignRollId"] = new SelectList(_context.AssignRoll, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code");
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id");
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id");
            return View();
        }

        // POST: ExamMarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Theory,Mcq,Practical,Total,Grade,CourseId,SessionId,StudentClassId,AssignRollId")] ExamMark examMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AssignRollId"] = new SelectList(_context.AssignRoll, "Id", "Id", examMark.AssignRollId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code", examMark.CourseId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", examMark.SessionId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", examMark.StudentClassId);
            return View(examMark);
        }

        // GET: ExamMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examMark = await _context.ExamMark.FindAsync(id);
            if (examMark == null)
            {
                return NotFound();
            }
            ViewData["AssignRollId"] = new SelectList(_context.AssignRoll, "Id", "Id", examMark.AssignRollId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code", examMark.CourseId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", examMark.SessionId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", examMark.StudentClassId);
            return View(examMark);
        }

        // POST: ExamMarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Theory,Mcq,Practical,Total,Grade,CourseId,SessionId,StudentClassId,AssignRollId")] ExamMark examMark)
        {
            if (id != examMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamMarkExists(examMark.Id))
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
            ViewData["AssignRollId"] = new SelectList(_context.AssignRoll, "Id", "Id", examMark.AssignRollId);
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Code", examMark.CourseId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", examMark.SessionId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", examMark.StudentClassId);
            return View(examMark);
        }

        // GET: ExamMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examMark = await _context.ExamMark
                .Include(e => e.AssignRoll)
                .Include(e => e.Course)
                .Include(e => e.Session)
                .Include(e => e.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examMark == null)
            {
                return NotFound();
            }

            return View(examMark);
        }

        // POST: ExamMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examMark = await _context.ExamMark.FindAsync(id);
            _context.ExamMark.Remove(examMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamMarkExists(int id)
        {
            return _context.ExamMark.Any(e => e.Id == id);
        }
    }
}
