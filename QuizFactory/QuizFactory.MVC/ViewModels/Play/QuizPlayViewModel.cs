namespace QuizFactory.Mvc.ViewModels.Play
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;
    using QuizFactory.Mvc.ViewModels;

    public class QuizPlayViewModel : QuizMainInfoViewModel, IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        public ICollection<QuestionPlayViewModel> Questions { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<QuizDefinition, QuizPlayViewModel>()
                .ForMember(q => q.Questions, options => options.MapFrom(q => q.QuestionsDefinitions));
        }
    }
}