using QuizFactory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace QuizFactory.Mvc.ViewModels
{
    public class AnswerViewModel
    {
        public static Expression<Func<AnswerDefinition, AnswerViewModel>> FromAnswerDefinition
        {
            get
            {
                return answer => new AnswerViewModel
                {
                    Id = answer.Id,
                    Text = answer.Text,
                    IsCorrect = answer.IsCorrect,
                    Question = answer.QuestionDefinition.QuestionText
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Text { get; set; }

        [Display(Name = "Correct")]
        public bool IsCorrect { get; set; }

        public string Question { get; set; }
    }
}