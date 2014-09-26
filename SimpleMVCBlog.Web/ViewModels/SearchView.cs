using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class SearchView
    {
        public ArticlesListView ArticlesListView { get; set; }
        public string Query { get; set; }
    }
}