using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.Models
{
    public class Vote
    {
        public int Value { get; set; }
        [Key, Column(Order=0)]
        public int ArticleId {get;set;}
         [Key, Column(Order = 1)]
        public string UserId { get; set; }

        public virtual Article Article { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}