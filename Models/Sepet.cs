using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eczanesepeti2.Models
{
    public class Sepet
    {
        public int Id { get; set; }
        public int IlacId { get; set; }
        [ForeignKey("IlacId")]
        public double Ucret { get; set; }
        public Ilac Ilac { get; set; }

    }
}
