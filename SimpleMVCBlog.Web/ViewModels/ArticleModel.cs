using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class ArticleModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ShortBrief { get; set; }
        [Required]
        [AllowHtml]
        public string MainContent { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public List<KeywordView> Keywords { get; set; }
    }
}