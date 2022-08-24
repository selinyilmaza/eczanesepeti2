using eczanesepeti2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eczanesepeti2.Controllers
{
    public class gosteri : Controller
    {
        private readonly ApplicationDbContext _context;
        public gosteri(ApplicationDbContext context)

        {
            _context = context;
           
        }
        public IActionResult Index()
        {
            var ilaclist = (from g in _context.Ilac
                            select new IlacDTO
                            {
                                Adi = g.IlacAd,
                                Fotosu = g.Foto,
                                Fiyati = g.Fiyat,
                                Aciklama = g.Bilgi
                            })
                            .OrderBy(x => x.Fiyati).ToList();
             
                return View(ilaclist);
         
        }     
          
      
    }
    public class IlacDTO
    {
        public string Adi { get; internal set; }
        public string Fotosu { get; internal set; }
        public double Fiyati { get; internal set; }
        public string Aciklama { get; internal set; }

    }
}
