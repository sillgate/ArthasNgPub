namespace ArthasPub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removecartid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Order", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "CartId", c => c.Int(nullable: false));
        }
    }
}
