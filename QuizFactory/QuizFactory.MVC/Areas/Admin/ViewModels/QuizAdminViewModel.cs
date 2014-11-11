namespace QuizFactory.Mvc.Areas.Admin.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using System.Web.Mvc;

    public class QuizAdminViewModel : QuizViewModel
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
                    CategoryId = quiz.CategoryId,
                    IsPublic = quiz.IsPublic,
                    CreatedOn = quiz.CreatedOn,
                    Author = quiz.Author.UserName,
                    ModifiedOn = quiz.ModifiedOn,
                    NumberQuestions = quiz.QuestionsDefinitions.Count.ToString(),
                    //Questions = quiz.QuestionsDefinitions.Select(q => new QuestionAdminViewModel
                    //{
                    //    Id = q.Id,
                    //    QuestionText = q.QuestionText,
                    //    Number = q.Number,
                    //    Answers = q.AnswersDefinitions.Select(a => new AnswerViewModel
                    //    {
                    //        Id = a.Id,
                    //        Text = a.Text,
                    //        IsCorrect = a.IsCorrect,
                    //        Position = a.Position
                    //    }).ToList()
                    //}).ToList()
                };
            }
        }

        [HiddenInput(DisplayValue=false)]
        [Display(Name = "Modified On")]
        public DateTime? ModifiedOn { get; set; }

      //  public ICollection<QuestionAdminViewModel> Questions { get; set; }
    }
}