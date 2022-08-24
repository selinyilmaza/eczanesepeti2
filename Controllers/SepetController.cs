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
    public class SepetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SepetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sepet
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Sepet.Include(s => s.Ilac);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sepet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.Ilac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // GET: Sepet/Create
        public IActionResult Create()
        {
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "IlacAd");
            return View();
        }

        // POST: Sepet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IlacId,Ucret")] Sepet sepet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sepet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "Id", sepet.IlacId);
            return View(sepet);
        }

        // GET: Sepet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet.FindAsync(id);
            if (sepet == null)
            {
                return NotFound();
            }
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "Id", sepet.IlacId);
            return View(sepet);
        }

        // POST: Sepet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IlacId,Ucret")] Sepet sepet)
        {
            if (id != sepet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sepet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SepetExists(sepet.Id))
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
            ViewData["IlacId"] = new SelectList(_context.Ilac, "Id", "Id", sepet.IlacId);
            return View(sepet);
        }

        // GET: Sepet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sepet = await _context.Sepet
                .Include(s => s.Ilac)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sepet == null)
            {
                return NotFound();
            }

            return View(sepet);
        }

        // POST: Sepet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sepet = await _context.Sepet.FindAsync(id);
            _context.Sepet.Remove(sepet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SepetExists(int id)
        {
            return _context.Sepet.Any(e => e.Id == id);
        }
    }
}
