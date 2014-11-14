namespace QuizFactory.Mvc.Areas.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using QuizFactory.Mvc.Mapping;
    using QuizFactory.Data.Models;

    public abstract class QuizViewModel:IMapFrom<QuizDefinition>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "Created on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }
         
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }
        
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

            //  public ICollection<QuestionViewModel> Questions { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<QuizDefinition, QuizViewModel>()
                         .ForMember(dest => dest.NumberQuestions, opts => opts.MapFrom(src => src.QuestionsDefinitions.Where(o => !o.IsDeleted).Count()));

            configuration.CreateMap<QuizDefinition, QuizViewModel>()
                         .ForMember(dest => dest.Category, opts => opts.MapFrom(src => src.Category.Name));
        }
    }
}