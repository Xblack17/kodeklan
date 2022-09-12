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
    public class ExamTitlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamTitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExamTitles
        [Route("/HOD/ExamTitles")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExamTitle.Include(e => e.EducationLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExamTitles/Details/5
        [Route("/HOD/ExamTitles/Details")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTitle = await _context.ExamTitle
                .Include(e => e.EducationLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examTitle == null)
            {
                return NotFound();
            }

            return View(examTitle);
        }

        // GET: ExamTitles/Create
        [Route("/HOD/ExamTitles/Create")]

        public IActionResult Create()
        {
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame");
            return View();
        }

        // POST: ExamTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/ExamTitles/Create")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleName,EducationLevelId")] ExamTitle examTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        // GET: ExamTitles/Edit/5
        [Route("/HOD/ExamTitles/Edit")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTitle = await _context.ExamTitle.FindAsync(id);
            if (examTitle == null)
            {
                return NotFound();
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        // POST: ExamTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/ExamTitles/Edit")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleName,EducationLevelId")] ExamTitle examTitle)
        {
            if (id != examTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamTitleExists(examTitle.Id))
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
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        // GET: ExamTitles/Delete/5
        [Route("/HOD/ExamTitles/Delete")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTitle = await _context.ExamTitle
                .Include(e => e.EducationLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examTitle == null)
            {
                return NotFound();
            }

            return View(examTitle);
        }

        // POST: ExamTitles/Delete/5
        [Route("/HOD/ExamTitles/Delete")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examTitle = await _context.ExamTitle.FindAsync(id);
            _context.ExamTitle.Remove(examTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamTitleExists(int id)
        {
            return _context.ExamTitle.Any(e => e.Id == id);
        }
    }
}
