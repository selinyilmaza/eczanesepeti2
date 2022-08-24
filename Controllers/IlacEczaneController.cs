using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eczanesepeti2.Data;
using eczanesepeti2.Models;
using Microsoft.AspNetCore.Authorization;

namespace eczanesepeti2.Controllers
{
    [Authorize]
    public class IlacEczaneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IlacEczaneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IlacEczane
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IlacEczane.Include(i => i.Eczane).Include(i => i.Ilac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: IlacEczane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilacEczane = await _context.IlacEczane
                .Include(i => i.Eczane)
                .Include(i => i.Ilac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilacEczane == null)
            {
                return NotFound();
            }

            return View(ilacEczane);
        }

        // GET: IlacEczane/Create
        public IActionResult Create()
        {
            ViewData["EczaneId"] = new SelectList(_context.Eczane, "Id", "EczaneAd");
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "IlacAd");
            return View();
        }

        // POST: IlacEczane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IlacId,EczaneId,Sira")] IlacEczane ilacEczane)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilacEczane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EczaneId"] = new SelectList(_context.Eczane, "Id", "EczaneAd", ilacEczane.EczaneId);
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "IlacAd", ilacEczane.IlacId);
            return View(ilacEczane);
        }

        // GET: IlacEczane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilacEczane = await _context.IlacEczane.FindAsync(id);
            if (ilacEczane == null)
            {
                return NotFound();
            }
            ViewData["EczaneId"] = new SelectList(_context.Eczane, "Id", "EczaneAd", ilacEczane.EczaneId);
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "IlacAd", ilacEczane.IlacId);
            return View(ilacEczane);
        }

        // POST: IlacEczane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IlacId,EczaneId,Sira")] IlacEczane ilacEczane)
        {
            if (id != ilacEczane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilacEczane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlacEczaneExists(ilacEczane.Id))
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
            ViewData["EczaneId"] = new SelectList(_context.Eczane, "Id", "EczaneAd", ilacEczane.EczaneId);
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "IlacAd", ilacEczane.IlacId);
            return View(ilacEczane);
        }

        // GET: IlacEczane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilacEczane = await _context.IlacEczane
                .Include(i => i.Eczane)
                .Include(i => i.Ilac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilacEczane == null)
            {
                return NotFound();
            }

            return View(ilacEczane);
        }

        // POST: IlacEczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilacEczane = await _context.IlacEczane.FindAsync(id);
            _context.IlacEczane.Remove(ilacEczane);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlacEczaneExists(int id)
        {
            return _context.IlacEczane.Any(e => e.Id == id);
        }
    }
}
