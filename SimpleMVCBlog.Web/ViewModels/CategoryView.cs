using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class SuperCategoryView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryModel> SubCats { get; set; }
    }

    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}