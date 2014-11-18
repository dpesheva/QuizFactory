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
    using System.Collections.Generic;
    using System.Linq.Expressions;

    [HandleError]
    public class HomeController : BaseController
    {
        private const int PageSize = 6;
        private static readonly Random random = new Random();

        public HomeController(IQuizFactoryData data)
            : base(data)
        {
        }

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
                int rnd = random.Next(this.db.QuizzesDefinitions.All().Count());

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

            return new HttpStatusCodeResult(HttpStatusCode.NotFound, "No available quzzes");
        }

        public ActionResult Search(string search, int? page)
        {
            search = search ?? "";
            var quizzes = this.db.QuizzesDefinitions
                .All()
                .Where(q => q.Title.ToLower().Contains(search.ToLower()))
                .Project()
                .To<QuizMainInfoViewModel>()
                .ToList();
            ViewBag.SearchString = search;
            ViewBag.Pages = Math.Ceiling((double)quizzes.Count() / PageSize);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.View(quizzes.ToPagedList(currentPageIndex, PageSize));
        }

        public ActionResult RecentContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = GetData(catId, q => q.CreatedOn, false);
            return FormatOutput(page, quizzes);
        }

        public ActionResult PopularContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = GetData(catId, q => q.TakenQuizzes.Count, false);
            return FormatOutput(page, quizzes);
        }

        public ActionResult ByNameContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = GetData(catId, q => q.Title, true);
            return FormatOutput(page, quizzes);
        }

        public ActionResult ByRatingContent(int? catId, int? page)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            var quizzes = GetData(catId, q => q.Rating, false);
            return FormatOutput(page, quizzes);
        }

        public ActionResult Error()
        {
            return this.View();
        }

        private IEnumerable<QuizMainInfoViewModel> GetData<TOrderBy>(int? catId, Expression<Func<QuizDefinition, TOrderBy>> predicate, bool asc)
        {
            IQueryable<QuizDefinition> quizzesQuery;

            if (asc)
            {
                quizzesQuery = this.db.QuizzesDefinitions.All().OrderBy(predicate);
            }
            else
            {
                quizzesQuery = this.db.QuizzesDefinitions.All().OrderByDescending(predicate);
            }

            Expression<Func<QuizDefinition, bool>> filter = (q => q.IsPublic == true);

            if (catId != null)
            {
                filter = q => (q.IsPublic == true) && (q.Category.Id == catId);
            }

            return quizzesQuery.Where(filter)
                               .Project()
                               .To<QuizMainInfoViewModel>()
                               .ToList();

        }

        private ActionResult FormatOutput(int? page, IEnumerable<QuizMainInfoViewModel> quizzes)
        {
            ViewBag.Pages = Math.Ceiling((double)quizzes.Count() / PageSize);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.PartialView("_ListQuizBoxesPartial", quizzes.ToPagedList(currentPageIndex, PageSize));
        }
    }
}