using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVCBlog.Web.Models
{
    public class Article
    {
        public Article()
        {
            CreatedTime = DateTime.Now;
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortBrief { get; set; }
        [Required]
        public string MainContent { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string CreatedUserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? PublishedTime { get; set; }

        public virtual SubCategory Category { get; set; }
        [ForeignKey("CreatedUserId")]
        public virtual ApplicationUser CreatedUser { get; set; }
        public virtual ICollection<Keyword> Keywords { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}