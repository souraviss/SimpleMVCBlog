using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class ArticlesListView
    {
        public List<ArticleView> Articles { get; set; }
        public double PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}