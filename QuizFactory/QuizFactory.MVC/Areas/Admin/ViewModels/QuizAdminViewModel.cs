namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Common.ViewModels;
    using QuizFactory.Mvc.Mapping;

    public class QuizAdminViewModel : QuizViewModel, IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }

        //  public ICollection<QuestionAdminViewModel> Questions { get; set; }
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<QuizViewModel, QuizDefinition>();
            
            configuration.CreateMap<QuizViewModel, QuizDefinition>()
                            .ForMember(dest => dest.Author, opts => opts.Ignore());            
            configuration.CreateMap<QuizViewModel, QuizDefinition>()
                            .ForMember(dest => dest.Category, opts => opts.Ignore());

            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                            .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author.UserName));

            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                            .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}