namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.Controllers;

    [Authorize]
    public class QuizUsersController : HomeController
    {
        public QuizUsersController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var allQuizzes = this.GetAllQuizzes();
            return this.View(allQuizzes);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return this.View(this.GetViewModelById(id));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizViewModel quizViewModel = this.GetViewModelById(id);
            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizViewModel.CategoryId);

            return this.View(quizViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuizViewModel quizViewModel)
        {
            if (this.ModelState.IsValid)
            {
                // create new and disable the old //TODO Manage questions
                this.CreateQuiz(quizViewModel, true);
                this.db.QuizzesDefinitions.Delete(quizViewModel.Id);

                this.db.SaveChanges();
                return this.RedirectToAction("Index");
            }

            return this.View(quizViewModel);
        }

        // GET: User/QuizAdministration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return this.View(this.GetViewModelById(id));
        }

        // POST: ***/QuizAdministration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var quiz = this.db.QuizzesDefinitions
                           .SearchFor(q => q.Id == id)
                           .FirstOrDefault();

            this.db.QuizzesDefinitions.Delete(quiz);
            this.db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        // GET: User/QuizAdministration/Create
        public ActionResult Create()
        {
            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name");
            return this.View();
        }

        // POST: User/QuizAdministration/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuizViewModel quizViewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.CreateQuiz(quizViewModel, false);
                this.db.SaveChanges();

                this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizViewModel.CategoryId);

                return this.RedirectToAction("Edit", quizViewModel);
            }

            return this.View(quizViewModel);
        }

        private void MapViewModelToModel(IQuizViewModel quizViewModel, QuizDefinition dbQuiz, bool replace)
        {
            var category = this.db.Categories.SearchFor(c => c.Id == quizViewModel.CategoryId).FirstOrDefault();
            if (category == null)
            {
                return; // TODO
            }

            dbQuiz.Title = quizViewModel.Title;
            dbQuiz.CategoryId = quizViewModel.CategoryId;
            dbQuiz.IsPublic = quizViewModel.IsPublic;

            var user = this.db.Users.Find(this.User.Identity.GetUserId());

            dbQuiz.Author = user;
            dbQuiz.QuestionsDefinitions = new List<QuestionDefinition>();
        }

        //return all quizzes by user
        private List<QuizViewModel> GetAllQuizzes()
        {
            var userId = this.User.Identity.GetUserId();

            var allQuizzes = this.db.QuizzesDefinitions
                                 .All()
                                 .Where(q => q.Author.Id == userId)
                                 .Select(QuizViewModel.FromQuizDefinition)
                                 .ToList();

            return allQuizzes;
        }

        private void CreateQuiz(QuizViewModel quizViewModel, bool replace)
        {
            QuizDefinition newQuiz = new QuizDefinition();
            this.MapViewModelToModel(quizViewModel, newQuiz, replace);
            this.db.QuizzesDefinitions.Add(newQuiz);
            quizViewModel.Id = newQuiz.Id;
        }

        private QuizViewModel GetViewModelById(int? id)
        {
            var userId = this.User.Identity.GetUserId();

            var quizViewModel = this.db.QuizzesDefinitions
                                    .SearchFor(q => q.Id == id && q.Author.Id == userId)
                                    .Select(QuizViewModel.FromQuizDefinition)
                                    .FirstOrDefault();

            return quizViewModel;
        }
    }
}