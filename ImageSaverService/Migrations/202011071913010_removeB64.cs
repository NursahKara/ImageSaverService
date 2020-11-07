namespace ImageSaverService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeB64 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ImageModels", "Base64");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageModels", "Base64", c => c.String(nullable: false));
        }
    }
}
