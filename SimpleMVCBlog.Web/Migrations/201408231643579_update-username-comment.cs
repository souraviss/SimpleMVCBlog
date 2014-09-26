namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateusernamecomment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "UserName", c => c.String(nullable: false));
        }
    }
}
