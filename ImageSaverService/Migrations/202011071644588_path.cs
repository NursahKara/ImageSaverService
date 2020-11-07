namespace ImageSaverService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class path : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageModels", "Path", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImageModels", "Path");
        }
    }
}
