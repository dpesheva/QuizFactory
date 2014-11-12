namespace QuizFactory.Mvc.ViewModels
{
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Mapping;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class AnswerViewModel : IMapFrom<AnswerDefinition>
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Text { get; set; }

        [Required]
        [UIHint("RadioButtonList")]
        public int Position { get; set; }

        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }

        // public string Question { get; set; }
    }
}