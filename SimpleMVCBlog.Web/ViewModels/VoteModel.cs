using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class VoteModel
    {
        public int Value { get; set; }
        public int ArticleId { get; set; }
        public bool IsVoted { get; set; }
        public double VoteAverage {get;set;}
        public int VoteCount { get; set; }
    }
}