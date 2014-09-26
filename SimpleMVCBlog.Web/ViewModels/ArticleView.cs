using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class ArticleView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortBrief { get; set; }
        public string MainContent { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool CanEditOrDelete { get; set; }
        public string CreatedDisplayName { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CreatedUserId { get; set; }
        public string LoggedDisplayName { get; set; }
        public List<CommentView> Comments { get; set; }
        public List<KeywordView> Keywords { get; set; }

        public double VoteAverage { get; set; }
    }
}