using SimpleMVCBlog.Web.Models;
using SimpleMVCBlog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleMVCBlog.Web.Helpers;
using System.IO;

namespace SimpleMVCBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        private AppDbContext db;
        public CommentController()
        {
            db = new AppDbContext();
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Recent()
        {
            var list = AutoMapper.Mapper.Map<List<CommentView>>(db.Comments.OrderByDescending(cmt => cmt.Id).Take(5));
            return PartialView(list);
        }

        [HttpPost]
        public JsonResult Create(CommentModel commentModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                ModelState.Remove("UserName");
            }
            if(ModelState.IsValid)
            {
                Comment comment = AutoMapper.Mapper.Map<Comment>(commentModel);
                if(User.Identity.IsAuthenticated)
                {
                    comment.UserId = User.Identity.GetUserId();
                }
                db.Comments.Add(comment);
                db.SaveChanges();
                db.Entry(comment).Reference(cmt => cmt.User).Load();
                var viewModel = AutoMapper.Mapper.Map<CommentView>(comment);
                
                viewModel.CanDelete = User.Identity.IsAuthenticated && (User.IsInRole("Admin") || User.IsInRole("SuperAdmin") || User.Identity.GetUserId() == comment.UserId);
                return Json(new { success = true, content = RenderPartialViewToString("Details", viewModel) });
            }
            else
            {
                return Json(new { success = false, content = RenderPartialViewToString("CommentForm", commentModel) });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            if(User.Identity.IsAuthenticated)
            {
                var comment = db.Comments.Find(id);
                if(comment==null)
                {
                    return Json(new { success = false, content = "Không tồn tại bình luận này!!!" });
                }
                else
                {
                    if (!User.IsInRole("Admin") && !User.IsInRole("SuperAdmin"))
                    {
                        if (comment.UserId != User.Identity.GetUserId())
                        {
                            return Json(new { success = false, content = "Không có quyền xóa bình luận này!!!" });
                        }
                    }
                    //Là admin hoặc là người tạo comment
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                    return Json(new { success = true, content = "Xóa thành công", id = comment.Id});
                }
            }
            else
            {
                return Json(new {success=false, content = "Bạn chưa đăng nhập. Không có quyền xóa bình luận này!!!"});
            }
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}