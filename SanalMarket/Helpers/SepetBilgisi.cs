using SanalMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanalMarket.Helpers
{
    public static class SepetBilgisi
    {
        public static int OgeAdet()
        {         
            return Ogeler().Sum(x => x.Adet);
        }

        public static List<SepetOge> Ogeler()
        {
            // Bu metodun kullanıldığı andaki Http İstek/Cevap Bilgilerini taşır.

            HttpContext context = HttpContext.Current;

            if (context.Session["sepet"] == null)
            {
                context.Session["sepet"] = new List<SepetOge>();
            }

            return (List<SepetOge>)context.Session["sepet"];
        }

        public static decimal ToplamTutar()
        {
            return Ogeler().Sum(x => x.Tutar());
        }
    }
}