namespace ArthasPub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItem", "OrderDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItem", "OrderDate");
        }
    }
}
