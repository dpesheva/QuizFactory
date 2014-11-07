using QuizFactory.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuizFactory.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Categories()
        {
            var categories = this.db.Categories.All().ToList();
            return this.PartialView("_CategoriesPartial", categories);
        }


        public ActionResult TopMostPopular()
        {
            if (this.db.QuizzesDefinitions.All().Any())
            {
                // TODO return 3 random quizzes
                var topPopular = this.db.QuizzesDefinitions.All().Take(3).Select(QuizViewModel.FromQuizDefinition).ToList();
                return this.PartialView("_ListQuizBoxesPartial", topPopular);
            }

            return null; // TOTO return DataErrorInfoModelValidatorProvider msg
        }

        public ActionResult RecentContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            List<QuizViewModel> quizzes = this.db.QuizzesDefinitions
                .All()
                .OrderByDescending(q => q.CreatedOn)
                .Select(QuizViewModel.FromQuizDefinition)
                .ToList();

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult PopularContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            // TOTO add constraints, orderby
            List<QuizViewModel> quizzes = this.db.QuizzesDefinitions.All()
                .OrderByDescending(q => q.TakenQuizzes.Count)
                .Select(QuizViewModel.FromQuizDefinition)
                .ToList();

            return this.PartialView("_ListQuizBoxesPartial", quizzes); ;// TODO return smthng
        }

        public ActionResult ByNameContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            List<QuizViewModel> quizzes = this.db.QuizzesDefinitions.All()
                .OrderBy(q => q.Title)
                .Select(QuizViewModel.FromQuizDefinition)
                .ToList();

            return this.PartialView("_ListQuizBoxesPartial", quizzes);// TODO return smthng
        }

        public ActionResult ByRatingContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            List<QuizViewModel> quizzes = this.db.QuizzesDefinitions.All()
               .OrderByDescending(q => q.Rating)
               .Select(QuizViewModel.FromQuizDefinition)
               .ToList();

            return this.PartialView("_ListQuizBoxesPartial", quizzes);// TODO return smthng
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


    }
}