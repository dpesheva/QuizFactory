namespace QuizFactory.Mvc.Areas.Users.ViewModels
{
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;

    public class QuizViewModel : IQuizViewModel
    {
        public static Expression<Func<QuizDefinition, QuizViewModel>> FromQuizDefinition
        {
            get
            {
                return quiz => new QuizViewModel
                {
                    Id = quiz.Id,
                    Title = quiz.Title,
                    Category = quiz.Category.Name,
                    CategoryId = quiz.CategoryId,
                    IsPublic = quiz.IsPublic,
                    CreatedOn = quiz.CreatedOn,
                    Author = quiz.Author.UserName,
                    NumberQuestions = quiz.QuestionsDefinitions.Count.ToString(),
                    Questions = quiz.QuestionsDefinitions.Select(q => new QuestionViewModel
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        Number = q.Number,
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

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Author { get; set; }

        [Display(Name = "Created on")]
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        public decimal Rating { get; set; }

        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        [UIHint("CategoryViewModel")]
        public string Category { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Questions")]
        [HiddenInput(DisplayValue = false)]
        public string NumberQuestions { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}