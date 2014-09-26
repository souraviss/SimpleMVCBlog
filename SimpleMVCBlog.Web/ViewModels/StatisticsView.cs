using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web.ViewModels
{
    public class StatisticsView
    {
        public int ArticlesCount { get; set; }
        public int VotesCount {get;set;}
        public double VoteAverage { get; set; }
    }
}