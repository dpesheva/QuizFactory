namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using MvcPaging;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Mvc.ViewModels.Play;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class ScoreController : BaseController
    {
        private const int PageSize = 10;

        public ScoreController(IQuizFactoryData data)
            : base(data)
        {
        }

        // GET: Users/Score
        public ActionResult Index(int? page)
        {
            var userId = this.User.Identity.GetUserId();

            var allTakenQuizzes = this.db.TakenQuizzes
                .All()
                .Where(q => q.UserId == userId)
                .Project()
                .To<TakenQuizViewModel>()
                .ToList();


            ViewBag.Pages = Math.Ceiling((double)allTakenQuizzes.Count / PageSize);
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;

            return View(allTakenQuizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult Details(int? id)
        {
            var takenQuiz = this.db.TakenQuizzes
                .All()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            var quizDef = this.db.QuizzesDefinitions
                .All()
                .Where(q => q.Id == takenQuiz.QuizDefinitionId)
                .Project()
                .To<QuizPlayViewModel>()
                .FirstOrDefault();

            if (takenQuiz == null || quizDef == null)
            {
                return this.Redirect("Error"); // TODO 
            }

            Dictionary<int, int> selectedAnswersInt = CollectAnswers(takenQuiz);
            TempData["results"] = selectedAnswersInt;

            return this.View("DisplayAnswers", quizDef);
        }
 
        private Dictionary<int, int> CollectAnswers(TakenQuiz takenQuiz)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            var userAnswers = takenQuiz.UsersAnswers;

            foreach (var item in userAnswers)
            {
                result.Add(item.AnswerDefinition.QuestionDefinition.Number, item.AnswerDefinitionId);
            }

            return result;
        }

    }
}