namespace QuizFactory.Mvc.ViewModels.Play
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;
    using QuizFactory.Mvc.ViewModels;
    using System.Collections;

    public class QuizPlayViewModel : QuizMainInfoViewModel, IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        public IList<QuestionPlayViewModel> Questions { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<QuizDefinition, QuizPlayViewModel>()
                .ForMember(q => q.Questions, options => options.MapFrom(q => q.QuestionsDefinitions.Where(o => !o.IsDeleted)))
                .ReverseMap();

            configuration.CreateMap<QuizDefinition, QuizPlayViewModel>()
                        .ForMember(dest => dest.NumberQuestions, opts => opts.MapFrom(src => src.QuestionsDefinitions.Where(o => !o.IsDeleted).Count()));

            configuration.CreateMap<QuizDefinition, QuizPlayViewModel>()
                         .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author.UserName))
                         .ReverseMap();

            configuration.CreateMap<QuizDefinition, QuizPlayViewModel>()
                         .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name))
                         .ReverseMap();

            Mapper.CreateMap<QuizDefinition, QuizPlayViewModel>().ReverseMap();
        }
    }
}