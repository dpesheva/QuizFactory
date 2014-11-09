namespace QuizFactory.Mvc.Areas.Users.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using QuizFactory.Mvc.ViewModels;
    using QuizFactory.Mvc.Controllers;
    using QuizFactory.Data.Models;

    [Authorize]
    public class QuizUsersController : AbstractController
    {
        // GET: User/QuizAdministration/Create
        public ActionResult Create()
        {
            this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().Where(c => c.IsDeleted == false).ToList(), "Id", "Name");
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
                MapViewModelToModel(quizViewModel, newQuiz);

                this.ViewBag.CategoryId = new SelectList(this.db.Categories.All().Where(c => c.IsDeleted == false).ToList(), "Id", "Name", quizViewModel.CategoryId);

                this.db.QuizzesDefinitions.Add(newQuiz);
                this.db.SaveChanges();

                quizViewModel.Id = newQuiz.Id;

                return this.RedirectToAction("Index");
            }

            return this.View(quizViewModel);
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

        protected override QuizViewModel GetViewModelById(int? id)
        {
            // TODO Refactor
            if (id == null)
            {
                // TODO
            }

            var userId = this.User.Identity.GetUserId();

            var quizAdminViewModel = this.db.QuizzesDefinitions
                                         .SearchFor(q => q.Id == id && q.Author.Id == userId)
                                         .Select(QuizViewModel.FromQuizDefinition)
                                         .FirstOrDefault();

            return quizAdminViewModel;
        }

        protected override void MapViewModelToModel(QuizViewModel quizViewModel, QuizDefinition newQuiz)
        {
            base.MapViewModelToModel(quizViewModel, newQuiz);

            var user = this.db.Users.Find(User.Identity.GetUserId());
            if (user == null)
            {
                return; // TODO
            }

            newQuiz.Author = user;
            newQuiz.QuestionsDefinitions = new List<QuestionDefinition>();
        }
    }
}