namespace QuizFactory.Mvc.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;

    public class AnswerViewModel : IMapFrom<AnswerDefinition>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Text { get; set; }

        [Required]
        public int Position { get; set; }

        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<AnswerDefinition, AnswerViewModel>()
               .ForMember(a => a.QuestionId, options => options.MapFrom(a => a.QuestionDefinition.Id))
               .ReverseMap();

            Mapper.CreateMap<AnswerDefinition, AnswerViewModel>().ReverseMap();
        }
    }
}