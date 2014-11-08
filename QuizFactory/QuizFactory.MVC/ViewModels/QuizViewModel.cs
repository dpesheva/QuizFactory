namespace QuizFactory.Mvc.ViewModels
{
    using QuizFactory.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class QuizViewModel
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
                    IsPublic = quiz.IsPublic,
                    CreatedOn = quiz.CreatedOn,
                    Author = quiz.Author.UserName,
                    NumberQuestions = quiz.QuestionsDefinitions.Count.ToString(),
                    Questions = quiz.QuestionsDefinitions.Select(q => new QuestionViewModel
                    {
                        Id = q.Id,
                        QuestionText = q.QuestionText,
                        Number = q.Number,
                        // QuizTitle = q.QuizDefinition.Title,
                        Answers = q.AnswersDefinitions.Select(a => new AnswerViewModel
                        {
                            Id = a.Id,
                            Text = a.Text,
                            IsCorrect = a.IsCorrect,
                            Position = a.Position,
                            //  Question = a.QuestionDefinition.QuestionText
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

        public string Category { get; set; }

        [Display(Name = "Number of questions")]
        public string NumberQuestions { get; set; }

        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}