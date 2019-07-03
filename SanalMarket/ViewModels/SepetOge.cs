using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalMarket.ViewModels
{
    public class SepetOge
    {
        public string Id { get; set; }

        public int UrunId { get; set; }

        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public string ResimAd { get; set; }

        public int Adet { get; set; }

        public decimal Tutar()
        {
            return Adet * BirimFiyat;
        }
    }
}