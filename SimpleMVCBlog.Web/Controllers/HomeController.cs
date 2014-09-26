using SimpleMVCBlog.Web.Models;
using SimpleMVCBlog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVCBlog.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            var view = new HomeView
            {
                Lastest = AutoMapper.Mapper.Map<List<ArticleView>>(db.Articles.OrderBy(a=>a.CreatedTime).Take(5).ToList()),
                Hostest = AutoMapper.Mapper.Map<List<ArticleView>>(db.Articles.OrderBy(a => a.Comments.Count).Take(5).ToList()),
            };
            return View(view);
        }

        public ActionResult Timeline()
        {
            return View();
        }

        public ActionResult Search(string query, int page = 1)
        {
             var list = new ArticlesListView() {
                Articles = AutoMapper.Mapper.Map<List<ArticleView>>(db.Articles.Where(a=>a.MainContent.Contains(query) || a.ShortBrief.Contains(query) || a.Title.Contains(query)).ToList()),
                CurrentPage = 1,
                PageCount = 1
            };
             var searchView = new SearchView
             {
                 ArticlesListView = list,
                 Query = query
             };
            return View(searchView);
        }
        public PartialViewResult NavBar()
        {
            List<SuperCategoryView> superCats = AutoMapper.Mapper.Map<List<SuperCategoryView>>(db.SuperCategories.ToList());
            return PartialView("_NavBar", superCats);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}