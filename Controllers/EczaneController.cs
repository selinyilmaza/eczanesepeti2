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
    public class EczaneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EczaneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eczane
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Eczane.Include(e => e.Ilce);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Eczane/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eczane = await _context.Eczane
                .Include(e => e.Ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eczane == null)
            {
                return NotFound();
            }

            return View(eczane);
        }

        // GET: Eczane/Create
        public IActionResult Create()
        {
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "IlceAd");
            return View();
        }

        // POST: Eczane/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EczaneAd,TelNo,IlceId")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eczane);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "Id", eczane.IlceId);
            return View(eczane);
        }

        // GET: Eczane/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eczane = await _context.Eczane.FindAsync(id);
            if (eczane == null)
            {
                return NotFound();
            }
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "Id", eczane.IlceId);
            return View(eczane);
        }

        // POST: Eczane/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EczaneAd,TelNo,IlceId")] Eczane eczane)
        {
            if (id != eczane.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eczane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EczaneExists(eczane.Id))
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
            ViewData["IlceId"] = new SelectList(_context.Ilce, "Id", "Id", eczane.IlceId);
            return View(eczane);
        }

        // GET: Eczane/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eczane = await _context.Eczane
                .Include(e => e.Ilce)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eczane == null)
            {
                return NotFound();
            }

            return View(eczane);
        }

        // POST: Eczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eczane = await _context.Eczane.FindAsync(id);
            _context.Eczane.Remove(eczane);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EczaneExists(int id)
        {
            return _context.Eczane.Any(e => e.Id == id);
        }
    }
}
