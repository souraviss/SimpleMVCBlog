using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.Models
{
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int SuperCatId { get; set; }

        [ForeignKey("SuperCatId")]
        public virtual SuperCategory SuperCat { get; set; }
        public virtual ICollection<Article> Articles { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}