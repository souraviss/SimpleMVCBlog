using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.Models
{
    public class SuperCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<SubCategory> SubCats { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}