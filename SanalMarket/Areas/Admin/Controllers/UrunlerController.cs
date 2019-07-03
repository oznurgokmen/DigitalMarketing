using SanalMarket.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalMarket.Areas.Admin.Controllers
{
    public class UrunlerController : AdminBaseController
    {
        // GET: Urunler
        public ActionResult Index()
        {
            return View(db.Urunler.ToList());
        }

        //GET: Urunler/Ekle
        public ActionResult Ekle()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Ekle(Urun urun, HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                //Dosya Kaydetme Kısmı

                if (dosya != null && dosya.ContentLength > 0)
                {
                    var ext = Path.GetExtension(dosya.FileName);

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (dosya.ContentType.StartsWith("image/") && allowedExtensions.Contains(ext))
                    {
                        string yeniDosyaAd = Guid.NewGuid().ToString() + ext;

                        string kaydetmeYolu = Server.MapPath("~/img/" + yeniDosyaAd);

                        dosya.SaveAs(kaydetmeYolu);

                        urun.ResimAd = yeniDosyaAd;
                    }
                }

                db.Urunler.Add(urun);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sil(int id)
        {
            Urun urun = db.Urunler.Find(id);

            if (urun != null)
            {
                db.Urunler.Remove(urun);
                db.SaveChanges();

                if (!string.IsNullOrEmpty(urun.ResimAd))
                {
                    var resimYolu = Server.MapPath("~/img/" + urun.ResimAd);

                    if (System.IO.File.Exists(resimYolu))
                    {
                        System.IO.File.Delete(resimYolu);
                    }
                }
            }

            return RedirectToAction("Index", new { sonuc = "silindi" });
        }

        public ActionResult Duzenle(int id)
        {
            Urun urun = db.Urunler.Find(id);

            if (urun == null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Duzenle(Urun urun, HttpPostedFileBase dosya)
        {
            if (ModelState.IsValid)
            {
                Urun dbdekiEskiUrun = db.Urunler.Find(urun.Id);

                //Dosya Kaydetme Kısmı

                if (dosya != null && dosya.ContentLength >0 && dosya.ContentLength < 2*1024*1024)
                {
                    // Yüklenen dosya resim mi?

                    var ext = Path.GetExtension(dosya.FileName);

                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (!dosya.ContentType.StartsWith("image/") || !allowedExtensions.Contains(ext))
                    {
                        ModelState.AddModelError("ResimAd", "Geçersiz bir resim dosyası yüklediniz.");
                    }

                    // Eğer Form verilerinde hala hata yoksa

                    if (ModelState.IsValid)
                    {
                        // Yeni dosye geldiğine göre eski dosya varsa silelim

                        if (!string.IsNullOrEmpty(dbdekiEskiUrun.ResimAd))
                        {
                            var dbdekiEskiUrunResimYol = Server.MapPath("~/img/" + dbdekiEskiUrun.ResimAd);

                            if (System.IO.File.Exists(dbdekiEskiUrunResimYol))
                            {
                                System.IO.File.Delete(dbdekiEskiUrunResimYol);
                            }
                        }

                        // Yeni resmin kaydedilmesi

                        string yeniDosyaAd = Guid.NewGuid().ToString() + ext;

                        string kaydetmeYolu = Server.MapPath("~/img/" + yeniDosyaAd);

                        dosya.SaveAs(kaydetmeYolu);

                        dbdekiEskiUrun.ResimAd = yeniDosyaAd;
                    }
                }

                if (ModelState.IsValid)
                {
                    // Gelen ürün bilgilerini DB deki eski ürün bilgilerine aktar.

                    dbdekiEskiUrun.UrunAd = urun.UrunAd;
                    dbdekiEskiUrun.KategoriId = urun.KategoriId;
                    dbdekiEskiUrun.Birimfiyat = urun.Birimfiyat;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            //Gönderilen formda hata varsa aynı verilerle ve hata mesajlarıyla formu tekrar aç.

            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View(urun);
        }
    }
}