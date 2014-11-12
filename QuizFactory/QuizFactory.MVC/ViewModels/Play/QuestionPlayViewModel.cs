namespace QuizFactory.Mvc.ViewModels.Play
{
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    
    public class QuestionPlayViewModel : IMapFrom<QuestionDefinition>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }

        public int QuizId { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
        
        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<QuestionDefinition, QuestionPlayViewModel>()
                .ForMember(a => a.Answers, opt => opt.MapFrom(a => a.AnswersDefinitions));
        }
    }
}