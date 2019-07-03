using SanalMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalMarket.Areas.Admin.Controllers
{
    public class KategorilerController : AdminBaseController
    {
        // GET: Kategoriler
        public ActionResult Index()
        {
            return View(db.Kategoriler.ToList());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult Ekle(string kategoriAd)
        {
            db.Kategoriler.Add(new Kategori { KategoriAd = kategoriAd });
            db.SaveChanges();

            TempData["mesaj"] = "\"" + kategoriAd + "\" adlı kategori başarıyla eklendi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Duzenle(int id, string kategoriAd)
        {
            Kategori kat = db.Kategoriler.Find(id);
            kat.KategoriAd = kategoriAd;

            db.SaveChanges();

            TempData["mesaj"] = "\"" + kat.KategoriAd + "\" adlı kategori başarıyla düzenlendi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sil(int id)
        {
            Kategori kat = db.Kategoriler.Find(id);

            if (kat.Urunler.Count > 0)
            {
                TempData["hata"] = "\"" + kat.KategoriAd + "\" adlı kategori altında ürünler bulunduğundan silinemiyor.";

                return RedirectToAction("Index");
            }
            db.Kategoriler.Remove(kat);

            db.SaveChanges();

            TempData["mesaj"] = "\"" + kat.KategoriAd + "\" adlı kategori başarıyla silindi.";

            return RedirectToAction("Index");
        }
    }
}