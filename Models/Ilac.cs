using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eczanesepeti2.Models
{
    public class Ilac
    {
        public int Id { get; set; }
        public string IlacAd { get; set; }
        public string Bilgi { get; set; }
        public double Fiyat { get; set; }
        public string Foto { get; set; }
        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public Kategori Kategori { get; set; }




    }
}
