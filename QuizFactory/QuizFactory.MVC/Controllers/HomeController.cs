namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using QuizFactory.Mvc.ViewModels;
    using QuizFactory.Models;

    public class HomeController : BaseController
    {
        Random Random = new Random();

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
                int rnd = Random.Next();

                var ramdomQuizzes = this.db.QuizzesDefinitions
                    .All()
                    .Where(q => q.IsPublic == true)
                    .OrderBy(e => rnd)
                    .Take(3)
                    .Project()
                    .To<QuizMainInfoViewModel>()
                    .ToList();

                return this.PartialView("_ListQuizBoxesPartial", ramdomQuizzes);
            }

            return null; // TODO return DataErrorInfoModelValidatorProvider msg
        }

        public ActionResult RecentContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.CreatedOn);
            var quizzes = RojectQuery(quizzesQuery);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult PopularContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.TakenQuizzes.Count);
            var quizzes = RojectQuery(quizzesQuery);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult ByNameContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderBy(q => q.Title);
            var quizzes = RojectQuery(quizzesQuery);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult ByRatingContent()
        {
            if (!Request.IsAjaxRequest())
            {
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.Rating);
            var quizzes = RojectQuery(quizzesQuery);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        private object RojectQuery(IOrderedQueryable<QuizDefinition> quizzesQuery)
        {
            return quizzesQuery.Where(q => q.IsPublic == true)
                 .Project()
                 .To<QuizMainInfoViewModel>()
                 .ToList();
        }



    }
}