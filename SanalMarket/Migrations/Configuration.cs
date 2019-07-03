namespace SanalMarket.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using SanalMarket.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SanalMarket.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SanalMarket.Models.ApplicationDbContext context)
        {
            if (!context.Kategoriler.Any())
            {
                context.Kategoriler.Add(new Models.Kategori { KategoriAd = "Telefon" });
                context.Kategoriler.Add(new Models.Kategori { KategoriAd = "Bilgisayar" });
                context.Kategoriler.Add(new Models.Kategori { KategoriAd = "Beyaz Eþya" });
                context.Kategoriler.Add(new Models.Kategori { KategoriAd = "Mobilya" });
            }

            if (false)
            {
                // Ürün Sayýsýný 60'a Çýkar
                List<int> kategoriIdler = context
                    .Kategoriler
                    .Select(x => x.Id)
                    .ToList();

                Random rnd = new Random();
                var urunSayisi = context.Urunler.Count();
                var adet = 60 - urunSayisi;

                for (int i = 0; i < adet; i++)
                {
                    context.Urunler.Add(new Urun
                    {
                        UrunAd = "Ürün No " + i,
                        Birimfiyat = rnd.Next(1, 100),
                        KategoriId = kategoriIdler[rnd.Next(0, kategoriIdler.Count)]
                    });
                }
            }

            if (!context.Sehirler.Any())
            {
                context.Sehirler.Add(new Sehir { Id = 6, SehirAd = "Ankara" });
                context.Sehirler.Add(new Sehir { Id = 34, SehirAd = "Ýstanbul" });
                context.Sehirler.Add(new Sehir { Id = 35, SehirAd = "Ýzmir" });
            }

            if (!context.Roles.Any(r => r.Name == "yonetici"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "yonetici" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "admin@example.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin@example.com", Email = "admin@example.com" };

                // user kullanýcýsýný Ankara1. parolasýyla oluþtur
                manager.Create(user, "Ankara1.");

                //.... id'li kullanýcý ... rolüne ata
                manager.AddToRole(user.Id, "yonetici");
            }
        }
    }
}
