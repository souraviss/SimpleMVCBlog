namespace SimpleMVCBlog.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createduseridarticle : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Articles", name: "UserId", newName: "CreatedUserId");
            RenameIndex(table: "dbo.Articles", name: "IX_UserId", newName: "IX_CreatedUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Articles", name: "IX_CreatedUserId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Articles", name: "CreatedUserId", newName: "UserId");
        }
    }
}
