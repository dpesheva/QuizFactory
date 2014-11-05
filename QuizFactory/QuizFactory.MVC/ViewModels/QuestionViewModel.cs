using QuizFactory.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace QuizFactory.Mvc.ViewModels
{
    public class QuestionViewModel
    {
        public static Expression<Func<QuestionDefinition, QuestionViewModel>> FromQuestionDefinition
        {
            get
            {
                return question => new QuestionViewModel
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    QuizTitle = question.QuizDefinition.Title,
                    Answers = question.AnswersDefinitions.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        Question = a.QuestionDefinition.QuestionText
                    }).ToList()
                };
            }
        }

        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Display(Name = "Quiz Title")]
        public string QuizTitle { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}