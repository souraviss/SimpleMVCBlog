using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.Models
{
    public class Comment
    {
        public Comment()
        {
            CreatedTime = DateTime.Now;
        }
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
     
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int ArticleId { get; set; }
        public DateTime CreatedTime { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("ArticleId")]
        public virtual Article Article { get; set; }
    }
}