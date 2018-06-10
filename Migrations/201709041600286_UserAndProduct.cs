namespace DCNE_Cake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAndProduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Complaint", "Product_ProductId", "dbo.Product");
            DropIndex("dbo.Complaint", new[] { "Product_ProductId" });
            RenameColumn(table: "dbo.Complaint", name: "Product_ProductId", newName: "ProductId");
            AddForeignKey("dbo.Complaint", "ProductId", "dbo.Product", "ProductId", cascadeDelete: true);
            CreateIndex("dbo.Complaint", "ProductId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Complaint", new[] { "ProductId" });
            DropForeignKey("dbo.Complaint", "ProductId", "dbo.Product");
            RenameColumn(table: "dbo.Complaint", name: "ProductId", newName: "Product_ProductId");
            CreateIndex("dbo.Complaint", "Product_ProductId");
            AddForeignKey("dbo.Complaint", "Product_ProductId", "dbo.Product", "ProductId");
        }
    }
}
