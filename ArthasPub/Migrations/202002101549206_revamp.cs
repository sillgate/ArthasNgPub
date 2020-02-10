namespace ArthasPub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Disable", c => c.Boolean(nullable: false));
            DropColumn("dbo.Item", "Visible");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "Visible", c => c.Boolean(nullable: false));
            DropColumn("dbo.Item", "Disable");
        }
    }
}
