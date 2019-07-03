using Microsoft.AspNet.Identity;
using SanalMarket.Helpers;
using SanalMarket.Models;
using SanalMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalMarket.Controllers
{
    public class SepetController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sepet
        public ActionResult Index()
        {
            SepetIndexVM vm = new SepetIndexVM();

            vm.Ogeler = (List<SepetOge>)(Session["sepet"] ?? new List<SepetOge>());

            vm.ToplamTutar = vm.Ogeler.Sum(x => x.Tutar());
           
            return View(vm);
        }

        [HttpPost]

        public ActionResult Ekle(int urunId, int adet = 1)
        {
            if (Session["sepet"] == null)
            {
                Session["sepet"] = new List<SepetOge>();
            }

            List<SepetOge> sepet = (List<SepetOge>)Session["sepet"];

            SepetOge sepetteki = sepet.FirstOrDefault(x => x.UrunId == urunId);

            // Eklenmek istenen ürün zaten sepette ise

            if (sepetteki != null)
            {
                sepetteki.Adet += adet;
            }
            else
            {
                Urun urun = db.Urunler.Find(urunId);

                SepetOge oge = new SepetOge
                {
                    Id = Guid.NewGuid().ToString(),

                    UrunId = urun.Id,
                    UrunAd = urun.UrunAd,
                    BirimFiyat = urun.Birimfiyat,
                    ResimAd = urun.ResimAd,
                    Adet = adet
                };

                sepet.Add(oge);
            }
      
            return Json(new { SepetUrunAdet = sepet.Sum(x => x.Adet) });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sil(string sepetOgeId)
        {
            if (Session["sepet"] != null)
            {
                var ogeler = (List<SepetOge>)Session["sepet"];

                var silinecek = ogeler.FirstOrDefault(x => x.Id == sepetOgeId);

                ogeler.Remove(silinecek);
            }

            return RedirectToAction("Index");
        }

        [Authorize]

        public ActionResult OdemeYap()
        {
            var vm = new OdemeYapVM();

            // Name E-mail ile aynı kaydettik
            vm.Email = User.Identity.Name;
            vm.AdresimeGonder = true;

            ViewBag.SehirId = new SelectList(db.Sehirler.ToList(), "Id", "SehirAd");

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
       
        public ActionResult OdemeYap(OdemeYapVM vm)
        {
            if (SepetBilgisi.OgeAdet() == 0)
            {
                ModelState.AddModelError("", "Sepetinizde hiçbir öğe bulunamamaktadır.");
            }

            if (ModelState.IsValid)
            {
                // Bu noktada kredi kartı bilgilerini kullanarak ödeme tutarını karttan çekebilirsiniz

                // Ödeme başarılı ise

                Siparis siparis = new Siparis
                {
                    MusteriId = User.Identity.GetUserId(),
                    Ad = vm.Ad,
                    Soyad = vm.Soyad,
                    Email = vm.Email,
                    Adres1 = vm.Adres1,
                    Adres2 = vm.Adres2,
                    SehirId = vm.SehirId,
                    PostaKodu = vm.PostaKodu,
                    AdresimeGonder = vm.AdresimeGonder,
                    SiparisVerilmeZamani = DateTime.Now,
                    OdemeTutari = SepetBilgisi.ToplamTutar()
                };

                siparis.SiparisDetaylar = new List<SiparisDetay>();

                foreach (var item in SepetBilgisi.Ogeler())
                {
                    siparis.SiparisDetaylar.Add(new SiparisDetay
                    {
                        UrunId = item.UrunId,
                        UrunAd = item.UrunAd,
                        BirimFiyat = item.BirimFiyat,
                        Adet = item.Adet,
                        Tutar = item.Tutar()
                    });
                }

                db.Siparisler.Add(siparis);

                db.SaveChanges();

                Session.Remove("sepet");  // sepeti boşalt

                return RedirectToAction("OdemeBasarili", new { id = siparis.Id});
            }

            ViewBag.SehirId = new SelectList(db.Sehirler.ToList(), "Id", "SehirAd");

            return View(vm);
        }

        public ActionResult OdemeBasarili(int id)
        {
            ViewBag.SiparisId = id;

            return View();
        }
    }
}