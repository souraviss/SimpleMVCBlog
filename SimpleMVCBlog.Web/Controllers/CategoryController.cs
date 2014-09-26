using SimpleMVCBlog.Web.Models;
using SimpleMVCBlog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVCBlog.Web.Controllers
{

    public class CategoryController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSubCatsBySuperCatId(int? superCatId)
        {
            var list = (superCatId!=null && superCatId != 0 ? db.SubCategories.Where(subCat=> subCat.SuperCatId == superCatId):db.SubCategories)
                .Select(cat => new CategoryModel
                {
                    Id = cat.Id,
                    Name = cat.Name
                }).ToList();
            return Json(list , JsonRequestBehavior.AllowGet);
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