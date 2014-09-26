namespace SimpleMVCBlog.Web.Migrations
{
    using SimpleMVCBlog.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleMVCBlog.Web.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(SimpleMVCBlog.Web.Models.AppDbContext context)
        {
           
            context.Roles.AddOrUpdate(role => role.Id,
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {
                    Id = "Registered",
                    Name = "Registered"
                },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {
                    Id = "Moderator",
                    Name = "Moderator"
                },
                new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {
                    Id = "Admin",
                    Name = "Admin"
                }
                );
            context.SuperCategories.AddOrUpdate(scat => scat.Id,
                new SuperCategory { Id = 1, Name = "Lap trinh can ban"},
                new SuperCategory { Id = 2, Name = "Cau truc du lieu" },
                new SuperCategory { Id = 3, Name = "Giai thuat" },
                new SuperCategory { Id = 4, Name = "Lap trinh web" },
                new SuperCategory { Id = 5, Name = "Lap trinh di dong" });

            context.SubCategories.AddOrUpdate(scat => scat.Id,
                new SubCategory { Id = 1, Name = "Khai niem can ban", SuperCatId = 1 },
                new SubCategory { Id = 2, Name = "Toan tu, ham", SuperCatId = 1 },
                new SubCategory { Id = 3, Name = "MVC", SuperCatId = 4 },
                new SubCategory { Id = 4, Name = "Javascript & JQuery", SuperCatId = 4 },
                new SubCategory { Id = 5, Name = "IOS", SuperCatId = 5 },
                new SubCategory { Id = 6, Name= "Tìm đường đi ngắn nhất", SuperCatId = 3},
                new SubCategory { Id = 6, Name = "Sắp sếp", SuperCatId = 3 },
                 new SubCategory { Id = 7, Name = "Phương pháp lập trình", SuperCatId = 1 });
            context.Keywords.AddOrUpdate(kw => kw.Id,
                new Keyword {Id = "jQuery", Name = "jQuery" },
                new Keyword {Id= "MVC", Name = "MVC" });
        }
    }
}
