using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eczanesepeti2.Models
{
    public class Eczane
    {
        public int Id { get; set; }
        public string EczaneAd { get; set; }
        public string? TelNo { get; set; }
        public int IlceId { get; set; }
        [ForeignKey("IlceId")]
        public Ilce Ilce { get; set; }
    }
}
