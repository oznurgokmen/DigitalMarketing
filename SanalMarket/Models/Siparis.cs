using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SanalMarket.Models
{
    [Table("Siparisler")]

    public class Siparis
    {
        public int Id { get; set; }

        [ForeignKey("Musteri")]
        public string MusteriId { get; set; }

        public string Ad { get; set; }

        public string Soyad { get; set; }

        [Required]
        [EmailAddress]

        public string Email { get; set; }

        public string Adres1 { get; set; }

        public string Adres2 { get; set; }

        public int? SehirId { get; set; }

        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Geçersiz Posta Kodu")]

        public string PostaKodu { get; set; }

        public bool AdresimeGonder { get; set; }

        [Required]

        public DateTime? SiparisVerilmeZamani { get; set; }

        public decimal OdemeTutari { get; set; }

        public virtual ApplicationUser Musteri { get; set; }

        public virtual List<SiparisDetay> SiparisDetaylar { get; set; }

        public virtual Sehir Sehir { get; set; }

    }
}