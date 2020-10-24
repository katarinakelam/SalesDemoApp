using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DemoSalesApp.Models;
using DemoSalesApp.ViewModels.ViewModels;

namespace DemoSalesApp.AutoMapper
{
    /// <summary>
    /// The automapper profile.
    /// </summary>
    public class Mapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mapper"/> class.
        /// </summary>
        public Mapper()
        {
            this.CreateMap<SaleEvent, SaleEventViewModel>()
                .ForMember(dest => dest.ArticleSoldPrice, opt => opt.MapFrom(src => src.ArticleSoldPrice));
            this.CreateMap<Article, ArticleViewModel>();
        }
    }
}
