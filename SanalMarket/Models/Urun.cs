using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SanalMarket.Models
{
    [Table("Urunler")]

    public class Urun
    {
        public int Id { get; set; }

        [DisplayName("Kategori Adı")]

        public int KategoriId { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Ürün Adı")]

        public string UrunAd { get; set; }

        [DisplayName("Birim Fiyatı")]

        public decimal Birimfiyat { get; set; }

        [DisplayName("Resim")]

        public string ResimAd { get; set; }

        public virtual Kategori Kategori { get; set; }

        public virtual List<SiparisDetay> SiparisDetaylar { get; set; }

    }
}