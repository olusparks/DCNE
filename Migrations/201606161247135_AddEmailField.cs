namespace DCNE_Cake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Email", c => c.String(nullable: false));
            
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Email");
           
        }
    }
}
