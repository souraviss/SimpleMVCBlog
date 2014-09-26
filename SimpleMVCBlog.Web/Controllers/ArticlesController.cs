using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleMVCBlog.Web.Models;
using SimpleMVCBlog.Web.ViewModels;
using System.Data.Entity.Validation;

namespace SimpleMVCBlog.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Articles
        public ActionResult Index(int page = 1, int take = 10)
        {
            if (page < 1) page = 1;

            ArticlesListView viewModel = new ArticlesListView();
            var articlesQuery = db.Articles;
            viewModel.Articles = AutoMapper.Mapper.Map<List<ArticleView>>(articlesQuery.OrderBy(a=>a.CreatedTime).Skip((page-1)*take).Take(take).ToList());
            viewModel.Articles.ForEach(a => a.CanEditOrDelete = User.Identity.IsAuthenticated && (a.CreatedUserId == User.Identity.GetUserId() || User.IsInRole("Admin") || User.IsInRole("Moderator")));
            viewModel.PageCount = Math.Ceiling(articlesQuery.Count() / 10.0d);
            viewModel.CurrentPage = page;
            return View(viewModel);
        }

        [Authorize]
        public ActionResult MyArticles(int page = 1, int take = 10)
        {
            if (page < 1) page = 1;
            var userId = User.Identity.GetUserId();

            ArticlesListView viewModel = new ArticlesListView();
            var articlesQuery = db.Articles.Where(a=>a.CreatedUserId == userId);
            viewModel.Articles = AutoMapper.Mapper.Map<List<ArticleView>>(articlesQuery.OrderBy(a => a.CreatedTime).Skip((page - 1) * take).Take(take).ToList());
            viewModel.Articles.ForEach(a=>a.CanEditOrDelete = User.Identity.IsAuthenticated && (a.CreatedUserId == User.Identity.GetUserId() || User.IsInRole("Admin") || User.IsInRole("Moderator")));
            viewModel.PageCount = Math.Ceiling(articlesQuery.Count() / 10.0d);
            viewModel.CurrentPage = page;
            return View(viewModel);

        }

        [Authorize]
        public ActionResult MyStatistics()
        {
            var userId = User.Identity.GetUserId();
            var myArticles= db.Articles.Where(a=>a.CreatedUserId== userId).ToList();
            var view = new StatisticsView 
            {
                ArticlesCount = myArticles.Count
            };
            return PartialView("_MyStatistics", view);
        }
        public PartialViewResult Recent()
        {
            var recentArticles = AutoMapper.Mapper.Map<List<ArticleView>>(db.Articles.OrderBy(a => a.CreatedTime).Take(5).ToList());

            return PartialView(recentArticles);
        }
        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var viewModel = AutoMapper.Mapper.Map<ArticleView>(article);
            if (User.Identity.IsAuthenticated)
                viewModel.LoggedDisplayName = db.Users.Find(User.Identity.GetUserId()).DisplayName;
            viewModel.Comments.ForEach(cmt => cmt.CanDelete = User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("SuperAdmin") || User.Identity.GetUserId() == cmt.UserId));
            return View(viewModel);
        }

        // GET: Articles/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.SuperCategories = new SelectList(db.SuperCategories, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.SubCategories, "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                var article = AutoMapper.Mapper.Map<Article>(model);
                article.CreatedUserId = User.Identity.GetUserId();
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuperCategories = new SelectList(db.SuperCategories, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.SubCategories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuperCategories = new SelectList(db.SuperCategories, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.SubCategories, "Id", "Name", article.CategoryId);
            return View(AutoMapper.Mapper.Map<ArticleModel>(article));
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var article = db.Articles.Find(model.Id);
                    article.MainContent = model.MainContent;
                    article.ShortBrief = model.ShortBrief;
                    article.CategoryId = model.CategoryId;
                    article.Title = model.Title;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = model.Id});
                }
                catch (DbEntityValidationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            ViewBag.SuperCategories = new SelectList(db.SuperCategories, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.SubCategories, "Id", "Name", model.CategoryId);
            return View(model);
        }

        // GET: Articles/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CommentCount(int id)
        {
            var count = db.Comments.Count(cmt => cmt.ArticleId == id);
            return Content(count.ToString());
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
