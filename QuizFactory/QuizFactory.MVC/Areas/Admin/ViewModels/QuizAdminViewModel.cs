namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    using QuizFactory.Models;
    using QuizFactory.Mvc.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class QuizAdminViewModel
    {
        public static Expression<Func<QuizDefinition, QuizAdminViewModel>> FromQuizDefinition
        {
            get
            {
                return quiz => new QuizAdminViewModel
                {
                    Id = quiz.Id,
                    Title = quiz.Title,
                    Category = quiz.Category.Name,
                    IsPublic = quiz.IsPublic,
                    CreatedOn = quiz.CreatedOn,
                    Author = quiz.Author.UserName,
                    IsDeleted = quiz.IsDeleted,
                    UpdatedOn = quiz.UpdatedOn,
                    NumberQuestions = quiz.QuestionsDefinitions.Count.ToString(),
                    Questions = quiz.QuestionsDefinitions.Select(q => new QuestionAdminViewModel
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        Number = q.Number,
                        IsDeleted = q.IsDeleted,
                        UpdatedOn = q.UpdatedOn,
                        Answers = q.AnswersDefinitions.Select(a => new AnswerViewModel
                        {
                            Id = a.Id,
                            Text = a.Text,
                            IsCorrect = a.IsCorrect,
                            Position = a.Position
                        }).ToList()
                    }).ToList()
                };
            }
        }

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

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

        public string Category { get; set; }

        [Display(Name = "Number of questions")]
        public string NumberQuestions { get; set; }

        public ICollection<QuestionAdminViewModel> Questions { get; set; }
    }
}