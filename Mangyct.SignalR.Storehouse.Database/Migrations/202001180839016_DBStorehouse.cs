namespace Mangyct.SignalR.Storehouse.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBStorehouse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountStores",
                c => new
                    {
                        CountId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PriceId = c.Int(nullable: false),
                        DateEdited = c.DateTime(nullable: false),
                        CountUp = c.Int(),
                        CountDown = c.Int(),
                    })
                .PrimaryKey(t => t.CountId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.PriceStores", t => t.PriceId, cascadeDelete: false)
                .Index(t => t.ProductId)
                .Index(t => t.PriceId);
            
            CreateTable(
                "dbo.PriceStores",
                c => new
                    {
                        PriceId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        DateEdited = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PriceId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                        PriceId = c.Int(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountStores", "PriceId", "dbo.PriceStores");
            DropForeignKey("dbo.PriceStores", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CountStores", "ProductId", "dbo.Products");
            DropIndex("dbo.PriceStores", new[] { "ProductId" });
            DropIndex("dbo.CountStores", new[] { "PriceId" });
            DropIndex("dbo.CountStores", new[] { "ProductId" });
            DropTable("dbo.Products");
            DropTable("dbo.PriceStores");
            DropTable("dbo.CountStores");
        }
    }
}
