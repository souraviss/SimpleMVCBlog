namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Avartar", c => c.String());
            AddColumn("dbo.AspNetUsers", "About", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "About");
            DropColumn("dbo.AspNetUsers", "Avartar");
        }
    }
}
