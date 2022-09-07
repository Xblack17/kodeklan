using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using iTut.Data;
using iTut.Models.Users;

namespace iTut.Areas.HOD.Controllers
{
    [Area("HOD")]
    public class ParentUsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParentUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParentUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parents.ToListAsync());
        }

        // GET: ParentUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentUser = await _context.Parents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parentUser == null)
            {
                return NotFound();
            }

            return View(parentUser);
        }

        // GET: ParentUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParentUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,EmailAddress,PhoneNumber,Gender,Race,PhysicalAddress,CreatedOn,Archived")] ParentUser parentUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parentUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parentUser);
        }

        // GET: ParentUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentUser = await _context.Parents.FindAsync(id);
            if (parentUser == null)
            {
                return NotFound();
            }
            return View(parentUser);
        }

        // POST: ParentUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserId,FirstName,LastName,EmailAddress,PhoneNumber,Gender,Race,PhysicalAddress,CreatedOn,Archived")] ParentUser parentUser)
        {
            if (id != parentUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parentUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentUserExists(parentUser.Id))
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
            return View(parentUser);
        }

        // GET: ParentUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentUser = await _context.Parents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parentUser == null)
            {
                return NotFound();
            }

            return View(parentUser);
        }

        // POST: ParentUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parentUser = await _context.Parents.FindAsync(id);
            _context.Parents.Remove(parentUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentUserExists(string id)
        {
            return _context.Parents.Any(e => e.Id == id);
        }
    }
}
