using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eczanesepeti2.Data;
using eczanesepeti2.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace eczanesepeti2.Controllers
{
    [Authorize]
    public class IlacController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public string Foto { get; private set; }

        public IlacController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Ilac
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ilac.Include(i => i.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ilac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilac
                .Include(i => i.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilac == null)
            {
                return NotFound();
            }

            return View(ilac);
        }

        // GET: Ilac/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "KategoriAd");
            return View();
        }

        // POST: Ilac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IlacAd,Bilgi,Fiyat,Foto,KategoriId")] Ilac ilac)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                Foto = @"\images\" + fileName + extension;

                    _context.Add(ilac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "KategoriAd", ilac.KategoriId);
            return View(ilac);
        }

        // GET: Ilac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilac.FindAsync(id);
            if (ilac == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", ilac.KategoriId);
            return View(ilac);
        }

        // POST: Ilac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IlacAd,Bilgi,Fiyat,Foto,KategoriId")] Ilac ilac)
        {
            if (id != ilac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlacExists(ilac.Id))
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
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", ilac.KategoriId);
            return View(ilac);
        }

        // GET: Ilac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilac
                .Include(i => i.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ilac == null)
            {
                return NotFound();
            }

            return View(ilac);
        }

        // POST: Ilac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilac = await _context.Ilac.FindAsync(id);
            _context.Ilac.Remove(ilac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlacExists(int id)
        {
            return _context.Ilac.Any(e => e.Id == id);
        }
    }
}
