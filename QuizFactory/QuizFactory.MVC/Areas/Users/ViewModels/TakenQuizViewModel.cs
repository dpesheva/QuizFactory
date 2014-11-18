namespace QuizFactory.Mvc.Areas.Users.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;

    public class TakenQuizViewModel : IMapFrom<TakenQuiz>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Taken on")]
        public DateTime CreatedOn { get; set; }

        [Range(0, 100)]
        public int Score { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<TakenQuiz, TakenQuizViewModel>()
                         .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.QuizDefinition.Title));
        }
    }
}