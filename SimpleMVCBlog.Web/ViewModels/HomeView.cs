using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class HomeView
    {
        public List<ArticleView> Lastest {get;set;}
        public List<ArticleView> Hostest {get;set;}

    }
}