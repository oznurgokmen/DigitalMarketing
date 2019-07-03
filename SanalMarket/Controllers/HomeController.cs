
using PagedList;
using SanalMarket.Models;
using SanalMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SanalMarket.Controllers
{
    //https://docs.microsoft.com/tr-tr/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Home

        // cid: category id
        public ActionResult Index(int? cid, int? page, string ara, string sirala = "az")
        {
            HomeIndexVM vm = new HomeIndexVM();

            IQueryable<Urun> urunlerSorgu = db.Urunler;

            // Eğer kategori seçilmişse (Url cid içeriyorsa)

            if (cid != null && cid != -1)
            {
                Kategori seciliKategori = db.Kategoriler.Find(cid);

                if (seciliKategori == null)
                {
                    return HttpNotFound();
                }

                vm.SeciliKategoriId = cid.Value;

                vm.SeciliKategoriAd = seciliKategori.KategoriAd;

                urunlerSorgu = urunlerSorgu.Where(x => x.KategoriId == cid);
            }

            if (!string.IsNullOrEmpty(ara))
            {
                // sql'cesi WHERE UrunAd LIKE '%' + ara + '%'

                urunlerSorgu = urunlerSorgu.Where(x => x.UrunAd.Contains(ara));

                ViewBag.ara = ara;
            }

            else
            {
                ViewBag.ara = "";
            }

            //vm.Urunler = urunlerSorgu.ToList();

            vm.Kategoriler = db.Kategoriler.ToList();

            //vm.Kategoriler.Insert(0, new Kategori { KategoriAd = "Tümü", Id = -1 });

            switch (sirala)
            {
                case "az":
                    urunlerSorgu = urunlerSorgu.OrderBy(x => x.UrunAd);
                    break;
                case "fiyatArtan":
                    urunlerSorgu = urunlerSorgu.OrderBy(x => x.Birimfiyat);
                    break;
                case "fiyatAzalan":
                    urunlerSorgu = urunlerSorgu.OrderByDescending(x => x.Birimfiyat);
                    break;
                default:
                    urunlerSorgu = urunlerSorgu.OrderBy(x => x.UrunAd);
                    sirala = "az";
                    break;
            }

            ViewBag.siralaSecenek = sirala;

            ViewBag.sirala = new SelectList(new List<SelectListItem>
            {
                new SelectListItem{ Value= "az", Text= "A-Z Sırala"},
                new SelectListItem{ Value= "fiyatArtan", Text= "Fiyat Artan"},
                new SelectListItem{ Value= "fiyatAzalan", Text= "Fiyat Azalan"}

            }, "Value", "Text", sirala);

            int pageSize = 6;
            int pageNumber = page ?? 1;

            vm.Urunler = urunlerSorgu.ToPagedList(pageNumber, pageSize);

            return View(vm);
        }

        public ActionResult Iletisim()
        {
            return View();
        }
    }
}