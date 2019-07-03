namespace SanalMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SiparisTablolari : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiparisDetaylar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SiparisId = c.Int(nullable: false),
                        UrunId = c.Int(nullable: false),
                        UrunAd = c.String(nullable: false),
                        BirimFiyat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Adet = c.Int(nullable: false),
                        Tutar = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Siparisler", t => t.SiparisId, cascadeDelete: true)
                .ForeignKey("dbo.Urunler", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.SiparisId)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.Siparisler",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MusteriId = c.String(maxLength: 128),
                        Ad = c.String(),
                        Soyad = c.String(),
                        Email = c.String(nullable: false),
                        Adres1 = c.String(),
                        Adres2 = c.String(),
                        SehirId = c.Int(),
                        PostaKodu = c.String(),
                        AdresimeGonder = c.Boolean(nullable: false),
                        SiparisVerilmeZamani = c.DateTime(nullable: false),
                        OdemeTutari = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.MusteriId)
                .ForeignKey("dbo.Sehirler", t => t.SehirId)
                .Index(t => t.MusteriId)
                .Index(t => t.SehirId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SiparisDetaylar", "UrunId", "dbo.Urunler");
            DropForeignKey("dbo.SiparisDetaylar", "SiparisId", "dbo.Siparisler");
            DropForeignKey("dbo.Siparisler", "SehirId", "dbo.Sehirler");
            DropForeignKey("dbo.Siparisler", "MusteriId", "dbo.AspNetUsers");
            DropIndex("dbo.Siparisler", new[] { "SehirId" });
            DropIndex("dbo.Siparisler", new[] { "MusteriId" });
            DropIndex("dbo.SiparisDetaylar", new[] { "UrunId" });
            DropIndex("dbo.SiparisDetaylar", new[] { "SiparisId" });
            DropTable("dbo.Siparisler");
            DropTable("dbo.SiparisDetaylar");
        }
    }
}
