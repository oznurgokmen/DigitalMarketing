using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SanalMarket.Models
{
    [Table("SiparisDetaylar")]

    public class SiparisDetay
    {
        public int Id { get; set; }

        public int SiparisId { get; set; }

        public int UrunId { get; set; }

        [Required]
        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public int Adet { get; set; }

        public decimal Tutar { get; set; }

        public virtual Siparis Siparis { get; set; }

        public virtual Urun Urun { get; set; }

    }
}