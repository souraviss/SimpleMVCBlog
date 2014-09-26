using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
        //public DbSet<ArticleVote> ArticleVotes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<SuperCategory> SuperCategories { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        //public System.Data.Entity.DbSet<SimpleMVCBlog.Web.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}