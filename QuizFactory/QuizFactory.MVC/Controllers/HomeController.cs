namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels;

    public class HomeController : BaseController
    {
        private Random Random = new Random();

        public ActionResult Index(int? catId)
        {
            this.ViewBag.CategoryId = catId;

            return this.View();
        }

        public ActionResult Categories()
        {
            var categories = this.db.Categories.All().Where(c => c.IsDeleted == false).ToList();
            return this.PartialView("_CategoriesPartial", categories);
        }

        public ActionResult GetRandomQuizzes()
        {
            if (this.db.QuizzesDefinitions.All().Any())
            {
                int rnd = this.Random.Next();

                var ramdomQuizzes = this.db.QuizzesDefinitions
                                        .All()
                                        .Where(q => q.IsPublic == true && q.IsDeleted == false)
                                        .OrderBy(e => rnd)
                                        .Take(3)
                                        .Project()
                                        .To<QuizMainInfoViewModel>()
                                        .ToList();

                return this.PartialView("_ListQuizBoxesPartial", ramdomQuizzes);
            }

            return null; // TODO return DataErrorInfoModelValidatorProvider msg
        }

        public ActionResult RecentContent(int? catId)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.CreatedOn);
            var quizzes = this.RojectQuery(quizzesQuery, catId);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult PopularContent(int? catId)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.TakenQuizzes.Count);
            var quizzes = this.RojectQuery(quizzesQuery, catId);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult ByNameContent(int? catId)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderBy(q => q.Title);
            var quizzes = this.RojectQuery(quizzesQuery, catId);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        public ActionResult ByRatingContent(int? catId)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.Rating);
            var quizzes = this.RojectQuery(quizzesQuery, catId);

            return this.PartialView("_ListQuizBoxesPartial", quizzes);
        }

        private object RojectQuery(IOrderedQueryable<QuizDefinition> quizzesQuery, int? catId)
        {
            if (catId == null)
            {
                return quizzesQuery.Where(q => q.IsPublic == true && q.IsDeleted == false)
                                   .Project()
                                   .To<QuizMainInfoViewModel>()
                                   .ToList();
            }

            return quizzesQuery.Where(q => q.IsPublic == true && q.Category.Id == catId && q.IsDeleted == false)
                               .Project()
                               .To<QuizMainInfoViewModel>()
                               .ToList();
        }
    }
}