namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createedtimearticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "CreatedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Articles", "PublishedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "PublishedTime");
            DropColumn("dbo.Articles", "CreatedTime");
        }
    }
}
