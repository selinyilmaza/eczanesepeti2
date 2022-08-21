using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eczanesepeti2.Models
{
    public class IlacEczane
    {
        public int Id { get; set; }
        public int IlacId { get; set; }
        public int EczaneId { get; set; }
        public int? Sira { get; set; }


        public Ilac Ilac { get; set; }
        public Eczane Eczane { get; set; }

    }
}
