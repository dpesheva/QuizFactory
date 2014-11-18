namespace QuizFactory.Mvc.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;

    public class QuizMainInfoViewModel : IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Author { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public decimal Rating { get; set; }

        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        public string Category { get; set; }

        [Display(Name = "Number of questions")]
        public int NumberQuestions { get; set; }

        public virtual void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<QuizDefinition, QuizMainInfoViewModel>()
                         .ForMember(dest => dest.NumberQuestions, opts => opts.MapFrom(src => src.QuestionsDefinitions.Where(o => !o.IsDeleted).Count()));

            configuration.CreateMap<QuizDefinition, QuizMainInfoViewModel>()
                         .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author.UserName));

            configuration.CreateMap<QuizDefinition, QuizMainInfoViewModel>()
                         .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}