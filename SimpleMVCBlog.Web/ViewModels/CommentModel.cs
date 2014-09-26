using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class CommentModel
    {
        public int Id { get; set; }

        [MinLength(10, ErrorMessage = "Nội dung phải chứa hơn 10 kí tự")]
        [AllowHtml]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        [Required(ErrorMessage="Phải nhập họ tên")]
        public string UserName { get; set; }
        
        
    }
}