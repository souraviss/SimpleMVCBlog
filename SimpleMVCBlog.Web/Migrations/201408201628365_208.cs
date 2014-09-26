namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _208 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CreatedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CreatedTime");
        }
    }
}
