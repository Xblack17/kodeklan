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
    public class EmployeeEducationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeEducationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeEducations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeEducation.Include(e => e.EducationLevel).Include(e => e.Employee).Include(e => e.ExamTitle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmployeeEducations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeEducation = await _context.EmployeeEducation
                .Include(e => e.EducationLevel)
                .Include(e => e.Employee)
                .Include(e => e.ExamTitle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeEducation == null)
            {
                return NotFound();
            }

            return View(employeeEducation);
        }

        // GET: EmployeeEducations/Create
        public IActionResult Create()
        {
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email");
            ViewData["ExamTitleId"] = new SelectList(_context.ExamTitle, "Id", "TitleName");
            return View();
        }

        // POST: EmployeeEducations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EducationLevelId,ExamTitleId,Major,InstituteName,ResultType,CGPA,Scale,Marks,PassingYear,Duration,Achievement,EmployeeId")] EmployeeEducation employeeEducation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeEducation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", employeeEducation.EducationLevelId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employeeEducation.EmployeeId);
            ViewData["ExamTitleId"] = new SelectList(_context.ExamTitle, "Id", "TitleName", employeeEducation.ExamTitleId);
            return View(employeeEducation);
        }

        // GET: EmployeeEducations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeEducation = await _context.EmployeeEducation.FindAsync(id);
            if (employeeEducation == null)
            {
                return NotFound();
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", employeeEducation.EducationLevelId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employeeEducation.EmployeeId);
            ViewData["ExamTitleId"] = new SelectList(_context.ExamTitle, "Id", "TitleName", employeeEducation.ExamTitleId);
            return View(employeeEducation);
        }

        // POST: EmployeeEducations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EducationLevelId,ExamTitleId,Major,InstituteName,ResultType,CGPA,Scale,Marks,PassingYear,Duration,Achievement,EmployeeId")] EmployeeEducation employeeEducation)
        {
            if (id != employeeEducation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeEducation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeEducationExists(employeeEducation.Id))
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
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", employeeEducation.EducationLevelId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employeeEducation.EmployeeId);
            ViewData["ExamTitleId"] = new SelectList(_context.ExamTitle, "Id", "TitleName", employeeEducation.ExamTitleId);
            return View(employeeEducation);
        }

        // GET: EmployeeEducations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeEducation = await _context.EmployeeEducation
                .Include(e => e.EducationLevel)
                .Include(e => e.Employee)
                .Include(e => e.ExamTitle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeEducation == null)
            {
                return NotFound();
            }

            return View(employeeEducation);
        }

        // POST: EmployeeEducations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeEducation = await _context.EmployeeEducation.FindAsync(id);
            _context.EmployeeEducation.Remove(employeeEducation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeEducationExists(int id)
        {
            return _context.EmployeeEducation.Any(e => e.Id == id);
        }
    }
}
