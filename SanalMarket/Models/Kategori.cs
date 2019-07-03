using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SanalMarket.Models
{
    [Table("Kategoriler")]

    public class Kategori
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [DisplayName("Kategori Adı")]

        public string KategoriAd { get; set; }

        public virtual List<Urun> Urunler { get; set; }

    }
}