using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class CommentView
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Content { get; set; }
        public bool CanDelete { get; set; }
    }
}