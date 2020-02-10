namespace ArthasPub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancelorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItem", "Cancel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItem", "Cancel");
        }
    }
}
