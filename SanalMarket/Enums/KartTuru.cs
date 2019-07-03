using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SanalMarket.Enums
{
    public enum KartTuru
    {
        [Display(Name = "Seçilmedi")]
        Secilmedi = 0,

        [Display(Name = "Kredi Kartı")]
        KrediKarti = 1,

        [Display(Name = "Banka Kartı")]
        BankaKarti = 2,

        [Display(Name = "PayPal")]
        PayPal = 3
    }
}