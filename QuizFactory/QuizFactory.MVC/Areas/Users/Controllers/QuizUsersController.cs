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
    public class QuizUsersController : AbstractController
    {
        public QuizUsersController(IQuizFactoryData data)
            : base(data)
        {
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            QuizViewModel quizViewModel = this.GetViewModelById(id);
            return this.View(quizViewModel);
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
                // TODO create new and disable the old
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

            QuizViewModel quizViewModel = this.GetViewModelById(id);

            return this.View(quizViewModel);
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
                QuizDefinition newQuiz = new QuizDefinition();
                this.MapViewModelToModel(quizViewModel, newQuiz);

                this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().ToList(), "Id", "Name", quizViewModel.CategoryId);

                this.db.QuizzesDefinitions.Add(newQuiz);
                this.db.SaveChanges();

                quizViewModel.Id = newQuiz.Id;

                return this.RedirectToAction("Index");
            }

            return this.View(quizViewModel);
        }

        protected override void MapViewModelToModel(IQuizViewModel quizViewModel, QuizDefinition newQuiz)
        {
            base.MapViewModelToModel(quizViewModel, newQuiz);

            var user = this.db.Users.Find(this.User.Identity.GetUserId());
            
            newQuiz.Author = user;
            newQuiz.QuestionsDefinitions = new List<QuestionDefinition>();
        }

        //return all quizzes by user
        protected override List<QuizViewModel> GetAllQuizzes()
        {
            var userId = this.User.Identity.GetUserId();

            var allQuizzes = this.db.QuizzesDefinitions
                                 .All()
                                 .Where(q => q.Author.Id == userId)
                                 .Select(QuizViewModel.FromQuizDefinition)
                                 .ToList();

            return allQuizzes;
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