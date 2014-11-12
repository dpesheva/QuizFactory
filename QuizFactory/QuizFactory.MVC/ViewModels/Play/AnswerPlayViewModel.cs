namespace QuizFactory.Mvc.ViewModels.Play
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;

    public class AnswerPlayViewModel : IMapFrom<AnswerDefinition>, IHaveCustomMappings
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
            Mapper.CreateMap<AnswerDefinition, AnswerPlayViewModel>()
                  .ForMember(a => a.QuestionId, options => options.MapFrom(a => a.QuestionDefinition.Id))
                  .ReverseMap();

            Mapper.CreateMap<AnswerDefinition, AnswerPlayViewModel>().ReverseMap();
        }
    }
}