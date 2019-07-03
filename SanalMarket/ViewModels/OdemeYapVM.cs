using SanalMarket.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SanalMarket.ViewModels
{
    public class OdemeYapVM
    {
        [DisplayName("Ad")]

        public string Ad { get; set; }

        [DisplayName("Soyad")]

        public string Soyad { get; set; }

        [Required]
        [EmailAddress]
        [DisplayName("E-Mail")]

        public string Email { get; set; }

        [DisplayName("Adres")]

        public string Adres1 { get; set; }

        [DisplayName("Adres Satır 2")]

        public string Adres2 { get; set; }

        [Required]
        [DisplayName("Şehir")]

        public int? SehirId { get; set; }

        [DisplayName("Posta Kodu")]
        [RegularExpression("^[0-9]{5}$", ErrorMessage = "Geçersiz Posta Kodu")]

        public string PostaKodu { get; set; }

        [DisplayName("Faturayı adresime Gönder")]

        public bool AdresimeGonder { get; set; }

        [DisplayName("Gelecek sefer kullanmak üzere bilgilerimi hatırla")]

        public bool BilgileriHatirla { get; set; }

        [DisplayName("Kart Türü")]

        public KartTuru KKTur { get; set; }

        [DisplayName("Ad Soyad")]

        public string KKSahibi { get; set; }

        [DisplayName("Kart No")]

        public string KKNo { get; set; }

        [DisplayName("CVV")]

        public string KKCvv { get; set; }

        [DisplayName("SKT")]

        public string KKSonKullanmaTarihi { get; set; }


    }
}