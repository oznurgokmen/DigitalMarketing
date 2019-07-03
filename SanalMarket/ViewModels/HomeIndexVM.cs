using PagedList;
using SanalMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalMarket.ViewModels
{
    public class HomeIndexVM
    {
        public HomeIndexVM()
        {
            SeciliKategoriId = -1;  // Varsayılan değer
        }

        public IPagedList<Urun> Urunler { get; set; }

        public List<Kategori> Kategoriler { get; set; }

        // Seçili değilse -1 olsun.

        public int SeciliKategoriId { get; set; }

        public string SeciliKategoriAd { get; set; }


    }
}