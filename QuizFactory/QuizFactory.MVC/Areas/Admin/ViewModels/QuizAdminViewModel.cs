namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;

    public class QuizAdminViewModel : IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Created on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }
                
        [Range(0, 5)]
        public decimal Rating { get; set; }

        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        public string Category { get; set; }

        [Required]
        [UIHint("CategoryViewModel")]
        public int CategoryId { get; set; }

        [Display(Name = "Questions")]
        [HiddenInput(DisplayValue = false)]
        public string NumberQuestions { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            Mapper.CreateMap<QuizAdminViewModel, QuizDefinition>();
            
            configuration.CreateMap<QuizAdminViewModel, QuizDefinition>()
                         .ForMember(dest => dest.Author, opts => opts.Ignore());            
            configuration.CreateMap<QuizAdminViewModel, QuizDefinition>()
                         .ForMember(dest => dest.Category, opts => opts.Ignore());

            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                         .ForMember(dest => dest.NumberQuestions, opts => opts.MapFrom(src => src.QuestionsDefinitions.Where(o => !o.IsDeleted).Count().ToString()));

            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                         .ForMember(dest => dest.Author, opts => opts.MapFrom(src => src.Author.UserName));

            configuration.CreateMap<QuizDefinition, QuizAdminViewModel>()
                         .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}