using AutoMapper;
using SimpleMVCBlog.Web.Models;
using SimpleMVCBlog.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleMVCBlog.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            ConfigureDomainToView();
            ConfigureViewToDomain();
        }

        private static void ConfigureDomainToView()
        {
            Mapper.CreateMap<ArticleModel, Article>();
            Mapper.CreateMap<CommentModel, Comment>();
        }

        private static void ConfigureViewToDomain()
        {
            Mapper.CreateMap<Article, ArticleModel>();
            Mapper.CreateMap<Article, ArticleView>()
                .ForMember(x => x.CreatedDisplayName, opt => opt.MapFrom(src => src.CreatedUser.DisplayName));
              

            Mapper.CreateMap<Comment, CommentView>()
                .ForMember(x=>x.UserName, opt=>opt.MapFrom(src=> string.IsNullOrEmpty(src.UserId)?src.UserName : src.User.DisplayName));

            Mapper.CreateMap<Keyword, KeywordView>();
            Mapper.CreateMap<ApplicationUser, ProfileView>();
            Mapper.CreateMap<SuperCategory, SuperCategoryView>();
            Mapper.CreateMap<SubCategory, CategoryModel>();
        }
    }
}