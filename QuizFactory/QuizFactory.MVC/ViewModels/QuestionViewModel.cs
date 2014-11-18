namespace QuizFactory.Mvc.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using QuizFactory.Data.Models;

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
                    Number = question.Number,
                    QuizId = question.QuizDefinition.Id,
                    Answers = question.AnswersDefinitions.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public int QuizId { get; set; }

        public IList<AnswerViewModel> Answers { get; set; }

        public int CorrectAnswerId { get; set; }
    }
}