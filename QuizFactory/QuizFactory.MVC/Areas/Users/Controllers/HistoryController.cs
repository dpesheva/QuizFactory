namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using MvcPaging;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Mvc.ViewModels.Play;

    [Authorize]
    public class HistoryController : BaseController
    {
        private const int PageSize = 10;

        public HistoryController(IQuizFactoryData data)
            : base(data)
        {
        }

        // GET: Users/History
        public ActionResult Index(int? page)
        {
            var userId = this.User.Identity.GetUserId();

            var allTakenQuizzes = this.db.TakenQuizzes
                                      .All()
                                      .Where(q => q.UserId == userId)
                                      .Project()
                                      .To<TakenQuizViewModel>()
                                      .ToList();

            this.ViewBag.Pages = Math.Ceiling((double)allTakenQuizzes.Count / PageSize);
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            return this.View(allTakenQuizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult Details(int? id)
        {
            var takenQuiz = this.db.TakenQuizzes
                                .All()
                                .Where(t => t.Id == id)
                                .FirstOrDefault();

            var quizDef = this.db.QuizzesDefinitions
                              .AllWithDeleted()
                              .Where(q => q.Id == takenQuiz.QuizDefinitionId)
                              .Project()
                              .To<QuizPlayViewModel>()
                              .FirstOrDefault();

            if (takenQuiz == null || quizDef == null)
            {
                throw new HttpException("Quiz not found");
            }

            Dictionary<int, int> selectedAnswersInt = this.CollectAnswers(takenQuiz);
            this.TempData["results"] = selectedAnswersInt;
            this.TempData["scorePercentage"] = takenQuiz.Score;

            var userId = User.Identity.GetUserId();
            ViewBag.Voted = this.db.Votes.All().Where(v => v.QuizId == quizDef.Id && v.UserId == userId).Any();

            return this.View("DisplayAnswers", quizDef);
        }

        private Dictionary<int, int> CollectAnswers(TakenQuiz takenQuiz)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            var userAnswers = takenQuiz.UsersAnswers;

            foreach (var item in userAnswers)
            {
                result.Add(item.AnswerDefinition.QuestionDefinition.Id, item.AnswerDefinitionId);
            }

            return result;
        }
    }
}