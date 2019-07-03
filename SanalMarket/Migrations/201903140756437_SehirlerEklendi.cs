namespace SanalMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SehirlerEklendi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sehirler",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SehirAd = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sehirler");
        }
    }
}
