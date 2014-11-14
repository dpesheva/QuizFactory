namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using QuizFactory.Mvc.Areas.Common.ViewModels;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;
    using AutoMapper;

    public class QuizAdminViewModel : QuizViewModel, IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }

        //  public ICollection<QuestionAdminViewModel> Questions { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                         .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author.UserName));
           
            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                         .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}