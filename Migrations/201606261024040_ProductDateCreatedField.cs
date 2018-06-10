namespace DCNE_Cake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductDateCreatedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "DateCreated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "DateCreated");
        }
    }
}
