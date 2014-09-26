using SimpleMVCBlog.Web.Models;
using SimpleMVCBlog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVCBlog.Web.Controllers
{
    
    public class KeywordController : Controller
    { 
        private AppDbContext db;
        public KeywordController()
        {
            db = new AppDbContext();
        }
        // GET: Keyword
        public JsonResult JsonList()
        {
            var list = db.Keywords.Select(kw => kw.Name).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Popular()
        {
            var list = AutoMapper.Mapper.Map<List<KeywordView>>(db.Keywords.OrderBy(kw => kw.Articles.Count).ToList());
            return PartialView("Popular", list);
        }
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}