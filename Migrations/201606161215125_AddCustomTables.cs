namespace DCNE_Cake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomTables : DbMigration
    {
        public override void Up()
        {
           /* CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserId);*/

            CreateTable(
                "dbo.Complaint",
                c => new
                    {
                        ComplaintID = c.Int(nullable: false, identity: true),
                        ComplaintBox = c.String(nullable: false),
                        ComplaintDate = c.DateTime(nullable: false),
                        LocationID = c.Int(nullable: false),
                        ComplaintCatId = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ComplaintID)
                .ForeignKey("dbo.ComplaintCategory", t => t.ComplaintCatId, cascadeDelete: true)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .ForeignKey("dbo.Location", t => t.LocationID, cascadeDelete: true)
                .Index(t => t.ComplaintCatId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.LocationID);
            
            CreateTable(
                "dbo.ComplaintCategory",
                c => new
                    {
                        ComplaintCatId = c.Int(nullable: false, identity: true),
                        ComplaintCatName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ComplaintCatId);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PictureUrl = c.String(maxLength: 600),
                        ManufacturedDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(),
                        ProductCatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategory", t => t.ProductCatId, cascadeDelete: true)
                .Index(t => t.ProductCatId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ProductCatId = c.Int(nullable: false, identity: true),
                        ProductCat = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ProductCatId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Product", new[] { "ProductCatId" });
            DropIndex("dbo.Complaint", new[] { "LocationID" });
            DropIndex("dbo.Complaint", new[] { "Product_ProductId" });
            DropIndex("dbo.Complaint", new[] { "ComplaintCatId" });
            DropForeignKey("dbo.Product", "ProductCatId", "dbo.ProductCategory");
            DropForeignKey("dbo.Complaint", "LocationID", "dbo.Location");
            DropForeignKey("dbo.Complaint", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.Complaint", "ComplaintCatId", "dbo.ComplaintCategory");
            DropTable("dbo.Location");
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Product");
            DropTable("dbo.ComplaintCategory");
            DropTable("dbo.Complaint");
            DropColumn("dbo.UserProfile", "Email");
            DropIndex("dbo.UserProfile", "Email");
            //DropTable("dbo.UserProfile");
        }
    }
}
