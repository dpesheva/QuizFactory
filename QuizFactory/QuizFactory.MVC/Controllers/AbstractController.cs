namespace QuizFactory.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using QuizFactory.Data.Models;
    using QuizFactory.Mvc.Areas.Users.ViewModels;
    using System.Linq.Expressions;
    using QuizFactory.Data;

    public abstract class AbstractController : BaseController
    {
        public AbstractController(IQuizFactoryData data)
            : base(data)
        {
        }

        // GET: ***/QuizAdministration
        public ActionResult Index()
        {
            var allQuizzes = this.GetAllQuizzes();
            return this.View(allQuizzes);
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

        protected virtual void MapViewModelToModel(IQuizViewModel quizViewModel, QuizDefinition quiz)
        {
            var category = this.db.Categories.SearchFor(c => c.Id == quizViewModel.CategoryId).FirstOrDefault();
            if (category == null)
            {
                return; // TODO
            }

            quiz.Title = quizViewModel.Title;
            quiz.CategoryId = quizViewModel.CategoryId;
            quiz.IsPublic = quizViewModel.IsPublic;
        }

        protected virtual List<QuizViewModel> GetAllQuizzes()
        {
            var allQuizzes = this.db.QuizzesDefinitions
                                  .All()
                                  .Select(QuizViewModel.FromQuizDefinition)
                                  .ToList();

            return allQuizzes;
        }
    }
}