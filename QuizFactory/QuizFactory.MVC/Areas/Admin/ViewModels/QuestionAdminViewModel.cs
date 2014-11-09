namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class QuestionAdminViewModel
    {
        public static Expression<Func<QuestionDefinition, QuestionAdminViewModel>> FromQuestionDefinition
        {
            get
            {
                return question => new QuestionAdminViewModel
                {
                    Id = question.Id,
                    QuestionText = question.QuestionText,
                    Number = question.Number,
                    IsDeleted = question.IsDeleted,
                    DeletedOn = question.DeletedOn,
                    ModifiedOn = question.ModifiedOn,
                    Answers = question.AnswersDefinitions.Select(a => new AnswerViewModel
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsCorrect = a.IsCorrect,
                        Position = a.Position,
                    }).ToList()
                };
            }
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string QuestionText { get; set; }

        [Required]
        public int Number { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }

        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}