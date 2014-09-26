namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        ArticleId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.UserId })
                .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "ArticleId" });
            DropTable("dbo.Votes");
        }
    }
}
