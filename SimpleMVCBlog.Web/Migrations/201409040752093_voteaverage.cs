namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voteaverage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "VoteAverage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "VoteAverage");
        }
    }
}
