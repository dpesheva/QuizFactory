namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    using MvcPaging;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.ViewModels;
    using QuizFactory.Data;
    using System.Collections;
    using System.Collections.Generic;

    public class HomeController : BaseController
    {
        const int PageSize = 3;

        public HomeController(IQuizFactoryData data)
            : base(data)
        {
        }

        private Random Random = new Random();

        public ActionResult Index(int? catId)
        {
            this.ViewBag.CategoryId = catId;
            var category = this.db.Categories.Find(catId);

            if (category != null)
            {
                this.ViewBag.Category = category.Name;
            }

            ViewBag.Pages = Math.Ceiling((double)db.QuizzesDefinitions.All().Count() / PageSize);

            return this.View();
        }

        [OutputCache(Duration = 10 * 60)]
        public ActionResult Categories()
        {
            var categories = this.db.Categories.All().ToList();
            return this.PartialView("_CategoriesPartial", categories);
        }

        public ActionResult GetRandomQuizzes()
        {
            if (this.db.QuizzesDefinitions.All().Any())
            {
                int rnd = this.Random.Next(this.db.QuizzesDefinitions.All().Count());

                var ramdomQuizzes = this.db.QuizzesDefinitions
                                        .All()
                                        .Where(q => q.IsPublic == true)
                                        .OrderBy(e => rnd)
                                        .Take(3)
                                        .Project()
                                        .To<QuizMainInfoViewModel>()
                                        .ToList();

                return this.PartialView("_RandomQuizBoxesPartial", ramdomQuizzes);
            }

            return null; // TODO return DataErrorInfoModelValidatorProvider msg
        }

        public ActionResult RecentContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.CreatedOn);
            var quizzes = this.ProjectQuery(quizzesQuery, page, catId);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.PartialView("_ListQuizBoxesPartial", quizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult PopularContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.TakenQuizzes.Count);
            var quizzes = this.ProjectQuery(quizzesQuery, page, catId);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.PartialView("_ListQuizBoxesPartial", quizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult ByNameContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderBy(q => q.Title);
            var quizzes = this.ProjectQuery(quizzesQuery, page, catId);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.PartialView("_ListQuizBoxesPartial", quizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult ByRatingContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                this.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null; // TODO return error
            }

            var quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(q => q.Rating);
            var quizzes = this.ProjectQuery(quizzesQuery, page, catId);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.PartialView("_ListQuizBoxesPartial", quizzes.ToPagedList(currentPageIndex, PageSize));
        }

        private IEnumerable<QuizMainInfoViewModel> ProjectQuery(IQueryable<QuizDefinition> quizzesQuery, int? page, int? catId)
        {
            ViewBag.Pages = Math.Ceiling((double)quizzesQuery.Count() / PageSize);

            var filteredQuizzes = quizzesQuery;

            if (catId != null)
            {
                filteredQuizzes = quizzesQuery.Where(q => q.Category.Id == catId);
            }

            return filteredQuizzes.Where(q => q.IsPublic == true)
                               .Project()
                               .To<QuizMainInfoViewModel>()
                               .ToList();
        }
    }
}