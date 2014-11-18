namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using MvcPaging;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Mvc.Filters;

    [Authorize]
    public class QuizUsersController : BaseController
    {
        private const int PageSize = 10;

        public QuizUsersController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Index(int? page)
        {
            var allQuizzes = this.GetAllQuizzes();
            this.ViewBag.Pages = Math.Ceiling((double)allQuizzes.Count() / PageSize);

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return this.View(allQuizzes.ToPagedList(currentPageIndex, PageSize));
        }

        [OwnerOrAdminAttribute("id")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return this.View(this.GetViewModelById(id));
        }

        [OwnerOrAdminAttribute("id")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizUserViewModel quizViewModel = this.GetViewModelById(id);
            this.ViewBag.CategoryId = new SelectList(this.Db.Categories.All().ToList(), "Id", "Name", quizViewModel.CategoryId);

            return this.View(quizViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuizUserViewModel quizViewModel)
        {
            if (this.ModelState.IsValid)
            {
                // create new and disable the old //TODO Manage questions
                QuizDefinition newQuiz = this.CreateQuiz(quizViewModel, false);

                this.Db.QuizzesDefinitions.Delete(quizViewModel.Id);

                this.Db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(quizViewModel);
        }

        // GET: User/QuizAdministration/Delete/5
        [OwnerOrAdminAttribute("id")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return this.View(this.GetViewModelById(id));
        }

        // POST: ***/QuizAdministration/Delete/5
        [OwnerOrAdminAttribute("id")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var quiz = this.Db.QuizzesDefinitions
                           .SearchFor(q => q.Id == id)
                           .FirstOrDefault();

            this.Db.QuizzesDefinitions.Delete(quiz);
            this.Db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        // GET: User/QuizAdministration/Create
        public ActionResult Create()
        {
            this.ViewBag.CategoryId = new SelectList(this.Db.Categories.All().ToList(), "Id", "Name");
            return this.View();
        }

        // POST: User/QuizAdministration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuizUserViewModel quizViewModel)
        {
            if (this.ModelState.IsValid)
            {
                QuizDefinition newQuiz = this.CreateQuiz(quizViewModel, false);
                this.Db.SaveChanges();

                quizViewModel.Id = newQuiz.Id;

                return this.RedirectToAction("Index");
            }

            return this.View(quizViewModel);
        }

        private void MapViewModelToModel(QuizUserViewModel quizViewModel, QuizDefinition dbQuiz, bool replace)
        {
            var category = this.Db.Categories.SearchFor(c => c.Id == quizViewModel.CategoryId).FirstOrDefault();
            if (category == null)
            {
                throw new HttpException("Category not found.");
            }

            dbQuiz.Title = quizViewModel.Title;
            dbQuiz.CategoryId = quizViewModel.CategoryId;
            dbQuiz.IsPublic = quizViewModel.IsPublic;

            var user = this.Db.Users.Find(this.User.Identity.GetUserId());

            dbQuiz.Author = user;
            dbQuiz.QuestionsDefinitions = new List<QuestionDefinition>();
        }

        // return all quizzes by user
        private List<QuizUserViewModel> GetAllQuizzes()
        {
            var userId = this.User.Identity.GetUserId();

            var allQuizzes = this.Db.QuizzesDefinitions
                                 .All()
                                 .Where(q => q.Author.Id == userId)
                                 .Select(QuizUserViewModel.FromQuizDefinition)
                                 .ToList();

            return allQuizzes;
        }

        private QuizDefinition CreateQuiz(QuizUserViewModel quizViewModel, bool replace)
        {
            QuizDefinition newQuiz = new QuizDefinition();
            this.MapViewModelToModel(quizViewModel, newQuiz, replace);
            this.Db.QuizzesDefinitions.Add(newQuiz);
            return newQuiz;
        }

        private QuizUserViewModel GetViewModelById(int? id)
        {
            var userId = this.User.Identity.GetUserId();

            var quizViewModel = this.Db.QuizzesDefinitions
                                    .All()
                                    .Where(q => q.Id == id && q.Author.Id == userId)
                                    .Select(QuizUserViewModel.FromQuizDefinition)
                                    .FirstOrDefault();

            return quizViewModel;
        }
    }
}