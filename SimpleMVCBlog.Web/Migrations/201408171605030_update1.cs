namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "ArticleId", c => c.Int(nullable: false));
            AlterColumn("dbo.SubCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SuperCategories", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            AlterColumn("dbo.SuperCategories", "Name", c => c.Int(nullable: false));
            AlterColumn("dbo.SubCategories", "Name", c => c.Int(nullable: false));
            DropColumn("dbo.Comments", "ArticleId");
        }
    }
}
